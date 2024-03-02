using System.Collections.Generic;

namespace MediaManager.Globals
{
    public class StringComparer : IComparer<string>
    {
        private static bool IsStringNumeric(string value) => int.TryParse(value, out _);

        public int Compare(string str1, string str2)
        {
            var str1_start = str1.Split(' ')[0];
            var str2_start = str2.Split(' ')[0];

            if (IsStringNumeric(str1_start) && IsStringNumeric(str2_start))
            {
                var num1 = int.Parse(str1_start);
                var num2 = int.Parse(str2_start);

                return num1 - num2;
            }

            return string.Compare(str1, str2);
        }
    }
}
