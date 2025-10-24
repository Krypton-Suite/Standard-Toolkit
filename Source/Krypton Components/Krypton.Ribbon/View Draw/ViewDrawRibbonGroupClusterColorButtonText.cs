#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws the text string for a group color cluster button.
/// </summary>
internal class ViewDrawRibbonGroupClusterColorButtonText : ViewLeaf,
    IContentValues
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroupClusterColorButton _ribbonColorButton;
    private readonly RibbonGroupNormalDisabledTextToContent _contentProvider;
    private IDisposable? _memento;
    private int _heightExtra;
    private Size _preferredSize;
    private Rectangle _displayRect;
    private int _dirtyPaletteSize;
    private int _dirtyPaletteLayout;
    private PaletteState _cacheState;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupClusterColorButtonText class.
    /// </summary>
    /// <param name="ribbon">Source ribbon control.</param>
    /// <param name="ribbonColorButton">Group cluster color button to display title for.</param>
    public ViewDrawRibbonGroupClusterColorButtonText([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupClusterColorButton? ribbonColorButton)
                                             
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(ribbonColorButton != null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _ribbonColorButton = ribbonColorButton ?? throw new ArgumentNullException(nameof(ribbonColorButton));

        // Use a class to convert from ribbon group to content interface
        _contentProvider = new RibbonGroupNormalDisabledTextToContent(ribbon.StateCommon.RibbonGeneral,
            ribbon.StateNormal.RibbonGroupButtonText,
            ribbon.StateDisabled.RibbonGroupButtonText);
    }        

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupClusterColorButtonText:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region MakeDirty
    /// <summary>
    /// Make dirty so cached values are not used.
    /// </summary>
    public void MakeDirty()
    {
        _dirtyPaletteSize = 0;
        _dirtyPaletteLayout = 0;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        // Debug.Assert() causes the null assignment warning.
        // Suppressed by the null forgiving operator
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context!.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // A change in state always causes a size and layout calculation
        if (_cacheState != State)
        {
            MakeDirty();
            _cacheState = State;
        }

        // If the palette has changed since we last calculated
        if (_ribbon.DirtyPaletteCounter != _dirtyPaletteSize)
        {
            // Ask the renderer for the contents preferred size
            _preferredSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _contentProvider, 
                this, VisualOrientation.Top,
                State);

            // Subtract the extra space used to ensure it draws
            _heightExtra = (_ribbon.CalculatedValues.DrawFontHeight - _ribbon.CalculatedValues.RawFontHeight) * 2;
            _preferredSize.Height -= _heightExtra;

            // If the text is actually empty, then force it to be zero width
            if (string.IsNullOrEmpty(GetShortText()))
            {
                _preferredSize.Width = 0;
            }

            // Cached value is valid till dirty palette noticed
            _dirtyPaletteSize = _ribbon.DirtyPaletteCounter;
        }

        return _preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // A change in state always causes a size and layout calculation
        if (_cacheState != State)
        {
            MakeDirty();
            _cacheState = State;
        }

        // Do we need to actually perform the relayout?
        if ((_displayRect != ClientRectangle) ||
            (_ribbon.DirtyPaletteCounter != _dirtyPaletteLayout))
        {
            // Remember to dispose of old memento
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }

            Rectangle drawRect = ClientRectangle;

            // Adjust the client rect so the text has enough room to be drawn
            drawRect.Height += _heightExtra;
            drawRect.Y -= _heightExtra / 2;

            // Use the renderer to layout the text
            _memento = context.Renderer.RenderStandardContent.LayoutContent(context, drawRect,
                _contentProvider, this,
                VisualOrientation.Top,
                PaletteState.Normal);

            // Cache values that are needed to decide if layout is needed
            _displayRect = ClientRectangle;
            _dirtyPaletteLayout = _ribbon.DirtyPaletteCounter;
        }
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        Rectangle drawRect = ClientRectangle;

        // Adjust the client rect so the text has enough room to be drawn
        drawRect.Height += _heightExtra;
        drawRect.Y -= _heightExtra / 2;

        // Use renderer to draw the text content
        if (_memento != null)
        {
            context.Renderer.RenderStandardContent.DrawContent(context, drawRect,
                _contentProvider, _memento,
                VisualOrientation.Top,
                State, true);
        }
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the image used for the ribbon tab.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Image.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be interpreted as transparent.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Transparent Color.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetShortText() => _ribbonColorButton.KryptonCommand != null
        ? _ribbonColorButton.KryptonCommand.TextLine1
        : _ribbonColorButton.TextLine;

    /// <summary>
    /// Gets the long text used as the secondary ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}