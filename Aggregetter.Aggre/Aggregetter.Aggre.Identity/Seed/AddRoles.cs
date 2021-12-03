using Aggregetter.Aggre.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Identity.Seed
{
    public static class AddRoles
    {
        public static async Task InitiliseAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach(Enums.Role enumRole in Enum.GetValues(typeof(Enums.Role)))
            {
                var normalisedEnumRole = enumRole.ToString().ToUpper();
                var namedEnumRole = enumRole.ToString();

                if (!roleManager.Roles.Any(role => role.NormalizedName == normalisedEnumRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(namedEnumRole));
                }
            }
        }
    }
}
