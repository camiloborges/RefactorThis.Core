using RefactorThis.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Core.UnitTests.Mocks
{
    public static class ProductMocks
    {
        public static Product ProductSamsungGalaxyS7
        {
            get
            {
                return new Product()
                {
                    Id = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                    Name = "Samsung Galaxy S7",
                    Description = "Newest mobile product from Apple supporting lambas",
                    Price = new Decimal(1924.99),
                    DeliveryPrice = new decimal(0.0),
                    ProductOptions = new List<ProductOption>{
                        new ProductOption()     {
                           Id = new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9"),
                           ProductId = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                            Name = "White",
                            Description = "White Samsung Galaxy S7"
                        },
                        new ProductOption(){
                            Id= new Guid( "a21d5777-a655-4020-b431-624bb331e9a2"),
                            ProductId =new Guid( "8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                            Name =  "Black",
                            Description=  "Black Samsung Galaxy S7"
                        }
                 }
                };
            }
        }

        public static Product ProductSamsungGalaxyS7Updated
        {
            get
            {
                return new Product()
                {
                    Id = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                    Name = "Samsung Galaxy S7 Updated",
                    Description = "Newest mobile product from Apple supporting lambas Updated",
                    Price = new Decimal(1024.99),
                    DeliveryPrice = new decimal(1.0),
                    ProductOptions = new List<ProductOption>{
                        new ProductOption()     {
                           Id = new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9"),
                           ProductId = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                            Name = "White",
                            Description = "White Samsung Galaxy S7"
                        },
                        new ProductOption(){
                            Id= new Guid( "a21d5777-a655-4020-b431-624bb331e9a2"),
                            ProductId =new Guid( "8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                            Name =  "Black",
                            Description=  "Black Samsung Galaxy S7"
                        }
                 }
                };
            }
        }

        public static IEnumerable<Product> ProductsBaseDataset
        {
            get
            {
                return new List<Product>() {
            ProductSamsungGalaxyS7,
            new Product()
        {
            Id = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                Name = "Apple iPhone 6S",
                Description = "Newest mobile product from Apple.",
                Price = new decimal(1299.99),
                DeliveryPrice = new decimal(15.99),
                ProductOptions = new List<ProductOption>{
                    new ProductOption()     {
                        Id = new Guid("5c2996ab-54ad-4999-92d2-89245682d534"),
                        ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                        Name = "Rose Gold",
                        Description = "Gold Apple iPhone 6S"
                    },
                    new ProductOption()     {
                        Id = new Guid("9ae6f477-a010-4ec9-b6a8-92a85d6c5f03"),
                        ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                        Name = "White",
                        Description = "White Apple iPhone 6S"
                    },
                     new ProductOption()     {
                        Id = new Guid("4e2bc5f2-699a-4c42-802e-ce4b4d2ac0ef"),
                        ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                        Name = "Black",
                        Description = "Black Apple iPhone 6S"
                    }
                }
            }
            };
            }
        }

        public static IEnumerable<Product> ProductsWithNoOptions
        {
            get
            {
                return ProductsBaseDataset.Select(p =>
                {
                    p.ProductOptions = new List<ProductOption>();
                    return p;
                });
            }
        }

        public static IEnumerable<Product> ProductsWithNullOptions
        {
            get
            {
                return ProductsBaseDataset.Select(p =>
                {
                    p.ProductOptions = null;
                    return p;
                });
            }
        }

        public static Product NewProductiPhoneXS
        {
            get
            {
                return new Product()
                {
                    Id = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db668"),
                    Name = "iPhone XS Lambda",
                    Description = "Newest mobile product from Apple supporting lambas.",
                    Price = new decimal(1924.99),
                    DeliveryPrice = new decimal(0.0),
                    ProductOptions = new List<ProductOption> {
                            new ProductOption(){
                              Id = new Guid( "0643ccf0-ab00-4862-b3c5-40e2731ab640"),
                              ProductId = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db668"),
                              Name =  "White",
                              Description = "Gold"
                            },
                            new ProductOption(){
                              Id= new Guid("a21d5777-a655-4020-b431-624bb331e941"),
                              ProductId = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db668"),
                              Name = "Black",
                              Description =  "Black"
                            } }
                };
            }
        }
    }
}