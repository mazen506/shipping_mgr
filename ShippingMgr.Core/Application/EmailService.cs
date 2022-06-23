using Microsoft.AspNetCore.Hosting;
using MimeKit;
using ShippingMgr.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application
{
    public class EmailService : IEmailService
    {
        private IHostingEnvironment env;
        private ILocalizeService localizer;
        public EmailService(IHostingEnvironment env,
                            ILocalizeService localizer)
        {
            this.env = env;
            this.localizer = localizer;
        }
        public async Task<bool> SendEmail(string email, string Name, string callbackUrl,string actionName)
        {
            try
            {
                string body = string.Empty;

                var webRoot = env.WebRootPath; //get wwwroot Folder  

                //GetCurrentLocale
                var locale = CultureInfo.CurrentCulture;

                //Get TemplateFile located at wwwroot/Templates/EmailTemplate/Register_EmailTemplate.html  
                var pathToFile = env.WebRootPath
                        + Path.DirectorySeparatorChar.ToString()
                        + "Templates"
                        + Path.DirectorySeparatorChar.ToString()
                        + "EmailTemplates"
                        + Path.DirectorySeparatorChar.ToString()
                        + actionName + "." + locale + ".html";

                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }

                var subject = localizer.GetValue(actionName);

                string messageBody = string.Format(builder.HtmlBody,
                    subject,
                    Name,
                    callbackUrl
                    );

                var message = new MailMessage();
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("mazen506@hotmail.com");  // replace with valid value
                message.Subject = subject;
                message.Body = messageBody;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "mazen506@hotmail.com",  // replace with valid value
                        Password = "Maseen@90"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.office365.com";
                    smtp.Port = 587; // Google smtp port
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
                return true;
            } catch(Exception ex)
            {
                //Log Error (Implement Logger)
                return false;
            }
            
        }
    }
}
