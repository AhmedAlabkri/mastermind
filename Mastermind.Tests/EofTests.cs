using System;
using Mastermind;
using FluentAssertions;
using Xunit;

namespace Mastermind.Tests;

/// <summary>
/// Checks how the game handles EOF signals like Ctrl+D or Ctrl+Z.
/// </summary>
public class EofTests
{
    [Fact]
    public void Run_ExitsGracefully_OnEofSignal()
    {
        // Arrange
        var console = new MockConsole(Array.Empty<string>()); // Simulates immediate EOF
        var game = new Game(new GameOptions(Code.From("0123"), 1), console);

        // Act
        Action act = () => game.Run();

        // Assert
        act.Should().NotThrow(); // The most important thing is that it doesn't crash.
        console.CapturedOutput.Should().Contain("Will you find the secret code?");
    }
}