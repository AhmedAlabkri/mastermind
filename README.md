# Mastermind — Console Edition

[![.NET Build & Test](https://github.com/AhmedAlabkri/mastermind/actions/workflows/build.yml/badge.svg)](https://github.com/AhmedAlabkri/mastermind/actions/workflows/build.yml)

This project is a console-based implementation of the classic Mastermind game, developed in C# as part of the Steer Elite Internship Program skills assessment. It is built with modern .NET 9, adheres to strict OOP principles, and is validated by a comprehensive xUnit test suite with over 90% code coverage.

## AI-Assisted Workflow

In line with the assessment's goal to demonstrate an "Ability to create and enhance AI-generated content," this project was developed through a structured, human-led collaboration with AI. My role was to act as the architect and final arbiter of quality.

### Concrete Example: Achieving Testability

A primary goal was to ensure the console application was fully testable—a non-trivial task.

1.  **Brainstorming:** I used **OpenAI o3** to discuss strategies for testing console I/O. The model suggested abstracting `System.Console` behind an interface, a standard best practice.

2.  **Guided Implementation:** I asked the AI to generate the boilerplate for an `IConsole` interface and a `MockConsole` class. After reviewing and refining that code, I refactored the `Game` class to depend on the abstraction. (Dependency Injection).

3.  **Human-Led Validation:** With this testable architecture in place, I was able to write the `GameTranscriptTests`. These tests prove the application's correctness by comparing its output character-for-character against predefined transcripts.

### Research Accelerator

To ground the project in solid engineering principles, I used ChatGPT as a research accelerator for modern .NET practices. I explored its suggestions on OOP patterns, testing strategies, and CI/CD, then verified the most promising ideas against official Microsoft documentation to ensure the final architecture was robust and appropriate.

Additionally, the included README, commit messages, and test
  transcripts were reviewed and polished using AI to ensure clarity
  and consistency.

  

## Features

*   **Classic Mastermind Logic:** Guess a 4-digit secret code composed of distinct numbers from 0-8.
*   **Dynamic Configuration:**
    *   Specify a secret code at runtime with the `-c` or `--code` flag.
    *   Set the number of attempts with the `-t` or `--tries` flag (defaults to 10).
*   **Random Code Generation:** If no code is specified, a new pseudorandom code is generated for each game.
*   **Robust Input Validation:** Gracefully handles invalid user guesses and provides clear feedback.
*   **Clean Exit:** Correctly handles `Ctrl+D` (EOF) signals for a smooth user experience.

## Design & Architecture

The application is designed with a strong emphasis on modern Object-Oriented Programming (OOP) principles, focusing on separation of concerns, testability, and robustness.

*   **Dependency Injection:** An `IConsole` interface is used to abstract away console I/O. This allows the core game logic to be completely decoupled from the `System.Console`, enabling precise and reliable testing via a `MockConsole`.
*   **Separation of Concerns:**
    *   `Game.cs`: Contains the main game loop and user interaction logic.
    *   `CodeEvaluator.cs`: A static class responsible for the pure logic of comparing a guess against the secret code.
    *   `Domain.cs`: Defines immutable record types (`Code`, `CodeEvaluation`) to ensure data integrity. The `Code` record's factory method (`From`) encapsulates all validation logic.
*   **Robust Argument Parsing:** The industry-standard `System.CommandLine` library is used to professionally parse and validate command-line arguments.

## Getting Started

### Prerequisites

*   [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

### Installation & Running

1.  Clone the repository:
    ```bash
    git clone https://github.com/AhmedAlabkri/mastermind.git
    ```
2.  Navigate to the project directory:
    ```bash
    cd mastermind
    ```
3.  Run the application with your desired options. The `--` is used to separate `dotnet` arguments from application arguments.
    ```bash
    # Run with a specific code and 5 attempts
    dotnet run -- -c 1234 -t 5

    # Run with a random code and default 10 attempts
    dotnet run
    ```


### Running the Tests

The project includes a comprehensive test suite. To run the tests:
```bash
dotnet test
```

### Publishing an Executable

To compile a self-contained executable for your platform, use the `publish` command.

1.  Publish the project in Release mode:
    ```bash
    dotnet publish ./mastermind.csproj -c Release -o ./publish
    ```

2.  Run the created executable from the `publish` directory, just like in the assessment example:
    ```bash
    cd publish
    ./my_mastermind -c "0123"
    ```


## Command-Line Arguments

The application accepts the following command-line arguments:

```text
  -c, --code <CODE>    Specifies the 4-digit secret code (distinct, 0-8).
                       [default: A random code is generated]

  -t, --tries <TRIES>  Specifies the number of attempts allowed.
                       [default: 10]
