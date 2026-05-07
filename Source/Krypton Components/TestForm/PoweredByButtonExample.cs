#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class PoweredByButtonExample : KryptonForm
{
    public PoweredByButtonExample()
    {
        InitializeComponent();

        foreach (var value in Enum.GetValues(typeof(ToolkitSupportType)))
        {
            kryptonComboBox1.Items.Add(value);
        }

        kryptonComboBox1.SelectedIndex = 0;
    }

    private void PoweredByButtonExample_Load(object sender, EventArgs e)
    {

    }

    private void kchkShowReadmeButton_CheckedChanged(object sender, EventArgs e)
    {
        kryptonPoweredByButton1.ButtonValues.ShowReadmeButton = kchkShowReadmeButton.Checked;
    }

    private void kchkShowChangelogButton_CheckedChanged(object sender, EventArgs e)
    {
        kryptonPoweredByButton1.ButtonValues.ShowChangeLogButton = kchkShowChangelogButton.Checked;
    }

    private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ToolkitSupportType support = (ToolkitSupportType)kryptonComboBox1.SelectedItem;

        kryptonPoweredByButton1.ButtonValues.ToolkitSupportType = support;
    }
}