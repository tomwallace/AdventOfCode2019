using AdventOfCode2019.Utility;
using System;
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
            string filePath = @"Ten\DayTenInput.txt";
            //int destroyed = RunAsteroidRoutine(filePath, 200);

            return ""; //destroyed.ToString();
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

        public long StepsUntilPositionsRepeated(string filePath)
        {
            long stepCounter = 0;
            int numberDuplicatedMoons = 0;
            List<Moon> moons = CreateMoons(filePath);

            // TODO: Remove code duplication through private method
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

                stepCounter++;

                numberDuplicatedMoons = moons.Count(m => m.FirstStepDuplicated() > -1);

            } while (numberDuplicatedMoons < moons.Count);

            // TODO: Return here and figure out way to get common multiplier
            long[] firstDuplicates = moons.Select(m => m.FirstStepDuplicated()).ToArray();

            return FindSmallestNumberAllDivideWithoutRemainder(firstDuplicates);
            //var result = findGCD(firstDuplicates, moons.Count);
            //var lcm = findlcm(firstDuplicates, moons.Count);
            //return stepCounter;
        }

        private long FindSmallestNumberAllDivideWithoutRemainder(long[] firstDuplicates)
        {
            for (long i = 1; i < long.MaxValue; i++)
            {
                if (firstDuplicates.All(d => i % d == 0))
                    return i;
            }

            throw new ArgumentException("Arguments do not divide evenly into each other, so coding error");
        }

        // Utility function to find 
        // GCD of 'a' and 'b' 
        private long gcd(long a, long b)
        {
            if (b == 0)
                return a;
            return gcd(b, a % b);
        }

        // Returns LCM of array elements 
        private long findlcm(long[] arr, int n)
        {
            // Initialize result 
            long ans = arr[0];

            // ans contains LCM of arr[0], ..arr[i] 
            // after i'th iteration, 
            for (int i = 1; i < n; i++)
                ans = (((arr[i] * ans)) /
                       (gcd(arr[i], ans)));

            return ans;
        }

        // Function to return gcd of a and b 
        /*
        private long gcd(long a, long b)
        {
            if (a == 0)
                return b;
            return gcd(b % a, a);
        }
        */
        // Function to find gcd of array of 
        // numbers 
        long findGCD(long[] arr, int n)
        {
            long result = arr[0];
            for (int i = 1; i < n; i++)
            {
                result = gcd(arr[i], result);

                if (result == 1)
                {
                    return 1;
                }
            }
            return result;
        }

        private string CompositeMoonString(List<Moon> moons)
        {
            string composite = "";
            foreach (Moon moon in moons)
            {
                composite = $"{composite}{moon}";
            }

            return composite;
        }

        private List<Moon> CreateMoons(string filePath)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            List<Moon> moons = fileLines.Select(m => new Moon(m)).ToList();
            return moons;
        }
    }

    public class Moon
    {
        private Dimension _position;
        private Dimension _velocity;
        private Dictionary<string, long> _previousStates;
        private long _firstStepDuplicated;

        public Moon(string input)
        {
            _position = ParsePosition(input);
            _velocity = new Dimension();
            _previousStates = new Dictionary<string, long>();
            _firstStepDuplicated = -1;
        }

        public void ApplyGravity(Moon otherMoon)
        {
            if (otherMoon.Equals(this))
                return;

            if (otherMoon.GetPosition().X > _position.X)
                _velocity.X++;

            if (otherMoon.GetPosition().X < _position.X)
                _velocity.X--;

            if (otherMoon.GetPosition().Y > _position.Y)
                _velocity.Y++;

            if (otherMoon.GetPosition().Y < _position.Y)
                _velocity.Y--;

            if (otherMoon.GetPosition().Z > _position.Z)
                _velocity.Z++;

            if (otherMoon.GetPosition().Z < _position.Z)
                _velocity.Z--;
        }

        public Dimension GetPosition()
        {
            return _position;
        }

        public void Move(long stepNumber)
        {
            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
            _position.Z += _velocity.Z;

            if (_firstStepDuplicated == -1 && _previousStates.ContainsKey(ToString()))
                _firstStepDuplicated = stepNumber;

            if (_firstStepDuplicated == -1)
                _previousStates.Add(ToString(),stepNumber);
        }

        public long FirstStepDuplicated()
        {
            return _firstStepDuplicated;
        }

        public int TotalEnergy()
        {
            return (Math.Abs(_position.X) + Math.Abs(_position.Y) + Math.Abs(_position.Z)) *
                   (Math.Abs(_velocity.X) + Math.Abs(_velocity.Y) + Math.Abs(_velocity.Z));
        }

        public override string ToString()
        {
            return $"[{_position}] [{_velocity}]";
        }

        private Dimension ParsePosition(string input)
        {
            Dimension position = new Dimension();

            string[] split = input.Split(',');
            string[] splitOne = split[0].Split('=');
            position.X = int.Parse(splitOne[1].Trim());

            string[] splitTwo = split[1].Split('=');
            position.Y = int.Parse(splitTwo[1].Trim());

            string[] splitThree = split[2].Split('=');
            string[] splitThreeNoCloseBracket = splitThree[1].Split('>');
            position.Z = int.Parse(splitThreeNoCloseBracket[0].Trim());

            return position;
        }
    }

    public class Dimension
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }
}