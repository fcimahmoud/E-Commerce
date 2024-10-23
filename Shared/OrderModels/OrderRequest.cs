

namespace Shared.OrderModels
{
    public record OrderRequest
    {
        public string BasketId { get; init; }
        public AddressDTO ShippingAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
