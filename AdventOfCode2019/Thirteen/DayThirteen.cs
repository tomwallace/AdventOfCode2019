using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Thirteen
{
    public class DayThirteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Care Package [IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 13;
        }

        public string PartA()
        {
            string filePath = @"Thirteen\DayThirteenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            long[] inputs = new long[] { };
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, inputs);
            computer.ProcessInstructions();
            List<long> intComputerOutput = computer.GetOutput();

            GameBoard gameBoard = new GameBoard(intComputerOutput);
            return gameBoard.CountBlockTiles().ToString();
        }

        public string PartB()
        {
            string filePath = @"Thirteen\DayThirteenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            long score = DetermineFinalPlayerScore(memoryInput);
            return score.ToString();
        }

        public long DetermineFinalPlayerScore(string memoryInput)
        {
            long finalScore = 0;
            long[] inputs = new long[] { };
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, inputs);
            computer.SetMemoryLocation(0, 2);

            do
            {
                computer.ProcessInstructions();
                List<long> intComputerOutput = computer.GetOutput();

                GameBoard gameBoard = new GameBoard(intComputerOutput);

                // Loop until all tiles are destroyed
                if (gameBoard.CountBlockTiles() == 0)
                {
                    finalScore = gameBoard.PlayerScore;
                    break;
                }

                long paddleX = gameBoard.FindFirstXValueOfType(GamePixel.Paddle);
                long ballX = gameBoard.FindFirstXValueOfType(GamePixel.Ball);

                // Move paddle in direction of ball
                long newInput = paddleX > ballX ? -1 : paddleX < ballX ? 1 : 0;

                computer.SetInput(new long[] { newInput });
            } while (finalScore == 0);

            return finalScore;
        }
    }
}