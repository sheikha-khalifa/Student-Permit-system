using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using student_permit_system.PL.Models;

namespace student_permit_system.PL.Models
{
    public class Employee : ApplicationUser
    {
        // Remove the ID property

        //[Required]
        //[StringLength(80, MinimumLength = 2)]
        //public string Name { get; set; }

        ////[Required]
        ////[DataType(DataType.PhoneNumber)]
        ////public string PhoneNo { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        //[Required]
        //public string Address { get; set; }

        //[Required]
        //public string UserType { get; set; }

        public ICollection<Requests>? ApprovedRequests { get; set; }
    }
}
