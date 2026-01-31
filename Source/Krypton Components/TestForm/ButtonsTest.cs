#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ButtonsTest : KryptonForm
{
    public ButtonsTest()
    {
        InitializeComponent();
        kcbColorScheme.SelectedItem = "OfficeThemes";
        kcbSortMode.Enabled = false;
    }

    private void kcbtnDropDown_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonButton3.Values.DropDownArrowColor = e.Color;

        kryptonButton4.Values.DropDownArrowColor = e.Color;

        kryptonButton7.Values.DropDownArrowColor = e.Color;

        kryptonButton8.Values.DropDownArrowColor = e.Color;
    }

    private void kbtnButtonStyles_Click(object sender, EventArgs e)
    {
        new ButtonStyleExamples().Show();
    }

    private void kryptonCommand1_Execute(object sender, EventArgs e)
    {
        var typeName = sender.GetType().FullName;
        var item = sender as KryptonContextMenuItem;
        var paramText = item?.CommandParameter is string s ? s : "<no param>";

        KryptonMessageBox.Show($"Command executed by:\nType: {typeName}\nParam: {paramText}", "Context Item Called");
    }

    private void kcbSortMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var text = kcbSortMode.SelectedItem?.ToString();
        if (!Enum.TryParse<Krypton.Toolkit.ThemeColorSortMode>(text, true, out var mode))
        {
            mode = Krypton.Toolkit.ThemeColorSortMode.OKLCH;
        }
        kcbtnDropDown.ThemeColorSortMode = mode;
    }

    private void kcbColorScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        var text = kcbColorScheme.SelectedItem?.ToString();
        if (!Enum.TryParse<Krypton.Toolkit.ColorScheme>(text, true, out var scheme))
        {
            scheme = Krypton.Toolkit.ColorScheme.OfficeThemes;
        }

        kcbtnDropDown.SchemeThemes = scheme;
        kcbSortMode.Enabled = scheme == Krypton.Toolkit.ColorScheme.PaletteColors;
    }

    private void KryptonCalcInput1_ButtonSpecClicked(object sender, ButtonSpecEventArgs e)
    {
        if (e.ButtonSpec is ButtonSpecAny any && any.Type == PaletteButtonSpecStyle.Close)
        {
            KryptonCalcInput1.Value = 0m;
        }
    }
}