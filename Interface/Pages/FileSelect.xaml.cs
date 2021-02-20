using Interface.Interfaces;
using System.Windows.Controls;

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