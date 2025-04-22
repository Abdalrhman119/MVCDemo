using Demo.DataAccess.Models.IdintityModaels;
using Demo.Peresentation.Helper;
using Demo.Peresentation.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Peresentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    UserName = viewModel.UserName,
                };
                var result = _userManager.CreateAsync(user, viewModel.Password).Result;
                if (result.Succeeded) return RedirectToAction("LogIn");
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(viewModel);
        }
        #endregion

        #region LogIn
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }
                else
                {
                    var flag = _userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                    if (flag)
                    {
                        var result = _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false).Result;
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Login");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login");
                    }
                }
            }
            return View(viewModel);
        }
        #endregion

        #region LogOut
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion

        #region  Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null)
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var url = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = $"<h1>Click Here To Reset Your Password</h1><br/><a href='{url}'>Reset Password</a>"
                    };
                    bool isMaiSent = EmailSettings.SendEmail(email);
                    if (isMaiSent)
                    {
                        return RedirectToAction(nameof(CheckYourInbox));
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View("ForgetPassword");
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as String;
                var token = TempData["token"] as String;

                if (email is null || token is null) return BadRequest();
                else
                {
                    var user = _userManager.FindByEmailAsync(email).Result;
                    if (user is not null)
                    {
                        var res = _userManager.ResetPasswordAsync(user, token, viewModel.Password).Result;
                        if (res.Succeeded) return RedirectToAction(nameof(LogIn));
                    }
                    else ModelState.AddModelError(string.Empty, "Error");

                }
            }
            return View(viewModel);
        }
        #endregion
        public IActionResult CheckYourInbox()
        {
            return View();
        }



    }
}