namespace ExceptionMailer.Infrastructure
{
    public interface IInternalErrorMailSender
    {
        void Send(string subject, string body);

    }
}