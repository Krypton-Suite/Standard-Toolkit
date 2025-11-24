#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonTaskDialogElementFooterBar
{
    public enum CommonButtonsProperties
    {
        None = 0,
        Buttons,
        AcceptButton,
        CancelButton,
    }

    public enum FooterBarProperties
    {
        None = 0,
        FootNoteText,
        ExpanderExpandedText,
        ExpanderCollapsedText,
        IconType,
        EnableExpanderControls
    }
}