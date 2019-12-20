using AdventOfCode2019.Utility;
using System;
using System.Collections.Generic;

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
            string filePath = @"Fourteen\DayFourteenInput.txt";
            long calculation = CalculateOreRequired(filePath, 1);

            return calculation.ToString();
        }

        public string PartB()
        {
            string filePath = @"Fourteen\DayFourteenInput.txt";
            long calculation = AmountFuelProduced(filePath);

            return calculation.ToString();
        }

        public long CalculateOreRequired(string filePath, long fuelRequired)
        {
            Dictionary<string, Reaction> reactionCatalog = GetReactions(filePath);
            List<string> orderedKeys = new ReactionTopographicalSorter(reactionCatalog).GetOrderedKeys();

            Dictionary<string, long> quantitiesNeeded = new Dictionary<string, long>();
            quantitiesNeeded.Add("FUEL", fuelRequired);

            foreach (string key in orderedKeys)
            {
                long amountProduced = reactionCatalog[key].Amount;
                long amountNeeded = quantitiesNeeded.ContainsKey(key) ? quantitiesNeeded[key] : 1;
                long toMake = (long)Math.Ceiling((decimal)amountNeeded / amountProduced);

                foreach (var output in reactionCatalog[key].Outputs)
                {
                    if (quantitiesNeeded.ContainsKey(output.Key))
                        quantitiesNeeded[output.Key] += output.Value.Amount * toMake;
                    else
                        quantitiesNeeded.Add(output.Key, output.Value.Amount * toMake);
                }
            }
            return quantitiesNeeded["ORE"];
        }

        public long AmountFuelProduced(string filePath)
        {
            // We have up to 1000000000000 of ore, so need to iterate over the different amounts to calculate max fuel
            // To make this faster, come from opposite ends of the range, and vary by half each time
            long requiredOre = CalculateOreRequired(filePath, 1);

            long target = 1000000000000;
            long lowThreshold = (target / requiredOre) - 1000;
            long highThreshold = (target / requiredOre) + 1000000000;

            while (lowThreshold < highThreshold)
            {
                long mid = (lowThreshold + highThreshold) / 2;
                long guess = CalculateOreRequired(filePath, mid);
                if (guess > target)
                {
                    highThreshold = mid;
                }
                else if (guess < target)
                {
                    if (mid == lowThreshold)
                        break;

                    lowThreshold = mid;
                }
                else
                {
                    lowThreshold = mid;
                    break;
                }
            }

            return lowThreshold;
        }

        private Dictionary<string, Reaction> GetReactions(string filePath)
        {
            Dictionary<string, Reaction> reactions = new Dictionary<string, Reaction>();
            // Add ORE default
            Reaction ore = new Reaction();
            ore.Name = "ORE";
            reactions.Add("ORE", ore);

            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            foreach (string line in fileLines)
            {
                Reaction reaction = new Reaction(line);
                reactions.Add(reaction.Name, reaction);
            }

            return reactions;
        }
    }
}