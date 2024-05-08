using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Project_Data;


namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    /// using System.Collections.Generic;
    public partial class MainWindow : Window
    {
        
        // public void ShowStudyProgram(HighSchool highSchool)
        // {
        //     WPF.StudyProgramsControl.SelectedHighSchool = highSchool;
        //     var newWindow = new AddStudyProgram();
        //     Container.Content = newWindow;
        // }
        
        public MainWindow()
        {
            InitializeComponent();

            var schools = new SchoolsControl();
            Container.Content = schools;

            this.DataContext = this;
        }
    }
}