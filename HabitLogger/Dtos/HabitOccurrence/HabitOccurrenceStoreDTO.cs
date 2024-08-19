namespace HabitLogger.Dtos.HabitOccurrence;

internal class HabitOccurrenceStoreDTO
{
    internal string Description { get; }
    internal string Username { get; }

    internal HabitOccurrenceStoreDTO(string description, string username)
    {
        Description = description;
        Username = username;
    }
}
