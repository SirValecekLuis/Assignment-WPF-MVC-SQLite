using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Project_Data;

namespace WPF;

public partial class StudyProgramsControl : UserControl
{
    public static HighSchool SelectedHighSchool { get; set; } = null!;
    public static ObservableCollection<StudyProgram> Programs { get; set; } = null!;

    public void SetPrograms()
    {
        List<StudyProgram>? studyPrograms = CustomDb.GetObjectsFromDb<StudyProgram>(joinAfter: $"where HighSchoolID = {1}");

        Programs = studyPrograms == null ? new ObservableCollection<StudyProgram>() : new ObservableCollection<StudyProgram>(studyPrograms);
        
        InitializeComponent();
    }
    
    public StudyProgramsControl()
    {
        this.DataContext = this;
    }
}