using System;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BlingRus.Domain
{
    public class SendGridMailService : IMailService
    {
        private readonly string _apiKey;
        private bool CanSendMail => !string.IsNullOrEmpty(_apiKey) && _apiKey.StartsWith("SG.");
        public SendGridMailService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public void SendOrderConfirmationMail(string customerEmail, string htmlContent)
        {
            if (!CanSendMail)
                return;
           
            var message = new SendGridMessage();
            message.AddTo(customerEmail);
            message.SetFrom("noreply@blingrus.azurewebsites.net", "Bling-R-Us");
            message.Subject = "Your order from Bling-R-Us";
            message.HtmlContent = htmlContent;

            try
            {
                var client = new SendGridClient(_apiKey);
                var result = client.SendEmailAsync(message).Result;
                if(result.StatusCode != HttpStatusCode.OK)
                    throw new MailDeliveryFailedException($"Unable to send mail. Sendgrid status code: {result.StatusCode}");
            }
            catch (Exception)
            {
                // Orka...
            }
        }
    }
}