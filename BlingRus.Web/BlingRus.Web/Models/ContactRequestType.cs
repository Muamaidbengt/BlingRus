using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public enum ContactRequestType
    {
        [Display(Name = "I have a question about your products")]
        Inquiry,

        [Display(Name = "I have a question about my order/bill")]
        Billing,

        [Display(Name = "I want to register a complaint")]
        Complaint,

        [Display(Name = "Other")]
        Other
    }
}
