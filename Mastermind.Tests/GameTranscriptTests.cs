using System.IO;
using Mastermind;
using FluentAssertions;
using Xunit;

namespace Mastermind.Tests;

public class GameTranscriptTests
{
    private static string Transcript(string name) =>
        File.ReadAllText(Path.Combine("Transcripts", $"{name}.txt"))
            .Replace("\r\n", "\n");

    [Fact]
    public void Run_PrintsExactTranscript_ForWinningGame()
    {
        var console  = new MockConsole(new[] { "0123" });
        var game     = new Game(new GameOptions(Code.From("0123"), 10), console);
        var expected = Transcript("example_win");

        game.Run();

        console.CapturedOutput.Replace("\r\n", "\n")
              .TrimEnd()
              .Should().Be(expected.TrimEnd());
    }

    [Fact]
    public void Run_PrintsExactTranscript_ForLosingGame()
    {
        var console  = new MockConsole(new[] { "5678" });
        var game     = new Game(new GameOptions(Code.From("0123"), 1), console);
        var expected = Transcript("example_lose");

        game.Run();

        console.CapturedOutput.Replace("\r\n", "\n")
              .TrimEnd()
              .Should().Be(expected.TrimEnd());
    }
}
