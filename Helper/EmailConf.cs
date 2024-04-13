using student_permit_system.PL.Models;
using System.Net.Mail;
using System.Net;

namespace student_permit_system.PL.Helper
{
    public class EmailConf
    {
        public static void ResetPasswordEmail(Email em)
        {
            // using Google Mailing Services
            var client = new SmtpClient("smtp.gmail.com", 465);
            client.EnableSsl = true;
            //								account Credentials  ||  Passwrod Generated Once 
            client.Credentials = new NetworkCredential("@gmail.com", "----------");
            client.Send("SenderEmail", em.To, em.title, em.Body);

        }
    }
}
