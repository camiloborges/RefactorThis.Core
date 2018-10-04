using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactorThis.Core.Domain.Extensions
{
    public static class ProductExtensions
    {
        public static ProductOption GetProductOption(this Product product, Guid optionId)
        {
            return product.ProductOptions.FirstOrDefault(po => po.Id == optionId);
        }
    }
}
