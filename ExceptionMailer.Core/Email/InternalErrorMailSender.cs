using System.Configuration;
using System.Net.Mail;
using Castle.Core.Smtp;
using ExceptionMailer.Infrastructure;

namespace ExceptionMailer.Core.Email
{
    public class InternalErrorMailSender : IInternalErrorMailSender
    {
        private readonly IEmailSender _emailSender;

        public InternalErrorMailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Send(string subject, string body)
        {
            //only send email if ErrorEmailTo is set
            var addressTo = ConfigurationManager.AppSettings["DefaultEmailTo"];

            if (string.IsNullOrWhiteSpace(addressTo)) return;

            var mail = new MailMessage(ConfigurationManager.AppSettings["SupportAddress"], addressTo)
                           {
                               Subject = subject,
                               Body = body
                           };


            _emailSender.Send(mail);

        }

    }
}