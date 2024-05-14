using System.Windows;
using Project_Data;

namespace WPF;

public partial class AddSchoolDialogWindow
{
    public AddSchoolDialogWindow()
    {
        InitializeComponent();
    }
    

    public void AddSchoolToDb(object sender, RoutedEventArgs e)
    {
        if (TextBoxName.Text == "" || TextBoxAddress.Text == "")
        {
            InfoLabel.Content = "Není vyplněno jméno a adresa!";
        }
        
        HighSchool highSchool = new();
        highSchool.Id = MainWindow.MyDatabase.GetNextIdFromDb<HighSchool>();
        highSchool.Name = TextBoxName.Text;
        highSchool.Address = TextBoxAddress.Text;

        MainWindow.MyDatabase.InsertObjectToDb(highSchool);
        InfoLabel.Content = "Škola úspěšně přidána!";
        
        SchoolsControl.Schools.Add(highSchool);
    }
}