namespace ExceptionMailer.Core.Email
{
    public interface ISmsSender
    {
        void Send(string subject, string body);

    }
}