using System.Configuration;
using System.Text;
using System.Timers;
using ExceptionMailer.Core.Data.Context;
using System.Linq;
using ExceptionMailer.Core.Email;
using ExceptionMailer.Infrastructure;
using FamineDto.Infrastructure;

namespace ExceptionMailer.Core.Commands.Concrete
{
    public class EchoWebProcessorChecker : TimerPlugInBase
    {
        private readonly ITransactionDomainContext _transactionDomainContext;
        private int _takecount = 20;
        private int _smsCount = 0;

        public EchoWebProcessorChecker(IExceptionEmailSender emailSender, ISmsSender smsSender, ITransactionDomainContext transactionDomainContext)
            : base(emailSender, smsSender, null)
        {
            _transactionDomainContext = transactionDomainContext;
            _takecount = int.Parse(ConfigurationManager.AppSettings["CheckerRowsToTake"]);
        }


        public override void DoInvoke()
        {
            var p = _transactionDomainContext.EchoWebServiceRequests.OrderByDescending(n => n.Id).Take(_takecount).ToList();
            var failed = p.Where(echoWebServiceRequest => echoWebServiceRequest.Status == EchoWebServiceStatus.Failed).ToList();

            if (failed.Count() > _takecount - 3)
            {
                if (_smsCount < 3)
                {
                    SmsSender.Send("An error has been raised by the EchoWebProcessor checker",
                                      string.Format("At least {0} errors have occurred out of the last {1}", failed.Count(), _takecount));
                    _smsCount++;
                }

                var sb = new StringBuilder();

                foreach (var echoWebServiceRequest in failed)
                {
                    sb.Append(echoWebServiceRequest.TransactionId + "\n");
                }
                EmailSender.Send("An error has been raised by the EchoWebProcessor checker",
                                      string.Format("At least {0} errors have occurred out of the last {1}\n{2}", failed.Count(), _takecount, sb));
            }
        }

    }
}