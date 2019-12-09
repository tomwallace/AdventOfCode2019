namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public class StoreIfEqualTo : InstructionBase, IInstruction
    {
        private const int IntCode = 8;

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
            long updatedValue = valueOne + valueTwo;

            SetParameterValue(3, valueOne == valueTwo ? 1 : 0, dto);

            // Step forward
            dto.InstructionPointer += 4;

            return dto;
        }
    }
}