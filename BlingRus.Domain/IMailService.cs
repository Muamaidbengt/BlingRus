namespace BlingRus.Domain
{
    public interface IMailService
    {
        void SendOrderConfirmationMail(Order order);
    }
}