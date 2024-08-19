using HabitLogger.Daos;
using HabitLogger.Dtos.Habit;
using HabitLogger.Dtos.HabitOccurrence;

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

    private void CheckUser()
    {
        if (CurrentUser == null)
        {
            AskName();
            ConsoleHelper.ClearWindow();
        }
    }

    private void AskName()
    {
        ConsoleHelper.ClearWindow();
        string name = ConsoleHelper.GetText("What is your [blue]name[/]? ");

        CurrentUser = name;
    }

    private void CreateHabit()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]Create an habit[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        string description = ConsoleHelper.GetText("What's the habit description?");

        HabitsDao.StoreHabit(new HabitStoreDTO(description, CurrentUser!));
        ConsoleHelper.ShowMessage("Habit stored successfully!");

        ConsoleHelper.PressEnterToContinue();
    }

    private void UpdateHabit()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]Update an habit[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        int? id = ShowHabitsAndAskForId("Whats the habit ID to update?");

        if (id.HasValue)
        {
            string description = ConsoleHelper.GetText("What's the habit new description?");

            bool result = HabitsDao.UpdateHabit(new HabitUpdateDTO(id.Value, description, CurrentUser!));

            ConsoleHelper.ShowMessage(result ? "Habit updated successfully!" : "Something went wrong :(");
            ConsoleHelper.PressEnterToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("This habit could not be found!");
            ConsoleHelper.PressEnterToContinue();
        }
    }

    private void DeleteHabit()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]Delete an habit[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        int? id = ShowHabitsAndAskForId("Whats the habit ID to delete?");

        if (id.HasValue)
        {
            bool result = HabitsDao.DeleteHabit(id.Value, CurrentUser!);

            ConsoleHelper.ShowMessage(result ? "Habit deleted successfully!" : "Something went wrong :(");
            ConsoleHelper.PressEnterToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("This habit could not be found!");
            ConsoleHelper.PressEnterToContinue();
        }
    }

    private void InformHabit()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]Inform habit[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        int? id = ShowHabitsAndAskForId("Whats the habit ID to inform?");

        if (id.HasValue)
        {
            DateTime dateTime = ConsoleHelper.GetDateTime();

            HabitsOccurrencesDao.StoreOccurrence(new HabitOccurrenceStoreDTO(id.Value, dateTime));

            ConsoleHelper.ShowMessage("Habit informed successfully!");
            ConsoleHelper.PressEnterToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("This habit could not be found!");
            ConsoleHelper.PressEnterToContinue();
        }
    }

    private void PrintHabit(HabitShowDTO habit)
    {
        ConsoleHelper.ShowMessage($"{habit.Id} - {habit.Description}");
    }

    private void ListHabits()
    {
        ConsoleHelper.ShowMessage("HabitLogger - [underline blue]List of habits[/]", true, true, false);
        ConsoleHelper.ShowMessage("");

        List<HabitShowDTO> habits = HabitsDao.GetAllHabits(CurrentUser!);

        if (habits.Count() > 0)
        {
            foreach (HabitShowDTO habit in habits)
            {
                PrintHabit(habit);
            }

            ConsoleHelper.PressEnterToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("No habits found.");
            ConsoleHelper.PressEnterToContinue();
        }

    }

    private int? ShowHabitsAndAskForId(string message)
    {
        List<HabitShowDTO> habits = HabitsDao.GetAllHabits(CurrentUser!);

        if (habits.Count <= 0)
        {
            return null;
        }
        else
        {
            foreach (HabitShowDTO habit in habits)
            {
                PrintHabit(habit);
            }

            int.TryParse(ConsoleHelper.GetText(message), out int id);

            return habits.Where(habit => habit.Id == (id > 0 ? id : 0)).Count() > 0 ? id : null;
        }
    }

    private void RouteToOption(char option)
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
            case '4':
                DeleteHabit();
                Run();
                break;
            case '5':
                InformHabit();
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
