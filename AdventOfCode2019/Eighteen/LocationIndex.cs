namespace AdventOfCode2019.Eighteen
{
    public class LocationIndex
    {
        public char Location { get; set; }
        public int Index { get; set; }

        public LocationIndex(char location, int index)
        {
            Location = location;
            Index = index;
        }
    }
}