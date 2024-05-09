using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Project_Data;

namespace WPF;

public partial class SchoolsControl : UserControl
{
    public static ObservableCollection<HighSchool> Schools { get; set; }

    public HighSchool HighSchoolChosen { get; set; }

    public void AddSchool(object sender, RoutedEventArgs e)
    {
        var newWindow = new AddSchoolDialogWindow();
        newWindow.ShowDialog();
    }
    
    public void DoubleClickSchool(object sender, RoutedEventArgs e)
    {
        var container = (ItemsControl)sender;
        var item = container.ItemContainerGenerator.ItemFromContainer(container.ContainerFromElement((FrameworkElement)e.OriginalSource));

        if (item == null) return;
        HighSchoolChosen = (HighSchool)item;
        
        StudyProgramsControl.SelectedHighSchool = HighSchoolChosen;
        MainWindow.Programs.SetPrograms();
        MainWindow.MainWindowRef.Container.Content = MainWindow.Programs;
        MainWindow.LastUserControl.Add(this);
    }
    
    public SchoolsControl()
    {
        InitializeComponent();
        
        var schools = CustomDb.GetObjectsFromDb<HighSchool>();
            
        Schools = schools == null ? new ObservableCollection<HighSchool>() : new ObservableCollection<HighSchool>(schools);

        this.DataContext = this;
    }
}