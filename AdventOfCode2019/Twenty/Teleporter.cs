namespace AdventOfCode2019.Twenty
{
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
}