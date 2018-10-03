using AutoMapper;
using RefactorThis.Core.API.Application.Commands;
using RefactorThis.Core.Application.Extensions;
using RefactorThis.Core.Application.ViewModels;
using System.Linq;

namespace Refactortis.Core.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, CreateProductCommand>()
                .ConstructUsing(c => new CreateProductCommand(c.Name, c.Description, c.Price, c.DeliveryPrice, c.ProductOptions.ToProductOptionsDTO()));
            CreateMap<ProductViewModel, UpdateProductCommand>()
                .ConstructUsing(c => new UpdateProductCommand(c.Name, c.Description, c.Price, c.DeliveryPrice));
        }
    }
}
