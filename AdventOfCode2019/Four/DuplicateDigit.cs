using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Four
{
    /// <summary>
    /// Has at least one duplicate digit that are next to each other
    /// </summary>
    public class DuplicateDigit : IPasswordRule
    {
        public bool IsValid(int password)
        {
            char[] split = password.ToString().ToCharArray();
            List<int> splitInts = split.Select(s => int.Parse($"{s}")).ToList();
            int lastInt = -1;
            bool hasDuplicateDigit = false;

            foreach (int splitInt in splitInts)
            {
                if (splitInt == lastInt)
                    hasDuplicateDigit = true;

                lastInt = splitInt;
            }

            return hasDuplicateDigit;
        }
    }
}