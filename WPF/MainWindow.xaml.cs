using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Project_Data;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    /// using System.Collections.Generic;
    public partial class MainWindow
    {
        // This must be at the TOP
        public static CustomDatabase MyDatabase { get; set; } = new();
        public static Queue<UserControl> LastUserControl { get; set; } = new();

        public static SchoolsControl SchoolsContent = new();
        public static StudyProgramsControl ProgramsContent = new();
        public static ApplicationsControl ApplicationsContent = new();
        public static MainWindow? MainWindowRef { get; set; }


        private void BackButtonPressed(object sender, RoutedEventArgs e)
        {
            if (LastUserControl.Count > 0)
            {
                this.Container.Content = LastUserControl.Dequeue();
                ApplicationsControl.ApplicationChosen = null;
            }
        }

        private async void ButtonCreateReport(object sender, RoutedEventArgs e)
        {
            await CreateReport();
        }

        private async Task CreateReport()
        {
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ReportWindow window = new();

                window.Show();
            });
        }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowRef = this;

            Container.Content = SchoolsContent;
            this.DataContext = this;
        }
    }
}