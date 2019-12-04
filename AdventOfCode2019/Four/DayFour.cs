using System.Collections.Generic;

namespace AdventOfCode2019.Four
{
    public class DayFour : IAdventProblemSet
    {
        private const int INPUT_LOWER = 138307;
        private const int INPUT_UPPER = 654504;

        public string Description()
        {
            return "Secure Container";
        }

        public int SortOrder()
        {
            return 4;
        }

        public string PartA()
        {
            List<IPasswordRule> rules = new List<IPasswordRule>()
            {
                new CorrectDigits(), new AdjacentDigitsDoNotDecrease(), new DuplicateDigit()
            };

            int result = NumberMatchingPasswords(INPUT_LOWER, INPUT_UPPER, rules);
            return result.ToString();
        }

        public string PartB()
        {
            List<IPasswordRule> rules = new List<IPasswordRule>()
            {
                new CorrectDigits(), new AdjacentDigitsDoNotDecrease(), new DuplicateDigitExcludeTriple()
            };

            int result = NumberMatchingPasswords(INPUT_LOWER, INPUT_UPPER, rules);
            return result.ToString();
        }

        public int NumberMatchingPasswords(int inputLower, int inputUpper, List<IPasswordRule> rules)
        {
            int numberMatchingPasswords = 0;

            for (int i = inputLower; i <= inputUpper; i++)
            {
                if (MatchesPasswordRules(i, rules))
                    numberMatchingPasswords++;
            }

            return numberMatchingPasswords;
        }

        private bool MatchesPasswordRules(int password, List<IPasswordRule> rules)
        {
            foreach (IPasswordRule rule in rules)
            {
                if (!rule.IsValid(password))
                    return false;
            }

            return true;
        }
    }
}