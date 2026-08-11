#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Borderless top-level host used when a <see cref="KryptonRadialMenuControl"/> is dragged outside its parent.
/// </summary>
internal sealed class VisualRadialMenuFloatForm : Form
{
    private static readonly Color TransparencyKeyColor = Color.Magenta;

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualRadialMenuFloatForm"/> class.
    /// </summary>
    public VisualRadialMenuFloatForm()
    {
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        MinimizeBox = false;
        MaximizeBox = false;
        ControlBox = false;
        AutoScaleMode = AutoScaleMode.None;
        KeyPreview = true;
        Text = @"Radial Menu";
        // Match Magenta fills from the hosted control so corners outside the radial artwork are see-through.
        BackColor = TransparencyKeyColor;
        TransparencyKey = TransparencyKeyColor;
    }

    /// <inheritdoc />
    protected override bool ShowWithoutActivation => true;

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        using var brush = new SolidBrush(TransparencyKeyColor);
        e.Graphics.FillRectangle(brush, e.ClipRectangle);
    }

    /// <inheritdoc />
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            // Tool window: no taskbar button, stays with the owner without a caption chrome.
            cp.ExStyle |= unchecked((int)PI.WS_EX_.TOOLWINDOW);
            return cp;
        }
    }
}
