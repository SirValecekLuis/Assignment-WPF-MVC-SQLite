using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Project_Data;

namespace WPF;

public partial class StudyProgramsControl
{
    public static ObservableCollection<StudyProgram> Programs { get; set; } = new();
    
    private Border? LastBorder { get; set; }
    public StudyProgram? StudyProgramChosen { get; set; }

    public void SetPrograms(HighSchool selectedHighSchool)
    {
        List<StudyProgram>? studyPrograms = MainWindow.MyDatabase.GetObjectsFromDb<StudyProgram>(joinAfter: $"where HighSchoolID = {selectedHighSchool.Id}");
        Programs.Clear();

        if (studyPrograms == null) return;
        
        foreach (var program in studyPrograms)
        {
            Programs.Add(program);
        }
    }

    public void AddProgram(object sender, RoutedEventArgs e)
    {
        var window = new AddProgramDialogWindow();
        window.ShowDialog();
    }

    public void DeleteProgram(object sender, RoutedEventArgs e)
    {
        if (StudyProgramChosen == null) return;

        MainWindow.MyDatabase.DeleteObjectFromDb<StudyProgram>(StudyProgramChosen.Id);
        Programs.Remove(StudyProgramChosen);
        StudyProgramChosen = null;
    }

    public void DoubleClickProgram(object sender, RoutedEventArgs e)
    {
        // https://stackoverflow.com/questions/34168662/wpf-set-textbox-border-color-from-c-sharp-code
        // https://stackoverflow.com/questions/72306766/wpf-how-to-find-a-specific-control-in-an-itemscontrol-with-data-binding
        var container = (ItemsControl)sender;
        var itemContainer = container.ContainerFromElement((FrameworkElement)e.OriginalSource);
        if (itemContainer == null) return;
        
        // StudyProgram item
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
   
        StudyProgramChosen = (StudyProgram)item;
    }
    
    public StudyProgramsControl()
    {
        this.DataContext = this;
        
        InitializeComponent();
    }
}