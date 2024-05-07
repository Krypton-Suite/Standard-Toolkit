#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using System.Diagnostics;

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
            KryptonMessageBoxDataDep data = new KryptonMessageBoxDataDep
            {
                MessageText = @"This is a test!",
                Caption = @"Hello World",
                Buttons = KryptonMessageBoxButtons.OK,
                Icon = KryptonMessageBoxIcon.Information,
                MessageContentAreaType = MessageBoxContentAreaType.LinkLabel,
                ShowCloseButton = kryptonCheckBox1.Checked,
                //Options = MessageBoxOptions.RtlReading
            };

            KryptonMessageBoxDep.Show(@"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information, contentAreaType: MessageBoxContentAreaType.LinkLabel,
                linkAreaCommand: kcmdMessageboxTest, showCloseButton: kryptonCheckBox1.Checked);

            KryptonMessageBoxDep.Show(@"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
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
