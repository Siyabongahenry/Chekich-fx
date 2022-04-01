using System.Text.RegularExpressions;

namespace Chekich_fx.Extensions
{
    public static class ValidatePhoneNumbers
    {
        public static bool IsValidNumber(this string number)
        {
            string pattern = @"^((+27)|(0))[0-9]{10}$";
            Regex reg = new Regex(pattern);
            if (reg.IsMatch(number))
            {
                return true;
            }
            return false;
        }
    }
}
