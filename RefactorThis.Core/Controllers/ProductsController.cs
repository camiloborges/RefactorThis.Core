using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RefactorThis.Core;
using RefactorThis.Core.Models;
using RefactorThis.Core.Repository;

namespace RefactorThis.Controllers
{
  //  [RoutePrefix("products")]
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IProductsRepository _repository;

        public ProductsController(ILogger<ProductsController> logger, IProductsRepository repository) {
            _logger = logger;// logger.CreateLogger("RefactorThis.Controllers.ProductsController"); ;
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
                    products = _repository.GetAllProducts();
                else
                    products = _repository.SearchByName(name);
                return new OkObjectResult( products.ToList());
            }
            catch(Exception exception) {
                _logger.LogError(LoggingEvents.GetAll, exception, "Getting Exception {name}", name);
                throw exception;

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
                if (product is null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(new Product() {
                    DeliveryPrice = product.DeliveryPrice,
                    Name = product.Name,
                    Id = product.Id,
                    Description = product.Description,
                    Price = product.Price,
                    ProductOptions = product.ProductOptions.ToList(),
                });
            }
            catch (Exception exception) {
                _logger.LogError(LoggingEvents.GetProduct, exception, "GetProduct Exception {id}", id);
                throw exception;
            }
        }

        [HttpPost]
        public void Post(Product product)
        {
            try { 
                Product item = SanitizeInput(product.Id, product);
                _repository.AddProduct(item);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.AddProduct, exception, "AddProduct Exception {id}", product.Id);
                throw exception;
            }
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            try
            {
                Product orig = SanitizeInput(id, product);
                _repository.UpdateProduct(orig);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.UpdateProduct, exception, "UpdateProduct Exception {id}", product.Id);
                throw exception;
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
            try
            {
                var product = _repository.GetProduct(id);
                _repository.DeleteProduct(product);
            }
            catch (Exception exception) {
                _logger.LogError(LoggingEvents.DeleteProduct, exception, "DeleteProduct Exception Id {id}", id);
                throw exception;
            }
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            try { 
                return new OkObjectResult( _repository.GetProductOptions(productId) );
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetProductOptions, exception, "GetProductOptions Exception productId {productId}", productId);
                throw exception;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ActionResult<ProductOption> GetProductOption(Guid productId, Guid id)
        {
            try
            {
                var item = _repository.GetProductOption(productId, id);
                return new OkObjectResult(item);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetProductOption, exception, "GetProductOption Exception productId {productId} id {id}", productId, id);
                throw exception;
            }
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void AddProductOption(Guid productId, ProductOption option)
        {
            try
            {
                option.ProductId = productId;
                _repository.AddProductOption(option);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.AddProductOption, exception, "AddProductOption Exception productId {productId} id {id}", productId, option.Id);
                throw exception;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid productId, Guid id,  ProductOption option)
        {
            try
            {
                var orig = new ProductOption
                {
                    Id = id,
                    Name = option.Name,
                    Description = option.Description,
                    ProductId = productId
                };
                _repository.UpdateProductOption(orig);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.UpdateProductOption, exception, "UpdateProductOption Exception productId {productId} id {id}", productId, id);
                throw exception;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid productId, Guid id )
        {
            try
            {
                _repository.DeleteProductOption(productId, id);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.DeleteProductOption, exception, "DeleteOption Exception productId {productId} id {id}", productId, id);
                throw exception;
            }
        }
    }
}
