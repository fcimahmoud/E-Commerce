﻿
namespace Domain.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethod { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
