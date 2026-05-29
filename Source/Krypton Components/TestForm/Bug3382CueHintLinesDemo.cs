#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual demo for GitHub issue #3382: stray top/left lines when painting CueHint on <see cref="KryptonTextBox"/>
/// (vertical alignment incorrectly followed horizontal — cue was top-aligned when TextH was Near).
/// </summary>
public partial class Bug3382CueHintLinesDemo : KryptonForm
{
    public Bug3382CueHintLinesDemo()
    {
        InitializeComponent();
    }
}
