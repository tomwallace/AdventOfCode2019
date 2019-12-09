namespace AdventOfCode2019.IntCodeComputer.Instructions
{
    public interface IInstruction
    {
        int GetIntCode();

        bool IsApplicable(long operationValue);

        InstructionDto Run(InstructionDto dto);
    }
}