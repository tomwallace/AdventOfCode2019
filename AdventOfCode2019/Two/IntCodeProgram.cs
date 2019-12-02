using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Two
{
    // An Intcode program is a list of integers separated by commas (like 1,0,0,3,99).
    public class IntCodeProgram
    {
        private Dictionary<int, int> _memory;
        private int _instructionPointer;

        private int _noun;
        private int _verb;

        // Construct an IntCodeProgram from a memoryInput string and noun and verb modifiers
        public IntCodeProgram(string memoryInput, int noun, int verb)
        {
            _memory = SplitInputIntoMemory(memoryInput, noun, verb);
            _noun = noun;
            _verb = verb;

            _instructionPointer = 0;
        }

        public int GetNoun()
        {
            return _noun;
        }

        public int GetVerb()
        {
            return _verb;
        }

        // Processes the instructions in memory by moving through each instruction and its parameters
        public void ProcessInstructions()
        {
            bool finished = false;
            do
            {
                switch (_memory[_instructionPointer])
                {
                    case 1:
                        InstructionOneAddition();
                        // Step forward
                        _instructionPointer += 4;
                        break;

                    case 2:
                        InstructionTwoMultiplication();
                        // Step forward
                        _instructionPointer += 4;
                        break;

                    case 99:
                        finished = true;
                        break;

                    default:
                        throw new ArgumentException($"Instruction dictionary in bad state at ${_instructionPointer}");
                }
            } while (!finished);
        }

        public int Result()
        {
            return _memory[0];
        }

        private void InstructionTwoMultiplication()
        {
            int parameterOne = _memory[_memory[_instructionPointer + 1]];
            int parameterTwo = _memory[_memory[_instructionPointer + 2]];
            int updatedValue = parameterOne * parameterTwo;

            _memory[_memory[_instructionPointer + 3]] = updatedValue;
        }

        private void InstructionOneAddition()
        {
            int parameterOne = _memory[_memory[_instructionPointer + 1]];
            int parameterTwo = _memory[_memory[_instructionPointer + 2]];
            int updatedValue = parameterOne + parameterTwo;

            _memory[_memory[_instructionPointer + 3]] = updatedValue;
        }

        private Dictionary<int, int> SplitInputIntoMemory(string memoryInput, int noun, int verb)
        {
            string[] instructions = memoryInput.Split(',').ToArray();
            Dictionary<int, int> memory = new Dictionary<int, int>();
            int address = 0;

            foreach (string instruction in instructions)
            {
                if (address == 1)
                    memory.Add(address, noun);
                else if (address == 2)
                    memory.Add(address, verb);
                else
                    memory.Add(address, int.Parse(instruction));

                address++;
            }

            return memory;
        }
    }
}