#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class PanelStyleExamples : KryptonForm
{
    public PanelStyleExamples()
    {
        InitializeComponent();
    }

    private void PanelStyleExamples_Load(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"Panel styles for theme: {kryptonThemeComboBox1.Text}";

        // Panel fixed states
        panel1Disabled.SetFixedState(PaletteState.Disabled);
        kryptonPanel3.SetFixedState(PaletteState.Disabled);
        kryptonLabel8.SetFixedState(PaletteState.Disabled);
        panel1Normal.SetFixedState(PaletteState.Normal);
    }

    private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"Panel styles for theme: {kryptonThemeComboBox1.Text}";
    }
}