using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PainAssessment.Areas.Admin.Models;
//using PainAssessment.Areas.ModuleTwo.Data;
//using PainAssessment.Areas.ModuleTwo.Models;
using PainAssessment.Areas.Admin.Services;
using System.Data;
using PainAssessment.Interfaces;

namespace PainAssessment.Areas.ModuleTwo.Controllers
{
    [Area("ModuleTwo")]
    public class PractPatientController : Controller
    {
        private readonly IPatientService patientService;
        private readonly IPractitionerService practitionerService;
        private readonly ILoginService loginService;

        public PractPatientController(IPatientService patientService, IPractitionerService practitionerService, ILoginService loginService)
        {
            this.patientService = patientService;
            this.practitionerService = practitionerService;
            this.loginService = loginService;
        }

        // GET: ModuleTwo/PractPatient/
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            Practitioner practitioner = practitionerService.GetPractitioner(Guid.Parse("a95d92f1-7845-4ac7-7cec-08da118e2549"));

            //Code to get the logged in service
            //var loginID = loginService.GetAccountId;


            //Patient patient = patientService.GetAllPatients;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewData["CurrentFilter"] = searchString;

            var patients = from s in practitioner.Patients select s;
            //var patients = from s in patientService.GetAllPatients() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                //patients = patients.Where(s => s.Name.Contains(searchString));
                patients = patients.Where(s => s.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            switch (sortOrder)
            {
                case "Name":
                    patients = patients.OrderByDescending(s => s.Name);
                    break;
            }

            return View(patients.ToList());
        }

        // GET: ModuleTwo/PractPatient/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient patient = patientService.GetPatient((Guid)id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: ModuleTwo/PractPatient/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient patient = patientService.GetPatient((Guid)id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: ModuleTwo/PractPatient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    patientService.UpdatePatient(patient);
                    patientService.SavePatient();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (patientService.GetPatient(patient.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: ModuleTwo/PractPatient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModuleTwo/PractPatient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)            // WILL NEED TO PASS IN PRACTITIONER INFO HERE
        {
            //Code to get the logged in service
            //var loginID = loginService.GetAccountId;

            if (ModelState.IsValid)
            {
                patientService.CreatePatient(patient);
                patientService.SavePatient();
                Practitioner practitioner = practitionerService.GetPractitioner(Guid.Parse("a95d92f1-7845-4ac7-7cec-08da118e2549"));
                practitioner.AddPatientRelation(patient);
                practitionerService.SavePractitioner();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: ModuleTwo/PractPatient/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient patient = patientService.GetPatient((Guid)id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: ModuleTwo/PractPatient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            patientService.DeletePatient(id);
            patientService.SavePatient();
            return RedirectToAction(nameof(Index));
        }
    }
}
