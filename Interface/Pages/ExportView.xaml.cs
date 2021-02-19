using Interface.Exporting;
using Interface.Interfaces;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ExportView.xaml
    /// </summary>
    public partial class ExportView : Page
    {
        private BackgroundWorker _bg;

        private List<int> _numbersToExport;

        public IFrameNavigation FrameWindow { get; set; }



        #region MessageQueueProperty
        public SnackbarMessageQueue MessageQueue
        {
            get { return (SnackbarMessageQueue)GetValue(MessageQueueProperty); }
            set { SetValue(MessageQueueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageQueue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageQueueProperty =
            DependencyProperty.Register("MessageQueue", typeof(SnackbarMessageQueue), typeof(ExportView), new PropertyMetadata(new SnackbarMessageQueue(TimeSpan.FromSeconds(3), Application.Current.Dispatcher)));
        #endregion

        #region IsShowingProgressProperty

        public bool IsShowingProgress
        {
            get { return (bool)GetValue(IsShowingProgressProperty); }
            set { SetValue(IsShowingProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowingProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowingProgressProperty =
            DependencyProperty.Register("IsShowingProgress", typeof(bool), typeof(ExportView), new PropertyMetadata(false));


        #endregion

        #region CanShowOptionsProperty

        public bool CanShowOptions
        {
            get { return (bool)GetValue(CanShowOptionsProperty); }
            set { SetValue(CanShowOptionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowingProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanShowOptionsProperty =
            DependencyProperty.Register("CanShowOptions", typeof(bool), typeof(ExportView), new PropertyMetadata(true));


        #endregion

        public ExportView(IFrameNavigation frameWindow, List<int>numbersToExport )
        {
            InitializeComponent();
            InstanceBackgroundWorker();
            _numbersToExport = numbersToExport;
            FrameWindow = frameWindow;
            DataContext = this;

        }
        private void InstanceBackgroundWorker()
        {
            _bg = new BackgroundWorker();
            _bg.DoWork += WorkerDoWork;
            _bg.RunWorkerCompleted += WorkerRunWorkerCompleted;
            _bg.WorkerReportsProgress = true;


        }
        private void ExportTxt(object sender, RoutedEventArgs e)
        {
            ExportInterface exportUtility;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {

                sfd.Title = "Salvar arquivo txt";
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.DefaultExt = ".txt";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;


                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    exportUtility = new ExportInterface(_numbersToExport, ExportInterface.FileExtension.Txt, sfd.FileName);

                    CanShowOptions = false;
                    IsShowingProgress = true;
                    _bg.RunWorkerAsync(exportUtility);

                    

                }
            }

        }
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            ExportInterface export = (ExportInterface)e.Argument;
            e.Result = export.FilePath;
            export.Export();
        }
        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CanShowOptions = true;
            IsShowingProgress = false;
            MessageQueue.Enqueue($"O arquivo '{new FileInfo((string)e.Result).Name}' foi criado!");
           
        }
        private void GoBackToMenu(object sender, RoutedEventArgs e)
        {
            FrameWindow.Frame.Navigate(new MenuView(FrameWindow));
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            _bg.Dispose();
            _numbersToExport = null;
        }
    }
}
