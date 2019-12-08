namespace AdventOfCode2019.Eight
{
    public class Pixel
    {
        public int Color { get; set; }

        public Pixel(int color)
        {
            Color = color;
        }

        public Pixel(char color)
        {
            Color = int.Parse(color.ToString());
        }
    }
}