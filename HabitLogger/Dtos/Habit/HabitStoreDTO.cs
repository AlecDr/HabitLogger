namespace HabitLogger.Dtos.Habit;

internal class HabitStoreDTO
{
    internal string Description { get; }
    internal string Username { get; }

    internal HabitStoreDTO(string description, string username)
    {
        Description = description;
        Username = username;
    }
}
