using AutoMapper;
using RefactorThis.Core.Application.ViewModels;
using RefactorThis.Core.Domain;

namespace RefactorThis.Core.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductOption, ProductOptionViewModel>();
        }
    }
}
