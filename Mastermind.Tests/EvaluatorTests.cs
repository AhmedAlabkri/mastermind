using Mastermind;
using FluentAssertions;
using Xunit;

namespace Mastermind.Tests;

public class EvaluatorTests
{
    [Theory]
    [InlineData("0123", "0123", 4, 0)] // Exact match
    [InlineData("0123", "3210", 0, 4)] // All misplaced
    [InlineData("0123", "0456", 1, 0)] // One well-placed
    [InlineData("0123", "4560", 0, 1)] // One misplaced
    [InlineData("1234", "1243", 2, 2)] // Mixed
    [InlineData("1234", "5678", 0, 0)] // No match
    public void Evaluate_ReturnsExpectedCounts(
        string secret, string guess, int expectedWellPlaced, int expectedMisplaced)
    {
        // Arrange
        var secretCode = Code.From(secret);
        var guessCode = Code.From(guess);

        // Act
        var result = CodeEvaluator.Evaluate(secretCode, guessCode);

        // Assert
        result.WellPlaced.Should().Be(expectedWellPlaced);
        result.Misplaced.Should().Be(expectedMisplaced);
    }
}