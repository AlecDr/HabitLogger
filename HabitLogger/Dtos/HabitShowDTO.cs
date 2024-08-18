namespace HabitLogger.Dtos;

internal class HabitShowDTO
{
    internal int Id { get; }
    internal string Description { get; }
    internal string Username { get; }

    internal HabitShowDTO(int id, string description, string username)
    {
        Id = id;
        Description = description;
        Username = username;
    }
}
