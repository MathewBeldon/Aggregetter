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
            foreach(Enums.Role enumRole in Enum.GetValues(typeof(Enums.Role)))
            {
                var enumRoleName = enumRole.ToString();
                var enumRoleNormalised = enumRoleName.ToUpper();

                if (!roleManager.Roles.Any(role => role.NormalizedName == enumRoleNormalised))
                {
                    await roleManager.CreateAsync(new IdentityRole(enumRoleName));
                }
            }
        }
    }
}
