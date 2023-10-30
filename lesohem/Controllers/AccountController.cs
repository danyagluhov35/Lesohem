using lesohem.Models;
using lesohem.Models.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace lesohem.Controllers
{
    public class AccountController : Controller
    {
        private readonly LesohemContext _db;
        public AccountController(LesohemContext db) => _db = db;
        public IActionResult Login() => View(new User());

        [HttpPost]
        public async Task<IActionResult> Login(User modelUser)
        {
            var res = HttpContext.Request.Headers["Accept-Encoding"];
            
            if(ModelState.IsValid)
            {
                try
                {
                    User? data = await _db.Users.FirstOrDefaultAsync(u => u.Name == modelUser.Name && u.Password == modelUser.Password);
                    if (data != null)
                    {
                        var claims = new List<Claim>() { new Claim(ClaimTypes.Name, data.Name!) };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, "CookieUserIdentity")));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        modelUser.ErrorMessage = "Пользователь не найден";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error - {ex.Message}");
                }
            }
            return View(modelUser);
        }
    }
}
