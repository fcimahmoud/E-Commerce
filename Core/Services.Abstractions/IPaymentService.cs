
namespace Services.Abstractions
{
    public interface IPaymentService
    {
        public Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId);
        public Task UpdateOrderPaymentStatus(string request, string stripeHeader);
    }
}
