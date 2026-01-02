using Microsoft.AspNetCore.Mvc;

namespace JustWatch.Web.Controllers
{
     public class AccountController : Controller
     {
          public IActionResult LogIn()
          {
               return View();
          }
          public IActionResult Register()
          {
               return View();
          }
     }
}
