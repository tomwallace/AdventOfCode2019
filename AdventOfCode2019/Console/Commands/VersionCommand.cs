using System.Configuration;

namespace AdventOfCode2019.Console.Commands
{
    public class VersionCommand : ICommand
    {
        public void Execute()
        {
            string versionNumber = ConfigurationManager.AppSettings["version"];
            System.Console.WriteLine($"AdventOfCode2019 version: {versionNumber}"); 
            System.Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}