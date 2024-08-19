using System.Data.SQLite;

namespace HabitLogger.Helpers;

internal abstract class DatabaseHelper
{
    static SQLiteConnection? _sqliteConnection;
    internal static SQLiteConnection? SqliteConnection { get { return GetConnection(); } private set { SqliteConnection = value; } }

    private static SQLiteConnection GetConnection()
    {
        if (_sqliteConnection == null)
        {
            CreateDatabase();
            CreateConnection();
            CreateTables();
        }

        return _sqliteConnection!;
    }

    internal static void CreateConnection()
    {
        _sqliteConnection = new SQLiteConnection($"Data Source={GetDatabasePath()};Version=3;");
    }

    private static string GetDatabasePath()
    {
        string projectFolder = Environment.CurrentDirectory;
        string databasePath = System.IO.Path.Combine(projectFolder, "habits.db");

        return databasePath;
    }

    internal static SQLiteCommand CreateCommand()
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
        _sqliteConnection!.Open();

        // habits table
        SQLiteCommand command = CreateCommand();

        command.CommandText = "CREATE TABLE IF NOT EXISTS HABITS(id INTEGER PRIMARY KEY AUTOINCREMENT, description VARCHAR(255), username VARCHAR(255))";
        command.ExecuteNonQuery();

        // habits logs
        command = CreateCommand();

        command.CommandText = "CREATE TABLE IF NOT EXISTS HABITS_OCCURRENCES(id INTEGER PRIMARY KEY AUTOINCREMENT, habit_id INTEGER, datetime VARCHAR(19), FOREIGN KEY (habit_id) REFERENCES HABITS(id))";
        command.ExecuteNonQuery();

        _sqliteConnection.Close();
    }
}
