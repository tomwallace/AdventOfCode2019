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

        public int CountBlockTiles(GameBoard gameBoard)
        {
            int count = gameBoard.Board.Count(g => g.Value == GamePixel.Block);
            return count;
        }

        public long AttemptTwo(string memoryInput)
        {
            int blocksRemaining = int.MaxValue;
            GameBoard gameBoard;

            Queue<List<long>> queue = new Queue<List<long>>();
            queue.Enqueue(new List<long>() {-1});
            queue.Enqueue(new List<long>() { 0 });
            queue.Enqueue(new List<long>() { 0 });

            long[] inputOptions = new long[] { -1, 0, 1 };
            do
            {
                List<long> currentInput = queue.Dequeue();
                IntCodeComputer.IntCodeComputer optionComputer = new IntCodeComputer.IntCodeComputer(memoryInput, currentInput.ToArray());
                optionComputer.ProcessInstructions();
                List<long> intComputerOutput = optionComputer.GetOutput();

                gameBoard = new GameBoard(intComputerOutput);
                blocksRemaining = gameBoard.CountBlockTiles();

                foreach (long input in inputOptions)
                {
                    optionComputer = new IntCodeComputer.IntCodeComputer(memoryInput, input);
                    optionComputer.ProcessInstructions();
                    intComputerOutput = optionComputer.GetOutput();

                    gameBoard = new GameBoard(intComputerOutput);

                    if (gameBoard.CountBlockTiles() == 0)
                    {
                        break;
                    }

                    //queue.Enqueue(optionComputer.CloneMemory());
                }
            } while (blocksRemaining > 0);

            return gameBoard.PlayerScore;
        }

        public long DetermineFinalPlayerScore(string memoryInput)
        {
            long finalScore = 0;
            long[] inputs = new long[] { };
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, inputs);
            computer.SetMemoryLocation(0, 2);

            Queue<Dictionary<long, long>> queue = new Queue<Dictionary<long, long>>();
            queue.Enqueue(computer.CloneMemory());

            long[] inputOptions = new long[] { -1, 0, 1 };
            do
            {
                Dictionary<long, long> existingMemory = queue.Dequeue();

                foreach (long input in inputOptions)
                {
                    IntCodeComputer.IntCodeComputer optionComputer = new IntCodeComputer.IntCodeComputer(existingMemory, input);
                    optionComputer.ProcessInstructions();
                    List<long> intComputerOutput = optionComputer.GetOutput();

                    GameBoard gameBoard = new GameBoard(intComputerOutput);

                    if (gameBoard.CountBlockTiles() == 0)
                    {
                        finalScore = gameBoard.PlayerScore;
                        break;
                    }

                    queue.Enqueue(optionComputer.CloneMemory());
                }
            } while (finalScore == 0);

            return finalScore;
        }

        private Dictionary<string, GamePixel> MakeGameBoard(List<long> intComputerOutput)
        {
            Dictionary<string, GamePixel> gameBoard = new Dictionary<string, GamePixel>();

            for (int i = 0; i < intComputerOutput.Count; i += 3)
            {
                string key = $"{intComputerOutput[i]},{intComputerOutput[i + 1]}";
                gameBoard.Add(key, (GamePixel)intComputerOutput[i + 2]);
            }

            return gameBoard;
        }
    }

    // TODO: Split out classes
    public enum GamePixel
    {
        /*
         * 0 is an empty tile. No game object appears in this tile.
1 is a wall tile. Walls are indestructible barriers.
2 is a block tile. Blocks can be broken by the ball.
3 is a horizontal paddle tile. The paddle is indestructible.
4 is a ball tile. The ball moves diagonally and bounces off objects.
         */
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4
    }

    public class GameBoard
    {
        public Dictionary<string, GamePixel> Board { get; }

        public long PlayerScore { get; }

        public GameBoard(List<long> intComputerOutput)
        {
            Dictionary<string, GamePixel> gameBoard = new Dictionary<string, GamePixel>();

            for (int i = 0; i < intComputerOutput.Count; i += 3)
            {
                if (intComputerOutput[i] == -1 && intComputerOutput[i + 1] == 0)
                    PlayerScore = intComputerOutput[i + 2];
                else
                {
                    string key = $"{intComputerOutput[i]},{intComputerOutput[i + 1]}";

                    if (gameBoard.ContainsKey(key))
                        gameBoard[key] = (GamePixel)intComputerOutput[i + 2];
                    else
                        gameBoard.Add(key, (GamePixel)intComputerOutput[i + 2]);
                }
            }

            Board = gameBoard;
        }

        public int CountBlockTiles()
        {
            int count = Board.Count(g => g.Value == GamePixel.Block);
            return count;
        }
    }
}