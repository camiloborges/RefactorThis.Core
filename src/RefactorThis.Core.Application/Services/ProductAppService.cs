using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Application.EventSourcedNormalizers;
using RefactorThis.Core.API.Application.Commands;
using RefactorThis.Core.Application.Interfaces;
using RefactorThis.Core.Application.ViewModels;
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
    }
}
