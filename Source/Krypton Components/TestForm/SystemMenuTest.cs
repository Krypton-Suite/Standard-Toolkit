namespace TestForm;

/// <summary>
/// Test form to demonstrate the system menu functionality.
/// </summary>
public partial class SystemMenuTest : KryptonForm
{
    public SystemMenuTest()
    {
        InitializeComponent();

        // Set form properties
        Text = "System Menu Test";
        Size = new Size(600, 400);
        StartPosition = FormStartPosition.CenterScreen;

        // Enable the system menu (enabled by default)
        //UseThemedSystemMenu = true;

        // Configure how the menu appears
        //ThemedSystemMenu.ShowOnLeftClick = true;   // Left-click on title bar
        SystemMenuValues.ShowOnRightClick = true;  // Right-click on title bar
        SystemMenuValues.ShowOnAltSpace = true;    // Alt+Space keyboard shortcut
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Initialize the checkboxes to match current settings
        //kryptonCheckBox1.Checked = ThemedSystemMenu.ShowOnLeftClick;
        kryptonCheckBox2.Checked = SystemMenuValues.ShowOnRightClick;
        kryptonCheckBox3.Checked = SystemMenuValues.ShowOnAltSpace;

        // Demonstrate the enhanced system menu features
        if (KryptonSystemMenu != null)
        {
            // Add a custom menu item using the new method
            KryptonSystemMenu.AddCustomMenuItem("About This Form", (sender, args) =>
            {
                KryptonMessageBox.Show("This is a test form demonstrating the system menu functionality!",
                    "About", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            });

            // Add a separator
            KryptonSystemMenu.AddSeparator();

            // Add another custom item
            KryptonSystemMenu.AddCustomMenuItem("Refresh Menu", (sender, args) =>
            {
                KryptonSystemMenu.Refresh();
                KryptonMessageBox.Show("Menu refreshed! Current item count: " + KryptonSystemMenu.MenuItemCount,
                    "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
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
        if (KryptonSystemMenu != null)
        {
            var themeInfo = $"Theme: {KryptonSystemMenu.CurrentIconTheme}";
            Text = $"System Menu Test - {KryptonSystemMenu.MenuItemCount} items - {themeInfo}";
                
            // Also update the theme label
            UpdateThemeLabel();
        }
    }

    /// <summary>
    /// Updates the theme label to show current theme information.
    /// </summary>
    private void UpdateThemeLabel()
    {
        if (KryptonSystemMenu != null)
        {
            var currentTheme = KryptonSystemMenu.CurrentIconTheme;
            kryptonLabel3.Values.Text = $"Current Theme: {currentTheme} (Auto-detected)";
        }
    }
        
    /// <summary>
    /// Debug method to check icon generation.
    /// </summary>
    private void DebugIconGeneration()
    {
        System.Diagnostics.Debug.WriteLine("=== DEBUG ICON GENERATION ===");
        System.Diagnostics.Debug.WriteLine($"SystemMenu is null: {KryptonSystemMenu == null}");
            
        if (KryptonSystemMenu != null)
        {
            System.Diagnostics.Debug.WriteLine($"Menu item count: {KryptonSystemMenu.MenuItemCount}");
            System.Diagnostics.Debug.WriteLine($"Current theme: {KryptonSystemMenu.CurrentIconTheme}");
        }
    }

    private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        //ThemedSystemMenu.ShowOnLeftClick = kryptonCheckBox1.Checked;
    }

    private void kryptonCheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        SystemMenuValues.ShowOnRightClick = kryptonCheckBox2.Checked;
    }

    private void kryptonCheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        SystemMenuValues.ShowOnAltSpace = kryptonCheckBox3.Checked;
    }

    private void kryptonButton1_Click(object sender, EventArgs e)
    {
        // Clear custom menu items and restore default menu
        if (KryptonSystemMenu != null)
        {
            KryptonSystemMenu.ClearCustomItems();
            KryptonMessageBox.Show("Custom items cleared! Menu restored to default.", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);

            // Update the title to show new item count
            Text = $"System Menu Test - {KryptonSystemMenu.MenuItemCount} items";
        }
    }

    private void kryptonButton2_Click(object sender, EventArgs e)
    {
        // Show information about the current menu
        if (KryptonSystemMenu != null)
        {
            var customItems = KryptonSystemMenu.GetCustomMenuItems();
            var info = $"Menu Information:\n" +
                       $"Total Items: {KryptonSystemMenu.MenuItemCount}\n" +
                       $"Has Items: {KryptonSystemMenu.HasMenuItems}\n" +
                       $"Custom Items: {customItems.Count}\n" +
                       $"Custom Items: {string.Join(", ", customItems)}\n" +
                       $"Current Icon Theme: {KryptonSystemMenu.CurrentIconTheme}";

            KryptonMessageBox.Show(info, "Menu Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
    }

    private void kryptonButton3_Click(object sender, EventArgs e)
    {
        // Test theme switching and icon refreshing
        if (KryptonSystemMenu != null)
        {
            // Get available themes
            var availableThemes = new[] { "Office2013", "Office2010", "Office2007", "Sparkle", "Professional", "Microsoft365", "Office2003" };
                
            // Find current theme index
            var currentTheme = KryptonSystemMenu.CurrentIconTheme;
            var currentIndex = Array.IndexOf(availableThemes, currentTheme);
            var nextIndex = (currentIndex + 1) % availableThemes.Length;
            var nextTheme = availableThemes[nextIndex];
                
            // Set the next theme
            KryptonSystemMenu.SetIconTheme(nextTheme);
                
            // Refresh the icons
            KryptonSystemMenu.RefreshThemeIcons();
                
            // Update the form title and theme label
            UpdateFormTitle();
                
            // Update the theme label to show the new theme
            kryptonLabel3.Values.Text = $"Current Theme: {nextTheme} (Manually Set)";

            KryptonMessageBox.Show($"Switched to {nextTheme} theme and refreshed icons!\n" +
                                   $"Previous theme: {currentTheme}\n" +
                                   $"New theme: {nextTheme}",
                "Theme Changed", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
    }
}