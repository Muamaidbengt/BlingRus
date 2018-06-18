using System;
using System.ComponentModel.DataAnnotations;

namespace BlingRus.Web.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiresFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var expiryDate = (DateTime?) value;
            return !expiryDate.HasValue || expiryDate.Value >= DateTime.Today;
        }
    }
}