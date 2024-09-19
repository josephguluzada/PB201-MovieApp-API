using Microsoft.AspNetCore.Mvc;
using PB201MovieApp.MVC.Services.Interfaces;
using PB201MovieApp.MVC.ViewModels.AuthVMs;

namespace PB201MovieApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM vm)
        {
            if (!ModelState.IsValid) return View();

            var data = await _authService.Login(vm);

            HttpContext.Response.Cookies.Append("token", data.AccessToken, new CookieOptions
            {
                Expires = data.ExpireDate,
                HttpOnly = true
            });

            return RedirectToAction("Index", "Genre");
        }

        public IActionResult Logout()
        {
            _authService.Logout();

            return RedirectToAction("login");
        }
    }
}
