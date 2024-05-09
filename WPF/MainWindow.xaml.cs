using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Project_Data;
using WPF;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    /// using System.Collections.Generic;
    
    public partial class MainWindow : Window
    {
        
        public static SchoolsControl Schools = new SchoolsControl();
        public static StudyProgramsControl Programs = new StudyProgramsControl();
        public static MainWindow MainWindowRef { get; set; } = null!;
        public static List<UserControl> LastUserControl { get; set; } = null!;

        public MainWindow()
        {
            LastUserControl = new List<UserControl>();
            InitializeComponent();
            
            MainWindowRef = this;
            
            Container.Content = Schools;
            this.DataContext = this;
        }
    }
}