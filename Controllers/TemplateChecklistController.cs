using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PainAssessment.Interfaces;
using Microsoft.Extensions.Logging;



namespace PainAssessment.Controllers
{
    public class TemplateChecklistController : Controller
    {

        //include services and logger
      
        private readonly ILogger<TemplateChecklistController> _logger;        
        private readonly IDefaultQuestionsService defaultQuestionsService;
        private ITemplateChecklistAdapter TChecklistAdapter;


        public TemplateChecklistController(ILogger<TemplateChecklistController> logger, ITemplateChecklistService templateChecklistService, IDefaultQuestionsService defaultQuestionsService, ITemplateChecklistAdapter tchecklistadapter)
        {
            _logger = logger;
            this.defaultQuestionsService = defaultQuestionsService;
            this.TChecklistAdapter = tchecklistadapter;
        }

        //get all template checklist by admin and display
        public IActionResult ViewTemplateChecklist()
        {
            var checklists = TChecklistAdapter.GetAllAdminChecklists();
            return View(checklists);
        }


        //navigate to create template checklist and initialise checklist object
        [HttpGet]
        public IActionResult CreateTemplateChecklist(int user)
        {
            var checklists = TChecklistAdapter.InitialiseChecklist();
            return View(checklists);
        }
        //create template checklist
        [HttpPost]
        public IActionResult CreateTemplateChecklist(Areas.ModuleTwo.Models.Checklist checklist)
        {
            TChecklistAdapter.InsertNewChecklist(checklist);
            return RedirectToAction(nameof(ViewTemplateChecklist));
        }

        // delete template checklist
        [HttpGet]
        public IActionResult DeleteTemplateChecklist(int id)
        {
            var checklist = TChecklistAdapter.GetChecklistByChecklistID(id);
            return View(checklist);
        }
        // confirm delete template checklist
        [HttpPost]
        public IActionResult DeleteTemplateChecklist(Areas.ModuleTwo.Models.Checklist checklist)
        {
            TChecklistAdapter.DeleteChecklist(checklist.ChecklistId);
            return RedirectToAction(nameof(ViewTemplateChecklist));
        }
        // navigate to edit/update template checklist with the checklist id
        [HttpGet]
        public IActionResult EditTemplateChecklist(int id)
        {
            var checklist = TChecklistAdapter.GetChecklistByChecklistID(id);
            return View(checklist);
        }
        // edit/update template checklist with the checklist
        [HttpPost]
        public IActionResult EditTemplateChecklist(Areas.ModuleTwo.Models.Checklist checklist)
        {

            TChecklistAdapter.EditTemplate(checklist.ChecklistId, checklist.ChecklistName, checklist.ChecklistDescription, checklist.Active);
            return RedirectToAction(nameof(ViewTemplateChecklist));
        }

        [HttpPost]
        public ActionResult Update(int checklistID)
        {
            TChecklistAdapter.UpdateActive(checklistID);
            return RedirectToAction(nameof(ViewTemplateChecklist));
        }

        [HttpGet]
        public IActionResult ManageTemplateChecklist(int id)
        {

            var checklist = TChecklistAdapter.GetChecklistByChecklistID(id);
            return View(checklist);
        }


        [HttpPost]
        public IActionResult AddQuestion(int checklistID, int domain, string new_determinant, string new_sub_domain, int new_max_weightage)
        {

            //defaultQuestionsService.CreateDefaultQuestion(1, QString, "", int.Parse(PainSection), double.Parse(weightage));
            this.TChecklistAdapter.AddQuestion(checklistID, new_sub_domain, new_determinant, domain, new_max_weightage);
            Console.WriteLine("Inside addQuestion");
            //RedirectToAction()
            var newChecklistID = this.TChecklistAdapter.GetRecentlyModifiedChecklist();
            return RedirectToAction("ManageTemplateChecklist", new { id  = newChecklistID});
        }

        
        [HttpGet]
        public IActionResult UpdateQuestion(int checklistId, int domain, int rowId, string determinant, string sub_domain, int max_value)
        {
            this.TChecklistAdapter.UpdateQuestion(checklistId, sub_domain, determinant, domain, max_value, rowId);

            var newChecklistID = this.TChecklistAdapter.GetRecentlyModifiedChecklist();

            return RedirectToAction("ManageTemplateChecklist", new { id = newChecklistID });
        }

        [HttpGet]
        public IActionResult DeleteQuestion(int checklistId, int domain, int rowId, string determinant, string sub_domain, int max_value)
        {
            this.TChecklistAdapter.DeleteQuestion(checklistId, sub_domain, determinant, domain, max_value, rowId);

            var newChecklistID = this.TChecklistAdapter.GetRecentlyModifiedChecklist();

            return RedirectToAction("ManageTemplateChecklist", new { id = newChecklistID });
        }

    }
}
