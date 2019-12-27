using AdventOfCode2019.Utility;
using System.Collections.Generic;

namespace AdventOfCode2019.TwentyFive
{
    public class DayTwentyFive : IAdventProblemSet
    {
        public string Description()
        {
            return "Cryostasis [ACII IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 25;
        }

        public string PartA()
        {
            string filePath = @"TwentyFive\DayTwentyFiveInput.txt";
            TraverseSantasShip(filePath);

            return "Did you play the game long enough to get the password to the main airlock?";
        }

        public string PartB()
        {
            return "Need to complete all other 49 stars to get Day TwentyFive Part B";
        }

        public void TraverseSantasShip(string filePath)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, s => s);
            string memoryInput = fileLines[0];
            var computer = new IntCodeComputer.IntCodeComputer(memoryInput, 0);

            System.Console.WriteLine("Play the game until you get the password to the main airlock on Santa's ship.  Use /q to quit to the main menu.");

            while (true)
            {
                computer.ProcessInstructions();
                computer.PrintAsciiOutput(true);
                computer.ClearOutput();

                string commandLine = System.Console.ReadLine();

                // Provide ability to exit
                if (commandLine == "/q")
                    return;

                string inputMultiLine = $@"{commandLine.Trim()}
";
                computer.SetInputFromMultiLineAscii(inputMultiLine);
            }
        }
    }
}