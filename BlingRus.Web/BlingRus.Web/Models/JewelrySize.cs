using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models
{
    public enum JewelrySize
    {
        [Display(Name = nameof(ModelResources.JewelrySizeSmall), ResourceType = typeof(ModelResources))]
        Small = Domain.JewelrySize.Small,

        [Display(Name = nameof(ModelResources.JewelrySizeMedium), ResourceType = typeof(ModelResources))]
        Medium = Domain.JewelrySize.Medium,

        [Display(Name = nameof(ModelResources.JewelrySizeLarge), ResourceType = typeof(ModelResources))]
        Large = Domain.JewelrySize.Large,

        [Display(Name = nameof(ModelResources.JewelrySizeHumongous), ResourceType = typeof(ModelResources))]
        Humongous = Domain.JewelrySize.Humongous
    }
}