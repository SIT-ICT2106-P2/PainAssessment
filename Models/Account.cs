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

        public Account(Guid AccountId, string Username, string Password, string AccountStatus, string Role, DateTime CreatedAt, bool FirstSignIn)
        {
            this.AccountId = AccountId;
            this.Username = Username;
            this.Password = Password;
            this.AccountStatus = AccountStatus;
            this.Role = Role;
            this.CreatedAt = CreatedAt;
            this.FirstSignIn = FirstSignIn;
        }


        public Account()
        {
        }

    }
}
