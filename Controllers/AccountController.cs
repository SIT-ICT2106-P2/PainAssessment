using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PainAssessment.Interfaces;
using PainAssessment.Models;
using PainAssessment.ViewModels;
using System;
using BC = BCrypt.Net.BCrypt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PainAssessment.Areas.Admin.Services;
using PainAssessment.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using PainAssessment.Areas.Admin.Models.Builder;

namespace PainAssessment.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IAdministratorService administratorService;
        private readonly ILoginService loginService;
        private readonly IPractitionerService practitionerService;
        private readonly IClinicalAreaService clinicalAreaService;
        private readonly IPainEducationService painEducationService;
        private readonly IPracticeTypeService practiceTypeService;


        private const string REDIRECT_CNTR = "Home";
        //private const string DIRECT_CNTR = "Login";
        private const string DIRECT_ACTN = "Index";

        /**
         * Constructor
         */
        public AccountController(
            IAccountService accountService,
            IAdministratorService administratorService,
            ILoginService loginService,
            IPractitionerService practitionerService,
            IClinicalAreaService clinicalAreaService,
            IPainEducationService painEducationService,
            IPracticeTypeService practiceTypeService

           )
        {
            this.accountService = accountService;
            this.administratorService = administratorService;
            this.loginService = loginService;
            this.practitionerService = practitionerService;
            this.clinicalAreaService = clinicalAreaService;
            this.painEducationService = painEducationService;
            this.practiceTypeService = practiceTypeService;

        }

        /**
         * Change Password
         * Function to return view for the user to change password
         */

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        /**
         * Description: Function to change password
         * It will call accountService to check if there's an account with the same username
         * 
         */
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (accountService.CheckUsername(model.Username).Equals(true))
                {
                    System.Diagnostics.Debug.WriteLine(model.NewPassword);
                    var user = accountService.GetAccountByUsername(model.Username);
                    System.Diagnostics.Debug.WriteLine(user.Password);
                    if (user != null)
                    {
                        user.Password = BC.HashPassword(model.NewPassword);
                        System.Diagnostics.Debug.WriteLine(user.Password);
                        accountService.UpdatePassword(user);
                        ViewData["Message"] = "Password successfully changed!";
                        ViewData["MsgType"] = "success";
                        return View(model);
                    }
                    return View();
                };
                return View();
            }
            else
            {
                return View();
            }

        }

        /**
         * Description: When users wants to create accounts for administrators and practitioners. 
         * 
         * Return to View(CreateAccountModel) if the user is an administrator, practitioner will see a 404 page
         */
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateAccount()
        {
            ViewData["ClinicalAreaID"] = new SelectList(clinicalAreaService.GetAllClinicalAreas(), "Id", "Name");
            ViewData["PracticeTypeID"] = new SelectList(practiceTypeService.GetAllPracticeTypes(), "Id", "Name");
            ViewData["PainEducationID"] = new MultiSelectList(painEducationService.GetAllPainEducations(), "Id", "Name");
            return View(new CreateAccountModel());
        }

        /**
         * Description: When users wants to create accounts for administrators and practitioners
         * 
         * Before creating an account, the accountService will check for any duplicate username. If it is not, it will do the following:
         * 
         * 1. If the account role is an administrator, it will call the administratorService.
         * 2. If the account role is an practitioners, it will call the practitionerService which is done by P2-3
         * 
         */
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateAccount(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (accountService.CheckUsername(model.Username).Equals(false))
                {
                    var AccountId = Guid.NewGuid();
                    var cost = 16;

                    Account account = new Account
                    {
                        AccountId = AccountId,
                        Username = model.Username,
                        Password = BC.HashPassword(model.Password, cost),
                        Role = model.Role,
                        CreatedAt = DateTime.Now,
                        AccountStatus = "active",
                        FirstSignIn = true,
                        //IsAuthenticated = false,
                    };

                    if (account != null)
                    {
                        accountService.CreateAcc(account);
                        if (account.Role == "Administrator")
                        {
                            // Administrator service
                            /*Administrator admin = new Administrator
                            {
                                Account = account,
                                FullName = model.FullName,
                                Experience = 0
                            };*/

                            Administrator admin = new Administrator(
                                model.FullName, model.Username,  "0", model.ClinicalAreaID, AccountId);
                            administratorService.CreateAdmin(admin);

                        }
                        else if (account.Role == "Practitioner")
                        {
                            Practitioner p = new Practitioner(
                                model.FullName, "0 years", "0", model.ClinicalAreaID, model.PracticeTypeID, AccountId);
                            practitionerService.CreatePractitioner(p);
                            practitionerService.SavePractitioner();
                        }
                        return RedirectToAction(DIRECT_ACTN, REDIRECT_CNTR);
                    }
                    return View(model);
                }
                else
                {
                    ViewData["Message"] = "Username is taken. Please re-enter a username";
                    ViewData["MsgType"] = "danger";
                    return View(model);
                }

            }
            return View(model);
        }

    }
}
