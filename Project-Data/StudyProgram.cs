namespace Project_Data;

public class StudyProgram
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long FreePositions { get; set; }
    public long OccupiedPositions { get; set; }
    public long HighSchoolId { get; set; }

    public StudyProgram()
    {
    }

    public StudyProgram(long id, string name, string description, long freePositions, long occupiedPositions,
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