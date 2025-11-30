#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class KryptonTaskDialogDemoForm
{
    private void ModelessProgressDialog1()
    {
        KryptonTaskDialog taskDialog = new(750);

        taskDialog.Dialog.Form.Text = "System progress message";
        taskDialog.Dialog.Form.TopMost = true;
        taskDialog.Dialog.Form.StartPosition = FormStartPosition.CenterScreen;

        taskDialog.Dialog.Form.CloseBox = false;
        taskDialog.Dialog.Form.MaximizeBox = false;
        taskDialog.Dialog.Form.MinimizeBox = false;
        taskDialog.Dialog.Form.ControlBox = true;

        taskDialog.Content.Text =
            "Sit tight and hang on while we are checking your computer.\n" +
            "As soon as the process completes the dialog will be closed.";

        taskDialog.Content.ContentImage.Visible = false;
        taskDialog.Content.Visible = true;

        taskDialog.ProgresBar.ShowDescription = false;
        taskDialog.ProgresBar.ProgressBar.Minimum = 0;
        taskDialog.ProgresBar.ProgressBar.Maximum = 100;
        taskDialog.ProgresBar.ProgressBar.Value = 0;
        taskDialog.ProgresBar.ProgressBar.UseValueAsText = true;
        taskDialog.ProgresBar.ShowSeparator = true;
        taskDialog.ProgresBar.Visible = true;

        taskDialog.FooterBar.Footer.EnableExpanderControls = false;
        taskDialog.FooterBar.CommonButtons.Buttons = KryptonTaskDialogCommonButtonTypes.OK;
        taskDialog.FooterBar.CommonButtons.AcceptButton = KryptonTaskDialogCommonButtonTypes.None;
        taskDialog.FooterBar.CommonButtons.CancelButton = KryptonTaskDialogCommonButtonTypes.None;
        taskDialog.FooterBar.ShowSeparator = true;
        taskDialog.FooterBar.Visible = true;

        taskDialog.Show();

        for (int i = 0; i < 100; i++)
        {
            taskDialog.ProgresBar.ProgressBar.Value = i;
            Application.DoEvents();
            System.Threading.Thread.Sleep(20);
        }

        taskDialog.CloseDialog();
    }
}