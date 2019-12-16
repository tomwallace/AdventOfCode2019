namespace AdventOfCode2019.Fifteen
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Adjust(Position modifier)
        {
            X += modifier.X;
            Y += modifier.Y;
        }
    }
}