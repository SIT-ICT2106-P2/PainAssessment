using PainAssessment.Models;
using System.Collections.Generic;

namespace PainAssessment.Interfaces
{
    public interface ITemplateChecklistService
    {
        // Write all the functions controller can call e.g. CRUD
        IEnumerable<TemplateChecklist> GetAllTemplateChecklist();
        TemplateChecklist GetOneTemplateChecklist(string templateID);
        void CreateTemplateChecklist(string templateID, string templateName, string description);
        void UpdateTemplateChecklist(string templateID);
        void DeleteTemplateChecklist(string templateID);
    }
}
