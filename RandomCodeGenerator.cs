namespace Mastermind;

public static class RandomCodeGenerator
{
    private static readonly Random _rng = new();

    public static Code NextSecret()
    {
        // digits 0-8, take first 4
        var digits = Enumerable.Range(0, 9)
                               .Select(d => (char)('0' + d))
                               .ToArray();

        for (int i = digits.Length - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (digits[i], digits[j]) = (digits[j], digits[i]);
        }

        return Code.From(new string(digits, 0, GameConstants.CodeLength));
    }
}
