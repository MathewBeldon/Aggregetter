using System.ComponentModel.DataAnnotations;

namespace Aggregetter.Aggre.Application.Models.Authentication
{
    public sealed class RegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
