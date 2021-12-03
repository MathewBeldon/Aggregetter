using System.ComponentModel.DataAnnotations;

namespace Aggregetter.Aggre.Application.Models.Authentication
{
    public sealed class RegistrationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "minimum length is 8 characters")]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
