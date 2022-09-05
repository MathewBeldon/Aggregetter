using Aggregetter.Aggre.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Identity.Seed
{
    public static class AddRoles
    {
        public static Task<IdentityResult[]> InitiliseAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleCreationTasks = Enum.GetValues<Role>()
                .Select(role => role.ToString())
                .Where(enumRole => !roleManager.Roles.Any(role => string.Equals(role.NormalizedName, enumRole, StringComparison.OrdinalIgnoreCase)))
                .Select(role => roleManager.CreateAsync(new IdentityRole(role)));

            return Task.WhenAll(roleCreationTasks);
        }
    }
}