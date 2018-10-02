using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Models;
using System;

namespace RefactorThis.Infrastructure
{
    public interface IProductsContext: IDisposable
    {
        DbSet<Product> Product { get; set; }
        DbSet<ProductOption> ProductOption { get; set; }

        bool SaveContextChanges();
    }
}