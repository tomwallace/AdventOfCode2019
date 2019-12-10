using AdventOfCode2019.IntCodeComputer.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.IntCodeComputer
{
    // An Intcode program is a list of integers separated by commas (like 1,0,0,3,99).
    public class IntCodeComputer
    {
        private Dictionary<long, long> _memory;
        private long _instructionPointer;

        private long? _noun;
        private long? _verb;

        private long[] _input;
        private long _inputPointer;

        private List<long> _output;
        private bool _pauseOnOutput;

        private long _relativeBase;

        private readonly List<IInstruction> _instructions;

        // Construct an IntCodeProgram from a memoryInput string and noun and verb modifiers (needed in Day 2)
        public IntCodeComputer(string memoryInput, long? noun, long? verb)
        {
            _memory = new Dictionary<long, long>();
            SplitInputIntoMemory(memoryInput, noun, verb);
            _noun = noun;
            _verb = verb;
            _input = null;
            _instructionPointer = 0;

            _output = new List<long>();
            _pauseOnOutput = false;

            _inputPointer = 0;
            _relativeBase = 0;

            _instructions = GetInstructionsReflection();
        }

        // Construct an IntCodeProgram from a memoryInput with an input - but no noun or verb (needed in Days 5 and 9)
        // Takes a single input, but puts it in an array
        public IntCodeComputer(string memoryInput, long input)
        {
            _memory = new Dictionary<long, long>();
            SplitInputIntoMemory(memoryInput, null, null);
            _noun = null;
            _verb = null;
            _input = new long[] { input };
            _instructionPointer = 0;

            _output = new List<long>();
            _pauseOnOutput = false;

            _inputPointer = 0;
            _relativeBase = 0;

            _instructions = GetInstructionsReflection();
        }

        // Construct an IntCodeProgram from a memoryInput with an array of input - but no noun or verb (needed in Day 7)
        public IntCodeComputer(string memoryInput, long[] input, bool pauseOnOutput = false)
        {
            _memory = new Dictionary<long, long>();
            SplitInputIntoMemory(memoryInput, null, null);
            _noun = null;
            _verb = null;
            _input = input;
            _instructionPointer = 0;

            _output = new List<long>();
            _pauseOnOutput = pauseOnOutput;

            _inputPointer = 0;
            _relativeBase = 0;

            _instructions = GetInstructionsReflection();
        }

        public long? GetNoun()
        {
            return _noun;
        }

        public long? GetVerb()
        {
            return _verb;
        }

        public long GetDiagnosticCode()
        {
            return _output.LastOrDefault();
        }

        public void SetInput(long[] inputs)
        {
            _input = inputs;
            _inputPointer = 0;
        }

        public long GetMemorySlotOne()
        {
            return _memory[0];
        }

        // Processes the instructions in memory by moving through each instruction and its parameters
        public int ProcessInstructions()
        {
            bool finished = false;
            do
            {
                long currentValue = _memory[_instructionPointer];
                if (currentValue == 99)
                {
                    finished = true;
                }
                else
                {
                    foreach (IInstruction instruction in _instructions)
                    {
                        long operationValue = _memory[_instructionPointer];
                        if (instruction.IsApplicable(operationValue))
                        {
                            InstructionDto dto = MapInstructionDto(operationValue);
                            InstructionDto updatedDto = instruction.Run(dto);
                            SetValuesFromDto(updatedDto);

                            // Handle output "pause"
                            if (_pauseOnOutput && instruction.GetType() == typeof(PublishOutput))
                            {
                                return 0;
                            }

                            break;
                        }
                    }
                }
            } while (!finished);

            return 1;
        }

        private InstructionDto MapInstructionDto(long operationValue)
        {
            return new InstructionDto()
            {
                Memory = _memory,
                InstructionPointer = _instructionPointer,
                InputPointer = _inputPointer,
                Input = _input,
                OperationValue = operationValue,
                Output = _output,
                RelativeBase = _relativeBase
            };
        }

        private void SetValuesFromDto(InstructionDto dto)
        {
            _memory = dto.Memory;
            _instructionPointer = dto.InstructionPointer;
            _inputPointer = dto.InputPointer;
            _input = dto.Input;
            _output = dto.Output;
            _relativeBase = dto.RelativeBase;
        }

        // Collect list of instructions using reflection
        private List<IInstruction> GetInstructionsReflection()
        {
            var interfaceType = typeof(IInstruction);
            List<IInstruction> instructions = System.Reflection.Assembly.GetAssembly(interfaceType).GetTypes()
                .Where(a => interfaceType.IsAssignableFrom(a) && a.IsInterface == false)
                .Select(t => (IInstruction)Activator.CreateInstance(t, null)).ToList();

            return instructions;
        }

        private void SplitInputIntoMemory(string memoryInput, long? noun, long? verb)
        {
            string[] instructions = memoryInput.Split(',').ToArray();
            List<long> parameterModes = new List<long>();
            long address = 0;

            // Set up the memory
            foreach (string instruction in instructions)
            {
                if (noun != null && address == 1)
                    _memory.Add(address, noun.Value);
                else if (verb != null && address == 2)
                    _memory.Add(address, verb.Value);
                else
                    _memory.Add(address, long.Parse(instruction));

                address++;
            }
        }
    }
}