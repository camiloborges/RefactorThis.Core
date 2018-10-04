using Moq;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.CommandHandlers;
using RefactorThis.Core.Domain.Commands;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Notifications;
using RefactorThis.Core.Domain.Events;
using RefactorThis.Core.Domain.Extensions;
using RefactorThis.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace RefactorThis.Core.UnitTests.Domain.CommandHandlers
{
    public class ProductOptionCommandHandlerTests
    {
        [Fact]
        public void AddProductOptionSuccess()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mockProduct = Mocks.ProductMocks.NewProductiPhoneXS;
            var mock = mockProduct.ProductOptions.First();

            repo.Setup(r => r.GetById(mockProduct.Id)).Returns(mockProduct);
            repo.Setup(r => r.Update(It.IsAny<Product>())).Verifiable();

            uow.Setup(u => u.Commit()).Returns(true);
            var createProductcommand = new CreateProductOptionCommand(mock.ProductId, mock.Name, mock.Description);
            handler.Handle(createProductcommand, CancellationToken.None);
            repo.Verify(r=> r.Update(It.IsAny<Product>()),Times.Once);
            bus.Verify(b => b.RaiseMediatorEvent<ProductOptionCreatedEvent>(It.IsAny<ProductOptionCreatedEvent>()), Times.Once);
        }

        [Fact]
        public void AddProductOptionAlreadyExistsError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mockProduct = Mocks.ProductMocks.NewProductiPhoneXS;
            var mock = mockProduct.ProductOptions.First();

            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetById(mockProduct.Id)).Returns(mockProduct);

            repo.Setup(r => r.GetProductOption(mock.ProductId,mock.Id)).Returns(mock);
            //adding an 'exception' should code attempt to add product
            repo.Setup(r => r.AddProductOption(It.IsAny<ProductOption>())).Throws<Exception>();

            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var createProductcommand = new CreateProductOptionCommand(mock.ProductId, mock.Name, mock.Description);
            handler.Handle(createProductcommand, CancellationToken.None);
            repo.Verify();
            bus.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<ProductCreatedEvent>(It.IsAny<ProductCreatedEvent>()), Times.Never);
        }

        [Fact]
        public void AddProductOptionFailedValidationError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS.ProductOptions.First();
            mock.Name = "";
            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetProductOption(mock.ProductId, It.IsAny<Guid>())).Returns(mock);
            //adding an 'exception' should code attempt to add product
            repo.Setup(r => r.AddProductOption(It.IsAny<ProductOption>())).Throws<Exception>();

            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productCommand = new CreateProductOptionCommand(mock.ProductId, mock.Name, mock.Description);
            handler.Handle(productCommand, CancellationToken.None);
            repo.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<DomainNotification>(It.IsAny<DomainNotification>()), Times.Exactly(2));
            bus.Verify(b => b.RaiseMediatorEvent<ProductCreatedEvent>(It.IsAny<ProductCreatedEvent>()), Times.Never);
        }

        [Fact]
        public void UpdateProductOptionSuccess()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            repo.Setup(r => r.GetProductOption(mock.ProductId, mock.Id)).Returns(mock);

            uow.Setup(u => u.Commit()).Returns(true);
            var productCommand = new UpdateProductOptionCommand(mock.ProductId, mock.Id, mock.Name, mock.Description);
            handler.Handle(productCommand, CancellationToken.None);
            bus.Verify(b => b.RaiseMediatorEvent<ProductOptionUpdatedEvent>(It.IsAny<ProductOptionUpdatedEvent>()), Times.Once);
        }

        [Fact]
        public void UpdateProductOptionKeyNotFoundError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS.ProductOptions.First();

            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetProductOption(mock.ProductId,mock.Id)).Returns(mock);

            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productcommand = new UpdateProductOptionCommand(mock.ProductId, mock.Id, mock.Name, mock.Description);
            handler.Handle(productcommand, CancellationToken.None);
            repo.Verify();
            bus.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<DomainNotification>(It.IsAny<DomainNotification>()), Times.Exactly(1));
            bus.Verify(b => b.RaiseMediatorEvent<ProductOptionUpdatedEvent>(It.IsAny<ProductOptionUpdatedEvent>()), Times.Never);
        }

        [Fact]
        public void UpdateProductOptionFailedValidationError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS.ProductOptions.First();
            mock.Name = "";
            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetProductOption(mock.ProductId, mock.Id)).Returns(mock);
            //adding an 'exception' should code attempt to add product
            repo.Setup(r => r.Add(It.IsAny<Product>())).Throws<Exception>();

            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productCommand = new UpdateProductOptionCommand(mock.ProductId, mock.Id, mock.Name, mock.Description);
            handler.Handle(productCommand, CancellationToken.None);
            repo.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<DomainNotification>(It.IsAny<DomainNotification>()), Times.Exactly(2));
            bus.Verify(b => b.RaiseMediatorEvent<ProductOptionUpdatedEvent>(It.IsAny<ProductOptionUpdatedEvent>()), Times.Never);
        }

        [Fact]
        public void RemoveProductOptionSuccess()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            repo.Setup(r => r.GetProductOption(mock.ProductId, mock.Id)).Returns(mock);

            uow.Setup(u => u.Commit()).Returns(true);
            var command = new RemoveProductOptionCommand(mock.ProductId, mock.Id);
            handler.Handle(command, CancellationToken.None);
            bus.Verify(b => b.RaiseMediatorEvent<ProductOptionRemovedEvent>(It.IsAny<ProductOptionRemovedEvent>()), Times.Once);
        }

        [Fact]
        public void RemoveProductOptionKeyNotFoundError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS.ProductOptions.First();

            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.Remove(It.IsAny<Guid>())).Throws<KeyNotFoundException>();

            var handler = new ProductOptionCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productcommand = new RemoveProductOptionCommand(mock.ProductId, mock.Id);
            var allGood = true;
            try ///TODO: correct Assertion wasn't working. please fix it
            {
                var result = handler.Handle(productcommand, CancellationToken.None);
            }
            catch
            {
                allGood = false;
            }
            allGood.ShouldBeFalse();
            bus.Verify(b => b.RaiseMediatorEvent<DomainNotification>(It.IsAny<DomainNotification>()), Times.Never);
            bus.Verify(b => b.RaiseMediatorEvent<ProductOptionRemovedEvent>(It.IsAny<ProductOptionRemovedEvent>()), Times.Never);
        }
    }
}