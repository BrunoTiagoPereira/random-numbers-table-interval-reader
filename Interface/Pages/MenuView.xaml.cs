using Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuView : Page
    {

        public int ValueLimit
        {
            get { return (int)GetValue(ValueLimitProperty); }
            set { SetValue(ValueLimitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueLimit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueLimitProperty =
            DependencyProperty.Register("ValueLimit", typeof(int), typeof(MenuView), new PropertyMetadata(0));


        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(MenuView), new PropertyMetadata(0));



        public int NumbersAmount
        {
            get { return (int)GetValue(NumbersAmountProperty); }
            set { SetValue(NumbersAmountProperty, value); }
        }


        public static readonly DependencyProperty NumbersAmountProperty =
            DependencyProperty.Register("NumbersAmount", typeof(int), typeof(MenuView), new PropertyMetadata(0));


        public IFrameNavigation FrameWindow  { get; set; }

        public MenuView(IFrameNavigation frameWindow)
        {
            InitializeComponent();

            this.FrameWindow = frameWindow;

            DataContext = this;

            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
