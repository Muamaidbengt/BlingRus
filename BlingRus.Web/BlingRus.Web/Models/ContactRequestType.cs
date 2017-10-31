using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public enum ContactRequestType
    {
        [Display(Name = "Jag har en fråga om en av era produkter")]
        Inquiry,

        [Display(Name = "Jag har en fråga om betalning")]
        Invoicing,

        [Display(Name = "Jag är missnöjd med mina varor")]
        OrderComplaint,

        [Display(Name = "Jag är missnöjd med min faktura")]
        BillComplaint,

        [Display(Name = "Övrigt")]
        Other
    }
}
