using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2019.Eleven
{
    public class DayEleven : IAdventProblemSet
    {
        public string Description()
        {
            return "Space Police [IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 11;
        }

        public string PartA()
        {
            string filePath = @"Eleven\DayElevenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            int paintedSections = GetNumberOfPaintedHullSections(memoryInput, 0);

            return paintedSections.ToString();
        }

        public string PartB()
        {
            string filePath = @"Eleven\DayElevenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            int paintedSections = GetNumberOfPaintedHullSections(memoryInput, 1);

            return paintedSections.ToString();
        }

        public int GetNumberOfPaintedHullSections(string memoryInput, long startingColor)
        {
            long[] colorInput = new []{startingColor};
            int resultCode = 0;
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, colorInput, true);
            PaintingRobot robot = new PaintingRobot();
            robot.X = 100;
            robot.Y = 100;
            robot.PaintedHullSections.Add($"{robot.X},{robot.Y}", startingColor);

            long[,] grid = new long[200,200];

            do
            {
                colorInput = new[] { robot.ColorCurrentlyOver() };
                computer.SetInput(colorInput);

                resultCode += computer.ProcessInstructions();
                long colorOutput = computer.GetDiagnosticCode();

                colorInput = new long[] { };

                resultCode += computer.ProcessInstructions();
                long changeFacing = computer.GetDiagnosticCode();

                grid[robot.X, robot.Y] = colorOutput;
                robot.ProcessInstruction(colorOutput, changeFacing);

                computer.ClearOutput();
            } while (resultCode == 0);

            PrintGrid(grid, 200, 200);
            return robot.PaintedHullSections.Count;
        }

        private void PrintGrid(long[,] grid, int rows, int cols)
        {
            Debug.WriteLine("Printing Image Layer ---------------------------");
            Debug.WriteLine("");

            for (int colPointer = (cols - 1); colPointer >= 0; colPointer--)
            {
                string row = "";

                for (int rowPointer = 0; rowPointer < rows; rowPointer++)
                {
                    long color = grid[rowPointer, colPointer];
                    if (color == 0)
                        row = $"{row} ";
                    else if (color == 1)
                        row = $"{row}#";
                    else
                        row = $"{row} ";
                }

                Debug.WriteLine(row);
            }

            Debug.WriteLine("");
            Debug.WriteLine("---------------------------------------");
        }
    }
}