using System;
using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public class ContactRequestModel
    {
        [Display(Name = "Your name")]
        public string CustomerName { get; set; }

        [Display(Name = "Your massage for us")]
        public string Message { get; set; }

        [EmailAddress]
        [Display(Name = "Your e-mail adress")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Your mailing adress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Your phone no")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Order date (if applicable)")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "What would be the best time interval for us to reach you during the day?")]
        public string ContactStart { get; set; }
        public string ContactEnd { get; set; }

        [Display(Name = "How may we assist you?")]
        public ContactRequestType RequestType { get; set; }
    }
}
