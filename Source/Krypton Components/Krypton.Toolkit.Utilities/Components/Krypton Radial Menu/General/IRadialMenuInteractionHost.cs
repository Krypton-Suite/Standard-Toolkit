#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Host surface callbacks used by <see cref="RadialMenuInteractionCore"/>.
/// </summary>
internal interface IRadialMenuInteractionHost
{
    /// <summary>Gets appearance values.</summary>
    KryptonRadialMenuValues Values { get; }

    /// <summary>Gets the root item collection.</summary>
    KryptonRadialMenuItemCollection RootItems { get; }

    /// <summary>Gets whether the host is enabled.</summary>
    bool Enabled { get; }

    /// <summary>Gets appearance colour resolvers.</summary>
    IRadialMenuAppearance Appearance { get; }

    /// <summary>Gets the active palette.</summary>
    PaletteBase ResolvePalette();

    /// <summary>Gets the client size of the painted surface.</summary>
    Size ClientSize { get; }

    /// <summary>Gets whether layout should mirror for RTL.</summary>
    bool IsRightToLeft { get; }

    /// <summary>
    /// Gets the combined layout scale (device DPI / 96 × <see cref="KryptonRadialMenuValues.Scale"/>).
    /// </summary>
    float LayoutScale { get; }

    /// <summary>Gets the scaled, viewport-clamped layout metrics for the current surface.</summary>
    RadialMenuMetrics Metrics { get; }

    /// <summary>Gets the outer radius used for layout and painting.</summary>
    int EffectiveMenuRadius { get; }

    /// <summary>Gets the inner radius used for layout and painting.</summary>
    int EffectiveInnerRadius { get; }

    /// <summary>Gets optional tooltip host; may be <see langword="null"/>.</summary>
    RadialMenuToolTipHost? ToolTipHost { get; }

    /// <summary>
    /// Gets whether leaf <see cref="KryptonRadialMenuItem.AutoClose"/> should dismiss the host.
    /// </summary>
    bool SupportsAutoClose { get; }

    /// <summary>Requests a paint of the radial surface.</summary>
    void InvalidateSurface();

    /// <summary>Raises item activation.</summary>
    /// <param name="item">Activated item.</param>
    void RaiseItemClick(KryptonRadialMenuItemBase item);

    /// <summary>Raises root centre-button activation.</summary>
    void RaiseCenterButtonClick();

    /// <summary>Requests the host close (popup only; hosted control may no-op).</summary>
    /// <param name="reason">Close reason.</param>
    void RequestClose(ToolStripDropDownCloseReason reason);

    /// <summary>Called after navigation rebuilds the current ring.</summary>
    void OnNavigated();

    /// <summary>Updates the host accessible name.</summary>
    /// <param name="name">Accessible name.</param>
    void SetAccessibleName(string name);
}
