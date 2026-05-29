#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ProgressBarTest : KryptonForm
{
    public ProgressBarTest()
    {
        InitializeComponent();
        ktrkProgressValues.Value = 75;
    }

    private void ProgressBarTest_Load(object sender, EventArgs e)
    {
        foreach (var value in Enum.GetValues(typeof(ProgressBarStyle)))
        {
            kcmbProgressBarStyle.Items.Add(value);
        }

        kcmbProgressBarStyle.SelectedIndex = 1;

        foreach (var value in Enum.GetValues(typeof(PaletteColorStyle)))
        {
            kcmbColorStyle.Items.Add(value);
        }

        kcmbColorStyle.SelectedItem = PaletteColorStyle.GlassNormalFull;
    }

    private void ktrkProgressValues_ValueChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.Value = ktrkProgressValues.Value;

        kryptonProgressBar2.Value = ktrkProgressValues.Value;

        kryptonProgressBarVert1.Value = ktrkProgressValues.Value;

        kryptonProgressBarVert2.Value = ktrkProgressValues.Value;
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

        kryptonProgressBarVert1.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);

        kryptonProgressBarVert2.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);
    }

    private void kcbtnProgressBarColour_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonProgressBar1.StateCommon.Back.Color1 = e.Color;

        kryptonProgressBar2.StateCommon.Back.Color1 = e.Color;

        kryptonProgressBarVert1.StateCommon.Back.Color1 = e.Color;

        kryptonProgressBarVert2.StateCommon.Back.Color1 = e.Color;
    }

    private void kcmbColorStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        var style = (PaletteColorStyle)Enum.Parse(typeof(PaletteColorStyle), kcmbColorStyle.Text);
        kryptonProgressBar1.ValueBackColorStyle = style;

        kryptonProgressBar2.ValueBackColorStyle = style;

        kryptonProgressBarVert1.ValueBackColorStyle = style;

        kryptonProgressBarVert2.ValueBackColorStyle = style;
    }

    private void kchkShowTextBackdrop_CheckedChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.ShowTextBackdrop = kchkShowTextBackdrop.Checked;

        kryptonProgressBar2.ShowTextBackdrop = kchkShowTextBackdrop.Checked;

        kryptonProgressBarVert1.ShowTextBackdrop = kchkShowTextBackdrop.Checked;

        kryptonProgressBarVert2.ShowTextBackdrop = kchkShowTextBackdrop.Checked;
    }

    private void kchkShowTextShadow_CheckedChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.ShowTextShadow = kchkShowTextShadow.Checked;

        kryptonProgressBar2.ShowTextShadow = kchkShowTextShadow.Checked;

        kryptonProgressBarVert1.ShowTextShadow = kchkShowTextShadow.Checked;

        kryptonProgressBarVert2.ShowTextShadow = kchkShowTextShadow.Checked;
    }

    private void kcbtnBackdropColor_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonProgressBar1.TextBackdropColor = e.Color;

        kryptonProgressBar2.TextBackdropColor = e.Color;

        kryptonProgressBarVert1.TextBackdropColor = e.Color;

        kryptonProgressBarVert2.TextBackdropColor = e.Color;
    }
}