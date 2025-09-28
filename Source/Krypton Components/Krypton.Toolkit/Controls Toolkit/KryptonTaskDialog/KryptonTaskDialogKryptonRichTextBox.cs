#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Internally used by KryptonTaskDialogElementRichTextBox
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class KryptonTaskDialogKryptonRichTextBox : KryptonRichTextBox
{
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // This handles the enter key behaviour when the Richtextbox has focus and receives and Enter key.
        // This key may not be forwarded from the Richtextbox as it triggers the Form.AcceptButton and closes the form when set.
        if (keyData == Keys.Enter)
        {
            this.SelectedText = Environment.NewLine;
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
}