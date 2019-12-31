using System.Linq;

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
            var result = DayTwentyTwoOther.Run(filePath);
            return result.ToString();
        }
    }
}