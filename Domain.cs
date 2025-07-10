namespace Mastermind
{
    /// <summary>
    /// Defines shared constants used throughout the game logic.
    /// </summary>
    public static class GameConstants
    {
        public const int CodeLength = 4;
    }

    /// <summary>
    /// Represents a valid game code.
    /// Implemented as an immutable record to ensure code integrity after creation.
    /// </summary>
    public record Code
    {
        public string Value { get; }

        private Code(string value) => Value = value;

        /// <summary>
        /// Factory method to create a valid Code instance.
        /// Enforces all game rules: 4 distinct digits, each between '0' and '8'.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the value is invalid.</exception>
        public static Code From(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != GameConstants.CodeLength)
            {
                throw new ArgumentException($"Code must be {GameConstants.CodeLength} characters long.");
            }
            if (value.Any(c => c < '0' || c > '8'))
            {
                throw new ArgumentException("Code must only contain digits 0-8.");
            }
            if (value.Distinct().Count() != GameConstants.CodeLength)
            {
                throw new ArgumentException($"Code must contain {GameConstants.CodeLength} distinct digits.");
            }
            return new Code(value);
        }

        public override string ToString() => Value;
    }

    /// <summary>
    /// Represents the result of a single guess, containing the count of
    /// well-placed and misplaced pieces.
    /// </summary>
    public readonly struct CodeEvaluation
    {
        public int WellPlaced { get; init; }
        public int Misplaced { get; init; }
        
        /// <summary>
        /// A win condition is met when all pieces are well-placed.
        /// </summary>
        public bool IsCorrect => WellPlaced == GameConstants.CodeLength;
    }
}