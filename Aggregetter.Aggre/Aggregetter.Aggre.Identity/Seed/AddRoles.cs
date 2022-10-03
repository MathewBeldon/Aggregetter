using Aggregetter.Aggre.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Identity.Seed
{
    public static class AddRoles
    {
        public static async Task InitiliseAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (Role enumRole in Enum.GetValues(typeof(Role)))
            {
                var enumRoleName = enumRole.ToString();

                if (!roleManager.Roles.Any(role => string.Equals(role.NormalizedName.ToLower(), enumRoleName.ToLower())))
                {
                    await roleManager.CreateAsync(new IdentityRole(enumRoleName));
                }
            }
        }
    }
}
