using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace currencyExchangeDB.Utils
{
    public class UtilityFunctions
    {
        public static T PromptValidInput<T>(string promptMessage, Func<T, bool> isValidInput)
        {
            T input;
            do
            {
                Console.WriteLine(promptMessage);
                string userInput = Console.ReadLine().Trim();

                try
                {
                    input = (T)Convert.ChangeType(userInput, typeof(T));
                }
                catch
                {
                    input = default(T);
                }
            } while (!isValidInput(input));

            return input;
        }
        public static bool IsValidName(string name)
        {

            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex("^[a-zA-Zа-яА-Я]+$");

            return name.Length > 0 && regex.IsMatch(name);
        }

        public static bool IsValidEmail(string email)
        {
            System.Text.RegularExpressions.Regex regex =
            new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$");

            return !string.IsNullOrEmpty(email) && regex.IsMatch(email);
        }
        public static bool IsValidPassportNumber(string passportNumber)
        {
            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(@"^\d{4} \d{6}$|^\d{10}$");

            return !string.IsNullOrEmpty(passportNumber) && regex.IsMatch(passportNumber);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(@"^\d-\d{3}-\d{3}-\d{2}-\d{2}$");

            return !string.IsNullOrEmpty(phoneNumber) && regex.IsMatch(phoneNumber);
        }

        public static bool IsValidCurrencyAmount(double number)
        {
            if (number > 0)
                return true;
            return false;
        }

        public static bool IsValidCurrencyCode(string currencyCode)
        {
            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex("^[a-zA-Z]{3}$");

            return !string.IsNullOrEmpty(currencyCode) && regex.IsMatch(currencyCode);
        }

        public static bool IsValidCurrencyName(string currencyName)
        {
            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex("^[a-zA-Zа-яА-Я ]+$");

            return !string.IsNullOrEmpty(currencyName) && regex.IsMatch(currencyName);
        }
    }
}
