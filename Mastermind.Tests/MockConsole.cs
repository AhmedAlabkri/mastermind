using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mastermind;

namespace Mastermind.Tests;

public sealed class MockConsole : IConsole
{
    private readonly Queue<string> _inputs;
    private readonly StringBuilder _output = new();

    public MockConsole(IEnumerable<string>? scriptedInputs = null)
    {
        _inputs = new Queue<string>(scriptedInputs ?? Enumerable.Empty<string>());
    }

    public string CapturedOutput => _output.ToString();

    public string? ReadLine() => _inputs.Count > 0 ? _inputs.Dequeue() : null;

    public void Write(string value) => _output.Append(value);

    public void WriteLine(string value = "") => _output.AppendLine(value);
}