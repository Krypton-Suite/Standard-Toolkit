#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class ProgressBarTest : KryptonForm
{
    public ProgressBarTest()
    {
        InitializeComponent();
    }

    private void ProgressBarTest_Load(object sender, EventArgs e)
    {
        foreach (var value in Enum.GetValues(typeof(ProgressBarStyle)))
        {
            kcmbProgressBarStyle.Items.Add(value);
        }

        kcmbProgressBarStyle.SelectedIndex = 1;
    }

    private void ktrkProgressValues_ValueChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.Value = ktrkProgressValues.Value;

        kryptonProgressBar2.Value = ktrkProgressValues.Value;
    }

    private void kchkUseProgressValueAsText_CheckedChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.UseValueAsText = kchkUseProgressValueAsText.Checked;

        kryptonProgressBar2.UseValueAsText = kchkUseProgressValueAsText.Checked;
    }

    private void kcmbProgressBarStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);

        kryptonProgressBar2.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);
    }

    private void kcbtnProgressBarColour_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonProgressBar1.StateCommon.Back.Color1 = e.Color;

        kryptonProgressBar2.StateCommon.Back.Color1 = e.Color;
    }
}