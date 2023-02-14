using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;

namespace Learning.Pages.Users
{
    public class EmailModel : PageModel
    {
                public string errorMessage;
        public string emailclient;
        

        public void OnPost()
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;
            string emailFromAddress = Request.Form["adminemail"]; //Sender Email Address  
            string password = Request.Form["password"]; //Sender Password  
            string emailToAddress = Request.Query["email"]; //Receiver Email Address  
            string subject =Request.Form["subject"];
            string body = Request.Form["emailbody"];
            
            try
            {
                
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress);
                    mail.To.Add(emailToAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }
        public void OnGet()
        {
            string emailToAddress = Request.Query["email"]; //Receiver Email Address 
            emailclient = emailToAddress;
            Console.WriteLine(emailclient);
        }
    }
}
