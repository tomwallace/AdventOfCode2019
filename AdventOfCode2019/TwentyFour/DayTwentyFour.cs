using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
            string filePath = @"Twenty\DayTwentyInput.txt";
            //int result = FindFewestStepsInMaze(filePath, true);
            return ""; //result.ToString();
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
    }

    public class BugPlanet
    {
        private int _xSize;
        private int _ySize;
        
        public Dictionary<int,char[,]> Map { get; set; }

        public int Iterations { get; set; }
        
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
            Map.Add(0,map);

            Iterations = 0;
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
                    int adjecentBugs = CountAdjacentBugs(x, y, Map[0]);
                    newMap[x, y] = DetermineBugLife(adjecentBugs, Map[0][x, y]);
                }
            }

            string newMapString = MapString(newMap);
            Map[0] = newMap;
            Iterations++;

            if (PreviousStates.Contains(ToString()))
                return true;

            PreviousStates.Add(ToString());

            return false;
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

        private int CountAdjacentBugs(int x, int y, char[,] map)
        {
            int adjacentBugs = 0;

            // North
            if (y > 0 && map[x, y - 1] == '#')
                adjacentBugs++;

            // East
            if (x < map.GetLength(0) - 1 && map[x + 1, y] == '#')
                adjacentBugs++;

            // South
            if (y < map.GetLength(1) - 1 && map[x, y + 1] == '#')
                adjacentBugs++;

            // West
            if (x > 0 && map[x - 1, y] == '#')
                adjacentBugs++;

            return adjacentBugs;
        }
    }
}