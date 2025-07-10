namespace Mastermind
{
    /// <summary>
    /// Manages the main game loop and player interaction.
    /// </summary>
    public class Game
    {
        private readonly Code _secretCode;
        private readonly int _maxAttempts;

        public Game(Code secretCode, int maxAttempts)
        {
            _secretCode = secretCode;
            _maxAttempts = maxAttempts;
        }

        /// <summary>
        /// Starts and runs the main game loop until the player wins, loses, or quits.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Will you find the secret code?");
            Console.WriteLine("Please enter a valid guess.");

            int currentAttempt = 0;
            while (currentAttempt < _maxAttempts)
            {
                Console.WriteLine("---");
                Console.WriteLine($"Round {currentAttempt}");
                Console.Write(">");

                string? input = Console.ReadLine();

                // Handle EOF (Ctrl+D on Unix, Ctrl+Z on Windows) for graceful exit.
                if (input == null)
                {
                    Console.WriteLine("\nExiting game.");
                    return;
                }
                
                // Basic format check before creating a Code object.
                if (input.Length != GameConstants.CodeLength || input.Any(c => !char.IsDigit(c)))
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }

                Code guess;
                try
                {
                    guess = Code.From(input);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }

                var result = Evaluator.Evaluate(_secretCode, guess);
                if (result.IsCorrect)
                {
                    Console.WriteLine("Congratz! You did it!");
                    return;
                }

                Console.WriteLine($"Well placed pieces: {result.WellPlaced}");
                Console.WriteLine($"Misplaced pieces: {result.Misplaced}");
                currentAttempt++;
            }

            Console.WriteLine("---");
            Console.WriteLine("You ran out of attempts! The code was: " + _secretCode.Value);
        }
    }
}