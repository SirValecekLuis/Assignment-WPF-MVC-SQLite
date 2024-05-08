using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Project_Data;

namespace WPF;

public partial class SchoolsControl : UserControl
{
    public static ObservableCollection<HighSchool> Schools { get; set; }

    public void AddSchool(object sender, RoutedEventArgs e)
    {
        var newWindow = new AddSchoolDialogWindow();
        newWindow.ShowDialog();
    }
    
    public SchoolsControl()
    {
        InitializeComponent();
        
        var schools = CustomDb.GetObjectsFromDb<HighSchool>();
            
        Schools = schools == null ? new ObservableCollection<HighSchool>() : new ObservableCollection<HighSchool>(schools);

        this.DataContext = this;
    }
}