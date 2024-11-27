
namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _lazyBasketService;
        private readonly Lazy<IAuthenticationService> _lazyAuthentication;
        private readonly Lazy<IOrderService> _lazyOrdersService;
        private readonly Lazy<IPaymentService> _lazyPaymentService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User> userManager, IOptions<JwtOptions> options, IConfiguration configuration)
        {
            _productService = new Lazy<IProductService>
                (()=> new ProductService(unitOfWork, mapper));

            _lazyBasketService = new Lazy<IBasketService>
                (() => new BasketService(basketRepository, mapper));

            _lazyAuthentication = new Lazy<IAuthenticationService>
                (() => new AuthenticationService(userManager, options, mapper));

            _lazyOrdersService = new Lazy<IOrderService>
                (() => new OrderService(unitOfWork, mapper, basketRepository));

            _lazyPaymentService = new Lazy<IPaymentService>
                (() => new PaymentService(basketRepository, unitOfWork, mapper, configuration));
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthentication.Value;

        public IOrderService OrderService => _lazyOrdersService.Value;

        public IPaymentService PaymentService => _lazyPaymentService.Value;
    }
}
