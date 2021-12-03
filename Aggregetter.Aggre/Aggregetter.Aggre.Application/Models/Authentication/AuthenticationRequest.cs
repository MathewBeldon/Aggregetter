using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Models.Authentication
{
    public sealed class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
