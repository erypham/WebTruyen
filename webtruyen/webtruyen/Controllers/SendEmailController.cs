using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace webtruyen.Controllers
{
    public class SendEmailController : Controller
    {
        // GET: SendEmail
        public class EmailService
        {
            public bool SendEmail(string receiver, string subject, string message)
            {
                try
                {
                    var senderEmail = new MailAddress("enryu2310@gmail.com", "Add");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "lehuan.2310";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}