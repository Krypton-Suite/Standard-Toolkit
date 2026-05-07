#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for general ribbon values.
/// </summary>
public class PaletteRibbonFileAppTab : Storage, IPaletteRibbonFileAppTab
{
    #region Instance Fields
    private Color _ribbonFileAppTabBottomColor;
    private Color _ribbonFileAppTabTopColor;
    private Color _ribbonFileAppTabTextColor;
    private IPaletteRibbonFileAppTab _inherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonGeneral class.
    /// </summary>
    /// <param name="inherit">Source for inheriting general values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonFileAppTab([DisallowNull] IPaletteRibbonFileAppTab inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Remember inheritance
        _inherit = inherit!;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Set default values
        _ribbonFileAppTabBottomColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;
        _ribbonFileAppTabTopColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;
        _ribbonFileAppTabTextColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeRibbonFileAppTabBottomColor() &&
                                      !ShouldSerializeRibbonFileAppTabTopColor() &&
                                      !ShouldSerializeRibbonFileAppTabTextColor()
    ;
    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteRibbonFileAppTab inherit) => _inherit = inherit;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        RibbonFileAppTabBottomColor = GetRibbonFileAppTabBottomColor(PaletteState.Normal);
        RibbonFileAppTabTopColor = GetRibbonFileAppTabTopColor(PaletteState.Normal);
        RibbonFileAppTabTextColor = GetRibbonFileAppTabTextColor(PaletteState.Normal);
    }
    #endregion

    #region RibbonFileAppTabBottomColor
    /// <summary>
    /// Gets access to ribbon app button dark color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon app button dark color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color RibbonFileAppTabBottomColor
    {
        get => _ribbonFileAppTabBottomColor;

        set
        {
            if (_ribbonFileAppTabBottomColor != value)
            {
                _ribbonFileAppTabBottomColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetRibbonFileAppTabBottomColor() => RibbonFileAppTabBottomColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;
    private bool ShouldSerializeRibbonFileAppTabBottomColor() => RibbonFileAppTabBottomColor != GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR;

    /// <inheritdoc />
    public Color GetRibbonFileAppTabBottomColor(PaletteState state) => ShouldSerializeRibbonFileAppTabBottomColor()
        ? RibbonFileAppTabBottomColor
        : _inherit.GetRibbonFileAppTabBottomColor(state);
    #endregion

    #region RibbonFileAppTabTopColor
    /// <summary>
    /// Gets access to ribbon app button light color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon app button light color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color RibbonFileAppTabTopColor
    {
        get => _ribbonFileAppTabTopColor;

        set
        {
            if (_ribbonFileAppTabTopColor != value)
            {
                _ribbonFileAppTabTopColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetRibbonFileAppTabTopColor() => RibbonFileAppTabTopColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;
    private bool ShouldSerializeRibbonFileAppTabTopColor() => RibbonFileAppTabTopColor != GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR;

    /// <inheritdoc />
    public Color GetRibbonFileAppTabTopColor(PaletteState state) => ShouldSerializeRibbonFileAppTabTopColor()
        ? RibbonFileAppTabTopColor
        : _inherit.GetRibbonFileAppTabTopColor(state);

    #endregion

    #region RibbonFileAppTabTextColor

    /// <summary>
    /// Gets access to ribbon app button text color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon app button text color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color RibbonFileAppTabTextColor
    {
        get => _ribbonFileAppTabTextColor;

        set
        {
            if (_ribbonFileAppTabTextColor != value)
            {
                _ribbonFileAppTabTextColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetRibbonFileAppTabTextColor() => RibbonFileAppTabTextColor = GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;
    private bool ShouldSerializeRibbonFileAppTabTextColor() => RibbonFileAppTabTextColor != GlobalStaticValues.DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR;

    /// <inheritdoc />
    public Color GetRibbonFileAppTabTextColor(PaletteState state) => ShouldSerializeRibbonFileAppTabTextColor()
        ? RibbonFileAppTabTextColor
        : _inherit.GetRibbonFileAppTabTextColor(state);

    #endregion

}