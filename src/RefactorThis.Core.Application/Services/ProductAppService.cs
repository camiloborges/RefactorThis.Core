using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RefactorThis.Core.Application.Interfaces;
using RefactorThis.Core.Application.ViewModels;
using RefactorThis.Core.Domain.Commands;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Interfaces;
using RefactorThis.Core.Infra.Data.Repository.EventSourcing;

namespace RefactorThis.Core.Application.Services
{
    public sealed class ProductAppService : IProductAppService
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public ProductAppService(IMapper mapper,
                                  IProductsRepository customerRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _productRepository = customerRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return _productRepository.GetAll().ProjectTo<ProductViewModel>();
        }

        public ProductViewModel GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(_productRepository.GetById(id));
        }
        public IEnumerable<ProductViewModel> SearchByName(string name)
        {
            return _productRepository.SearchByName(name).ProjectTo<ProductViewModel>();
        }

        public IEnumerable<ProductOptionViewModel> GetProductOptions(Guid productId)
        {
            return _productRepository.GetProductOptions(productId).ProjectTo<ProductOptionViewModel>();
        }
        public ProductOptionViewModel GetProductOption(Guid productId, Guid productOptionId)
        {
            return _mapper.Map<ProductOptionViewModel>(_productRepository.GetProductOption(productId,productOptionId));
        }
        public void Create(ProductViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<CreateProductCommand>(customerViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(ProductViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateProductCommand>(customerViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveProductCommand(id);
            Bus.SendCommand(removeCommand);
        }
/*
        public IList<ProductOptionsViewModel> GetAllHistory(Guid id)
        {
            return CustomerHistory.ToJavaScriptCustomerHistory(_eventStoreRepository.All(id));
        }
*/
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void CreateProductOption(ProductOptionViewModel productOptionViewModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductOption(ProductOptionViewModel productViewModel)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductOption(Guid id, Guid productOptionId)
        {
            throw new NotImplementedException();
        }
    }
}
