#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public class KryptonClearClipboard : ToolStripItem // MenuItem
{
    #region Identity

    public KryptonClearClipboard()
    {
        // Todo: Add an image to the button
        Text = KryptonManager.Strings.ToolStripItemStrings.ClearClipboardText;
    }
    #endregion

    #region Implementation

    private bool IsClipboardEmpty() => Clipboard.ContainsText();

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnClick(EventArgs e)
    {
        if (!IsClipboardEmpty())
        {
            Clipboard.Clear();
        }

        base.OnClick(e);
    }
    #endregion
}
