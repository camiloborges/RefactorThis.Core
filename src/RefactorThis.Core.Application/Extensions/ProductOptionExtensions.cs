
using RefactorThis.Core.API.Application.Commands;
using RefactorThis.Core.Application.ViewModels;
using RefactorThis.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RefactorThis.Core.API.Application.Commands.CreateProductCommand;

namespace RefactorThis.Core.Application.Extensions
{
    public static class ProductOptionExtensions
    {
        public static IEnumerable<ProductOptionDTO> ToProductOptionsDTO(this IEnumerable<ProductOptionViewModel> options)
        {
            foreach (var item in options)
            {
                yield return item.ToProductOptionDTO();
            }
        }

        public static ProductOptionDTO ToProductOptionDTO(this ProductOptionViewModel item)
        {
            return new ProductOptionDTO()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };
        }
    }
}
