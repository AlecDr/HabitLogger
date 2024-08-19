namespace HabitLogger.Dtos.HabitOccurrence;

internal class HabitOccurrenceUpdateDTO : HabitOccurrenceStoreDTO
{
    internal int Id { get; }

    internal HabitOccurrenceUpdateDTO(int id, string description, string username) : base(description, username)
    {
        Id = id;
    }
}
