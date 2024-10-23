
namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException(int id)
        : NotFoundException($"No Delivery Method with Id {id} was found!.")
    {
    }
}
