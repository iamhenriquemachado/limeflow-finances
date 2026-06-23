using System;
using System.Text.RegularExpressions;

namespace LimeFlow.Application.Common.Utils
{
    public class Functions
    {
        public static bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            string pattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
