#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Event data for customising a page's Windows taskbar thumbnail or live-preview bitmap.
/// </summary>
public class QueryTaskbarThumbnailEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the QueryTaskbarThumbnailEventArgs class.
    /// </summary>
    /// <param name="page">Page that needs a thumbnail image.</param>
    /// <param name="size">Requested image size in pixels.</param>
    /// <param name="livePreview">True when the request is for Aero Peek live preview; otherwise a flyout thumbnail.</param>
    public QueryTaskbarThumbnailEventArgs(KryptonPage page, Size size, bool livePreview)
    {
        Page = page;
        Size = size;
        LivePreview = livePreview;
    }

    /// <summary>
    /// Gets the page that needs a thumbnail image.
    /// </summary>
    public KryptonPage Page { get; }

    /// <summary>
    /// Gets the requested image size in pixels.
    /// </summary>
    public Size Size { get; }

    /// <summary>
    /// Gets a value indicating whether the request is for Aero Peek live preview.
    /// </summary>
    public bool LivePreview { get; }

    /// <summary>
    /// Gets or sets an optional custom bitmap. When null, the component captures the page via PrintWindow/DrawToBitmap.
    /// Ownership of the bitmap remains with the event handler; the component clones it before use.
    /// </summary>
    public Bitmap? Thumbnail { get; set; }
}
