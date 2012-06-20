using System.Net.Mail;
using Castle.Core.Smtp;
using ExceptionMailer.Core.Email;
using ExceptionMailer.Infrastructure;

namespace ExceptionMailer.Core.Commands.Concrete
{
    public class StartUpPlugIn : IPlugIn
    {
        private readonly IInternalErrorMailSender _defaultEmailSender;

        public StartUpPlugIn(IInternalErrorMailSender defaultEmailSender)
        {
            _defaultEmailSender = defaultEmailSender;
        }

        public void Execute()
        {
            _defaultEmailSender.Send("mailer startup", "mailer startup plugin executed");
        }
    }
}