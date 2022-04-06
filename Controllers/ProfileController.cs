﻿using Microsoft.AspNetCore.Http;
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
using PainAssessment.Areas.Admin.Models.ViewModels.Profile;
using System.Text.Json;

namespace PainAssessment.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        // Include services
        private readonly ILoginService loginService;
        private readonly IPractitionerService practitionerService;
        private readonly IAdministratorService administratorService;
        private readonly IClinicalAreaService clinicalAreaService;

        public ProfileController(ILogger<ProfileController> logger, ILoginService loginService,  IPractitionerService practitionerService, IAdministratorService administratorService, IClinicalAreaService clinicalAreaService)
        {
            _logger = logger;
            this.loginService = loginService;
            this.practitionerService = practitionerService;
            this.administratorService = administratorService;
            this.clinicalAreaService = clinicalAreaService;
            
        }

        public ActionResult ViewProfile()
        {

            var userid = loginService.GetAccountId();
            var user = loginService.GetAccount(userid);

            switch (user.Role)
            {
                case "Administrator":
                    // admin
                    return (ActionResult)AdminView(userid);

                case "Practitioner":
                    // practitioner
                    var practitionerProfile = PractionerView(user);
                    return View("ViewPrac", practitionerProfile);
                default:
                    // unrecognised method; return to the blank form
                    return RedirectToAction("Index");

            }

        }
        //returns the admin view
        public IActionResult AdminView(Guid id)
        {
            Administrator admin = administratorService.GetOneAdmin(id);
            var clinicalArea = clinicalAreaService.GetClinicalArea(admin.ClinicalAreaID);
            admin.ClinicalArea = clinicalArea.Name;
            return View("AdminProfile",admin);

        }


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




    }
}
