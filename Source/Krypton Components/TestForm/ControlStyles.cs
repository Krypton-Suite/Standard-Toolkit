#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ControlStyles : KryptonForm
{
    public ControlStyles()
    {
        InitializeComponent();
    }

    private void kbtnButtonStyles_Click(object sender, EventArgs e)
    {
        new ButtonStyleExamples().Show();
    }

    private void kbtnCheckBoxStyles_Click(object sender, EventArgs e)
    {
        new CheckBoxStyleExamples().Show();
    }

    private void kbtnRadioButtonStyles_Click(object sender, EventArgs e)
    {
        new RadioButtonStyleExamples().Show();
    }

    private void kbtnPanelStyles_Click(object sender, EventArgs e)
    {
        new PanelStyleExamples().Show();
    }

    private void kbtnLabelStyles_Click(object sender, EventArgs e)
    {
        new LabelStyleExamples().Show();
    }
}