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
/// Implement storage for tab specific palette border, background and content.
/// </summary>
public class PaletteTabTripleRedirect : Storage,
    IPaletteTriple
{
    #region Instance Fields

    private readonly PaletteBackInheritRedirect _backInherit;
    private readonly PaletteBorderInheritRedirect _borderInherit;
    private readonly PaletteContentInheritRedirect _contentInherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteTabTripleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="contentStyle">Initial content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTabTripleRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        NeedPaintHandler? needPaint)
    {
        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(redirect is not null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Store the inherit instances
        _backInherit = new PaletteBackInheritRedirect(redirect!, backStyle);
        _borderInherit = new PaletteBorderInheritRedirect(redirect!, borderStyle);
        _contentInherit = new PaletteContentInheritRedirect(redirect!, contentStyle);

        // Create storage that maps onto the inherit instances
        Back = new PaletteBack(_backInherit, needPaint);
        Border = new PaletteTabBorder(_borderInherit, needPaint);
        Content = new PaletteContent(_contentInherit, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Back.IsDefault &&
                                      Border.IsDefault &&
                                      Content.IsDefault;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect)
    {
        _backInherit.SetRedirector(redirect);
        _borderInherit.SetRedirector(redirect);
        _contentInherit.SetRedirector(redirect);
    }
    #endregion

    #region SetStyles
    /// <summary>
    /// Update the palette styles using a tab style.
    /// </summary>
    /// <param name="tabStyle">New tab style.</param>
    public void SetStyles(TabStyle tabStyle)
    {
        switch (tabStyle)
        {
            case TabStyle.HighProfile:
                SetStyles(PaletteBackStyle.TabHighProfile,
                    PaletteBorderStyle.TabHighProfile,
                    PaletteContentStyle.TabHighProfile);
                break;
            case TabStyle.StandardProfile:
                SetStyles(PaletteBackStyle.TabStandardProfile,
                    PaletteBorderStyle.TabStandardProfile,
                    PaletteContentStyle.TabStandardProfile);
                break;
            case TabStyle.LowProfile:
                SetStyles(PaletteBackStyle.TabLowProfile,
                    PaletteBorderStyle.TabLowProfile,
                    PaletteContentStyle.TabLowProfile);
                break;
            case TabStyle.OneNote:
                SetStyles(PaletteBackStyle.TabOneNote,
                    PaletteBorderStyle.TabOneNote,
                    PaletteContentStyle.TabOneNote);
                break;
            case TabStyle.Dock:
                SetStyles(PaletteBackStyle.TabDock,
                    PaletteBorderStyle.TabDock,
                    PaletteContentStyle.TabDock);
                break;
            case TabStyle.DockAutoHidden:
                SetStyles(PaletteBackStyle.TabDockAutoHidden,
                    PaletteBorderStyle.TabDockAutoHidden,
                    PaletteContentStyle.TabDockAutoHidden);
                break;
            case TabStyle.Custom1:
                SetStyles(PaletteBackStyle.TabCustom1,
                    PaletteBorderStyle.TabCustom1,
                    PaletteContentStyle.TabCustom1);
                break;
            case TabStyle.Custom2:
                SetStyles(PaletteBackStyle.TabCustom2,
                    PaletteBorderStyle.TabCustom2,
                    PaletteContentStyle.TabCustom2);
                break;
            case TabStyle.Custom3:
                SetStyles(PaletteBackStyle.TabCustom3,
                    PaletteBorderStyle.TabCustom3,
                    PaletteContentStyle.TabCustom3);
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(tabStyle.ToString());
                break;
        }
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        Back.PopulateFromBase(state);
        Border.PopulateFromBase(state);
        Content.PopulateFromBase(state);
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
    public PaletteBack Back { get; }

    private bool ShouldSerializeBack() => !Back.IsDefault;

    /// <summary>
    /// Gets the background palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteBack PaletteBack => Back;

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
    public PaletteTabBorder Border { get; }

    private bool ShouldSerializeBorder() => !Border.IsDefault;

    /// <summary>
    /// Gets the border palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteBorder? PaletteBorder => Border;

    /// <summary>
    /// Gets and sets the border palette style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBorderStyle BorderStyle
    {
        get => _borderInherit.Style;
        set => _borderInherit.Style = value;
    }
    #endregion

    #region Content
    /// <summary>
    /// Gets access to the content palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining content appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent Content { get; }

    private bool ShouldSerializeContent() => !Content.IsDefault;

    /// <summary>
    /// Gets the content palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteContent? PaletteContent => Content;

    /// <summary>
    /// Gets and sets the content palette style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteContentStyle ContentStyle
    {
        get => _contentInherit.Style;
        set => _contentInherit.Style = value;
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

    #region Implementation
    private void SetStyles(PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle)
    {
        BackStyle = backStyle;
        BorderStyle = borderStyle;
        ContentStyle = contentStyle;
    }
    #endregion
}