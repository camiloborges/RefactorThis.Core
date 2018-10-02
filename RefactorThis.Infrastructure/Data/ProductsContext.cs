
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Models;

namespace RefactorThis.Infrastructure.Data
{
    public partial class ProductsContext : DbContext, IProductsContext
    {
      
        private readonly IDomainEventDispatcher _dispatcher;

      
        public ProductsContext(DbContextOptions<ProductsContext> options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public ProductsContext()
        {
        }

        public bool SaveContextChanges()
        {
            var changes = base.SaveChanges();
            return changes > 0;
            // SaveChanges();
        }

        public  DbSet<Product> Product { get; set; }
        public  DbSet<ProductOption> ProductOption { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //
                IConfigurationRoot configuration = new ConfigurationBuilder()
                      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                      .AddJsonFile("appsettings.json")
                      .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}