using Microsoft.Extensions.Logging;
using RefactorThis.Core.Models;
using RefactorThis.Core.Repository;
using RefactorThis.Core.UnitTests.Mocks;
using System;
using System.Linq;
using Moq;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;

namespace RefactorThis.Core.UnitTests
{
    public class ProductsRepositoryTests
    {
        
        [Fact]
        public void AddRangeGetAllReturnsAll2Items()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var products = context.Product.ToList();
                products.Count().ShouldEqual(2);
            }
        }

        [Fact]
        public void GetAllProductsReturnsEmptyList()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                var products = context.Product.ToList();
                products.Count().ShouldEqual(0);
            }
        }
     
        [Fact]
        public void GetProductByNameReturnsSuccess()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);

                var matches = repository.SearchByName("Samsung");
                matches.Count().ShouldEqual(1);

                var product = matches.First();
                product.Name.ShouldEqual(ProductMocks.ProductSamsungGalaxyS7.Name);
            }
        }
        [Fact]
        public void GetProductByNameReturnsEmpty()
        {   
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);

                var matches = repository.SearchByName("Microsoft");
                matches.Count().ShouldEqual(0);
            }
        }

        [Fact]
        public void GetProductByIdReturnsSucessAndWithOptions()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                var product = repository.GetProduct(ProductMocks.ProductSamsungGalaxyS7.Id);
                var compareLogic = new CompareLogic();

                var result = compareLogic.Compare(product, ProductMocks.ProductSamsungGalaxyS7);
                result.AreEqual.ShouldBeTrue();
                product.ProductOptions.Count().ShouldEqual(2);
            }
        }
       
        [Fact]
        public void AddProductSuccess()
        {
            try
            {
                var options = EfInMemory.CreateOptions<ProductsContext>();
                using (var context = new ProductsContext(options))
                {

                    var logger = new Mock<ILogger<ProductsRepository>>();
                    var repository = new ProductsRepository(context, logger.Object);
                    repository.AddProduct(ProductMocks.NewProductiPhoneXS);

                    var product = repository.GetProduct(ProductMocks.NewProductiPhoneXS.Id);
                    product.ShouldBeSameAs(ProductMocks.NewProductiPhoneXS);
                }
            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }
        }
        [Fact]
        public void AddProductDuplicateKeyError()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                Assert.Throws<InvalidOperationException>(() => repository.AddProduct(ProductMocks.ProductSamsungGalaxyS7));
            }
        }
        [Fact]
        public void UpdateProductNotFoundError()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                Assert.Throws<KeyNotFoundException>(() => repository.UpdateProduct(ProductMocks.ProductSamsungGalaxyS7));
            }
        }
        [Fact]
        public void UpdateProductSuccess()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                var product = ProductMocks.ProductSamsungGalaxyS7;
                product.Description = "TestDescription";
                product.Name = "TestName";
                repository.UpdateProduct(product);
                var updatedProduct = repository.GetProduct(product.Id);
                updatedProduct.Name.ShouldBeSameAs(product.Name);
                updatedProduct.Description.ShouldBeSameAs(product.Description);
            }
        }
        [Fact]
        public void DeleteProductSuccess()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                repository.DeleteProduct(ProductMocks.ProductSamsungGalaxyS7).ShouldBeTrue();
            }
        }

        [Fact]
        public void DeleteProductNotFound()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                Assert.Throws<KeyNotFoundException>(() => repository.DeleteProduct(ProductMocks.NewProductiPhoneXS));
            }
        }
        [Fact]
        public void GetProductOptionsSuccess()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                var productOptions = repository.GetProductOptions(ProductMocks.ProductSamsungGalaxyS7.Id);
                productOptions.Count().ShouldEqual(ProductMocks.ProductSamsungGalaxyS7.ProductOptions.Count());
            }
        }
        

        [Fact]
        public void GetProductOptionSuccess()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var targetOption = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                var sourceOption = repository.GetProductOption(targetOption.ProductId, targetOption.Id);
                var compareLogic = new CompareLogic();

                var result = compareLogic.Compare(targetOption, sourceOption);
                result.AreEqual.ShouldBeTrue();
            }
        }

        [Fact]
        public void GetProductOptionNotFoundError()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                Assert.Throws<KeyNotFoundException>(() => repository.GetProductOption(new Guid(), new Guid()));
            }
        }

        [Fact]
        public void UpdateProductOptionSuccess()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                var compareLogic = new CompareLogic();
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);

                option.Name = "UpdatedName";
                option.Description = "UpdatedDescription";
                repository.UpdateProductOption(option);
                var updatedOption = repository.GetProductOption(option.ProductId, option.Id);
                var result = compareLogic.Compare(option, updatedOption);
                result.AreEqual.ShouldBeTrue();
            }
        }

        [Fact]
        public void UpdateProductOptionNotFoundError()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                option.Name = "UpdatedName";
                option.Description = "UpdatedDescription";
                Assert.Throws<KeyNotFoundException>(() => repository.UpdateProductOption(option)); 
            }
        }

        [Fact]
        public void DeleteProductOptionSuccess()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                context.FeedDataContext(ProductMocks.ProductsBaseDataset);
                var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                repository.DeleteProductOption(option.ProductId, option.Id).ShouldBeTrue();
            }
        }

        [Fact]
        public void DeleteProductOptionNotFoundError()
        {
            var options = EfInMemory.CreateOptions<ProductsContext>();
            using (var context = new ProductsContext(options))
            {
                var option = ProductMocks.ProductSamsungGalaxyS7.ProductOptions.First();
                var logger = new Mock<ILogger<ProductsRepository>>();
                var repository = new ProductsRepository(context, logger.Object);
                Assert.Throws<KeyNotFoundException>(() => repository.DeleteProductOption(option.ProductId, option.Id));
            }
        }
    }
}
