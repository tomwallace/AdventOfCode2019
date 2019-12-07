using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Six
{
    public class DaySix : IAdventProblemSet
    {
        public string Description()
        {
            return "Universal Orbit Map";
        }

        public int SortOrder()
        {
            return 6;
        }

        public string PartA()
        {
            string filePath = @"Six\DaySixInput.txt";
            int result = CalculateNumberOfOrbits(filePath);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Six\DaySixInput.txt";
            int result = CalculateOrbitsBetweenYouAndSanta(filePath);
            return result.ToString();
        }

        public int CalculateNumberOfOrbits(string filePath)
        {
            List<AstralBody> astralBodies = CreateAstralBodies(filePath);
            AstralBody root = DetermineRootBodyFromTree(astralBodies);

            int orbits = FindOrbitsRecursive(root, 0);

            return orbits;
        }

        // Originally tried using a BFS queue search, which worked fine for the sample, but
        // resulted in out of memory for the real puzzle.  So, refactored to count steps
        // back to root and do the same for Santa, ignoring the part where the paths overlapped
        public int CalculateOrbitsBetweenYouAndSanta(string filePath)
        {
            List<AstralBody> astralBodies = CreateAstralBodies(filePath);
            AstralBody yourAstralBody = astralBodies.First(a => a.Name == "YOU").Parent;
            AstralBody santaAstralBody = astralBodies.First(a => a.Name == "SAN").Parent;
            AstralBody root = DetermineRootBodyFromTree(astralBodies);

            // Determine number of steps it takes you to get to root
            int yourSteps = 0;
            AstralBody current = yourAstralBody;
            do
            {
                current.YourFlightTrace = true;
                current = current.Parent;
                yourSteps++;
            } while (!current.Equals(root));

            // Determine number of steps for Santa to get to root, noting first time crosses your path
            int santaSteps = 0;
            current = santaAstralBody;
            bool firstTimeCrossing = false;
            AstralBody firstComesAcrossYourTrail = root;
            do
            {
                current = current.Parent;
                santaSteps++;
                if (current.YourFlightTrace && !firstTimeCrossing)
                {
                    firstTimeCrossing = true;
                    firstComesAcrossYourTrail = current;
                }
            } while (!current.Equals(root));

            // Determine the number of steps for the firstComesAcrossYourTrail to root
            int firstCrossingStepsToRoot = 0;
            current = firstComesAcrossYourTrail;
            do
            {
                current = current.Parent;
                firstCrossingStepsToRoot++;
            } while (!current.Equals(root));

            // Answer is your steps - firstCrossingToRoot + santa steps - firstCrossingToRoot
            return (yourSteps - firstCrossingStepsToRoot) + (santaSteps - firstCrossingStepsToRoot);
        }

        private int FindOrbitsRecursive(AstralBody current, int orbitsSoFar)
        {
            if (current.Orbiters.Count == 0)
                return orbitsSoFar;

            int orbitsFromChildren = 0;
            foreach (AstralBody orbiter in current.Orbiters)
            {
                orbitsFromChildren += FindOrbitsRecursive(orbiter, orbitsSoFar + 1);
            }

            return orbitsFromChildren + orbitsSoFar;
        }

        private AstralBody DetermineRootBodyFromTree(List<AstralBody> astralBodies)
        {
            HashSet<string> allOrbiterNames = new HashSet<string>();
            foreach (AstralBody astralBody in astralBodies)
            {
                allOrbiterNames.UnionWith(new HashSet<string>(astralBody.Orbiters.Select(a => a.Name)));
            }

            // Now find the AstralBody not in the allOrbiterNames collection and that is the root
            AstralBody root = astralBodies.FirstOrDefault(a => !allOrbiterNames.Contains(a.Name));
            return root;
        }

        private List<AstralBody> CreateAstralBodies(string filePath)
        {
            List<AstralBody> astralBodies = new List<AstralBody>();
            List<string> instructions = FileUtility.ParseFileToList(filePath, line => line);

            for (int i = 0; i < instructions.Count; i++)
            {
                string instruction = instructions[i];
                string[] bodyNames = instruction.Split(')');
                AstralBody bodyOne = astralBodies.FirstOrDefault(a => a.Name == bodyNames[0]);
                if (bodyOne == null)
                    bodyOne = new AstralBody(bodyNames[0]);

                AstralBody bodyTwo = astralBodies.FirstOrDefault(a => a.Name == bodyNames[1]);
                if (bodyTwo == null)
                    bodyTwo = new AstralBody(bodyNames[1]);

                bodyOne.Orbiters.Add(bodyTwo);
                bodyTwo.Parent = bodyOne;

                astralBodies.Add(bodyOne);
                astralBodies.Add(bodyTwo);
            }

            return astralBodies;
        }
    }
}