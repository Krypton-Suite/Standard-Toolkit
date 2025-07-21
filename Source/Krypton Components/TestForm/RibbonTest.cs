#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class RibbonTest : KryptonForm
{
    public RibbonTest()
    {
        InitializeComponent();
    }

    private void krgbtnTest1715_Click(object sender, EventArgs e)
    {
        kryptonRibbon.SelectedTab!.ContextName = @"Testing";
    }

    private void kryptonToggleSwitch1_CheckedChanged(object sender, EventArgs e)
    {
        // Toggle the RightToLeft property of the form
        RightToLeft = kryptonToggleSwitch1.Checked ? RightToLeft.Yes : RightToLeft.No;
        // Update the layout accordingly
        RightToLeftLayout = kryptonToggleSwitch1.Checked;
        
        // Optionally, update any controls that need to reflect this change
        kryptonRibbon.RightToLeft = RightToLeft;
        //kryptonRibbon.RightToLeftLayout = RightToLeftLayout;
    }
}