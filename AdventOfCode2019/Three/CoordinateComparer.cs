using System.Collections.Generic;

namespace AdventOfCode2019.Three
{
    public class CoordinateComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate c1, Coordinate c2)
        {
            if (c2 == null && c1 == null)
                return true;
            else if (c1 == null | c2 == null)
                return false;
            else if (c1.X == c2.X && c1.Y == c2.Y)
                return true;
            else
                return false;
        }

        public int GetHashCode(Coordinate c)
        {
            return $"{c.X}{c.Y}".GetHashCode();
        }
    }
}