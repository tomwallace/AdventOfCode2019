namespace AdventOfCode2019.Console.Commands
{
    public class DefaultCommand : ICommand
    {
        public void Execute()
        {
            System.Console.WriteLine("Command not recognized.");
            System.Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}