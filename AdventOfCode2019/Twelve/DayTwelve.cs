using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Twelve
{
    public class DayTwelve : IAdventProblemSet
    {
        public string Description()
        {
            return "The N-Body Problem";
        }

        public int SortOrder()
        {
            return 12;
        }

        public string PartA()
        {
            string filePath = @"Twelve\DayTwelveInput.txt";
            int calculation = CalculateTotalSystemEnergy(filePath, 1000);

            return calculation.ToString();
        }

        public string PartB()
        {
            string filePath = @"Twelve\DayTwelveInput.txt";
            var calculation = StepsUntilPositionsRepeated(filePath);

            return calculation.ToString();
        }

        public int CalculateTotalSystemEnergy(string filePath, int timeSteps)
        {
            List<Moon> moons = CreateMoons(filePath);

            for (int i = 0; i < timeSteps; i++)
            {
                // Apply gravity
                for (int m = 0; m < moons.Count; m++)
                {
                    Moon current = moons[m];
                    for (int innerM = 0; innerM < moons.Count; innerM++)
                    {
                        Moon comparingMoon = moons[innerM];
                        current.ApplyGravity(comparingMoon);
                    }
                }

                // Apply velocity
                for (int m = 0; m < moons.Count; m++)
                {
                    Moon current = moons[m];
                    current.Move(0);
                }
            }

            int totalSystemEnergy = moons.Sum(m => m.TotalEnergy());
            return totalSystemEnergy;
        }

        public double StepsUntilPositionsRepeated(string filePath)
        {
            long stepCounter = 0;
            int numberDuplicatedMoons = 0;
            List<Moon> moons = CreateMoons(filePath);
            long minX = -1;
            long minY = -1;
            long minZ = -1;

            do
            {
                // Apply gravity
                for (int m = 0; m < moons.Count; m++)
                {
                    Moon current = moons[m];
                    for (int innerM = 0; innerM < moons.Count; innerM++)
                    {
                        Moon comparingMoon = moons[innerM];
                        current.ApplyGravity(comparingMoon);
                    }
                }

                // Apply velocity
                for (int m = 0; m < moons.Count; m++)
                {
                    Moon current = moons[m];
                    current.Move(stepCounter);
                }

                //Can't take credit for this
                //Planets move in symetrical cycles which means their velocity will reach zero at half the number of steps it'll take to get back to their original position.
                if (minX == -1 && moons.All(m => m.GetVelocity().X == 0))
                    minX = stepCounter + 1;
                if (minY == -1 && moons.All(m => m.GetVelocity().Y == 0))
                    minY = stepCounter + 1;
                if (minZ == -1 && moons.All(m => m.GetVelocity().Z == 0))
                    minZ = stepCounter + 1;

                stepCounter++;
            } while (minX == -1 || minY == -1 || minZ == -1);

            return (LCM(minX, LCM(minY, minZ)) * 2);
        }

        private double GCD(double a, double b)
        {
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }

        // Calculate the least common multiple
        private double LCM(double a, double b)
        {
            return a * b / GCD(a, b);
        }

        private List<Moon> CreateMoons(string filePath)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            List<Moon> moons = fileLines.Select(m => new Moon(m)).ToList();
            return moons;
        }
    }
}