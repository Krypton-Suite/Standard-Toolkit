#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonRating"/> precision, hover preview, glyphs, and theme colours (issue #3928).
/// </summary>
public partial class KryptonRatingDemo : KryptonForm
{
    public KryptonRatingDemo()
    {
        InitializeComponent();
    }

    private void KryptonRatingDemo_Load(object? sender, EventArgs e)
    {
        PopulateCombo(kcmbPrecision, typeof(KryptonRatingPrecision), kratingMain.Precision);
        PopulateCombo(kcmbGlyph, typeof(KryptonRatingGlyph), kratingMain.RatingValues.Glyph);
        PopulateCombo(kcmbOrientation, typeof(Orientation), kratingMain.Orientation);

        knudMaximum.Value = kratingMain.Maximum;
        knudItemSize.Value = kratingMain.RatingValues.ItemSize;
        kchkReadOnly.Checked = kratingMain.ReadOnly;
        kchkAllowClear.Checked = kratingMain.AllowClear;
        kchkEnabled.Checked = kratingMain.Enabled;
        kchkRtl.Checked = kratingMain.RightToLeft == RightToLeft.Yes;

        kratingDisabled.Value = 3m;
        kratingImage.Value = 4m;
        kratingImage.RatingValues.Glyph = KryptonRatingGlyph.Image;
        kratingHeart.Value = 2.5m;
        kratingHeart.Precision = KryptonRatingPrecision.Half;
        kratingHeart.RatingValues.Glyph = KryptonRatingGlyph.Heart;
        kratingVertical.Value = 3m;
        kratingVertical.Orientation = Orientation.Vertical;
        kratingVertical.Maximum = 5;

        kpgMain.SelectedObject = kratingMain;
        UpdateReadout();
    }

    private static void PopulateCombo(KryptonComboBox combo, Type enumType, object selected)
    {
        combo.Items.Clear();
        foreach (object value in Enum.GetValues(enumType))
        {
            combo.Items.Add(value);
        }

        combo.SelectedItem = selected;
    }

    private void UpdateReadout()
    {
        klblValue.Values.Text = $@"Value: {kratingMain.Value} / {kratingMain.Maximum}";
        klblHover.Values.Text = kratingMain.IsHovering
            ? $@"Hover: {kratingMain.HoverValue}"
            : @"Hover: (none)";
        klblStatus.Values.Text =
            @"Click a glyph, use arrow keys when focused, or type 1–9. Click the current rating again to clear when AllowClear is on.";
    }

    private void kratingMain_ValueChanged(object? sender, EventArgs e) => UpdateReadout();

    private void kratingMain_MouseMove(object? sender, MouseEventArgs e) => UpdateReadout();

    private void kratingMain_MouseLeave(object? sender, EventArgs e) => UpdateReadout();

    private void kcmbPrecision_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbPrecision.SelectedItem is KryptonRatingPrecision precision)
        {
            kratingMain.Precision = precision;
            UpdateReadout();
        }
    }

    private void kcmbGlyph_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbGlyph.SelectedItem is KryptonRatingGlyph glyph)
        {
            kratingMain.RatingValues.Glyph = glyph;
        }
    }

    private void kcmbOrientation_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbOrientation.SelectedItem is Orientation orientation)
        {
            kratingMain.Orientation = orientation;
        }
    }

    private void knudMaximum_ValueChanged(object? sender, EventArgs e)
    {
        kratingMain.Maximum = (int)knudMaximum.Value;
        UpdateReadout();
    }

    private void knudItemSize_ValueChanged(object? sender, EventArgs e) =>
        kratingMain.RatingValues.ItemSize = (int)knudItemSize.Value;

    private void kchkReadOnly_CheckedChanged(object? sender, EventArgs e) =>
        kratingMain.ReadOnly = kchkReadOnly.Checked;

    private void kchkAllowClear_CheckedChanged(object? sender, EventArgs e) =>
        kratingMain.AllowClear = kchkAllowClear.Checked;

    private void kchkEnabled_CheckedChanged(object? sender, EventArgs e) =>
        kratingMain.Enabled = kchkEnabled.Checked;

    private void kchkRtl_CheckedChanged(object? sender, EventArgs e) =>
        kratingMain.RightToLeft = kchkRtl.Checked ? RightToLeft.Yes : RightToLeft.No;

    private void kbtnFillGold_Click(object? sender, EventArgs e)
    {
        kratingMain.ResetStateCommon();
        kratingMain.ResetStateNormal();
        kratingMain.ResetStateTracking();
        kratingMain.ResetStateDisabled();
        UpdateReadout();
    }

    private void kbtnFillBlue_Click(object? sender, EventArgs e)
    {
        kratingMain.StateNormal.Fill = Color.DodgerBlue;
        kratingMain.StateTracking.Fill = Color.SkyBlue;
        kratingMain.StateCommon.Empty = Color.LightSteelBlue;
    }
}
