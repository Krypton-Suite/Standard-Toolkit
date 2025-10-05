#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class LabelStyleExamples : KryptonForm
{
    public LabelStyleExamples()
    {
        InitializeComponent();
    }

    private void LabelStyleExamples_Load(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"Label styles for theme: {kryptonThemeComboBox1.Text}";

        // Labels fixed states
        label1Disabled.SetFixedState(PaletteState.Disabled);
        label1Normal.SetFixedState(PaletteState.Normal);
        label1LinkDisabled.SetFixedState(PaletteState.Disabled);
        label1Visited.SetFixedState(PaletteState.Normal);
        label1NotVisited.SetFixedState(PaletteState.Normal);
        label1Pressed.SetFixedState(PaletteState.Pressed);
    }

    private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"Label styles for theme: {kryptonThemeComboBox1.Text}";
    }
}