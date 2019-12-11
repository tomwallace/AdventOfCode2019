using AdventOfCode2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Ten
{
    public class DayTen : IAdventProblemSet
    {
        public string Description()
        {
            return "Monitoring Station";
        }

        public int SortOrder()
        {
            return 10;
        }

        public string PartA()
        {
            string filePath = @"Ten\DayTenInput.txt";
            int calculation = HowManyAsteroidsSeenFromBestLocation(filePath).Distances.Count;

            return calculation.ToString();
        }

        public string PartB()
        {
            string filePath = @"Ten\DayTenInput.txt";
            int destroyed = RunAsteroidRoutine(filePath, 200);

            return destroyed.ToString();
        }

        public BestLocationOutput HowManyAsteroidsSeenFromBestLocation(string filePath)
        {
            List<Asteroid> asteroidBelt = MakeAsteroidBelt(filePath);
            BestLocationOutput bestLocationOutput = new BestLocationOutput();
            int bestCount = 0;

            for (int a = 0; a < asteroidBelt.Count; a++)
            {
                Asteroid currentAsteroid = asteroidBelt[a];
                Dictionary<double, List<AsteroidDistance>> distances = new Dictionary<double, List<AsteroidDistance>>();
                for (int c = 0; c < asteroidBelt.Count; c++)
                {
                    if (a != c)
                    {
                        Asteroid comparing = asteroidBelt[c];
                        double angle = currentAsteroid.GetComparisonAngle(comparing);
                        int distance = currentAsteroid.GetDistance(comparing);
                        AsteroidDistance asteroidDistance = new AsteroidDistance() { Asteroid = comparing, Distance = distance };
                        if (distances.ContainsKey(angle))
                            distances[angle].Add(asteroidDistance);
                        else
                            distances.Add(angle, new List<AsteroidDistance>() { asteroidDistance });
                    }
                }

                // Sort distances
                double[] keys = distances.Keys.ToArray();
                for (int i = 0; i < distances.Count; i++)
                {
                    List<AsteroidDistance> distanceSet = distances[keys[i]];
                    List<AsteroidDistance> sorted = distanceSet.OrderByDescending(d => d.Distance).ToList();
                    distances[keys[i]] = sorted;
                }

                if (distances.Count > bestCount)
                {
                    bestCount = distances.Count;
                    bestLocationOutput.BestLocation = currentAsteroid;
                    bestLocationOutput.Distances = distances;
                }
            }

            return bestLocationOutput;
        }

        public int RunAsteroidRoutine(string filePath, int numberAsteroidsToDestroy)
        {
            BestLocationOutput output = HowManyAsteroidsSeenFromBestLocation(filePath);
            List<Asteroid> destroyed = new List<Asteroid>();
            int angleCounter = 0;
            double[] sortedAngles = output.Distances.Keys.OrderByDescending(d => d).ToArray();
            do
            {
                double currentAngle = sortedAngles[angleCounter];
                // vaporize!
                destroyed.Add(output.Distances[currentAngle].First().Asteroid);
                output.Distances[currentAngle].RemoveAt(0);

                angleCounter++;
            } while (destroyed.Count < numberAsteroidsToDestroy);

            return destroyed.Last().X * 100 + destroyed.Last().Y;
        }

        private List<Asteroid> MakeAsteroidBelt(string filePath)
        {
            List<Asteroid> asteroidBelt = new List<Asteroid>();

            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            for (int y = 0; y < fileLines.Count; y++)
            {
                char[] row = fileLines[y].ToCharArray();
                for (int x = 0; x < row.Length; x++)
                {
                    if (row[x] == '#')
                        asteroidBelt.Add(new Asteroid(x, y));
                }
            }

            return asteroidBelt;
        }
    }

    public class Asteroid
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double GetComparisonAngle(Asteroid comparing)
        {
            // Geometry is not my thing, so I had to look this angle calculation up
            // https://en.wikipedia.org/wiki/Atan2
            int diffX = comparing.X - X;
            int diffY = comparing.Y - Y;

            return Math.PI / 2 + Math.Atan2(diffX, diffY);
        }

        public int GetDistance(Asteroid comparing)
        {
            int xDistance = Math.Abs(comparing.X - X);
            int yDistance = Math.Abs(comparing.Y - Y);

            return xDistance + yDistance;
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }

    public class AsteroidDistance
    {
        public Asteroid Asteroid { get; set; }
        public int Distance { get; set; }
    }

    public class BestLocationOutput
    {
        public Asteroid BestLocation { get; set; }
        public Dictionary<double, List<AsteroidDistance>> Distances { get; set; }
    }
}