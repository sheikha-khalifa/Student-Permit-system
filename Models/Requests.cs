using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using student_permit_system.PL.Models;

namespace student_permit_system.PL.Models
{
    public enum Status
    {
        Pending = 1,
        Approve,
        Decline,
        Done
    }
    public class Requests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public Status Status { get; set; } = Status.Pending;

        [Required]
        public string CarNumber { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [NotMapped] // This property won't be mapped to the database
        public IFormFile Image { get; set; }

        // Relationship with Student
        [ForeignKey("Student")]
        public string Id { get; set; } // Use Id from ApplicationUser as foreign key

        public ApplicationUser Student { get; set; }

        // Relationship with Employee who approves the request
        [ForeignKey("Employee")]
        public string? EmpID { get; set; }  // Change int? to string?

        // Change the data type of EmpID to string to match the Id property in ApplicationUser
        public Employee Employee { get; set; }

        public Requests()
        {
            Status = Status.Pending;
        }
    }
}

