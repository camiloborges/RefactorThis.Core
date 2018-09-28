using Microsoft.EntityFrameworkCore;

namespace RefactorThis.Core.Models
{
    public interface IProductsContext
    {
        DbSet<Product> Product { get; set; }
        DbSet<ProductOption> ProductOption { get; set; }

        bool SaveContextChanges();
    }
}