using HabitLogger.Dtos;
using System.Data.SQLite;

namespace HabitLogger.Helpers;

internal abstract class DatabaseHelper
{
    private static SQLiteConnection? sqliteConnection;

    private static SQLiteConnection GetConnection()
    {
        if (sqliteConnection == null)
        {
            CreateDatabase();
            CreateConnection();
            CreateTables();
        }

        return sqliteConnection!;
    }

    private static void CreateConnection()
    {
        sqliteConnection = new SQLiteConnection($"Data Source={GetDatabasePath()};Version=3;");
    }

    private static string GetDatabasePath()
    {
        string projectFolder = Environment.CurrentDirectory;
        string databasePath = System.IO.Path.Combine(projectFolder, "habits.db");

        return databasePath;
    }

    private static SQLiteCommand CreateCommand()
    {
        return GetConnection().CreateCommand();
    }

    private static void CreateDatabase()
    {
        if (!File.Exists(GetDatabasePath()))
        {
            SQLiteConnection.CreateFile(GetDatabasePath());
        }
    }

    private static void CreateTables()
    {
        sqliteConnection!.Open();
        SQLiteCommand command = CreateCommand();

        command.CommandText = "CREATE TABLE IF NOT EXISTS HABITS(id INTEGER PRIMARY KEY AUTOINCREMENT, description Varchar(255), username VarChar(255))";

        command.ExecuteNonQuery();
        sqliteConnection.Close();
    }

    public static HabitShowDTO? FindHabit(int id, string username)
    {
        HabitShowDTO? habit = null;

        using (SQLiteCommand command = CreateCommand())
        {
            sqliteConnection!.Open();

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


            sqliteConnection!.Close();
        }

        return habit;
    }

    public static List<HabitShowDTO> GetAllHabits(string username)
    {
        List<HabitShowDTO> habits = [];

        using (SQLiteCommand command = CreateCommand())
        {
            sqliteConnection!.Open();
            command.CommandText = "SELECT * FROM HABITS WHERE username = @username;";
            command.Parameters.AddWithValue("username", username);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    habits.Add(new HabitShowDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }


            sqliteConnection!.Close();
        }

        return habits;
    }

    public static void StoreHabit(HabitStoreDTO habitStoreDTO)
    {
        sqliteConnection!.Open();

        using (SQLiteCommand command = CreateCommand())
        {
            command.CommandText = "INSERT INTO HABITS (description, username) VALUES (@description, @username)";

            command.Parameters.AddWithValue("description", habitStoreDTO.Description);
            command.Parameters.AddWithValue("username", habitStoreDTO.Username);

            command.ExecuteNonQuery();
        }

        sqliteConnection.Close();
    }

    public static bool UpdateHabit(HabitUpdateDTO habitUpdateDTO)
    {
        HabitShowDTO? habit = FindHabit(habitUpdateDTO.Id, habitUpdateDTO.Username);

        if (habit != null)
        {
            sqliteConnection!.Open();

            using (SQLiteCommand command = CreateCommand())
            {
                command.CommandText = "UPDATE HABITS SET description = @description WHERE id = @id and username = @username;";

                command.Parameters.AddWithValue("id", habitUpdateDTO.Id);
                command.Parameters.AddWithValue("description", habitUpdateDTO.Description);
                command.Parameters.AddWithValue("username", habitUpdateDTO.Username);

                command.ExecuteNonQuery();
            }

            sqliteConnection.Close();

            return true;
        }

        return false;
    }

    public static bool DeleteHabit(HabitUpdateDTO habitUpdateDTO)
    {
        HabitShowDTO? habit = FindHabit(habitUpdateDTO.Id, habitUpdateDTO.Username);

        if (habit != null)
        {

            sqliteConnection!.Open();

            using (SQLiteCommand command = CreateCommand())
            {
                command.CommandText = "DELETE FROM HABITS WHERE id = @id and username = @username;";

                command.Parameters.AddWithValue("id", habitUpdateDTO.Description);
                command.Parameters.AddWithValue("username", habitUpdateDTO.Username);

                command.ExecuteNonQuery();
            }

            sqliteConnection.Close();

            return true;
        }

        return false;
    }
}
