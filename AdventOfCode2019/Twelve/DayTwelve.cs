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

        public Dimension GetVelocity()
        {
            return _velocity;
        }

        public void Move(long stepNumber)
        {
            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
            _position.Z += _velocity.Z;

            if (_firstStepDuplicated == -1 && _previousStates.ContainsKey(ToString()))
                _firstStepDuplicated = stepNumber;

            if (_firstStepDuplicated == -1)
                _previousStates.Add(ToString(), stepNumber);
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