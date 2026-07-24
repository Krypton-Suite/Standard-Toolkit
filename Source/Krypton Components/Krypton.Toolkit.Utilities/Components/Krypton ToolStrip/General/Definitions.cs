#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region Enum BlinkState

public enum BlinkState
{
    NormalBlink = 0,
    SoftBlink = 1
}

#endregion

#region Enum BlinkMode

/// <summary>
/// Blink animation style for <see cref="KryptonBlinkingToolStripStatusLabel"/>.
/// </summary>
public enum BlinkMode
{
    /// <summary>
    /// Abruptly alternate between <see cref="BlinkingStatusLabelValues.BlinkColorOne"/> and
    /// <see cref="BlinkingStatusLabelValues.BlinkColorTwo"/> on each interval tick.
    /// </summary>
    Hard = 0,

    /// <summary>
    /// Smoothly interpolate colours between the two blink endpoints over a full soft-blink cycle.
    /// </summary>
    Soft = 1,

    /// <summary>
    /// Hide and show the label content (via transparent text or <c>Visible</c>), controlled by
    /// <see cref="VisibilityStyle"/>.
    /// </summary>
    Visibility = 2
}

#endregion

#region Enum BlinkTarget

/// <summary>
/// Which colour channels are driven by hard/soft blink animation.
/// Ignored when <see cref="BlinkMode"/> is <see cref="BlinkMode.Visibility"/>.
/// </summary>
public enum BlinkTarget
{
    /// <summary>
    /// Animate <c>ForeColor</c> only.
    /// </summary>
    Foreground = 0,

    /// <summary>
    /// Animate <c>BackColor</c> only.
    /// </summary>
    Background = 1,

    /// <summary>
    /// Animate both <c>ForeColor</c> and <c>BackColor</c>.
    /// </summary>
    Both = 2
}

#endregion

#region Enum VisibilityStyle

/// <summary>
/// How <see cref="BlinkMode.Visibility"/> hides the label during the off phase.
/// </summary>
public enum VisibilityStyle
{
    /// <summary>
    /// Hide text by setting <c>ForeColor</c> to transparent (avoids StatusStrip reflow). Default.
    /// </summary>
    HideText = 0,

    /// <summary>
    /// Toggle the item <c>Visible</c> property (may cause StatusStrip layout changes).
    /// </summary>
    ToggleVisible = 1
}

#endregion

#region Enum CheckMarkDisplayStyle

/// <summary>
/// Check mark display style. 
/// </summary>
public enum CheckMarkDisplayStyle
{
    CheckBox = 0,
    RadioButton = 1
}

#endregion

#region Enum MarqueeScrollDirection

public enum MarqueeScrollDirection
{
    RightToLeft,
    LeftToRight
}

#endregion

#region Enum StylePresets

public enum StylePresets
{
    MacOSX,
    Firefox,
    IE7,
    Custom
}

#endregion
