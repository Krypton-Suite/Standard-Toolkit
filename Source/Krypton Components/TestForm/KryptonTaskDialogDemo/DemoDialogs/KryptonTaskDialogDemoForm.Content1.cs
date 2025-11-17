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
    public void KryptonTaskDialogDemoFormContent()
    {
        KryptonTaskDialog taskDialog = new KryptonTaskDialog();

        taskDialog.Dialog.Form.Text = "A Content sample with an image displayed";

        string text = BogusData.BogusGenerator.GenerateLoremIpsumLines(5) + Environment.NewLine + Environment.NewLine +
            BogusData.BogusGenerator.GenerateLoremIpsumLines(5) + Environment.NewLine + Environment.NewLine +
            BogusData.BogusGenerator.GenerateLoremIpsumLines(5) + Environment.NewLine + Environment.NewLine +
            BogusData.BogusGenerator.GenerateLoremIpsumLines(5);

        taskDialog.Content.ContentImage.Image = KryptonTaskDialogDemo.KryptonTaskDialogDemoResources.lorem_ipsum;
        taskDialog.Content.ContentImage.Size = new Size(150, 150);
        taskDialog.Content.ContentImage.Visible = true;
        taskDialog.Content.ContentImage.PositionedLeft = true;
        taskDialog.Content.Visible = true;
        taskDialog.Content.Text = text;

        taskDialog.Show(this);
    }
}
