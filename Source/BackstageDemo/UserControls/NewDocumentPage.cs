using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace BackstageDemo.UserControls
{
    public partial class NewDocumentPage : UserControl
    {
        public NewDocumentPage()
        {
            InitializeComponent();
        }

        private void BlankDocButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creating blank document...", "New Document");
        }

        private void TemplateButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening template gallery...", "Templates");
        }

        private void RecentButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Showing recent files...", "Recent");
        }
    }
}