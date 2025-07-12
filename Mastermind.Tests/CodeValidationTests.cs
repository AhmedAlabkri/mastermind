using System;
using Mastermind;
using FluentAssertions;
using Xunit;

namespace Mastermind.Tests;

/// <summary>
/// Tests to make sure Code.From() handles input validation correctly.
/// </summary>
public class CodeValidationTests
{
    [Theory]
    [InlineData("0123")]
    [InlineData("8042")]
    public void From_AcceptsValidCodes(string code)
    {
        // Act / Assert — no exception should occur
        Code.From(code).Value.Should().Be(code);
    }

    [Theory]
    [InlineData("0129")]  // digit out of allowed range
    [InlineData("0000")]  // duplicated digits
    [InlineData("123")]   // too short
    [InlineData("12345")] // too long
    public void From_ThrowsArgumentException_OnInvalidData(string invalidCode)
    {
        // Act
        Action act = () => Code.From(invalidCode);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void From_ThrowsArgumentException_OnNullInput()
    {
        // Arrange
        string? nullCode = null;

        // Act
        Action act = () => Code.From(nullCode!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}