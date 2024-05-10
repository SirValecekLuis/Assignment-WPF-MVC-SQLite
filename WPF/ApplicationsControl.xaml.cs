using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Project_Data;

namespace WPF;

public partial class ApplicationsControl
{
    static public ObservableCollection<Application> Applications { get; set; } = null!;

    public void SetApplications(List<Application>? applications)
    {
        Applications.Clear();
        if (applications == null) return;
        
        foreach (var app in applications)
        {
            Applications.Add(app);
        }
    }
    
    public ApplicationsControl()
    {
        Applications = new ObservableCollection<Application>();

        this.DataContext = this;
        
        InitializeComponent();
    }
}