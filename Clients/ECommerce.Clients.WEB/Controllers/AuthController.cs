using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Clients.WEB.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signInInput)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            Response<bool> response = await _identityService.SıgnIn(signInInput);

            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(error =>  ModelState.AddModelError(string.Empty,error));

                return View();
            }

            return RedirectToAction(nameof(Index), "Home");

            
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);//It's clears to cookie
            await _identityService.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}
