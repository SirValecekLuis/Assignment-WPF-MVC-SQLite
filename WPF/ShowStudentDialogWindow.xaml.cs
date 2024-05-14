using Project_Data;

namespace WPF;

public partial class ShowStudentDialogWindow
{
    // Student je nastaven předtím, než je vytvořena instance této třídy, jinak se instance nevytvoří.
    public static Student StudentChosen { get; set; } = null!;


    public ShowStudentDialogWindow()
    {
        InitializeComponent();

        this.DataContext = this;
    }
}