using System.Collections.Generic;

namespace AdventOfCode2019.Ten
{
    public class BestLocationOutput
    {
        public Asteroid BestLocation { get; set; }
        public Dictionary<double, List<AsteroidDistance>> Distances { get; set; }
    }
}