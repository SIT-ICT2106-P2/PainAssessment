using PainAssessment.Interfaces;
using PainAssessment.Models;
using System.Collections.Generic;

namespace PainAssessment.Domain
{
    public class TemplateChecklistService : ITemplateChecklistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TemplateChecklistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateTemplateChecklist(string templateID, string templateName, string description)
        {
            // Implement here
            throw new System.NotImplementedException();
        }

        public void DeleteTemplateChecklist(string templateID)
        {
            // Implement here
            throw new System.NotImplementedException();
        }

        public IEnumerable<TemplateChecklist> GetAllTemplateChecklist()
        {
            // Implement here
            return _unitOfWork.TemplateChecklistRepository.GetAll();
        }

        public TemplateChecklist GetOneTemplateChecklist(string templateID)
        {
            // Implement here
            throw new System.NotImplementedException();
        }

        public void UpdateTemplateChecklist(string templateID)
        {
            // Implement here
            throw new System.NotImplementedException();
        }
    }
}
