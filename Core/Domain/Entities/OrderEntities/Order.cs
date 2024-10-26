

namespace Domain.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public Order() { }
        public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress; 
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        // 1. User Email
        public string UserEmail { get; set; }
        // 2. Address 
        public Address ShippingAddress { get; set; }
        // 3. Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        // 4. Order Items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Collection Navigational Prop
        // 5. Payment Status
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        // 6. Delivery Method
        public DeliveryMethod DeliveryMethod { get; set; } // Ref Navigational Prop
        public int? DeliveryMethodId { get; set; }
        // 7. SubTotal = {Order Item Price * Quantity} For All Items
        public decimal SubTotal { get; set; }
        // 8. Payment => Required for the next Session
        public string PaymentIntentId { get; set; } = string.Empty;

    }
}
