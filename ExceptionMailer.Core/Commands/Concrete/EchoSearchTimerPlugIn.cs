using System;
using System.Linq;
using Castle.Core.Smtp;
using EchoWebService.Core.EchoWebServiceSearch;
using EchoWebService.Core.Services;
using ExceptionMailer.Core.Email;
using ExceptionMailer.Infrastructure;

namespace ExceptionMailer.Core.Commands.Concrete
{
    public class EchoSearchTimerPlugIn : TimerPlugInBase
    {
        private readonly IInternalErrorMailSender _defaultEmailSender;
        private readonly IEchoSearchService _echoSearchService;

        public EchoSearchTimerPlugIn(IExceptionEmailSender emailSender, ISmsSender smsSender, IInternalErrorMailSender defaultEmailSender,
                                IEchoSearchService echoSearchService)
            : base(emailSender, smsSender, null)
        {
            _defaultEmailSender = defaultEmailSender;
            _echoSearchService = echoSearchService;
        }

        public override void DoInvoke()
        {
            try
            {
                const string subject = "Echo search failed";
                const string body = "Call to echo SearchSupporterGroup method failed - the echo service appears to be down";

                if (!SearchSuccessful())
                {
                    //search again
                    if (!SearchSuccessful())
                    {
                        SmsSender.Send(subject, body);
                        EmailSender.Send(subject, body);
                    }

                }

            }
            catch (Exception ex)
            {
                _defaultEmailSender.Send("Echo search failed", string.Format("Call to echo SearchSupporterGroup method failed: {0}", ex));
            }

        }

        private bool SearchSuccessful()
        {
            var searchSupporterGroup = _echoSearchService.SearchSupporterGroup("methodist", null, "vic").ToList();

            if (searchSupporterGroup.Count() == 1)
            {
                var group = searchSupporterGroup.First();
                if (group.Message.ApplicationMessage != ApplicationMessage.Processed)
                    return false;
            }
            return true;
        }

    }
}