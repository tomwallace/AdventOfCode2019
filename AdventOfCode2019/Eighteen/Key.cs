using System.Collections.Generic;

namespace AdventOfCode2019.Eighteen
{
    public class Key
    {
        public char Char { get; }
        public int Pos { get; }
        public Dictionary<char, RelativeKey> RelativeKeys { get; }

        public Key(char key, int pos, Dictionary<char, RelativeKey> relativeKeys)
        {
            Char = key;
            Pos = pos;
            RelativeKeys = relativeKeys;
        }
    }
}