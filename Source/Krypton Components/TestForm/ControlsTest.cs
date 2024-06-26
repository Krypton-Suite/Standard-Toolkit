namespace TestForm
{
    public partial class ControlsTest : KryptonForm
    {
        public ControlsTest()
        {
            InitializeComponent();
        }

        private void ControlsTest_Load(object sender, EventArgs e)
        {
            kryptonRibbonGroupComboBox1.SelectedIndex = 1;

            kryptonRibbonGroupComboBox2.SelectedIndex = 1;
        }
    }
}
