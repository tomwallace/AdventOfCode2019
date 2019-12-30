namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public class SaveInput : InstructionBase, IInstruction
    {
        private const int IntCode = 3;

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
            if (dto.Input != null)
            {
                SetParameterValue(1, dto.Input[dto.InputPointer], dto);

                dto.InputPointer++;
            }

            // Step forward
            dto.InstructionPointer += 2;

            return dto;
        }
    }
}