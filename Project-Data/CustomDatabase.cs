using System.Data.SQLite;
using System.Reflection;

namespace Project_Data;

public class CustomDatabase
{
    private readonly string _dbPath;
    private SQLiteConnection _connection = null!;

    public CustomDatabase()
    {
        _dbPath = GetDbPath();
        SetupConnection();
    }

    ~CustomDatabase()
    {
        _connection.Close();
    }

    private static string GetDbPath()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string[] path = assembly.Location.Split('\\');
        return String.Join('\\', path.Take(path.Length - 5).ToArray()) + "\\Db.db";
    }

    private void CreateDatabase(bool replace = false)
    {
        const string command =
            "-- Create tables\nCREATE TABLE Application (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Date DATETIME NOT NULL\n);\n\nCREATE TABLE Form (\n  StudyProgramId INTEGER NOT NULL,\n  ApplicationId INTEGER NOT NULL,\n  FOREIGN KEY (ApplicationId) REFERENCES Application(Id) ON DELETE NO ACTION ON UPDATE NO ACTION,\n  PRIMARY KEY (StudyProgramId, ApplicationId)\n);\n\nCREATE TABLE HighSchool (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Address TEXT NOT NULL\n);\n\nCREATE TABLE Student (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Address TEXT NOT NULL,\n  PhoneNumber TEXT NOT NULL,\n  BirthNumber TEXT NOT NULL,\n  ApplicationId INTEGER NOT NULL,\n  FOREIGN KEY (ApplicationId) REFERENCES Application(Id) ON DELETE NO ACTION ON UPDATE NO ACTION\n);\n\nCREATE TABLE StudyProgram (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Description TEXT NOT NULL,\n  FreePositions INTEGER NOT NULL,\n  OccupiedPositions INTEGER NOT NULL,\n  HighSchoolId INTEGER NOT NULL,\n  FOREIGN KEY (HighSchoolId) REFERENCES HighSchool(Id) ON DELETE NO ACTION ON UPDATE NO ACTION\n);";

        _connection = new SQLiteConnection($"Data Source={_dbPath}");
        _connection.Open();

        using var cmd = _connection.CreateCommand();
        cmd.CommandText = command;

        cmd.ExecuteNonQuery();
        Console.WriteLine("Databáze vytvořena!");
    }

    private void SetupConnection()
    {
        if (!File.Exists(_dbPath))
        {
            CreateDatabase();
            return;
        }

        _connection = new SQLiteConnection($"Data Source={_dbPath}");
        _connection.Open();
    }

    public List<T>? GetObjectsFromDb<T>(float? id = null, string joinAfter = "")
    {
        var type = typeof(T);
        var objects = new List<T>();
        try
        {
            var attributes = type.GetProperties();
            var propertyNames = string.Join(",", attributes.Select(p => (type.Name) + "." + p.Name));
            var commandText =
                $"SELECT DISTINCT {propertyNames} FROM {type.Name} {(id == null ? "" : "WHERE id = @id")} " +
                joinAfter;

            using var command = new SQLiteCommand(commandText, _connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();


            while (reader.Read() || attributes.Length != reader.FieldCount)
            {
                var obj = Activator.CreateInstance<T>();

                for (var i = 0; i < attributes.Length; i++)
                {
                    var property = attributes[i];
                    var value = reader.GetValue(i);
                    property.SetValue(obj, value);
                }

                objects.Add(obj);
            }
        }
        catch (SQLiteException e)
        {
            Console.WriteLine($"Získání objektu selhalo. Error: {e}");
            return default;
        }

        return objects.Count == 0 ? default : objects;
    }

    public long GetNextIdFromDb<T>(string customIdName = "")
    {
        var idName = customIdName == "" ? "Id" : customIdName;

        var type = typeof(T);
        var commandText = $"SELECT MAX({idName}) FROM {type.Name}";

        using var command = new SQLiteCommand(commandText, _connection);

        return (long)command.ExecuteScalar() + 1;
    }

    public bool InsertObjectToDb<T>(T obj)
    {
        try
        {
            var type = typeof(T);

            var properties = type.GetProperties();
            var insertQuery = $"INSERT INTO {type.Name} (";
            var parameterList = "";

            foreach (var p in properties)
            {
                insertQuery += $"{p.Name}, ";
                parameterList += $"@{p.Name}, ";
            }

            insertQuery = insertQuery.Substring(0, insertQuery.Length - 2) + ")";
            parameterList = parameterList.Substring(0, parameterList.Length - 2);

            insertQuery += $" VALUES ({parameterList})";
            using var command = new SQLiteCommand(insertQuery, _connection);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                command.Parameters.AddWithValue($"@{property.Name}", value);
            }

            command.ExecuteNonQuery();
        }
        catch (SQLiteException e)
        {
            Console.WriteLine($"Insert objektu selhal. Error: {e}");
            return false;
        }

        return true;
    }

    public bool UpdateObjectInDb<T>(T obj)
    {
        var type = typeof(T);
        try
        {
            var properties = type.GetProperties();
            var updateQuery = $"UPDATE {type.Name} SET ";

            foreach (var property in properties)
            {
                updateQuery += $"{property.Name} = @{property.Name}, ";
            }

            updateQuery = updateQuery.Substring(0, updateQuery.Length - 2) + " WHERE id = @id";

            using var command = new SQLiteCommand(updateQuery, _connection);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                command.Parameters.AddWithValue($"@{property.Name}", value);
            }

            var idProperty = type.GetProperty("Id");
            if (idProperty == null) return false;

            var idValue = idProperty.GetValue(obj);
            command.Parameters.AddWithValue("@id", idValue);

            var affected = command.ExecuteNonQuery();
            return affected != 0 ? true : false;
        }
        catch (SQLiteException e)
        {
            Console.WriteLine($"Update objektu selhal. Error: {e}");
            return false;
        }
    }

    public bool DeleteObjectFromDb<T>(float id)
    {
        var type = typeof(T);

        var deleteQuery = $"DELETE FROM {type.Name} WHERE id = @id";
        using var command = new SQLiteCommand(deleteQuery, _connection);

        command.Parameters.AddWithValue("@id", id);
        var affected = command.ExecuteNonQuery();

        return affected != 0 ? true : false;
    }

    public static void Main()
    {
    }
}