using System.IO.Pipelines;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(
            UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user  = await _userManager.FindByEmailAsync(model.Email);

                if(user != null)
                {
                    await _signInManager.SignOutAsync();

                    if(!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınızı onaylayınız.");
                        return View(model);
                    }

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,true);

                    if(result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        return RedirectToAction("Index","Home");
                    }
                    else if(result.IsLockedOut)
                    {
                        var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError("", $"Hesabınız kitlendi, Lütfen {timeLeft.Minutes} dakika sonra deneyiniz");
                    }
                    else
                    {
                        ModelState.AddModelError("", "parolanız hatalı");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "bu email adresiyle bir hesap bulunamadı");
                }
            }
            return View(model);
        }
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser { 
                    UserName = model.UserName,
                    Email = model.Email, 
                    FullName = model.FullName 
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail","Account", new { user.Id, token} );

                    // email
                    await _emailSender.SendEmailAsync(user.Email, "Hesap Onayı", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:5034{url}'>tıklayınız.</a>");

                    TempData["message"]  = "Email hesabınızdaki onay mailini tıklayınız."; 
                    return RedirectToAction("Login","Account");
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);                    
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            if(Id == null || token == null)
            {
                TempData["message"]  = "Geçersiz token bilgisi";
                return View();
            }

            var user = await _userManager.FindByIdAsync(Id);

            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if(result.Succeeded)
                {
                    TempData["message"]  = "Hesabınız onaylandı";
                    return RedirectToAction("Login","Account");
                }
            }

            TempData["message"]  = "Kullanıcı bulunamadı";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                TempData["message"] = "Eposta adresinizi giriniz.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if(user == null)
            {
                TempData["message"] = "Eposta adresiyle eşleşen bir kayıt yok.";
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var url = Url.Action("ResetPassword","Account", new { user.Id, token} );

            await _emailSender.SendEmailAsync(Email, "Parola Sıfırlama", $"Parolanızı yenilemek için linke <a href='http://localhost:5034{url}'>tıklayınız.</a>.");

            TempData["message"] = "Eposta adresinize gönderilen link ile şifrenizi sıfırlayabilirsiniz.";

            return View();

        }

    }
}