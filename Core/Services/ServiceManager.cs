﻿
using Domain.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _lazyBasketService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository)
        {
            _productService = new Lazy<IProductService>(()=> new ProductService(unitOfWork, mapper));
            _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;
    }
}
