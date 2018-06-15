using System;
using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public class ContactRequestModel
    {
        [Required]
        [Display(Name = "CustomerName")]
        public string CustomerName { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [EmailAddress]
        [Display(Name = "CustomerEmail")]
        public string CustomerEmail { get; set; }

        [Display(Name = "CustomerAddress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "CustomerPhone")]
        public string CustomerPhone { get; set; }

        [Display(Name = "OrderDate")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "ContactStart")]
        public string ContactStart { get; set; }
        public string ContactEnd { get; set; }

        [Required]
        [Display(Name = "RequestType")]
        public ContactRequestType RequestType { get; set; }
    }
}
