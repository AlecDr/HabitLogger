namespace HabitLogger.Dtos.Habit;

internal class HabitUpdateDTO : HabitStoreDTO
{
    internal int Id { get; }

    internal HabitUpdateDTO(int id, string description, string username) : base(description, username)
    {
        Id = id;
    }
}
