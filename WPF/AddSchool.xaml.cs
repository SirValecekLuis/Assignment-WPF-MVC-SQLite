using System;
using System.Windows;
using Project_Data;
using static WPF.MainWindow;

namespace WPF;

public partial class AddSchool : Window
{
    public AddSchool()
    {
        InitializeComponent();
    }

    public void AddSchoolToDb(object sender, RoutedEventArgs e)
    {
        if (TextBoxName.Text == "" || TextBoxAddress.Text == "")
        {
            InfoLabel.Content = "Není vyplněo jméno a adresa!";
        }
        
        HighSchool highSchool = new();
        highSchool.Id = CustomDb.GetCountFromDb<HighSchool>() + 1;
        highSchool.Name = TextBoxName.Text;
        highSchool.Address = TextBoxAddress.Text;

        CustomDb.InsertObjectToDb(highSchool);
        InfoLabel.Content = "Škola úspěšně přidána!";
        
        WPF.MainWindow.Schools.Add(highSchool);
    }
}