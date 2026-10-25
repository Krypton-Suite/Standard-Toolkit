#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Storage for general ribbon values.
/// </summary>
public class PaletteRibbonGeneralNavRedirect : Storage,
    IPaletteRibbonGeneral
{
    #region Instance Fields
    private Font? _textFont;
    private PaletteTextHint _textHint;
    private readonly PaletteRibbonGeneralInheritRedirect _inherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonGeneralNavRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonGeneralNavRedirect([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Store the inherit instances
        _inherit = new PaletteRibbonGeneralInheritRedirect(redirect!);

        // Set default values
        _textFont = null;
        _textHint = PaletteTextHint.Inherit;
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _inherit.SetRedirector(redirect);
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (TextFont == null) && (TextHint == PaletteTextHint.Inherit);

    #endregion

    #region ContextTextAlign
    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) => _inherit.GetRibbonContextTextAlign(state);

    #endregion

    #region ContextTextFont
    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font GetRibbonContextTextFont(PaletteState state) => _inherit.GetRibbonContextTextFont(state);

    #endregion

    #region ContextTextColor
    /// <summary>
    /// Gets the color for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Color GetRibbonContextTextColor(PaletteState state) => _inherit.GetRibbonContextTextColor(state);

    #endregion

    #region DisabledDark
    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDisabledDark(PaletteState state) => _inherit.GetRibbonDisabledDark(state);

    #endregion

    #region DisabledLight
    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDisabledLight(PaletteState state) => _inherit.GetRibbonDisabledLight(state);

    #endregion

    #region DropArrowLight
    /// <summary>
    /// Gets the color for the drop arrow light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDropArrowLight(PaletteState state) => _inherit.GetRibbonDropArrowLight(state);

    #endregion

    #region DropArrowDark
    /// <summary>
    /// Gets the color for the drop arrow dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonDropArrowDark(PaletteState state) => _inherit.GetRibbonDropArrowDark(state);

    #endregion

    #region GroupDialogDark
    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupDialogDark(PaletteState state) => _inherit.GetRibbonGroupDialogDark(state);

    #endregion

    #region GroupDialogLight
    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupDialogLight(PaletteState state) => _inherit.GetRibbonGroupDialogLight(state);

    #endregion

    #region GroupSeparatorDark
    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupSeparatorDark(PaletteState state) => _inherit.GetRibbonGroupSeparatorDark(state);

    #endregion

    #region GroupSeparatorLight
    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonGroupSeparatorLight(PaletteState state) => _inherit.GetRibbonGroupSeparatorLight(state);

    #endregion

    #region MinimizeBarDarkColor
    /// <summary>
    /// Gets the color for the ribbon minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonMinimizeBarDark(PaletteState state) => _inherit.GetRibbonMinimizeBarDark(state);

    #endregion

    #region MinimizeBarLightColor
    /// <summary>
    /// Gets the color for the ribbon minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonMinimizeBarLight(PaletteState state) => _inherit.GetRibbonMinimizeBarLight(state);

    #endregion

    #region RibbonTabRowBackgroundGradientRaftingDark

    /// <inheritdoc />
    public Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        _inherit.GetRibbonTabRowBackgroundGradientRaftingDark(state);

    #endregion

    #region RibbonTabRowBackgroundGradientRaftingLight

    /// <inheritdoc />
    public Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        _inherit.GetRibbonTabRowBackgroundGradientRaftingLight(state);

    #endregion

    #region RibbonTabRowBackgroundSolidColor

    /// <inheritdoc />
    public Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) =>
        _inherit.GetRibbonTabRowBackgroundSolidColor(state);

    #endregion

    #region RibbonTabRowGradientRaftingAngle

    /// <inheritdoc />
    public float GetRibbonTabRowGradientRaftingAngle(PaletteState state) =>
        _inherit.GetRibbonTabRowGradientRaftingAngle(state);

    #endregion

    #region RibbonTabRowGradientColor1

    /// <inheritdoc />
    public Color GetRibbonTabRowGradientColor1(PaletteState state) => _inherit.GetRibbonTabRowGradientColor1(state);

    #endregion

    #region GetRibbonShape
    /// <summary>
    /// Gets the ribbon shape.
    /// </summary>
    /// <returns>Ribbon shape value.</returns>
    public PaletteRibbonShape GetRibbonShape() => _inherit.GetRibbonShape();

    #endregion

    #region TabSeparatorColor
    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTabSeparatorColor(PaletteState state) => _inherit.GetRibbonTabSeparatorColor(state);

    #endregion

    #region TabSeparatorContextColor
    /// <summary>
    /// Gets the color for the tab context separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTabSeparatorContextColor(PaletteState state) => _inherit.GetRibbonTabSeparatorContextColor(state);

    #endregion

    #region TextFont
    /// <summary>
    /// Gets and sets the font for the ribbon text.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Font for the ribbon text.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Font? TextFont
    {
        get => _textFont;

        set
        {
            if (_textFont != value)
            {
                _textFont = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Reset the TextFont to the default value.
    /// </summary>
    public void ResetTextFont() => TextFont = null;

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
    [Category(@"Visuals")]
    [Description(@"Rendering hint for the text font.")]
    //[DefaultValue(typeof(PaletteTextHint), "Inherit")]
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

    /// <summary>
    /// Reset the TextHint to the default value.
    /// </summary>
    public void ResetTextHint() => TextHint = PaletteTextHint.Inherit;

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetRibbonTextHint(PaletteState state) =>
        TextHint != PaletteTextHint.Inherit ? TextHint : _inherit.GetRibbonTextHint(state);

    #endregion

    #region QATButtonDarkColor
    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonQATButtonDark(PaletteState state) => _inherit.GetRibbonQATButtonDark(state);

    #endregion

    #region QATButtonLightColor
    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonQATButtonLight(PaletteState state) => _inherit.GetRibbonQATButtonLight(state);

    #endregion
}