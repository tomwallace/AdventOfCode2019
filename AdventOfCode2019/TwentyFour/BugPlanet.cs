using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.TwentyFour
{
    public class BugPlanet
    {
        private int _xSize;
        private int _ySize;

        public Dictionary<int, char[,]> Map { get; set; }

        public HashSet<string> PreviousStates { get; set; }

        public BugPlanet(string filePath)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, s => s);
            _xSize = fileLines[0].Length;
            _ySize = fileLines.Count;

            char[,] map = new char[_xSize, _ySize];

            for (int y = 0; y < _ySize; y++)
            {
                char[] line = fileLines[y].ToCharArray();

                for (int x = 0; x < _xSize; x++)
                {
                    char current = line[x];
                    map[x, y] = current;
                }
            }

            Map = new Dictionary<int, char[,]>();
            // Bugs only start on original level
            Map.Add(0, map);

            PreviousStates = new HashSet<string>() { ToString() };
        }

        public bool Iterate()
        {
            // Clone map
            char[,] newMap = new char[_xSize, _ySize];

            // Run life cycle
            for (int y = 0; y < _ySize; y++)
            {
                for (int x = 0; x < _xSize; x++)
                {
                    int adjacentBugs = CountAdjacentBugs(x, y);
                    newMap[x, y] = DetermineBugLife(adjacentBugs, Map[0][x, y]);
                }
            }

            Map[0] = newMap;

            if (PreviousStates.Contains(ToString()))
                return true;

            PreviousStates.Add(ToString());

            return false;
        }

        public void IterateRecursive()
        {
            // Make sure levels above and below exist
            int min = Map.Keys.Min();
            int max = Map.Keys.Max();
            CreateMapLevel(min - 1);
            CreateMapLevel(max + 1);

            // New map
            Dictionary<int, char[,]> newMap = new Dictionary<int, char[,]>();

            foreach (int key in Map.Keys)
            {
                newMap.Add(key, new char[_xSize, _ySize]);

                // Run life cycle
                for (int y = 0; y < _ySize; y++)
                {
                    for (int x = 0; x < _xSize; x++)
                    {
                        int adjacentBugs = CountAdjacentBugsRecursive(x, y, key);
                        newMap[key][x, y] = DetermineBugLife(adjacentBugs, Map[key][x, y]);
                    }
                }
            }

            Map = newMap;
        }

        public override string ToString()
        {
            return MapString(Map[0]);
        }

        public string MapString(char[,] map)
        {
            StringBuilder complete = new StringBuilder();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                StringBuilder line = new StringBuilder();

                for (int x = 0; x < map.GetLength(0); x++)
                {
                    char current = map[x, y];
                    line.Append(current);
                }

                complete.AppendLine(line.ToString());
            }

            return complete.ToString();
        }

        public void Print()
        {
            Debug.WriteLine("");

            for (int y = 0; y < _ySize; y++)
            {
                StringBuilder line = new StringBuilder();

                for (int x = 0; x < _xSize; x++)
                {
                    char current = Map[0][x, y];
                    line.Append(current);
                }

                Debug.WriteLine(line.ToString());
            }

            Debug.WriteLine("");
        }

        public double CalculateBioDiversity()
        {
            double bioDiversity = 0;
            int counter = 0;
            for (int y = 0; y < _ySize; y++)
            {
                for (int x = 0; x < _xSize; x++)
                {
                    if (Map[0][x, y] == '#')
                        bioDiversity += PowerOfTwo(counter);

                    counter++;
                }
            }

            return bioDiversity;
        }

        public int CountBugs()
        {
            int counter = 0;
            foreach (var level in Map.Keys)
            {
                for (int y = 0; y < _ySize; y++)
                {
                    for (int x = 0; x < _xSize; x++)
                    {
                        if (Map[level][x, y] == '#')
                            counter++;
                    }
                }
            }

            return counter;
        }

        private int PowerOfTwo(int counter)
        {
            int result = 1;
            for (int i = 0; i < counter; i++)
            {
                result *= 2;
            }

            return result;
        }

        private char DetermineBugLife(int adjacentBugs, char current)
        {
            if (current == '#' && adjacentBugs != 1)
                return '.';

            if (current == '.' && (adjacentBugs == 1 || adjacentBugs == 2))
                return '#';

            return current;
        }

        private int CountAdjacentBugs(int x, int y)
        {
            int adjacentBugs = 0;

            // North
            if (y > 0 && Map[0][x, y - 1] == '#')
                adjacentBugs++;

            // East
            if (x < _xSize - 1 && Map[0][x + 1, y] == '#')
                adjacentBugs++;

            // South
            if (y < _ySize - 1 && Map[0][x, y + 1] == '#')
                adjacentBugs++;

            // West
            if (x > 0 && Map[0][x - 1, y] == '#')
                adjacentBugs++;

            return adjacentBugs;
        }

        private int CountAdjacentBugsRecursive(int x, int y, int level)
        {
            int adjacentBugs = 0;

            // Skip middle
            if (x == 2 && y == 2)
                return 0;

            // North
            if (y > 0 && Map[level][x, y - 1] == '#')
                adjacentBugs++;

            // North - level up
            if (y == 0 && Map.ContainsKey(level + 1) && Map[level + 1][2, 1] == '#')
                adjacentBugs++;

            // East
            if (x < _xSize - 1 && Map[level][x + 1, y] == '#')
                adjacentBugs++;

            // East - level up
            if (x == _xSize - 1 && Map.ContainsKey(level + 1) && Map[level + 1][3, 2] == '#')
                adjacentBugs++;

            // South
            if (y < _ySize - 1 && Map[level][x, y + 1] == '#')
                adjacentBugs++;

            // South - level up
            if (y == _ySize - 1 && Map.ContainsKey(level + 1) && Map[level + 1][2, 3] == '#')
                adjacentBugs++;

            // West
            if (x > 0 && Map[level][x - 1, y] == '#')
                adjacentBugs++;

            // West - level up
            if (x == 0 && Map.ContainsKey(level + 1) && Map[level + 1][1, 2] == '#')
                adjacentBugs++;

            // Internal - level down
            if (Map.ContainsKey(level - 1))
            {
                // North
                if (x == 2 && y == 1)
                {
                    for (int innerX = 0; innerX < 5; innerX++)
                    {
                        if (Map[level - 1][innerX, 0] == '#')
                        {
                            adjacentBugs++;
                        }
                    }
                }

                // East
                if (x == 3 && y == 2)
                {
                    for (int innerY = 0; innerY < 5; innerY++)
                    {
                        if (Map[level - 1][4, innerY] == '#')
                        {
                            adjacentBugs++;
                        }
                    }
                }

                // South
                if (x == 2 && y == 3)
                {
                    for (int innerX = 0; innerX < 5; innerX++)
                    {
                        if (Map[level - 1][innerX, 4] == '#')
                        {
                            adjacentBugs++;
                        }
                    }
                }

                // West
                if (x == 1 && y == 2)
                {
                    for (int innerY = 0; innerY < 5; innerY++)
                    {
                        if (Map[level - 1][0, innerY] == '#')
                        {
                            adjacentBugs++;
                        }
                    }
                }
            }

            return adjacentBugs;
        }

        private void CreateMapLevel(int newMapLevel)
        {
            if (Map.ContainsKey(newMapLevel))
                return;

            char[,] newLevel = new char[_xSize, _ySize];
            for (int y = 0; y < _ySize; y++)
            {
                for (int x = 0; x < _xSize; x++)
                {
                    newLevel[x, y] = '.';
                }
            }
            Map.Add(newMapLevel, newLevel);
        }
    }
}