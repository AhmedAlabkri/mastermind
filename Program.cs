using Mastermind;
/// <summary>
/// The main entry point for the Mastermind console application.
/// </summary>


// fixed secret code and attempt count for simple testing.
// Future versions will source these values from command-line arguments.
var secret   = Code.From("0123"); 
var attempts = 10;

var game = new Game(secret, attempts);
game.Run();