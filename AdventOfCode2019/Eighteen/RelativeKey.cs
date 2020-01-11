namespace AdventOfCode2019.Eighteen
{
    public class RelativeKey
    {
        public char Char { get; }
        public int Pos { get; }
        public int Distance { get; }
        public char[] RequiredKeys { get; }

        public RelativeKey(char key, int pos, int distance, char[] requiredKeys)
        {
            Char = key;
            Pos = pos;
            Distance = distance;
            RequiredKeys = requiredKeys;
        }
    }
}