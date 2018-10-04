using Moq;
using RefactorThis.Core.Domain.CommandHandlers;
using RefactorThis.Core.Domain.Commands;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Notifications;
using RefactorThis.Core.Domain.Extensions;
using RefactorThis.Core.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace RefactorThis.Core.UnitTests.Domain.CommandHandlers
{
    public class ProductCommandTests
    {
        [Fact]
        public void CreateProductCommandIsValidTrue()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;
            var createProductcommand = new CreateProductCommand(mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            handler.Handle(createProductcommand, CancellationToken.None);
        }

        [Fact]
        public void CreateProductCommandIsValidFalseMissingName()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;
            mock.Name = "";

            var createProductcommand = new CreateProductCommand(mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            createProductcommand.IsValid().ShouldBeFalse();
            createProductcommand.ValidationResult.Errors.Count().ShouldEqual(2);
        }

        [Fact]
        public void CreateProductCommandIsValidFalseMissingPrice()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;
            mock.Price = 0;

            var createProductcommand = new CreateProductCommand(mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            createProductcommand.IsValid().ShouldBeFalse();
            createProductcommand.ValidationResult.Errors.Count().ShouldEqual(2);
        }

        [Fact]
        public void UpdateProductCommandIsValidFalseMissingId()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;
            mock.Id = Guid.Empty;

            var createProductcommand = new UpdateProductCommand(mock.Id, mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            createProductcommand.IsValid().ShouldBeFalse();
            createProductcommand.ValidationResult.Errors.Count().ShouldEqual(1);
        }

        [Fact]
        public void IsValidFalseMissingPriceAndName()
        {
            var repo = new Mock<IProductsRepository>();
            var uow = new Mock<IUnitOfWork>();
            var bus = new Mock<IMediatorHandler>();
            var notifications = new Mock<DomainNotificationHandler>();
            var handler = new ProductCommandHandler(repo.Object, uow.Object, bus.Object, notifications.Object);
            var mock = Mocks.ProductMocks.NewProductiPhoneXS;
            mock.Price = 0;
            mock.Name = "";
            var createProductcommand = new CreateProductCommand(mock.Name, mock.Description, mock.Price, mock.DeliveryPrice, mock.ProductOptions.ToProductOptionsDTO().ToList());
            createProductcommand.IsValid().ShouldBeFalse();
            createProductcommand.ValidationResult.Errors.Count().ShouldEqual(4);
        }
    }
}
