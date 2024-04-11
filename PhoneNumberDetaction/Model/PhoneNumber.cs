using Microsoft.AspNetCore.Mvc;
using PhoneNumberDetaction.Pages;

namespace PhoneNumberDetaction.Model
{
    public class PhoneNumber
    {
        public string Number { get; set; }
        public PhoneFormat Format { get; set; }

        
    }

    public enum PhoneFormat
    {
        Normal,
        TenDigit,
        WithCountryCode,
        WithParenthesesforareacode,
        WithSpacesOrDashes,
        EnglishNumber,
        HindiNumber,
        EnglishAndHindi
        
    }
}
