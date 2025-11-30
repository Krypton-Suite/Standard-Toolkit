using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace BackstageDemo.UserControls
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void AutoSaveCheck_CheckedChanged(object sender, EventArgs e)
        {
            // Handle auto save setting change
            MessageBox.Show($"Auto save is now {(autoSaveCheck.Checked ? "enabled" : "disabled")}", "Settings");
        }

        private void ThemeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle theme change
            MessageBox.Show($"Theme changed to: {themeCombo.Text}", "Theme Changed");
        }

        private void LanguageCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle language change
            MessageBox.Show($"Language changed to: {languageCombo.Text}", "Language Changed");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings saved successfully!", "Settings");
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset all settings to defaults?", "Reset Settings", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                autoSaveCheck.Checked = true;
                themeCombo.SelectedIndex = 0;
                languageCombo.SelectedIndex = 0;
                MessageBox.Show("Settings reset to defaults!", "Reset Complete");
            }
        }
    }
}