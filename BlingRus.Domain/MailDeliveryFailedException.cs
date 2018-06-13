using System;

namespace BlingRus.Domain
{
    public class MailDeliveryFailedException : Exception
    {
        public MailDeliveryFailedException(string message) : base(message)
        {
        }
    }
}