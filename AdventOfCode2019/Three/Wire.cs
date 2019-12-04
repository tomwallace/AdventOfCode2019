using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Three
{
    public class Wire
    {
        private readonly HashSet<Coordinate> _locations;

        public Wire(string input)
        {
            _locations = new HashSet<Coordinate>();
            TraceWire(input);
        }

        public HashSet<Coordinate> GetLocations()
        {
            return _locations;
        }

        public HashSet<Coordinate> FindIntersections(HashSet<Coordinate> otherWireLocations)
        {
            HashSet<Coordinate> intersections = new HashSet<Coordinate>(_locations);
            intersections.IntersectWith(otherWireLocations);
            return intersections;
        }

        private void TraceWire(string input)
        {
            int stepCounter = 0;
            Coordinate currentLocation = new Coordinate()
            {
                X = 0,
                Y = 0,
                Steps = stepCounter
            };
            _locations.Add(currentLocation);

            string[] instructions = input.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (string instruction in instructions)
            {
                string direction = instruction.Substring(0, 1);
                int distance = int.Parse(instruction.Substring(1));

                for (int d = 0; d < distance; d++)
                {
                    stepCounter++;
                    currentLocation = GetNextGridLocation(currentLocation, direction);
                    currentLocation.Steps = stepCounter;
                    _locations.Add(currentLocation);
                }
            }
        }

        private Coordinate GetNextGridLocation(Coordinate currentLocation, string direction)
        {
            Coordinate location = new Coordinate()
            {
                X = currentLocation.X,
                Y = currentLocation.Y
            };

            switch (direction)
            {
                case "U":
                    location.Y--;
                    break;

                case "R":
                    location.X++;
                    break;

                case "D":
                    location.Y++;
                    break;

                case "L":
                    location.X--;
                    break;

                default:
                    throw new ArgumentException($"Passed direction '{direction}' is not recognized.");
            }

            return location;
        }
    }
}