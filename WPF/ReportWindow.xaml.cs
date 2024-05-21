using System.Windows;
using Project_Data;
using Application = Project_Data.Application;

namespace WPF;

public partial class ReportWindow
{
    public string SchoolsCount { get; set; }
    public string StudyProgramsCount { get; set; }
    public string StudentsCount { get; set; }
    public string ApplicationsCount { get; set; }

    private void CreateReport()
    {
        var schools = MainWindow.MyDatabase.GetObjectsFromDb<HighSchool>();
        SchoolsCount = schools?.Count.ToString() ?? "0";
        
        var studyPrograms = MainWindow.MyDatabase.GetObjectsFromDb<StudyProgram>();
        StudyProgramsCount = studyPrograms?.Count.ToString() ?? "0";
        
        var students = MainWindow.MyDatabase.GetObjectsFromDb<Student>();
        StudentsCount = students?.Count.ToString() ?? "0";

        var applications = MainWindow.MyDatabase.GetObjectsFromDb<Application>();
        ApplicationsCount = applications?.Count.ToString() ?? "0";
    }
    
    public ReportWindow() 
    {
        InitializeComponent();

        this.DataContext = this;
        CreateReport();
    }
}