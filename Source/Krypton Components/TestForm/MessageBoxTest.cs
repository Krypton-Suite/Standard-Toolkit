#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

using System.Diagnostics;

namespace TestForm;

public partial class MessageBoxTest : KryptonForm
{
    public MessageBoxTest()
    {
        InitializeComponent();
    }

    private void kbtnTestMessagebox_Click(object sender, EventArgs e)
    {
        KryptonMessageBox.Show(this, @"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Error, showCloseButton: kryptonCheckBox1.Checked);

        KryptonMessageBox.Show(this, @"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Error, KryptonMessageBoxDefaultButton.Button1,
            options: MessageBoxOptions.RtlReading,
            showCloseButton: kryptonCheckBox1.Checked);
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

    private void kbtnCustomMessageBox_Click(object sender, EventArgs e)
    {
        var customMessageBoxTest = new CustomMessageBoxTest();

        customMessageBoxTest.Show();
    }
}