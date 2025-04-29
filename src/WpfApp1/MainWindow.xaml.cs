// TODO: backspace I use as clear all, change to backspace
// Replace 3 with Pi
// Ability to add - / * before any term
// Right arrow can go over len

using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
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

                    String past_num_str = equation.InnerTerms[idx].InnerTerms[sub_idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "");
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
                    String past_num_str = equation.InnerTerms[idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "");
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
            // Plus
            else if (sender.Equals(Plus))
            {
                equation.InnerTerms.Add(new SumOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true }
                }));

                unselectOnNew();
            }
            // Minus
            else if (sender.Equals(Minus))
            {
                equation.InnerTerms.Add(new SubOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true }
                }));

                unselectOnNew();
            }
            // Multiply
            else if (sender.Equals(Multiply))
            {
                equation.InnerTerms.Add(new MulOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true },
                        new EmptyMember()
                }));

                unselectOnNew();
            }
            // Absolute
            else if (sender.Equals(Absolute))
            {
                equation.InnerTerms.Add(new AbsOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true }
                }));

                unselectOnNew();
            }
            // Factorial
            else if (sender.Equals(Factorial))
            {
                equation.InnerTerms.Add(new FactorialOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true }
                }));

                unselectOnNew();
            }
            // Modulo
            else if (sender.Equals(Modulo))
            {
                equation.InnerTerms.Add(new ModuloOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true },
                        new EmptyMember()
                }));

                unselectOnNew();
            }
            // Square Root 2
            else if (sender.Equals(Square_Root_2))
            {
                equation.InnerTerms.Add(new NthRootOperation
                     (
                         2,
                         new List<Term>()
                         {
                             new EmptyMember(){ IsSelected = true },
                         }
                     ));

                unselectOnNew();
            }
            // Power of 2
            else if (sender.Equals(Power_2))
            {
                if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                {
                    if (equation.InnerTerms[idx].InnerTerms[sub_idx].GetType() != typeof(EmptyMember)){
                        String past_num_str = equation.InnerTerms[idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "");
                        if (int.TryParse(past_num_str, out int new_num))
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx] = new PowerOperation(2,new List<Term>(){new AbsoluteMember(new_num){ IsSelected = true }});
                        }
                    }
                    else
                    {
                        equation.InnerTerms[idx].InnerTerms[sub_idx] = new PowerOperation(2, new List<Term>() { new EmptyMember() { IsSelected = true } });
                    }
                }
                else
                {
                    equation.InnerTerms.Add(new PowerOperation(2, new List<Term>() { new EmptyMember() { IsSelected = true } }));

                    unselectOnNew();
                }
            }
            // Left Arrow
            else if (sender.Equals(Left_Arrow))
            {
                if (idx <= 0)
                {
                    return;
                }

                if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                {
                    // Move in term itself
                    if (sub_idx == 1)
                    {
                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;
                        sub_idx = 0;
                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                    }
                    // Move out of term
                    else
                    {
                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                        idx--;
                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                        {
                            sub_idx = 0;
                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                        }
                        else
                        {
                            equation.InnerTerms[idx].IsSelected = true;
                        }
                    }
                }
                else
                {
                    equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                    idx--;
                    if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                    {
                        sub_idx = 0;
                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                    }
                    else
                    {
                        equation.InnerTerms[idx].IsSelected = true;
                    }
                }
            }
            // Right Arrow
            else if (sender.Equals(Right_Arrow))
            {
                if (idx >= len)
                {
                    return;
                }
                if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                {
                    if (idx + 1 < len)
                    {
                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;
                        idx++;

                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                        {
                            sub_idx = 0;
                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                        }
                        else
                        {
                            equation.InnerTerms[idx].IsSelected = true;
                        }
                    }
                }
                else if (idx + 1 < len)
                {
                    equation.InnerTerms[idx].IsSelected = false;
                    idx++;
                    if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                    {
                        sub_idx = 0;
                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                    }
                    else
                    {
                        equation.InnerTerms[idx].IsSelected = true;
                    }
                }
            }
            // Backspace
            else if (sender.Equals(Backspace))
            {
                if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                {
                    if (equation.InnerTerms[idx].InnerTerms[sub_idx].GetType() != typeof(EmptyMember))
                    {
                        String past_num_str = equation.InnerTerms[idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "");
                        past_num_str = past_num_str.Substring(0, past_num_str.Length - 1);

                        if (past_num_str.Length != 0 && !char.IsDigit(past_num_str[0]))
                        {
                            past_num_str = past_num_str.Substring(1);
                        }

                        if (string.IsNullOrWhiteSpace(past_num_str) || past_num_str.Length == 0)
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx] = new EmptyMember { IsSelected = true };
                        }
                        else if (int.TryParse(past_num_str, out int new_num))
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(new_num) { IsSelected = true };
                        }
                    }
                    else
                    {
                        equation.InnerTerms.RemoveAt(idx);
                        idx--;
                        len--;

                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                        }
                        else
                        {
                            equation.InnerTerms[idx].IsSelected = true;
                        }
                    }
                }
                else
                {
                    String past_num_str = equation.InnerTerms[idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "");
                    if (past_num_str == "0")
                    {
                        return;
                    }

                    past_num_str = past_num_str.Substring(0, past_num_str.Length - 1);

                    if (string.IsNullOrWhiteSpace(past_num_str) || past_num_str.Length == 0)
                    {
                        equation.InnerTerms[idx] = new AbsoluteMember(0) { IsSelected = true };
                    }
                    else if (int.TryParse(past_num_str, out int new_num))
                    {
                        equation.InnerTerms[idx] = new AbsoluteMember(new_num) { IsSelected = true };
                    }
                }

                update();
            }
            // Equals
            else if (sender.Equals(Equals))
            {
                var result = equation.GetResult();
                formulaWraper.Formula = result.ToString();
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
        void unselectOnNew()
        {
            if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember))
            {
                equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;
            }
            else
            {
                equation.InnerTerms[idx].IsSelected = false;
            }

            sub_idx = 0;
            idx++;
            len++;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Dot, Modulo, Factorial, Brackets
            if (e.Key == Key.OemPeriod)
                Dot.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D5 && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                Modulo.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D1 && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                Factorial.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D9 && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                Left_Bracket.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D0 && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                Right_Bracket.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

            // Numbers
            else if (e.Key == Key.D0 || e.Key == Key.NumPad0)
                _0.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D1 || e.Key == Key.NumPad1)
                _1.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
                _2.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
                _3.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
                _4.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
                _5.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
                _6.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
                _7.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D8 || e.Key == Key.NumPad8)
                _8.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.D9 || e.Key == Key.NumPad9)
                _9.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

            // Arrows
            else if (e.Key == Key.Left)
                Left_Arrow.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.Right)
                Right_Arrow.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

            // Plus, Minus, Multiply, Dividi
            else if (e.Key == Key.Add)
                Plus.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.Subtract)
                Minus.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.Multiply)
                Multiply.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.Divide)
                Divide.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

            // Backspace
            else if (e.Key == Key.Back)
                Backspace.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

        }
    }
}