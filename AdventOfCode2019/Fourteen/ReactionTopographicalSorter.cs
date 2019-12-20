using System.Collections.Generic;

namespace AdventOfCode2019.Fourteen
{
    public class ReactionTopographicalSorter
    {
        private List<string> depthFirstOrder;
        private HashSet<string> markedKeys;

        // Perform a topographical sort of the Reactions
        // https://en.wikipedia.org/wiki/Topological_sorting and help from https://github.com/fdouw/AoC2019/blob/master/Day14/Day14.cs
        public ReactionTopographicalSorter(Dictionary<string, Reaction> reactions)
        {
            depthFirstOrder = new List<string>();
            markedKeys = new HashSet<string>();

            foreach (string item in reactions.Keys)
                if (!markedKeys.Contains(item))
                    DepthFirstSearch(reactions, item);

            depthFirstOrder.Reverse();
        }

        private void DepthFirstSearch(Dictionary<string, Reaction> reactions, string start)
        {
            markedKeys.Add(start);

            foreach (var item in reactions[start].Outputs)
                if (!markedKeys.Contains(item.Key))
                    DepthFirstSearch(reactions, item.Key);

            depthFirstOrder.Add(start);
        }

        public List<string> GetOrderedKeys()
        {
            return depthFirstOrder;
        }
    }
}