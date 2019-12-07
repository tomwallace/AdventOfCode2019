using System.Collections.Generic;

namespace AdventOfCode2019.Six
{
    public class AstralBody
    {
        public string Name { get; set; }

        public HashSet<AstralBody> Orbiters { get; set; }

        public AstralBody Parent { get; set; }

        public bool YourFlightTrace { get; set; }

        public AstralBody(string name)
        {
            Name = name;
            Orbiters = new HashSet<AstralBody>();
            YourFlightTrace = false;
        }

        public override bool Equals(object obj)
        {
            AstralBodyComparer comparer = new AstralBodyComparer();
            return comparer.Equals(this, (AstralBody)obj);
        }

        public override int GetHashCode()
        {
            AstralBodyComparer comparer = new AstralBodyComparer();
            return comparer.GetHashCode(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}