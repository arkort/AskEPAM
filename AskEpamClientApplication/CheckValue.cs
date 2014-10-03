using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskEpamClientApplication
{
    public static class CheckValue
    {
        public static bool ContainsOnlyDigitOrLetter(string testString)
        {
            foreach (char c in testString)
            {
                if (!(Char.IsLetter(c) || Char.IsDigit(c)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
