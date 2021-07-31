using System;
using System.Text.RegularExpressions;

namespace GpsNotebook.Validators
{
    //move this folder to core
    public static class Validator
    {
        public static bool EmailValidator(String input)
        {
            Regex regex = new Regex("^[a-zA-Z]|\\d+@[a-z]+\\.[a-z]$");

            return regex.IsMatch(input);
        }

        public static bool PasswordValidator(String input)
        {
            Regex regex = new Regex(@"^([\d]|[\w]|[\!\+\*\?\^\.\[\]\{\}\(\)\|]){8,}$");

            return regex.IsMatch(input);
        }

        public static bool ConfirmPassowrdValidator(string password, string confirmPassword)
        {
            return password.Equals(confirmPassword);
        }

        public static bool NameValidator(String input)
        {
            Regex regex = new Regex(@"^[\w][\w]|[\d]{1,16}$");

            return regex.IsMatch(input);
        }


        public static bool AllFieldsIsNullOrEmpty(params string[] entryInputs)
        {
            var countFilledEntrys = 0;

            foreach (var item in entryInputs)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    countFilledEntrys++;
                }
            }

            return countFilledEntrys == entryInputs.Length;
        }
    }
}
