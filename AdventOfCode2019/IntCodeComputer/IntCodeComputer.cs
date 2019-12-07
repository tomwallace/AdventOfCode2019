using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.IntCodeComputer
{
    // TODO: Refactor this class, around references to "pointed values"
    // TODO: Pull instructions out into their own classes
    // TODO: Write tests for DayFive
    // An Intcode program is a list of integers separated by commas (like 1,0,0,3,99).
    public class IntCodeComputer
    {
        private Dictionary<int, Instruction> _memory;
        private int _instructionPointer;

        private int? _noun;
        private int? _verb;

        private int? _input;
        private List<int> _output;

        // Construct an IntCodeProgram from a memoryInput string and noun and verb modifiers
        public IntCodeComputer(string memoryInput, int? noun, int? verb)
        {
            _memory = new Dictionary<int, Instruction>();
            SplitInputIntoMemory(memoryInput, noun, verb);
            _noun = noun;
            _verb = verb;
            _input = null;

            _output = new List<int>();

            _instructionPointer = 0;
        }

        // Construct an IntCodeProgram from a memoryInput with an input - but no noun or verb
        public IntCodeComputer(string memoryInput, int? input)
        {
            _memory = new Dictionary<int, Instruction>();
            SplitInputIntoMemory(memoryInput, null, null);
            _noun = null;
            _verb = null;
            _input = input;

            _output = new List<int>();

            _instructionPointer = 0;
        }

        public int? GetNoun()
        {
            return _noun;
        }

        public int? GetVerb()
        {
            return _verb;
        }

        public int GetDiagnosticCode()
        {
            return _output.Last();
        }

        // Processes the instructions in memory by moving through each instruction and its parameters
        public void ProcessInstructions()
        {
            bool finished = false;
            do
            {
                switch (_memory[_instructionPointer].GetRawValue())
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

                    case 3:
                        InstructionThreeSaveInput();
                        // Step forward
                        _instructionPointer += 2;
                        break;

                    case 4:
                        InstructionFourPublishOutput();
                        // Step forward
                        _instructionPointer += 2;
                        break;

                    case 99:
                        finished = true;
                        break;

                    default:
                        _output.Add(_memory[_instructionPointer].GetRawValue());
                        _instructionPointer++;
                        break;
                        //throw new ArgumentException($"Instruction dictionary in bad state at ${_instructionPointer}");
                }
            } while (!finished);
        }

        public int Result()
        {
            return _memory[0].GetRawValue();
        }

        // TODO: Determine Instruction class and Interface, including how to process inputs/outputs and having variable parameters
        private void InstructionTwoMultiplication()
        {
            Instruction parameterOne = _memory[_instructionPointer + 1];
            Instruction parameterTwo = _memory[_instructionPointer + 2];
            int valueOne = parameterOne.GetValue(_memory[parameterOne.GetRawValue()]);
            int valueTwo = parameterTwo.GetValue(_memory[parameterTwo.GetRawValue()]);

            int updatedValue = valueOne * valueTwo;

            _memory[_memory[_instructionPointer + 3].GetRawValue()].SetValue(updatedValue);
        }

        private void InstructionOneAddition()
        {
            Instruction parameterOne = _memory[_instructionPointer + 1];
            Instruction parameterTwo = _memory[_instructionPointer + 2];
            int valueOne = parameterOne.GetValue(_memory[parameterOne.GetRawValue()]);
            int valueTwo = parameterTwo.GetValue(_memory[parameterTwo.GetRawValue()]);

            int updatedValue = valueOne + valueTwo;

            _memory[_memory[_instructionPointer + 3].GetRawValue()].SetValue(updatedValue);
        }

        private void InstructionThreeSaveInput()
        {
            if (_input != null)
            {
                Instruction parameterOne = _memory[_instructionPointer + 1];
                int valueOne = parameterOne.GetValue(_memory[parameterOne.GetRawValue()]);
                _memory[valueOne].SetValue(_input.Value);
            }
        }

        private void InstructionFourPublishOutput()
        {
            Instruction parameterOne = _memory[_instructionPointer + 1];
            int valueOne = parameterOne.GetValue(_memory[parameterOne.GetRawValue()]);
            _output.Add(valueOne);
        }

        private void SplitInputIntoMemory(string memoryInput, int? noun, int? verb)
        {
            string[] instructions = memoryInput.Split(',').ToArray();
            List<int> parameterModes = new List<int>();
            int address = 0;

            // Set up the memory
            foreach (string instruction in instructions)
            {
                if (address == 0)
                    parameterModes = ProcessParameterModes(instruction);
                else if (noun != null && address == 1)
                    _memory.Add(address, new Instruction(noun.Value));
                else if (verb != null && address == 2)
                    _memory.Add(address, new Instruction(verb.Value));
                else
                    _memory.Add(address, new Instruction(int.Parse(instruction)));

                address++;
            }

            // Add parameter modes to memory instructions
            for (int p = 1; p <= parameterModes.Count; p++)
            {
                _memory[p].ParameterMode = parameterModes[p - 1];
            }
        }

        private List<int> ProcessParameterModes(string instruction)
        {
            List<int> parameterModes = new List<int>();
            
            // It does not have Parameter Modes
            if (instruction.Length <= 2)
            {
                // Memory processes as normal
                _memory.Add(0, new Instruction(int.Parse(instruction)));
                return parameterModes;
            }
                
            // Otherwise, we need to handle parameter modes
            char[] digits = instruction.ToCharArray();
            int digitLength = digits.Length;

            int firstInstruction = int.Parse($"{digits[digitLength - 2]}{digits[digitLength - 1]}");
            _memory.Add(0, new Instruction(firstInstruction));

            // Handle parameter modes
            for (int i = digitLength - 3; i <= 0; i--)
            {
                parameterModes.Add(int.Parse($"{digits[i]}"));
            }

            return parameterModes;
        }
    }

    public class Instruction
    {
        public int ParameterMode { get; set; }
        private int _value;

        public Instruction(int value)
        {
            _value = value;
            ParameterMode = 0;
        }

        public int GetValue(Instruction pointedTo)
        {
            if (ParameterMode == 1)
                return GetRawValue();

            return pointedTo.GetRawValue();
        }

        public int GetRawValue()
        {
            return _value;
        }

        public void SetValue(int value)
        {
            _value = value;
        }
    }
}