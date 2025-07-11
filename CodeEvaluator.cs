namespace Mastermind;

public static class CodeEvaluator
{
    // Marks a digit as consumed during evaluation
    private const char Consumed = '\0';

    public static CodeEvaluation Evaluate(Code secret, Code guess)
    {
        int wellPlaced = 0;
        int misplaced  = 0;

        char[] secretChars = secret.Value.ToCharArray();
        char[] guessChars  = guess.Value.ToCharArray();

        // Pass 1 – exact matches
        for (int i = 0; i < GameConstants.CodeLength; i++)
        {
            if (secretChars[i] == guessChars[i])
            {
                wellPlaced++;
                secretChars[i] = Consumed;
                guessChars[i]  = Consumed;
            }
        }

        // Pass 2 – digits present but misplaced
        for (int i = 0; i < GameConstants.CodeLength; i++)
        {
            if (guessChars[i] == Consumed) continue;

            int pos = Array.IndexOf(secretChars, guessChars[i]);
            if (pos != -1)
            {
                misplaced++;
                secretChars[pos] = Consumed;
            }
        }

        return new CodeEvaluation { WellPlaced = wellPlaced, Misplaced = misplaced };
    }
}
