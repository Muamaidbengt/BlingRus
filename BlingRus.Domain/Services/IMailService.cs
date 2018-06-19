using BlingRus.Domain.Shopping;

namespace BlingRus.Domain.Services
{
    public interface IMailService
    {
        void SendOrderConfirmationMail(Order order);
    }
}