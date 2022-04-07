using System.ComponentModel.DataAnnotations;
using System;
namespace PainAssessment.ViewModels
{
    public class UpdateUsernameModel
    {
        public string Name { get; set; } // real name

        [Required(ErrorMessage = "Please enter your desired User Name")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "User Name can't be less that 6 characters")]
        [DataType(DataType.Text)]
        public string NewUserName { get; set; }
        public string Role { get; set; } // role
        public Guid AccountID { get; set; } //guid
        public string PriorPainEducation { get; set; } 
        public string ClinicalArea { get; set; }
        public string PracticeType { get; set; }

    }
}
