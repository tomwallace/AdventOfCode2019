namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public class StepIfZero : InstructionBase, IInstruction
    {
        private const int IntCode = 6;

        public int GetIntCode()
        {
            return IntCode;
        }

        public bool IsApplicable(long operationValue)
        {
            return operationValue % 10 == IntCode;
        }

        public InstructionDto Run(InstructionDto dto)
        {
            long valueOne = GetParameterValue(1, dto);
            long valueTwo = GetParameterValue(2, dto);
            bool shouldStep = valueOne == 0;

            // Step forward
            dto.InstructionPointer = shouldStep ? valueTwo : dto.InstructionPointer += 3;

            return dto;
        }
    }
}