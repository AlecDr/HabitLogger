using HabitLogger.Dtos.Habit;
using HabitLogger.Helpers;
using System.Data.SQLite;

namespace HabitLogger.Daos;

internal abstract class HabitsDao
{
    internal static HabitShowDTO? FindHabit(int id, string username)
    {
        HabitShowDTO? habit = null;

        using (SQLiteCommand command = DatabaseHelper.CreateCommand())
        {
            DatabaseHelper.SqliteConnection!.Open();

            command.CommandText = "SELECT * FROM HABITS WHERE id = @id AND username = @username;";
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("username", username);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    habit = new HabitShowDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
            }

            DatabaseHelper.SqliteConnection!.Close();
        }

        return habit;
    }

    internal static List<HabitShowDTO> GetAllHabits(string username)
    {
        List<HabitShowDTO> habits = [];

        using (SQLiteCommand command = DatabaseHelper.CreateCommand())
        {
            DatabaseHelper.SqliteConnection!.Open();
            command.CommandText = "SELECT * FROM HABITS WHERE username = @username;";
            command.Parameters.AddWithValue("username", username);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    habits.Add(new HabitShowDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }


            DatabaseHelper.SqliteConnection!.Close();
        }

        return habits;
    }

    internal static void StoreHabit(HabitStoreDTO habitStoreDTO)
    {
        DatabaseHelper.SqliteConnection!.Open();

        using (SQLiteCommand command = DatabaseHelper.CreateCommand())
        {
            command.CommandText = "INSERT INTO HABITS (description, username) VALUES (@description, @username)";

            command.Parameters.AddWithValue("description", habitStoreDTO.Description);
            command.Parameters.AddWithValue("username", habitStoreDTO.Username);

            command.ExecuteNonQuery();
        }

        DatabaseHelper.SqliteConnection!.Close();
    }

    internal static bool UpdateHabit(HabitUpdateDTO habitUpdateDTO)
    {
        HabitShowDTO? habit = FindHabit(habitUpdateDTO.Id, habitUpdateDTO.Username);

        if (habit != null)
        {
            DatabaseHelper.SqliteConnection!.Open();

            using (SQLiteCommand command = DatabaseHelper.CreateCommand())
            {
                command.CommandText = "UPDATE HABITS SET description = @description WHERE id = @id and username = @username;";

                command.Parameters.AddWithValue("id", habitUpdateDTO.Id);
                command.Parameters.AddWithValue("description", habitUpdateDTO.Description);
                command.Parameters.AddWithValue("username", habitUpdateDTO.Username);

                command.ExecuteNonQuery();
            }

            DatabaseHelper.SqliteConnection!.Close();

            return true;
        }

        return false;
    }

    internal static bool DeleteHabit(int id, string username)
    {
        HabitShowDTO? habit = FindHabit(id, username);

        if (habit != null)
        {
            DatabaseHelper.SqliteConnection!.Open();

            using (SQLiteCommand command = DatabaseHelper.CreateCommand())
            {
                command.CommandText = "DELETE FROM HABITS WHERE id = @id and username = @username;";

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("username", username);

                command.ExecuteNonQuery();
            }

            DatabaseHelper.SqliteConnection!.Close();

            return true;
        }

        return false;
    }
}
