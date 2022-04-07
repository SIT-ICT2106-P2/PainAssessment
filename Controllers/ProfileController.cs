using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PainAssessment.Areas.Admin.Services;
using Microsoft.Extensions.Logging;
using PainAssessment.Interfaces;
using PainAssessment.ViewModels.Profile;
using System.Security.Claims;
using PainAssessment.Models;
using PainAssessment.Areas.Admin.Models;
using System.Text.Json;
using BC = BCrypt.Net.BCrypt;
using PainAssessment.ViewModels;

namespace PainAssessment.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        /**
         * Include services
         */
        private readonly ILoginService loginService;
        private readonly IPractitionerService practitionerService;
        private readonly IAdministratorService administratorService;
        private readonly IClinicalAreaService clinicalAreaService;
        private readonly IAccountService accountService;

        public ProfileController(ILogger<ProfileController> logger, ILoginService loginService, IPractitionerService practitionerService, IAdministratorService administratorService, IClinicalAreaService clinicalAreaService, IAccountService accountService)
        {
            this.loginService = loginService;
            this.practitionerService = practitionerService;
            this.administratorService = administratorService;
            this.clinicalAreaService = clinicalAreaService;
            this.accountService = accountService;
        }
        /*
         returns the details based on the role of the user
         if the user has no role, return to the home page
         */
        public ActionResult ViewProfile()
        {

            Guid userid = loginService.GetAccountId();
            var user = accountService.GetAccount(userid);

            switch (user.Role)
            {
                case "Administrator":
                    // admin
                    return (ActionResult)AdminView(userid);

                case "Practitioner":
                    // practitioner
                    var practitionerProfile = PractionerView(user);
                    return View("ViewPractitionerProfile", practitionerProfile);
                default:
                    // unrecognised method; return to the blank form
                    return RedirectToAction("Index", "Home");

            }

        }


        /*
         returns the details of the specific admin
         */
        public IActionResult AdminView(Guid id)
        {

            Administrator admin = administratorService.GetOneAdmin(id);
            var clinicalArea = clinicalAreaService.GetClinicalArea(admin.ClinicalAreaID);
            admin.ClinicalArea = clinicalArea.Name;
            return View("ViewAdminProfile", admin);
        }

        /*
         returns the details of the specific practitioner
         */
        private PractionerModel PractionerView(Account user)
        {
            Practitioner practionerDetails = practitionerService.GetPractitioner(user.AccountId);
            ClinicalArea clinicalPrac = clinicalAreaService.GetClinicalArea(practionerDetails.ClinicalAreaID);
            var practionerViewModel = new PractionerModel
            {
                Name = practionerDetails.Name,
                FullName = user.Username,
                Role = user.Role,
                AccountID = user.AccountId,
                PriorPainEducation = practionerDetails.PriorPainEducation,
                ClinicalArea = clinicalPrac.Name,
                PracticeType = practionerDetails.PracticeType.Name

            };
            return practionerViewModel;
        }


        private UpdateUsernameModel UpdatePractionerView(Account user)
        {
            Practitioner practionerDetails = practitionerService.GetPractitioner(user.AccountId);
            ClinicalArea clinicalPrac = clinicalAreaService.GetClinicalArea(practionerDetails.ClinicalAreaID);
            var practionerViewModel = new UpdateUsernameModel
            {
                Name = practionerDetails.Name,
                NewUserName = user.Username,
                Role = user.Role,
                AccountID = user.AccountId,
                PriorPainEducation = practionerDetails.PriorPainEducation,
                ClinicalArea = clinicalPrac.Name,
                PracticeType = practionerDetails.PracticeType.Name

            };
            return practionerViewModel;
        }

        private UpdatePasswordModel UpdatePasswordView(Account user)
        {
            Practitioner practionerDetails = practitionerService.GetPractitioner(user.AccountId);
            var practionerViewModel = new UpdatePasswordModel
            {
                Username = user.Username
            };
            return practionerViewModel;
        }

        /**
         * returns the information that the user wishes to edit
         */
        [HttpGet]
        public IActionResult EditProfile()
        {

            Guid userid = loginService.GetAccountId();
            var user = accountService.GetAccount(userid);
            System.Diagnostics.Debug.WriteLine(user.AccountId);
            System.Diagnostics.Debug.WriteLine(user.AccountStatus);
            System.Diagnostics.Debug.WriteLine(user.Password);
            System.Diagnostics.Debug.WriteLine(user.Username);
            switch (user.Role)
            {
                case "Administrator":
                    // admin
                    return (ActionResult)AdminView(userid);

                case "Practitioner":
                    // practitioner
                    var practitionerProfile = UpdatePractionerView(user);
                    return View("EditProfile", practitionerProfile);
                default:
                    // unrecognised method; return to the blank form
                    return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditProfile(UpdateUsernameModel practionerModel)
        {
            Guid userid = loginService.GetAccountId();
            var userAccount = accountService.GetAccount(userid);
            userAccount.Username = practionerModel.NewUserName;
            practionerModel = UpdatePractionerView(userAccount);
            System.Diagnostics.Debug.WriteLine(practionerModel.AccountID);
            System.Diagnostics.Debug.WriteLine(practionerModel.NewUserName);
            accountService.UpdateAccountStatus(userAccount);
            System.Diagnostics.Debug.WriteLine(practionerModel.NewUserName);
            ViewData["Message"] = "Username successfully changed!";
            ViewData["MsgType"] = "success";
            return View("EditProfile", practionerModel);
        }

        /**
     * returns the information that the user wishes to edit
     */
        [HttpGet]
        public IActionResult UpdatePassword()
        {

            Guid userid = loginService.GetAccountId();
            var user = accountService.GetAccount(userid);
            switch (user.Role)
            {
                case "Administrator":
                    // admin
                    return (ActionResult)AdminView(userid);

                case "Practitioner":
                    // practitioner
                    var practitionerProfile = UpdatePasswordView(user);
                    return View("UpdatePassword", practitionerProfile);
                default:
                    // unrecognised method; return to the blank form
                    return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult UpdatePassword(UpdatePasswordModel practionerModel)
        {
            Guid userid = loginService.GetAccountId();
            var userAccount = accountService.GetAccount(userid);
            userAccount.Password = BC.HashPassword(practionerModel.NewPassword);
            practionerModel = UpdatePasswordView(userAccount);
            System.Diagnostics.Debug.WriteLine(practionerModel.NewPassword);
            accountService.UpdatePassword(userAccount);
            ViewData["Message"] = "Password successfully changed!";
            ViewData["MsgType"] = "success";
            return View("UpdatePassword", practionerModel);
        }

    }
}
