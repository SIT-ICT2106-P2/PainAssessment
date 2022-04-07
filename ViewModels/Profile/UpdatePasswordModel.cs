using System.ComponentModel.DataAnnotations;
using System;

namespace PainAssessment.ViewModels
{
    public class UpdatePasswordModel
    {
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your desired password")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password can't be less that 6 characters")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter the password again")]
        [Compare("NewPassword", ErrorMessage = "Password do not match")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password can't be less that 6 characters")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
