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
/// Implement storage for palette border and background.
/// </summary>
public class PaletteDoubleRedirect : Storage,
    IPaletteDouble
{
    #region Instance Fields
#pragma warning disable CS3008 // Identifier is not CLS-compliant
    // Dotnet having troubles with the underscores
    protected PaletteBack _back;
    protected PaletteBorder _border;
    protected PaletteBackInheritRedirect _backInherit;
#pragma warning restore CS3008 // Identifier is not CLS-compliant

    #endregion

    #region Identity

    /// <inheritdoc />
    protected PaletteDoubleRedirect()
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteDoubleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    public PaletteDoubleRedirect(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle)
        : this(redirect, backStyle, borderStyle, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteDoubleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteDoubleRedirect(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        NeedPaintHandler? needPaint)
    {
        // Store the inherit instances
        var backInherit = new PaletteBackInheritRedirect(redirect, backStyle);
        var borderInherit = new PaletteBorderInheritRedirect(redirect, borderStyle);

        // Create storage that maps onto the inherit instances
        var back = new PaletteBack(backInherit, needPaint);
        var border = new PaletteBorder(borderInherit, needPaint);

        Construct(redirect, back, backInherit, border, borderInherit, needPaint);
    }

    /// <summary>
    /// Initialize a new instance of the PaletteDoubleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="back">Storage for back values.</param>
    /// <param name="backInherit">inheritance for back values.</param>
    /// <param name="border">Storage for border values.</param>
    /// <param name="borderInherit">inheritance for border values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteDoubleRedirect(PaletteRedirect redirect,
        PaletteBack back,
        PaletteBackInheritRedirect backInherit,
        PaletteBorder border,
        PaletteBorderInheritRedirect borderInherit,
        NeedPaintHandler needPaint)
    {
        Construct(redirect, back, backInherit, border, borderInherit, needPaint);
    }
    #endregion

    #region GetRedirector
    /// <summary>
    /// Gets the redirector instance.
    /// </summary>
    /// <returns>Return the currently used redirector.</returns>
    public PaletteRedirect GetRedirector() => _backInherit.GetRedirector();

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect)
    {
        _backInherit.SetRedirector(redirect);
        BorderRedirect.SetRedirector(redirect);
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">State to use when pulling values.</param>
    public void PopulateFromBase(PaletteState state)
    {
        _back.PopulateFromBase(state);
        _border.PopulateFromBase(state);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Back.IsDefault && Border.IsDefault;

    #endregion

    #region SetStyles
    /// <summary>
    /// Update the palette styles to the separator style.
    /// </summary>
    /// <param name="backStyle">New back style.</param>
    /// <param name="borderStyle">New border style.</param>
    public void SetStyles(PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle)
    {
        BackStyle = backStyle;
        BorderStyle = borderStyle;
    }

    /// <summary>
    /// Update the palette styles to the separator style.
    /// </summary>
    /// <param name="separatorStyle">New separator style.</param>
    public void SetStyles(SeparatorStyle separatorStyle)
    {
        switch (separatorStyle)
        {
            case SeparatorStyle.LowProfile:
                SetStyles(PaletteBackStyle.SeparatorLowProfile, PaletteBorderStyle.SeparatorLowProfile);
                break;
            case SeparatorStyle.HighProfile:
                SetStyles(PaletteBackStyle.SeparatorHighProfile, PaletteBorderStyle.SeparatorHighProfile);
                break;
            case SeparatorStyle.HighInternalProfile:
                SetStyles(PaletteBackStyle.SeparatorHighInternalProfile, PaletteBorderStyle.SeparatorHighInternalProfile);
                break;
            case SeparatorStyle.Custom1:
                SetStyles(PaletteBackStyle.SeparatorCustom1, PaletteBorderStyle.SeparatorCustom1);
                break;
            case SeparatorStyle.Custom2:
                SetStyles(PaletteBackStyle.SeparatorCustom2, PaletteBorderStyle.SeparatorCustom2);
                break;
            case SeparatorStyle.Custom3:
                SetStyles(PaletteBackStyle.SeparatorCustom3, PaletteBorderStyle.SeparatorCustom3);
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(separatorStyle.ToString());
                break;
        }
    }

    /// <summary>
    /// Update the palette styles to the input control style.
    /// </summary>
    /// <param name="inputControlStyle">New input control style.</param>
    public void SetStyles(InputControlStyle inputControlStyle)
    {
        switch (inputControlStyle)
        {
            case InputControlStyle.Standalone:
                SetStyles(PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone);
                break;
            case InputControlStyle.Ribbon:
                SetStyles(PaletteBackStyle.InputControlRibbon, PaletteBorderStyle.InputControlRibbon);
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(inputControlStyle.ToString());
                break;
        }
    }
    #endregion

    #region Back
    /// <summary>
    /// Gets access to the background palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteBack Back => _back;

    private bool ShouldSerializeBack() => !_back.IsDefault;

    /// <summary>
    /// Gets the background palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IPaletteBack PaletteBack => Back;

    /// <summary>
    /// Gets and sets the back palette style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBackStyle BackStyle
    {
        get => _backInherit.Style;
        set => _backInherit.Style = value;
    }
    #endregion

    #region Border
    /// <summary>
    /// Gets access to the border palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteBorder Border => _border;

    private bool ShouldSerializeBorder() => !_border.IsDefault;

    /// <summary>
    /// Gets the border palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IPaletteBorder? PaletteBorder => Border;

    /// <summary>
    /// Gets and sets the border palette style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBorderStyle BorderStyle
    {
        get => BorderRedirect.Style;
        set => BorderRedirect.Style = value;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Handle a change event from palette source.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="needLayout">True if a layout is also needed.</param>
    protected void OnNeedPaint(object? sender, bool needLayout) =>
        // Pass request from child to our own handler
        PerformNeedPaint(needLayout);

    #endregion

    #region Internal
    internal PaletteBorderInheritRedirect BorderRedirect { get; private set; }

    #endregion

    #region protected
    protected void Construct(PaletteRedirect redirect,
        PaletteBack back,
        PaletteBackInheritRedirect backInherit,
        PaletteBorder border,
        PaletteBorderInheritRedirect borderInherit,
        NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;
        _backInherit = backInherit;
        BorderRedirect = borderInherit;
        _back = back;
        _border = border;
    }
    #endregion
}