using JustWatch.Application.DTO;
using JustWatch.Application.Interfaces;
using JustWatch.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JustWatch.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var request = new RegisterRequestDTO
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
            };

            var result = await _authService.RegisterAsync(request);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(registerViewModel);
            }

            return RedirectToAction("LogIn", "Account");
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var request = new LoginRequestDTO
            {
                Email = loginViewModel.Email,
                Password = loginViewModel.Password,
            };

            var result = await _authService.LoginAsync(request);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Wrong email or password");
                return View(loginViewModel);
            }

            var user = result.Data;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, Settings.AuthCookie);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(Settings.AuthCookie, principal);

            return RedirectToAction("Start", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Settings.AuthCookie);
            return RedirectToAction("Start", "Home");
        }
    }
}
