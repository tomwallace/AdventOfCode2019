namespace AdventOfCode2019.TwentyTwo
{
    public class IncrementCards : IDeckModification
    {
        private int _n;

        public IncrementCards(string input)
        {
            _n = int.Parse(input.Split(' ')[3]);
        }

        public static bool Applies(string input)
        {
            return input.Contains("with increment");
        }

        public int[] Do(int[] start)
        {
            int end = start.Length;
            int[] output = new int[end];

            int position = 0;
            for (int i = 0; i < end; i++)
            {
                output[position] = start[i];
                position += _n;

                if (position >= end)
                    position = position - end;
            }

            return output;
        }
    }
}