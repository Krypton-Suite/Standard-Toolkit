#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Specifies the type of control used to display suggestions.
/// </summary>
public enum SearchSuggestionDisplayType
{
    /// <summary>
    /// Display suggestions using a KryptonListBox.
    /// </summary>
    ListBox,

    /// <summary>
    /// Display suggestions using a KryptonDataGridView.
    /// </summary>
    DataGridView
}