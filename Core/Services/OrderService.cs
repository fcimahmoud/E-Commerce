
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Shared.OrderModels;

namespace Services
{
    internal class OrderService (IUnitOfWork unitOfWork, IMapper mapper, 
        IBasketRepository basketRepository)
        : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            // 1. Address
            var address = mapper.Map<Address>(orderRequest.ShippingAddress);

            // 2. Order Items :: Basket => Basket Items => Order Items
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>()
                    .GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }

            // 3. Delivery Method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(orderRequest.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            // 4. SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // Save To DataBase
            var order = new Order(userEmail, address, orderItems, deliveryMethod, subTotal);
            await unitOfWork.GetRepository<Order, Guid>()
                .AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            // Map & Return
            return mapper.Map<OrderResult>(order);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
            => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);

        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
