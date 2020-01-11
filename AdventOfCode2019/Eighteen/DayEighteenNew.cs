using AdventOfCode2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Eighteen
{
    // I really struggled with this Day and only was able to work through it by consulting
    // the solution found here:  https://adventofcode.com/2019
    public class DayEighteenNew : IAdventProblemSet
    {
        private int myWidth;
        private int myHeight;
        private int myMapSize;
        private int myKeyCount;
        private int myBestDistance;
        private int[] myDirections;

        private const char Entrance = '@';
        private const char Empty = '.';
        private const char Wall = '#';

        public string Description()
        {
            return "Many-Worlds Interpretation [HARD]";
        }

        public int SortOrder()
        {
            return 18;
        }

        public string PartA()
        {
            string filePath = @"Eighteen\DayEighteenInput.txt";
            int result = FindFewestStepsThroughMap(filePath).Result;

            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Eighteen\DayEighteenInput.txt";
            int result = FindStepsWithMultipleRobots(filePath).Result;

            return result.ToString();
        }

        public async Task<int> FindFewestStepsThroughMap(string filePath)
        {
            char[] map = GetMap(filePath);
            List<LocationIndex> locationIndices = WithIndex(map.ToList());
            int robotLocation = locationIndices.Where(x => x.Location == Entrance).Select(x => x.Index).First();

            map[robotLocation] = Empty;
            int fewestSteps = await Solve(map, new[] { robotLocation });

            return fewestSteps;
        }

        public async Task<int> FindStepsWithMultipleRobots(string filePath)
        {
            char[] map = GetMap(filePath);
            int[] robotPositions = ConvertEntrances(map);

            int fewestSteps = await Solve(map, robotPositions);

            return fewestSteps;
        }

        private async Task<int> Solve(char[] map, int[] robotPositions)
        {
            myBestDistance = int.MaxValue;
            var keysByChar = GetDistances(map);
            var relativeKeysByPos = keysByChar.Values.ToDictionary(k => k.Pos, v => v.RelativeKeys.Values.ToArray());
            robotPositions.ToList().ForEach(p => relativeKeysByPos.Add(p, DiscoverRelativeKeys(map, p).Values.ToArray()));

            await Backtrack(robotPositions, new List<char>(), relativeKeysByPos, new Dictionary<string, int>(), 0);

            return myBestDistance;
        }

        private Dictionary<char, Key> GetDistances(char[] map)
        {
            return map
                .Select((t, p) => new LocationIndex(t, p))
                .Where(x => IsKey(x.Location))
                .ToDictionary(k => k.Location, v => new Key(v.Location, v.Index, DiscoverRelativeKeys(map, v.Index)));
        }

        private Dictionary<char, RelativeKey> DiscoverRelativeKeys(char[] map, int startPos)
        {
            char originalKey = map[startPos];
            HashSet<int> visited = new HashSet<int>();
            Dictionary<char, RelativeKey> foundKeys = new Dictionary<char, RelativeKey>();
            Queue<MapStepState> queue = new Queue<MapStepState>();
            queue.Enqueue(new MapStepState(startPos, 0, new List<char>()));

            while (queue.Count > 0)
            {
                MapStepState current = queue.Dequeue();
                var tile = map[current.Position];
                if (tile == Wall) { continue; }

                if (IsKey(tile) && !foundKeys.ContainsKey(tile) && tile != originalKey)
                {
                    foundKeys.Add(tile, new RelativeKey(tile, current.Position, current.Distance, current.Doors.Select(d => ConvertDoorToKey(d)).ToArray()));
                }

                if (IsDoor(tile))
                {
                    List<char> newDoors = current.Doors.ToList();
                    newDoors.Add(tile);
                    current.Doors = newDoors;
                }

                foreach (var direction in myDirections)
                {
                    var nextPos = current.Position + direction;
                    if (!IsWithinBounds(nextPos) || !visited.Add(nextPos)) { continue; }
                    queue.Enqueue(new MapStepState(nextPos, current.Distance + 1, current.Doors));
                }
            }

            return foundKeys;
        }

        private async Task Backtrack(int[] robotPositions, List<char> keys, Dictionary<int, RelativeKey[]> relativeKeysByPos, Dictionary<string, int> states, int currentDistance = 0)
        {
            if (currentDistance >= myBestDistance) { return; }
            //if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(states.Count, robotPositions.Length == 1 ? 50000 : 200000); }
            if (keys.Count == myKeyCount && currentDistance < myBestDistance)
            {
                myBestDistance = currentDistance;
                return;
            }

            if (currentDistance > 0)
            {
                long posState = 0;
                foreach (var robotPosition in robotPositions)
                {
                    posState *= myMapSize;
                    posState += robotPosition;
                }
                var state = posState + string.Join("", keys.OrderBy(x => x));
                if (states.TryGetValue(state, out var storedDistance) && storedDistance <= currentDistance)
                {
                    return;
                }
                states[state] = currentDistance;
            }

            List<PositionIndex> robotIndices = WithIndex(robotPositions.ToList());
            foreach (PositionIndex robotIndex in robotIndices)
            {
                var relativeKeys = relativeKeysByPos[robotIndex.Position];
                var keysUpper = keys.Select(k => char.ToUpper(k));
                var reachableKeys = relativeKeys
                    .Where(r => !keys.Contains(r.Char) && !r.RequiredKeys.Except(keysUpper).Any())
                    .OrderBy(x => x.Distance);
                foreach (var destination in reachableKeys)
                {
                    robotPositions[robotIndex.Index] = destination.Pos;
                    keys.Add(destination.Char);
                    await Backtrack(robotPositions, keys, relativeKeysByPos, states, currentDistance + destination.Distance);
                    keys.RemoveAt(keys.Count - 1);
                }
                robotPositions[robotIndex.Index] = robotIndex.Position;
            }
        }

        private char[] GetMap(string filePath)
        {
            List<string> lines = FileUtility.ParseFileToList(filePath, line => line);
            myWidth = lines.First().Length;
            myHeight = lines.Count;
            myMapSize = myWidth * myHeight;
            myDirections = new[] { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) }.Select(GetCoord).ToArray();

            var map = lines.SelectMany(x => x).ToArray();
            myKeyCount = map.Count(IsKey);

            return map;
        }

        private int[] ConvertEntrances(char[] map)
        {
            var entrance = Array.IndexOf(map, Entrance);
            var topLeft = GetPoint(entrance + myDirections[0] + myDirections[3]);
            var bottomRight = GetPoint(entrance + myDirections[1] + myDirections[2]);
            for (var x = topLeft.X; x <= bottomRight.X; x++)
            {
                for (var y = topLeft.Y; y <= bottomRight.Y; y++)
                {
                    if (new[] { topLeft.X, bottomRight.X }.Contains(x) &&
                        new[] { topLeft.Y, bottomRight.Y }.Contains(y))
                    {
                        map[GetCoord(x, y)] = '@';
                    }
                    else
                    {
                        map[GetCoord(x, y)] = '#';
                    }
                }
            }

            List<LocationIndex> entranceIndices = WithIndex(map.ToList());
            var entrances = entranceIndices.Where(x => x.Location == Entrance).Select(x => x.Index).ToArray();
            entrances.ToList().ForEach(p => map[p] = Empty);

            return entrances;
        }

        private int GetCoord(Point point)
        {
            return GetCoord(point.X, point.Y);
        }

        private int GetCoord(int x, int y)
        {
            return x + y * myWidth;
        }

        private Point GetPoint(int coord)
        {
            return new Point(coord % myWidth, coord / myWidth);
        }

        private bool IsWithinBounds(int coord)
        {
            return coord >= 0 && coord < myMapSize;
        }

        private bool IsKey(char location)
        {
            return char.IsLower(location);
        }

        private bool IsDoor(char location)
        {
            return char.IsUpper(location);
        }

        private char ConvertDoorToKey(char door)
        {
            return char.ToUpper(door);
        }

        private List<LocationIndex> WithIndex(List<char> sequence)
        {
            List<LocationIndex> result = new List<LocationIndex>();
            for (int i = 0; i < sequence.Count(); i++)
            {
                result.Add(new LocationIndex(sequence[i], i));
            }

            return result;
        }

        private List<PositionIndex> WithIndex(List<int> sequence)
        {
            List<PositionIndex> result = new List<PositionIndex>();
            for (int i = 0; i < sequence.Count(); i++)
            {
                result.Add(new PositionIndex(sequence[i], i));
            }

            return result;
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class LocationIndex
    {
        public char Location { get; set; }
        public int Index { get; set; }

        public LocationIndex(char location, int index)
        {
            Location = location;
            Index = index;
        }
    }

    public class PositionIndex
    {
        public int Position { get; set; }
        public int Index { get; set; }

        public PositionIndex(int position, int index)
        {
            Position = position;
            Index = index;
        }
    }

    public class Key
    {
        public char Char { get; }
        public int Pos { get; }
        public Dictionary<char, RelativeKey> RelativeKeys { get; }

        public Key(char key, int pos, Dictionary<char, RelativeKey> relativeKeys)
        {
            Char = key;
            Pos = pos;
            RelativeKeys = relativeKeys;
        }
    }

    public class RelativeKey
    {
        public char Char { get; }
        public int Pos { get; }
        public int Distance { get; }
        public char[] RequiredKeys { get; }

        public RelativeKey(char key, int pos, int distance, char[] requiredKeys)
        {
            Char = key;
            Pos = pos;
            Distance = distance;
            RequiredKeys = requiredKeys;
        }
    }

    public class MapStepState
    {
        public int Position { get; set; }
        public int Distance { get; set; }
        public List<char> Doors { get; set; }

        public MapStepState(int pos, int distance, List<char> doors)
        {
            Position = pos;
            Distance = distance;
            Doors = doors;
        }
    }
}