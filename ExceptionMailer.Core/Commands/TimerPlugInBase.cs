using System;
using System.Configuration;
using System.Timers;
using Castle.Core.Smtp;
using ExceptionMailer.Core.Email;
using ExceptionMailer.Infrastructure;

namespace ExceptionMailer.Core.Commands
{
    public abstract class TimerPlugInBase : IPlugIn
    {
        protected Timer Timer;

        protected readonly IExceptionEmailSender EmailSender;
        protected readonly ISmsSender SmsSender;
        protected readonly IInternalErrorMailSender DefaultEmailSender;
        protected int IntervalMins = 10;

        public abstract void DoInvoke();

        protected TimerPlugInBase(IExceptionEmailSender emailSender, ISmsSender smsSender, IInternalErrorMailSender defaultEmailSender)
        {
            try
            {
                var typeName = this.GetType().Name;
                IntervalMins = int.Parse(ConfigurationManager.AppSettings[string.Format("{0}-{1}", typeName, "IntervalMins")]);
            }
            catch
            {
            }

            EmailSender = emailSender;
            SmsSender = smsSender;
            DefaultEmailSender = defaultEmailSender;
        }

        public void Execute()
        {
            Timer = new Timer();
            Timer.Elapsed += TimerElapsed;

            Timer.Interval = 1000 * 60 * IntervalMins;
            Timer.Start();

            DoInvoke();
        }

        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                DoInvoke();
            }
            catch (Exception ex)
            {
                DefaultEmailSender.Send(string.Format("Error in {0}", this.GetType().Name), ex.ToString());
            }

        }
    }
}