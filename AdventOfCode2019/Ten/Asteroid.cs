using System;

namespace AdventOfCode2019.Ten
{
    public class Asteroid
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double GetComparisonAngle(Asteroid comparing)
        {
            // Geometry is not my thing, so I had to look this angle calculation up
            // https://en.wikipedia.org/wiki/Atan2
            int diffX = comparing.X - X;
            int diffY = comparing.Y - Y;

            return Math.PI / 2 + Math.Atan2(diffX, diffY);
        }

        public int GetDistance(Asteroid comparing)
        {
            int xDistance = Math.Abs(comparing.X - X);
            int yDistance = Math.Abs(comparing.Y - Y);

            return xDistance + yDistance;
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }
}