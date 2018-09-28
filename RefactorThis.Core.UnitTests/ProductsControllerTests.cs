using Microsoft.Extensions.Logging;
using Moq;
using RefactorThis.Core.Repository;
using RefactorThis.Controllers;
using System;
using Xunit;
using RefactorThis.Core.UnitTests.Mocks;
using System.Collections.Generic;
using RefactorThis.Core.Models;
using System.Linq;
using Xunit.Extensions.AssertExtensions;
using Microsoft.AspNetCore.Mvc;

namespace RefactorThis.Core.UnitTests
{
    public class ProductsControllerTests
    {
       
        [Fact]
        public void GetAllProductsReturnsAllItems()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.GetAllProducts()).Returns(ProductMocks.ProductsBaseDataset);

            var controller = new ProductsController(logger.Object, repo.Object);
            var result = controller.Get(null);
            var resultValue= Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<List<Product>>(resultValue.Value);
            repo.Verify();
            okResult.Count().ShouldEqual(2);
        }
        [Fact]
        public void GetProductByNameReturnsSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.SearchByName("Galaxy")).Returns(ProductMocks.ProductsBaseDataset);

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
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.SearchByName("Microsoft")).Returns(new List<Product>());
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
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.GetProduct(ProductMocks.ProductSamsungGalaxyS7.Id)).Returns(ProductMocks.ProductSamsungGalaxyS7);
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
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.AddProduct(ProductMocks.NewProductiPhoneXS)).Returns(ProductMocks.NewProductiPhoneXS);
            repo.Setup(r => r.GetProduct(ProductMocks.NewProductiPhoneXS.Id)).Returns(ProductMocks.NewProductiPhoneXS);

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

            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.AddProduct(It.IsAny<Product>())).Throws<InvalidOperationException>();
            var controller = new ProductsController(logger.Object, repo.Object);

            Assert.Throws< InvalidOperationException>(()=> controller.Post(targetProduct));
        }
        [Fact]
        public void UpdateProductNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.UpdateProduct(It.IsAny<Product>())).Throws<KeyNotFoundException>();

            var controller = new ProductsController(logger.Object, repo.Object);
            Assert.Throws<KeyNotFoundException>(() => controller.Update(ProductMocks.NewProductiPhoneXS.Id, ProductMocks.NewProductiPhoneXS));
        }
        [Fact]
        public void UpdateProductSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.UpdateProduct(ProductMocks.ProductSamsungGalaxyS7Updated)).Returns(ProductMocks.ProductSamsungGalaxyS7Updated);

            var controller = new ProductsController(logger.Object, repo.Object);
            controller.Update(ProductMocks.ProductSamsungGalaxyS7Updated.Id, ProductMocks.ProductSamsungGalaxyS7Updated);

            repo.Verify();
        }
        [Fact]
        public void DeleteProductSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.DeleteProduct(ProductMocks.ProductSamsungGalaxyS7)).Returns(true);
            var controller = new ProductsController(logger.Object, repo.Object);
            controller.Delete(ProductMocks.ProductSamsungGalaxyS7.Id);
            repo.Verify();

        }

        [Fact]
        public void DeleteProductNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.DeleteProduct(It.IsAny<Product>())).Throws<KeyNotFoundException>();
            var controller = new ProductsController(logger.Object, repo.Object);
            Assert.Throws<KeyNotFoundException>(() => controller.Delete(ProductMocks.ProductSamsungGalaxyS7.Id))    ;
            repo.Verify();

        }
        [Fact]
        public void GetProductOptionsSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.GetProductOptions(It.IsAny<Guid>())).Returns(ProductMocks.ProductSamsungGalaxyS7.ProductOptions);
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
            var repo = new Mock<IProductsRepository>();
            repo.Setup(r => r.GetProductOptions(It.IsAny<Guid>())).Throws<KeyNotFoundException>();
            var controller = new ProductsController(logger.Object, repo.Object);

            Assert.Throws<KeyNotFoundException>(() => controller.GetProductOptions(ProductMocks.ProductSamsungGalaxyS7.Id));
            repo.Verify();

        }

        [Fact]
        public void GetProductOptionSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();
            var targetOption = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();

            repo.Setup(r => r.GetProductOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(targetOption);
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
            var repo = new Mock<IProductsRepository>();
            var targetOption = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();

            repo.Setup(r => r.GetProductOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws<KeyNotFoundException>();
            var controller = new ProductsController(logger.Object, repo.Object);
            Assert.Throws<KeyNotFoundException>(() => controller.GetProductOption(targetOption.ProductId, targetOption.Id));
            repo.Verify();

        }
        [Fact]
        public void UpdateProductOptionSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            var controller = new ProductsController(logger.Object, repo.Object);
            repo.Setup(r => r.UpdateProductOption(It.IsAny<ProductOption>())).Returns(null);
            controller.UpdateOption(option.ProductId, option.Id, option);
            repo.Verify();
        }

        [Fact]
        public void UpdateProductOptionNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            var controller = new ProductsController(logger.Object, repo.Object);
            repo.Setup(r => r.UpdateProductOption(It.IsAny<ProductOption>())).Throws<KeyNotFoundException>();
            Assert.Throws<KeyNotFoundException>(() => controller.UpdateOption(option.ProductId, option.Id, option));
            repo.Verify();
        }

        [Fact]
        public void DeleteProductOptionSuccess()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            var controller = new ProductsController(logger.Object, repo.Object);
            repo.Setup(r => r.DeleteProductOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(null);
            controller.DeleteOption(option.ProductId, option.Id);
            repo.Verify();

        }

        [Fact]
        public void DeleteProductOptionNotFoundError()
        {
            var logger = new Mock<ILogger<ProductsController>>();
            var repo = new Mock<IProductsRepository>();

            var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
            option.Name = "UpdatedName";
            option.Description = "UpdatedDescription";
            var controller = new ProductsController(logger.Object, repo.Object);
            repo.Setup(r => r.DeleteProductOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws<KeyNotFoundException>();
            Assert.Throws<KeyNotFoundException>(() => controller.DeleteOption(option.ProductId, option.Id));
            repo.Verify();

        }
    }
}
