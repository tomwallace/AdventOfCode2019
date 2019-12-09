using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.IntCodeComputer
{
    // TODO: Pull instructions out into their own classes
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
                            InstructionThreeSaveInput(_memory[_instructionPointer]);
                            break;

                        case 4:
                            InstructionFourPublishOutput(_memory[_instructionPointer]);
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

                        case 9:
                            InstructionNineAdjustRelativeBase(_memory[_instructionPointer]);
                            break;

                        default:
                            throw new ArgumentException(
                                $"Instruction dictionary in bad state at {_instructionPointer}");
                    }
                }
            } while (!finished);

            return 1;
        }

        public long Result()
        {
            return _memory[0];
        }

        // TODO: Determine Instruction class and Interface, including how to process inputs/outputs and having variable parameters
        private void InstructionTwoMultiplication(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            long valueTwo = GetParameterValue(operationValue, 2);
            long updatedValue = valueOne * valueTwo;

            SetParameterValue(operationValue, 3, updatedValue);

            // Step forward
            _instructionPointer += 4;
        }

        private void InstructionOneAddition(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            long valueTwo = GetParameterValue(operationValue, 2);
            long updatedValue = valueOne + valueTwo;

            SetParameterValue(operationValue, 3, updatedValue);

            // Step forward
            _instructionPointer += 4;
        }

        private void InstructionThreeSaveInput(long operationValue)
        {
            if (_input != null)
            {
                SetParameterValue(operationValue, 1, _input[_inputPointer]);

                if (_inputPointer < (_input.Length - 1))
                    _inputPointer++;
            }

            // Step forward
            _instructionPointer += 2;
        }

        private void InstructionFourPublishOutput(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            _output.Add(valueOne);

            // Step forward
            _instructionPointer += 2;
        }

        private void InstructionFiveStepIfNonZero(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            long valueTwo = GetParameterValue(operationValue, 2);
            bool shouldStep = valueOne != 0;

            // Step forward
            _instructionPointer = shouldStep ? valueTwo : _instructionPointer += 3;
        }

        private void InstructionSixStepIfZero(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            long valueTwo = GetParameterValue(operationValue, 2);
            bool shouldStep = valueOne == 0;

            // Step forward
            _instructionPointer = shouldStep ? valueTwo : _instructionPointer += 3;
        }

        private void InstructionSevenStoreIfLessThan(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            long valueTwo = GetParameterValue(operationValue, 2);
            //_memory[_memory[_instructionPointer + 3]] = valueOne < valueTwo ? 1 : 0;
            SetParameterValue(operationValue, 3, valueOne < valueTwo ? 1 : 0);

            // Step forward
            _instructionPointer += 4;
        }

        private void InstructionEightStoreIfEqual(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            long valueTwo = GetParameterValue(operationValue, 2);
            //_memory[_memory[_instructionPointer + 3]] = valueOne == valueTwo ? 1 : 0;
            SetParameterValue(operationValue, 3, valueOne == valueTwo ? 1 : 0);

            // Step forward
            _instructionPointer += 4;
        }

        private void InstructionNineAdjustRelativeBase(long operationValue)
        {
            long valueOne = GetParameterValue(operationValue, 1);
            _relativeBase += valueOne;

            // Step forward
            _instructionPointer += 2;
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

        private long GetParameterValue(long operationValue, int parameterNumber)
        {
            long parameterMode = parameterNumber == 1 ? (operationValue / 100) % 10 : (operationValue / 1000) % 10;

            if (parameterMode == 1)
                return GetMemoryValue(_instructionPointer + parameterNumber);

            if (parameterMode == 2)
                return GetMemoryValue(GetMemoryValue(_instructionPointer + parameterNumber) + _relativeBase);

            return GetMemoryValue(GetMemoryValue(_instructionPointer + parameterNumber));
        }

        private long GetMemoryValue(long pointer)
        {
            if (_memory.ContainsKey(pointer))
                return _memory[pointer];

            _memory.Add(pointer, 0);
            return 0;
        }

        private void SetParameterValue(long operationValue, int parameterNumber, long valueToSet)
        {
            long parameterMode = 0;
            if (parameterNumber == 1)
                parameterMode = (operationValue / 100) % 10;
            if (parameterNumber == 2)
                parameterMode = (operationValue / 1000) % 10;
            if (parameterNumber == 3)
                parameterMode = (operationValue / 10000) % 10;

            if (parameterMode == 1)
                throw new ArgumentException("Cannot use immediate mode while setting memory location value");

            if (parameterMode == 2)
                SetMemoryValue(GetMemoryValue(_instructionPointer + parameterNumber) + _relativeBase, valueToSet);

            SetMemoryValue(GetMemoryValue(_instructionPointer + parameterNumber), valueToSet);
        }

        private void SetMemoryValue(long pointer, long valueToSet)
        {
            if (!_memory.ContainsKey(pointer))
                _memory.Add(pointer, 0);

            _memory[pointer] = valueToSet;
        }
    }
}