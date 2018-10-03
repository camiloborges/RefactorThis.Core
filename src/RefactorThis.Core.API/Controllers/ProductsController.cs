using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefactorThis.Core;
using RefactorThis.Core.Application.Interfaces;
using RefactorThis.Core.Application.ViewModels;
using RefactorThis.Core.Controllers;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Core;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Notifications;
using RefactorThis.Core.Domain.Interfaces;
using RefactorThis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ApiController
    {
        private readonly ILogger _logger;
     //   private readonly IProductsRepository _repository;
        private readonly IProductAppService _service;

        public ProductsController(
             IProductAppService productAppService,
            INotificationHandler<DomainNotification> notifications,
            ILogger<ProductsController> logger,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _logger = logger;
            _service = productAppService;
        }
       
        [HttpGet]
        public ActionResult<IEnumerable<ProductViewModel>> Get([FromQuery]string name)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetAll, "Getting {name}", (name??""));
                IEnumerable<ProductViewModel> products;
                if (String.IsNullOrEmpty(name))
                    products = _service.GetAll();
                else
                    products = _service.SearchByName(name);
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
                var product = _service.GetById(id);
                if (product is null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(product);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetProduct, exception, "GetProduct Exception {id}", id);
                throw ;
            }
        }

        [HttpPost]
        public void Post(ProductViewModel product)
        {
            try
            {
              //  Product item = SanitizeInput(product.Id, product);
                _service.Create(product);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.AddProduct, exception, "AddProduct Exception {id}", product.Id);
                throw ;
            }
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, ProductViewModel product)
        {
            try
            {
               // Product orig = SanitizeInput(id, product);
                _service.Update(product);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.UpdateProduct, exception, "UpdateProduct Exception {id}", product.Id);
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
            try
            {
                var product = _service.GetById(id);
                if (product is null)
                    throw new KeyNotFoundException();

                _service.Remove(id);
                
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
            try
            {
                return new OkObjectResult(_service.GetProductOptions(productId));
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
            try
            {
                var item = _service.GetProductOption(productId, id);
                return new OkObjectResult(item);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.GetProductOption, exception, "GetProductOption Exception productId {productId} id {id}", productId, id);
                throw;
            }
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void AddProductOption(Guid productId, ProductOptionViewModel option)
        {
            try
            {
                option.ProductId = productId;
                _service.CreateProductOption(option);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.AddProductOption, exception, "AddProductOption Exception productId {productId} id {id}", productId, option.Id);
                throw ;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid productId, Guid id, ProductOptionViewModel option)
        {
            try
            {
                _service.UpdateProductOption(option);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.UpdateProductOption, exception, "UpdateProductOption Exception productId {productId} id {id}", productId, id);
                throw ;
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void RemoveOption(Guid productId, Guid id)
        {
            try
            {
                _service.RemoveProductOption(productId, id);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.DeleteProductOption, exception, "DeleteOption Exception productId {productId} id {id}", productId, id);
                throw ;
            }
        }
    }
}