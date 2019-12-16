using System.Collections.Generic;

namespace AdventOfCode2019.Fifteen
{
    public class RepairBotNode
    {
        public Position Position { get; set; }
        public int StepsTaken { get; set; }

        public IntCodeComputer.IntCodeComputer Computer { get; set; }
        public HashSet<string> LocationsVisited { get; set; }

        public RepairBotNode(Position position, int stepsTaken)
        {
            LocationsVisited = new HashSet<string>();
            Position = position;
            StepsTaken = stepsTaken;
        }
    }
}