using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AddEvent(string message) => kryptonListBox1.Items.Add(message);

        private void textBox1_Validated(object sender, EventArgs e) => AddEvent(nameof(textBox1_Validated));

        private void kryptonTextBox1_Validated(object sender, EventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_Validated));

        private void kryptonTextBox1_DoubleClick(object sender, EventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_DoubleClick));

        private void kryptonTextBox1_MouseDoubleClick(object sender, MouseEventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_MouseDoubleClick));

        private void kryptonTextBox1_MouseClick(object sender, MouseEventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_MouseClick));

        private void kryptonTextBox1_Click(object sender, EventArgs e) => AddEvent(nameof(kryptonTextBox1_Click));

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e) =>
            AddEvent(nameof(textBox1_MouseDoubleClick));

        private void textBox1_MouseClick(object sender, MouseEventArgs e) => AddEvent(nameof(textBox1_MouseClick));

        private void textBox1_DoubleClick(object sender, EventArgs e) => AddEvent(nameof(textBox1_DoubleClick));

        private void textBox1_Click(object sender, EventArgs e) => AddEvent(nameof(textBox1_Click));

        private void textBox1_Validating(object sender, CancelEventArgs e) => AddEvent(nameof(textBox1_Validating));

        private void kryptonTextBox1_Validating(object sender, CancelEventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_Validating));

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) =>
            AddEvent(nameof(textBox1_PreviewKeyDown));

        private void kryptonTextBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_PreviewKeyDown));

        private void kryptonTextBox1_KeyDown(object sender, KeyEventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_KeyDown));

        private void kryptonTextBox1_KeyPress(object sender, KeyPressEventArgs e) =>
            AddEvent(nameof(kryptonTextBox1_KeyPress));

        private void kryptonTextBox1_KeyUp(object sender, KeyEventArgs e) => AddEvent(nameof(kryptonTextBox1_KeyUp));

        private void textBox1_KeyDown(object sender, KeyEventArgs e) => AddEvent(nameof(textBox1_KeyDown));

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) => AddEvent(nameof(textBox1_KeyPress));

        private void textBox1_KeyUp(object sender, KeyEventArgs e) => AddEvent(nameof(textBox1_KeyUp));

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();

            form2.ShowDialog();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            var form3 = new Form3();

            form3.ShowDialog();
        }

        private void kbtnTestMessagebox_Click(object sender, EventArgs e) => KryptonMessageBox.Show(@"This is a test!",
            @"Testing", KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Information, contentAreaType: MessageBoxContentAreaType.LinkLabel,
            linkAreaCommand: kcmdMessageboxTest);

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

        private void kbtnIntegratedToolbar_Click(object sender, EventArgs e)
        {
            Form5 integratedToolBar = new Form5();

            integratedToolBar.Show();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            KryptonThemeBrowserForm themeBrowser = new KryptonThemeBrowserForm();

            themeBrowser.ShowDialog();
        }

        private void kchkUseProgressValueAsText_CheckedChanged(object sender, EventArgs e)
        {
            kryptonProgressBar1.UseValueAsText = kchkUseProgressValueAsText.Checked;
        }

        private void ktrkProgressValues_ValueChanged(object sender, EventArgs e)
        {
            kryptonProgressBar1.Value = ktrkProgressValues.Value;
        }

        private void kbtnVisualStudio2010Theme_Click(object sender, EventArgs e)
        {
            Form5 vsTheme = new Form5();

            vsTheme.Show();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            new Form4().Show();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            KryptonAboutToolkitData data = new KryptonAboutToolkitData();

            KryptonAboutToolkit.Show(data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kcbtnNone.Checked = true;
        }

        private void kbtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            kryptonCustomPaletteBase1.Import();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            kryptonCustomPaletteBase1.Export();
        }

        private void kcbtnNone_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
        }

        private void kcbtnFixedSingle_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void kcbtnFixed3D_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void kcbtnFixedDialog_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void kcbtnSizable_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void kcbtnFixedToolWindow_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void kcbtnSizableToolWindow_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }
    }
}