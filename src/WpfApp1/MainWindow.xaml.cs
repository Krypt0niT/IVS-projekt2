using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
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
        bool bool_decimal = false;

        int max_size= 22;
        int current_size = 0;

        bool bool_out = false;

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

        private void FormulaWrapper_Loaded(object sender, RoutedEventArgs e)
        {

        }


        /*
         * Buttons
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Data
                var term = equation.InnerTerms[idx];
                bool isPower = term is PowerOperation;
                bool isComplex = term is not AbsoluteMember && term is not PiConst;
                var subTerm = isComplex ? term.InnerTerms[sub_idx] : null;
                bool isSubTermEmpty = subTerm is EmptyMember;

                string CleanLatex(string s) => s.Replace("\\colorbox{red}{", "").Replace("}", "").Replace("^{2", "").Replace("\\sqrt{", "").Replace("^{\\square", "").Replace("\\sqrt", "").Replace("[\\square]{", "").Replace("\\square", "");

                // Add number
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
                if (current_size < max_size && senderToNumber.TryGetValue(sender, out int number))
                {
                    current_size++;

                    // Logic
                    if (isComplex)
                    {
                        if (isSubTermEmpty)
                        {
                            decimal value = bool_decimal ? Convert.ToDecimal("0." + number) : number;
                            SetValue(term, value, isComplex);
                        }
                        else
                        {
                            string past = "";
                            if (term is PowerOperation pwd)
                            {
                                if (pwd.Exponent.IsSelected && pwd.Exponent is EmptyMember)
                                    past = "0";
                                else
                                    past = CleanLatex(pwd.Exponent.IsSelected ? CleanLatex(pwd.Exponent.GetLateX()) : CleanLatex(pwd.InnerTerms[0].GetLateX()));
                            }
                            else if (term is NthRootOperation sqr)
                            {
                                if (sqr.Degree.IsSelected && sqr.Degree is EmptyMember)
                                    past = "0";
                                else
                                    past = CleanLatex(sqr.Degree.IsSelected ? CleanLatex(sqr.Degree.GetLateX()) : CleanLatex(sqr.InnerTerms[0].GetLateX()));
                            }
                            else
                                past = CleanLatex(subTerm.GetLateX());

                            if (bool_decimal)
                                SetValue(term, Convert.ToDecimal(past + "." + number), isComplex);
                            else if (past.Contains("."))
                            {
                                var parts = past.Split('.');
                                if (decimal.TryParse(parts[1], out decimal behind) && behind < 999_999_999)
                                {
                                    var combined = parts[0] + "." + (behind * 10 + number);
                                    if (decimal.TryParse(combined, out decimal result))
                                        SetValue(term, result, isComplex);
                                }
                            }
                            else if (decimal.TryParse(past, out decimal new_num) && new_num < 999_999_999_999_999_999)
                            {
                                if (new_num < 0)
                                    SetValue(term, new_num * 10 - (decimal) number, isComplex);
                                else
                                    SetValue(term, new_num * 10 + (decimal)number, isComplex);
                            }
                        }
                    }
                    else
                    {
                        string past = CleanLatex(term.GetLateX());

                        if (bool_decimal)
                            SetValue(term, Convert.ToDecimal(past + "." + number), isComplex);
                        else if (past.Contains("."))
                        {
                            var parts = past.Split('.');
                            if (decimal.TryParse(parts[1], out decimal behind) && behind < 999_999_999)
                            {
                                var combined = parts[0] + "." + (behind * 10 + number);
                                if (decimal.TryParse(combined, out decimal result))
                                    SetValue(term, result, isComplex);
                            }
                        }
                        else if (decimal.TryParse(past, out decimal new_num) && new_num < 999_999_999_999_999_999)
                        {
                            if (new_num < 0)
                                SetValue(term, new_num * 10 - (decimal)number, isComplex);
                            else
                                SetValue(term, new_num * 10 + (decimal)number, isComplex);
                        }

                    }
                }
                // Dot
                else if (sender.Equals(Dot))
                {
                    if (isComplex && !isSubTermEmpty && CleanLatex(subTerm.GetLateX()).Contains("."))
                        return;
                    else if (!isComplex && CleanLatex(term.GetLateX()).Contains("."))
                        return;

                    bool_decimal = true;
                    update();
                    return;
                }
                // Plus
                else if (current_size + 3 < max_size && sender.Equals(Plus))
                {
                    current_size += 3;
                    equation.InnerTerms.Add(new SumOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true }
                    }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Minus
                else if (sender.Equals(Minus))
                {
                    if (current_size + 3 < max_size && bool_out)
                    {
                        current_size += 3;
                        equation.InnerTerms.Add(new SubOperation(new List<Term>() {
                        new EmptyMember() { IsSelected = true }
                    }));
                        unselectOnNew();
                    }
                    else if (isComplex)
                    {
                        string past = CleanLatex(subTerm.GetLateX());
                        if (decimal.TryParse(past, out decimal new_num))
                            equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(-new_num) { IsSelected = true };
                    }
                    else
                    {
                        string past = CleanLatex(term.GetLateX());
                        if (decimal.TryParse(past, out decimal new_num))
                            equation.InnerTerms[idx] = new AbsoluteMember(-new_num) { IsSelected = true };
                    }
                        bool_out = false;
                }
                // Multiply
                else if (current_size + 7 < max_size && sender.Equals(Multiply))
                {
                    current_size += 7;
                    equation.InnerTerms.Add(new MulOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true },
                        new EmptyMember()
                    }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Divide
                else if (current_size < max_size && sender.Equals(Divide))
                {
                    current_size+=3;
                    equation.InnerTerms.Add(new DivOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true },
                        new EmptyMember()
                    }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Absolute
                else if (current_size < max_size && sender.Equals(Absolute))
                {
                    current_size+=3;
                    equation.InnerTerms.Add(new AbsOperation(new List<Term>() { new EmptyMember() { IsSelected = true } }));
                        unselectOnNew();
                    bool_out = false;
                }
                // Factorial
                else if (current_size < max_size && sender.Equals(Factorial))
                {
                    current_size+=3;
                    equation.InnerTerms.Add(new FactorialOperation(new List<Term>() { new EmptyMember() { IsSelected = true } }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Modulo
                else if (current_size + 7 < max_size && sender.Equals(Modulo))
                {
                    current_size += 7;
                    equation.InnerTerms.Add(new ModuloOperation(new List<Term>(){
                        new EmptyMember() { IsSelected = true },
                        new EmptyMember()
                    }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Pi
                else if (current_size+2< max_size && sender.Equals(Pi))
                {
                    current_size+=2;
                    equation.InnerTerms.Add(new PiConst() { IsSelected = true });
                        unselectOnNew();
                    bool_out = false;
                }
                // Negation
                else if (current_size+3< max_size && sender.Equals(Negation))
                {
                    String num_str = CleanLatex(term.GetLateX());
                    if (num_str.Length == 0)
                        num_str = "0";

                    if (term is SumOperation)
                        equation.InnerTerms[idx] = new SubOperation(new List<Term>() {
                                new AbsoluteMember(Convert.ToDecimal(num_str)) { IsSelected = true }
                        });
                    else if (term is SubOperation)
                    {
                        // Remove -
                        if (!string.IsNullOrEmpty(num_str) && num_str.Length > 1)
                            num_str = num_str.Substring(1);

                        equation.InnerTerms[idx] = new SumOperation(new List<Term>() {
                            new AbsoluteMember(Convert.ToDecimal(num_str)) { IsSelected = true }
                        });
                    }
                }
                // Square Root of 2
                else if (current_size < max_size && sender.Equals(Square_Root_2))
                {
                    current_size+=3;
                    equation.InnerTerms.Add(new NthRootOperation(new AbsoluteMember(2), new List<Term>() { new EmptyMember() { IsSelected = true } }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Square Root of N
                else if (current_size+3< max_size && sender.Equals(Square_Root))
                {
                    current_size+=3;
                    equation.InnerTerms.Add(new NthRootOperation(new EmptyMember(), new List<Term>() { new EmptyMember() { IsSelected = true } }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Power of 2
                else if (current_size+3< max_size && sender.Equals(Power_2))
                {
                    current_size+=3;
                    equation.InnerTerms.Add(new PowerOperation(new AbsoluteMember(2), new List<Term>() { new EmptyMember() { IsSelected = true } }));
                        unselectOnNew();
                    bool_out = false;
                }
                // Power of N
                else if (current_size+3< max_size && sender.Equals(Power))
                {
                    current_size+=3;
                    equation.InnerTerms.Add(new PowerOperation(new EmptyMember(), new List<Term>() { new EmptyMember() { IsSelected = true } }));
                    unselectOnNew();
                    bool_out = false;
                }
                // Left Arrow
                else if (sender.Equals(Left_Arrow))
                {
                    if (idx <= 0)
                        return;

                    if (isComplex)
                    {
                        // Move in term itself
                        if (sub_idx > 0)
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx--].IsSelected = false;
                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                        }
                        // Move out of term
                        else
                        {
                            if (equation.InnerTerms[idx].GetType() == typeof(PowerOperation) || equation.InnerTerms[idx].GetType() == typeof(NthRootOperation))
                            {
                                if (equation.InnerTerms[idx] is PowerOperation pwd)
                                {
                                    if (sub_idx > 0)
                                    {
                                        pwd.Exponent.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx--].IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                                    }
                                    else if (!pwd.Exponent.IsSelected)
                                    {
                                        pwd.Exponent.IsSelected = true;
                                        pwd.InnerTerms[sub_idx].IsSelected = false;
                                    }
                                    else
                                    {
                                        pwd.Exponent.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                                        idx--;
                                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
                                        {
                                            sub_idx = equation.InnerTerms[idx].InnerTerms.Count - 1;
                                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                                        }
                                        else
                                        {
                                            equation.InnerTerms[idx].IsSelected = true;
                                        }
                                    }
                                }
                                else if (equation.InnerTerms[idx] is NthRootOperation sqr)
                                {
                                    if (sub_idx > 0)
                                    {
                                        sqr.Degree.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx--].IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                                    }
                                    else if (!sqr.Degree.IsSelected)
                                    {
                                        sqr.Degree.IsSelected = true;
                                        sqr.InnerTerms[sub_idx].IsSelected = false;
                                    }
                                    else
                                    {
                                        sqr.Degree.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                                        idx--;
                                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
                                        {
                                            sub_idx = equation.InnerTerms[idx].InnerTerms.Count - 1;
                                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                                        }
                                        else
                                        {
                                            equation.InnerTerms[idx].IsSelected = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                                idx--;
                                if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
                                {
                                    sub_idx = equation.InnerTerms[idx].InnerTerms.Count - 1;
                                    equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                                }
                                else
                                {
                                    equation.InnerTerms[idx].IsSelected = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        equation.InnerTerms[idx].IsSelected = false;

                        idx--;
                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
                        {
                            sub_idx = equation.InnerTerms[idx].InnerTerms.Count - 1;
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
                    if (idx + 1 == len)
                        bool_out = true;
                    if (idx + 1 >= len && (equation.InnerTerms[idx].InnerTerms == null || equation.InnerTerms[idx].InnerTerms.Count - sub_idx <= 1))
                        return;

                    if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
                    {
                        // Move in term
                        if (equation.InnerTerms[idx].InnerTerms.Count - sub_idx > 1)
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;
                            sub_idx++;
                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                        }
                        // Move out of term
                        else if (idx + 1 < len)
                        {
                            if (equation.InnerTerms[idx].GetType() == typeof(PowerOperation) || equation.InnerTerms[idx].GetType() == typeof(NthRootOperation))
                            {
                                if (equation.InnerTerms[idx].GetType() == typeof(PowerOperation))
                                {
                                    PowerOperation a = (PowerOperation)equation.InnerTerms[idx];
                                    if (equation.InnerTerms[idx].InnerTerms.Count - sub_idx > 1)
                                    {
                                        a.Exponent.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;
                                        sub_idx++;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                                    }
                                    else if (!a.Exponent.IsSelected)
                                    {
                                        a.Exponent.IsSelected = true;
                                        a.InnerTerms[sub_idx].IsSelected = false;
                                    }
                                    else
                                    {
                                        a.Exponent.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                                        idx++;
                                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
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
                                    NthRootOperation a = (NthRootOperation)equation.InnerTerms[idx];
                                    if (equation.InnerTerms[idx].InnerTerms.Count - sub_idx > 1)
                                    {
                                        a.Degree.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;
                                        sub_idx++;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                                    }
                                    else if (!a.Degree.IsSelected)
                                    {
                                        a.Degree.IsSelected = true;
                                    }
                                    else
                                    {
                                        a.Degree.IsSelected = false;
                                        equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                                        idx++;
                                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
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
                            }
                            else
                            {
                                equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;

                                idx++;
                                if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
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
                    }
                    else if (idx + 1 < len)
                    {
                        equation.InnerTerms[idx].IsSelected = false;
                        idx++;
                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
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
                    if (isComplex)
                    {
                        if (!isSubTermEmpty)
                        {
                            string past_num_str;
                            if (term is PowerOperation pwd)
                            {
                                if (pwd.Exponent.IsSelected && pwd.Exponent is EmptyMember)
                                    past_num_str = "0";
                                else
                                    past_num_str = CleanLatex(pwd.Exponent.IsSelected ? CleanLatex(pwd.Exponent.GetLateX()) : CleanLatex(pwd.InnerTerms[0].GetLateX()));
                            }
                            else if (term is NthRootOperation sqr)
                            {
                                if (sqr.Degree.IsSelected && sqr.Degree is EmptyMember)
                                    past_num_str = "0";
                                else
                                    past_num_str = CleanLatex(sqr.Degree.IsSelected ? CleanLatex(sqr.Degree.GetLateX()) : CleanLatex(sqr.InnerTerms[0].GetLateX()));
                            }
                            else if (subTerm != null)
                                past_num_str = CleanLatex(subTerm.GetLateX());
                            else
                                past_num_str = CleanLatex(term.GetLateX());

                            past_num_str = past_num_str.Substring(0, past_num_str.Length - 1);
                            current_size--;
                            if (past_num_str.Length != 0 && !char.IsDigit(past_num_str[0]))
                                past_num_str = past_num_str.Substring(1);


                            if (string.IsNullOrWhiteSpace(past_num_str) || past_num_str.Length == 0)
                                equation.InnerTerms[idx].InnerTerms[sub_idx] = new EmptyMember { IsSelected = true };
                            else if (decimal.TryParse(past_num_str, out decimal result))
                                SetValue(term, result, isComplex);
                        }
                        else
                        {
                            if (term is MulOperation || term is ModuloOperation)
                                current_size -= 7;
                            else if (isComplex)
                                current_size -= 3;
                            else current_size--;

                            equation.InnerTerms.RemoveAt(idx);
                            idx--;
                            len--;

                            if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
                            {
                                equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                            }
                            else
                            {
                                equation.InnerTerms[idx].IsSelected = true;
                            }
                        }
                    }
                    else if (term is PiConst)
                    {
                        current_size -= 2;
                        equation.InnerTerms.RemoveAt(idx);
                        idx--;
                        len--;

                        if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
                        {
                            equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = true;
                        }
                        else
                        {
                            equation.InnerTerms[idx].IsSelected = true;
                        }
                    }
                    else
                    {
                        String past_num_str = equation.InnerTerms[idx].GetLateX().Replace("\\colorbox{red}{", "").Replace("}", "").Replace("^{2", "").Replace("\\sqrt{", "").Replace("^{\\square", "");
                        if (past_num_str == "0")
                        {
                            return;
                        }


                        past_num_str = past_num_str.Substring(0, past_num_str.Length - 1);
                        current_size--;
                        if (string.IsNullOrWhiteSpace(past_num_str) || past_num_str.Length == 0 || past_num_str == "-")
                            equation.InnerTerms[idx] = new AbsoluteMember(0) { IsSelected = true };
                        else if (decimal.TryParse(past_num_str, out decimal result))
                            equation.InnerTerms[idx] = new AbsoluteMember(result) { IsSelected = true };
                        else if (decimal.TryParse(past_num_str, out decimal new_num))
                        {
                            equation.InnerTerms[idx] = new AbsoluteMember(new_num) { IsSelected = true };
                        }
                    }

                    update();
                }
                // CE
                else if (sender.Equals(Right_Bracket))
                {
                    equation = new SumOperation(new List<Term>() { new AbsoluteMember(0) { IsSelected = true } });
                    idx = 0;
                    sub_idx = 0;
                    len = 1;
                    bool_decimal = false;
                }
                // Equals
                else if (sender.Equals(Equals))
                {
                    var result = equation.GetResult();
                    formulaWraper.Formula = result.ToString();
                }
                // Open Github
                else if (sender.Equals(Github))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://github.com/Krypt0niT/IVS-projekt2",
                        UseShellExecute = true
                    });
                }
                else if (sender.Equals(Popup_Btn))
                {
                    Popup.IsOpen = true;
                }
                bool_decimal = false;
                // Apply changes
                if (!sender.Equals(Equals))
                {
                    update();
                }

            }
            catch (Exception ex) { ErrorLabel.Content = ex.Message; }

        }


        void update()
        {
            formulaWraper.Formula = equation.GetLateX();
        }
        void SetValue(object target, decimal value, bool isComplex)
        {
            // Power operation uses Exponent
            if (target is PowerOperation p)
            {
                if (p.Exponent.IsSelected) p.Exponent = new AbsoluteMember(value) { IsSelected = true };
                else if (isComplex) equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(value) { IsSelected = true };
                else equation.InnerTerms[idx] = new AbsoluteMember(value) { IsSelected = true };
            }
            // Square root uses Degree
            else if (target is NthRootOperation s)
            {
                if (s.Degree.IsSelected) s.Degree = new AbsoluteMember(value) { IsSelected = true };
                else if (isComplex) equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(value) { IsSelected = true };
                else equation.InnerTerms[idx] = new AbsoluteMember(value) { IsSelected = true };
            }
            // Others
            else
            {
                if (isComplex) equation.InnerTerms[idx].InnerTerms[sub_idx] = new AbsoluteMember(value) { IsSelected = true };
                else equation.InnerTerms[idx] = new AbsoluteMember(value) { IsSelected = true };
            }
        }

        void unselectOnNew()
        {
            if (equation.InnerTerms[idx].GetType() != typeof(AbsoluteMember) && equation.InnerTerms[idx].GetType() != typeof(PiConst))
            {
                equation.InnerTerms[idx].InnerTerms[sub_idx].IsSelected = false;
            }
            else
            {
                equation.InnerTerms[idx].IsSelected = false;
            }

            sub_idx = 0;
            idx = equation.InnerTerms.Count - 1;
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
            else if (e.Key == Key.Up)
                Left_Arrow.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.Down)
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

            // Backspace, Equals (Enter)
            else if (e.Key == Key.Back)
                Backspace.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            else if (e.Key == Key.Enter)
                Equals.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

        }
    }
}