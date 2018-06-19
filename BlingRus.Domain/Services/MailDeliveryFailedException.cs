using System;

namespace BlingRus.Domain.Services
{
    public class MailDeliveryFailedException : Exception
    {
        public MailDeliveryFailedException(string message) : base(message)
        {
        }
    }
}