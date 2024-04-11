using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneNumberDetaction.Model;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace PhoneNumberDetaction.Pages
{
    public class Index1Model : PageModel
    {

        
        [BindProperty]
        public string InputText { get; set; }  //to hold the input text

        public List<PhoneNumber> PhoneNumbers { get; set; } //List to store detected phone numbers with format
        public void OnGet() //initialize state for the page
        {
            PhoneNumbers = new List<PhoneNumber>();  // Initialize the list of phone numbers
        }
        public void OnPost() //handle form submissions
        {
            PhoneNumbers = new List<PhoneNumber>();

            if (int.TryParse(InputText, out _)) //Check if the input is integer
            {

            }
            else if (double.TryParse(InputText, out _)) //Check if the input is double
            {

            }
            else //Check if the input is strings
            {
                
                var words = InputText.Split(new[] { ' ', ',', '.', '(', ')', '+', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries); // Split the input text with is value

                //Create a dictionary to word to didgit
                var numberDictionary = new Dictionary<string, string> { { "zero", "0" }, { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" }, { "five", "5" }, { "six", "6" }, { "seven", "7" }, { "eight", "8" }, { "nine", "9" }, { "शूआ", "0" }, { "एक", "1" }, { "दो", "2" }, { "तीन", "3" }, { "चार", "4" }, { "पांच", "5" }, { "छह", "6" }, { "सात", "7" }, { "आठ", "8" }, { "नौ", "9" } };

                // Clear the existing input text
                InputText = "";

                //Convert word in the input text into a digit
                foreach (var word in words)
                {
                    
                    if (numberDictionary.TryGetValue(word.ToLower(), out string digit))
                    {
                        InputText += digit;
                    }
                }

            }

 

            PhoneNumberDetector pnd = new PhoneNumberDetector(); //Create the instance of PhoneNumberDetector Class 
            PhoneNumbers = pnd.DetectPhoneNumbers(InputText); //Call the PhoneNumberDetector Class to detect phone numbers
        }
    }

}
