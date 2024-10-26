
namespace Domain.Exceptions
{
    public class UserNotFoundException(string email)
        : NotFoundException($"No User With Email {email} Was Found!.")
    {
    }
}
