#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of extended and infinite tooltip timeout (Issue #3075).
/// Krypton tooltips support AutoPopDelay &gt; 5000ms and AutoPopDelay = 0 (infinite) on all Windows versions.
/// </summary>
public partial class TooltipTimeoutTest : KryptonForm
{
    public TooltipTimeoutTest()
    {
        InitializeComponent();
        InitializeTooltips();
    }

    private void InitializeTooltips()
    {
        kcmbTimeoutMode.SelectedIndex = 0; // Default (5000 ms)
        ApplyTimeoutMode();
    }

    private void ApplyTimeoutMode()
    {
        int autoPopDelay = GetSelectedAutoPopDelay();

        // Update ToolTipValues for controls with built-in tooltips (KryptonButton, etc.)
        ApplyToolTipValuesTimeout(autoPopDelay);

        klblCurrentMode.Text = GetModeDescription(autoPopDelay);
    }

    private int GetSelectedAutoPopDelay()
    {
        int index = kcmbTimeoutMode.SelectedIndex;
        return index switch
        {
            0 => 5000,   // Default (5 seconds)
            1 => 30000,  // Extended (30 seconds)
            2 => 0,      // Infinite
            _ => 5000
        };
    }

    private static string GetModeDescription(int autoPopDelay)
    {
        return autoPopDelay switch
        {
            0 => "Infinite – tooltip stays until the pointer leaves the control.",
            5000 => "Default – tooltip hides after 5 seconds.",
            30000 => "Extended – tooltip hides after 30 seconds.",
            _ => $"Custom – tooltip hides after {autoPopDelay / 1000} seconds."
        };
    }

    private void ApplyToolTipValuesTimeout(int closeIntervalDelay)
    {
        // Controls with ToolTipValues (KryptonButton, etc.) use CloseIntervalDelay
        kbtnBuiltInTooltip.ToolTipValues.EnableToolTips = true;
        kbtnBuiltInTooltip.ToolTipValues.Heading = "Built-in ToolTipValues";
        kbtnBuiltInTooltip.ToolTipValues.Description = closeIntervalDelay == 0
            ? "This uses ToolTipValues.CloseIntervalDelay=0 (infinite). Stays until pointer leaves."
            : $"Uses CloseIntervalDelay={closeIntervalDelay}ms. Hides after {closeIntervalDelay / 1000} seconds.";
        kbtnBuiltInTooltip.ToolTipValues.CloseIntervalDelay = closeIntervalDelay;
        kbtnBuiltInTooltip.ToolTipValues.ShowIntervalDelay = 500;
    }

    private void kcmbTimeoutMode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ApplyTimeoutMode();
    }

    private void kbtnApply_Click(object sender, EventArgs e)
    {
        ApplyTimeoutMode();
        KryptonMessageBox.Show(this,
            "Timeout mode applied. Hover over the demo controls to see the tooltip behavior.",
            "Tooltip Timeout",
            KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Information);
    }
}
