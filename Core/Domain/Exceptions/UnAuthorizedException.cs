
namespace Domain.Exceptions
{
    public class UnAuthorizedException(string message = "Invalid Email or Password")
        : Exception(message)
    {
    }
}
