using System;

namespace PainAssessment.ViewModels.Profile
{
    public class PractionerModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public Guid AccountID { get; set; }
        public string PriorPainEducation { get; set; }
        public string ClinicalArea { get; set; }
        public string PracticeType { get; set; }

    }
}
