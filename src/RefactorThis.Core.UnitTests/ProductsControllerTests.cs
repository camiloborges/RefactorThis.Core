using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RefactorThis.Controllers;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Models;
using RefactorThis.Core.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions.AssertExtensions;
using RefactorThis.Core.Core.Extensions;

namespace RefactorThis.Core.UnitTests
{
    public class ProductsControllerTests
    {
        [Fact]
        public void GetAllProductsReturnsAllItems()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.List<Product>()).Returns(ProductMocks.ProductsBaseDataset);

            var controller = new ProductsController(logger.Object, repo.Object);
            var result = controller.Get(null);
            var resultValue = Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<List<Product>>(resultValue.Value);
            repo.Verify();
            okResult.Count().ShouldEqual(2);
        }

        [Fact]
        public void GetProductByNameReturnsSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.ListByName<Product>("Galaxy")).Returns(ProductMocks.ProductsBaseDataset);

            var controller = new ProductsController(logger.Object, repo.Object);
            var result = controller.Get("Galaxy");
            var resultValue = Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<List<Product>>(resultValue.Value);
            repo.Verify();
            okResult.Count().ShouldEqual(2);

            var product = okResult.First();
            product.Name.ShouldEqual(ProductMocks.ProductSamsungGalaxyS7.Name);
        }

        [Fact]
        public void GetProductByNameThatDoesntExistReturnsEmpty()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.ListByName<Product>("Microsoft")).Returns(new List<Product>());

            var controller = new ProductsController(logger.Object, repo.Object);
            var result = controller.Get("Microsoft");
            var resultValue = Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<List<Product>>(resultValue.Value);
            repo.Verify();
            okResult.Count().ShouldEqual(0);
        }

        [Fact]
        public void GetProductByIdReturnsSucessAndWithOptions()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetById<Product>(ProductMocks.ProductSamsungGalaxyS7.Id)).Returns(ProductMocks.ProductSamsungGalaxyS7);
            var controller = new ProductsController(logger.Object, repo.Object);
            var result = controller.GetProduct(ProductMocks.ProductSamsungGalaxyS7.Id);
            var resultValue = Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<Product>(resultValue.Value);
            repo.Verify();
            okResult.Id.ShouldEqual(ProductMocks.ProductSamsungGalaxyS7.Id);
            okResult.Name.ShouldEqual(ProductMocks.ProductSamsungGalaxyS7.Name);
            okResult.ProductOptions.Count().ShouldEqual(2);
        }

        [Fact]
        public void AddProductSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.Add(ProductMocks.NewProductiPhoneXS)).Returns(ProductMocks.NewProductiPhoneXS);
            repo.Setup(r => r.GetById<Product>(ProductMocks.NewProductiPhoneXS.Id)).Returns(ProductMocks.NewProductiPhoneXS);

            var controller = new ProductsController(logger.Object, repo.Object);
            controller.Post(ProductMocks.NewProductiPhoneXS);
            var result = controller.GetProduct(ProductMocks.NewProductiPhoneXS.Id);
            var resultValue = Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<Product>(resultValue.Value);

            okResult.Id.ShouldEqual(ProductMocks.NewProductiPhoneXS.Id);
            okResult.Name.ShouldEqual(ProductMocks.NewProductiPhoneXS.Name);
            okResult.ProductOptions.Count().ShouldEqual(2);
        }

        [Fact]
        public void AddProductDuplicateKeyError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var targetProduct = ProductMocks.NewProductiPhoneXS;
            var targetId = targetProduct.Id;

            var repo = new Mock<IRepository>();
            repo.Setup(r => r.Add(It.IsAny<Product>())).Throws<InvalidOperationException>();
            var controller = new ProductsController(logger.Object, repo.Object);

            Assert.Throws<InvalidOperationException>(() => controller.Post(targetProduct));
        }

        [Fact]
        public void UpdateProductNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Throws<KeyNotFoundException>();

            repo.Setup(r => r.Update(It.IsAny<Product>())).Throws<KeyNotFoundException>();

            var controller = new ProductsController(logger.Object, repo.Object);
            Assert.Throws<KeyNotFoundException>(() => controller.Update(ProductMocks.NewProductiPhoneXS.Id, ProductMocks.NewProductiPhoneXS));
        }

        [Fact]
        public void UpdateProductSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.Update(It.IsAny<Product>())).Verifiable();
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7);
            
            var controller = new ProductsController(logger.Object, repo.Object);
            controller.Update(ProductMocks.ProductSamsungGalaxyS7Updated.Id, ProductMocks.ProductSamsungGalaxyS7Updated);

            repo.Verify();
        }

        [Fact]
        public void DeleteProductSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7);
            repo.Setup(r => r.Delete(It.IsAny<Product>())).Verifiable();
            var controller = new ProductsController(logger.Object, repo.Object);
            controller.Delete(ProductMocks.ProductSamsungGalaxyS7.Id);
            repo.Verify();
        }

        [Fact]
        public void DeleteProductNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.Delete(It.IsAny<Product>())).Throws<KeyNotFoundException>();
            var controller = new ProductsController(logger.Object, repo.Object);
            Assert.Throws<KeyNotFoundException>(() => controller.Delete(ProductMocks.ProductSamsungGalaxyS7.Id));
            repo.Verify();
        }

        [Fact]
        public void GetProductOptionsSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7);
            var controller = new ProductsController(logger.Object, repo.Object);

            var result = controller.GetProductOptions(ProductMocks.ProductSamsungGalaxyS7.Id);
            var resultValue = Assert.IsType<OkObjectResult>(result.Result);
            var productOptions = Assert.IsType<List<ProductOption>>(resultValue.Value);
            repo.Verify();
            productOptions.Count().ShouldEqual(ProductMocks.ProductSamsungGalaxyS7.ProductOptions.Count());
        }

        [Fact]
        public void GetProductOptionsNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Throws<KeyNotFoundException>();
            var controller = new ProductsController(logger.Object, repo.Object);

            Assert.Throws<KeyNotFoundException>(() => controller.GetProductOptions(ProductMocks.ProductSamsungGalaxyS7.Id));
            repo.Verify();
        }

        [Fact]
        public void GetProductOptionSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            var targetOption = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();

            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7);
            var controller = new ProductsController(logger.Object, repo.Object);
            var result = controller.GetProductOption(targetOption.ProductId, targetOption.Id);
            var resultValue = Assert.IsType<OkObjectResult>(result.Result);
            var productOption = Assert.IsType<ProductOption>(resultValue.Value);
            repo.Verify();
            productOption.Id.ShouldEqual(ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First().Id);
        }

        [Fact]
        public void GetProductOptionNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();
            var targetOption = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();

            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Throws<KeyNotFoundException>();
            var controller = new ProductsController(logger.Object, repo.Object);
            Assert.Throws<KeyNotFoundException>(() => controller.GetProductOption(targetOption.ProductId, targetOption.Id));
            repo.Verify();
        }

        [Fact]
        public void UpdateProductOptionSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7);
            repo.Setup(r => r.Update(It.IsAny<Product>())).Verifiable();;

            var controller = new ProductsController(logger.Object, repo.Object);

            controller.UpdateOption(option.ProductId, option.Id, option);
            repo.Verify();
        }

        [Fact]
        public void UpdateProductOptionNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            var controller = new ProductsController(logger.Object, repo.Object);
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7);

            repo.Setup(r => r.Update(It.IsAny<Product>())).Throws<KeyNotFoundException>();
            Assert.Throws<KeyNotFoundException>(() => controller.UpdateOption(option.ProductId, option.Id, option));
            repo.Verify();
        }

        [Fact]
        public void DeleteProductOptionSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            var controller = new ProductsController(logger.Object, repo.Object);
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7);

            repo.Setup(r => r.Update<Product>(It.IsAny<Product>())).Verifiable();
            controller.DeleteOption(option.ProductId, option.Id);
            repo.Verify();
        }

        [Fact]
        public void DeleteProductOptionNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            var controller = new ProductsController(logger.Object, repo.Object);
           
            repo.Setup(r => r.GetById<Product>(It.IsAny<Guid>())).Throws<KeyNotFoundException>();
            Assert.Throws<KeyNotFoundException>(() => controller.DeleteOption(option.ProductId, option.Id));
            repo.Verify();
        }
    }
}