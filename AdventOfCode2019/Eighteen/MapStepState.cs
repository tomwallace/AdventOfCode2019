using System.Collections.Generic;

namespace AdventOfCode2019.Eighteen
{
    public class MapStepState
    {
        public int Position { get; set; }
        public int Distance { get; set; }
        public List<char> Doors { get; set; }

        public MapStepState(int pos, int distance, List<char> doors)
        {
            Position = pos;
            Distance = distance;
            Doors = doors;
        }
    }
}