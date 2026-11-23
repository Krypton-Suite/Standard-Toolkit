#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Optional pulsing form chrome for the update prompt, notes, and progress windows.</summary>
public sealed class KryptonUpdatePulse
{
    /// <summary>Initializes pulse settings.</summary>
    /// <param name="enable">When true, the form chrome glows.</param>
    /// <param name="color">Primary glow colour. Empty keeps the toolkit default.</param>
    /// <param name="style">Edge style. Defaults to <see cref="InputPulsingBorderStyle.All"/>.</param>
    /// <param name="speed">Animation speed multiplier. Values at or below zero become 1.</param>
    public KryptonUpdatePulse(bool enable, Color color, InputPulsingBorderStyle style, float speed)
    {
        Enable = enable;
        Color = color;
        Style = style;
        Speed = speed <= 0f ? 1f : speed;
    }

    /// <summary>Gets whether the form chrome glows.</summary>
    public bool Enable { get; }

    /// <summary>Gets the primary glow colour. Empty keeps the toolkit default.</summary>
    public Color Color { get; }

    /// <summary>Gets whether the glow is on the bottom edge or the full border.</summary>
    public InputPulsingBorderStyle Style { get; }

    /// <summary>Gets the animation speed multiplier.</summary>
    public float Speed { get; }
}
