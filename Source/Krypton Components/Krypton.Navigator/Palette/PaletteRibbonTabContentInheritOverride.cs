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

namespace Krypton.Navigator;

/// <summary>
/// Inherit properties from primary source in preference to the backup source.
/// </summary>
public class PaletteRibbonTabContentInheritOverride : PaletteRibbonDoubleInherit,
    IPaletteContent
{
    #region Instance Fields

    private readonly IPaletteRibbonBack _primaryBack;
    private readonly IPaletteRibbonBack _backupBack;
    private readonly IPaletteRibbonText _primaryText;
    private readonly IPaletteRibbonText _backupText;
    private readonly IPaletteContent _primaryContent;
    private readonly IPaletteContent _backupContent;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonTabContentInheritOverride class.
    /// </summary>
    /// <param name="primaryBack">First choice inheritance background.</param>
    /// <param name="primaryText">First choice inheritance text.</param>
    /// <param name="primaryContent">First choice inheritance content.</param>
    /// <param name="backupBack">Backup inheritance background.</param>
    /// <param name="backupText">Backup inheritance text.</param>
    /// <param name="backupContent">Backup inheritance content.</param>
    /// <param name="state">Palette state to override.</param>
    public PaletteRibbonTabContentInheritOverride([DisallowNull] IPaletteRibbonBack primaryBack,
        [DisallowNull] IPaletteRibbonText primaryText,
        [DisallowNull] IPaletteContent primaryContent,
        [DisallowNull] IPaletteRibbonBack backupBack,
        [DisallowNull] IPaletteRibbonText backupText,
        [DisallowNull] IPaletteContent backupContent,
        PaletteState state)
    {
        Debug.Assert(primaryBack != null);
        Debug.Assert(primaryText != null);
        Debug.Assert(primaryContent != null);
        Debug.Assert(backupBack != null);
        Debug.Assert(backupText != null);
        Debug.Assert(backupContent != null);

        // Remember values
        _primaryBack = primaryBack ?? throw new ArgumentNullException(nameof(primaryBack));
        _primaryText = primaryText ?? throw new ArgumentNullException(nameof(primaryText));
        _primaryContent = primaryContent ?? throw new ArgumentNullException(nameof(primaryContent));
        _backupBack = backupBack ?? throw new ArgumentNullException(nameof(backupBack));
        _backupText = backupText ?? throw new ArgumentNullException(nameof(backupText));
        _backupContent = backupContent ?? throw new ArgumentNullException(nameof(backupContent));

        // Default state
        Apply = false;
        Override = true;
        OverrideState = state;
    }
    #endregion

    #region Apply
    /// <summary>
    /// Gets and sets a value indicating if override should be applied.
    /// </summary>
    public bool Apply { get; set; }

    #endregion

    #region Override
    /// <summary>
    /// Gets and sets a value indicating if override state should be applied.
    /// </summary>
    public bool Override { get; set; }

    #endregion

    #region OverrideState
    /// <summary>
    /// Gets and sets the override palette state to use.
    /// </summary>
    public PaletteState OverrideState { get; set; }

    #endregion

    #region IPaletteRibbonBack
    /// <summary>
    /// Gets the method used to draw the background of a ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteRibbonBackStyle value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteRibbonColorStyle ret = _primaryBack.GetRibbonBackColorStyle(Override ? OverrideState : state);

            if (ret == PaletteRibbonColorStyle.Inherit)
            {
                ret = _backupBack.GetRibbonBackColorStyle(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColorStyle(state);
        }
    }

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor1(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupBack.GetRibbonBackColor1(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor1(state);
        }
    }

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor2(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupBack.GetRibbonBackColor2(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor2(state);
        }
    }

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor3(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor3(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupBack.GetRibbonBackColor3(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor3(state);
        }
    }

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor4(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor4(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupBack.GetRibbonBackColor4(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor4(state);
        }
    }

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor5(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryBack.GetRibbonBackColor5(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupBack.GetRibbonBackColor5(state);
            }

            return ret;
        }
        else
        {
            return _backupBack.GetRibbonBackColor5(state);
        }
    }
    #endregion

    #region IPaletteRibbonText
    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTextColor(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryText.GetRibbonTextColor(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupText.GetRibbonTextColor(state);
            }

            return ret;
        }
        else
        {
            return _backupText.GetRibbonTextColor(state);
        }
    }
    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public virtual InheritBool GetContentDraw(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primaryContent.GetContentDraw(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backupContent.GetContentDraw(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentDraw(state);
        }
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public virtual InheritBool GetContentDrawFocus(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primaryContent.GetContentDrawFocus(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backupContent.GetContentDrawFocus(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentDrawFocus(state);
        }
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentImageH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentImageH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentImageH(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentImageH(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentImageV(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentImageV(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentImageV(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentImageV(state);
        }
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public virtual PaletteImageEffect GetContentImageEffect(PaletteState state)
    {
        if (Apply)
        {
            PaletteImageEffect ret = _primaryContent.GetContentImageEffect(Override ? OverrideState : state);

            if (ret == PaletteImageEffect.Inherit)
            {
                ret = _backupContent.GetContentImageEffect(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentImageEffect(state);
        }
    }

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentImageColorMap(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryContent.GetContentImageColorMap(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupContent.GetContentImageColorMap(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentImageColorMap(state);
        }
    }

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentImageColorTo(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryContent.GetContentImageColorTo(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupContent.GetContentImageColorTo(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentImageColorTo(state);
        }
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public virtual Font GetContentShortTextFont(PaletteState state)
    {
        if (Apply)
        {
            Font ret = _primaryContent.GetContentShortTextFont(Override ? OverrideState : state) ?? _backupContent.GetContentShortTextFont(state) 
                ?? throw new NullReferenceException("The result of GetContentShortTextFont() cannot be null.");

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextFont(state) ?? throw new NullReferenceException("The result of GetContentShortTextFont() cannot be null.");
        }
    }

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public virtual Font GetContentShortTextNewFont(PaletteState state)
    {
        if (Apply)
        {
            Font ret = _primaryContent.GetContentShortTextNewFont(Override ? OverrideState : state) ?? _backupContent.GetContentShortTextNewFont(state)
                ?? throw new NullReferenceException("The result of GetContentShortTextNewFont() cannot be null.");

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextNewFont(state) ?? throw new NullReferenceException("The result of GetContentShortTextNewFont() cannot be null.");
        }
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public virtual PaletteTextHint GetContentShortTextHint(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHint ret = _primaryContent.GetContentShortTextHint(Override ? OverrideState : state);

            if (ret == PaletteTextHint.Inherit)
            {
                ret = _backupContent.GetContentShortTextHint(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextHint(state);
        }
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public virtual PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHotkeyPrefix ret = _primaryContent.GetContentShortTextPrefix(Override ? OverrideState : state);

            if (ret == PaletteTextHotkeyPrefix.Inherit)
            {
                ret = _backupContent.GetContentShortTextPrefix(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextPrefix(state);
        }
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public virtual InheritBool GetContentShortTextMultiLine(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primaryContent.GetContentShortTextMultiLine(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backupContent.GetContentShortTextMultiLine(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextMultiLine(state);
        }
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public virtual PaletteTextTrim GetContentShortTextTrim(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextTrim ret = _primaryContent.GetContentShortTextTrim(Override ? OverrideState : state);

            if (ret == PaletteTextTrim.Inherit)
            {
                ret = _backupContent.GetContentShortTextTrim(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextTrim(state);
        }
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentShortTextH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentShortTextH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentShortTextH(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextH(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentShortTextV(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentShortTextV(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentShortTextV(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextV(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentShortTextMultiLineH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentShortTextMultiLineH(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextMultiLineH(state);
        }
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentShortTextColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryContent.GetContentShortTextColor1(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupContent.GetContentShortTextColor1(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextColor1(state);
        }
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentShortTextColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryContent.GetContentShortTextColor2(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupContent.GetContentShortTextColor2(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextColor2(state);
        }
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public virtual PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteColorStyle ret = _primaryContent.GetContentShortTextColorStyle(Override ? OverrideState : state);

            if (ret == PaletteColorStyle.Inherit)
            {
                ret = _backupContent.GetContentShortTextColorStyle(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextColorStyle(state);
        }
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public virtual PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primaryContent.GetContentShortTextColorAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backupContent.GetContentShortTextColorAlign(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextColorAlign(state);
        }
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public virtual float GetContentShortTextColorAngle(PaletteState state)
    {
        if (Apply)
        {
            var ret = _primaryContent.GetContentShortTextColorAngle(Override ? OverrideState : state);

            if (ret == -1f)
            {
                ret = _backupContent.GetContentShortTextColorAngle(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextColorAngle(state);
        }
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public virtual Image? GetContentShortTextImage(PaletteState state)
    {
        if (Apply)
        {
            Image? ret = _primaryContent.GetContentShortTextImage(Override ? OverrideState : state) ?? _backupContent.GetContentShortTextImage(state);

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextImage(state);
        }
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public virtual PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteImageStyle ret = _primaryContent.GetContentShortTextImageStyle(Override ? OverrideState : state);

            if (ret == PaletteImageStyle.Inherit)
            {
                ret = _backupContent.GetContentShortTextImageStyle(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextImageStyle(state);
        }
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public virtual PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primaryContent.GetContentShortTextImageAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backupContent.GetContentShortTextImageAlign(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentShortTextImageAlign(state);
        }
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public virtual Font GetContentLongTextFont(PaletteState state)
    {
        if (Apply)
        {
            Font ret = _primaryContent.GetContentLongTextFont(Override ? OverrideState : state) ?? _backupContent.GetContentLongTextFont(state)
                ?? throw new NullReferenceException("The result of GetContentLongTextFont() cannot be null.");

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextFont(state)
                   ?? throw new NullReferenceException("The result of GetContentLongTextFont() cannot be null.");
        }
    }

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public virtual Font GetContentLongTextNewFont(PaletteState state)
    {
        if (Apply)
        {
            Font ret = _primaryContent.GetContentLongTextNewFont(Override ? OverrideState : state) ?? _backupContent.GetContentLongTextNewFont(state)
                ?? throw new NullReferenceException("The result of GetContentLongTextNewFont() cannot be null.");

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextNewFont(state) ?? throw new NullReferenceException("The result of GetContentLongTextNewFont() cannot be null.");

        }
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public virtual PaletteTextHint GetContentLongTextHint(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHint ret = _primaryContent.GetContentLongTextHint(Override ? OverrideState : state);

            if (ret == PaletteTextHint.Inherit)
            {
                ret = _backupContent.GetContentLongTextHint(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextHint(state);
        }
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public virtual PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHotkeyPrefix ret = _primaryContent.GetContentLongTextPrefix(Override ? OverrideState : state);

            if (ret == PaletteTextHotkeyPrefix.Inherit)
            {
                ret = _backupContent.GetContentLongTextPrefix(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextPrefix(state);
        }
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public virtual InheritBool GetContentLongTextMultiLine(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primaryContent.GetContentLongTextMultiLine(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backupContent.GetContentLongTextMultiLine(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextMultiLine(state);
        }
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public virtual PaletteTextTrim GetContentLongTextTrim(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextTrim ret = _primaryContent.GetContentLongTextTrim(Override ? OverrideState : state);

            if (ret == PaletteTextTrim.Inherit)
            {
                ret = _backupContent.GetContentLongTextTrim(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextTrim(state);
        }
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentLongTextH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentLongTextH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentLongTextH(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextH(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentLongTextV(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentLongTextV(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentLongTextV(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextV(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primaryContent.GetContentLongTextMultiLineH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backupContent.GetContentLongTextMultiLineH(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextMultiLineH(state);
        }
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentLongTextColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryContent.GetContentLongTextColor1(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupContent.GetContentLongTextColor1(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextColor1(state);
        }
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentLongTextColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primaryContent.GetContentLongTextColor2(Override ? OverrideState : state);

            if (ret == Color.Empty)
            {
                ret = _backupContent.GetContentLongTextColor2(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextColor2(state);
        }
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public virtual PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteColorStyle ret = _primaryContent.GetContentLongTextColorStyle(Override ? OverrideState : state);

            if (ret == PaletteColorStyle.Inherit)
            {
                ret = _backupContent.GetContentLongTextColorStyle(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextColorStyle(state);
        }
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public virtual PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primaryContent.GetContentLongTextColorAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backupContent.GetContentLongTextColorAlign(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextColorAlign(state);
        }
    }

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public virtual float GetContentLongTextColorAngle(PaletteState state)
    {
        if (Apply)
        {
            var ret = _primaryContent.GetContentLongTextColorAngle(Override ? OverrideState : state);

            if (ret == -1f)
            {
                ret = _backupContent.GetContentLongTextColorAngle(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextColorAngle(state);
        }
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public virtual Image? GetContentLongTextImage(PaletteState state)
    {
        if (Apply)
        {
            Image? ret = _primaryContent.GetContentLongTextImage(Override ? OverrideState : state) ?? _backupContent.GetContentLongTextImage(state);

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextImage(state);
        }
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public virtual PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteImageStyle ret = _primaryContent.GetContentLongTextImageStyle(Override ? OverrideState : state);

            if (ret == PaletteImageStyle.Inherit)
            {
                ret = _backupContent.GetContentLongTextImageStyle(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextImageStyle(state);
        }
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public virtual PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primaryContent.GetContentLongTextImageAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backupContent.GetContentLongTextImageAlign(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentLongTextImageAlign(state);
        }
    }

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public virtual Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state)
    {
        if (Apply)
        {
            Padding ret = _primaryContent.GetBorderContentPadding(owningForm, Override ? OverrideState : state);

            if (ret.All == -1)
            {
                ret = _backupContent.GetBorderContentPadding(owningForm, state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetBorderContentPadding(owningForm, state);
        }
    }

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public virtual int GetContentAdjacentGap(PaletteState state)
    {
        if (Apply)
        {
            var ret = _primaryContent.GetContentAdjacentGap(Override ? OverrideState : state);

            if (ret == -1)
            {
                ret = _backupContent.GetContentAdjacentGap(state);
            }

            return ret;
        }
        else
        {
            return _backupContent.GetContentAdjacentGap(state);
        }
    }

    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public virtual PaletteContentStyle GetContentStyle() =>
        Apply ? _primaryContent.GetContentStyle() : _backupContent.GetContentStyle();

    #endregion
}