using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeratGraphic.Models.Account;
using Microsoft.AspNetCore.Identity;
using SeratGraphic.DomainModels.Entities;
using SeratGraphic.Messaging.WhiteSms;

namespace SeratGraphic.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly WhiteSmsService _whiteSmsService;
        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            WhiteSmsService whiteSmsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _whiteSmsService = whiteSmsService;
        }
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.PhoneNumber, loginModel.Password, true, true);

                if (result.Succeeded)
                {
                    return RedirectPermanent("/");
                }
                else
                {
                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "حساب کاربری شما فعال نشده است");
                    }
                    else
                    {
                        ModelState.AddModelError("", "کاربری یافت نشد");
                    }
                }
            }
            return View(loginModel);
        }

        [Route("register")]
        public IActionResult Register(string returnUrl)
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(new User
                {
                    UserName = registerModel.PhoneNumber,
                    PhoneNumber = registerModel.PhoneNumber,
                    FullName = registerModel.FullName,
                    RegisterDate = DateTime.Now
                }, registerModel.Password);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(registerModel.PhoneNumber);

                    await _userManager.AddToRoleAsync(user, "User");

                    var verificationCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                    var smsResponse = await _whiteSmsService.SendAsync($"صراط گرافیک : کدفعالسازی شما : {verificationCode}", user.PhoneNumber);

                    return RedirectToAction(nameof(Verify), new { phoneNumber = registerModel.PhoneNumber });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerModel);
        }

        [Route("verify")]
        public IActionResult Verify(string phoneNumber)
        {
            ViewBag.PhoneNumber = phoneNumber;
            return View();
        }

        [Route("verify")]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Verify(VerificationModel verificationModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(verificationModel.PhoneNumber);

                if (user == null)
                {
                    ModelState.AddModelError("", "شماره همراه مربوطه معتبر نمی باشد");
                }

                if (user.PhoneNumberConfirmed)
                {
                    ModelState.AddModelError("", "حساب کاربری مربوطه قبلا فعالسازی شده است");
                }

                var result = await _userManager.ChangePhoneNumberAsync(user, verificationModel.PhoneNumber, verificationModel.Code);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);

                    return RedirectPermanent("/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }


            ViewBag.PhoneNumber = verificationModel.PhoneNumber;
            return View(verificationModel);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectPermanent("/");
        }
    }
}