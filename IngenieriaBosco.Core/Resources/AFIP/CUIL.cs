using System;

namespace IngenieriaBosco.Core.Resources.AFIP
{
    public static class CUIL
    {
        public static bool IsValid(string cuil)
        {
            string x_cuil = cuil[..^1];
            string valid_digit = cuil[^1..];
            x_cuil = StrReverse(x_cuil);
            int SUM_MOD11 = 0;
            for (int i = 0; i < x_cuil.Length; i++)
                SUM_MOD11 += Convert.ToInt32(x_cuil[i] - '0') * (i % 6 + 2);
            SUM_MOD11 %= 11;
            return 11 - Convert.ToInt32(valid_digit[0] - '0') == SUM_MOD11;
        }

        private static string StrReverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
