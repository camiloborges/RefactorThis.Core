using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Core.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.API.Application.Commands
{
    public partial class CreateProductCommand:Command
    {
        private string name;
        private string description;
        private decimal price;
        private decimal deliveryPrice;
        private IEnumerable<ProductOptionDTO> productOptions;

        public CreateProductCommand(string name, string description, decimal price, decimal deliveryPrice, IEnumerable<ProductOptionDTO> productOptions)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.deliveryPrice = deliveryPrice;
            this.productOptions = productOptions;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
