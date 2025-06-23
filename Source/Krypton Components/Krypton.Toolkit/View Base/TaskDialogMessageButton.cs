#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Internal button used by the VisualTaskDialog
/// </summary>
[ToolboxItem(false)]
public class TaskDialogMessageButton : KryptonButton
{
    #region Identity

    /// <summary>
    /// Gets and sets the ignoring of Alt+F4
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool IgnoreAltF4 { get; set; }

    #endregion

    #region Protected

    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">The Windows Message to process. </param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.KEYDOWN:
            case PI.WM_.SYSKEYDOWN:
                if (IgnoreAltF4)
                {
                    // Extract the keys being pressed
                    var keys = (Keys)(int)m.WParam.ToInt64();

                    // If the user standard combination ALT + F4
                    if ((keys == Keys.F4) && ((ModifierKeys & Keys.Alt) == Keys.Alt))
                    {
                        // Eat the message, so standard window proc does not close the window
                        return;
                    }
                }

                break;
        }

        base.WndProc(ref m);
    }

    #endregion
}