using MediatR;
using RefactorThis.Core.Domain.Commands;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Notifications;
using RefactorThis.Core.Domain.Events;
using RefactorThis.Core.Domain.Interfaces;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.CommandHandlers
{
    public class ProductOptionCommandHandler : CommandHandler,
        IRequestHandler<UpdateProductOptionCommand>,
        IRequestHandler<CreateProductOptionCommand>,

        IRequestHandler<RemoveProductOptionCommand>
    {
        private readonly IProductsRepository _repository;
        private readonly IMediatorHandler Bus;

        public ProductOptionCommandHandler(IProductsRepository productRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _repository = productRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(CreateProductOptionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            var productOption = new ProductOption(Guid.NewGuid(), message.ProductId, message.Name, message.Description);

            var existingProduct = _repository.GetById(message.ProductId);

            if (existingProduct == null)
            {
                var busMessage = String.Format(CultureInfo.InvariantCulture, "Product id ({0}) not found .", message.Id);
                Bus.RaiseMediatorEvent(new DomainNotification(message.MessageType, busMessage));
                return Unit.Task;
            }
            existingProduct.ProductOptions.Add(productOption);
            _repository.Update(existingProduct);

            if (Commit())
            {
                Bus.RaiseMediatorEvent(new ProductOptionCreatedEvent(productOption.Id, productOption.ProductId, productOption.Name, productOption.Description));
            }

            return Unit.Task;
        }

        public Task<Unit> Handle(UpdateProductOptionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            var product = new ProductOption(message.Id, message.ProductId, message.Name, message.Description);
            var existingOption = _repository.GetProductOption(message.ProductId, message.Id);

            if (existingOption == null)
            {
                var busMessage = String.Format(CultureInfo.InvariantCulture, "This product id ({0}) doesn't exist.", message.Id);
                Bus.RaiseMediatorEvent(new DomainNotification(message.MessageType, busMessage));
                return Unit.Task;
            }

            _repository.UpdateProductOption(product);

            if (Commit())
            {
                Bus.RaiseMediatorEvent(new ProductOptionUpdatedEvent(product.Id, product.ProductId));
            }

            return Unit.Task;
        }

        public Task<Unit> Handle(RemoveProductOptionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            _repository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseMediatorEvent(new ProductOptionRemovedEvent(message.ProductId, message.Id));
            }

            return Unit.Task;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        //Task<Unit> IRequestHandler<CreateProductCommand, Unit>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Unit> IRequestHandler<UpdateProductCommand, Unit>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Unit> IRequestHandler<RemoveProductCommand, Unit>.Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
