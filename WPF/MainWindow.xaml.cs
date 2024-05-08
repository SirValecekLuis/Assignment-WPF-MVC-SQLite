using System;
using System.Collections.ObjectModel;
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
    public partial class MainWindow : Window
    {
        public static ObservableCollection<HighSchool> Schools { get; set; }

        public void AddSchool(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddSchool();
            newWindow.Width = this.ActualWidth;
            newWindow.Height = this.ActualHeight;
            newWindow.Top = this.Top;
            newWindow.Left = this.Left;
            newWindow.ShowDialog();
        }

        public void AddStudyProgram(HighSchool highSchool)
        {
            WPF.AddStudyProgram.SelectedHighSchool = highSchool;
            var newWindow = new AddStudyProgram();
            newWindow.Width = this.ActualWidth;
            newWindow.Height = this.ActualHeight;
            newWindow.Top = this.Top;
            newWindow.Left = this.Left;
            newWindow.ShowDialog();
        }
                        
        public void DoubleClickSchool(object sender, RoutedEventArgs e)
        {
            var container = (ItemsControl)sender;
            var item = container.ItemContainerGenerator.ItemFromContainer(container.ContainerFromElement((FrameworkElement)e.OriginalSource));

            if (item == null) return;
            HighSchool highSchool = (HighSchool)item;

            AddStudyProgram(highSchool);
        }

        public MainWindow()
        {
            InitializeComponent();
            var schools = CustomDb.GetObjectsFromDb<HighSchool>();
            
            Schools = schools == null ? new ObservableCollection<HighSchool>() : new ObservableCollection<HighSchool>(schools);

            this.DataContext = this;
        }
    }
}