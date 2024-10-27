
namespace Services.Specifications
{
    internal class OrderWithPaymentIntentIdSpecifications : Specifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) 
            : base(order => order.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
