using System.Windows.Forms;
using Krypton.Toolkit;

namespace MultiSelectionTest
{
    partial class MultiSelectionTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = "KryptonCheckedListBox Selection Modes Test";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Status label
            _statusLabel = new Label
            {
                Text = "Select a mode and test different selection behaviors",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(580, 20)
            };

            // CheckedListBox
            _checkedListBox = new KryptonCheckedListBox
            {
                Location = new System.Drawing.Point(10, 40),
                Size = new System.Drawing.Size(300, 200),
                SelectionMode = CheckedSelectionMode.MultiSimple,
                CheckOnClick = true
            };

            // Add test items
            for (int i = 1; i <= 10; i++)
            {
                _checkedListBox.Items.Add($"Item {i}");
            }

            // Add controls to form
            this.Controls.Add(_statusLabel);
            this.Controls.Add(_checkedListBox);

            this.ResumeLayout(false);
        }

        #endregion

        private KryptonCheckedListBox _checkedListBox;
        private Label _statusLabel;
    }
}
