using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public enum ContactRequestType
    {
        [Display(Name = nameof(ModelResources.ContactRequestTypeInquiry), ResourceType = typeof(ModelResources))]
        Inquiry,

        [Display(Name = nameof(ModelResources.ContactRequestTypeInvoicing), ResourceType = typeof(ModelResources))]
        Invoicing,

        [Display(Name = nameof(ModelResources.ContactRequestTypeOrderComplaint), ResourceType = typeof(ModelResources))]
        OrderComplaint,

        [Display(Name = nameof(ModelResources.ContactRequestTypeBillComplaint), ResourceType = typeof(ModelResources))]
        BillComplaint,

        [Display(Name = nameof(ModelResources.ContactRequestTypeOther), ResourceType = typeof(ModelResources))]
        Other
    }
}
