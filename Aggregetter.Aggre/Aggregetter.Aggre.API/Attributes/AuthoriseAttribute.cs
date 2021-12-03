using Aggregetter.Aggre.Identity.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Aggregetter.Aggre.API.Attributes
{
    public class AuthoriseAttribute : AuthorizeAttribute
    {
        public AuthoriseAttribute(params Role[] roles)
        {
            Roles = string.Join(",", roles.Select(role => role.ToString()));
        }
    }
}
