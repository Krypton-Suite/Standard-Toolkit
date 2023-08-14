using System.ComponentModel;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class Form1 : KryptonForm
    {
        public Form1()//This is constructor
        {
            InitializeComponent();
        }

        private void AddEvent(string message)
        {
            kryptonListBox1.Items.Add(message);
        }

        private void textBox1_Validated(object sender, System.EventArgs e)
        {
            AddEvent("textBox1_Validated");
        }

        private void kryptonTextBox1_Validated(object sender, System.EventArgs e)
        {
            AddEvent("kryptonTextBox1_Validated");

        }

        private void kryptonTextBox1_DoubleClick(object sender, System.EventArgs e)
        {
            AddEvent("kryptonTextBox1_DoubleClick");

        }

        private void kryptonTextBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent("kryptonTextBox1_MouseDoubleClick");

        }

        private void kryptonTextBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent("kryptonTextBox1_MouseClick");

        }

        private void kryptonTextBox1_Click(object sender, System.EventArgs e)
        {
            AddEvent("kryptonTextBox1_Click");

        }

        private void textBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent("textBox1_MouseDoubleClick");

        }

        private void textBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AddEvent("textBox1_MouseClick");

        }

        private void textBox1_DoubleClick(object sender, System.EventArgs e)
        {
            AddEvent("textBox1_DoubleClick");

        }

        private void textBox1_Click(object sender, System.EventArgs e)
        {
            AddEvent("textBox1_Click");

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            AddEvent("textBox1_Validating");
        }

        private void kryptonTextBox1_Validating(object sender, CancelEventArgs e)
        {
            AddEvent("kryptonTextBox1_Validating");
        }

        private void textBox1_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            AddEvent("textBox1_PreviewKeyDown");
        }

        private void kryptonTextBox1_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            AddEvent("kryptonTextBox1_PreviewKeyDown");
        }

        private void kryptonTextBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent("kryptonTextBox1_KeyDown");
        }

        private void kryptonTextBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            AddEvent("kryptonTextBox1_KeyPress");

        }

        private void kryptonTextBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent("kryptonTextBox1_KeyUp");

        }

        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent("textBox1_KeyDown");

        }

        private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            AddEvent("textBox1_KeyPress");
        }

        private void textBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AddEvent("textBox1_KeyUp");

        }
    }
}