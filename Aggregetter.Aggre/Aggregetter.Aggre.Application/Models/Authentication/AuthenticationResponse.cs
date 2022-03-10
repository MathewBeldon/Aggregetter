namespace Aggregetter.Aggre.Application.Models.Authentication
{
    public sealed class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
