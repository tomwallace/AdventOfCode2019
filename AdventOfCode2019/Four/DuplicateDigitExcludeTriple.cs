using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Four
{
    /// <summary>
    /// Has at least one duplicate digit that are next to each other.  Exclude any groups of digits MORE
    /// than two
    /// </summary>
    public class DuplicateDigitExcludeTriple : IPasswordRule
    {
        public bool IsValid(int password)
        {
            char[] split = password.ToString().ToCharArray();
            List<int> splitInts = split.Select(s => int.Parse($"{s}")).ToList();
            bool hasDuplicateDigit = false;
            int lengthDuplicateSection = 1;

            for (int i = 0; i < splitInts.Count; i++)
            {
                int currentInt = splitInts[i];
                int nextInt = (i + 1) == splitInts.Count ? -1 : splitInts[i + 1];

                if (currentInt == nextInt)
                {
                    lengthDuplicateSection++;
                }

                else
                {
                    // We are coming out of a duplicate group
                    if (lengthDuplicateSection > 1)
                    {
                        // Only if the duplicate group is exactly one duplicate digit do we count it
                        if (lengthDuplicateSection == 2)
                            hasDuplicateDigit = true;
                    }

                    lengthDuplicateSection = 1;
                }
            }

            return hasDuplicateDigit;
        }
    }
}