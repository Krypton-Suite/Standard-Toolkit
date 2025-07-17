#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for general ribbon values.
/// </summary>
public class PaletteRibbonGeneral : Storage,
    IPaletteRibbonGeneral
{
    #region Instance Fields
    private PaletteRelativeAlign _contextTextAlign;
    private Color _contextTextColor;
    private Font? _contextTextFont;
    private IPaletteRibbonGeneral _inherit;
    private PaletteRibbonShape _ribbonShape;
    private Color _dialogDarkColor;
    private Color _dialogLightColor;
    private Color _disabledDarkColor;
    private Color _disabledLightColor;
    private Color _dropArrowDarkColor;
    private Color _dropArrowLightColor;
    private Color _groupSeparatorDark;
    private Color _groupSeparatorLight;
    private Color _minimizeBarDarkColor;
    private Color _minimizeBarLightColor;
    private Color _tabRowBackgroundSolidColor;
    private Color _tabBackgroundGradientRaftingDarkColor;
    private Color _tabBackgroundGradientRaftingLightColor;
    private Color _tabRowBackgroundGradientFirstColor;
    private Color _qatButtonDarkColor;
    private Color _qatButtonLightColor;
    private Color _tabSeparatorColor;
    private Color _tabSeparatorContextColor;
    private Font? _textFont;
    private float _ribbonTabRowGradientRaftingAngle;
    private PaletteTextHint _textHint;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonGeneral class.
    /// </summary>
    /// <param name="inherit">Source for inheriting general values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonGeneral([DisallowNull] IPaletteRibbonGeneral inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Remember inheritance
        _inherit = inherit!;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Set default values
        _contextTextAlign = PaletteRelativeAlign.Inherit;
        _contextTextColor = GlobalStaticValues.EMPTY_COLOR;
        _contextTextFont = null;
        _disabledDarkColor = GlobalStaticValues.EMPTY_COLOR;
        _disabledLightColor = GlobalStaticValues.EMPTY_COLOR;
        _dialogDarkColor = GlobalStaticValues.EMPTY_COLOR;
        _dialogLightColor = GlobalStaticValues.EMPTY_COLOR;
        _dropArrowLightColor = GlobalStaticValues.EMPTY_COLOR;
        _dropArrowDarkColor = GlobalStaticValues.EMPTY_COLOR;
        _groupSeparatorDark = GlobalStaticValues.EMPTY_COLOR;
        _groupSeparatorLight = GlobalStaticValues.EMPTY_COLOR;
        _minimizeBarDarkColor = GlobalStaticValues.EMPTY_COLOR;
        _minimizeBarLightColor = GlobalStaticValues.EMPTY_COLOR;
        _tabBackgroundGradientRaftingDarkColor = GlobalStaticValues.EMPTY_COLOR;
        _tabBackgroundGradientRaftingLightColor = GlobalStaticValues.EMPTY_COLOR;
        _tabRowBackgroundSolidColor = GlobalStaticValues.EMPTY_COLOR;
        _tabRowBackgroundGradientFirstColor = GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;
        _ribbonTabRowGradientRaftingAngle = GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;
        _ribbonShape = PaletteRibbonShape.Inherit;
        _tabSeparatorColor = GlobalStaticValues.EMPTY_COLOR;
        _tabSeparatorContextColor = GlobalStaticValues.EMPTY_COLOR;
        _textFont = null;
        _textHint = PaletteTextHint.Inherit;
        _qatButtonDarkColor = GlobalStaticValues.EMPTY_COLOR;
        _qatButtonLightColor = GlobalStaticValues.EMPTY_COLOR;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeContextTextAlign() &&
                                      !ShouldSerializeContextTextFont() &&
                                      !ShouldSerializeContextTextColor() &&
                                      !ShouldSerializeDisabledDark() &&
                                      !ShouldSerializeDisabledLight() &&
                                      !ShouldSerializeGroupDialogDark() &&
                                      !ShouldSerializeGroupDialogLight() &&
                                      !ShouldSerializeDropArrowDark() &&
                                      !ShouldSerializeDropArrowLight() &&
                                      !ShouldSerializeGroupSeparatorDark() &&
                                      !ShouldSerializeGroupSeparatorLight() &&
                                      !ShouldSerializeMinimizeBarDarkColor() &&
                                      !ShouldSerializeMinimizeBarLightColor() &&
                                      !ShouldSerializeTabRowBackgroundSolidColor() &&
                                      !ShouldSerializeTabRowBackgroundGradientRaftingDarkColor() &&
                                      !ShouldSerializeTabRowBackgroundGradientRaftingLightColor() &&
                                      !ShouldSerializeTabRowBackgroundGradientFirstColor() &&
                                      !ShouldSerializeRibbonTabRowGradientRaftingAngle() &&
                                      !ShouldSerializeRibbonShape() &&
                                      !ShouldSerializeTabSeparatorColor() &&
                                      !ShouldSerializeTabSeparatorContextColor() &&
                                      !ShouldSerializeTextFont() &&
                                      !ShouldSerializeTextHint() &&
                                      !ShouldSerializeQATButtonDarkColor() &&
                                      !ShouldSerializeQATButtonLightColor();
    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteRibbonGeneral inherit) => _inherit = inherit;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        ContextTextAlign = GetRibbonContextTextAlign(PaletteState.Normal);
        ContextTextFont = GetRibbonContextTextFont(PaletteState.Normal);
        ContextTextColor = GetRibbonContextTextColor(PaletteState.Normal);
        DisabledDark = GetRibbonDisabledDark(PaletteState.Normal);
        DisabledLight = GetRibbonDisabledLight(PaletteState.Normal);
        DropArrowDark = GetRibbonDropArrowDark(PaletteState.Normal);
        DropArrowLight = GetRibbonDropArrowLight(PaletteState.Normal);
        GroupDialogDark = GetRibbonGroupDialogDark(PaletteState.Normal);
        GroupDialogLight = GetRibbonGroupDialogLight(PaletteState.Normal);
        GroupSeparatorDark = GetRibbonGroupSeparatorDark(PaletteState.Normal);
        GroupSeparatorLight = GetRibbonGroupSeparatorLight(PaletteState.Normal);
        MinimizeBarDarkColor = GetRibbonMinimizeBarDark(PaletteState.Normal);
        MinimizeBarLightColor = GetRibbonMinimizeBarLight(PaletteState.Normal);
        TabRowBackgroundSolidColor = GetRibbonTabRowBackgroundSolidColor(PaletteState.Normal);
        TabRowBackgroundGradientRaftingDarkColor = GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState.Normal);
        TabRowBackgroundGradientRaftingLightColor = GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState.Normal);
        TabRowBackgroundGradientFirstColor = GetRibbonTabRowGradientColor1(PaletteState.Normal);
        RibbonTabRowGradientRaftingAngle = GetRibbonTabRowGradientRaftingAngle(PaletteState.Normal);
        RibbonShape = GetRibbonShape();
        TabSeparatorColor = GetRibbonTabSeparatorColor(PaletteState.Normal);
        TabSeparatorContextColor = GetRibbonTabSeparatorContextColor(PaletteState.Normal);
        TextFont = GetRibbonTextFont(PaletteState.Normal);
        TextHint = GetRibbonTextHint(PaletteState.Normal);
        QATButtonDarkColor = GetRibbonGroupDialogDark(PaletteState.Normal);
        QATButtonLightColor = GetRibbonGroupDialogLight(PaletteState.Normal);
    }
    #endregion

    #region ContextTextAlign
    /// <summary>
    /// Gets and sets the text alignment for the ribbon context text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Text alignment for the ribbon context text.")]
    [DefaultValue(PaletteRelativeAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteRelativeAlign ContextTextAlign
    {
        get => _contextTextAlign;

        set
        {
            if (_contextTextAlign != value)
            {
                _contextTextAlign = value;
                PerformNeedPaint(true);
            }
        }
    }
    private void ResetContextTextAlign() => ContextTextAlign = PaletteRelativeAlign.Inherit;
    private bool ShouldSerializeContextTextAlign() => ContextTextAlign != PaletteRelativeAlign.Inherit;

    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) =>
        ShouldSerializeContextTextAlign()
            ? ContextTextAlign
            : _inherit.GetRibbonContextTextAlign(state);

    #endregion

    #region ContextTextFont
    /// <summary>
    /// Gets and sets the font for the ribbon context text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Font for the ribbon context text.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Font? ContextTextFont
    {
        get => _contextTextFont;

        set
        {
            if (_contextTextFont != value)
            {
                _contextTextFont = value;
                PerformNeedPaint(true);
            }
        }
    }
    private void ResetContextTextFont() => ContextTextFont = null;
    private bool ShouldSerializeContextTextFont() => ContextTextFont != null;

    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font GetRibbonContextTextFont(PaletteState state) => ContextTextFont ?? _inherit.GetRibbonContextTextFont(state);

    #endregion

    #region ContextTextColor
    /// <summary>
    /// Gets and sets the text color used for ribbon context text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color used for ribbon context text.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color ContextTextColor
    {
        get => _contextTextColor;

        set
        {
            if (_contextTextColor != value)
            {
                _contextTextColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetContextTextColor() => ContextTextColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeContextTextColor() => ContextTextColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color of the ribbon caption text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonContextTextColor(PaletteState state) => ShouldSerializeContextTextColor()
        ? ContextTextColor
        : _inherit.GetRibbonContextTextColor(state);

    #endregion

    #region DisabledDark
    /// <summary>
    /// Gets access to dark disabled color used for ribbon glyphs.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Dark disabled color for ribbon glyphs.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color DisabledDark
    {
        get => _disabledDarkColor;

        set
        {
            if (_disabledDarkColor != value)
            {
                _disabledDarkColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetDisabledDark() => DisabledDark = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeDisabledDark() => DisabledDark != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDisabledDark(PaletteState state) =>
        ShouldSerializeDisabledDark() ? DisabledDark : _inherit.GetRibbonDisabledDark(state);

    #endregion

    #region DisabledLight
    /// <summary>
    /// Gets access to light disabled color used for ribbon glyphs.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Light disabled color for ribbon glyphs.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color DisabledLight
    {
        get => _disabledLightColor;

        set
        {
            if (_disabledLightColor != value)
            {
                _disabledLightColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetDisabledLight() => DisabledLight = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeDisabledLight() => DisabledLight != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDisabledLight(PaletteState state) =>
        ShouldSerializeDisabledLight() ? DisabledLight : _inherit.GetRibbonDisabledLight(state);

    #endregion

    #region GroupDialogDark
    /// <summary>
    /// Gets access to ribbon dialog launcher button dark color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon group dialog launcher button dark color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color GroupDialogDark
    {
        get => _dialogDarkColor;

        set
        {
            if (_dialogDarkColor != value)
            {
                _dialogDarkColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetGroupDialogDark() => GroupDialogDark = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeGroupDialogDark() => GroupDialogDark != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupDialogDark(PaletteState state) => ShouldSerializeGroupDialogDark()
        ? GroupDialogDark
        : _inherit.GetRibbonGroupDialogDark(state);

    #endregion

    #region GroupDialogLight
    /// <summary>
    /// Gets access to ribbon group dialog launcher button light color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon group dialog launcher button light color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color GroupDialogLight
    {
        get => _dialogLightColor;

        set
        {
            if (_dialogLightColor != value)
            {
                _dialogLightColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetGroupDialogLight() => GroupDialogLight = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeGroupDialogLight() => GroupDialogLight != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupDialogLight(PaletteState state) => ShouldSerializeGroupDialogLight()
        ? GroupDialogLight
        : _inherit.GetRibbonGroupDialogLight(state);

    #endregion

    #region DropArrowDark
    /// <summary>
    /// Gets access to ribbon drop arrow dark color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon drop arrow dark color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color DropArrowDark
    {
        get => _dropArrowDarkColor;

        set
        {
            if (_dropArrowDarkColor != value)
            {
                _dropArrowDarkColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetDropArrowDark() => DropArrowDark = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeDropArrowDark() => DropArrowDark != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the drop arrow dark color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDropArrowDark(PaletteState state) =>
        ShouldSerializeDropArrowDark() ? DropArrowDark : _inherit.GetRibbonDropArrowDark(state);

    #endregion

    #region DropArrowLight
    /// <summary>
    /// Gets access to ribbon drop arrow light color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon drop arrow light color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color DropArrowLight
    {
        get => _dropArrowLightColor;

        set
        {
            if (_dropArrowLightColor != value)
            {
                _dropArrowLightColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetDropArrowLight() => DropArrowLight = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeDropArrowLight() => DropArrowLight != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the drop arrow light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDropArrowLight(PaletteState state) => ShouldSerializeDropArrowLight()
        ? DropArrowLight
        : _inherit.GetRibbonDropArrowLight(state);

    #endregion

    #region GroupSeparatorDark
    /// <summary>
    /// Gets access to ribbon group separator dark color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon group separator dark color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color GroupSeparatorDark
    {
        get => _groupSeparatorDark;

        set
        {
            if (_groupSeparatorDark != value)
            {
                _groupSeparatorDark = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetGroupSeparatorDark() => GroupSeparatorDark = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeGroupSeparatorDark() => GroupSeparatorDark != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupSeparatorDark(PaletteState state) => ShouldSerializeGroupSeparatorDark()
        ? GroupSeparatorDark
        : _inherit.GetRibbonGroupSeparatorDark(state);

    #endregion

    #region GroupSeparatorLight
    /// <summary>
    /// Gets access to ribbon group separator light color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon group separator light color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color GroupSeparatorLight
    {
        get => _groupSeparatorLight;

        set
        {
            if (_groupSeparatorLight != value)
            {
                _groupSeparatorLight = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetGroupSeparatorLight() => GroupDialogLight = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeGroupSeparatorLight() => GroupDialogLight != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupSeparatorLight(PaletteState state) => ShouldSerializeGroupSeparatorLight()
        ? GroupSeparatorLight
        : _inherit.GetRibbonGroupSeparatorLight(state);

    #endregion

    #region MinimizeBarDarkColor
    /// <summary>
    /// Gets access to ribbon minimize bar dark color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon minimize bar dark color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color MinimizeBarDarkColor
    {
        get => _minimizeBarDarkColor;

        set
        {
            if (_minimizeBarDarkColor != value)
            {
                _minimizeBarDarkColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetMinimizeBarDarkColor() => MinimizeBarDarkColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeMinimizeBarDarkColor() => MinimizeBarDarkColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the ribbon minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonMinimizeBarDark(PaletteState state) => ShouldSerializeMinimizeBarDarkColor()
        ? MinimizeBarDarkColor
        : _inherit.GetRibbonMinimizeBarDark(state);

    #endregion

    #region MinimizeBarLightColor
    /// <summary>
    /// Gets access to ribbon minimize bar light color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon minimize bar light color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color MinimizeBarLightColor
    {
        get => _minimizeBarLightColor;

        set
        {
            if (_minimizeBarLightColor != value)
            {
                _minimizeBarLightColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetMinimizeBarLightColor() => MinimizeBarLightColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeMinimizeBarLightColor() => MinimizeBarLightColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the ribbon minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonMinimizeBarLight(PaletteState state) => ShouldSerializeMinimizeBarLightColor()
        ? MinimizeBarLightColor
        : _inherit.GetRibbonMinimizeBarLight(state);

    #endregion

    #region TabRowBackgroundSolidColor

    /// <summary>
    /// Gets access to ribbon tab row solid color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab row background solid color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color TabRowBackgroundSolidColor
    {
        get => _tabRowBackgroundSolidColor;

        set
        {
            if (_tabRowBackgroundSolidColor != value)
            {
                _tabRowBackgroundSolidColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetTabRowBackgroundSolidColor() => TabRowBackgroundSolidColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeTabRowBackgroundSolidColor() => TabRowBackgroundSolidColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the solid color for the ribbon tab row.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => ShouldSerializeTabRowBackgroundSolidColor()
        ? TabRowBackgroundSolidColor
        : _inherit.GetRibbonTabRowBackgroundSolidColor(state);

    #endregion

    #region TabRowBackgroundGradientRaftingDarkColor

    /// <summary>
    /// Gets access to ribbon tab row gradient dark rafting color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab row background gradient dark rafting color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color TabRowBackgroundGradientRaftingDarkColor
    {
        get => _tabBackgroundGradientRaftingDarkColor;

        set
        {
            if (_tabBackgroundGradientRaftingDarkColor != value)
            {
                _tabBackgroundGradientRaftingDarkColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetTabRowBackgroundGradientRaftingDarkColor() => TabRowBackgroundGradientRaftingDarkColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeTabRowBackgroundGradientRaftingDarkColor() => TabRowBackgroundGradientRaftingDarkColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the dark Gradient rafting color for the ribbon tab row.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => ShouldSerializeTabRowBackgroundGradientRaftingDarkColor()
        ? TabRowBackgroundGradientRaftingDarkColor
        : _inherit.GetRibbonTabRowBackgroundGradientRaftingDark(state);

    #endregion

    #region TabRowBackgroundGradientRaftingLightColor

    /// <summary>
    /// Gets access to ribbon tab row gradient light rafting color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab row background gradient light rafting color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color TabRowBackgroundGradientRaftingLightColor
    {
        get => _tabBackgroundGradientRaftingLightColor;

        set
        {
            if (_tabBackgroundGradientRaftingLightColor != value)
            {
                _tabBackgroundGradientRaftingLightColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetTabRowBackgroundGradientRaftingLightColor() => TabRowBackgroundGradientRaftingLightColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeTabRowBackgroundGradientRaftingLightColor() => TabRowBackgroundGradientRaftingLightColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the light rafting color for the ribbon tab row.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => ShouldSerializeTabRowBackgroundGradientRaftingLightColor()
        ? TabRowBackgroundGradientRaftingLightColor
        : _inherit.GetRibbonTabRowBackgroundGradientRaftingLight(state);

    #endregion

    #region TabRowBackgroundGradientFirstColor

    /// <summary>
    /// Gets access to ribbon tab row gradient first color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab row background gradient first color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color TabRowBackgroundGradientFirstColor
    {
        get => _tabRowBackgroundGradientFirstColor;

        set
        {
            if (_tabRowBackgroundGradientFirstColor != value)
            {
                _tabRowBackgroundGradientFirstColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetTabRowBackgroundGradientFirstColor() => TabRowBackgroundGradientFirstColor = GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;
    private bool ShouldSerializeTabRowBackgroundGradientFirstColor() => TabRowBackgroundGradientFirstColor != GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;

    /// <inheritdoc />
    public Color GetRibbonTabRowGradientColor1(PaletteState state) => ShouldSerializeTabRowBackgroundGradientFirstColor()
        ? TabRowBackgroundGradientFirstColor
        : _inherit.GetRibbonTabRowGradientColor1(state);

    #endregion

    #region RibbonTabRowGradientRaftingAngle

    /// <summary>
    /// Gets access to ribbon tab row gradient rafting angle.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab row background gradient rafting angle.")]
    [DefaultValue(GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT)]
    [RefreshProperties(RefreshProperties.All)]
    public float RibbonTabRowGradientRaftingAngle
    {
        get => _ribbonTabRowGradientRaftingAngle;

        set
        {
            if (_ribbonTabRowGradientRaftingAngle != value)
            {
                _ribbonTabRowGradientRaftingAngle = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetRibbonTabRowGradientRaftingAngle() => RibbonTabRowGradientRaftingAngle =
        GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;
    private bool ShouldSerializeRibbonTabRowGradientRaftingAngle() => RibbonTabRowGradientRaftingAngle !=
                                                                      GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;

    /// <summary>
    /// Gets the rafting angle for the ribbon tab row.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Rafting angle value.</returns>
    public float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => ShouldSerializeRibbonTabRowGradientRaftingAngle()
        ? RibbonTabRowGradientRaftingAngle
        : _inherit.GetRibbonTabRowGradientRaftingAngle(state);

    #endregion

    #region RibbonShape
    /// <summary>
    /// Gets access to ribbon shape.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon shape.")]
    [DefaultValue(PaletteRibbonShape.Inherit)]
    public PaletteRibbonShape RibbonShape
    {
        get => _ribbonShape;

        set
        {
            if (_ribbonShape != value)
            {
                _ribbonShape = value;
                PerformNeedPaint(true);
            }
        }
    }
    private void ResetRibbonShape() => RibbonShape = PaletteRibbonShape.Inherit;
    private bool ShouldSerializeRibbonShape() => RibbonShape != PaletteRibbonShape.Inherit;

    /// <summary>
    /// Gets the ribbon shape.
    /// </summary>
    /// <returns>Color value.</returns>
    public PaletteRibbonShape GetRibbonShape() => ShouldSerializeRibbonShape()
        ? RibbonShape : _inherit.GetRibbonShape();

    #endregion

    #region TabSeparatorColor
    /// <summary>
    /// Gets access to ribbon tab separator color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab separator color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color TabSeparatorColor
    {
        get => _tabSeparatorColor;

        set
        {
            if (_tabSeparatorColor != value)
            {
                _tabSeparatorColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetTabSeparatorColor() => TabSeparatorColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeTabSeparatorColor() => TabSeparatorColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTabSeparatorColor(PaletteState state) => ShouldSerializeTabSeparatorColor()
        ? TabSeparatorColor
        : _inherit.GetRibbonTabSeparatorColor(state);

    #endregion

    #region TabSeparatorContextColor
    /// <summary>
    /// Gets access to ribbon context tab separator color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Ribbon tab context separator color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color TabSeparatorContextColor
    {
        get => _tabSeparatorContextColor;

        set
        {
            if (_tabSeparatorContextColor != value)
            {
                _tabSeparatorContextColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetTabSeparatorContextColor() => TabSeparatorContextColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeTabSeparatorContextColor() => TabSeparatorContextColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the tab context separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTabSeparatorContextColor(PaletteState state) =>
        ShouldSerializeTabSeparatorContextColor()
            ? TabSeparatorContextColor
            : _inherit.GetRibbonTabSeparatorContextColor(state);

    #endregion

    #region TextFont
    /// <summary>
    /// Gets and sets the font for the ribbon text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Font for the ribbon text.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Font? TextFont
    {
        get => _textFont;

        set
        {
            if (!Equals(_textFont, value))
            {
                _textFont = value;
                PerformNeedPaint(true);
            }
        }
    }
    private void ResetTextFont() => TextFont = null;
    private bool ShouldSerializeTextFont() => TextFont != null;

    /// <summary>
    /// Gets the font for the ribbon text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font GetRibbonTextFont(PaletteState state) => TextFont ?? _inherit.GetRibbonTextFont(state);

    #endregion

    #region TextHint
    /// <summary>
    /// Gets and sets the rendering hint for the text font.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Rendering hint for the text font.")]
    [DefaultValue(PaletteTextHint.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteTextHint TextHint
    {
        get => _textHint;

        set
        {
            if (_textHint != value)
            {
                _textHint = value;
                PerformNeedPaint(true);
            }
        }
    }
    private void ResetTextHint() => TextHint = PaletteTextHint.Inherit;
    private bool ShouldSerializeTextHint() => TextHint != PaletteTextHint.Inherit;

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetRibbonTextHint(PaletteState state) =>
        ShouldSerializeTextHint() ? TextHint : _inherit.GetRibbonTextHint(state);

    #endregion

    #region QATButtonDarkColor
    /// <summary>
    /// Gets access to extra QAT extra button dark content color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Quick access toolbar extra button dark color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color QATButtonDarkColor
    {
        get => _qatButtonDarkColor;

        set
        {
            if (_qatButtonDarkColor != value)
            {
                _qatButtonDarkColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetQATButtonDarkColor() => QATButtonDarkColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeQATButtonDarkColor() => QATButtonDarkColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonQATButtonDark(PaletteState state) => ShouldSerializeQATButtonDarkColor()
        ? QATButtonDarkColor
        : _inherit.GetRibbonQATButtonDark(state);

    #endregion

    #region QATButtonLightColor
    /// <summary>
    /// Gets access to extra QAT extra button light content color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Quick access toolbar extra button light color.")]
    [RefreshProperties(RefreshProperties.All)]
    public Color QATButtonLightColor
    {
        get => _qatButtonLightColor;

        set
        {
            if (_qatButtonLightColor != value)
            {
                _qatButtonLightColor = value;
                PerformNeedPaint();
            }
        }
    }
    private void ResetQATButtonLightColor() => QATButtonLightColor = GlobalStaticValues.EMPTY_COLOR;
    private bool ShouldSerializeQATButtonLightColor() => QATButtonLightColor != GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonQATButtonLight(PaletteState state) => ShouldSerializeQATButtonLightColor()
        ? QATButtonLightColor
        : _inherit.GetRibbonQATButtonLight(state);

    #endregion
}