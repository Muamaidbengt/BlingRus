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
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Display(Name = "CustomerAddress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "CustomerPhone")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        [Display(Name = "OrderDate")]
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "ContactStart")]
        [DataType(DataType.Time)]
        public string ContactStart { get; set; }

        [DataType(DataType.Time)]
        public string ContactEnd { get; set; }

        [Required]
        [Display(Name = "RequestType")]
        public ContactRequestType RequestType { get; set; }
    }
}
