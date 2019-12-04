namespace AdventOfCode2019.Three
{
    public class Coordinate
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Steps { get; set; }

        public override bool Equals(object obj)
        {
            CoordinateComparer comparer = new CoordinateComparer();
            return comparer.Equals(this, (Coordinate)obj);
        }

        public override int GetHashCode()
        {
            CoordinateComparer comparer = new CoordinateComparer();
            return comparer.GetHashCode(this);
        }
    }
}