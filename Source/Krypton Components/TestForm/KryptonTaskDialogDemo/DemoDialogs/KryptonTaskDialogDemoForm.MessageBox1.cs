#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class KryptonTaskDialogDemoForm
{
    /// <summary>
    /// Roll your own messagebox 
    /// </summary>
    /// <param name="message">Say what?</param>
    /// <param name="dialogWidth">It does what it says</param>
    public void MessageBox1(string message, int dialogWidth = 600)
    {
        KryptonTaskDialog taskDialog = new(dialogWidth);

        taskDialog.Dialog.Form.ControlBox = true;
        taskDialog.Dialog.Form.CloseBox   = false;
        taskDialog.Dialog.Form.Text       = string.Empty;

        taskDialog.Heading.IconType = KryptonTaskDialogIconType.ShieldWarning;
        taskDialog.Heading.Text     = message;
        taskDialog.Heading.Visible  = true;

        taskDialog.FooterBar.CommonButtons.Buttons      = KryptonTaskDialogCommonButtonTypes.OK;
        taskDialog.FooterBar.CommonButtons.AcceptButton = KryptonTaskDialogCommonButtonTypes.OK;
        taskDialog.FooterBar.CommonButtons.CancelButton = KryptonTaskDialogCommonButtonTypes.None;

        taskDialog.FooterBar.Footer.EnableExpanderControls = false;
        taskDialog.FooterBar.Footer.IconType               = KryptonTaskDialogIconType.None;
        taskDialog.FooterBar.Footer.FootNoteText           = string.Empty;
        taskDialog.FooterBar.ShowSeparator                 = true;

        taskDialog.ShowDialog(this);
    }
}