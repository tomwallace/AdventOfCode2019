using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Eighteen
{
    public class DayEighteen : IAdventProblemSet
    {
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
            int result = FindFewestStepsThroughMap(filePath);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Ten\DayTenInput.txt";
            //int destroyed = RunAsteroidRoutine(filePath, 200);

            return ""; //destroyed.ToString();
        }

        public int FindFewestStepsThroughMap(string filePath)
        {
            Queue<Vault> queue = new Queue<Vault>();
            queue.Enqueue(new Vault(filePath));

            int bestStepsTaken = int.MaxValue;

            do
            {
                Vault current = queue.Dequeue();

                if (current.Steps >= bestStepsTaken)
                    continue;

                if (current.KeysRemaining() == 0)
                {
                    bestStepsTaken = current.Steps;
                    continue;
                }
                
                Vault north = new Vault(current);
                if (north.MoveMe(0, 1))
                    queue.Enqueue(north);

                Vault east = new Vault(current);
                if (east.MoveMe(1, 0))
                    queue.Enqueue(east);

                Vault south = new Vault(current);
                if (south.MoveMe(0, -1))
                    queue.Enqueue(south);

                Vault west = new Vault(current);
                if (west.MoveMe(-1, 0))
                    queue.Enqueue(west);

            } while (queue.Any());

            return bestStepsTaken;
        }
    }

    public class Vault
    {
        public MapCell[,] Map { get; set; }
        public MapCell Me { get; set; }
        public HashSet<string> StepsVisited { get; set; }

        public int Steps { get; set; }

        public HashSet<char> KeysFound { get; set; }

        public Vault(string filePath)
        {
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            StepsVisited = new HashSet<string>();

            int xCount = fileLines[0].Length;
            int yCount = fileLines.Count;
            MapCell[,] map = new MapCell[xCount, yCount];

            for (int y = 0; y < yCount; y++)
            {
                string currentLine = fileLines[y];
                char[] chars = currentLine.ToCharArray();

                for (int x = 0; x < xCount; x++)
                {
                    map[x, y] = new MapCell(chars[x], x, y);
                    if (chars[x] == '@')
                    {
                        Me = new MapCell(chars[x], x, y);
                        StepsVisited.Add($"{x},{y}");
                    }
                        
                }
            }

            Map = map;
            Steps = 0;
            KeysFound = new HashSet<char>();
        }

        public Vault(Vault existing)
        {
            Map = CloneMap(existing);
            Me = new MapCell(existing.Me);
            Steps = existing.Steps;
            KeysFound = new HashSet<char>(existing.KeysFound);
            StepsVisited = new HashSet<string>(existing.StepsVisited);
        }

        private MapCell[,] CloneMap(Vault existing)
        {
            int xCount = existing.Map.GetLength(0);
            int yCount = existing.Map.GetLength(1);
            MapCell[,] map = new MapCell[xCount, yCount];

            for (int y = 0; y < yCount; y++)
            {
                for (int x = 0; x < xCount; x++)
                {
                    map[x, y] = new MapCell(existing.Map[x,y]);
                }
            }

            return map;
        }

        public int KeysRemaining()
        {
            int keysRemaining = 0;
            int xCount = Map.GetLength(0);
            int yCount = Map.GetLength(1);

            for (int y = 0; y < yCount; y++)
            {
                for (int x = 0; x < xCount; x++)
                {
                    if (Map[x, y].IsKey())
                    {
                        keysRemaining++;
                    }
                }
            }

            return keysRemaining;
        }

        public bool MoveMe(int x, int y)
        {
            if (StepsVisited.Contains($"{Me.X + x},{Me.Y + y}"))
                return false;

            if (!Map[Me.X + x, Me.Y + y].IsPassable(KeysFound))
                return false;
            
            Me = Map[Me.X + x, Me.Y + y];
            char? keyFound = Me.KeyFound();
            if (keyFound != null)
            {
                KeysFound.Add(keyFound.Value);
                // Need to reset places been
                StepsVisited = new HashSet<string>();
            }

            if (Me.UnlockIfPossible(KeysFound))
            {
                // Need to reset places been
                StepsVisited = new HashSet<string>();
            }

            Steps++;

            StepsVisited.Add($"{Me.X},{Me.Y}");

            return true;
        }
    }

    public class MapCell
    {
        private char[] _nonDoorCharacters = new[] { '.', '#', '@' };

        public char Value;
        public int X;
        public int Y;

        public MapCell(char input, int x, int y)
        {
            Value = input;
            X = x;
            Y = y;
        }

        public MapCell(MapCell other)
        {
            Value = other.Value;
            X = other.X;
            Y = other.Y;
        }

        public bool IsDoor()
        {
            return char.IsUpper(Value);
        }

        public bool IsKey()
        {
            return char.IsLower(Value);
        }

        public bool IsPassable(HashSet<char> keys)
        {
            if (Value == '#')
                return false;

            if (Value == '.')
                return true;

            // If a door, check for key
            if (IsDoor())
            {
                return keys.Any(k => k.ToString().ToUpper() == Value.ToString());
            }

            // Otherwise, it is a key, so passable
            return true;
        }

        public char? KeyFound()
        {
            if (IsKey())
            {
                char value = Value;
                Value = '.';
                return value;
            }

            return null;
        }

        public bool UnlockIfPossible(HashSet<char> keys)
        {
            bool isPossible = keys.Any(k => k.ToString().ToUpper() == Value.ToString());
            if (isPossible)
                Value = '.';

            return isPossible;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}