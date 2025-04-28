using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using System.Windows;
using System.Windows.Media;
using XamlMath;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SumOperation equation;

        public MainWindow()
        {
            InitializeComponent();
            formulaWraper.Formula = @"\color{red}{|} -10  \color{green}{|}"
;
            formulaWraper.FontSize = 30;  // Font
            formulaWraper.FontFamily = new System.Windows.Media.FontFamily("Arial");  // Font
            formulaWraper.Foreground = new SolidColorBrush(Colors.Black);  // Farba textu


            equation = new SumOperation
            (
                new List<Term>() { 
                    new SubOperation(new List<Term>() 
                    {
                        new AbsoluteMember(1) { IsSelected = true},
                        new AbsoluteMember(5)
                    }),
                    new NthRootOperation
                    (
                        5,
                        new List<Term>()
                        {
                            new AbsoluteMember(5646)
                        }
                    ),
                    new AbsoluteMember(2)
                }
            );
            var sub = equation.InnerTerms.ElementAt(0);
            var firts = sub.InnerTerms.ElementAt(0);

            var result = equation.GetLateX();
            formulaWraper.Formula = result;
        }

        private void WindowsFormsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            
        }

        private void FormulaWrapper_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e) // +
        {
            
        }
    }
}