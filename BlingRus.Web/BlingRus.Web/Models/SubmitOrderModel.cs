using System;
using System.ComponentModel.DataAnnotations;
using BlingRus.Domain;
using BlingRus.Web.Models.Validation;

namespace BlingRus.Web.Models
{
    public class SubmitOrderModel
    {
        [Required]
        [Display(Name = "CustomerName")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "ShippingAddress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "CustomerEmail")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Display(Name = "CustomerPhone")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        [Required]
        [RequiresSafeCreditCardNumber]
        [DataType(DataType.CreditCard)]
        [Display(Name = "CreditCardNumber")]
        public string CreditCardNumber { get; set; }

        [Required]
        [RequiresFutureDate]
        [DataType(DataType.Date)]
        [Display(Name = "CreditCardExpiration")]
        public DateTime? CreditCardExpiration { get; set; }

        public Order Order { get; set; }
    }
}
