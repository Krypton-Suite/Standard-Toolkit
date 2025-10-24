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
    public DialogResult FreeWheeler2CheckedSetButtons()
    {
        KryptonTaskDialog taskDialog = new(500);
        KryptonCheckSet checkSet = new();

        taskDialog.Heading.Text = "Check That!";
        taskDialog.Heading.IconType = KryptonTaskDialogIconType.CheckGreen;
        taskDialog.Heading.Visible = true;

        for( int i = 0; i < 10; i++)
        {
            var button = new KryptonCheckButton()
            {
                Checked = false,
                Text = $"Button {i}",
                Size = new Size(75, 24)
            };

            checkSet.CheckButtons.Add(button);
            taskDialog.FreeWheeler1.FlowLayoutPanel.Controls.Add(button);
        }

        taskDialog.FreeWheeler1.FlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
        taskDialog.FreeWheeler1.Visible = true;

        return taskDialog.ShowDialog(this);
    }
}
