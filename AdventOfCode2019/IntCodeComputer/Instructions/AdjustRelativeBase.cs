namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public class AdjustRelativeBase : InstructionBase, IInstruction
    {
        private const int IntCode = 9;

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
            dto.RelativeBase += valueOne;

            // Step forward
            dto.InstructionPointer += 2;

            return dto;
        }
    }
}