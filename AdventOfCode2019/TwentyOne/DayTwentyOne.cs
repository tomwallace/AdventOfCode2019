namespace AdventOfCode2019.TwentyOne
{
    public class DayTwentyOne : IAdventProblemSet
    {
        public string Description()
        {
            return "Springdroid Adventure [ACII IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 21;
        }

        public string PartA()
        {
            string filePath = @"TwentyOne\DayTwentyOneInput.txt";

            SpringDroid springDroid = new SpringDroid(filePath);

            string inputMultiLine = @"OR A J
AND B J
AND C J
NOT J J
AND D J
WALK
";

            long result = springDroid.Activate(inputMultiLine);

            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"TwentyOne\DayTwentyOneInput.txt";

            SpringDroid springDroid = new SpringDroid(filePath);

            string inputMultiLine = @"OR A J
AND B J
AND C J
NOT J J
AND D J
OR I T
OR F T
AND E T
OR H T
AND T J
RUN
";

            long result = springDroid.Activate(inputMultiLine);

            return result.ToString();
        }
    }
}