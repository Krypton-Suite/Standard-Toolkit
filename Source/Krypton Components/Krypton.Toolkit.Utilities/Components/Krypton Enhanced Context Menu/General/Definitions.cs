#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Enum KryptonMiniToolbarPosition

/// <summary>
/// Position of the Mini Toolbar relative to the paired context menu.
/// </summary>
public enum KryptonMiniToolbarPosition
{
    /// <summary>Place the Mini Toolbar above the menu when space allows.</summary>
    Auto,

    /// <summary>Place the Mini Toolbar above the menu.</summary>
    Above,

    /// <summary>Place the Mini Toolbar below the menu.</summary>
    Below
}

#endregion

#region Enum KryptonMiniToolbarButtonType

/// <summary>
/// Button behaviour for a Mini Toolbar button.
/// </summary>
public enum KryptonMiniToolbarButtonType
{
    /// <summary>Momentary push button.</summary>
    Push,

    /// <summary>Toggle / check button.</summary>
    Check
}

#endregion
