#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

using BogusData;

public partial class KryptonTaskDialogDemoForm
{
    public void FreeWheeler1DataGridView(int dialogWidth = 800)
    {
        KryptonTaskDialog taskDialog = new(dialogWidth);

        taskDialog.Dialog.Form.ControlBox = false;
        taskDialog.Dialog.Form.CloseBox = false;
        taskDialog.Dialog.Form.Text = "FreeWheeler Database Grid Demo";
        taskDialog.Dialog.Form.StartPosition = FormStartPosition.CenterScreen;

        taskDialog.Heading.IconType = KryptonTaskDialogIconType.ShieldSuccess;
        taskDialog.Heading.Text = "People from across the world.";
        taskDialog.Heading.Visible = true;

        taskDialog.FooterBar.CommonButtons.Buttons = KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Cancel;
        taskDialog.FooterBar.CommonButtons.AcceptButton = KryptonTaskDialogCommonButtonTypes.OK;
        taskDialog.FooterBar.CommonButtons.CancelButton = KryptonTaskDialogCommonButtonTypes.Cancel;

        taskDialog.FooterBar.Footer.EnableExpanderControls = false;
        taskDialog.FooterBar.Footer.IconType = KryptonTaskDialogIconType.None;
        taskDialog.FooterBar.Footer.FootNoteText = string.Empty;
        taskDialog.FooterBar.ShowSeparator = true;
        taskDialog.FooterBar.Visible = true;

        KryptonDataGridView kdgv = new();

        List<BogusUser> bogusUsers = BogusGenerator.GenerateUserList(100);
        kdgv.DataSource = bogusUsers;
        kdgv.Dock = DockStyle.Fill;

        taskDialog.FreeWheeler2.Visible = true;
        taskDialog.FreeWheeler2.ElementHeight = 300;
        taskDialog.FreeWheeler2.TableLayoutPanel.Visible = true;
        taskDialog.FreeWheeler2.TableLayoutPanel.Controls.Add(kdgv);

        taskDialog.Show(this);
    }
}