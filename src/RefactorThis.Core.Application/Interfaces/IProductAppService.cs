using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using RefactorThis.Core.Application.ViewModels;

namespace RefactorThis.Core.Application.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        void Create(ProductViewModel customerViewModel);
        IEnumerable<ProductViewModel> GetAll();
        ProductViewModel GetById(Guid id);
        void Update(ProductViewModel customerViewModel);
        void Remove(Guid id);
    }
}
