namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public class Addition : InstructionBase, IInstruction
    {
        private const int IntCode = 1;

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

            SetParameterValue(3, updatedValue, dto);

            // Step forward
            dto.InstructionPointer += 4;

            return dto;
        }

        // TODO: Put in a base class, then delete
        /*
        private long GetParameterValue(int parameterNumber, InstructionDto dto)
        {
            long parameterMode = parameterNumber == 1 ? (dto.OperationValue / 100) % 10 : (dto.OperationValue / 1000) % 10;

            if (parameterMode == 1)
                return GetMemoryValue(dto.InstructionPointer + parameterNumber, dto);

            if (parameterMode == 2)
                return GetMemoryValue(GetMemoryValue(dto.InstructionPointer + parameterNumber, dto) + dto.RelativeBase, dto);

            return GetMemoryValue(GetMemoryValue(dto.InstructionPointer + parameterNumber, dto), dto);
        }

        private long GetMemoryValue(long pointer, InstructionDto dto)
        {
            if (dto.Memory.ContainsKey(pointer))
                return dto.Memory[pointer];

            dto.Memory.Add(pointer, 0);
            return 0;
        }

        private void SetParameterValue(int parameterNumber, long valueToSet, InstructionDto dto)
        {
            long parameterMode = 0;
            if (parameterNumber == 1)
                parameterMode = (dto.OperationValue / 100) % 10;
            if (parameterNumber == 2)
                parameterMode = (dto.OperationValue / 1000) % 10;
            if (parameterNumber == 3)
                parameterMode = (dto.OperationValue / 10000) % 10;

            if (parameterMode == 1)
                throw new ArgumentException("Cannot use immediate mode while setting memory location value");

            if (parameterMode == 2)
                SetMemoryValue(GetMemoryValue(dto.InstructionPointer + parameterNumber, dto) + dto.RelativeBase, valueToSet, dto);

            SetMemoryValue(GetMemoryValue(dto.InstructionPointer + parameterNumber, dto), valueToSet, dto);
        }

        private void SetMemoryValue(long pointer, long valueToSet, InstructionDto dto)
        {
            if (!dto.Memory.ContainsKey(pointer))
                dto.Memory.Add(pointer, 0);

            dto.Memory[pointer] = valueToSet;
        }
        */
    }
}