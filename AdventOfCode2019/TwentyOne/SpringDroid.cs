using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Utility;

namespace AdventOfCode2019.TwentyOne
{
    public class SpringDroid
    {
        private string _memoryInput;
        private IntCodeComputer.IntCodeComputer _computer;

        public SpringDroid(string filePath)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, s => s);
            _memoryInput = fileLines[0];
            _computer = new IntCodeComputer.IntCodeComputer(_memoryInput, 0);
        }

        public long Activate(string inputMultiLine)
        {
            _computer.SetInputFromMultiLineAscii(inputMultiLine);
            _computer.ClearOutput();
            _computer.ProcessInstructions();

            _computer.PrintAsciiOutput();

            return _computer.GetDiagnosticCode();
        }
    }
}