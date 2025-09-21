using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace BackstageDemo.UserControls
{
    public partial class HelpPage : UserControl
    {
        public HelpPage()
        {
            InitializeComponent();
            SetupSystemInfo();
        }

        private void SetupSystemInfo()
        {
            osInfo.Text = $"OS: {Environment.OSVersion}";
            frameworkInfo.Text = $".NET Version: {Environment.Version}";
        }

        private void DocumentationButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening documentation...", "Documentation");
        }

        private void TutorialsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening video tutorials...", "Tutorials");
        }

        private void SupportButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening support portal...", "Support");
        }
    }
}