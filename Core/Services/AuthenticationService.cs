
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Shared.ErrorModels;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager) : IAuthenticationService
    {
        public async Task<UserResultDTO> LoginAsync(LoginDTO loginModel)
        {
            // Check If there is user under this Email
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");

            // Check If The Password Is Correct for this Email
            var result = await userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result) throw new UnAuthorizedException();

            // Create Token and Return Response
            return new UserResultDTO(
                user.DisplayName,
                user.Email!,
                "Token");
        }

        public Task<UserResultDTO> RegisterAsync(UserRegisterDTO registerModel)
        {
            throw new NotImplementedException();
        }
    }
}
