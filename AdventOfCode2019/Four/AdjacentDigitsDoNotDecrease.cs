using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Four
{
    /// <summary>
    /// Successive digits must never be less than the previous digit
    /// </summary>
    public class AdjacentDigitsDoNotDecrease : IPasswordRule
    {
        public bool IsValid(int password)
        {
            char[] split = password.ToString().ToCharArray();
            List<int> splitInts = split.Select(s => int.Parse($"{s}")).ToList();
            int lastInt = splitInts[0];

            foreach (int splitInt in splitInts)
            {
                if (splitInt < lastInt)
                    return false;

                lastInt = splitInt;
            }

            return true;
        }
    }
}