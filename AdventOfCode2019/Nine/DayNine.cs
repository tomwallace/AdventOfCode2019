using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Nine
{
    public class DayNine : IAdventProblemSet
    {
        public string Description()
        {
            return "Sensor Boost [IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 9;
        }

        public string PartA()
        {
            string filePath = @"Nine\DayNineInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            long result = CalculateBoostKeycode(memoryInput, 1);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Nine\DayNineInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            long result = CalculateBoostKeycode(memoryInput, 2);
            return result.ToString();
        }

        public long CalculateBoostKeycode(string memoryInput, long startingInput)
        {
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, startingInput);
            computer.ProcessInstructions();
            return computer.GetDiagnosticCode();
        }
    }
}