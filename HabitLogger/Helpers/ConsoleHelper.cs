using Spectre.Console;
using System.Globalization;

namespace HabitLogger.Helpers;

internal abstract class ConsoleHelper
{
    internal static string ShowMainMenu(string username)
    {
        ShowMessage($"HabitLogger - [blue] {username} [/] - [underline blue]Main Menu[/]", true, true, false);
        ShowMessage("");
        return GetChoice(HabitsLoggerHelper.GetMenuChoices(), "Choose an option bellow");
    }

    internal static void ClearWindow()
    {
        Console.Clear();
        AnsiConsole.Clear();
        Console.Clear();
    }

    internal static void ShowMessage(
        string message,
        bool breakLine = true,
        bool shouldClearWindow = false,
        bool figlet = false)
    {
        if (shouldClearWindow)
        {
            ClearWindow();
        }

        if (figlet)
        {
            if (breakLine)
            {
                AnsiConsole.WriteLine();
                AnsiConsole.Write(
            new FigletText(message)
                .LeftJustified()
                .Color(Color.Blue));
            }
            else
            {

                AnsiConsole.Write(
                new FigletText(message)
                    .LeftJustified()
                    .Color(Color.Blue));
            }
        }
        else
        {
            if (breakLine)
            {
                AnsiConsole.MarkupLine(message);
            }
            else
            {
                AnsiConsole.Markup(message);
            }
        }

    }

    internal static string? GetText(string message)
    {
        //return AnsiConsole.Ask<string>(message);

        ConsoleKey key;
        string input = string.Empty;

        // Inform the user about cancellation option
        AnsiConsole.MarkupLine("[grey](Press Esc to cancel)[/]");

        // Begin the prompt
        AnsiConsole.Markup($"[bold]{message}[/] ");

        // Read user input, one key at a time
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            // Handle Escape key
            if (key == ConsoleKey.Escape)
            {
                AnsiConsole.MarkupLine("\n[red]Input cancelled.[/]");
                return null;  // Or throw an exception if you want to handle it elsewhere
            }

            // Handle Backspace key
            if (key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input[..^1];
                Console.Write("\b \b");  // Erase the last character on the console
            }
            // Handle standard keys
            else if (key != ConsoleKey.Enter && key != ConsoleKey.Backspace)
            {
                input += keyInfo.KeyChar;
                Console.Write(keyInfo.KeyChar);  // Show the character
            }

        } while (key != ConsoleKey.Enter);

        AnsiConsole.WriteLine(); // Move to the next line after Enter is pressed
        return input;
    }

    internal static string GetChoice(
        List<string> choices,
        string title,
        int pageSize = 10,

        string moreChoicesText = "[grey](Move up and down to reveal more options)[/]")
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(pageSize)
                .MoreChoicesText(moreChoicesText)
                .AddChoices(choices));
    }

    internal static void PressAnyKeyToContinue()
    {
        ShowMessage("");
        ShowMessage("Press any key to continue");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    internal static DateTime GetDateTime()
    {
        TextPrompt<string> dateTimePrompt = new TextPrompt<string>("Enter a date and time [grey](yyyy-MM-dd HH:mm:ss)[/]:")
            .DefaultValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            .Validate(input =>
            {
                // Attempt to parse the input as a date and time in the specified format
                bool isValid = DateTime.TryParseExact(input,
                                                      "yyyy-MM-dd HH:mm:ss",
                                                      CultureInfo.InvariantCulture,
                                                      DateTimeStyles.None,
                                                      out _);
                return isValid ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid date and time format.[/]");
            });

        // Prompt the user for input
        string dateTimeInput = AnsiConsole.Prompt(dateTimePrompt);

        // Parse the valid input to a DateTime object
        DateTime dateTime = DateTime.ParseExact(dateTimeInput,
                                                "yyyy-MM-dd HH:mm:ss",
                                                CultureInfo.InvariantCulture);

        return dateTime;
    }

    internal static void ShowTitle(string message)
    {
        ShowMessage($"HabitLogger - [underline blue]{message}[/]", true, true, false);
        ShowMessage("");
    }
}
