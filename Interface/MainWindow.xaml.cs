using Interface.Interfaces;
using Interface.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IFrameNavigation
    {
        public Frame Frame { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Frame = MainFrame;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MenuView(this));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}