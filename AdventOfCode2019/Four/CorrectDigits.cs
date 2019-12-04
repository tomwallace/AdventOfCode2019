namespace AdventOfCode2019.Four
{
    /// <summary>
    /// Passwords must have exactly 6 digits
    /// </summary>
    public class CorrectDigits : IPasswordRule
    {
        public bool IsValid(int password)
        {
            char[] split = password.ToString().ToCharArray();
            return split.Length == 6;
        }
    }
}