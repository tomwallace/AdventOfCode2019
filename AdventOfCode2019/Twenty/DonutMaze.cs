using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Twenty
{
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
}