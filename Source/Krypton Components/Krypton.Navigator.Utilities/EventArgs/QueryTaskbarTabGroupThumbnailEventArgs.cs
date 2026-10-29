#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Event data for customising a tab-group Windows taskbar thumbnail or live-preview bitmap.
/// </summary>
public class QueryTaskbarTabGroupThumbnailEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="QueryTaskbarTabGroupThumbnailEventArgs"/> class.
    /// </summary>
    /// <param name="group">Tab group that needs a thumbnail image.</param>
    /// <param name="members">Eligible member pages used for the default collage.</param>
    /// <param name="size">Requested image size in pixels.</param>
    /// <param name="livePreview">True when the request is for Aero Peek live preview; otherwise a flyout thumbnail.</param>
    public QueryTaskbarTabGroupThumbnailEventArgs(
        NavigatorTabGroup group,
        IReadOnlyList<KryptonPage> members,
        Size size,
        bool livePreview)
    {
        Group = group;
        Members = members;
        Size = size;
        LivePreview = livePreview;
    }

    /// <summary>
    /// Gets the tab group that needs a thumbnail image.
    /// </summary>
    public NavigatorTabGroup Group { get; }

    /// <summary>
    /// Gets the eligible member pages used for the default collage.
    /// </summary>
    public IReadOnlyList<KryptonPage> Members { get; }

    /// <summary>
    /// Gets the requested image size in pixels.
    /// </summary>
    public Size Size { get; }

    /// <summary>
    /// Gets a value indicating whether the request is for Aero Peek live preview.
    /// </summary>
    public bool LivePreview { get; }

    /// <summary>
    /// Gets or sets an optional custom bitmap. When null, the component builds a collage from member snapshots.
    /// Ownership of the bitmap remains with the event handler; the component clones it before use.
    /// </summary>
    public Bitmap? Thumbnail { get; set; }
}
