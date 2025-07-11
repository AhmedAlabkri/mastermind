using Mastermind;
using System.CommandLine;
using System.CommandLine.Parsing;

// ── options ────────────────────────────────────────────────────────────────
var codeOption = new Option<string?>("--code", "-c")
{
    Description         = "Secret 4-digit code (digits 0-8, distinct).",
    Arity               = ArgumentArity.ZeroOrOne,
    DefaultValueFactory = _ => null
};

var triesOption = new Option<int>("--tries", "-t")
{
    Description         = "Maximum attempts (default 10).",
    Arity               = ArgumentArity.ZeroOrOne,
    DefaultValueFactory = _ => 10
};

// ── root command ───────────────────────────────────────────────────────────
var root = new RootCommand("Console Mastermind")
{
    codeOption,
    triesOption
};

root.SetAction(parseResult =>
{
    string? codeText = parseResult.GetValue(codeOption);
    int     tries    = parseResult.GetValue(triesOption);

    if (tries <= 0 || tries > 100)
    {
        Console.Error.WriteLine("Error: attempts must be between 1 and 100.");
        return 1;
    }

    Code secret;
    if (string.IsNullOrEmpty(codeText))
    {
        secret = RandomCodeGenerator.NextSecret();
    }
    else
    {
        try { secret = Code.From(codeText); }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    var game = new Game(new GameOptions(secret, tries), new SystemConsole());
    game.Run();
    return 0;
});

// parse and execute
return await root.Parse(args).InvokeAsync();
