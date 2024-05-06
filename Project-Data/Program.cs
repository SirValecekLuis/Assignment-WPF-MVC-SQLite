using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;


namespace Project_Data // Note: actual namespace depends on the project name.
{
    public class HighSchool
    {
        [Key] public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public HighSchool()
        {
        }

        public HighSchool(long id, string? name, string? address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }

    public class StudyProgram
    {
        [Key] public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int FreePositions { get; set; }
        public int OccupiedPositions { get; set; }
        public long HighSchoolId { get; set; }

        public StudyProgram()
        {
        }

        public StudyProgram(long id, string? name, string? description, int freePositions, int occupiedPositions,
            long highSchoolId)
        {
            Id = id;
            Name = name;
            Description = description;
            FreePositions = freePositions;
            OccupiedPositions = occupiedPositions;
            HighSchoolId = highSchoolId;
        }
    }

    public class Form
    {
        public long StudyProgramId { get; set; }
        public long ApplicationId { get; set; }

        public Form()
        {
        }

        public Form(long studyProgramId, long applicationId)
        {
            StudyProgramId = studyProgramId;
            ApplicationId = applicationId;
        }
    }

    public class Application
    {
        [Key] public long Id { get; set; }
        public DateTime Date { get; set; }

        public Application()
        {
        }

        public Application(long id, DateTime date)
        {
            Id = id;
            Date = date;
        }
    }

    public class Student
    {
        [Key] public long Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BirthNumber { get; set; }
        public long ApplicationId { get; set; }

        public Student()
        {
        }

        public Student(long id, string? name, string? phoneNumber, string? birthNumber, long applicationId)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            BirthNumber = birthNumber;
            ApplicationId = applicationId;
        }
    }

    public class Program
    {
        public static void create_database(bool replace = false)
        {
            string name = "DB.db";
            string command =
                "-- Create tables\nCREATE TABLE Application (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Date DATETIME NOT NULL\n);\n\nCREATE TABLE Form (\n  StudyProgramId INTEGER NOT NULL,\n  ApplicationId INTEGER NOT NULL,\n  FOREIGN KEY (ApplicationId) REFERENCES Application(Id) ON DELETE NO ACTION ON UPDATE NO ACTION,\n  PRIMARY KEY (StudyProgramId, ApplicationId)\n);\n\nCREATE TABLE HighSchool (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Address TEXT NOT NULL\n);\n\nCREATE TABLE Student (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Address TEXT NOT NULL,\n  PhoneNumber TEXT NOT NULL,\n  BirthNumber TEXT NOT NULL,\n  ApplicationId INTEGER NOT NULL,\n  FOREIGN KEY (ApplicationId) REFERENCES Application(Id) ON DELETE NO ACTION ON UPDATE NO ACTION\n);\n\nCREATE TABLE StudyProgram (\n  Id INTEGER NOT NULL PRIMARY KEY,\n  Name TEXT NOT NULL,\n  Description TEXT NOT NULL,\n  FreePositions INTEGER NOT NULL,\n  OccupiedPositions INTEGER NOT NULL,\n  HighSchoolId INTEGER NOT NULL,\n  FOREIGN KEY (HighSchoolId) REFERENCES HighSchool(Id) ON DELETE NO ACTION ON UPDATE NO ACTION\n);";


            if (File.Exists(name) & !replace)
            {
                Console.WriteLine("Databáze již existuje, nebudu tvořit novou!");
            }
            else
            {
                if (File.Exists(name))
                {
                    File.Delete(name);
                }

                using var connection = new SQLiteConnection($"Data Source={name}");
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.CommandText = command;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Databáze vytvořena!");
            }
        }

        public static void PrintDb()
        {
            string name = "DB.db";
            using var connection = new SQLiteConnection($"Data Source={name}");
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HighSchool";
            var count = (long?)cmd.ExecuteScalar();

            Console.WriteLine(count.ToString());
        }

        public static void GetInfoOfClass<T>()
        {
            var type = typeof(T);
            var attributes = type.GetProperties();

            Console.WriteLine(attributes.Length);

            foreach (var attr in attributes)
            {
                Console.WriteLine(attr.Name);
            }
        }

        public static T? GetObjectFromDb<T>(int id, string join = "")
        {
            // Variables
            var type = typeof(T);
            var commandText = join + $"SELECT * FROM {type.Name} WHERE id = @id";
            const string name = "DB.db";
            var attributes = type.GetProperties();

            // Connection
            using var connection = new SQLiteConnection($"Data Source={name}");
            connection.Open();

            // Command
            using var command = new SQLiteCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", id);

            // Reading
            using var reader = command.ExecuteReader();


            if (!reader.Read() || attributes.Length != reader.FieldCount)
            {
                return default;
            }

            var obj = Activator.CreateInstance<T>();


            for (var i = 0; i < attributes.Length; i++)
            {
                var property = attributes[i];
                var value = reader.GetValue(i);
                property.SetValue(obj, value);
            }

            return obj;
        }

        public static bool InsertObjectToDb<T>(T obj)
        {
            var type = typeof(T);
            const string name = "DB.db";
            using var connection = new SQLiteConnection($"Data Source={name}");
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
            const string name = "DB.db";
            using var connection = new SQLiteConnection($"Data Source={name}");
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
            const string name = "DB.db";
            using var connection = new SQLiteConnection($"Data Source={name}");
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

            // HighSchool? highSchool2 = GetObjectFromDb<HighSchool>(1);


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