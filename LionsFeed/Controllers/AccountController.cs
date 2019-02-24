using CryptoHelper;
using LionsFeed.Data;
using LionsFeed.Models;
using LionsFeed.ViewModel.Account;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LionsFeed.Controllers
{
    public class AccountController : BaseController
    {
        private LionsContext _context;

        public AccountController(LionsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var users = _context.Users.ToList<User>;
            return View();
        }

        //GET
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel model)
        {
            //var wNum = model.WNumber + "@selu.edu";

            var email = model.WNumber + "@selu.edu";

            var userLog = _context.Users.SingleOrDefault(u => (u.Email == email)/*||(u.Email==wNum)*/);


            bool pass = Crypto.VerifyHashedPassword(userLog.Password, model.Password);

            if (pass == true)
            {

                SetLoggedInUser(userLog);

                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, userLog.Email),
                    new Claim(ClaimTypes.Role, userLog.Role)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                //if (userLog.Role == "Admin")
                //{
                //    return RedirectToAction("Index", "ApplicationUsers");
                //}
                //else
                return RedirectToAction("HomeView", "Home");
            }
            else
                return Content("HAHHAHAHa");


        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        //GET
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var email = model.WNumber + "@selu.edu";

            var userLog = _context.Users.SingleOrDefault(u => u.Email == email);

            if (userLog == null)
            {
                var rnd = new Random();
                var pass = "" + rnd.Next(1, 9999);

                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = email,
                    Password = pass,
                    Gender = model.Gender,
                    Role = "User",
                    ImageUrl = (model.Gender == "Male") ? "/upload/img/default.png" : "/upload/img/female.png"

                };

                _context.Users.Add(user);
                _context.SaveChanges();

                string link = Url.Action("ResetPassword", "Account", "Click here to reset your password.", protocol: HttpContext.Request.Scheme);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Test Project", "test.project285@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Confirm Your Email";
                message.Body = new TextPart("html")
                {
                    Text = "<p>Your Email is: " + email + "<br/>" +
                    "Your temporary password is " + pass + "<br/><br/><br/>" +
                    link + "</p>"
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("test.project285@gmail.com", "Swoy@mbhu1234");
                    client.Send(message);
                    client.Disconnect(true);
                }

                return View("DisplayEmail");
            }
            else
                return RedirectToAction(nameof(Register));
        }

        //GET
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            var email = model.WNumber + "@selu.edu";
            var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();

            if (user != null)
            {
                var rnd = new Random();
                var pass = "" + rnd.Next(1, 9999);

                user.Password = pass;

                _context.Users.Update(user);
                _context.SaveChanges();

                string link = Url.Action("ResetPassword", "Account", "Click here to reset your password.", protocol: HttpContext.Request.Scheme);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Test Project", "test.project285@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Confirm Your Email";
                message.Body = new TextPart("html")
                {
                    Text = "<p>Your Email is: " + email + "<br/>" +
                    "Your temporary password is " + pass + "<br/><br/><br/>" +
                    link + "</p>"
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("test.project285@gmail.com", "Swoy@mbhu1234");
                    client.Send(message);
                    client.Disconnect(true);
                }

                return View("DisplayEmail");
            }
            else
                return RedirectToAction(nameof(Register));
        }


        //GET
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var email = model.WNumber + "@selu.edu";
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return Content("Enter a valid Wnumber");
            }
            else
            {

                //var pass = Crypto.VerifyHashedPassword(user.Password, model.tempPassword);
                var pass = false;
                if (user.Password == model.tempPassword)
                {
                    pass = true;
                }

                if (pass == false)
                {
                    return Content("The temp password you entered is incorrect.");
                }
                else
                {
                    user.Password = Crypto.HashPassword(model.NewPassword);
                    _context.SaveChanges();

                    var login = new LogInViewModel
                    {
                        WNumber = model.WNumber,
                        Password = model.NewPassword
                    };
                    return RedirectToAction("Login", login);
                }

            }
        }
    }
}