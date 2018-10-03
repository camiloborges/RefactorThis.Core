using AutoMapper;
using RefactorThis.Core.Application.Extensions;
using RefactorThis.Core.Application.ViewModels;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Commands;
using System.Linq;

namespace Refactortis.Core.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductOptionViewModel, ProductOption>();
            CreateMap<ProductViewModel, CreateProductCommand>()
                .ConstructUsing(c => new CreateProductCommand(c.Name, c.Description, c.Price, c.DeliveryPrice, c.ProductOptions.ToProductOptionsDTO().ToList()));
            CreateMap<ProductViewModel, UpdateProductCommand>()
                .ConstructUsing(c => new UpdateProductCommand(c.Id, c.Name, c.Description, c.Price, c.DeliveryPrice, c.ProductOptions.ToProductOptionsDTO().ToList()));
        }
    }
}