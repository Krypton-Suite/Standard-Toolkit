#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for ribbon background and text values.
/// </summary>
public class PaletteRibbonDoubleRedirect : Storage,
    IPaletteRibbonBack,
    IPaletteRibbonText
{
    #region Instance Fields
    private Color _backColor1;
    private Color _backColor2;
    private Color _backColor3;
    private Color _backColor4;
    private Color _backColor5;
    private Color _textColor;
    private readonly PaletteRibbonBackInheritRedirect _inheritBack;
    private readonly PaletteRibbonTextInheritRedirect _inheritText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonDoubleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">inheritance ribbon back style.</param>
    /// <param name="textStyle">inheritance ribbon text style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonDoubleRedirect(PaletteRedirect redirect,
        PaletteRibbonBackStyle backStyle,
        PaletteRibbonTextStyle textStyle,
        NeedPaintHandler? needPaint) 
    {
        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(redirect is not null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Store the inherit instances
        _inheritBack = new PaletteRibbonBackInheritRedirect(redirect!, backStyle);
        _inheritText = new PaletteRibbonTextInheritRedirect(redirect!, textStyle);

        // Define default values
        _backColor1 = GlobalStaticValues.EMPTY_COLOR;
        _backColor2 = GlobalStaticValues.EMPTY_COLOR;
        _backColor3 = GlobalStaticValues.EMPTY_COLOR;
        _backColor4 = GlobalStaticValues.EMPTY_COLOR;
        _backColor5 = GlobalStaticValues.EMPTY_COLOR;
        _textColor = GlobalStaticValues.EMPTY_COLOR;
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect)
    {
        _inheritBack.SetRedirector(redirect);
        _inheritText.SetRedirector(redirect);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (BackColor1 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor2 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor3 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor4 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor5 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (TextColor == GlobalStaticValues.EMPTY_COLOR);

    #endregion

    #region BackColorStyle
    /// <summary>
    /// Gets the background drawing style for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state) => _inheritBack.GetRibbonBackColorStyle(state);
    #endregion

    #region BackColor1
    /// <summary>
    /// Gets and sets the first background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"First background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public Color BackColor1
    {
        get => _backColor1;

        set
        {
            if (_backColor1 != value)
            {
                _backColor1 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor1() => BackColor1 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor1() => BackColor1 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor1(PaletteState state) =>
        BackColor1 != GlobalStaticValues.EMPTY_COLOR ? BackColor1 : _inheritBack.GetRibbonBackColor1(state);

    #endregion

    #region BackColor2
    /// <summary>
    /// Gets and sets the second background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Second background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public Color BackColor2
    {
        get => _backColor2;

        set
        {
            if (_backColor2 != value)
            {
                _backColor2 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor2() => BackColor2 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor2() => BackColor2 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor2(PaletteState state) =>
        BackColor2 != GlobalStaticValues.EMPTY_COLOR ? BackColor2 : _inheritBack.GetRibbonBackColor2(state);

    #endregion

    #region BackColor3
    /// <summary>
    /// Gets and sets the third background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Third background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public Color BackColor3
    {
        get => _backColor3;

        set
        {
            if (_backColor3 != value)
            {
                _backColor3 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor3() => BackColor3 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor3() => BackColor3 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor3(PaletteState state) =>
        BackColor3 != GlobalStaticValues.EMPTY_COLOR ? BackColor3 : _inheritBack.GetRibbonBackColor3(state);

    #endregion

    #region BackColor4
    /// <summary>
    /// Gets and sets the fourth background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Fourth background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public Color BackColor4
    {
        get => _backColor4;

        set
        {
            if (_backColor4 != value)
            {
                _backColor4 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor4() => BackColor4 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor4() => BackColor4 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor4(PaletteState state) =>
        BackColor4 != GlobalStaticValues.EMPTY_COLOR ? BackColor4 : _inheritBack.GetRibbonBackColor4(state);

    #endregion

    #region BackColor5
    /// <summary>
    /// Gets and sets the fifth background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Fifth background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public Color BackColor5
    {
        get => _backColor5;

        set
        {
            if (_backColor5 != value)
            {
                _backColor5 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor5() => BackColor5 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor5() => BackColor5 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor5(PaletteState state) =>
        BackColor5 != GlobalStaticValues.EMPTY_COLOR ? BackColor5 : _inheritBack.GetRibbonBackColor5(state);

    #endregion

    #region TextColor
    /// <summary>
    /// Gets and sets the Tab color for the item text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Tab color for the tab text.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public Color TextColor
    {
        get => _textColor;

        set
        {
            if (_textColor != value)
            {
                _textColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeTextColor() => TextColor != GlobalStaticValues.EMPTY_COLOR;
    private void ResetTextColor() => TextColor = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonTextColor(PaletteState state) =>
        TextColor != GlobalStaticValues.EMPTY_COLOR ? TextColor : _inheritText.GetRibbonTextColor(state);

    #endregion
}