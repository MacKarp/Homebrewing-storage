namespace Backend.Email
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
    }
}