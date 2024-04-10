namespace TestForm
{
    public partial class ThemeTest : KryptonForm
    {
        public ThemeTest()
        {
            InitializeComponent();
        }

        private void kbtnKMBTest_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show(@"Can you read this text?", @"Test", KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question);
        }

        private void ktrkProgressValues_ValueChanged(object sender, EventArgs e)
        {
            kryptonProgressBarToolStripItem1.Value = ktrkProgressValues.Value;
            toolStripProgressBar1.Value = ktrkProgressValues.Value;
        }

        private void leftToRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KryptonThemeBrowser.Show(FormStartPosition.CenterParent, GlobalStaticValues.GLOBAL_DEFAULT_THEME_INDEX);
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KryptonThemeBrowser.Show(FormStartPosition.CenterParent, Krypton.Toolkit.RightToLeftLayout.RightToLeft);
        }
    }
}
