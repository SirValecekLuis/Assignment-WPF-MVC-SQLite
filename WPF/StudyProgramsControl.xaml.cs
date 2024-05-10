using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using Project_Data;

namespace WPF;

public partial class StudyProgramsControl
{
    public ObservableCollection<StudyProgram> Programs { get; set; }

    public void SetPrograms(HighSchool selectedHighSchool)
    {
        List<StudyProgram>? studyPrograms = CustomDb.GetObjectsFromDb<StudyProgram>(joinAfter: $"where HighSchoolID = {selectedHighSchool!.Id}");
        Programs.Clear();

        if (studyPrograms == null) return;
        
        foreach (var program in studyPrograms)
        {
            Programs.Add(program);
        }
    }
    
    public StudyProgramsControl()
    {
        this.DataContext = this;
        Programs = new ObservableCollection<StudyProgram>();  
        
        InitializeComponent();
    }
}