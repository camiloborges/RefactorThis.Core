using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Domain;
using System;

namespace RefactorThis.Core.Infra.Data.Context
{
    public interface IProductsContext : IDisposable
    {
        DbSet<Product> Product { get; set; }
        DbSet<ProductOption> ProductOption { get; set; }

        int SaveContextChanges();
    }
}
