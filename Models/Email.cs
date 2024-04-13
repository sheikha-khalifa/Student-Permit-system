using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace student_permit_system.PL.Models
{
    public class Email
    {


        public int id { get; set; }

        public string To { get; set; }

        public string Body { get; set; }

        public string title { get; set; }
    }
    }
