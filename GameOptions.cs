namespace Mastermind;

/// <summary>Runtime parameters for a game session.</summary>
public sealed record GameOptions(Code SecretCode, int MaxAttempts);
