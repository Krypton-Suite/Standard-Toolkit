#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #2984: NullReferenceException in ViewDrawSeparator.RenderBefore.
/// Exercises controls that use ViewDrawSeparator: KryptonNavigator (Outlook mode),
/// KryptonSplitContainer, and KryptonSeparator. Use the theme combo to swap palettes
/// and verify no crash during paint.
/// </summary>
public partial class Bug2984SeparatorTest : KryptonForm
{
    public Bug2984SeparatorTest()
    {
        InitializeComponent();
    }
}
