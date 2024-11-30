
namespace Services
{
    public sealed class ServiceManager (
        IMapper mapper,
        IUnitOfWork unitOfWork, 
        IConfiguration configuration,
        IOptions<JwtOptions> options, 
        UserManager<User> userManager, 
        ICacheRepository cacheRepository,
        IBasketRepository basketRepository
        )
        : IServiceManager
    {
        private readonly Lazy<IProductService> _productService
            = new (() => new ProductService(unitOfWork, mapper));

        private readonly Lazy<IBasketService> _lazyBasketService
            = new (() => new BasketService(basketRepository, mapper));

        private readonly Lazy<IAuthenticationService> _lazyAuthentication
            = new (() => new AuthenticationService(userManager, options, mapper));

        private readonly Lazy<IOrderService> _lazyOrdersService
            = new (() => new OrderService(unitOfWork, mapper, basketRepository));

        private readonly Lazy<IPaymentService> _lazyPaymentService
            = new (() => new PaymentService(basketRepository, unitOfWork, mapper, configuration));

        private readonly Lazy<ICacheService> _lazyCacheService
            = new(() => new CacheService(cacheRepository));

        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _lazyBasketService.Value;
        public IAuthenticationService AuthenticationService => _lazyAuthentication.Value;
        public IOrderService OrderService => _lazyOrdersService.Value;
        public IPaymentService PaymentService => _lazyPaymentService.Value;
        public ICacheService CacheService => _lazyCacheService.Value;
    }
}
