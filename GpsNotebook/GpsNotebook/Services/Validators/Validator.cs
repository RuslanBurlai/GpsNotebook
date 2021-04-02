using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsNotebook.Services.Validators
{
    public static class Validator
    {
        public static void useRegex(String input)
        {
            Regex regex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

            Console.WriteLine(regex.IsMatch(input));
        }
    }
}
