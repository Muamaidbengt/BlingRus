using System.ComponentModel.DataAnnotations;
using BlingRus.Domain;

namespace BlingRus.Web.Models
{
    public class SubmitOrderModel
    {
        [Display(Name = "Your name")]
        public string CustomerName { get; set; }

        [Display(Name = "Your mailing adress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Your e-mail adress")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Your phone no")]
        public string CustomerPhone { get; set; }

        public Order Order { get; set; }
    }
}
