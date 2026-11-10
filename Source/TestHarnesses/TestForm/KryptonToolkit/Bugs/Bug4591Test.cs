#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Repro for issue #4591: Events toolbar button missing on <see cref="KryptonPropertyGrid"/>.
/// </summary>
public partial class Bug4591Test : KryptonForm
{
    public Bug4591Test()
    {
        InitializeComponent();
    }

    private void kcbShowEventsButton_CheckedChanged(object sender, EventArgs e) =>
        kryptonPropertyGrid1.SetEventsButtonVisible(kcbShowEventsButton.Checked);

    private void kbtnSelectSample_Click(object sender, EventArgs e) =>
        kryptonPropertyGrid1.SelectedObject = kryptonButton1;

    private void kbtnClearSelection_Click(object sender, EventArgs e) =>
        kryptonPropertyGrid1.SelectedObject = null;
}
