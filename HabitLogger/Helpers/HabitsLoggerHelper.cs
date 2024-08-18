using HabitLogger.Dtos;
using Spectre.Console;

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
        string option = ConsoleHelper.ShowMainMenu(CurrentUser!);

        while (option == null || option.Trim() == "")
        {
            ConsoleHelper.ClearWindow();
            option = ConsoleHelper.ShowMainMenu(CurrentUser);
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
        ConsoleHelper.ClearWindow();
        string name = ConsoleHelper.GetText("What is your [blue]name[/]? ");

        CurrentUser = name;
    }

    internal void CreateHabit()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]Create an habit[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        string description = ConsoleHelper.GetText("What's the habit description?");

        DatabaseHelper.StoreHabit(new HabitStoreDTO(description, CurrentUser!));
        ConsoleHelper.ShowMessage("Habit stored successfully!");

        ConsoleHelper.ShowMessage("Press enter to continue");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    internal void UpdateHabit()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]Update an habit[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        List<HabitShowDTO> habits = DatabaseHelper.GetAllHabits(CurrentUser!);

        if (habits.Count <= 0)
        {
            ConsoleHelper.ShowMessage("No habits found for this user");
            ConsoleHelper.ShowMessage("Press enter to continue");
            AnsiConsole.Console.Input.ReadKey(false);
        }
        else
        {

            foreach (HabitShowDTO habit in habits)
            {
                PrintHabit(habit);
            }

            int.TryParse(ConsoleHelper.GetText("What's the habit ID?"), out int id);

            if (habits.Where(habit => habit.Id == (id > 0 ? id : 0)).Count() > 0)
            {

                string description = ConsoleHelper.GetText("What's the habit new description?");

                bool result = DatabaseHelper.UpdateHabit(new HabitUpdateDTO(id, description, CurrentUser!));

                ConsoleHelper.ShowMessage(result ? "Habit updated successfully!" : "Something went wrong :(");
                ConsoleHelper.ShowMessage("Press enter to continue");
                AnsiConsole.Console.Input.ReadKey(false);
            }
            else
            {
                ConsoleHelper.ShowMessage("This habit could not be found!");
                ConsoleHelper.ShowMessage("Press enter to continue");
                AnsiConsole.Console.Input.ReadKey(false);

            }
        }

    }

    private void PrintHabit(HabitShowDTO habit)
    {
        ConsoleHelper.ShowMessage($"{habit.Id} - {habit.Username} - {habit.Description}");
    }

    internal void ListHabits()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]List of habits[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        List<HabitShowDTO> habits = DatabaseHelper.GetAllHabits(CurrentUser!);

        if (habits.Count() > 0)
        {
            foreach (HabitShowDTO habit in habits)
            {
                PrintHabit(habit);
            }
            ConsoleHelper.ShowMessage("Press enter to continue");
            AnsiConsole.Console.Input.ReadKey(false);

        }
        else
        {
            ConsoleHelper.ShowMessage("No habits found.");
            ConsoleHelper.ShowMessage("Press enter to continue");
            AnsiConsole.Console.Input.ReadKey(false);
        }

    }

    internal void RouteToOption(char option)
    {
        switch (option)
        {
            case '1':
                CreateHabit();
                Run();
                break;
            case '2':
                ListHabits();
                Run();
                break;
            case '3':
                UpdateHabit();
                Run();
                break;
            case '6':
                AskName();
                Run();
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
