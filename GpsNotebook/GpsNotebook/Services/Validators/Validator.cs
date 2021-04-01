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
            Regex regex = new Regex("^[a-zA-Z]+\\d!$");
            Console.WriteLine(regex.IsMatch(input));
        }
    }
}
