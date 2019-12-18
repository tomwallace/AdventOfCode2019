using AdventOfCode2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Fourteen
{
    public class DayFourteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Space Stoichiometry [HARD]";
        }

        public int SortOrder()
        {
            return 14;
        }

        public string PartA()
        {
            string filePath = @"Twelve\DayTwelveInput.txt";
            //int calculation = CalculateTotalSystemEnergy(filePath, 1000);

            return ""; //calculation.ToString();
        }

        public string PartB()
        {
            string filePath = @"Ten\DayTenInput.txt";
            //int destroyed = RunAsteroidRoutine(filePath, 200);

            return ""; //destroyed.ToString();
        }

        // TODO: Cannot wrap my mind around the steps from one to the other, as we have to "round up"
        public int CalculateOreRequired(string filePath)
        {
            Dictionary<string, Reaction> reactionCatalog = GetReactions(filePath);
            Reaction fuel = reactionCatalog["FUEL"];

            int result = RecurseThroughTree(fuel, reactionCatalog);

            return result;
            /*
            int totalAmountsOre = 0;

            Queue<ReactionStep> queue = new Queue<ReactionStep>();
            queue.Enqueue(new ReactionStep() { Steps = 1, Reaction = reactionCatalog["FUEL"] });

            do
            {
                ReactionStep current = queue.Dequeue();

                foreach (var reaction in current.Reaction.Outputs)
                {
                    if (reaction.Key == "ORE")
                        totalAmountsOre += current.Steps * reaction.Value.Amount;
                    else
                    {
                        ReactionStep newStep = new ReactionStep();
                        newStep.Steps = current.Steps * reaction.Value.Amount;
                        newStep.Reaction = reactionCatalog[reaction.Key];

                        queue.Enqueue(newStep);
                    }
                }

            } while (queue.Any());

            return totalAmountsOre;
            */
        }
        // current = fuel, amount = 1
        public int RecurseThroughTree(Reaction current, Dictionary<string, Reaction> reactionCatalog)
        {
            if (current.Outputs.Count == 0)
                return current.Amount;

            int runningTotal = 0;
            // CA, 4, BC 3, AB 2
            foreach (var child in current.Outputs)
            {
                if (!reactionCatalog.ContainsKey(child.Key))
                    return child.Value.Amount;

                var childCatalogEntry = reactionCatalog[child.Key];
                decimal divided = child.Value.Amount / childCatalogEntry.Amount;
                int numberOfTimesToRun = decimal.ToInt32(Math.Ceiling(divided));

                for (int i = 0; i < numberOfTimesToRun; i++)
                    runningTotal += RecurseThroughTree(childCatalogEntry, reactionCatalog);
            }

            return runningTotal;
        }

        private Dictionary<string, Reaction> GetReactions(string filePath)
        {
            Dictionary<string, Reaction> reactions = new Dictionary<string, Reaction>();

            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            foreach (string line in fileLines)
            {
                Reaction reaction = new Reaction(line);
                reactions.Add(reaction.Name, reaction);
            }

            return reactions;
        }
    }

    public class Reaction
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public Dictionary<string, Reaction> Outputs { get; set; }

        public Reaction(string creationInput)
        {
            Outputs = new Dictionary<string, Reaction>();
            string[] splits = creationInput.Split(new string[] { " => " }, StringSplitOptions.None);
            string[] splitOne = splits[1].Split(' ');
            Name = splitOne[1];
            Amount = int.Parse(splitOne[0]);

            string[] splitTwo = splits[0].Split(',');
            foreach (string outputs in splitTwo)
            {
                Reaction child = new Reaction();
                string[] childSplit = outputs.Trim().Split(' ');
                child.Name = childSplit[1];
                child.Amount = int.Parse(childSplit[0]);

                Outputs.Add(child.Name, child);
            }
        }

        public Reaction()
        {
            Outputs = new Dictionary<string, Reaction>();
        }

        public override string ToString()
        {
            return $"{Name},{Amount}->{Outputs.ToString()}";
        }
    }

    public class ReactionStep
    {
        public int Steps { get; set; }
        public Reaction Reaction { get; set; }
    }
}