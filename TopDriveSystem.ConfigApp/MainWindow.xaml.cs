using System;
using System.ComponentModel;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using MahApps.Metro.Controls;

namespace TopDriveSystem.ConfigApp
{
    public partial class MainWindow : MetroWindow
    {
        private readonly IThreadNotifier _appMainThreadNotifier;
        private readonly Action _closeOtherWindows;

        public MainWindow(IThreadNotifier appMainThreadNotifier, Action closeOtherWindows)
        {
            _appMainThreadNotifier = appMainThreadNotifier;
            _closeOtherWindows = closeOtherWindows;
            InitializeComponent();
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            _closeOtherWindows.Invoke();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            _appMainThreadNotifier.Notify(() => { Application.Current.Shutdown(); });
        }
    }
}