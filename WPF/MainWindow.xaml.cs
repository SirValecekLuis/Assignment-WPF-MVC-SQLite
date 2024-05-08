using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project_Data;


namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    /// using System.Collections.Generic;
    public partial class MainWindow : Window
    {
        public static ObservableCollection<HighSchool> Schools { get; set; }

        public void AddSchool(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Cc");
            var newWindow = new AddSchoolForm();
            newWindow.Width = this.ActualWidth;
            newWindow.Height = this.ActualHeight;
            newWindow.Top = this.Top;
            newWindow.Left = this.Left;
            newWindow.ShowDialog();
        }

        public MainWindow()
        {
            var schools = CustomDb.GetObjectsFromDb<HighSchool>();
            if (schools == null)
            {
                Console.WriteLine("School is NONE");
                Schools = new ObservableCollection<HighSchool>();
            }
            else
            {
                Schools = new ObservableCollection<HighSchool>(schools);
            }

            this.DataContext = this;
        }
    }
}