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

        private void AddEvent(string message)
        {
            kryptonListBox1.Items.Add(message);
        }

        private void textBox1_Validated(object sender, System.EventArgs e)
        {
            AddEvent(nameof(textBox1_Validated));
        }

        private void kryptonTextBox1_Validated(object sender, System.EventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_Validated));

        }

        private void kryptonTextBox1_DoubleClick(object sender, System.EventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_DoubleClick));

        }

        private void kryptonTextBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_MouseDoubleClick));

        }

        private void kryptonTextBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_MouseClick));

        }

        private void kryptonTextBox1_Click(object sender, System.EventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_Click));

        }

        private void textBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent(nameof(textBox1_MouseDoubleClick));

        }

        private void textBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent(nameof(textBox1_MouseClick));

        }

        private void textBox1_DoubleClick(object sender, System.EventArgs e)
        {
            AddEvent(nameof(textBox1_DoubleClick));

        }

        private void textBox1_Click(object sender, System.EventArgs e)
        {
            AddEvent(nameof(textBox1_Click));

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            AddEvent(nameof(textBox1_Validating));
        }

        private void kryptonTextBox1_Validating(object sender, CancelEventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_Validating));
        }

        private void textBox1_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            AddEvent(nameof(textBox1_PreviewKeyDown));
        }

        private void kryptonTextBox1_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_PreviewKeyDown));
        }

        private void kryptonTextBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_KeyDown));
        }

        private void kryptonTextBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_KeyPress));

        }

        private void kryptonTextBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent(nameof(kryptonTextBox1_KeyUp));

        }

        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent(nameof(textBox1_KeyDown));

        }

        private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            AddEvent(nameof(textBox1_KeyPress));

        }

        private void textBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent(nameof(textBox1_KeyUp));

        }

        private void kryptonButton1_Click(object sender, System.EventArgs e)
        {
            Form2 form2 = new Form2();

            form2.ShowDialog();
        }

        private void kryptonButton2_Click(object sender, System.EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.ShowDialog();
        }

        private void kbtnTestMessagebox_Click(object sender, System.EventArgs e)
        {
            KryptonMessageBox.Show(@"This is a test!", @"Testing", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information, contentAreaType: MessageBoxContentAreaType.LinkLabel,
                linkAreaCommand: kcmdMessageboxTest);
        }

        private void kcmdMessageboxTest_Execute(object sender, System.EventArgs e)
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