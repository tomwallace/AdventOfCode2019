using System;

namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public class InstructionBase
    {
        internal long GetParameterValue(int parameterNumber, InstructionDto dto)
        {
            long parameterMode = parameterNumber == 1 ? (dto.OperationValue / 100) % 10 : (dto.OperationValue / 1000) % 10;

            if (parameterMode == 1)
                return GetMemoryValue(dto.InstructionPointer + parameterNumber, dto);

            if (parameterMode == 2)
                return GetMemoryValue(GetMemoryValue(dto.InstructionPointer + parameterNumber, dto) + dto.RelativeBase, dto);

            return GetMemoryValue(GetMemoryValue(dto.InstructionPointer + parameterNumber, dto), dto);
        }

        internal long GetMemoryValue(long pointer, InstructionDto dto)
        {
            if (dto.Memory.ContainsKey(pointer))
                return dto.Memory[pointer];

            dto.Memory.Add(pointer, 0);
            return 0;
        }

        internal void SetParameterValue(int parameterNumber, long valueToSet, InstructionDto dto)
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

        internal void SetMemoryValue(long pointer, long valueToSet, InstructionDto dto)
        {
            if (!dto.Memory.ContainsKey(pointer))
                dto.Memory.Add(pointer, 0);

            dto.Memory[pointer] = valueToSet;
        }
    }
}