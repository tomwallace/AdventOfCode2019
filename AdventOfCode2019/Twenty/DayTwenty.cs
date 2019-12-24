using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Twenty
{
    public class DayTwenty : IAdventProblemSet
    {
        public string Description()
        {
            return "Donut Maze [HARD]";
        }

        public int SortOrder()
        {
            return 20;
        }

        public string PartA()
        {
            string filePath = @"Twenty\DayTwentyInput.txt";
            int result = FindFewestStepsInMaze(filePath, false);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Twenty\DayTwentyInput.txt";
            int result = FindFewestStepsInMaze(filePath, true);
            return result.ToString();
        }

        public int FindFewestStepsInMaze(string filePath, bool allowMazeLevels)
        {
            DonutMaze maze = new DonutMaze(filePath, allowMazeLevels);
            int bestStepsTaken = int.MaxValue;

            Queue<DonutMazeState> queue = new Queue<DonutMazeState>();
            queue.Enqueue(new DonutMazeState(new HashSet<string>() { $"{maze.StartX},{maze.StartY}" }, maze.StartX, maze.StartY, 0));

            do
            {
                DonutMazeState current = queue.Dequeue();

                // Exit if we already have a better score
                if (current.GetNumberOfSteps() >= bestStepsTaken)
                    continue;

                // If we have reached the target, set our best score
                if (current.GetX() == maze.EndX && current.GetY() == maze.EndY && current.MazeLevel == 0)
                {
                    bestStepsTaken = current.GetNumberOfSteps();
                    continue;
                }

                // Iterate over different steps
                DonutMazeState north = new DonutMazeState(current);
                if (north.MoveMe(maze, north.GetX(), north.GetY() - 1, north.MazeLevel))
                    queue.Enqueue(north);

                DonutMazeState east = new DonutMazeState(current);
                if (east.MoveMe(maze, east.GetX() + 1, east.GetY(), east.MazeLevel))
                    queue.Enqueue(east);

                DonutMazeState south = new DonutMazeState(current);
                if (south.MoveMe(maze, south.GetX(), south.GetY() + 1, south.MazeLevel))
                    queue.Enqueue(south);

                DonutMazeState west = new DonutMazeState(current);
                if (west.MoveMe(maze, west.GetX() - 1, west.GetY(), west.MazeLevel))
                    queue.Enqueue(west);

                // Check for teleportation
                Teleporter teleporterSendPoint = maze.GetTeleporterSendPoint(current.GetX(), current.GetY(), current.MazeLevel);
                if (teleporterSendPoint != null && !current.TeleporterJustTaken && current.MazeLevel < 30)
                {
                    DonutMazeState teleporter = new DonutMazeState(current);

                    if (teleporter.MoveMe(maze, teleporterSendPoint.Coord.X, teleporterSendPoint.Coord.Y, teleporter.MazeLevel))
                    {
                        if (maze.AllowMazeLevels)
                            teleporter.MazeLevel += teleporterSendPoint.ChangeMazeLevel;

                        teleporter.TeleporterJustTaken = true;

                        queue.Enqueue(teleporter);
                    }
                }
            } while (queue.Any());

            return bestStepsTaken;
        }
    }
}