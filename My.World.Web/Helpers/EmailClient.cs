using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace My.World.Web.Helpers
{
    public class EmailClient: IEmailClient
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromAddress;
        private readonly string _fromAddressTitle;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _enableSsl;
        private readonly bool _useDefaultCredentials;

        public EmailClient(IConfiguration config)
        {
            _smtpServer = config.GetValue<string>("Email:SmtpServer");
            _smtpPort = config.GetValue<int>("Email:SmtpPort");
            _fromAddress = config.GetValue<string>("Email:FromAddress");
            _fromAddress = config.GetValue<string>("Email:FromAddress");
            _fromAddressTitle = config.GetValue<string>("FromAddressTitle");
            _username = config.GetValue<string>("Email:SmtpUsername");
            _password = config.GetValue<string>("Email:SmtpPassword");
            _enableSsl = config.GetValue<bool>("Email:EnableSsl");
            _useDefaultCredentials = config.GetValue<bool>("Email:UseDefaultCredentials");
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_fromAddress, _fromAddressTitle),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));

            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.EnableSsl = _enableSsl;
                client.Credentials = new System.Net.NetworkCredential(_username, _password);
                await client.SendMailAsync(message);
            }
        }
    }
}
