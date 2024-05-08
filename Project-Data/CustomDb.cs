using System.Data.SQLite;
using System.Reflection;

namespace Project_Data // Note: actual namespace depends on the project name.
{
    public class CustomDb
    {
        private static readonly string _dbPath = GetDbPath();

        private static string GetDbPath()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] path = assembly.Location.Split('\\');
            return String.Join('\\', path.Take(path.Length - 5).ToArray()) + "\\Db.db";
        }

        public static void CreateDatabase(bool replace = false)
        {
            const string command =
                "-- Create tables\nCREATE TABLE Application (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Date DATETIME NOT NULL\n);\n\nCREATE TABLE Form (\n  StudyProgramId INTEGER NOT NULL,\n  ApplicationId INTEGER NOT NULL,\n  FOREIGN KEY (ApplicationId) REFERENCES Application(Id) ON DELETE NO ACTION ON UPDATE NO ACTION,\n  PRIMARY KEY (StudyProgramId, ApplicationId)\n);\n\nCREATE TABLE HighSchool (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Address TEXT NOT NULL\n);\n\nCREATE TABLE Student (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Address TEXT NOT NULL,\n  PhoneNumber TEXT NOT NULL,\n  BirthNumber TEXT NOT NULL,\n  ApplicationId INTEGER NOT NULL,\n  FOREIGN KEY (ApplicationId) REFERENCES Application(Id) ON DELETE NO ACTION ON UPDATE NO ACTION\n);\n\nCREATE TABLE StudyProgram (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Description TEXT NOT NULL,\n  FreePositions INTEGER NOT NULL,\n  OccupiedPositions INTEGER NOT NULL,\n  HighSchoolId INTEGER NOT NULL,\n  FOREIGN KEY (HighSchoolId) REFERENCES HighSchool(Id) ON DELETE NO ACTION ON UPDATE NO ACTION\n);";


            if (File.Exists(_dbPath) & !replace)
            {
                Console.WriteLine("Databáze již existuje, nebudu tvořit novou!");
            }
            else
            {
                if (File.Exists(_dbPath))
                {
                    File.Delete(_dbPath);
                }

                using var connection = new SQLiteConnection($"Data Source={_dbPath}");
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.CommandText = command;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Databáze vytvořena!");
            }
        }

        public static List<T>? GetObjectsFromDb<T>(int id, string join = "")
        {
            // Variables
            var type = typeof(T);
            var commandText = join + $"SELECT * FROM {type.Name} WHERE id = @id";
            var attributes = type.GetProperties();

            // Connection
            using var connection = new SQLiteConnection($"Data Source={_dbPath}");
            connection.Open();

            // Command
            using var command = new SQLiteCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", id);

            // Reading
            using var reader = command.ExecuteReader();

            var objects = new List<T>();
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

            return objects.Count == 0 ? default : objects;
        }

        public static bool InsertObjectToDb<T>(T obj)
        {
            var type = typeof(T);
            using var connection = new SQLiteConnection($"Data Source={_dbPath}");
            connection.Open();

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
            using var command = new SQLiteCommand(insertQuery, connection);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                command.Parameters.AddWithValue($"@{property.Name}", value);
            }

            command.ExecuteNonQuery();

            return true;
        }

        public static bool UpdateObjectInDb<T>(T obj)
        {
            var type = typeof(T);
            using var connection = new SQLiteConnection($"Data Source={_dbPath}");
            connection.Open();

            var properties = type.GetProperties();
            var updateQuery = $"UPDATE {type.Name} SET ";

            foreach (var property in properties)
            {
                updateQuery += $"{property.Name} = @{property.Name}, ";
            }

            updateQuery = updateQuery.Substring(0, updateQuery.Length - 2) + " WHERE id = @id";

            using var command = new SQLiteCommand(updateQuery, connection);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                command.Parameters.AddWithValue($"@{property.Name}", value);
            }

            var idProperty = type.GetProperty("Id");
            var idValue = idProperty.GetValue(obj);
            command.Parameters.AddWithValue("@id", idValue);

            command.ExecuteNonQuery();

            return true;
        }

        public static bool DeleteObjectFromDb<T>(int id)
        {
            var type = typeof(T);
            using var connection = new SQLiteConnection($"Data Source={_dbPath}");
            connection.Open();

            var deleteQuery = $"DELETE FROM {type.Name} WHERE id = @id";
            using var command = new SQLiteCommand(deleteQuery, connection);

            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            return true;
        }


        static void Main(string[] args)
        {
            // HighSchool? highSchool = GetObjectFromDb<HighSchool>(1);
            // Console.WriteLine(highSchool.Address + highSchool.Name);
            // Console.WriteLine("Hello World!");

            // HighSchool highSchool = new(1, "test2", "test2address");
            // InsertObjectToDb(highSchool);

            List<HighSchool>? highSchool2 = GetObjectsFromDb<HighSchool>(1);
            Console.WriteLine(highSchool2[0].Name);

            // highSchool2.Name = "ještěvícnovýjméno";
            // UpdateObjectInDb(highSchool2);
            //
            // HighSchool? highSchool3 = GetObjectFromDb<HighSchool>(1);
            // Console.WriteLine(highSchool3.Address + ":" + highSchool3.Name);
            // Console.WriteLine("Hello World!");

            // DeleteObjectFromDb<HighSchool>(1);
        }
    }
}