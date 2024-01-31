using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class TextBoxEventTest : KryptonForm
    {
        public TextBoxEventTest()
        {
            InitializeComponent();
        }

        private void AddNormalTextBoxEvent(string message) => lbNormalTextBoxEvents.Items.Add(message);

        private void AddKryptonTextBoxEvent(string message) => klbKryptonTextBoxEvents.Items.Add(message);

        private void txtNormalTextBox_Click(object sender, EventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_Click));

        private void txtNormalTextBox_DoubleClick(object sender, EventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_DoubleClick));

        private void txtNormalTextBox_KeyDown(object sender, KeyEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_KeyDown));

        private void txtNormalTextBox_KeyPress(object sender, KeyPressEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_KeyPress));

        private void txtNormalTextBox_KeyUp(object sender, KeyEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_KeyUp));

        private void txtNormalTextBox_MouseClick(object sender, MouseEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_MouseClick));

        private void txtNormalTextBox_MouseDoubleClick(object sender, MouseEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_MouseDoubleClick));

        private void txtNormalTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_PreviewKeyDown));

        private void txtNormalTextBox_Validated(object sender, EventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_Validated));

        private void txtNormalTextBox_Validating(object sender, CancelEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_Validating));

        private void ktxtKryptonTextBox_Click(object sender, EventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_Click));

        private void ktxtKryptonTextBox_DoubleClick(object sender, EventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_DoubleClick));

        private void ktxtKryptonTextBox_KeyDown(object sender, KeyEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_KeyDown));

        private void ktxtKryptonTextBox_KeyPress(object sender, KeyPressEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_KeyPress));

        private void ktxtKryptonTextBox_KeyUp(object sender, KeyEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_KeyUp));

        private void ktxtKryptonTextBox_MouseClick(object sender, MouseEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_MouseClick));

        private void ktxtKryptonTextBox_MouseDoubleClick(object sender, MouseEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_MouseDoubleClick));

        private void ktxtKryptonTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_PreviewKeyDown));

        private void ktxtKryptonTextBox_Validated(object sender, EventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_Validated));

        private void ktxtKryptonTextBox_Validating(object sender, CancelEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_Validating));

        private void kbtnClearNormalEvents_Click(object sender, EventArgs e)
        {
            lbNormalTextBoxEvents.Items.Clear();

            //kbtnClearNormalEvents.Enabled = false;
        }

        private void kbtnClearKryptonEvents_Click(object sender, EventArgs e)
        {
            klbKryptonTextBoxEvents.Items.Clear();

            //kbtnClearKryptonEvents.Enabled = false;
        }
    }
}
