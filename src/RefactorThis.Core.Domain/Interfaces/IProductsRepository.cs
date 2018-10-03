using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Core.Domain.Interfaces
{
    public interface IProductsRepository: IRepository<Product>
    {
        IQueryable<Product> Items { get; }
        IQueryable<Product> GetAllProducts();
        Product GetProduct(Guid productId);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        ProductOption GetProductOption(Guid productId, Guid optionId);
        IQueryable<ProductOption> GetProductOptions(Guid productId);
        IQueryable<Product> SearchByName(string name);
        ProductOption AddProductOption(ProductOption productOption);
        bool UpdateProductOption(ProductOption orig);
        bool DeleteProductOption(Guid productId, Guid id);
    }
}