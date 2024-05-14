using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Project_Data;
using Application = Project_Data.Application;

namespace WPF;

public partial class ApplicationsControl
{
    public static ObservableCollection<Application> Applications { get; set; } = null!;

    public static Application? ApplicationChosen { get; set; }
    public Border? LastBorder { get; set; }

    public void SetApplications(List<Application>? applications)
    {
        Applications.Clear();
        if (applications == null) return;

        foreach (var app in applications)
        {
            Applications.Add(app);
        }
    }

    public void DoubleClickApplication(object sender, RoutedEventArgs e)
    {
        // https://stackoverflow.com/questions/34168662/wpf-set-textbox-border-color-from-c-sharp-code
        // https://stackoverflow.com/questions/72306766/wpf-how-to-find-a-specific-control-in-an-itemscontrol-with-data-binding
        var container = (ItemsControl)sender;
        var itemContainer = container.ContainerFromElement((FrameworkElement)e.OriginalSource);
        if (itemContainer == null) return;

        // Application item
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

        ApplicationChosen = (Application)item;
    }

    public void DeleteApplication(object sender, RoutedEventArgs e)
    {
        if (ApplicationChosen == null) return;

        var forms = MainWindow.MyDatabase.GetObjectsFromDb<Form>(joinAfter:$"WHERE FORM.ApplicationId = {ApplicationChosen.Id}");

        if (forms != null)
        {
            foreach (var form in forms)
            {
                Console.WriteLine(form.ApplicationId + " : " + form.StudyProgramId);
                MainWindow.MyDatabase.DeleteObjectFromDb<Form>(joinAfter:$"WHERE Form.ApplicationId = {ApplicationChosen.Id}");
            }
        }
        
        MainWindow.MyDatabase.DeleteObjectFromDb<Application>(ApplicationChosen.Id);
        Applications.Remove(ApplicationChosen);
        ApplicationChosen = null;
    }

    public void ShowStudent(object sender, RoutedEventArgs e)
    {
        if (ApplicationChosen == null) return;

        var student = MainWindow.MyDatabase.GetObjectsFromDb<Student>(joinAfter:$"WHERE ApplicationId = {ApplicationChosen.Id}");

        if (student == null) return;

        ShowStudentDialogWindow.StudentChosen = student[0];
                    
        var showStudentDialog = new ShowStudentDialogWindow();
        showStudentDialog.ShowDialog();
    }

    public void EditApplication(object sender, RoutedEventArgs e)
    {
        if (ApplicationChosen == null) return;
        
        var student = MainWindow.MyDatabase.GetObjectsFromDb<Student>(joinAfter:$"WHERE ApplicationId = {ApplicationChosen.Id}");
        
        if (student == null) return;

        EditApplicationDialogWindow.StudentChosen = student[0];
        
        var editApplicationDialog = new EditApplicationDialogWindow();
        editApplicationDialog.ShowDialog();
    }

    public ApplicationsControl()
    {
        Applications = new ObservableCollection<Application>();

        this.DataContext = this;

        InitializeComponent();
    }
}