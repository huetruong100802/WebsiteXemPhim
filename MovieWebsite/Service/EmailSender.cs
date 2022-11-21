using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MovieWebsite.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender( ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            
            var msg = new MailMessage()
            {
                From= new MailAddress(address:"huetruong100802@gmail.com"),
                Subject=subject,
                Body = htmlMessage,
                IsBodyHtml=true,
            };
            msg.To.Add(new MailAddress(email));
            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential
                {
                    UserName = "huetruong100802@gmail.com",
                    Password = "ruxlnscrgpdoztaf",//ruxlnscrgpdoztaf
                },
                Port = 587,//587 tls
                EnableSsl = true,
            };
            msg.BodyEncoding = Encoding.Default;
            await smtp.SendMailAsync(msg);
        }
    }
}
