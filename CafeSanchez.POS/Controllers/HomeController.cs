using CafeSanchez.POS.Models;
using CafeSanchez.POS.Services.Auth;
using CafeSanchez.POS.Services.OrderManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CafeSanchez.POS.Controllers;

public class HomeController(ILogger<HomeController> logger, LoginService userService) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly LoginService _userService = userService;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/Login")]
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

    [Authorize, HttpPost("/Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index");
    }

    [Authorize, HttpGet("/CreateUser")]
    public IActionResult CreateUser()
    {
        ViewBag.CreateNew = true;
        return View("AdminUser");
    }

    [Authorize, HttpPost("/CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUserModel model)
    {
        _userService.CreateUser(model.Username, model.Password, model.Fullname, model.Email);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index");
    }

    [Authorize, HttpGet("/ChangePassword")]
    public IActionResult ChangePassword()
    {
        ViewBag.CreateNew = false;
        return View("AdminUser");
    }

    [Authorize, HttpPost("/ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        string username = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Name).Value;

        if (_userService.Validate(username, model.OldPassword, out User? user))
        {
            _userService.ChangePassword(username, model.NewPassword);
        }
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
