using System;
using System.Diagnostics;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class MessageBoxTest : KryptonForm
    {
        public MessageBoxTest()
        {
            InitializeComponent();
        }

        private void kbtnTestMessagebox_Click(object sender, EventArgs e)
        {
            KryptonMessageBoxData data = new KryptonMessageBoxData()
            {
                MessageText = @"This is a test!",
                Caption = @"Hello World",
                Buttons = KryptonMessageBoxButtons.OK,
                Icon = KryptonMessageBoxIcon.Information,
                MessageContentAreaType = MessageBoxContentAreaType.LinkLabel,
                ShowCloseButton = kryptonCheckBox1.Checked,
                //Options = MessageBoxOptions.RtlReading
            };

            KryptonMessageBox.Show(@"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information, contentAreaType: MessageBoxContentAreaType.LinkLabel,
                linkAreaCommand: kcmdMessageboxTest, showCloseButton: kryptonCheckBox1.Checked);

            KryptonMessageBox.Show(@"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information, options: MessageBoxOptions.RtlReading,
                contentAreaType: MessageBoxContentAreaType.LinkLabel,
                linkAreaCommand: kcmdMessageboxTest, showCloseButton: kryptonCheckBox1.Checked);
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show(string.Empty, @"Test with no Text");
        }

        private void kcmdMessageboxTest_Execute(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"C:\\Windows\\Notepad.exe");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
