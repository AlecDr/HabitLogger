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

    internal static string GetText(string message)
    {
        return AnsiConsole.Ask<string>(message);
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

    internal static void PressEnterToContinue()
    {
        ConsoleHelper.ShowMessage("Press enter to continue");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    internal static DateTime GetDateTime()
    {
        TextPrompt<string> dateTimePrompt = new TextPrompt<string>("Enter a date and time [grey](yyyy-MM-dd HH:mm:ss)[/]:")
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
}
