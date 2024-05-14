using System.Windows;
using Project_Data;


namespace WPF;

public partial class EditApplicationDialogWindow
{
    
    // Tato property je nastavena před vytvořením dialogového okna
    public static Student StudentChosen { get; set; } = null!;

    public void FillInformation()
    {
        TextBoxName.Text = StudentChosen.Name;
        TextBoxAddress.Text = StudentChosen.Address;
        TextBoxPhoneNumber.Text = StudentChosen.PhoneNumber;
        TextBoxBirthNumber.Text = StudentChosen.BirthNumber;
    }

    public void SaveStudent(object sender, RoutedEventArgs e)
    {
        var name = TextBoxName.Text;
        var addr = TextBoxAddress.Text;
        var phone = TextBoxPhoneNumber.Text;
        var birth = TextBoxBirthNumber.Text;
        
        if (name == "" || addr == "" || phone == "" || birth == "")
        {
            InfoLabel.Content = "Všechna pole musí být vyplněna!";
            return;
        }

        Student student = new(StudentChosen.Id, name, addr, phone, birth, StudentChosen.ApplicationId);
        MainWindow.MyDatabase.UpdateObjectInDb(student);
        
        InfoLabel.Content = "Údaje o studentovi úspěšně změněny!";
    }

    public void CloseAndDontSave(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    public EditApplicationDialogWindow()
    {
        InitializeComponent();
        FillInformation();

        this.DataContext = this;
    }
}