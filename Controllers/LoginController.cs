using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PainAssessment.Interfaces;
using PainAssessment.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace PainAssessment.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        private readonly IAccountService accountService;

        private const string REDIRECT_CNTR = "Home";
        private const string REDIRECT_ACTN = "Index";
        private const string FIRSTSIGNIN_ACTN = "FirstSignIn";

        /**
         * Constructor
         */
        public LoginController(ILogger<LoginController> logger, ILoginService loginService, IAccountService accountService)
        {
            this.loginService = loginService;
            this.accountService = accountService;
        }

        /**
         * Return Login Page to user
         */
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /**
         * Description: To allow user to log in to the system to access authorized features
         */
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if(await AuthenticateUser(model) == true)
                {
                    var accId = loginService.GetAccountId();
                    var isFirstSignIn = loginService.IsFirstSignIn(accId);
                    if (isFirstSignIn.Equals("true"))
                    {
                        var account = accountService.GetAccount(accId);
                        account.FirstSignIn = false;
                        loginService.SetFirstSignInFalse(account);
                        return RedirectToAction(FIRSTSIGNIN_ACTN);
                    }
                    return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
                }
                else
                {
                    ViewData["Message"] = "Login details is incorrect";
                    ViewData["MsgType"] = "danger";
                    return View(model);
                }
            }
        }

        /**
         * Function to authenticate the user and set identity
         */
        private async Task<bool> AuthenticateUser(LoginModel model)
        {
            string username = model.Username;
            string password = model.Password;

            var user = loginService.Login(username, password);

            if (user != null)
            {

                /* 
                 * 1. Setup 1 ViewModel (storing data retreived database)
                 * 2. loginService.Login will verify entered credentials
                 * 3. Need to determine a way to retrieve credential details if found
                 * 4. Create new list of claims (claims) where each represent a peice of detail
                 * 5. Create new instance of ClaimsIdentity (with claims) and assign principal with the ClaimsIdentity
                 * 6. call await HttpContext.SignInAsyunc to register persistent authentication
                 * 7. return true and redirect to the page after successful login
                 * 
                 */

                // Attributes that are for authentication
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.GetGuid)),
                    new Claim(ClaimTypes.Role, user.GetRole),
                };

                // Initialise instance of ClaimsIdentity
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                // Initialise new instance of ClaimsPrincipal
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = model.RememberMe
                    //IsPersistent = true 
                });

                return true;
            }
            return false;
        }

        /**
         * Function to allow user to log out
         */
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to login page
            return LocalRedirect("/");
        }

        /**
         * First Sign In Page
         * This is a page for future implementation for the client as they want participants to accesss training when first sign in like a popup
         */
        [Authorize]
        public IActionResult FirstSignIn()
        {
            return View();
        }


    }
}
