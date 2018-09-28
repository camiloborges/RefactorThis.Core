using System.Data.Entity;

namespace RefactorThis.Models
{
    public interface IDataContext
    {
        DbSet<ProductOption> ProductOptions { get; set; }
        DbSet<Product> Products { get; set; }
    }
}