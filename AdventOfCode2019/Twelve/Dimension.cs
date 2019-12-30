namespace AdventOfCode2019.Twelve
{
    public class Dimension
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }
}