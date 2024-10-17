
global using Domain.Exceptions;
global using Microsoft.AspNetCore.Identity;
global using Shared.ErrorModels;

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

        public async Task<UserResultDTO> RegisterAsync(UserRegisterDTO registerModel)
        {
            var user = new User()
            {
                Email = registerModel.Email,
                DisplayName = registerModel.DisplayName,
                PhoneNumber = registerModel.Phone,
                UserName = registerModel.UserName,
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }

            return new UserResultDTO(
                user.DisplayName,
                user.Email!,
                "Token");
        }
    }
}
