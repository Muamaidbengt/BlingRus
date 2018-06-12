using System;
using System.ComponentModel.DataAnnotations;
using BlingRus.Domain;
using BlingRus.Web.Models.Validation;

namespace BlingRus.Web.Models
{
    public class SubmitOrderModel
    {
        [Required]
        [Display(Name = "Ditt namn")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Leveransadress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Epostadress")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Telefonnummer")]
        public string CustomerPhone { get; set; }

        [Required]
        [RequiresValidCreditCardNumber]
        [Display(Name = "Kreditkortsnummer")]
        public string CreditCardNumber { get; set; }

        [Required]
        [RequiresValidExpiryDate]
        [Display(Name = "Giltig till")]
        public DateTime? CreditCardExpiration { get; set; }

        public Order Order { get; set; }
    }
}
