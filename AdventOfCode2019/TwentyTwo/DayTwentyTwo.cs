using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.TwentyTwo
{
    public class DayTwentyTwo : IAdventProblemSet
    {
        public string Description()
        {
            return "Slam Shuffle";
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
            string filePath = @"Twenty\DayTwentyInput.txt";
            //int result = FindFewestStepsInMaze(filePath, true);
            return ""; //result.ToString();
        }
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