using RefactorThis.Core.Models;
using System;
using System.Collections.Generic;

namespace RefactorThis.Core.Domain
{
    public interface IProductsRepository
    {
        IEnumerable<Product> Items { get; }

        IEnumerable<Product> GetAllProducts();

        Product GetProduct(Guid productId);

        Product AddProduct(Product product);

        Product UpdateProduct(Product product);

        bool DeleteProduct(Product product);

        ProductOption GetProductOption(Guid productId, Guid optionId);

        IEnumerable<ProductOption> GetProductOptions(Guid productId);

        IEnumerable<Product> SearchByName(string name);

        ProductOption AddProductOption(ProductOption productOption);

        bool UpdateProductOption(ProductOption orig);

        bool DeleteProductOption(Guid productId, Guid id);
    }
}