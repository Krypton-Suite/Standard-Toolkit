#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Test form demonstrating designer-configured menu items for the themed system menu.
/// </summary>
public partial class DesignerMenuTest : KryptonForm
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DesignerMenuTest class.
    /// </summary>
    public DesignerMenuTest()
    {
        InitializeComponent();
        
        /*// Configure the themed system menu
        ThemedSystemMenuValues.Enabled = true;
        //ThemedSystemMenuValues.ShowOnLeftClick = true;
        ThemedSystemMenuValues.ShowOnRightClick = true;
        ThemedSystemMenuValues.ShowOnAltSpace = true;
        
        // Add some sample menu items programmatically (these will be in addition to designer items)
        if (ThemedSystemMenu != null)
        {
            ThemedSystemMenu.AddCustomMenuItem("Programmatic Item", (sender, args) => 
            {
                MessageBox.Show("This item was added programmatically!", "Programmatic Item", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }*/
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles the form load event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        // Update the form title to show the current menu item count
        if (KryptonSystemMenu != null)
        {
            Text = $"Designer Menu Test - {KryptonSystemMenu.MenuItemCount} total items";
        }
    }

    /// <summary>
    /// Handles the button click event to show menu information.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void kryptonButton1_Click(object? sender, EventArgs e)
    {
    }
    #endregion
}
