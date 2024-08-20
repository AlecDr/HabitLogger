using HabitLogger.Dtos.HabitOccurrence;
using HabitLogger.Helpers;
using System.Data.SQLite;
using System.Globalization;

namespace HabitLogger.Daos;

internal abstract class HabitsOccurrencesDao
{
    internal static void StoreOccurrence(HabitOccurrenceStoreDTO habitOccurrenceStoreDTO)
    {
        DatabaseHelper.SqliteConnection!.Open();

        using (SQLiteCommand command = DatabaseHelper.CreateCommand())
        {
            command.CommandText = "INSERT INTO HABITS_OCCURRENCES (habit_id, datetime) VALUES (@habit_id, @datetime)";

            command.Parameters.AddWithValue("habit_id", habitOccurrenceStoreDTO.HabitId);
            command.Parameters.AddWithValue("datetime", habitOccurrenceStoreDTO.Datetime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

            command.ExecuteNonQuery();
        }

        DatabaseHelper.SqliteConnection!.Close();
    }

    internal static List<HabitOccurrenceShowDTO> GetAllOccurrences(string username)
    {
        List<HabitOccurrenceShowDTO> occurrences = [];

        using (SQLiteCommand command = DatabaseHelper.CreateCommand())
        {
            DatabaseHelper.SqliteConnection!.Open();
            command.CommandText = "SELECT HABITS_OCCURRENCES.id, HABITS.description as 'habit_description', datetime FROM HABITS_OCCURRENCES JOIN HABITS on HABITS.ID = HABITS_OCCURRENCES.habit_id WHERE username = @username;";
            command.Parameters.AddWithValue("username", username);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    occurrences.Add(new HabitOccurrenceShowDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }

            DatabaseHelper.SqliteConnection!.Close();
        }

        return occurrences;
    }

    internal static bool DeleteOccurrencesFromHabit(int habitId)
    {

        DatabaseHelper.SqliteConnection!.Open();

        using (SQLiteCommand command = DatabaseHelper.CreateCommand())
        {
            command.CommandText = "DELETE FROM HABITS_OCCURRENCES WHERE habit_id = @habitId ";

            command.Parameters.AddWithValue("habitId", habitId);

            command.ExecuteNonQuery();
        }

        DatabaseHelper.SqliteConnection!.Close();

        return true;
    }
}
