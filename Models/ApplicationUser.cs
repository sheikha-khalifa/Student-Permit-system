using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace student_permit_system.PL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(8)]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
    }
}
