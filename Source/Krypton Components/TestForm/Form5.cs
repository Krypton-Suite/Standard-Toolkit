using System;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class Form5 : KryptonForm
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void kbtnKMBTest_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show(@"Can you read this text?", @"Test", KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question);
        }
    }
}
