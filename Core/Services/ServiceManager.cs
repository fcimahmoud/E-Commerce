
using Domain.Contracts;
using Microsoft.Extensions.Options;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _lazyBasketService;
        private readonly Lazy<IAuthenticationService> _lazyAuthentication;


        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User> userManager, IOptions<JwtOptions> options)
        {
            _productService = new Lazy<IProductService>
                (()=> new ProductService(unitOfWork, mapper));

            _lazyBasketService = new Lazy<IBasketService>
                (() => new BasketService(basketRepository, mapper));

            _lazyAuthentication = new Lazy<IAuthenticationService>
                (() => new AuthenticationService(userManager, options));
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthentication.Value;
    }
}
