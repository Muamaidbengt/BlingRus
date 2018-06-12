using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlingRus.Web.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiresValidCreditCardNumberAttribute : ValidationAttribute
    {
        private static readonly string[] ValidCreditCardNumbers = 
        {
            // source: https://www.paypalobjects.com/en_AU/vhelp/paypalmanager_help/paypalmanager.htm#credit_card_numbers.htm
            "378282246310005",
            "371449635398431",
            "378734493671000",
            "5610591081018250",
            "30569309025904",
            "38520000023237",
            "6011111111111117",
            "6011000990139424",
            "3530111333300000",
            "3566002020360505",
            "5555555555554444",
            "5105105105105100",
            "4111111111111111",
            "4012888888881881",
            "4222222222222",
            "76009244561",
            "5019717010103742",
            "6331101999990016"
        };

        public override bool IsValid(object value)
        {
            var number = (string) value;
            return ValidCreditCardNumbers.Contains(number);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class RequiresValidExpiryDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var expiryDate = (DateTime?) value;
            return !expiryDate.HasValue || expiryDate.Value >= DateTime.Today;
        }
    }
}