﻿
namespace Shared.OrderModels
{
    public record DeliveryMethodResult
    {
        public int Id { get; init; }
        public string ShortName { get; init; }
        public string Description { get; init; }
        public string DeliveryTime { get; init; }
        public decimal Price { get; init; }
    }
}
