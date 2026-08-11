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
        BackColor = SystemColors.Control;
    }

    /// <inheritdoc />
    protected override bool ShowWithoutActivation => true;

    /// <inheritdoc />
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            // Tool window: no taskbar button, stays with the owner without a caption chrome.
            cp.ExStyle |= 0x00000080; // WS_EX_TOOLWINDOW
            return cp;
        }
    }
}
