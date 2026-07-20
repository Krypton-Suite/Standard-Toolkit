#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual repro for Issue #3902: Krypton scrollbars should sit flush against the control border
/// with no 1–2px white gutter between the scrollbar and the themed edge.
/// </summary>
public partial class Bug3902ScrollbarGapDemo : KryptonForm
{
    public Bug3902ScrollbarGapDemo()
    {
        InitializeComponent();
    }

    private void Bug3902ScrollbarGapDemo_Load(object? sender, EventArgs e)
    {
        PopulateScrollableContent();
    }

    private void PopulateScrollableContent()
    {
        var lines = new string[80];
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = $"Line {i + 1}: scroll content to expose Krypton scrollbars.";
        }

        string block = string.Join(Environment.NewLine, lines);
        ktbxDemo.Text = block;
        krtbDemo.Text = block;

        klstDemo.Items.Clear();
        for (int i = 0; i < 60; i++)
        {
            klstDemo.Items.Add($"Item {i + 1}: verify vertical scrollbar alignment.");
        }
    }

    private void kchkThemedCorner_CheckedChanged(object? sender, EventArgs e)
    {
        ScrollbarCornerStyle style = kchkThemedCorner.Checked
            ? ScrollbarCornerStyle.ExtendHorizontal
            : ScrollbarCornerStyle.ThemedCorner;

        ktbxDemo.ScrollbarManager.CornerStyle = style;
        krtbDemo.ScrollbarManager.CornerStyle = style;
        klstDemo.ScrollbarManager.CornerStyle = style;
    }

    private void kchkCustomCornerColor_CheckedChanged(object? sender, EventArgs e)
    {
        // Demonstrates the CornerState### palette overrides: checked applies a solid
        // custom color to the corner, unchecked reverts to the theme default.
        Color color1 = kchkCustomCornerColor.Checked ? Color.IndianRed : GlobalStaticVariables.EMPTY_COLOR;

        ktbxDemo.ScrollbarManager.CornerStateCommon.Color1 = color1;
        krtbDemo.ScrollbarManager.CornerStateCommon.Color1 = color1;
        klstDemo.ScrollbarManager.CornerStateCommon.Color1 = color1;
    }

    private void kchkCornerPanelStyle_CheckedChanged(object? sender, EventArgs e)
    {
        // Demonstrates CornerPanelStyle: checked draws the corner using the theme's
        // PanelAlternate style, unchecked reverts to the flat scrollbar fill.
        PaletteBackStyle? style = kchkCornerPanelStyle.Checked
            ? PaletteBackStyle.PanelAlternate
            : null;

        ktbxDemo.ScrollbarManager.CornerPanelStyle = style;
        krtbDemo.ScrollbarManager.CornerPanelStyle = style;
        klstDemo.ScrollbarManager.CornerPanelStyle = style;
    }
}
