using CafeSanchez.POS.Models;
using CafeSanchez.POS.Services.Auth;
using CafeSanchez.POS.Services.OrderManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CafeSanchez.POS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LoginService _userService;

        public HomeController(ILogger<HomeController> logger, LoginService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            // Validate login and create authorization cookie
            if (_userService.Validate(login.Username, login.Password, out User? user))
            {
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, login.Username),
                    new("Fullname", user.Fullname),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Role, "Cashier")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                
                return RedirectToAction("Index", "Pos");
            }

            return RedirectToAction("Index");
        }

        [HttpPost("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
