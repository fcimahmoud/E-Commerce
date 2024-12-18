﻿
namespace Services
{
    internal class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            // Send SecretKey To Stripe to connect With the Right Account
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("StripeSettings")["SecretKey"];

            // Get Basket
            var basket = await basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);

            // Confirm The Price of Each Product
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>()
                    .GetAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }

            // Confirm The Shipping Price Of Delivery Method
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Is Selected");
            var method = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = method.Price;

            // Calculate The Total Amount
            var amount = (long)(basket.Items.Sum(item => item.Quantity * item.Price) + basket.ShippingPrice) * 100;

            // Create Or Update PaymentIntent 
            var service = new PaymentIntentService();
            if(string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                // Create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await service.CreateAsync(createOptions);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            await basketRepository.UpdateBasketAsync(basket);
            return mapper.Map<BasketDTO>(basket);
        }
        public async Task UpdateOrderPaymentStatus(string request, string stripeHeader)
        {
            var endPointSecret = configuration.GetRequiredSection("StripeSettings")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeader, endPointSecret);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentStatusFailed(paymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentStatusRecieved(paymentIntent.Id);
                    break;
                default:
                    Console.WriteLine("Unhandled Event Type: {0}", stripeEvent.Type);
                    break;
            }
        }

        private async Task UpdatePaymentStatusFailed(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId))
                ?? throw new OrderNotFoundException($"Order with PaymentIntentId {paymentIntentId} Not Found!.");

            order.PaymentStatus = OrderPaymentStatus.PaymentFailed;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }
        private async Task UpdatePaymentStatusRecieved(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId))
                ?? throw new OrderNotFoundException($"Order with PaymentIntentId {paymentIntentId} Not Found!.");

            order.PaymentStatus = OrderPaymentStatus.PaymentReceived;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
