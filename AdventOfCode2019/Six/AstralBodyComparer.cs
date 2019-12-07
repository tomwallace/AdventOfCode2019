using System.Collections.Generic;

namespace AdventOfCode2019.Six
{
    public class AstralBodyComparer : IEqualityComparer<AstralBody>
    {
        public bool Equals(AstralBody c1, AstralBody c2)
        {
            if (c2 == null && c1 == null)
                return true;
            else if (c1 == null | c2 == null)
                return false;
            else if (c1.Name == c2.Name)
                return true;
            else
                return false;
        }

        public int GetHashCode(AstralBody c)
        {
            return $"{c.Name}".GetHashCode();
        }
    }
}