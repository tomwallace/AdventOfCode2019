using System.Collections.Generic;

namespace AdventOfCode2019.Twenty
{
    public class DonutMazeState
    {
        private HashSet<string> _stepsTaken;
        private int _x;
        private int _y;
        private int _numberSteps;

        public bool TeleporterJustTaken { get; set; }

        public int MazeLevel { get; set; }

        public DonutMazeState(HashSet<string> stepsTaken, int x, int y, int numberOfSteps)
        {
            _stepsTaken = stepsTaken;
            _x = x;
            _y = y;
            _numberSteps = numberOfSteps;
            TeleporterJustTaken = false;
            MazeLevel = 0;
        }

        public DonutMazeState(DonutMazeState existing)
        {
            _stepsTaken = new HashSet<string>(existing.GetStepsTaken());
            _x = existing.GetX();
            _y = existing.GetY();
            _numberSteps = existing.GetNumberOfSteps();
            TeleporterJustTaken = existing.TeleporterJustTaken;
            MazeLevel = existing.MazeLevel;
        }

        public int GetX() => _x;

        public int GetY() => _y;

        public int GetNumberOfSteps() => _numberSteps;

        public HashSet<string> GetStepsTaken() => _stepsTaken;

        public bool MoveMe(DonutMaze maze, int x, int y, int mazeLevel)
        {
            if (_stepsTaken.Contains($"{x},{y},{mazeLevel}"))
                return false;

            if (!maze.IsPassable(x, y))
                return false;

            _x = x;
            _y = y;
            _numberSteps++;
            _stepsTaken.Add($"{_x},{_y},{mazeLevel}");
            TeleporterJustTaken = false;

            return true;
        }
    }
}