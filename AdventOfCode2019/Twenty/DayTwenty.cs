using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Twenty
{
    public class DayTwenty : IAdventProblemSet
    {
        public string Description()
        {
            return "Donut Maze [HARD]";
        }

        public int SortOrder()
        {
            return 20;
        }

        public string PartA()
        {
            string filePath = @"Twenty\DayTwentyInput.txt";
            int result = FindFewestStepsInMaze(filePath, false);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Twenty\DayTwentyInput.txt";
            int result = FindFewestStepsInMaze(filePath, true);
            return result.ToString();
        }

        public int FindFewestStepsInMaze(string filePath, bool allowMazeLevels)
        {
            DonutMaze maze = new DonutMaze(filePath, allowMazeLevels);
            int bestStepsTaken = 10000;

            Queue<DonutMazeState> queue = new Queue<DonutMazeState>();
            queue.Enqueue(new DonutMazeState(new HashSet<string>() { $"{maze.StartX},{maze.StartY}" }, maze.StartX, maze.StartY, 0));

            do
            {
                DonutMazeState current = queue.Dequeue();

                // Exit if we already have a better score
                if (current.GetNumberOfSteps() >= bestStepsTaken)
                    continue;

                // If we have reached the target, set our best score
                if (current.GetX() == maze.EndX && current.GetY() == maze.EndY && current.MazeLevel == 0)
                {
                    bestStepsTaken = current.GetNumberOfSteps();
                    continue;
                }

                // Iterate over different steps
                DonutMazeState north = new DonutMazeState(current);
                if (north.MoveMe(maze, north.GetX(), north.GetY() - 1, north.MazeLevel))
                    queue.Enqueue(north);

                DonutMazeState east = new DonutMazeState(current);
                if (east.MoveMe(maze, east.GetX() + 1, east.GetY(), east.MazeLevel))
                    queue.Enqueue(east);

                DonutMazeState south = new DonutMazeState(current);
                if (south.MoveMe(maze, south.GetX(), south.GetY() + 1, south.MazeLevel))
                    queue.Enqueue(south);

                DonutMazeState west = new DonutMazeState(current);
                if (west.MoveMe(maze, west.GetX() - 1, west.GetY(), west.MazeLevel))
                    queue.Enqueue(west);

                // Check for teleportation
                Teleporter teleporterSendPoint = maze.GetTeleporterSendPoint(current.GetX(), current.GetY(), current.MazeLevel);
                if (teleporterSendPoint != null && !current.TeleporterJustTaken)
                {
                    DonutMazeState teleporter = new DonutMazeState(current);
                    

                    if (teleporter.MoveMe(maze, teleporterSendPoint.Coord.X, teleporterSendPoint.Coord.Y, teleporter.MazeLevel))
                    {
                        if (maze.AllowMazeLevels)
                            teleporter.MazeLevel += teleporterSendPoint.ChangeMazeLevel;

                        teleporter.TeleporterJustTaken = true;

                        queue.Enqueue(teleporter);
                    }
                }
            } while (queue.Any());

            return bestStepsTaken;
        }
    }

    public class DonutMazeState
    {
        private HashSet<string> _stepsTaken;
        private int _x;
        private int _y;
        private int _numberSteps;

        public bool TeleporterJustTaken { get; set; }

        public int MazeLevel { get; set; }

        public DonutMazeState(HashSet<string> stepsTaken, int x, int y, int numberOfSteps)
        {
            _stepsTaken = stepsTaken;
            _x = x;
            _y = y;
            _numberSteps = numberOfSteps;
            TeleporterJustTaken = false;
            MazeLevel = 0;
        }

        public DonutMazeState(DonutMazeState existing)
        {
            _stepsTaken = new HashSet<string>(existing.GetStepsTaken());
            _x = existing.GetX();
            _y = existing.GetY();
            _numberSteps = existing.GetNumberOfSteps();
            TeleporterJustTaken = existing.TeleporterJustTaken;
            MazeLevel = existing.MazeLevel;
        }

        public int GetX() => _x;

        public int GetY() => _y;

        public int GetNumberOfSteps() => _numberSteps;

        public HashSet<string> GetStepsTaken() => _stepsTaken;

        public bool MoveMe(DonutMaze maze, int x, int y, int mazeLevel)
        {
            if (_stepsTaken.Contains($"{x},{y},{mazeLevel}"))
                return false;

            if (!maze.IsPassable(x, y))
                return false;

            _x = x;
            _y = y;
            _numberSteps++;
            _stepsTaken.Add($"{_x},{_y},{mazeLevel}");
            TeleporterJustTaken = false;

            return true;
        }
    }

    public class DonutMaze
    {
        public char[,] Maze { get; }

        private Dictionary<string, Teleporter> _teleporters;

        public int StartX { get; set; }

        public int StartY { get; set; }

        public int EndX { get; set; }

        public int EndY { get; set; }

        public bool AllowMazeLevels { get; set; }

        public DonutMaze(string filePath, bool allowMazeLevels)
        {
            _teleporters = new Dictionary<string, Teleporter>();
            AllowMazeLevels = allowMazeLevels;

            Dictionary<string, List<MapperDto>> mapper = new Dictionary<string, List<MapperDto>>();

            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            int xSize = fileLines[2].Length;
            int ySize = fileLines.Count;
            Maze = new char[xSize, ySize];

            for (int y = 0; y < ySize; y++)
            {
                char[] line = fileLines[y].ToCharArray();

                for (int x = 0; x < xSize; x++)
                {
                    char current = line[x];
                    if (char.IsUpper(current))
                    {
                        mapper = ProcessMapperPossibilities(fileLines, mapper, x, y);
                    }

                    Maze[x, y] = line[x];
                }
            }

            // Construct teleporters
            foreach (var entry in mapper)
            {
                _teleporters.Add(entry.Value.First().CoordString, new Teleporter(entry.Value.Last(), entry.Value.First().ChangeMazeLevel));
                _teleporters.Add(entry.Value.Last().CoordString, new Teleporter(entry.Value.First(), entry.Value.Last().ChangeMazeLevel));
            }
        }

        public bool IsPassable(int x, int y)
        {
            if (x >= Maze.GetLength(0))
                return false;

            if (y >= Maze.GetLength(1))
                return false;

            return Maze[x, y] == '.';
        }

        public Teleporter GetTeleporterSendPoint(int x, int y, int mazeLevel)
        {
            string location = $"{x},{y}";
            if (!_teleporters.ContainsKey(location))
                return null;

            Teleporter match = _teleporters[location];
            if (AllowMazeLevels && match.ChangeMazeLevel < 0 && mazeLevel == 0)
                return null;

            return _teleporters[location];
        }

        private Dictionary<string, List<MapperDto>> UpdateMapperNode(Dictionary<string, List<MapperDto>> mapper, char one, char two, int x, int y, int maxX, int maxY)
        {
            if (one == 'A' && two == 'A')
            {
                StartX = x;
                StartY = y;
                return mapper;
            }

            if (one == 'Z' && two == 'Z')
            {
                EndX = x;
                EndY = y;
                return mapper;
            }

            string teleporterKey = $"{one}{two}";
            bool isOutsideEdge = x <= 2 || x >= maxX - 3 || y <= 2 || y >= maxY - 3;
            if (mapper.ContainsKey(teleporterKey))
            {
                mapper[teleporterKey].Add(new MapperDto()
                {
                    Name = teleporterKey,
                    CoordString = $"{x},{y}",
                    ChangeMazeLevel = isOutsideEdge ? -1 : 1
                });
            }
            else
            {
                mapper.Add(teleporterKey, new List<MapperDto>() {
                    new MapperDto()
                    {
                        Name = teleporterKey,
                        CoordString = $"{x},{y}",
                        ChangeMazeLevel = isOutsideEdge ? -1 : 1
                    }
                });
            }

            return mapper;
        }

        private Dictionary<string, List<MapperDto>> ProcessMapperPossibilities(List<string> lines, Dictionary<string, List<MapperDto>> mapper, int x, int y)
        {
            // Matching teleporter location will be a '.' and one away, which we can use to derive direction
            char[] previousLine = y - 1 < 0 ? null : lines[y - 1].ToCharArray();
            char[] currentLine = lines[y].ToCharArray();
            char[] nextLine = y + 1 >= lines.Count ? null : lines[y + 1].ToCharArray();

            // Check North
            if (previousLine != null && nextLine != null && previousLine[x] == '.')
                return UpdateMapperNode(mapper, currentLine[x], nextLine[x], x, y - 1, lines[0].Length, lines.Count);

            // Check East
            if (x != 0 && x + 1 < currentLine.Length && currentLine[x + 1] == '.')
                return UpdateMapperNode(mapper, currentLine[x - 1], currentLine[x], x + 1, y, lines[0].Length, lines.Count);

            // Check South
            if (previousLine != null && nextLine != null && nextLine[x] == '.')
                return UpdateMapperNode(mapper, previousLine[x], currentLine[x], x, y + 1, lines[0].Length, lines.Count);

            // Check West
            if (x != 0 && x + 1 < currentLine.Length && currentLine[x - 1] == '.')
                return UpdateMapperNode(mapper, currentLine[x], currentLine[x + 1], x - 1, y, lines[0].Length, lines.Count);

            // Did not match, so return mapper
            return mapper;
        }
    }

    public class Teleporter
    {
        public string Name { get; set; }

        public Coord Coord { get; set; }

        public int ChangeMazeLevel { get; set; }

        public Teleporter(MapperDto dto, int changeMazeLevel)
        {
            Name = dto.Name;
            Coord = new Coord(dto.CoordString);
            ChangeMazeLevel = changeMazeLevel;
        }
    }

    // TODO: Refactor to fully use Coord class
    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord(string coords)
        {
            string[] split = coords.Split(',');
            X = int.Parse(split[0]);
            Y = int.Parse(split[1]);
        }
    }

    public class MapperDto
    {
        public string Name { get; set; }
        public string CoordString { get; set; }
        public int ChangeMazeLevel { get; set; }
    }
}