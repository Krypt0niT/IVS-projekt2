namespace WpfApp1
{
    public partial class FormulaWrapper : System.Windows.Controls.UserControl
    {
        public FormulaWrapper()
        {
            InitializeComponent();
        }

        // Môžeš pridať aj vlastnosti na dynamickú zmenu formulí
        public string Formula
        {
            get { return formulaControl.Formula; }
            set { formulaControl.Formula = value; }
        }
    }
}
