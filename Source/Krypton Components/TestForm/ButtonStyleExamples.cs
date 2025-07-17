#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ButtonStyleExamples : KryptonForm
{
    public ButtonStyleExamples()
    {
        InitializeComponent();
    }

    private void ButtonStyleExamples_Load(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"Button styles for theme: {kryptonThemeComboBox1.Text}";

        // Button fixed states
        buttonDisabled.SetFixedState(PaletteState.Disabled);
        buttonDefaultFocus.SetFixedState(PaletteState.NormalDefaultOverride);
        buttonNormal.SetFixedState(PaletteState.Normal);
        buttonTracking.SetFixedState(PaletteState.Tracking);
        buttonPressed.SetFixedState(PaletteState.Pressed);
        buttonCheckedNormal.SetFixedState(PaletteState.CheckedNormal);
        buttonCheckedTracking.SetFixedState(PaletteState.CheckedTracking);
        buttonCheckedPressed.SetFixedState(PaletteState.CheckedPressed);
    }

    private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"Button styles for theme: {kryptonThemeComboBox1.Text}";
    }

    private void buttonLive_Click(object sender, EventArgs e)
    {

    }

    private void buttonDefaultFocus_Click(object sender, EventArgs e)
    {

    }

    private void buttonTracking_Click(object sender, EventArgs e)
    {

    }

    private void buttonNormal_Click(object sender, EventArgs e)
    {

    }

    private void buttonPressed_Click(object sender, EventArgs e)
    {

    }

    private void buttonDisabled_Click(object sender, EventArgs e)
    {

    }

    private void buttonCheckedNormal_Click(object sender, EventArgs e)
    {

    }

    private void buttonCheckedTracking_Click(object sender, EventArgs e)
    {

    }

    private void buttonCheckedPressed_Click(object sender, EventArgs e)
    {

    }
}