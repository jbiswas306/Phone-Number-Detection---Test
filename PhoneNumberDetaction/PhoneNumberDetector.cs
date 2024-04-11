using PhoneNumberDetaction.Pages;
using System.Text.RegularExpressions;
using PhoneNumberDetaction.Model;

namespace PhoneNumberDetaction
{
    public class PhoneNumberDetector
    {
        public List<PhoneNumber> DetectPhoneNumbers(string inputText)
        {
            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();


            // Define regular expressions
            var Normal = @"\b\d{3}[-.]?\d{3}[-.]?\d{4}\b";  // Regular expression for normal phone number format
            var countryCode = @"\+\d{1,3}[-.]?\d{3}[-.]?\d{3}[-.]?\d{4}\b"; // Regular expression for phone numbers with country code
            var Bracket = @"\(\d{2,3}\)\d{10}\b"; // Regular expression for phone numbers with braket for area code
            var SoD = @"\d{3}[-.\s]\d{3}[-.\s]?\d{4}\b"; // Regular expression for phone numbers with spaces or dashes
            var English = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" }; // English
            var hindi = new[] { "शूआ", "एक", "दो", "तीन", "चार", "पांच", "छह", "सात", "आठ", "नौ" }; //Hindi
            var brackten = $@"(?:{Normal}|{Bracket})"; // numbers with or without parentheses
            var eng = @"\b(" + string.Join("|", English) + @")\b"; // English
            var hind = @"\b(" + string.Join("|", hindi) + @")\b"; // Hindi

            // Construct regular expression pattern using the defined expressions
            var regex = new Regex($"{brackten}|{Normal}|{countryCode}|{Bracket}|{SoD}|{eng}|{hind}", RegexOptions.IgnoreCase);

            // find matches in the input text
            var matches = regex.Matches(inputText);

            
            foreach (Match match in matches)
            {
                // Add detected phone number to the list of PhoneNumber objects, along with its format
                phoneNumbers.Add(new PhoneNumber { Number = match.Value, Format = GetPhoneFormat(match.Value) });
            }

            // Return the list of detected phone numbers
            return phoneNumbers;
        }

        private PhoneFormat GetPhoneFormat(string phoneNumber) // Helper method to determine the format
        {
            // Check if the phone number contains a country code
            if (phoneNumber.Contains("+"))
            {
                return PhoneFormat.WithCountryCode;
            }
            // Check if the phone number contains braket for the area code
            else if (phoneNumber.Contains("(") && phoneNumber.Contains(")"))
            {
                return PhoneFormat.WithParenthesesforareacode;
            }
            // Check if the phone number contains space dashes or dots
            else if (phoneNumber.Contains("-") || phoneNumber.Contains(".") || phoneNumber.Contains(" "))
            {
                return PhoneFormat.WithSpacesOrDashes;
            }
            // Check if the length of the phone number is 10 digits
            else if (phoneNumber.Length == 10)
            {
                return PhoneFormat.TenDigit;
            }
            // If none of the above conditions match, then the phone number as normal digit or word
            else
            {
                return PhoneFormat.Normal;
            }
        }
    }
}
