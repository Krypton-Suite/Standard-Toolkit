using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    /// <summary>
    /// Test form to demonstrate the themed system menu functionality.
    /// </summary>
    public partial class ThemedSystemMenuTest : KryptonForm
    {
        public ThemedSystemMenuTest()
        {
            InitializeComponent();

            // Set form properties
            Text = "Themed System Menu Test";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            // Enable the themed system menu (enabled by default)
            UseThemedSystemMenu = true;

            // Configure how the menu appears
            ThemedSystemMenuValues.ShowOnLeftClick = true;   // Left-click on title bar
            ThemedSystemMenuValues.ShowOnRightClick = true;  // Right-click on title bar
            ThemedSystemMenuValues.ShowOnAltSpace = true;    // Alt+Space keyboard shortcut
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Initialize the checkboxes to match current settings
            kryptonCheckBox1.Checked = ThemedSystemMenuValues.ShowOnLeftClick;
            kryptonCheckBox2.Checked = ThemedSystemMenuValues.ShowOnRightClick;
            kryptonCheckBox3.Checked = ThemedSystemMenuValues.ShowOnAltSpace;

            // Demonstrate the enhanced themed system menu features
            if (ThemedSystemMenu != null)
            {
                // Add a custom menu item using the new method
                ThemedSystemMenu.AddCustomMenuItem("About This Form", (sender, args) =>
                {
                    MessageBox.Show("This is a test form demonstrating the themed system menu functionality!",
                                  "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

                // Add a separator
                ThemedSystemMenu.AddSeparator();

                // Add another custom item
                ThemedSystemMenu.AddCustomMenuItem("Refresh Menu", (sender, args) =>
                {
                    ThemedSystemMenu.Refresh();
                    MessageBox.Show("Menu refreshed! Current item count: " + ThemedSystemMenu.MenuItemCount,
                                  "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

                // Show the current menu item count and theme
                UpdateFormTitle();
                
                // Debug icon generation
                DebugIconGeneration();
            }
        }

        /// <summary>
        /// Updates the form title to show current menu information and theme.
        /// </summary>
        private void UpdateFormTitle()
        {
            if (ThemedSystemMenu != null)
            {
                var themeInfo = $"Theme: {ThemedSystemMenu.CurrentIconTheme}";
                Text = $"Themed System Menu Test - {ThemedSystemMenu.MenuItemCount} items - {themeInfo}";
                
                // Also update the theme label
                UpdateThemeLabel();
            }
        }

        /// <summary>
        /// Updates the theme label to show current theme information.
        /// </summary>
        private void UpdateThemeLabel()
        {
            if (ThemedSystemMenu != null)
            {
                var currentTheme = ThemedSystemMenu.CurrentIconTheme;
                kryptonLabel3.Values.Text = $"Current Theme: {currentTheme} (Auto-detected)";
            }
        }
        
        /// <summary>
        /// Debug method to check icon generation.
        /// </summary>
        private void DebugIconGeneration()
        {
            System.Diagnostics.Debug.WriteLine("=== DEBUG ICON GENERATION ===");
            System.Diagnostics.Debug.WriteLine($"ThemedSystemMenu is null: {ThemedSystemMenu == null}");
            
            if (ThemedSystemMenu != null)
            {
                System.Diagnostics.Debug.WriteLine($"Menu item count: {ThemedSystemMenu.MenuItemCount}");
                System.Diagnostics.Debug.WriteLine($"Current theme: {ThemedSystemMenu.CurrentIconTheme}");
            }
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ThemedSystemMenuValues.ShowOnLeftClick = kryptonCheckBox1.Checked;
        }

        private void kryptonCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            ThemedSystemMenuValues.ShowOnRightClick = kryptonCheckBox2.Checked;
        }

        private void kryptonCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            ThemedSystemMenuValues.ShowOnAltSpace = kryptonCheckBox3.Checked;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Clear custom menu items and restore default menu
            if (ThemedSystemMenu != null)
            {
                ThemedSystemMenu.ClearCustomItems();
                MessageBox.Show("Custom items cleared! Menu restored to default.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update the title to show new item count
                Text = $"Themed System Menu Test - {ThemedSystemMenu.MenuItemCount} items";
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            // Show information about the current menu
            if (ThemedSystemMenu != null)
            {
                var customItems = ThemedSystemMenu.GetCustomMenuItems();
                var info = $"Menu Information:\n" +
                          $"Total Items: {ThemedSystemMenu.MenuItemCount}\n" +
                          $"Has Items: {ThemedSystemMenu.HasMenuItems}\n" +
                          $"Custom Items: {customItems.Count}\n" +
                          $"Custom Items: {string.Join(", ", customItems)}\n" +
                          $"Current Icon Theme: {ThemedSystemMenu.CurrentIconTheme}";

                MessageBox.Show(info, "Menu Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // Test theme switching and icon refreshing
            if (ThemedSystemMenu != null)
            {
                // Get available themes
                var availableThemes = new[] { "Office2013", "Office2010", "Office2007", "Sparkle", "Professional", "Microsoft365", "Office2003" };
                
                // Find current theme index
                var currentTheme = ThemedSystemMenu.CurrentIconTheme;
                var currentIndex = Array.IndexOf(availableThemes, currentTheme);
                var nextIndex = (currentIndex + 1) % availableThemes.Length;
                var nextTheme = availableThemes[nextIndex];
                
                // Set the next theme
                ThemedSystemMenu.SetIconTheme(nextTheme);
                
                // Refresh the icons
                ThemedSystemMenu.RefreshThemeIcons();
                
                // Update the form title and theme label
                UpdateFormTitle();
                
                // Update the theme label to show the new theme
                kryptonLabel3.Values.Text = $"Current Theme: {nextTheme} (Manually Set)";
                
                MessageBox.Show($"Switched to {nextTheme} theme and refreshed icons!\n" +
                              $"Previous theme: {currentTheme}\n" +
                              $"New theme: {nextTheme}",
                              "Theme Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
