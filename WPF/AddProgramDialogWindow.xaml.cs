using System.Windows;
using Project_Data;

namespace WPF;

public partial class AddProgramDialogWindow
{
    public AddProgramDialogWindow()
    {
        InitializeComponent();
    }
    
    public void AddStudyProgramToDb(object sender, RoutedEventArgs e)
    {
        if (TextBoxName.Text == "" || TextBoxDescription.Text == "" || TextBoxFreePositions.Text == "")
        {
            InfoLabel.Content = "Není vyplněno jméno a adresa!";
        }
        
        StudyProgram studyProgram = new();
        studyProgram.Id = MainWindow.MyDatabase.GetNextIdFromDb<StudyProgram>();
        studyProgram.Name = TextBoxName.Text;

        try
        {
            studyProgram.FreePositions = long.Parse(TextBoxFreePositions.Text);
        }
        catch
        {
            InfoLabel.Content = "Volné pozice musí být celé číslo!";
            return;
        }

        studyProgram.Description = TextBoxDescription.Text;
        studyProgram.OccupiedPositions = 0;
        studyProgram.HighSchoolId = SchoolsControl.HighSchoolChosen!.Id;

        MainWindow.MyDatabase.InsertObjectToDb(studyProgram);
        StudyProgramsControl.Programs.Add(studyProgram);
        InfoLabel.Content = "Obor úspěšně přidán!";
    }
}