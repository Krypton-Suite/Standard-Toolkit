#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class KryptonTaskDialogDemoForm : KryptonForm
{
    public DialogResult CommandLinkButtons1(string headingText, 
        string message, 
        KryptonTaskDialogIconType headingIcon, 
        KryptonTaskDialogCommonButtonTypes buttons,
        KryptonTaskDialogCommonButtonTypes acceptButton,
        KryptonTaskDialogCommonButtonTypes cancelButton,
        int dialogWidth = 600)
    {
        KryptonTaskDialog taskDialog = new(dialogWidth);

        taskDialog.Dialog.Form.ControlBox = false;
        taskDialog.Dialog.Form.CloseBox = false;
        taskDialog.Dialog.Form.Text = string.Empty;
        taskDialog.Dialog.Form.StartPosition = FormStartPosition.CenterScreen;

        taskDialog.Content.Text = message;
        taskDialog.Content.ShowSeparator = true;
        taskDialog.Content.ContentImage.Visible = false;
        taskDialog.Content.Visible = true;

        taskDialog.Heading.IconType = headingIcon;
        taskDialog.Heading.Text = headingText;
        taskDialog.Heading.Visible = true;

        taskDialog.FooterBar.CommonButtons.Buttons = buttons;
        taskDialog.FooterBar.CommonButtons.AcceptButton = acceptButton;
        taskDialog.FooterBar.CommonButtons.CancelButton = cancelButton;

        taskDialog.FooterBar.Footer.EnableExpanderControls = false;
        taskDialog.FooterBar.Footer.IconType = KryptonTaskDialogIconType.None;
        taskDialog.FooterBar.Footer.FootNoteText = string.Empty;
        taskDialog.FooterBar.ShowSeparator = true;
        taskDialog.FooterBar.Visible = true;

        return taskDialog.ShowDialog(this);
    }
}