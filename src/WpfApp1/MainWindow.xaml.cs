// TODO: backspace I use as clear all, change to backspace
// Replace 3 with Pi

using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using System.Runtime.CompilerServices;
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
        SumOperation equation = new SumOperation(new List<Term>() { new AbsoluteMember(0) { IsSelected = true } });
        int idx = 0;
        int sub_idx = 0;
        int len = 1;

        public MainWindow()
        {
            InitializeComponent();
            formulaWraper.Formula = @"\color{red}{|} -10  \color{green}{|}"
;
            formulaWraper.FontSize = 30;  // Font
            formulaWraper.FontFamily = new System.Windows.Media.FontFamily("Arial");  // Font
            formulaWraper.Foreground = new SolidColorBrush(Colors.Black);  // Farba textu

            formulaWraper.Formula = equation.GetLateX();
        }

        private void WindowsFormsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void FormulaWrapper_Loaded(object sender, RoutedEventArgs e)
        {

        }


        /*
         * Buttons
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var senderToNumber = new Dictionary<object, int>
            {
                { _0, 0 },
                { _1, 1 },
                { _2, 2 },
                { _3, 3 },
                { _4, 4 },
                { _5, 5 },
                { _6, 6 },
                { _7, 7 },
                { _8, 8 },
                { _9, 9 }
            };

            // Add number
            if (senderToNumber.TryGetValue(sender, out int number) || sender.Equals(Pi))
            {
                if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                {
                    if (equation.InnerTerms[idx].InnerTerms[sub_idx].GetType() == typeof(EmptyMember))
                    {
                        if (sender.Equals(Pi))
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(3) { IsSelected = true };
                        }
                        else
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(number) { IsSelected = true };
                        }

                        update();
                        return;
                    }

                    var past_num_str = equation.InnerTerms[idx].InnerTerms[sub_idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "");
                    if (int.TryParse(past_num_str, out int new_num))
                    {
                        new_num *= 10;
                        if (sender.Equals(Pi))
                        {
                            new_num += 3;
                        }
                        else
                        {
                            new_num += number;
                        }

                        equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(new_num) { IsSelected = true };
                        len++;
                    }
                }
                else
                {
                    var past_num_str = equation.InnerTerms[idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "");
                    if (int.TryParse(past_num_str, out int new_num))
                    {
                        new_num *= 10;
                        if (sender.Equals(Pi))
                        {
                            new_num += 3;
                        }
                        else
                        {
                            new_num += number;
                        }
                        equation.InnerTerms[idx] = new AbsoluteMember(new_num) { IsSelected = true };
                        len++;
                    }
                }
            }

            // Apply changes
            if (!sender.Equals(Equals))
            {
                update();
            }
        }

        void update()
        {
            formulaWraper.Formula = equation.GetLateX();
        }
    }
}