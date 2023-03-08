using ECommerce.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            ApplicationUser existUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existUser==null)
            {
                Dictionary<string,object> errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email yada şifre yanlış" });
                return;
            }
            Boolean passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);

            if (passwordCheck == false)
            {
                Dictionary<string, object> errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email yada şifre yanlış" });
                return;
            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(),OidcConstants.AuthenticationMethods.Password);
        }
    }
}
