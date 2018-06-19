using System.Collections.Generic;
using BlingRus.Domain;
using BlingRus.Domain.Shopping;

namespace BlingRus.Web.Models
{
    public class CatalogModel
    {
        public IEnumerable<Jewelry> Catalog { get; set; }
        public int CartId { get; set; }
    }
}
