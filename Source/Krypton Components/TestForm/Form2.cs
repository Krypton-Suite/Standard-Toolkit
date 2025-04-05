using Krypton.Toolkit;

namespace TestForm
{
    public partial class Form2 : KryptonForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void kryptonPropertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            KryptonMessageBox.Show("Event fired");
        }
    }
}
