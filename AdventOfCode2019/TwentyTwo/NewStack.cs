using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.TwentyTwo
{
    public class NewStack : IDeckModification
    {
        public NewStack(string input)
        {
            // input is not used here
        }

        public static bool Applies(string input)
        {
            return input.Contains("new stack");
        }

        public int[] Do(int[] start)
        {
            List<int> outputList = start.ToList();
            outputList.Reverse();
            return outputList.ToArray();
        }
    }
}