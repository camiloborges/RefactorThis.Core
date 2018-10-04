using System;
using System.Linq;

namespace RefactorThis.Core.Domain.Interfaces
{
    public interface IProductsRepository : IRepository<Product>
    {
        IQueryable<Product> Items { get; }
       
        ProductOption GetProductOption(Guid productId, Guid optionId);

        IQueryable<ProductOption> GetProductOptions(Guid productId);

        IQueryable<Product> SearchByName(string name);

        ProductOption AddProductOption(ProductOption productOption);

        bool UpdateProductOption(ProductOption orig);

        bool DeleteProductOption(Guid productId, Guid id);
    }
}
