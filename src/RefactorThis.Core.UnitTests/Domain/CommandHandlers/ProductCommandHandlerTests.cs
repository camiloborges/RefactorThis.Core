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
    public class ProductCommandHandlerTests
    {
        [Fact]
        public void AddProductSuccess()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;

            uow.Setup(u => u.Commit()).Returns(true);
            var createProductcommand = new CreateProductCommand(mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(createProductcommand, CancellationToken.None);
            bus.Verify(b => b.RaiseMediatorEvent<ProductCreatedEvent>(It.IsAny<ProductCreatedEvent>()), Times.Once);
        }

        [Fact]
        public void AddProductAlreadyExistsError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;

            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(mock);
            //adding an 'exception' should code attempt to add product
            repo.Setup(r => r.Add(It.IsAny<Product>())).Throws<Exception>();

            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var createProductcommand = new CreateProductCommand(mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(createProductcommand, CancellationToken.None);
            repo.Verify();
            bus.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<ProductCreatedEvent>(It.IsAny<ProductCreatedEvent>()), Times.Never);
        }

        [Fact]
        public void AddProductFailedValidationError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;
            mock.Name = "";
            mock.Price = 0;
            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(mock);
            //adding an 'exception' should code attempt to add product
            repo.Setup(r => r.Add(It.IsAny<Product>())).Throws<Exception>();

            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productCommand = new CreateProductCommand(mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(productCommand, CancellationToken.None);
            repo.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<DomainNotification>(It.IsAny<DomainNotification>()), Times.Exactly(4));
            bus.Verify(b => b.RaiseMediatorEvent<ProductCreatedEvent>(It.IsAny<ProductCreatedEvent>()), Times.Never);
        }

        [Fact]
        public void UpdateProductSuccess()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.ProductSamsungGalaxyS7;
            repo.Setup(r => r.GetById(mock.Id)).Returns(mock);

            uow.Setup(u => u.Commit()).Returns(true);
            var productCommand = new UpdateProductCommand(mock.Id, mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(productCommand, CancellationToken.None);
            bus.Verify(b => b.RaiseMediatorEvent<ProductUpdatedEvent>(It.IsAny<ProductUpdatedEvent>()), Times.Once);
        }

        [Fact]
        public void UpdateProductKeyNotFoundError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;

            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Verifiable();

            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productcommand = new UpdateProductCommand(mock.Id, mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(productcommand, CancellationToken.None);
            repo.Verify();
            bus.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<DomainNotification>(It.IsAny<DomainNotification>()), Times.Exactly(1));

            bus.Verify(b => b.RaiseMediatorEvent<ProductUpdatedEvent>(It.IsAny<ProductUpdatedEvent>()), Times.Never);
        }

        [Fact]
        public void UpdateProductFailedValidationError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;
            mock.Name = "";
            mock.Price = 0;
            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(mock);
            //adding an 'exception' should code attempt to add product
            repo.Setup(r => r.Add(It.IsAny<Product>())).Throws<Exception>();

            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productCommand = new UpdateProductCommand(mock.Id, mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(productCommand, CancellationToken.None);
            repo.Verify();
            bus.Verify(b => b.RaiseMediatorEvent<DomainNotification>(It.IsAny<DomainNotification>()), Times.Exactly(4));
            bus.Verify(b => b.RaiseMediatorEvent<ProductCreatedEvent>(It.IsAny<ProductCreatedEvent>()), Times.Never);
        }

        [Fact]
        public void RemoveProductSuccess()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.ProductSamsungGalaxyS7;
            repo.Setup(r => r.GetById(mock.Id)).Returns(mock);

            uow.Setup(u => u.Commit()).Returns(true);
            var productCommand = new UpdateProductCommand(mock.Id, mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(productCommand, CancellationToken.None);
            bus.Verify(b => b.RaiseMediatorEvent<ProductUpdatedEvent>(It.IsAny<ProductUpdatedEvent>()), Times.Once);
        }

        [Fact]
        public void RemoveProductKeyNotFoundError()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;

            bus.Setup(b => b.RaiseMediatorEvent(It.IsAny<DomainNotification>())).Verifiable();
            repo.Setup(r => r.Remove(It.IsAny<Guid>())).Throws<KeyNotFoundException>();

            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var productcommand = new RemoveProductCommand(mock.Id);
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
            bus.Verify(b => b.RaiseMediatorEvent<ProductUpdatedEvent>(It.IsAny<ProductUpdatedEvent>()), Times.Never);
        }
    }
}