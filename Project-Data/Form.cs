namespace Project_Data;

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