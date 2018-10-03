using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Core;
using RefactorThis.Core.Domain.Interfaces;
using RefactorThis.Core.Infra.Data.Context;
using RefactorThis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Core.Infra.Data.Repository
{
    public sealed class ProductsRepository : IProductsRepository
    {
        public IQueryable<Product> Items { get; private set; }
        private ILogger<ProductsRepository> _logger;
        private IProductsContext _context;

        
//        public override Dispose(bool )
        public ProductsRepository(IProductsContext dataContext, ILogger<ProductsRepository> logger)
        {
            _context = dataContext;
            _logger = logger;
        }

        public IQueryable<Product> GetAllProducts()
        {
            var products = _context.Product;
            return products;
        }

        public IQueryable<ProductOption> GetProductOptions(Guid productId)
        {
            var products = _context.ProductOption.Where(p => p.ProductId == productId);
            return products;
        }

        public ProductOption GetProductOption(Guid productId, Guid optionId)
        {
            var option = _context.ProductOption.FirstOrDefault(p => p.ProductId == productId && p.Id == optionId);
            if (option == null)
            {
                throw new KeyNotFoundException();
            }
            return option;
        }

        public Product GetProduct(Guid productId)
        {
            var product = _context.Product.Include(p => p.ProductOptions).FirstOrDefault(p => p.Id == productId);
            return product;
        }

        public Product AddProduct(Product product)
        {
            try
            {
                var newProduct = _context.Product.Add(product);
                if (newProduct == null)
                {
                    throw new NullReferenceException("Product shouldn't be null after it is added");
                }
                _context.SaveContextChanges();
                return newProduct.Entity;
            }
            catch (InvalidOperationException operationException)
            {
                _logger.LogError(LoggingEvents.AddProduct, operationException, "Possible Primary Key Violation {id}", product.Id);
                throw ;
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.AddProduct, exception, "AddProduct Exception {id}", product.Id);
                throw ;
            }
        }

        public Product UpdateProduct(Product product)
        {
            var savedProduct = _context.Product.FirstOrDefault(p => p.Id == product.Id);
            if (savedProduct == null)
            {
                throw new KeyNotFoundException();
            }
            savedProduct.Name = product.Name;
            savedProduct.Price = product.Price;
            savedProduct.Description = product.Description;
            savedProduct.DeliveryPrice = product.DeliveryPrice;
            _context.Product.Update(savedProduct);
            _context.SaveContextChanges();

            return savedProduct;
        }

        public bool DeleteProduct(Product product)
        {
            var savedProduct = _context.Product.FirstOrDefault(p => p.Id == product.Id);
            if (savedProduct == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Product.Remove(savedProduct);
            return _context.SaveContextChanges();
        }

        public IQueryable<Product> SearchByName(string name)
        {
            var items = _context.Product.Where(p => p.Name.Contains(name, StringComparison.InvariantCulture));
            return items;
        }

        public ProductOption AddProductOption(ProductOption option)
        {
            var addedOption = _context.ProductOption.Add(option);
            _context.SaveContextChanges();
            return addedOption.Entity;
        }

        public bool UpdateProductOption(ProductOption orig)
        {
            var productOption = _context.ProductOption.FirstOrDefault(p => p.ProductId == orig.ProductId && p.Id == orig.Id);
            if (productOption == null)
                throw new KeyNotFoundException();
            productOption.Name = orig.Name;
            productOption.Description = orig.Description;

            _context.ProductOption.Update(productOption);
            return _context.SaveContextChanges();
        }

        public bool DeleteProductOption(Guid productId, Guid id)
        {
            var productOption = _context.ProductOption.FirstOrDefault(p => p.ProductId == productId && p.Id == id);
            if (productOption == null)
                throw new KeyNotFoundException();

            _context.ProductOption.Remove(productOption);
            return _context.SaveContextChanges();
        }

        public void Add(Product obj)
        {
            throw new NotImplementedException();
        }

        public Product GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void qDispose()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

      
    }
}