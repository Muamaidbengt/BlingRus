using System;
using System.Net;
using BlingRus.Domain.Shopping;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BlingRus.Domain.Services
{
    public class SendGridMailService : IMailService
    {
        private readonly string _apiKey;
        private readonly IViewRenderService _viewRenderService;
        private bool CanSendMail => !string.IsNullOrEmpty(_apiKey) && _apiKey.StartsWith("SG.");
        public SendGridMailService(string apiKey, IViewRenderService viewRenderService)
        {
            _apiKey = apiKey;
            _viewRenderService = viewRenderService;
        }

        public void SendOrderConfirmationMail(Order order)
        {
            if (!CanSendMail)
                return;

            if (order == null || string.IsNullOrEmpty(order.ConfirmationEmail))
                return;

            var mailContents =
                _viewRenderService.RenderToString(@"~/Views/Shared/Components/ConfirmationMail/Default.cshtml", order);

            var message = new SendGridMessage();
            message.AddTo(order.ConfirmationEmail);
            message.SetFrom("noreply@blingrus.azurewebsites.net", "Bling-R-Us");
            message.Subject = "Your order from Bling-R-Us";
            message.HtmlContent = mailContents;

            try
            {
                var client = new SendGridClient(_apiKey);
                var result = client.SendEmailAsync(message).Result;
                if(result.StatusCode < HttpStatusCode.OK || result.StatusCode > HttpStatusCode.Accepted)
                    throw new MailDeliveryFailedException($"Unable to send mail. Sendgrid status code: {result.StatusCode}");
            }
            catch (Exception)
            {
                // Shouldn't happen™
            }
        }
    }
}