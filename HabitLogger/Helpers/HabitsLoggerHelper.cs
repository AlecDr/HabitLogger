namespace HabitLogger.Helpers;

internal class HabitsLoggerHelper
{
    string? CurrentUser { get; set; }

    internal void Run()
    {
        CheckUser();

        string option = GetOption();

        RouteToOption(option.ElementAt(0));
    }

    private string GetOption()
    {
        string option = ConsoleHelper.ShowMainMenu();

        while (option == null || option.Trim() == "")
        {
            ConsoleHelper.ClearWindow();
            ConsoleHelper.ClearWindow();
            option = ConsoleHelper.ShowMainMenu();
        }

        return option;
    }

    internal void CheckUser()
    {
        if (CurrentUser == null)
        {
            AskName();
            ConsoleHelper.ClearWindow();
        }
    }

    internal void AskName()
    {
        string name = ConsoleHelper.GetText("What is your [blue]name[/]? ");

        CurrentUser = name;
    }

    internal void CreateHabit()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]Create an habit[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        ConsoleHelper.GetText("What's the habit name?");
    }

    internal void RouteToOption(char option)
    {
        switch (option)
        {
            case '1':
                CreateHabit();
                break;

            default:
                Run();
                break;
        }
    }

    internal static List<string> GetMenuChoices()
    {
        return [
            "1 - [blue]C[/]reate an habit",
            "2 - [blue]L[/]ist all habits",
            "3 - [blue]U[/]pdate habit",
            "4 - [blue]D[/]elete habit",
            "5 - [blue]I[/]nform habit",
            "6 - [blue]A[/]lter username",
            ];
    }
}
