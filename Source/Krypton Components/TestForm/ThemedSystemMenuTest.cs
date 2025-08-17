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

                // Show the current menu item count
                Text = $"Themed System Menu Test - {ThemedSystemMenu.MenuItemCount} items";
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
                          $"Custom Items: {string.Join(", ", customItems)}";

                MessageBox.Show(info, "Menu Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
