using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2019.Console.Commands
{
    public class RunCommand : ICommand
    {
        private readonly string _className;
        private readonly string _methodName;
        private readonly bool _errorInCreation;

        private readonly List<string> _allowedMethodNames = new List<string>() { "PartA", "PartB" };

        public RunCommand(string[] commandSplit)
        {
            if (commandSplit.Length != 3)
            {
                _errorInCreation = true;
            }
            else
            {
                _className = commandSplit[1];
                _methodName = commandSplit[2];

                if (!_className.Contains("Day") || !_allowedMethodNames.Contains(_methodName))
                    _errorInCreation = true;
            }
        }

        public void Execute()
        {
            var interfaceType = typeof(IAdventProblemSet);
            var problemSetTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsInterface == false);
            var problemSetType = problemSetTypes.FirstOrDefault(t => t.Name == _className);

            if (problemSetType == null)
            {
                System.Console.WriteLine("The problem set was not found.");
                System.Console.WriteLine("Have you entered a Day that has not occurred yet?");
                System.Console.WriteLine("");
                return;
            }

            var instance = Activator.CreateInstance(problemSetType);
            MethodInfo method = problemSetType.GetMethod(_methodName);
            string output = (string)method.Invoke(instance, new object[0]);

            System.Console.WriteLine($"The result is: {output}");
            System.Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return _errorInCreation;
        }
    }
}