using System.ComponentModel.DataAnnotations;

namespace student_permit_system.PL.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
