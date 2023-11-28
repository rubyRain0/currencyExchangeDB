using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace currencyExchangeDB.Utils
{
    public class UtilityFunctions
    {
        public static string PromptValidInput(string promptMessage, Func<string, bool> isValidInput)
        {
            string input;
            do
            {
                Console.WriteLine(promptMessage);
                input = Console.ReadLine().Trim();
            } while (!isValidInput(input));

            return input;
        }
        public static bool IsValidName(string name)
        {

            Regex regex =
                new Regex(@"[\p{Ll}\p{Lt}]+");

            return name.Length > 0 && regex.IsMatch(name);
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex =
            new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$");

            return !string.IsNullOrEmpty(email) && regex.IsMatch(email);
        }
        public static bool IsValidPassportNumber(string passportNumber)
        {
            Regex regex =
                new Regex(@"^\d{4} \d{6}$|^\d{10}$");

            return !string.IsNullOrEmpty(passportNumber) && regex.IsMatch(passportNumber);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex =
                new Regex(@"^\d-\d{3}-\d{3}-\d{2}-\d{2}$");

            return !string.IsNullOrEmpty(phoneNumber) && regex.IsMatch(phoneNumber);
        }

        public static bool IsValidCurrencyCode(string currencyCode)
        {
            Regex regex =
                new Regex(@"^[a-zA-Z]{3}$");

            return !string.IsNullOrEmpty(currencyCode) && regex.IsMatch(currencyCode);
        }

        public static bool IsValidCurrencyName(string currencyName)
        {
            Regex regex =
                new Regex(@"^[a-zA-Zа-яА-Я ]+$");

            return !string.IsNullOrEmpty(currencyName) && regex.IsMatch(currencyName);
        }
    }
}
