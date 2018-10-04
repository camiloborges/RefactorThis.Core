using RefactorThis.Core.Application.ViewModels;
using RefactorThis.Core.Domain.Commands;
using System.Collections.Generic;

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
