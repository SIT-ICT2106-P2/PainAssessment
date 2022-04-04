﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PainAssessment.Areas.Admin.Models.ViewModels.Profile;
using PainAssessment.Models;
using System.Diagnostics;
using System.Text.Json;

namespace PainAssessment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ViewAdmin(string data)
        {
            AdministratorModel adminViewModel = JsonSerializer.Deserialize<AdministratorModel>(data)!;
            return View(adminViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
