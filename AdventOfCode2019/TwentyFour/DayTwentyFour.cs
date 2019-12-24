namespace AdventOfCode2019.TwentyFour
{
    public class DayTwentyFour : IAdventProblemSet
    {
        public string Description()
        {
            return "Planet of Discord";
        }

        public int SortOrder()
        {
            return 24;
        }

        public string PartA()
        {
            string filePath = @"TwentyFour\DayTwentyFourInput.txt";
            double result = BioDiversityAfterFirstRepeat(filePath);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"TwentyFour\DayTwentyFourInput.txt";
            int result = CountBugsAfterIterations(filePath, 200);
            return result.ToString();
        }

        public double BioDiversityAfterFirstRepeat(string filePath)
        {
            bool found = false;
            BugPlanet bugPlanet = new BugPlanet(filePath);

            do
            {
                bugPlanet.Print();
                found = bugPlanet.Iterate();
            } while (!found);

            return bugPlanet.CalculateBioDiversity();
        }

        public int CountBugsAfterIterations(string filePath, int iterations)
        {
            BugPlanet bugPlanet = new BugPlanet(filePath);

            for (int i = 0; i < iterations; i++)
            {
                bugPlanet.IterateRecursive();
            }

            return bugPlanet.CountBugs();
        }
    }
}