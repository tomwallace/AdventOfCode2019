using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Thirteen
{
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

        public long FindFirstXValueOfType(GamePixel type)
        {
            string key = Board.FirstOrDefault(b => b.Value == type).Key;
            string[] split = key.Split(',');
            return long.Parse(split[0]);
        }
    }
}