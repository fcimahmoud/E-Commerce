
namespace Shared.OrderModels
{
    public record OrderResult
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; }
        public AddressDTO ShippingAddress { get; init; }
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;
        public ICollection<OrderItemDTO> OrderItems { get; init; } = new List<OrderItemDTO>();
        public string PaymentStatus { get; init; }
        public string DeliveryMethod { get; init; }
        public string PaymentIntentId { get; init; } = string.Empty;
        public decimal SubTotal { get; init; }
        public decimal Total { get; init; }
    }
}
