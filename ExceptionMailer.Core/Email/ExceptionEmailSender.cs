using System;
using System.Configuration;
using System.Net.Mail;
using Castle.Core.Smtp;
using ExceptionMailer.Infrastructure;

namespace ExceptionMailer.Core.Email
{
    public class ExceptionEmailSender : IExceptionEmailSender
    {
        private readonly IEmailSender _emailSender;

        public ExceptionEmailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Send(string subject, string body)
        {
            //only send email if ErrorEmailTo is set
            var addressTo = ConfigurationManager.AppSettings["ErrorEmailTo"];

            if (string.IsNullOrWhiteSpace(addressTo)) return;

            string[] addresses = addressTo.Split(';');
            var mail = new MailMessage
            {
                From = new MailAddress(ConfigurationManager.AppSettings["SupportAddress"]),
                Subject = subject,
                Body = body
            };

            foreach (var address in addresses)
            {
                mail.To.Add(address);
            }


            _emailSender.Send(mail);

        }

    }
}