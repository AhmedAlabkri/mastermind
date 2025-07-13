namespace Mastermind;

/// <summary>Constants used across the game.</summary>
public static class GameConstants
{
    public const int CodeLength = 4;
}

/// <summary>Immutable, validated secret or guess code.</summary>
public sealed record Code
{
    public string Value { get; }

    private Code(string value) => Value = value;

    public static Code From(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != GameConstants.CodeLength)
            throw new ArgumentException($"Code must be {GameConstants.CodeLength} characters long.");

        if (value.Any(c => c < '0' || c > '8'))
            throw new ArgumentException("Code must contain digits 0-8 only.");

        if (value.Distinct().Count() != GameConstants.CodeLength)
            throw new ArgumentException("Digits must be distinct.");

        return new Code(value);
    }

    public override string ToString() => Value;
}

/// <summary>What you get when a guess is checked.</summary>
public readonly struct CodeEvaluation
{
    public int WellPlaced { get; init; }
    public int Misplaced  { get; init; }

    public bool IsCorrect => WellPlaced == GameConstants.CodeLength;
}
