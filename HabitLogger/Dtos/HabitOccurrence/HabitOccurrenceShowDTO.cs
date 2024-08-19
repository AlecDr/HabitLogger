namespace HabitLogger.Dtos.HabitOccurrence;

internal class HabitOccurrenceShowDTO
{
    internal int Id { get; }
    internal string Description { get; }
    internal string Username { get; }

    internal HabitOccurrenceShowDTO(int id, string description, string username)
    {
        Id = id;
        Description = description;
        Username = username;
    }
}
