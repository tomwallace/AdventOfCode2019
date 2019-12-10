using System.Collections.Generic;
using AdventOfCode2019.Utility;

namespace AdventOfCode2019.Ten
{
    public class DayTen : IAdventProblemSet
    {
        public string Description()
        {
            return "Monitoring Station";
        }

        public int SortOrder()
        {
            return 10;
        }

        public string PartA()
        {
            //IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(INPUT, 1);
            //computer.ProcessInstructions();
            return ""; //computer.GetDiagnosticCode().ToString();
        }

        public string PartB()
        {
            //IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(INPUT, 5);
            //computer.ProcessInstructions();
            return ""; //computer.GetDiagnosticCode().ToString();
        }
        /*
        public int HowManyAsteroidsSeenFromBestLocation(string filePath, int rows, int cols)
        {
            char[,] asteroidBelt = MakeAsteroidBelt(filePath, rows, cols);

        }

        public int? CalculateAsteroidsSeen(char[,] asteroidBelt, int x, int y)
        {
            // If not an asteroid, return null
            if (asteroidBelt[x, y] == '.')
                return null;


        }
        */
        private char[,] MakeAsteroidBelt(string filePath, int rows, int cols)
        {
            char[,] asteroidBelt = new char[cols,rows];

            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            for (int y = 0; y < rows; y++)
            {
                char[] row = fileLines[y].ToCharArray();
                for (int x = 0; y < cols; x++)
                {
                    asteroidBelt[x, y] = row[x];
                }
            }

            return asteroidBelt;
        }
    }
}