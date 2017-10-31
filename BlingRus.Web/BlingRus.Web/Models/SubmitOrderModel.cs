using System.ComponentModel.DataAnnotations;
using BlingRus.Domain;

namespace BlingRus.Web.Models
{
    public class SubmitOrderModel
    {
        [Required]
        [Display(Name = "Ditt namn")]
        public string CustomerName { get; set; }

        [Display(Name = "Leveransadress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Epostadress")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Telefonnummer")]
        public string CustomerPhone { get; set; }

        public Order Order { get; set; }
    }
}
