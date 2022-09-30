using Aggregetter.Aggre.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Identity.Seed
{
    public static class AddRoles
    {
        public static IEnumerable<IdentityResult> Initilise(RoleManager<IdentityRole> roleManager)
        {
            return Enum.GetValues<Role>()
                .Select(role => role.ToString())
                .Where(enumRole => !roleManager.Roles.Any(role => string.Equals(role.NormalizedName.ToLower(), enumRole.ToLower())))
                .Select(role => roleManager.CreateAsync(new IdentityRole(role)).Result);
        }
    }
}