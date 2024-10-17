
namespace Presentation
{
    public class AuthenticationController(IServiceManager serviceManager) 
        : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO loginDTO)
        {
            var result = await serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(UserRegisterDTO registerModel)
        {
            var result = await serviceManager.AuthenticationService.RegisterAsync(registerModel);
            return Ok(result);
        }
    }
}
