using System;

using Krypton.Toolkit;

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
    }
}
