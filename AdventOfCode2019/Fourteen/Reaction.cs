using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Fourteen
{
    public class Reaction
    {
        public string Name { get; set; }
        public long Amount { get; set; }
        public Dictionary<string, Reaction> Outputs { get; set; }

        public Reaction(string creationInput)
        {
            Outputs = new Dictionary<string, Reaction>();
            string[] splits = creationInput.Split(new string[] { " => " }, StringSplitOptions.None);
            string[] splitOne = splits[1].Split(' ');
            Name = splitOne[1];
            Amount = long.Parse(splitOne[0]);

            string[] splitTwo = splits[0].Split(',');
            foreach (string outputs in splitTwo)
            {
                Reaction child = new Reaction();
                string[] childSplit = outputs.Trim().Split(' ');
                child.Name = childSplit[1];
                child.Amount = long.Parse(childSplit[0]);

                Outputs.Add(child.Name, child);
            }
        }

        public Reaction()
        {
            Outputs = new Dictionary<string, Reaction>();
            Amount = 1;
        }

        public override string ToString()
        {
            return $"{Name},{Amount}->{Outputs.ToString()}";
        }
    }
}