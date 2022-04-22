using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SinavOlusturma.Models;
using SinavOlusturma.Models.ViewModels;


namespace SinavOlusturma.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {

            if (ModelState.IsValid)
            {
                SinavDbContext db = new SinavDbContext();
                var Usr = db.Users.FirstOrDefault(x => x.UserName == loginModel.UserName && x.Password == loginModel.Password);
                if (Usr != null)
                {
                    var claims = new List<Claim>{
                                                    new Claim(ClaimTypes.Name, loginModel.UserName),
                                                    new Claim("Id",Usr.Id.ToString()),
                                                };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return Redirect("/Exam");
                }
                else
                {
                    TempData.Add("Result", "Kullanıcı Adı veya Şifreniz Hatalı!");
                }
            }
         
            

            return Redirect("/Login");
        }

        [Route("/logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
