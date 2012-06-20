namespace ExceptionMailer.Infrastructure
{
    public interface IExceptionEmailSender
    {
        void Send(string subject, string body);

    }
}