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

        [Display(Name = "Your e-mail adress")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Your mailing adress")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Order date (if applicable)")]
        public DateTime? OrderDate { get; set; }

        [Display(Name="When would be the best time during the day for us to reach you?")]
        public int ContactStart { get; set; }
        public int ContactEnd { get; set; }


    }
}
