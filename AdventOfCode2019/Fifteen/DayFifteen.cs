using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Fifteen
{
    public class DayFifteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Oxygen System [IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 15;
        }

        public string PartA()
        {
            string filePath = @"Fifteen\DayFifteenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            int bestSteps = FewestBotStepsToOxygenRepair(memoryInput).StepsTaken;
            return bestSteps.ToString();
        }

        public string PartB()
        {
            string filePath = @"Fifteen\DayFifteenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            int bestSteps = NumberOfStepsToFillOxygen(memoryInput);
            return bestSteps.ToString();
        }

        private RepairBotNode FewestBotStepsToOxygenRepair(string memoryInput)
        {
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, 0);
            int bestStepsToCompletion = int.MaxValue;
            RepairBotNode bestNode = new RepairBotNode(new Position(0, 0), 0);

            Queue<RepairBotNode> queue = new Queue<RepairBotNode>();
            RepairBotNode node = new RepairBotNode(new Position(0, 0), 0);
            node.Computer = computer;
            queue.Enqueue(node);

            Dictionary<long, Position> movements = GetMovements();
            do
            {
                RepairBotNode current = queue.Dequeue();

                foreach (var movement in movements)
                {
                    IntCodeComputer.IntCodeComputer newComputer = new IntCodeComputer.IntCodeComputer(current.Computer.CloneMemory(), movement.Key, true);
                    newComputer.ProcessInstructions();
                    long output = newComputer.GetDiagnosticCode();

                    Position newPosition = new Position(current.Position.X, current.Position.Y);
                    newPosition.Adjust(movement.Value);

                    // We got there!
                    if (output == 2 && (current.StepsTaken + 1) < bestStepsToCompletion)
                    {
                        bestStepsToCompletion = current.StepsTaken + 1;
                        RepairBotNode newNode = new RepairBotNode(newPosition, current.StepsTaken + 1);
                        newNode.LocationsVisited = current.LocationsVisited;
                        newNode.LocationsVisited.Add($"{newPosition.X},{newPosition.Y}");
                        newNode.Computer = newComputer;

                        bestNode = newNode;
                    }

                    // We did not hit a wall and we are still under our best time and we have not been there before
                    else if (output != 0 && current.StepsTaken < bestStepsToCompletion && !current.LocationsVisited.Contains($"{newPosition.X},{newPosition.Y}"))
                    {
                        // We took a step successfully, so enqueue it
                        RepairBotNode newNode = new RepairBotNode(newPosition, current.StepsTaken + 1);
                        newNode.LocationsVisited = current.LocationsVisited;
                        newNode.LocationsVisited.Add($"{newPosition.X},{newPosition.Y}");
                        newNode.Computer = newComputer;

                        queue.Enqueue(newNode);
                    }
                }
            } while (queue.Any());

            return bestNode;
        }

        private Dictionary<long, Position> GetMovements()
        {
            Dictionary<long, Position> movements = new Dictionary<long, Position>()
            {
                {1, new Position(0, 1)},
                {2, new Position(0, -1)},
                {3, new Position(-1, 0)},
                {4, new Position(1, 0)}
            };
            return movements;
        }

        private int NumberOfStepsToFillOxygen(string memoryInput)
        {
            List<int> longestTimes = new List<int>();
            Queue<RepairBotNode> queue = new Queue<RepairBotNode>();

            RepairBotNode repairNode = FewestBotStepsToOxygenRepair(memoryInput);
            repairNode.StepsTaken = 0;
            repairNode.LocationsVisited = new HashSet<string>();
            queue.Enqueue(repairNode);

            Dictionary<long, Position> movements = GetMovements();
            do
            {
                RepairBotNode current = queue.Dequeue();

                foreach (var movement in movements)
                {
                    IntCodeComputer.IntCodeComputer newComputer = new IntCodeComputer.IntCodeComputer(current.Computer.CloneMemory(), movement.Key, true);
                    newComputer.ProcessInstructions();
                    long output = newComputer.GetDiagnosticCode();

                    Position newPosition = new Position(current.Position.X, current.Position.Y);
                    newPosition.Adjust(movement.Value);

                    // We did not hit a wall and we are still under our best time and we have not been there before
                    if (output != 0 && !current.LocationsVisited.Contains($"{newPosition.X},{newPosition.Y}"))
                    {
                        // We took a step successfully, so enqueue it
                        RepairBotNode newNode = new RepairBotNode(newPosition, current.StepsTaken + 1);
                        newNode.LocationsVisited = current.LocationsVisited;
                        newNode.LocationsVisited.Add($"{newPosition.X},{newPosition.Y}");
                        newNode.Computer = newComputer;

                        queue.Enqueue(newNode);

                        longestTimes.Add(current.StepsTaken + 1);
                    }
                }
            } while (queue.Any());

            List<int> sorted = longestTimes.OrderByDescending(x => x).ToList();
            return sorted.First();
        }
    }
}