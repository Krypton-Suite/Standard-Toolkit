#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Issue #4366: dedicated <see cref="KryptonSplitButton"/> compared with <see cref="KryptonButton"/>
/// and <see cref="KryptonDropButton"/>.
/// </summary>
public partial class KryptonSplitButtonDemo : KryptonForm
{
    public KryptonSplitButtonDemo()
    {
        InitializeComponent();
    }

    private void OnAnyClick(object? sender, EventArgs e)
    {
        if (sender is Control control)
        {
            klblStatus.Values.Text = $"{control.Name}: Click (button body / default action)";
        }
    }

    private void OnAnyDropDown(object? sender, ContextPositionMenuArgs e)
    {
        if (sender is Control control)
        {
            klblStatus.Values.Text = $"{control.Name}: DropDown (chevron / whole drop-down)";
        }
    }
}
