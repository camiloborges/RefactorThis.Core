using System;
using System.Collections.Generic;
using System.Linq;
using RefactorThis.Core.Application.ViewModels;

namespace RefactorThis.Core.Application.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        void Create(ProductViewModel productViewModel);
        void CreateProductOption(ProductOptionViewModel productOptionViewModel);

        IEnumerable<ProductViewModel> GetAll();
        ProductViewModel GetById(Guid id);
        IEnumerable<ProductOptionViewModel> GetProductOptions(Guid productId);
        ProductOptionViewModel GetProductOption(Guid productId, Guid productOptionId);

        IEnumerable<ProductViewModel> SearchByName(string name);

        void Update(ProductViewModel productViewModel);
        void UpdateProductOption(ProductOptionViewModel productViewModel);

        void Remove(Guid id);
        void RemoveProductOption(Guid id, Guid productOptionId);

    }
}
