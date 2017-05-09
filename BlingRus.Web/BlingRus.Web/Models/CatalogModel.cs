using System;
using System.Collections.Generic;
using BlingRus.Domain;

namespace BlingRus.Web.Models
{
    public class CatalogModel
    {
        public IEnumerable<Jewelry> Catalog { get; set; }
        public Guid CartId { get; set; }
    }
}
