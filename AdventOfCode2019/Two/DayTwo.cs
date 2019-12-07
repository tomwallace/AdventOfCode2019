using System;
using AdventOfCode2019.IntCodeComputer;

namespace AdventOfCode2019.Two
{
    public class DayTwo : IAdventProblemSet
    {
        private const string INPUT = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,1,19,9,23,1,23,13,27,1,10,27,31,2,31,13,35,1,10,35,39,2,9,39,43,2,43,9,47,1,6,47,51,1,10,51,55,2,55,13,59,1,59,10,63,2,63,13,67,2,67,9,71,1,6,71,75,2,75,9,79,1,79,5,83,2,83,13,87,1,9,87,91,1,13,91,95,1,2,95,99,1,99,6,0,99,2,14,0,0";

        public string Description()
        {
            return "1202 Program Alarm";
        }

        public int SortOrder()
        {
            return 2;
        }

        public string PartA()
        {
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(INPUT, 12, 2);
            computer.ProcessInstructions();
            return computer.Result().ToString();
        }

        public string PartB()
        {
            IntCodeComputer.IntCodeComputer correctComputer = FindIntCodeProgramThatMatches(19690720);
            int result = (100 * correctComputer.GetNoun().Value) + correctComputer.GetVerb().Value;

            return result.ToString();
        }

        public IntCodeComputer.IntCodeComputer FindIntCodeProgramThatMatches(int resultToMatch)
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(INPUT, noun, verb);
                    computer.ProcessInstructions();

                    if (computer.Result() == resultToMatch)
                        return computer;
                }
            }

            throw new ArgumentOutOfRangeException("The correct IntCodeProgram was never found");
        }
    }
}