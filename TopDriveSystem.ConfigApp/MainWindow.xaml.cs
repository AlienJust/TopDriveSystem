using System;
using System.ComponentModel;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using MahApps.Metro.Controls;

namespace TopDriveSystem.ConfigApp
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}