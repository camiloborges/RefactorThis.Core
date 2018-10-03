using RefactorThis.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactorThis.Core.Domain.Extensions
{
    public static class ProductCommandExtensions
    {
        public static IList<ProductOption> ToProductOptions(this ProductCommand command)
        {
            if(command.Options == null)
                return new List<ProductOption>(); 
            return command.Options.Select(p => new ProductOption(p.Id, command.Id, p.Name, p.Description)).ToList();
        }
    }
}
