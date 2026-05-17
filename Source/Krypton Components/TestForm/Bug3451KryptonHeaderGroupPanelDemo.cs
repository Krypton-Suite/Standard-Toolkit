#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #3451: child controls must be parented to the internal Panel of
/// KryptonHeaderGroup, KryptonGroup, and KryptonGroupBox at design time.
/// </summary>
public partial class Bug3451KryptonHeaderGroupPanelDemo : KryptonForm
{
    public Bug3451KryptonHeaderGroupPanelDemo()
    {
        InitializeComponent();
    }
}
