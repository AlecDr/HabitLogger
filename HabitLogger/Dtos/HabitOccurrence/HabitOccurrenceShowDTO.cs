namespace HabitLogger.Dtos.HabitOccurrence;

internal class HabitOccurrenceShowDTO
{
    internal int Id { get; }
    internal string Description { get; }
    internal string Datetime { get; }

    internal HabitOccurrenceShowDTO(int id, string description, string datetime)
    {
        Id = id;
        Description = description;
        Datetime = datetime;
    }
}
