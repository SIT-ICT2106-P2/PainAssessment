using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PainAssessment.Areas.ModuleTwo.Data;
using PainAssessment.Areas.ModuleTwo.Models;

namespace PainAssessment.Areas.ModuleTwo.Controllers
{
    [Area("ModuleTwo")]
    public class TimelinesController : Controller
    {
        //        private readonly ILoginService patientService;
        //        private readonly IConsultationService consultationService;
        //        private readonly ISessionService sessionService;

        public async Task<IActionResult> Index(int id)
        {
            return View();
        }

    }
}
