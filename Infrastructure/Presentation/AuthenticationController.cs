﻿
namespace Presentation
{
    public class AuthenticationController(IServiceManager serviceManager) 
        : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO loginDTO)
            => Ok(await serviceManager.AuthenticationService.LoginAsync(loginDTO));

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(UserRegisterDTO registerModel)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerModel));

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
            => Ok(await serviceManager.AuthenticationService.CheckEmailExist(email));

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.GetUserByEmail(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.GetUserAddress(email);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.UpdateUserAddress(address, email);
            return Ok(result);
        }
    }
}
