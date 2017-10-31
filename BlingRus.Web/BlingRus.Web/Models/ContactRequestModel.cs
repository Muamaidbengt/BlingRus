using System;
using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public class ContactRequestModel
    {
        [Required]
        [Display(Name = "Ditt namn")]
        public string CustomerName { get; set; }

        [Display(Name = "Ditt medelande")]
        public string Message { get; set; }

        [EmailAddress]
        [Display(Name = "Din epostadress")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Din postadress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Telefonnummer")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Beställningsdatum (om applicerbart)")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "När på dagen föredrar du att vi kontaktar dig?")]
        public string ContactStart { get; set; }
        public string ContactEnd { get; set; }

        [Required]
        [Display(Name = "Hur kan vi stå till tjänst?")]
        public ContactRequestType RequestType { get; set; }
    }
}
