using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PainAssessment.Models
{
    public class TemplateChecklist
    {
        [Required]
        public string TemplateChecklistId { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }

    }
}
