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
    public class Feedback
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackID { get; set; }
        public int Rating { get; set; }
        
        public string Feadback { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime dateTime{ get; set; }
        //relationship
        [ForeignKey("RequestID")]
        public int RequestID { get; set; }

        [ForeignKey("StudentID")]
        public int StudentID { get; set; }

    //relationships
//        [ForeignKey("RequestID")]
//public int RequestID { get; set; }

//[ForeignKey("StudentID")]
//public int StudentID { get; set; }

    }
}
