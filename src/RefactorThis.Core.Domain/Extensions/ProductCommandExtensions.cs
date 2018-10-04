using RefactorThis.Core.Domain.Commands;
using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Core.Domain.Extensions
{
    public static class ProductCommandExtensions
    {
        public static IList<ProductOption> ToProductOptions(this ProductCommand command)
        {
            if (command.Options == null)
                return new List<ProductOption>();
            return command.Options.Select(p => new ProductOption(p.Id, command.Id, p.Name, p.Description)).ToList();
        }

        public static IEnumerable<ProductOptionDTO> ToProductOptionsDTO(this IEnumerable<ProductOption> options)
        {
            foreach (var item in options)
            {
                yield return item.ToProductOptionDTO();
            }
        }

        public static ProductOptionDTO ToProductOptionDTO(this ProductOption item)
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
