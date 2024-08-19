namespace HabitLogger.Dtos.HabitOccurrence;

internal class HabitOccurrenceStoreDTO
{
    internal int HabitId { get; }
    internal DateTime Datetime { get; }

    internal HabitOccurrenceStoreDTO(int habitId, DateTime datetime)
    {
        HabitId = habitId;
        Datetime = datetime;
    }
}
