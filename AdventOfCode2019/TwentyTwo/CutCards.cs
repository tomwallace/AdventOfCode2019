using System.Linq;

namespace AdventOfCode2019.TwentyTwo
{
    public class CutCards : IDeckModification
    {
        private int _n;

        public CutCards(string input)
        {
            _n = int.Parse(input.Split(' ')[1]);
        }

        public static bool Applies(string input)
        {
            return input.Contains("cut");
        }

        public int[] Do(int[] start)
        {
            if (_n > 0)
            {
                int[] bottom = start.ToList().Take(_n).ToArray();
                int[] top = start.ToList().Skip(_n).ToArray();

                return top.Concat(bottom).ToArray();
            }

            int[] b = start.ToList().Skip(start.Length + _n).ToArray();
            int[] t = start.ToList().Take(start.Length + _n).ToArray();

            return b.Concat(t).ToArray();
        }
    }
}