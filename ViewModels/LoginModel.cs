using System.ComponentModel.DataAnnotations;

namespace PainAssessment.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username{ get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password can't be less that 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
