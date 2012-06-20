using System.Configuration;
using System.Net.Mail;
using Castle.Core.Smtp;

namespace ExceptionMailer.Core.Email
{
    public class SmsSender : ISmsSender
    {
        private readonly IEmailSender _emailSender;

        public SmsSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Send(string subject, string body)
        {
            //only send email if ErrorEmailTo is set
            var addressTo = ConfigurationManager.AppSettings["SmsMailTo"];

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