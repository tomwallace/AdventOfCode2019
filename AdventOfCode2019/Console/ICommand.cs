namespace AdventOfCode2019.Console
{
    public interface ICommand
    {
        void Execute();

        bool HadErrorInCreation();
    }
}