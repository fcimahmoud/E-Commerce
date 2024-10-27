
namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications : Specifications<Order>
    {
        public OrderWithIncludeSpecifications(Guid id) 
            : base(order => order.Id == id)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.OrderItems);
        }
        public OrderWithIncludeSpecifications(string email)
            : base(order => order.UserEmail == email)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.OrderItems);

            SetOrderBy(order => order.OrderDate);
        }
    }
}
