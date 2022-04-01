using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Chekich_fx.Services
{
    public class EmailSender:IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            options = optionsAccessor.Value;
        }
        public AuthMessageSenderOptions options { get; }
        public Task SendEmailAsync(string email,string subject,string message)
        {
            return Execute(options.SendGridKey,subject,message,email);
        }
        public Task Execute(string apiKey,string subject,string message,string email)
        {
           
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("tumelo.chekich.fx@gmail.com","Chekich-FX"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }
    }
}
