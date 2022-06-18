using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApplication
{
    public class PasswordCheck
    {
        public static bool HasUpper(string password)
        {
            foreach (char c in password)
            {
                if (char.IsLetter(c) && char.IsUpper(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasDigit(string password)
        {
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasSpecialCharacter(string password)
        {
            foreach (char c in password)
            {
                if (c.Equals('!') ||
                    c.Equals('@') ||
                    c.Equals('#') ||
                    c.Equals('$') ||
                    c.Equals('%') ||
                    c.Equals('^'))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsLong(string password)
        {
            int count = 0;
            foreach (char c in password)
            {
                count++;
            }

            return count >= 6;
        }

        public static bool IsStrong(string password)
        {
            return IsLong(password) &&
                HasUpper(password) &&
                HasDigit(password) &&
                HasSpecialCharacter(password);
        }
    }
}
