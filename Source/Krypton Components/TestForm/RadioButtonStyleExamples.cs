#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class RadioButtonStyleExamples : KryptonForm
{
    public RadioButtonStyleExamples()
    {
        InitializeComponent();
    }

    private void RadioButtonStyleExamples_Load(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"RadioButton styles for theme: {kryptonThemeComboBox1.Text}";

        // RadioButton fixed states
        rbFocus.SetFixedState(true, true, false, false);
        rbCheckedDisabled.SetFixedState(false, false, false, false);
        rbCheckedNormal.SetFixedState(false, true, false, false);
        rbCheckedTracking.SetFixedState(false, true, true, false);
        rbCheckedPressed.SetFixedState(false, true, false, true);
        rbUncheckedDisabled.SetFixedState(false, false, false, false);
        rbUncheckedNormal.SetFixedState(false, true, false, false);
        rbUncheckedTracking.SetFixedState(false, true, true, false);
        rbUncheckedPressed.SetFixedState(false, true, false, true);
    }

    private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        kryptonLabel1.Text = $"RadioButton styles for theme: {kryptonThemeComboBox1.Text}";
    }
}