using Interface.Interfaces;
using Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Application = System.Windows.Application;

namespace Interface.Pages
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : Page
    {
        private BackgroundWorker _bg;

        public IFrameNavigation FrameWindow { get; set; }
        public RNT Rnt { get; set; }

        public ObservableCollection<int> NumbersResult { get; set; }

        #region IsReady Property

        public bool IsReady
        {
            get { return (bool)GetValue(IsReadyProperty); }
            set { SetValue(IsReadyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReady.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadyProperty =
            DependencyProperty.Register("IsReady", typeof(bool), typeof(GridView), new PropertyMetadata(false));

        #endregion IsReady Property

        #region IsShowingProgressProperty

        public bool IsShowingProgress
        {
            get { return (bool)GetValue(IsShowingProgressProperty); }
            set { SetValue(IsShowingProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowingProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowingProgressProperty =
            DependencyProperty.Register("IsShowingProgress", typeof(bool), typeof(GridView), new PropertyMetadata(true));

        #endregion IsShowingProgressProperty

        public GridView(IFrameNavigation frameWindow, RNT rnt)
        {
            InitializeComponent();
            InstanceBackgroundWorker();

            NumbersResult = new ObservableCollection<int>();

            FrameWindow = frameWindow;
            Rnt = rnt;
            DataContext = this;
        }

        private void InstanceBackgroundWorker()
        {
            _bg = new BackgroundWorker();
            _bg.DoWork += WorkerDoWork;
            _bg.RunWorkerCompleted += WorkerRunWorkerCompleted;
            _bg.WorkerReportsProgress = true;
        }

        private void OnLoad(object sender, RoutedEventArgs e) => _bg.RunWorkerAsync();

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            List<int> result = Rnt.GetNumbersList();
            foreach (int num in result)
            {
                Application.Current.Dispatcher.Invoke(() => { NumbersResult.Add(num); });
            }
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsShowingProgress = false;
            IsReady = true;
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            FrameWindow.Frame.Navigate(new ExportView(FrameWindow, NumbersResult.ToList()));
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            FrameWindow.Frame.Navigate(new MenuView(FrameWindow));
        }

        private void UnLoad(object sender, RoutedEventArgs e)
        {
            Rnt = null;
            NumbersResult = null;
            _bg.Dispose();
        }
    }
}