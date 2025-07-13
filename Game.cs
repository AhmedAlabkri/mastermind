namespace Mastermind;

/// <summary>Kicks off the Mastermind game loop, matching the PDF spec.</summary>
public class Game
{
    private readonly Code     _secretCode;
    private readonly int      _maxAttempts;
    private readonly IConsole _console;

    public Game(GameOptions opts, IConsole console)
    {
        _secretCode  = opts.SecretCode;
        _maxAttempts = opts.MaxAttempts;
        _console     = console;
    }

    public void Run()
    {
        // ── intro ──────────────────────────────────────────────────────────
        _console.WriteLine("Will you find the secret code?");
        _console.WriteLine("Please enter a valid guess");

        // ── rounds ────────────────────────────────────────────────────────
        for (int round = 0; round < _maxAttempts; round++)
        {
            _console.WriteLine("---");
            _console.WriteLine($"Round {round}");

            while (true)
            {
                _console.Write(">");              // no newline

                string? input = _console.ReadLine();

                // EOF  → graceful exit
                if (input is null)
                {
                    _console.WriteLine();
                    return;
                }

                try
                {
                    var guess  = Code.From(input);
                    var result = CodeEvaluator.Evaluate(_secretCode, guess);

                    if (result.IsCorrect)
                    {
                        _console.WriteLine("Congratz! You did it!");
                        return;
                    }

                    _console.WriteLine($"Well placed pieces: {result.WellPlaced}");
                    _console.WriteLine($"Misplaced pieces: {result.Misplaced}");
                    break;                         // next round
                }
                catch (ArgumentException)
                {
                    _console.WriteLine();          // blank line only after wrong input
                    _console.WriteLine("Wrong input!");
                }
            }
        }

        _console.WriteLine("---");
        _console.WriteLine($"You ran out of attempts! The code was: {_secretCode.Value}");
    }
}