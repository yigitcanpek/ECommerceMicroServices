using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;
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
    }
}
