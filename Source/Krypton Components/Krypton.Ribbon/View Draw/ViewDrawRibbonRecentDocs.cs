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
/// Draws the recent documents title string.
/// </summary>
internal class ViewDrawRibbonRecentDocs : ViewLeaf,
    IContentValues
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly RibbonRecentDocsTitleToContent _contentProvider;
    private IDisposable? _memento;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonRecentDocs class.
    /// </summary>
    /// <param name="ribbon">Source ribbon control.</param>
    public ViewDrawRibbonRecentDocs([DisallowNull] KryptonRibbon ribbon)
    {
        Debug.Assert(ribbon != null);
        _ribbon = ribbon!;

        // Use a class to convert from ribbon recent docs to content interface
        _contentProvider = new RibbonRecentDocsTitleToContent(ribbon!.StateCommon.RibbonGeneral,
            ribbon.StateCommon.RibbonAppMenuDocsTitle);
    }        

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonRecentDocs:{Id}";

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

                _contentProvider.Dispose();
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        return context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _contentProvider, this,
            VisualOrientation.Top,
            PaletteState.Normal);
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
    public string GetShortText() => !string.IsNullOrEmpty(KryptonManager.Strings.RibbonStrings.RecentDocuments)
        ? KryptonManager.Strings.RibbonStrings.RecentDocuments
        : string.Empty;

    /// <summary>
    /// Gets the long text used as the secondary ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}