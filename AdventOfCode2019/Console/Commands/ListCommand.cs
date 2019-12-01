using System;
using System.Linq;

namespace AdventOfCode2019.Console.Commands
{
    public class ListCommand : ICommand
    {
        public void Execute()
        {
            var interfaceType = typeof(IAdventProblemSet);
            var problemSetTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsInterface == false)
                .OrderBy(problemSetType =>
                {
                    IAdventProblemSet instance = (IAdventProblemSet)Activator.CreateInstance(problemSetType, null);
                    return instance.SortOrder();
                });

            System.Console.WriteLine("AdventOfCode2019 has the following problem sets:");
            System.Console.WriteLine("");

            System.Console.WriteLine("Class Name  -  Description");
            System.Console.WriteLine("-----------------------------------------------------");

            foreach (var problemSetType in problemSetTypes)
            {
                IAdventProblemSet instance = (IAdventProblemSet)Activator.CreateInstance(problemSetType, null);
                System.Console.WriteLine($"{problemSetType.Name}  -  {instance.Description()}");
            }

            System.Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}