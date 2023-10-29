using lesohem.Models;
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
        public IActionResult Index() => View();

        public async Task<IActionResult> Login(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User? data = await _db.Users.FirstOrDefaultAsync(u => u.Name == user.Name && u.Password == user.Password);
                    if(data != null)
                    {
                        var claims = new List<Claim>() { new Claim(ClaimTypes.Name, data?.Name!) };
                        HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, "UserCookies")));
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    user.NotFound = "Пользователь не найден";
            }
            catch (Exception ex)
            {
                // Выполнить логгирование
                Console.WriteLine(ex.Message);
            }

            return View(user);
        }
    }
}
