using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Project_Data;

namespace WPF;

public partial class AddStudyProgram : Window
{
    public static HighSchool SelectedHighSchool { get; set; }
    public static ObservableCollection<StudyProgram> Programs { get; set; }
    public AddStudyProgram()
    {
        List<StudyProgram>? studyPrograms = CustomDb.GetObjectsFromDb<StudyProgram>(joinAfter: $"where HighSchoolID = {1}");

        Programs = studyPrograms == null ? new ObservableCollection<StudyProgram>() : new ObservableCollection<StudyProgram>(studyPrograms);

        this.DataContext = this;
        InitializeComponent();
    }
}