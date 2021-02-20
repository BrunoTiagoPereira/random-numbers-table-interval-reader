using Interface.Interfaces;
using Interface.Validations;
using Interface.ViewModels;
using Models;
using Models.Validations;
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
using MessageBox = System.Windows.MessageBox;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Interface.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuView : Page
    {

        public MenuViewModel MenuModel { get; set; }

        public IFrameNavigation FrameWindow { get; set; }

        private BackgroundWorker _bg;


        #region IsFileSelectedProperty
        public bool IsFileSelected
        {
            get { return (bool)GetValue(IsFileSelectedProperty); }
            set { SetValue(IsFileSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFileSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFileSelectedProperty =
            DependencyProperty.Register("IsFileSelected", typeof(bool), typeof(MenuView), new PropertyMetadata(false));
        #endregion

        #region IsMainGridEnableProperty
        public bool IsMainGridEnable
        {
            get { return (bool)GetValue(IsMainGridEnableProperty); }
            set { SetValue(IsMainGridEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMainGridEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMainGridEnableProperty =
            DependencyProperty.Register("IsMainGridEnable", typeof(bool), typeof(MenuView), new PropertyMetadata(true));

        #endregion

        #region IsShowingProgressProperty
        public bool IsShowingProgress
        {
            get { return (bool)GetValue(IsShowingProgressProperty); }
            set { SetValue(IsShowingProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowingProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowingProgressProperty =
            DependencyProperty.Register("IsShowingProgress", typeof(bool), typeof(MenuView), new PropertyMetadata(false));
    
        #endregion



        public MenuView(IFrameNavigation frameWindow)
        {
            InitializeComponent();

            MenuModel = new MenuViewModel();

            this.FrameWindow = frameWindow;

            DataContext = this;

            InstanceBackgroundWorker();


        }

       

        private void InstanceBackgroundWorker()
        {
            _bg = new BackgroundWorker();
            _bg.DoWork += WorkerDoWork;
            _bg.RunWorkerCompleted += WorkerRunWorkerCompleted;

        }

      
        private void ChooseFile(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
              
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;


                FileValidation fileValidation = new FileValidation();
                ValidationResult result = new ValidationResult();
                string errorMessage;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    result = fileValidation.Validate(new FileInfo(openFileDialog.FileName));

                    if (!result.IsValid)
                    {
                        errorMessage = result.Errors
                            .Select(err => err.ErrorMessage)
                            .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");

                        MessageBox.Show(errorMessage);
                        return;
                    }

                    MenuModel.SelectedFile = new FileInfo(openFileDialog.FileName);

                    IsFileSelected = true;
                }
            }

        }

        private void Start(object sender, RoutedEventArgs e)
        {

        
            IsMainGridEnable = false;
            IsShowingProgress = true;
            _bg.RunWorkerAsync();
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsMainGridEnable = true;
            IsShowingProgress = false;

            if (e.Result is string)
            {
                MessageBox.Show(e.Result.ToString(), "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else ShowResults((RNT)e.Result);
        }



        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {

            MenuViewModelValidation validator = new MenuViewModelValidation();
            ValidationResult result = validator.Validate(MenuModel);
            string errorMessage;
            int startColumnIndex;
            int startRowIndex;


            if (!result.IsValid)
            {
                errorMessage = result
                    .Errors
                    .Select(err => err.ErrorMessage)
                    .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");

                e.Result = errorMessage;
                return;

            }

            try
            {

                startColumnIndex = (!string.IsNullOrWhiteSpace(this.MenuModel.StartColumnIndex)) ? int.Parse(MenuModel.StartColumnIndex) : 0;
                startRowIndex = (!string.IsNullOrWhiteSpace(this.MenuModel.StartRowIndex)) ? int.Parse(MenuModel.StartRowIndex) : 0;

                e.Result =  new RNT(MenuModel.SelectedFile, int.Parse(MenuModel.NumbersAmount), int.Parse(MenuModel.Interval), int.Parse(MenuModel.ValueLimit),startColumnIndex,startRowIndex);
            }
            catch (InvalidDataException ex)
            {

                e.Result = ex.Message;
            }
        }

        private void ShowResults(RNT rnt)
        {
            FrameWindow.Frame.Navigate(new GridView(FrameWindow, rnt));
        }

        private void UnLoad(object sender, RoutedEventArgs e)
        {
            MenuModel = null;
            _bg.Dispose();
        }
    }
}
