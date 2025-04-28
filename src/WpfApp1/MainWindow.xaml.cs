using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            formulaWraper.Formula = @"\frac{a}{b} + \sqrt{c^2 + d^2} = e";
            formulaWraper.FontSize = 30;  // Font
            formulaWraper.FontFamily = new System.Windows.Media.FontFamily("Arial");  // Font
            formulaWraper.Foreground = new SolidColorBrush(Colors.Blue);  // Farba textu
            formulaWraper.Background = new SolidColorBrush(Colors.Yellow); // Farba pozadia
        }

        private void WindowsFormsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            
        }

        private void FormulaWrapper_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}