using System;
using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2019.TwentyTwo
{
    public class DayTwentyTwo : IAdventProblemSet
    {
        public string Description()
        {
            return "Slam Shuffle [HARD]";
        }

        public int SortOrder()
        {
            return 22;
        }

        public string PartA()
        {
            string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
            int[] result = new SpaceCardShuffler(filePath, 10007).Shuffle();
            int location = result.ToList().FindIndex(i => i == 2019);

            return location.ToString();
        }

        public string PartB()
        {
            string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
            var result = RunPartB(filePath);
            return result.ToString();
        }

        // The math for this problem is way above my understanding, so I grabbed the following solution
        // from the Reddit thread
        // TODO: Review this against https://github.com/kbmacneal/adv_of_code_2019/blob/3bdc583ea5620296e187a076038ddde17e526abd/days/22.cs
        public BigInteger RunPartB(string filePath)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);

            BigInteger size = 119315717514047;
            BigInteger iter = 101741582076661;
            BigInteger position = 2020;
            BigInteger offset_diff = 0;
            BigInteger increment_mul = 1;

            foreach (var line in fileLines)
            {
                RunP2(ref increment_mul, ref offset_diff, size, line);
            }

            IncrementOffset incOff = Getseq(iter, increment_mul, offset_diff, size);

            var card = Get(incOff.Offset, incOff.Increment, 2020, size);
            return card;
        }

        private void RunP2(ref BigInteger inc_mul, ref BigInteger offset_diff, BigInteger size, string line)
        {
            if (line.StartsWith("cut"))
            {
                offset_diff += Int32.Parse(line.Split(' ').Last()) * inc_mul;
            }
            else if (line == "deal into new stack")
            {
                inc_mul *= -1;
                offset_diff += inc_mul;
            }
            else
            {
                var num = Int32.Parse(line.Split(' ').Last());

                var temp = TBI(num);
                temp = Inv(temp, size);
                inc_mul *= temp;
            }

            inc_mul = Mod(inc_mul,size);
            offset_diff = Mod(offset_diff, size);
        }

        private BigInteger Mod(BigInteger x, BigInteger m)
        {
            return (x % m + m) % m;
        }

        private BigInteger Inv(BigInteger num, BigInteger size)
        {
            return Mpow(num, size - 2, size);
        }

        private BigInteger Get(BigInteger offset, BigInteger increment, BigInteger i, BigInteger size)
        {
            return (offset + i * increment) % size;
        }

        private IncrementOffset Getseq(BigInteger iterations, BigInteger inc_mul, BigInteger offset_diff, BigInteger size)
        {
            var increment = Mpow(inc_mul,iterations, size);

            var offset = offset_diff * (1 - increment) * ((1 - inc_mul) % size);
            offset = Inv(offset, size);

            offset %= size;

            return new IncrementOffset() {Increment = increment, Offset = offset};
        }

        private BigInteger TBI(int num)
        {
            return new BigInteger(num);
        }

        private BigInteger Mpow(BigInteger bigInteger, BigInteger pow, BigInteger mod)
        {
            return BigInteger.ModPow(bigInteger, pow, mod);
        }

    }

    public class IncrementOffset
    {
        public BigInteger Increment { get; set; }
        public BigInteger Offset { get; set; }
    }

    public class SpaceCardShuffler
    {
        private List<string> _instructions;
        private int[] _startingCards;
        private List<IDeckModification> _modifications;

        public SpaceCardShuffler(string filePath, int cards)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            _instructions = fileLines;
            _startingCards = Enumerable.Range(0, cards).ToArray();

            _modifications = DetermineModifications();
        }

        public int[] Shuffle()
        {
            int[] output = _startingCards.ToArray();
            foreach (IDeckModification mod in _modifications)
            {
                int[] newOutput = mod.Do(output);
                output = newOutput;
            }

            return output;
        }

        private List<IDeckModification> DetermineModifications()
        {
            List<IDeckModification> modifications = new List<IDeckModification>();
            foreach (string instruction in _instructions)
            {
                if (NewStack.Applies(instruction))
                    modifications.Add(new NewStack(instruction));

                if (CutCards.Applies(instruction))
                    modifications.Add(new CutCards(instruction));

                if (IncrementCards.Applies(instruction))
                    modifications.Add(new IncrementCards(instruction));
            }

            return modifications;
        }
    }

    public class NewStack : IDeckModification
    {
        public NewStack(string input)
        {
            // input is not used here
        }

        public static bool Applies(string input)
        {
            return input.Contains("new stack");
        }

        public int[] Do(int[] start)
        {
            List<int> outputList = start.ToList();
            outputList.Reverse();
            return outputList.ToArray();
        }
    }

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

    public interface IDeckModification
    {
        int[] Do(int[] start);
    }
}