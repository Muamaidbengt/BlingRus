using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public enum ContactRequestType
    {
        [Display(Name = "I have a question about your products")]
        Inquiry,

        [Display(Name = "I have a question about invoicing")]
        Invoicing,

        [Display(Name = "I am not satisfied with my shipment")]
        OrderComplaint,

        [Display(Name = "I am not satisfied with my bill")]
        BillComplaint,

        [Display(Name = "Other")]
        Other
    }
}
