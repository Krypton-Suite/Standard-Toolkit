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
/// Inherit properties from primary source in preference to the backup source.
/// </summary>
public class PaletteContentInheritOverride : PaletteContentInherit
{
    #region Instance Fields

    private IPaletteContent _primary;
    private IPaletteContent _backup;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContentInheritOverride class.
    /// </summary>
    /// <param name="primary">First choice inheritance.</param>
    /// <param name="backup">Backup inheritance.</param>
    public PaletteContentInheritOverride(IPaletteContent primary,
        IPaletteContent backup)
        : this(primary, backup, PaletteState.Normal, true)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteContentInheritOverride class.
    /// </summary>
    /// <param name="primary">First choice inheritance.</param>
    /// <param name="backup">Backup inheritance.</param>
    /// <param name="overrideState">State used by the override.</param>
    /// <param name="apply">Should the override we used.</param>
    public PaletteContentInheritOverride([DisallowNull] IPaletteContent primary,
        [DisallowNull] IPaletteContent backup,
        PaletteState overrideState,
        bool apply)
    {
        Debug.Assert(primary != null);
        Debug.Assert(backup != null);

        // Store incoming values
        _primary = primary ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(primary)));
        _backup = backup ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(backup)));
        Apply = apply;
        OverrideState = overrideState;

        // By default, we do override the state
        Override = true;
    }
    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the primary and backup palettes.
    /// </summary>
    /// <param name="primary">New primary palette.</param>
    /// <param name="backup">New backup palette.</param>
    public void SetPalettes(IPaletteContent primary,
        IPaletteContent backup)
    {
        // Store incoming alternatives
        _primary = primary;
        _backup = backup;
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

    #region IPaletteContent
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDraw(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primary.GetContentDraw(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backup.GetContentDraw(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentDraw(state);
        }
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primary.GetContentDrawFocus(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backup.GetContentDrawFocus(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentDrawFocus(state);
        }
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentImageH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentImageH(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentImageH(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentImageV(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentImageV(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentImageV(state);
        }
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteState state)
    {
        if (Apply)
        {
            PaletteImageEffect ret = _primary.GetContentImageEffect(Override ? OverrideState : state);

            if (ret == PaletteImageEffect.Inherit)
            {
                ret = _backup.GetContentImageEffect(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentImageEffect(state);
        }
    }

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorMap(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetContentImageColorMap(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetContentImageColorMap(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentImageColorMap(state);
        }
    }

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorTo(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetContentImageColorTo(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetContentImageColorTo(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentImageColorTo(state);
        }
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteState state)
    {
        if (Apply)
        {
            Font? ret = _primary.GetContentShortTextFont(Override ? OverrideState : state) ?? _backup.GetContentShortTextFont(state);

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextFont(state);
        }
    }

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextNewFont(PaletteState state)
    {
        if (Apply)
        {
            Font? ret = _primary.GetContentShortTextNewFont(Override ? OverrideState : state) ?? _backup.GetContentShortTextNewFont(state);

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextNewFont(state);
        }
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHint ret = _primary.GetContentShortTextHint(Override ? OverrideState : state);

            if (ret == PaletteTextHint.Inherit)
            {
                ret = _backup.GetContentShortTextHint(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextHint(state);
        }
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHotkeyPrefix ret = _primary.GetContentShortTextPrefix(Override ? OverrideState : state);

            if (ret == PaletteTextHotkeyPrefix.Inherit)
            {
                ret = _backup.GetContentShortTextPrefix(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextPrefix(state);
        }
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primary.GetContentShortTextMultiLine(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backup.GetContentShortTextMultiLine(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextMultiLine(state);
        }
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextTrim ret = _primary.GetContentShortTextTrim(Override ? OverrideState : state);

            if (ret == PaletteTextTrim.Inherit)
            {
                ret = _backup.GetContentShortTextTrim(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextTrim(state);
        }
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentShortTextH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentShortTextH(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextH(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentShortTextV(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentShortTextV(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextV(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentShortTextMultiLineH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentShortTextMultiLineH(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextMultiLineH(state);
        }
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetContentShortTextColor1(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetContentShortTextColor1(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextColor1(state);
        }
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetContentShortTextColor2(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetContentShortTextColor2(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextColor2(state);
        }
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteColorStyle ret = _primary.GetContentShortTextColorStyle(Override ? OverrideState : state);

            if (ret == PaletteColorStyle.Inherit)
            {
                ret = _backup.GetContentShortTextColorStyle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextColorStyle(state);
        }
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primary.GetContentShortTextColorAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backup.GetContentShortTextColorAlign(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextColorAlign(state);
        }
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteState state)
    {
        if (Apply)
        {
            var ret = _primary.GetContentShortTextColorAngle(Override ? OverrideState : state);

            if (ret == -1f)
            {
                ret = _backup.GetContentShortTextColorAngle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextColorAngle(state);
        }
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteState state)
    {
        if (Apply)
        {
            Image ret = _primary.GetContentShortTextImage(Override ? OverrideState : state) ?? _backup.GetContentShortTextImage(state)!;

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextImage(state);
        }
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteImageStyle ret = _primary.GetContentShortTextImageStyle(Override ? OverrideState : state);

            if (ret == PaletteImageStyle.Inherit)
            {
                ret = _backup.GetContentShortTextImageStyle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextImageStyle(state);
        }
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primary.GetContentShortTextImageAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backup.GetContentShortTextImageAlign(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentShortTextImageAlign(state);
        }
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextFont(PaletteState state)
    {
        if (Apply)
        {
            Font? ret = _primary.GetContentLongTextFont(Override ? OverrideState : state) ?? _backup.GetContentLongTextFont(state);

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextFont(state);
        }
    }

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextNewFont(PaletteState state)
    {
        if (Apply)
        {
            Font? ret = _primary.GetContentLongTextNewFont(Override ? OverrideState : state) ?? _backup.GetContentLongTextNewFont(state);

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextNewFont(state);
        }
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHint ret = _primary.GetContentLongTextHint(Override ? OverrideState : state);

            if (ret == PaletteTextHint.Inherit)
            {
                ret = _backup.GetContentLongTextHint(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextHint(state);
        }
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextHotkeyPrefix ret = _primary.GetContentLongTextPrefix(Override ? OverrideState : state);

            if (ret == PaletteTextHotkeyPrefix.Inherit)
            {
                ret = _backup.GetContentLongTextPrefix(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextPrefix(state);
        }
    }
        
    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteState state)
    {
        if (Apply)
        {
            InheritBool ret = _primary.GetContentLongTextMultiLine(Override ? OverrideState : state);

            if (ret == InheritBool.Inherit)
            {
                ret = _backup.GetContentLongTextMultiLine(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextMultiLine(state);
        }
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteState state)
    {
        if (Apply)
        {
            PaletteTextTrim ret = _primary.GetContentLongTextTrim(Override ? OverrideState : state);

            if (ret == PaletteTextTrim.Inherit)
            {
                ret = _backup.GetContentLongTextTrim(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextTrim(state);
        }
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentLongTextH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentLongTextH(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextH(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentLongTextV(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentLongTextV(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextV(state);
        }
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
    {
        if (Apply)
        {
            PaletteRelativeAlign ret = _primary.GetContentLongTextMultiLineH(Override ? OverrideState : state);

            if (ret == PaletteRelativeAlign.Inherit)
            {
                ret = _backup.GetContentLongTextMultiLineH(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextMultiLineH(state);
        }
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetContentLongTextColor1(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetContentLongTextColor1(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextColor1(state);
        }
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteState state)
    {
        if (Apply)
        {
            Color ret = _primary.GetContentLongTextColor2(Override ? OverrideState : state);

            if (ret == GlobalStaticValues.EMPTY_COLOR)
            {
                ret = _backup.GetContentLongTextColor2(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextColor2(state);
        }
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteColorStyle ret = _primary.GetContentLongTextColorStyle(Override ? OverrideState : state);

            if (ret == PaletteColorStyle.Inherit)
            {
                ret = _backup.GetContentLongTextColorStyle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextColorStyle(state);
        }
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primary.GetContentLongTextColorAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backup.GetContentLongTextColorAlign(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextColorAlign(state);
        }
    }

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentLongTextColorAngle(PaletteState state)
    {
        if (Apply)
        {
            var ret = _primary.GetContentLongTextColorAngle(Override ? OverrideState : state);

            if (ret == -1f)
            {
                ret = _backup.GetContentLongTextColorAngle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextColorAngle(state);
        }
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteState state)
    {
        if (Apply)
        {
            Image ret = _primary.GetContentLongTextImage(Override ? OverrideState : state) ?? _backup.GetContentLongTextImage(state)!;

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextImage(state);
        }
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
    {
        if (Apply)
        {
            PaletteImageStyle ret = _primary.GetContentLongTextImageStyle(Override ? OverrideState : state);

            if (ret == PaletteImageStyle.Inherit)
            {
                ret = _backup.GetContentLongTextImageStyle(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextImageStyle(state);
        }
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
    {
        if (Apply)
        {
            PaletteRectangleAlign ret = _primary.GetContentLongTextImageAlign(Override ? OverrideState : state);

            if (ret == PaletteRectangleAlign.Inherit)
            {
                ret = _backup.GetContentLongTextImageAlign(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentLongTextImageAlign(state);
        }
    }

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state)
    {
        if (Apply)
        {
            Padding ret = _primary.GetBorderContentPadding(owningForm, Override ? OverrideState : state);

            if (ret.All == -1)
            {
                ret = _backup.GetBorderContentPadding(owningForm, state);
            }

            return ret;
        }
        else
        {
            return _backup.GetBorderContentPadding(owningForm, state);
        }
    }

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public override int GetContentAdjacentGap(PaletteState state)
    {
        if (Apply)
        {
            var ret = _primary.GetContentAdjacentGap(Override ? OverrideState : state);

            if (ret == -1)
            {
                ret = _backup.GetContentAdjacentGap(state);
            }

            return ret;
        }
        else
        {
            return _backup.GetContentAdjacentGap(state);
        }
    }

    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public override PaletteContentStyle GetContentStyle() => Apply ? _primary.GetContentStyle() : _backup.GetContentStyle();

    #endregion
}