using Interface.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace Interface.Pages
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : Page
    {
        private BackgroundWorker _bg;

        private BackgroundWorker _export;

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
        #endregion

        #region IsShowingProgressProperty


        public bool IsShowingProgress
        {
            get { return (bool)GetValue(IsShowingProgressProperty); }
            set { SetValue(IsShowingProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowingProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowingProgressProperty =
            DependencyProperty.Register("IsShowingProgress", typeof(bool), typeof(GridView), new PropertyMetadata(true));


        #endregion


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

            _export = new BackgroundWorker();
            _export.DoWork += ExportDoWork;
            _export.RunWorkerCompleted += ExportRunWorkerCompleted;
            _export.WorkerReportsProgress = true;

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
        private void ExportarExcel(object sender, RoutedEventArgs e)
        {
            
            

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                
                sfd.Title = "Salvar arquivo txt";
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;


                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string fileText = this.NumbersResult.
                            Select(num => num.ToString())
                            .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");

                        File.WriteAllText(sfd.FileName, fileText);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }

        }


        private void ExportDoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void ExportRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            FrameWindow.Frame.Navigate(new MenuView(FrameWindow));
        }

        
    }
}
