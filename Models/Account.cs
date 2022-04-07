using System;
using System.ComponentModel.DataAnnotations;

namespace PainAssessment.Models
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string AccountStatus { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public bool FirstSignIn { get; set; }

    }
}
