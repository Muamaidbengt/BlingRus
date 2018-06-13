namespace BlingRus.Domain
{
    public interface IMailService
    {
        void SendOrderConfirmationMail(string customerEmail, string htmlContent);
    }
}