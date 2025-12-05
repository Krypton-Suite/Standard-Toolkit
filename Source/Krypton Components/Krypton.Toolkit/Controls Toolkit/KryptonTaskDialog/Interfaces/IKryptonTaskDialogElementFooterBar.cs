#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementFooterBar
{
    /// <summary>
    /// Footnote text element to display.
    /// </summary>
    public string FootNoteText { get; set; }

    /// <summary>
    /// Text shown when the Expander element is expanded.
    /// </summary>
    public string ExpanderExpandedText { get; set; }

    /// <summary>
    /// Text shown when the Expander element is collapsed.
    /// </summary>
    public string ExpanderCollapsedText { get; set; }

    /// <summary>
    /// When disabled, the user cannot control the expander element.
    /// </summary>
    public bool EnableExpanderControls { get; set; }
}
