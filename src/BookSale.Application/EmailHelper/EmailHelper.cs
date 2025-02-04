using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Configuration;
namespace BookSale.Application.EmailHelper
{
    public class EmailHelper : IEmailHelper
    {
        EmailConfig _emailConfig;

        public EmailHelper(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmail(CancellationToken cancellationToken, EmailRequest emailRequest)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_emailConfig.Provider, _emailConfig.Port);
                smtpClient.Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(_emailConfig.DefaultSender);
                mailMessage.To.Add(emailRequest.To);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = emailRequest.Subject;
                mailMessage.Body = emailRequest.Content;

                if (emailRequest.AttachmentFilePaths.Length > 0)
                {
                    foreach (var path in emailRequest.AttachmentFilePaths)
                    {
                        Attachment attachment = new Attachment(path);
                        mailMessage.Attachments.Add(attachment);
                    }
                }
                await smtpClient.SendMailAsync(mailMessage, cancellationToken);
                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                throw ;
            }

        }
    }
}
