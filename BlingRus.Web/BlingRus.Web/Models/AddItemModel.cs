using System;

namespace BlingRus.Web.Models
{
    public class AddItemModel
    {
        public Guid ItemId { get; set; }
        public int Amount { get; set; }
        public string Customization { get; set; }
        public JewelrySize Size { get; set; }
    }
}
