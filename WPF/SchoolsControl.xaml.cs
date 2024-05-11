using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Project_Data;
using Application = Project_Data.Application;

namespace WPF;

public partial class SchoolsControl
{
    public static ObservableCollection<HighSchool> Schools { get; set; } = null!;

    public static HighSchool? HighSchoolChosen { get; set; }
    public List<Application>? Applications { get; set; }
    private Border? LastBorder { get; set; }
    public void AddSchool(object sender, RoutedEventArgs e)
    {
        var newWindow = new AddSchoolDialogWindow();
        newWindow.ShowDialog();
    }
    
    
    public void DoubleClickSchool(object sender, RoutedEventArgs e)
    {
        // https://stackoverflow.com/questions/34168662/wpf-set-textbox-border-color-from-c-sharp-code
        // https://stackoverflow.com/questions/72306766/wpf-how-to-find-a-specific-control-in-an-itemscontrol-with-data-binding
        var container = (ItemsControl)sender;
        var itemContainer = container.ContainerFromElement((FrameworkElement)e.OriginalSource);
        if (itemContainer == null) return;
        
        // HighSchool item
        var item = container.ItemContainerGenerator.ItemFromContainer(itemContainer);
        if (item == null) return;
        
        // Obarvení borderu, vím, že tam vždy bude
        if (LastBorder != null)
        {
            LastBorder.BorderBrush = System.Windows.Media.Brushes.Black;
        }
        var index = container.ItemContainerGenerator.IndexFromContainer(itemContainer);
        var cp = container.ItemContainerGenerator.ContainerFromIndex(index) as ContentPresenter;
        var border = cp!.ContentTemplate.FindName("ItemBorder", cp) as Border;
        border!.BorderBrush = System.Windows.Media.Brushes.Red;
        LastBorder = border;
   
        HighSchoolChosen = (HighSchool)item;
    }

    public void ShowPrograms(object sender, RoutedEventArgs e)
    {
        if (HighSchoolChosen == null) return;
        
        MainWindow.ProgramsContent.SetPrograms(HighSchoolChosen);
        MainWindow.MainWindowRef!.Container.Content = MainWindow.ProgramsContent;
        MainWindow.LastUserControl!.Enqueue(this);
    }

    public void DeleteSchool(object sender, RoutedEventArgs e)
    {
        if (HighSchoolChosen == null) return;

        CustomDb.DeleteObjectFromDb<HighSchool>(HighSchoolChosen.Id);
        Schools.Remove(HighSchoolChosen);
        HighSchoolChosen = null;
    }

    public void ShowApplications(object sender, RoutedEventArgs e)
    {
        if (HighSchoolChosen == null) return;
        
        // TODO: This is not correct, JOIN returns applications that are not part of the given StudyPrgraom from given HighSchool
        Applications = CustomDb.GetObjectsFromDb<Application>(joinAfter: $"JOIN FORM JOIN StudyProgram on StudyProgram.HighSchoolId = {HighSchoolChosen.Id}");
        MainWindow.ApplicationsContent.SetApplications(Applications);
        MainWindow.MainWindowRef!.Container.Content = MainWindow.ApplicationsContent;
        MainWindow.LastUserControl!.Enqueue(this);
    }
    
    public SchoolsControl()
    {
        var schools = CustomDb.GetObjectsFromDb<HighSchool>();
        Schools = schools == null ? new ObservableCollection<HighSchool>() : new ObservableCollection<HighSchool>(schools);
        
        this.DataContext = this;
        
        InitializeComponent();
    }
}