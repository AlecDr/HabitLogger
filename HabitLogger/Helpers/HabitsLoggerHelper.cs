using HabitLogger.Daos;
using HabitLogger.Dtos.Habit;
using HabitLogger.Dtos.HabitOccurrence;

namespace HabitLogger.Helpers;

internal class HabitsLoggerHelper
{
    string? CurrentUser { get; set; }

    internal void Run()
    {
        while (CurrentUser == null)
        {
            CheckUser();
        }

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
        ConsoleHelper.ShowTitle("User Selection");

        string? name = ConsoleHelper.GetText("What is your [blue]name[/]? ");

        if (name != null && name.Trim().Length > 0)
        {
            CurrentUser = name;
        }
    }

    private void CreateHabit()
    {
        ConsoleHelper.ShowTitle("Create an habit");

        string description = ConsoleHelper.GetText("What's the habit description?");

        HabitsDao.StoreHabit(new HabitStoreDTO(description, CurrentUser!));
        ConsoleHelper.ShowMessage("Habit stored successfully!");

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void UpdateHabit()
    {
        ConsoleHelper.ShowTitle("Update an habit");

        int? id = ShowHabitsAndAskForId("Whats the habit ID to update?");

        if (id.HasValue)
        {
            string description = ConsoleHelper.GetText("What's the habit new description?");

            bool result = HabitsDao.UpdateHabit(new HabitUpdateDTO(id.Value, description, CurrentUser!));

            ConsoleHelper.ShowMessage(result ? "Habit updated successfully!" : "Something went wrong :(");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("This habit could not be found!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void DeleteHabit()
    {
        ConsoleHelper.ShowTitle("Delete an habit");

        int? id = ShowHabitsAndAskForId("Whats the habit ID to delete?");

        if (id.HasValue)
        {
            bool result = HabitsDao.DeleteHabit(id.Value, CurrentUser!);

            ConsoleHelper.ShowMessage(result ? "Habit deleted successfully!" : "Something went wrong :(");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("This habit could not be found!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void InformHabit()
    {
        ConsoleHelper.ShowTitle("Inform habit");

        int? id = ShowHabitsAndAskForId("Whats the habit ID to inform?");

        if (id.HasValue)
        {
            DateTime dateTime = ConsoleHelper.GetDateTime();

            HabitsOccurrencesDao.StoreOccurrence(new HabitOccurrenceStoreDTO(id.Value, dateTime));

            ConsoleHelper.ShowMessage("Habit informed successfully!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("No habits found!");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }

    private void PrintHabit(HabitShowDTO habit)
    {
        ConsoleHelper.ShowMessage($"{habit.Id} - {habit.Description}");
    }

    private void ListHabits()
    {
        ConsoleHelper.ShowTitle("List of habits");

        List<HabitShowDTO> habits = HabitsDao.GetAllHabits(CurrentUser!);

        if (habits.Count() > 0)
        {
            foreach (HabitShowDTO habit in habits)
            {
                PrintHabit(habit);
            }

            ConsoleHelper.PressAnyKeyToContinue();
        }
        else
        {
            ConsoleHelper.ShowMessage("No habits found.");
            ConsoleHelper.PressAnyKeyToContinue();
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

            ConsoleHelper.ShowMessage("");

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
