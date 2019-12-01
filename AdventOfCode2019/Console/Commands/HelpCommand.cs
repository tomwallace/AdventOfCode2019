namespace AdventOfCode2019.Console.Commands
{
    public class HelpCommand : ICommand
    {
        public void Execute()
        {
            System.Console.WriteLine("AdventOfCode2019 offers the following commands");
            System.Console.WriteLine("");

            System.Console.WriteLine("/clear");
            System.Console.WriteLine("/c");
            System.Console.WriteLine("Clears the current console window.");
            System.Console.WriteLine("");

            System.Console.WriteLine("/help");
            System.Console.WriteLine("/h");
            System.Console.WriteLine("Displays the help menu.");
            System.Console.WriteLine("");

            System.Console.WriteLine("/list");
            System.Console.WriteLine("/l");
            System.Console.WriteLine("Lists the number of Days of puzzles in the project, along with their descriptions.");
            System.Console.WriteLine("");

            System.Console.WriteLine("/run");
            System.Console.WriteLine("/r");
            System.Console.WriteLine("Runs a problem.  Use like - /run DayOne PartA");
            System.Console.WriteLine("");

            System.Console.WriteLine("/quit");
            System.Console.WriteLine("/q");
            System.Console.WriteLine("Exits the program.");
            System.Console.WriteLine("");

            System.Console.WriteLine("/version");
            System.Console.WriteLine("/v");
            System.Console.WriteLine("Lists the version of the program.");
            System.Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}