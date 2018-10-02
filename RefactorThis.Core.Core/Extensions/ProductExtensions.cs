using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactorThis.Core.Core.Extensions
{
    public static class ProductExtensions
    {
        public static IList<ProductOption> GetProductOptions(this Product product)
        {
            var options = product.ProductOptions.ToList();
          
            return options;
        }
        public static  ProductOption GetProductOption(this Product product, Guid optionId)
        {
            var option = product.ProductOptions.FirstOrDefault<ProductOption>(po => po.Id == optionId);
            if (option == null)
                throw new KeyNotFoundException();
            return option;
        }
    }
}
