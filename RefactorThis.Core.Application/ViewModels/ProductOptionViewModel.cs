using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorThis.Core.Application.ViewModels
{
    public class ProductOptionViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
