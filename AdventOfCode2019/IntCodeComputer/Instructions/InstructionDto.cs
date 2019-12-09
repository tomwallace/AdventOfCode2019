using System.Collections.Generic;

namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public class InstructionDto
    {
        public Dictionary<long, long> Memory { get; set; }
        public long InstructionPointer { get; set; }
        public long RelativeBase { get; set; }
        public long OperationValue { get; set; }
        public long[] Input { get; set; }
        public long InputPointer { get; set; }
        public List<long> Output { get; set; }
    }
}