namespace AdventOfCode2019.TwentyThree
{
    public class DayTwentyThree : IAdventProblemSet
    {
        public string Description()
        {
            return "Category Six [IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 23;
        }

        public string PartA()
        {
            string filePath = @"TwentyThree\DayTwentyThreeInput.txt";
            long result = RunNetwork(filePath, false);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"TwentyThree\DayTwentyThreeInput.txt";
            long result = RunNetwork(filePath, true);
            return result.ToString();
        }

        public long RunNetwork(string filePath, bool useNat)
        {
            Network network = new Network(filePath, 50, useNat);
            do
            {
                long? possibleOutput = network.Execute();

                if (possibleOutput.HasValue)
                    return possibleOutput.Value;
            } while (true);
        }
    }
}