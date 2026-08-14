#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion


namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonPaletteSpecificValues : Storage
{
    #region Static Fields

    private const bool DEFAULT_USE_CLOSE_BUTTON_KRYPTON_TRACKING_COLORS = true;

    private const bool DEFAULT_USE_KRYPTON_PALETTE_TRACKING_COLORS = true;

    #endregion

    #region Instance Fields

    private KryptonManager _manager;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonPaletteSpecificValues"/> class.
    /// </summary>
    /// <param name="manager">The manager.</param>
    public KryptonPaletteSpecificValues(KryptonManager manager)
    {
        _manager = manager ?? ThrowHelper.ThrowArgumentNullException(manager);

        Reset();
    }

    #endregion

    [Browsable(false)]
    public override bool IsDefault => UseCloseButtonKryptonTrackingColors.Equals(DEFAULT_USE_CLOSE_BUTTON_KRYPTON_TRACKING_COLORS) &&
           UseKryptonPaletteTrackingColors.Equals(DEFAULT_USE_KRYPTON_PALETTE_TRACKING_COLORS);

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether the control box buttons should use the Krypton palette tracking colors when the mouse is over them (Office 2013 and Microsoft 365).
    /// </summary>
    [Description("Should the control box buttons use the Krypton palette tracking colors when the mouse is over them (Office 2013 and Microsoft 365).")]
    [Category("Visuals")]
    [DefaultValue(DEFAULT_USE_CLOSE_BUTTON_KRYPTON_TRACKING_COLORS)]
    public bool UseCloseButtonKryptonTrackingColors { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the controls should use the Krypton palette tracking colors when the mouse is over them (Office 2013 and Microsoft 365).
    /// </summary>
    [Description("Should the controls use the Krypton palette tracking colors when the mouse is over them (Office 2013 and Microsoft 365).")]
    [Category("Visuals")]
    [DefaultValue(DEFAULT_USE_KRYPTON_PALETTE_TRACKING_COLORS)]
    public bool UseKryptonPaletteTrackingColors { get; set; }

    #endregion

    #region Implementation

    public void Reset()
    {
        UseCloseButtonKryptonTrackingColors = DEFAULT_USE_CLOSE_BUTTON_KRYPTON_TRACKING_COLORS;
        UseKryptonPaletteTrackingColors = DEFAULT_USE_KRYPTON_PALETTE_TRACKING_COLORS;
    }

    #endregion

    #region Public Overrides

    public override string ToString() => IsDefault ? string.Empty : @"Modified";

    #endregion
}
