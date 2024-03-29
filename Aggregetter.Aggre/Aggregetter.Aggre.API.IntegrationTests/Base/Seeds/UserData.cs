﻿using Aggregetter.Aggre.Identity.Enums;
using Aggregetter.Aggre.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.IntegrationTests.Base.Seeds
{
    public static class UserData
    {
        public const string Username = "KnownUser";
        public const string Password = "KnownPassword1$";
        public const string Email = "Known@Email.com";
        public static async Task InitialiseAsync(UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = Username,
                Email = Email,
                EmailConfirmed = true,
            };

            var user = userManager.FindByEmailAsync(applicationUser.Email).Result;
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, Password);
                await userManager.AddToRoleAsync(applicationUser, Role.Editor.ToString());
            }
        }
    }
}
