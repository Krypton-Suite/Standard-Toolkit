#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Opt-in options for semantic accept / cancel / help / neutral dialog button colours.
/// </summary>
/// <remarks>
/// When <see cref="Scheme"/> is <see cref="KryptonDialogButtonColorScheme.None"/> and no colour
/// overrides are set, buttons keep themed Standalone chrome. Per-role override colours win over
/// the selected preset. Pass an instance to message-box / task-dialog APIs, or set
/// <see cref="KryptonManager.DialogButtonColors"/> for an application-wide default.
/// </remarks>
public class KryptonDialogButtonColorOptions
{
    #region Public

    /// <summary>Gets or sets the named colour scheme to apply.</summary>
    public KryptonDialogButtonColorScheme Scheme { get; set; } = KryptonDialogButtonColorScheme.None;

    /// <summary>Gets or sets an optional Accept-role fill colour override.</summary>
    public Color? AcceptBackColor { get; set; }

    /// <summary>Gets or sets an optional Accept-role border colour override.</summary>
    public Color? AcceptBorderColor { get; set; }

    /// <summary>Gets or sets an optional Accept-role text colour override.</summary>
    public Color? AcceptTextColor { get; set; }

    /// <summary>Gets or sets an optional Cancel-role fill colour override.</summary>
    public Color? CancelBackColor { get; set; }

    /// <summary>Gets or sets an optional Cancel-role border colour override.</summary>
    public Color? CancelBorderColor { get; set; }

    /// <summary>Gets or sets an optional Cancel-role text colour override.</summary>
    public Color? CancelTextColor { get; set; }

    /// <summary>Gets or sets an optional Help-role fill colour override.</summary>
    public Color? HelpBackColor { get; set; }

    /// <summary>Gets or sets an optional Help-role border colour override.</summary>
    public Color? HelpBorderColor { get; set; }

    /// <summary>Gets or sets an optional Help-role text colour override.</summary>
    public Color? HelpTextColor { get; set; }

    /// <summary>Gets or sets an optional Neutral-role fill colour override.</summary>
    public Color? NeutralBackColor { get; set; }

    /// <summary>Gets or sets an optional Neutral-role border colour override.</summary>
    public Color? NeutralBorderColor { get; set; }

    /// <summary>Gets or sets an optional Neutral-role text colour override.</summary>
    public Color? NeutralTextColor { get; set; }

    /// <summary>
    /// Gets a value indicating whether semantic colours should be applied
    /// (<see cref="Scheme"/> is not <see cref="KryptonDialogButtonColorScheme.None"/>, or any override is set).
    /// </summary>
    public bool IsActive =>
        Scheme != KryptonDialogButtonColorScheme.None || HasAnyOverride;

    /// <summary>Gets a value indicating whether any per-role colour override is set.</summary>
    public bool HasAnyOverride =>
        AcceptBackColor.HasValue || AcceptBorderColor.HasValue || AcceptTextColor.HasValue ||
        CancelBackColor.HasValue || CancelBorderColor.HasValue || CancelTextColor.HasValue ||
        HelpBackColor.HasValue || HelpBorderColor.HasValue || HelpTextColor.HasValue ||
        NeutralBackColor.HasValue || NeutralBorderColor.HasValue || NeutralTextColor.HasValue;

    /// <summary>Gets a preset options instance for <see cref="KryptonDialogButtonColorScheme.Standard"/>.</summary>
    public static KryptonDialogButtonColorOptions Standard =>
        new KryptonDialogButtonColorOptions { Scheme = KryptonDialogButtonColorScheme.Standard };

    /// <summary>Gets a preset options instance for <see cref="KryptonDialogButtonColorScheme.Deuteranopia"/>.</summary>
    public static KryptonDialogButtonColorOptions Deuteranopia =>
        new KryptonDialogButtonColorOptions { Scheme = KryptonDialogButtonColorScheme.Deuteranopia };

    /// <summary>Gets a preset options instance for <see cref="KryptonDialogButtonColorScheme.Protanopia"/>.</summary>
    public static KryptonDialogButtonColorOptions Protanopia =>
        new KryptonDialogButtonColorOptions { Scheme = KryptonDialogButtonColorScheme.Protanopia };

    /// <summary>Gets a preset options instance for <see cref="KryptonDialogButtonColorScheme.HighContrast"/>.</summary>
    public static KryptonDialogButtonColorOptions HighContrast =>
        new KryptonDialogButtonColorOptions { Scheme = KryptonDialogButtonColorScheme.HighContrast };

    /// <summary>
    /// Creates a <see cref="KryptonDialogButtonColorScheme.Custom"/> options instance that relies on overrides.
    /// </summary>
    /// <returns>A new options instance with <see cref="Scheme"/> set to Custom.</returns>
    public static KryptonDialogButtonColorOptions CreateCustom() =>
        new KryptonDialogButtonColorOptions { Scheme = KryptonDialogButtonColorScheme.Custom };

    #endregion
}
