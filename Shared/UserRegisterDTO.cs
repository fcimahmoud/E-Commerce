
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record UserRegisterDTO
    {
        [Required(ErrorMessage = "DisplayName Is Required")]
        public string DisplayName { get; init; }

        [EmailAddress]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; init; }

        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; init; }
        public string? Phone { get; init; }
    }
}
