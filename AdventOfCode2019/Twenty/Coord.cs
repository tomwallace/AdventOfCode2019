namespace AdventOfCode2019.Twenty
{
    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord(string coords)
        {
            string[] split = coords.Split(',');
            X = int.Parse(split[0]);
            Y = int.Parse(split[1]);
        }
    }
}