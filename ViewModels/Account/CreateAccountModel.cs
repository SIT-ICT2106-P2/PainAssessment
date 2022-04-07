using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PainAssessment.Areas.Admin.Models;
using PainAssessment.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PainAssessment.ViewModels
{

    public class CreateAccountModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password can't be less that 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; }

        //[DisplayName("Prior Pain Education")]
        //public string PriorPainEducation { get; set; }
        [Required(ErrorMessage = "Clinical Area is required.")]
        public int ClinicalAreaID { get; set; }

        public ClinicalArea ClinicalArea { get; set; }
        [Required(ErrorMessage = "Practice Type is required.")]
        public int PracticeTypeID { get; set; }

        public PracticeType PracticeType { get; set; }

        //[Required(ErrorMessage = "Prior pain education is required.")]
        //public string[] SelectedPainEducation { get; set; }
    }
}
