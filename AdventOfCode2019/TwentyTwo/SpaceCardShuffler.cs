using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.TwentyTwo
{
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
}