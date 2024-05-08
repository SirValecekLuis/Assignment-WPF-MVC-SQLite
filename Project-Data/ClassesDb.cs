namespace Project_Data
{
    public class HighSchool
    {
        public long Id { get; set; }
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
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long FreePositions { get; set; }
        public long OccupiedPositions { get; set; }
        public long HighSchoolId { get; set; }

        public StudyProgram()
        {
        }

        public StudyProgram(long id, string? name, string? description, long freePositions, long occupiedPositions,
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
        public long Id { get; set; }
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
        public long Id { get; set; }
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
}