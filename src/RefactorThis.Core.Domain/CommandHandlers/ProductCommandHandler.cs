using MediatR;
using RefactorThis.Core.Domain.Commands;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Notifications;
using RefactorThis.Core.Domain.Events;
using RefactorThis.Core.Domain.Extensions;
using RefactorThis.Core.Domain.Interfaces;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.CommandHandlers
{
    public class ProductCommandHandler : CommandHandler,
        IRequestHandler<UpdateProductCommand>,
        IRequestHandler<CreateProductCommand>,

        IRequestHandler<RemoveProductCommand>
    {
        private readonly IProductsRepository _repository;
        private readonly IMediatorHandler Bus;

        public ProductCommandHandler(IProductsRepository productRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _repository = productRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(CreateProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            var product = new Product(Guid.NewGuid(), message.Name, message.Description, message.Price, message.DeliveryPrice, message.ToProductOptions());

            var existingProduct = _repository.GetById(product.Id);

            if (existingProduct != null)
            {
                var busMessage = String.Format(CultureInfo.InvariantCulture, "This product id ({0}) is already taken.", message.Id);
                Bus.RaiseMediatorEvent(new DomainNotification(message.MessageType, busMessage));

                return Unit.Task;
            }

            _repository.Add(product);

            if (Commit())
            {
                Bus.RaiseMediatorEvent(new ProductCreatedEvent(product.Id, product.Name, product.Description, product.Price, product.DeliveryPrice));
            }

            return Unit.Task;
        }

        public Task<Unit> Handle(UpdateProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            var product = new Product(message.Id, message.Name, message.Description, message.Price, message.DeliveryPrice, message.ToProductOptions());
            var existingProduct = _repository.GetById(product.Id);

            if (existingProduct == null)
            {
                var busMessage = String.Format(CultureInfo.InvariantCulture, "This product id ({0}) doesn't exist.", message.Id);
                Bus.RaiseMediatorEvent(new DomainNotification(message.MessageType, busMessage));
                return Unit.Task;
            }

            _repository.Update(product);

            if (Commit())
            {
                Bus.RaiseMediatorEvent(new ProductUpdatedEvent(product.Id, product.Name, product.Description, product.Price, product.DeliveryPrice));
            }

            return Unit.Task;
        }

        public Task<Unit> Handle(RemoveProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            _repository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseMediatorEvent(new ProductRemovedEvent(message.Id));
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
