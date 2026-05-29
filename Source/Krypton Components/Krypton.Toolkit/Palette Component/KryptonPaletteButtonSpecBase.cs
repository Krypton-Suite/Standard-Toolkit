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
/// Base class for defining button specifications.
/// </summary>
public class KryptonPaletteButtonSpecBase : Storage,
    IPaletteButtonSpec
{
    #region Instance Fields

    private PaletteButtonStyle _style;
    private PaletteButtonOrientation _orientation;
    private PaletteRelativeEdgeAlign _edge;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a button spec change occurs.
    /// </summary>
    public event EventHandler? ButtonSpecChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteButtonSpecBase class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    internal KryptonPaletteButtonSpecBase([DisallowNull] PaletteRedirect redirector)
    {
        Debug.Assert(redirector != null);

        // Remember reference to redirector
        Redirector = redirector!;

        // Default the generic overridable values
        _style = PaletteButtonStyle.Inherit;
        _orientation = PaletteButtonOrientation.Inherit;
        _edge = PaletteRelativeEdgeAlign.Inherit;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Style == PaletteButtonStyle.Inherit) &&
                                      (Orientation == PaletteButtonOrientation.Inherit) &&
                                      (Edge == PaletteRelativeEdgeAlign.Inherit);

    #endregion

    #region Redirector
    /// <summary>
    /// Gets access to the redirector.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteRedirect Redirector { get; private set; }

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector([DisallowNull] PaletteRedirect redirect) => Redirector = redirect;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="style">The style of the button spec instance.</param>
    public virtual void PopulateFromBase(PaletteButtonSpecStyle style)
    {
        Style = Redirector.GetButtonSpecStyle(style);
        Orientation = Redirector.GetButtonSpecOrientation(style);
        Edge = Redirector.GetButtonSpecEdge(style);
    }
    #endregion

    #region Style
    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button style.")]
    [DefaultValue(PaletteButtonStyle.Inherit)]
    public PaletteButtonStyle Style
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Resets the Style property to its default value.
    /// </summary>
    public void ResetStyle() => Style = PaletteButtonStyle.Inherit;
    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the button orientation.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Defines the button orientation.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(PaletteButtonOrientation.Inherit)]
    public PaletteButtonOrientation Orientation
    {
        get => _orientation;

        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Resets the Orientation property to its default value.
    /// </summary>
    public void ResetOrientation() => Orientation = PaletteButtonOrientation.Inherit;

    #endregion

    #region Edge
    /// <summary>
    /// Gets and sets the header edge to display the button against.
    /// </summary>
    [KryptonPersist(false)]
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The header edge to display the button against.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(PaletteRelativeEdgeAlign.Inherit)]
    public PaletteRelativeEdgeAlign Edge
    {
        get => _edge;

        set
        {
            if (_edge != value)
            {
                _edge = value;
                OnButtonSpecChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Resets the Edge property to its default value.
    /// </summary>
    public void ResetEdge() => Edge = PaletteRelativeEdgeAlign.Inherit;

    #endregion

    #region IPaletteButtonSpec
    /// <summary>
    /// Gets the icon to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Icon value.</returns>
    public virtual Icon? GetButtonSpecIcon(PaletteButtonSpecStyle style) => Redirector.GetButtonSpecIcon(style);

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public virtual Image? GetButtonSpecImage(PaletteButtonSpecStyle style,
        PaletteState state) => Redirector.GetButtonSpecImage(style, state);

    /// <summary>
    /// Gets the image transparent color.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style) => Redirector.GetButtonSpecImageTransparentColor(style);

    /// <summary>
    /// Gets the short text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public virtual string GetButtonSpecShortText(PaletteButtonSpecStyle style) => Redirector.GetButtonSpecShortText(style);

    /// <summary>
    /// Gets the long text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public virtual string GetButtonSpecLongText(PaletteButtonSpecStyle style) => Redirector.GetButtonSpecLongText(style);

    /// <summary>
    /// Gets the tooltip title text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public virtual string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style) => Redirector.GetButtonSpecToolTipTitle(style);

    /// <summary>
    /// Gets the color to remap from the image to the container foreground.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetButtonSpecColorMap(PaletteButtonSpecStyle style) => Redirector.GetButtonSpecColorMap(style);

    /// <summary>
    /// Gets the button style used for drawing the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonStyle value.</returns>
    public virtual PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style) =>
        Style != PaletteButtonStyle.Inherit ? Style : Redirector.GetButtonSpecStyle(style);

    /// <summary>
    /// Get the location for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>HeaderLocation value.</returns>
    public virtual HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style) => Redirector.GetButtonSpecLocation(style);

    /// <summary>
    /// Gets the edge to position the button against.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteRelativeEdgeAlign value.</returns>
    public virtual PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style) =>
        Edge != PaletteRelativeEdgeAlign.Inherit ? Edge : Redirector.GetButtonSpecEdge(style);

    /// <summary>
    /// Gets the button orientation.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonOrientation value.</returns>
    public virtual PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style) =>
        Orientation != PaletteButtonOrientation.Inherit ? Orientation : Redirector.GetButtonSpecOrientation(style);

    #endregion

    #region OnButtonSpecChanged
    /// <summary>
    /// Raises the ButtonSpecChanged event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected virtual void OnButtonSpecChanged(object sender, EventArgs e) => ButtonSpecChanged?.Invoke(this, e);

    #endregion
}