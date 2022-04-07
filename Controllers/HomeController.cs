using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PainAssessment.Interfaces;
using PainAssessment.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace PainAssessment.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Include services
        private readonly ITemplateChecklistService templateChecklistService;
        private readonly IDefaultQuestionsService defaultQuestionsService;

        public HomeController(ILogger<HomeController> logger, ITemplateChecklistService templateChecklistService, IDefaultQuestionsService defaultQuestionsService)
        {
            _logger = logger;
            this.templateChecklistService = templateChecklistService;
            this.defaultQuestionsService = defaultQuestionsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ErrorPage(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404 || statusCode.Value == 500)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }
            return View();
        }
    }
}
