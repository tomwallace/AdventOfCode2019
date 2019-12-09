using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.IntCodeComputer
{
    // TODO: Pull instructions out into their own classes
    // An Intcode program is a list of integers separated by commas (like 1,0,0,3,99).
    public class IntCodeComputer
    {
        private Dictionary<int, int> _memory;
        private int _instructionPointer;

        private int? _noun;
        private int? _verb;

        private int[] _input;
        private int _inputPointer;

        private int _output;
        private bool _pauseOnOutput;

        // Construct an IntCodeProgram from a memoryInput string and noun and verb modifiers (needed in Day 2)
        public IntCodeComputer(string memoryInput, int? noun, int? verb)
        {
            _memory = new Dictionary<int, int>();
            SplitInputIntoMemory(memoryInput, noun, verb);
            _noun = noun;
            _verb = verb;
            _input = null;
            _instructionPointer = 0;

            _output = 0;
            _pauseOnOutput = false;

            _instructionPointer = 0;
        }

        // Construct an IntCodeProgram from a memoryInput with an input - but no noun or verb (needed in Day 5)
        // Takes a single input, but puts it in an array
        public IntCodeComputer(string memoryInput, int input)
        {
            _memory = new Dictionary<int, int>();
            SplitInputIntoMemory(memoryInput, null, null);
            _noun = null;
            _verb = null;
            _input = new int[] { input };
            _instructionPointer = 0;

            _output = 0;
            _pauseOnOutput = false;

            _instructionPointer = 0;
        }

        // Construct an IntCodeProgram from a memoryInput with an array of input - but no noun or verb
        public IntCodeComputer(string memoryInput, int[] input, bool pauseOnOutput = false)
        {
            _memory = new Dictionary<int, int>();
            SplitInputIntoMemory(memoryInput, null, null);
            _noun = null;
            _verb = null;
            _input = input;
            _instructionPointer = 0;

            _output = 0;
            _pauseOnOutput = pauseOnOutput;

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
            return _output;
        }

        public void SetInput(int[] inputs)
        {
            _input = inputs;
            _inputPointer = 0;
        }

        // Processes the instructions in memory by moving through each instruction and its parameters
        public int ProcessInstructions()
        {
            bool finished = false;
            do
            {
                int currentValue = _memory[_instructionPointer];
                if (currentValue == 99)
                {
                    finished = true;
                }
                else
                {
                    // Work with just the first two digits of the current value
                    switch (_memory[_instructionPointer] % 10)
                    {
                        case 1:
                            InstructionOneAddition(_memory[_instructionPointer]);
                            break;

                        case 2:
                            InstructionTwoMultiplication(_memory[_instructionPointer]);
                            break;

                        case 3:
                            InstructionThreeSaveInput();
                            break;

                        case 4:
                            InstructionFourPublishOutput();
                            if (_pauseOnOutput)
                            {
                                finished = true;
                                return 0;
                            }
                            break;

                        case 5:
                            InstructionFiveStepIfNonZero(_memory[_instructionPointer]);
                            break;

                        case 6:
                            InstructionSixStepIfZero(_memory[_instructionPointer]);
                            break;

                        case 7:
                            InstructionSevenStoreIfLessThan(_memory[_instructionPointer]);
                            break;

                        case 8:
                            InstructionEightStoreIfEqual(_memory[_instructionPointer]);
                            break;

                        default:
                            throw new ArgumentException(
                                $"Instruction dictionary in bad state at {_instructionPointer}");
                    }
                }
            } while (!finished);

            return 1;
        }

        public int Result()
        {
            return _memory[0];
        }

        // TODO: Determine Instruction class and Interface, including how to process inputs/outputs and having variable parameters
        private void InstructionTwoMultiplication(int operationValue)
        {
            int parameterOneMode = (operationValue / 100) % 10;
            int parameterTwoMode = (operationValue / 1000) % 10;

            int valueOne = parameterOneMode == 1
                ? _memory[_instructionPointer + 1]
                : _memory[_memory[_instructionPointer + 1]];
            int valueTwo = parameterTwoMode == 1
                ? _memory[_instructionPointer + 2]
                : _memory[_memory[_instructionPointer + 2]];

            int updatedValue = valueOne * valueTwo;

            _memory[_memory[_instructionPointer + 3]] = updatedValue;

            // Step forward
            _instructionPointer += 4;
        }

        private void InstructionOneAddition(int operationValue)
        {
            int parameterOneMode = (operationValue / 100) % 10;
            int parameterTwoMode = (operationValue / 1000) % 10;

            int valueOne = parameterOneMode == 1
                ? _memory[_instructionPointer + 1]
                : _memory[_memory[_instructionPointer + 1]];
            int valueTwo = parameterTwoMode == 1
                ? _memory[_instructionPointer + 2]
                : _memory[_memory[_instructionPointer + 2]];

            int updatedValue = valueOne + valueTwo;

            _memory[_memory[_instructionPointer + 3]] = updatedValue;

            // Step forward
            _instructionPointer += 4;
        }

        private void InstructionThreeSaveInput()
        {
            if (_input != null)
            {
                _memory[_memory[_instructionPointer + 1]] = _input[_inputPointer];
                if (_inputPointer < (_input.Length - 1))
                    _inputPointer++;
            }

            // Step forward
            _instructionPointer += 2;
        }

        private void InstructionFourPublishOutput()
        {
            _output = _memory[_memory[_instructionPointer + 1]];

            // Step forward
            _instructionPointer += 2;
        }

        private void InstructionFiveStepIfNonZero(int operationValue)
        {
            int parameterOneMode = (operationValue / 100) % 10;
            int parameterTwoMode = (operationValue / 1000) % 10;

            int valueOne = parameterOneMode == 1
                ? _memory[_instructionPointer + 1]
                : _memory[_memory[_instructionPointer + 1]];
            int valueTwo = parameterTwoMode == 1
                ? _memory[_instructionPointer + 2]
                : _memory[_memory[_instructionPointer + 2]];

            bool shouldStep = valueOne != 0;

            // Step forward
            _instructionPointer = shouldStep ? valueTwo : _instructionPointer += 3;
        }

        private void InstructionSixStepIfZero(int operationValue)
        {
            int parameterOneMode = (operationValue / 100) % 10;
            int parameterTwoMode = (operationValue / 1000) % 10;

            int valueOne = parameterOneMode == 1
                ? _memory[_instructionPointer + 1]
                : _memory[_memory[_instructionPointer + 1]];
            int valueTwo = parameterTwoMode == 1
                ? _memory[_instructionPointer + 2]
                : _memory[_memory[_instructionPointer + 2]];

            bool shouldStep = valueOne == 0;

            // Step forward
            _instructionPointer = shouldStep ? valueTwo : _instructionPointer += 3;
        }

        private void InstructionSevenStoreIfLessThan(int operationValue)
        {
            int parameterOneMode = (operationValue / 100) % 10;
            int parameterTwoMode = (operationValue / 1000) % 10;

            int valueOne = parameterOneMode == 1
                ? _memory[_instructionPointer + 1]
                : _memory[_memory[_instructionPointer + 1]];
            int valueTwo = parameterTwoMode == 1
                ? _memory[_instructionPointer + 2]
                : _memory[_memory[_instructionPointer + 2]];

            _memory[_memory[_instructionPointer + 3]] = valueOne < valueTwo ? 1 : 0;

            // Step forward
            _instructionPointer += 4;
        }

        private void InstructionEightStoreIfEqual(int operationValue)
        {
            int parameterOneMode = (operationValue / 100) % 10;
            int parameterTwoMode = (operationValue / 1000) % 10;

            int valueOne = parameterOneMode == 1
                ? _memory[_instructionPointer + 1]
                : _memory[_memory[_instructionPointer + 1]];
            int valueTwo = parameterTwoMode == 1
                ? _memory[_instructionPointer + 2]
                : _memory[_memory[_instructionPointer + 2]];

            _memory[_memory[_instructionPointer + 3]] = valueOne == valueTwo ? 1 : 0;

            // Step forward
            _instructionPointer += 4;
        }

        private void SplitInputIntoMemory(string memoryInput, int? noun, int? verb)
        {
            string[] instructions = memoryInput.Split(',').ToArray();
            List<int> parameterModes = new List<int>();
            int address = 0;

            // Set up the memory
            foreach (string instruction in instructions)
            {
                if (noun != null && address == 1)
                    _memory.Add(address, noun.Value);
                else if (verb != null && address == 2)
                    _memory.Add(address, verb.Value);
                else
                    _memory.Add(address, int.Parse(instruction));

                address++;
            }
        }
    }
}