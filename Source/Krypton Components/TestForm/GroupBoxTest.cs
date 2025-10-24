#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class GroupBoxTest : KryptonForm
{
    public GroupBoxTest()
    {
        InitializeComponent();

        // Runtime disable second combo to mirror the issue scenario
        kryptonComboBoxCtorDisabled.Enabled = false;

        // Seed items already set by designer; select first item for both
        if (kryptonComboBoxDesignDisabled.Items.Count > 0)
        {
            kryptonComboBoxDesignDisabled.SelectedIndex = 0;
        }
        if (kryptonComboBoxCtorDisabled.Items.Count > 0)
        {
            kryptonComboBoxCtorDisabled.SelectedIndex = 0;
        }
    }

    private void kryptonButtonToggleComboEnabled_Click(object? sender, EventArgs e)
    {
        kryptonComboBoxDesignDisabled.Enabled = !kryptonComboBoxDesignDisabled.Enabled;
        kryptonComboBoxCtorDisabled.Enabled = !kryptonComboBoxCtorDisabled.Enabled;
    }
}