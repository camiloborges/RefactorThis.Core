using RefactorThis.Core.SharedKernel;
using System;
using System.Collections.Generic;

namespace RefactorThis.Core.Models
{
    public partial class Product: BaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }

        public IList<ProductOption> ProductOptions { get; set; }
    }
}