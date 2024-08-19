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
}
