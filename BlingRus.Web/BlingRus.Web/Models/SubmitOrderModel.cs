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
        public string CustomerEmail { get; set; }

        [Display(Name = "CustomerPhone")]
        public string CustomerPhone { get; set; }

        [Required]
        [RequiresSafeCreditCardNumber]
        [Display(Name = "CreditCardNumber")]
        public string CreditCardNumber { get; set; }

        [Required]
        [RequiresFutureDate]
        [Display(Name = "CreditCardExpiration")]
        public DateTime? CreditCardExpiration { get; set; }

        public Order Order { get; set; }
    }
}
