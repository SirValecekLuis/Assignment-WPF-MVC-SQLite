﻿using System;
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
    
    public partial class MainWindow
    {
        
        public static SchoolsControl SchoolsContent = new();
        public static StudyProgramsControl ProgramsContent = new();
        public static ApplicationsControl ApplicationsContent = new();
        public static MainWindow? MainWindowRef { get; set; }
        public static Queue<UserControl>? LastUserControl { get; set; }

        public void BackButtonPressed(object sender, RoutedEventArgs e)
        {
            if (LastUserControl!.Count > 0)
            {
                this.Container.Content = LastUserControl.Dequeue();
                ApplicationsControl.ApplicationChosen = null;
            }
        }
        
        public MainWindow()
        {
            LastUserControl = new Queue<UserControl>();
            InitializeComponent();
            
            MainWindowRef = this;
            
            Container.Content = SchoolsContent;
            this.DataContext = this;
        }
    }
}