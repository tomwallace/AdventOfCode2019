using AdventOfCode2019.IntCodeComputer.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2019.IntCodeComputer
{
    // An Intcode program is a list of integers separated by commas (like 1,0,0,3,99).
    public class IntCodeComputer
    {
        private Dictionary<long, long> _memory;
        private long _instructionPointer;

        private long[] _input;
        private long _inputPointer;

        private List<long> _output;
        private bool _pauseOnOutput;

        private long _relativeBase;

        private readonly List<IInstruction> _instructions;

        // Construct an IntCodeProgram from a memoryInput with an input
        // Takes a single input, but puts it in an array
        public IntCodeComputer(string memoryInput, long input, bool pauseOnOutput = false)
        {
            _memory = new Dictionary<long, long>();
            SplitInputIntoMemory(memoryInput, null, null);
            _input = new long[] { input };
            _instructionPointer = 0;

            _output = new List<long>();
            _pauseOnOutput = pauseOnOutput;

            _inputPointer = 0;
            _relativeBase = 0;

            _instructions = GetInstructionsReflection();
        }

        // Construct an IntCodeProgram with an existing memorySet, which is useful for "cloning" a computer
        // Takes a single input, but puts it in an array
        public IntCodeComputer(Dictionary<long, long> memory, long input, bool pauseOnOutput = false)
        {
            _memory = memory;
            _input = new long[] { input };
            _instructionPointer = 0;

            _output = new List<long>();
            _pauseOnOutput = pauseOnOutput;

            _inputPointer = 0;
            _relativeBase = 0;

            _instructions = GetInstructionsReflection();
        }

        // Construct an IntCodeProgram from a memoryInput with an array of input - but no noun or verb (needed in Day 7)
        public IntCodeComputer(string memoryInput, long[] input, bool pauseOnOutput = false)
        {
            _memory = new Dictionary<long, long>();
            SplitInputIntoMemory(memoryInput, null, null);
            _input = input;
            _instructionPointer = 0;

            _output = new List<long>();
            _pauseOnOutput = pauseOnOutput;

            _inputPointer = 0;
            _relativeBase = 0;

            _instructions = GetInstructionsReflection();
        }

        // Working with Memory
        public long GetMemoryLocation(long location)
        {
            return _memory[location];
        }

        // TODO: Remove if not necessary
        public Dictionary<long, long> CloneMemory()
        {
            return _memory.ToDictionary(m => m.Key, m => m.Value);
        }

        public void SetMemoryLocation(long location, long value)
        {
            _memory[location] = value;
        }

        // Working with Input
        public void SetInput(long[] inputs)
        {
            _input = inputs;
            _inputPointer = 0;
        }

        public void AddInput(long input)
        {
            var temp = _input.ToList();
            temp.Add(input);

            _input = temp.ToArray();
        }
        
        public void SetInputFromMultiLineAscii(string multiLine)
        {
            // Note, must end the last line with a carriage return to get to work correctly
            char[] chars = multiLine.Replace(Environment.NewLine, "" + (char)10).ToCharArray();
            List<long> list = chars.Select(c => (long)c).ToList();

            _input = list.ToArray();
            _inputPointer = 0;
        }

        // Working with Output
        public long GetDiagnosticCode()
        {
            return _output.LastOrDefault();
        }

        public List<long> GetOutput()
        {
            return _output;
        }

        public void ClearOutput()
        {
            _output = new List<long>();
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
                            // Handle need input "pause"
                            if (instruction.GetType() == typeof(SaveInput) && _inputPointer >= _input.Length)
                                return 0;
                            
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

        // Prints the current output, translating each character into ASCII
        public void PrintAsciiOutput()
        {
            Debug.WriteLine("Printing Output ---------------------------");
            Debug.WriteLine("");

            foreach (long unicode in _output)
            {
                char character = (char)unicode;
                Debug.Write(character.ToString());
            }

            Debug.WriteLine("");
            Debug.WriteLine("End Output ---------------------------");
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