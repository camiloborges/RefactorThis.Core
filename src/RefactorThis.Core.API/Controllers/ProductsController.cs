using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefactorThis.Core;
using RefactorThis.Core.Core.Extensions;
using RefactorThis.Core.Infrastructure;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RefactorThis.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProductsRepository _repository;
      
        public ProductsController(
            ILogger<ProductsController> logger, IProductsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get([FromQuery]string name)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetAll, "Getting {name}", name);
                IEnumerable<Product> products;
                if (String.IsNullOrEmpty(name))
                    products = _repository.List<Product>().ToList();
                else
                    products = _repository.ListByName<Product>(name).ToList();
                return new OkObjectResult(products.ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetAll, exception, "Getting Exception {name}", name);
                throw ;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<Product> GetProduct(Guid id)
        {
            _logger.LogInformation(LoggingEvents.GetProduct, "GetProduct {id}", id);

            try
            {
                var product = _repository.GetProduct(id);
                var options = product.GetProductOptions();

                return new OkObjectResult(new Product()
                {
                    DeliveryPrice = product.DeliveryPrice,
                    Name = product.Name,
                    Id = product.Id,
                    Description = product.Description,
                    Price = product.Price,
                    ProductOptions = options.ToList(),
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetProduct, exception, "GetProduct Exception {id}", id);
                throw ;
            }
        }

        [HttpPost]
        public void Post(Product product)
        {
            _logger.LogInformation(LoggingEvents.AddProduct, "AddProduct {id}", product.Id);

            try
            {
                Product item = SanitizeInput(product.Id, product);
                _repository.Add(item);
                
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.AddProduct, exception, "AddProduct Exception {id}", product.Id);
                throw ;
            }
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product input)
        {
            _logger.LogInformation(LoggingEvents.UpdateProduct, "UpdateProduct {id}", input.Id);

            try
            {
                Product sanitizedProduct = SanitizeInput(id, input);
                var product = _repository.GetProduct(id);
                product.Name = sanitizedProduct.Name;
                product.Description = sanitizedProduct.Description;


                _repository.Update(product);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.UpdateProduct, exception, "UpdateProduct Exception {id}", input.Id);
                throw ;
            }
        }

        public static Product SanitizeInput(Guid id, Product product)
        {
            var productOptions = product.ProductOptions ?? new List<ProductOption>();
            productOptions = productOptions.Select(po =>
            {
                po.ProductId = id;
                return po;
            }).ToList();
            var orig = new Product()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice,
                ProductOptions = productOptions
            };
            return orig;
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _logger.LogInformation(LoggingEvents.DeleteProduct, "DeleteProduct {id}", id);

            try
            {
                var product = _repository.GetProduct(id);
                _repository.Delete(product);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.DeleteProduct, exception, "DeleteProduct Exception Id {id}", id);
                throw;
            }
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            _logger.LogInformation(LoggingEvents.GetProductOptions, "GetProductOptions {id}", productId);

            try
            {
                var options = _repository.GetProductOptions(productId);
                return new OkObjectResult(options);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetProductOptions, exception, "GetProductOptions Exception productId {productId}", productId);
                throw;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ActionResult<ProductOption> GetProductOption(Guid productId, Guid id)
        {
            _logger.LogInformation(LoggingEvents.GetProductOption, "GetProductOption  productId: {productId} id: {id}", productId, id);

            try
            {
                var option = _repository.GetProductOption(productId,id);
                return new OkObjectResult(option);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetProductOption, exception, "GetProductOption Exception productId {productId} id {id}", productId, id);
                throw;
            }
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void AddProductOption(Guid productId, ProductOption option)
        {
            _logger.LogInformation(LoggingEvents.AddProductOption, "AddProductOption  productId: {productId} id: {id}", productId, option.Id);

            try
            {
                option.ProductId = productId;
                var product = _repository.GetProduct(productId);
                product.ProductOptions.Add(option);
                _repository.Update(product); 
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.AddProductOption, exception, "AddProductOption Exception productId {productId} id {id}", productId, option.Id);
                throw ;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid productId, Guid id, ProductOption option)
        {
            _logger.LogInformation(LoggingEvents.UpdateProductOption, "UpdateProductOption productId: {productId} id: {id}", productId, option.Id);

            try
            {
                var product = _repository.GetProduct(productId);

                var productOption = product.GetProductOption(id);

                productOption.Name = option.Name;
                productOption.Description = option.Description;

                _repository.Update(product);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.UpdateProductOption, exception, "UpdateProductOption Exception productId {productId} id {id}", productId, id);
                throw ;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid productId, Guid id)
        {
            _logger.LogInformation(LoggingEvents.DeleteProductOption, "DeleteProductOption productId: {productId} id: {id}", productId, id);

            try
            {
                var product = _repository.GetProduct(productId);
                var productOption = product.GetProductOption(id);
                product.ProductOptions.Remove(productOption);
                _repository.Update(product);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.DeleteProductOption, exception, "DeleteOption Exception productId {productId} id {id}", productId, id);
                throw ;
            }
        }
    }
}