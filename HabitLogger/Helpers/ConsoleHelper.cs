using Spectre.Console;

namespace HabitLogger.Helpers;

internal abstract class ConsoleHelper
{
    internal static string ShowMainMenu()
    {
        ShowMessage("HabitLogger - [underline blue]Main Menu[/]", true, true, false);
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
}
