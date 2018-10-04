using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RefactorThis.Core.Domain
{
    public partial class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }

        public IList<ProductOption> ProductOptions { get; set; }

        public Product()
        {
        }

        public Product(Guid id, string name, string description, decimal price, decimal deliveryPrice, IList<ProductOption> options)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
            ProductOptions = options;
        }
    }
}
