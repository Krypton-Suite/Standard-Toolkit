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
/// Draws the title string for a group.
/// </summary>
internal class ViewDrawRibbonGroupTitle : ViewLeaf,
    IContentValues
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroup _ribbonGroup;
    private readonly RibbonGroupTextToContent _contentProvider;
    private IDisposable? _memento;
    private Rectangle _displayRect;
    private int _dirtyPaletteLayout;
    private PaletteState _cacheState;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupTitle class.
    /// </summary>
    /// <param name="ribbon">Source ribbon control.</param>
    /// <param name="ribbonGroup">Ribbon group to display title for.</param>
    public ViewDrawRibbonGroupTitle([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] KryptonRibbonGroup ribbonGroup)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(ribbonGroup != null);

        _ribbon = ribbon!;
        _ribbonGroup = ribbonGroup!;

        // Use a class to convert from ribbon group to content interface
        _contentProvider = new RibbonGroupTextToContent(ribbon!.StateCommon.RibbonGeneral,
            ribbon.StateNormal.RibbonGroupNormalTitle);
    }        

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupTitle:{Id}";

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
    public void MakeDirty() => _dirtyPaletteLayout = 0;
    #endregion

    #region PaletteRibbonGroup
    /// <summary>
    /// Gets and sets the ribbon group palette to use.
    /// </summary>
    public IPaletteRibbonText PaletteRibbonGroup
    {
        get => _contentProvider.PaletteRibbonGroup;
        set => _contentProvider.PaletteRibbonGroup = value;
    }
    #endregion

    #region Height
    /// <summary>
    /// Gets and sets the height of the title string.
    /// </summary>
    public int Height { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => new Size(0, Height);

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

            // Use the renderer to layout the text
            _memento = context.Renderer.RenderStandardContent.LayoutContent(context, ClientRectangle,
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
    public override void RenderBefore(RenderContext context) 
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Use renderer to draw the text content
        if (_memento != null)
        {
            context.Renderer.RenderStandardContent.DrawContent(context, ClientRectangle,
                _contentProvider, _memento,
                VisualOrientation.Top,
                PaletteState.Normal, true);
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
    public string GetShortText()
    {
        if (!string.IsNullOrEmpty(_ribbonGroup.TextLine2))
        {
            return $"{_ribbonGroup.TextLine1} {_ribbonGroup.TextLine2}";
        }
        else
        {
            return _ribbonGroup.TextLine1;
        }
    }

    /// <summary>
    /// Gets the long text used as the secondary ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}