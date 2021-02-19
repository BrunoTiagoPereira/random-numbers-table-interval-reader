using Interface.Interfaces;
using Models;
using Models.Validations;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FileSelect.xaml
    /// </summary>
    public partial class FileSelect : Page
    {
        public IFrameNavigation FrameWindow { get; set; }

        public FileSelect(IFrameNavigation frame)
        {
          
            InitializeComponent();
            FrameWindow = frame;
        }

        
           

          


        
    }
}
