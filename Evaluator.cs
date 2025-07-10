namespace Mastermind
{
    /// <summary>
    /// Contains the static logic for comparing a player's guess against the secret code.
    /// </summary>
    public static class Evaluator
    {
        /// <summary>
        /// Evaluates a guess and determines the number of well-placed and misplaced pieces.
        /// Uses a two-pass algorithm to prevent double-counting pieces.
        /// </summary>
        public static CodeEvaluation Evaluate(Code secret, Code guess)
        {
            int wellPlaced = 0;
            int misplaced = 0;

            char[] secretChars = secret.Value.ToCharArray();
            char[] guessChars  = guess.Value.ToCharArray();

            // Pass 1: Check for well-placed pieces.
            for (int i = 0; i < GameConstants.CodeLength; i++)
            {
                if (secretChars[i] == guessChars[i])
                {
                    wellPlaced++;
                    secretChars[i] = 'S'; // Mark as consumed
                    guessChars[i]  = 'G'; // Mark as consumed
                }
            }

            // Pass 2: Check for misplaced pieces using remaining characters.
            for (int i = 0; i < GameConstants.CodeLength; i++)
            {
                if (guessChars[i] == 'G')
                {
                    continue; // Skip characters already marked as well-placed.
                }

                int foundIndex = Array.IndexOf(secretChars, guessChars[i]);
                if (foundIndex != -1)
                {
                    misplaced++;
                    secretChars[foundIndex] = 'S'; // Mark as consumed
                }
            }

            return new CodeEvaluation
            {
                WellPlaced = wellPlaced,
                Misplaced  = misplaced
            };
        }
    }
}