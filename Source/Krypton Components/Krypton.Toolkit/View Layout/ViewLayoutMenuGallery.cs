#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Lays out a context-menu gallery: optional heading ranges, item grid, and More submenu.
/// </summary>
internal class ViewLayoutMenuGallery : ViewLayoutStack
{
    #region Instance Fields

    private readonly KryptonContextMenuGallery _gallery;
    private readonly IContextMenuProvider _provider;
    private readonly PaletteTripleToPalette _triple;
    private readonly NeedPaintHandler _needPaint;
    private readonly ViewContextMenuManager _viewManager;
    private readonly List<KryptonContextMenuHeading> _ownedHeadings = [];

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewLayoutMenuGallery"/> class.
    /// </summary>
    /// <param name="gallery">Owning gallery item.</param>
    /// <param name="provider">Context menu provider.</param>
    public ViewLayoutMenuGallery(KryptonContextMenuGallery gallery, IContextMenuProvider provider)
        : base(false)
    {
        _gallery = gallery;
        _provider = provider;
        _gallery.TrackingIndex = -1;
        ItemEnabled = provider.ProviderEnabled;
        _viewManager = provider.ProviderViewManager;
        _needPaint = provider.ProviderNeedPaintDelegate;

        PaletteBase palette = provider.ProviderPalette ?? KryptonManager.GetPaletteForMode(provider.ProviderPaletteMode);
        _triple = new PaletteTripleToPalette(palette,
            PaletteBackStyle.ButtonLowProfile,
            PaletteBorderStyle.ButtonLowProfile,
            PaletteContentStyle.ButtonLowProfile);
        _triple.SetStyles(gallery.ButtonStyle);

        BuildChildren();
    }

    /// <inheritdoc />
    public override string ToString() => $"ViewLayoutMenuGallery:{Id}";

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (KryptonContextMenuHeading heading in _ownedHeadings)
            {
                heading.Dispose();
            }

            _ownedHeadings.Clear();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the enabled state of the gallery.
    /// </summary>
    public bool ItemEnabled { get; }

    /// <summary>
    /// Gets a value indicating if the menu is capable of being closed.
    /// </summary>
    public bool CanCloseMenu => _provider.ProviderCanCloseMenu;

    /// <summary>
    /// Raises the Closing event on the provider.
    /// </summary>
    /// <param name="cea">Cancel arguments.</param>
    public void Closing(CancelEventArgs cea) => _provider.OnClosing(cea);

    /// <summary>
    /// Raises the Close event on the provider.
    /// </summary>
    /// <param name="e">Close reason.</param>
    public void Close(CloseReasonEventArgs e) => _provider.OnClose(e);

    #endregion

    #region Implementation

    private void BuildChildren()
    {
        IReadOnlyList<KryptonContextMenuGalleryItem> items = _gallery.GetResolvedItems();
        if (_gallery.Ranges.Count == 0)
        {
            Add(CreateGrid(items, 0, items.Count - 1));
        }
        else
        {
            foreach (KryptonContextMenuGalleryRange range in _gallery.Ranges)
            {
                var start = range.ImageIndexStart < 0 ? 0 : range.ImageIndexStart;
                var end = range.ImageIndexEnd < 0 ? items.Count - 1 : range.ImageIndexEnd;
                start = Math.Max(0, Math.Min(start, Math.Max(0, items.Count - 1)));
                end = Math.Max(start, Math.Min(end, items.Count - 1));

                if (!string.IsNullOrEmpty(range.Heading))
                {
                    var heading = new KryptonContextMenuHeading(range.Heading);
                    _ownedHeadings.Add(heading);
                    Add(new ViewDrawMenuHeading(heading, _provider.ProviderStateCommon.Heading));
                }

                if (items.Count > 0)
                {
                    Add(CreateGrid(items, start, end));
                }
            }
        }

        if (_gallery.MoreItems.Count > 0)
        {
            Add(_gallery.MoreItem.GenerateView(_provider, _gallery, _provider.ProviderViewColumns, true, true));
        }
    }

    private ViewLayoutMenuGalleryGrid CreateGrid(IReadOnlyList<KryptonContextMenuGalleryItem> items, int start, int end) =>
        new ViewLayoutMenuGalleryGrid(_gallery, this, _viewManager, _triple, _needPaint, items, start, end, ItemEnabled);

    #endregion
}

/// <summary>
/// Grid of gallery items within a single heading range.
/// </summary>
internal class ViewLayoutMenuGalleryGrid : ViewComposite
{
    #region Instance Fields

    private readonly KryptonContextMenuGallery _gallery;
    private readonly ViewLayoutMenuGallery _layout;
    private readonly ViewContextMenuManager _viewManager;
    private readonly PaletteTripleToPalette _triple;
    private readonly NeedPaintHandler _needPaint;
    private readonly IReadOnlyList<KryptonContextMenuGalleryItem> _items;
    private readonly int _start;
    private readonly int _end;
    private readonly int _count;
    private readonly int _lineItems;
    private readonly Padding _padding;
    private readonly bool _itemEnabled;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewLayoutMenuGalleryGrid"/> class.
    /// </summary>
    public ViewLayoutMenuGalleryGrid(KryptonContextMenuGallery gallery,
        ViewLayoutMenuGallery layout,
        ViewContextMenuManager viewManager,
        PaletteTripleToPalette triple,
        NeedPaintHandler needPaint,
        IReadOnlyList<KryptonContextMenuGalleryItem> items,
        int start,
        int end,
        bool itemEnabled)
    {
        _gallery = gallery;
        _layout = layout;
        _viewManager = viewManager;
        _triple = triple;
        _needPaint = needPaint;
        _items = items;
        _start = start;
        _end = end;
        _count = Math.Max(0, end - start + 1);
        _lineItems = Math.Max(1, gallery.LineItems);
        _padding = gallery.Padding;
        _itemEnabled = itemEnabled;
    }

    /// <inheritdoc />
    public override string ToString() => $"ViewLayoutMenuGalleryGrid:{Id}";

    #endregion

    #region Layout

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        SyncChildren();

        var preferredSize = Size.Empty;
        if (Count > 0)
        {
            for (var i = 0; i < Count; i++)
            {
                Size itemSize = this[i]!.GetPreferredSize(context!);
                preferredSize.Width = Math.Max(preferredSize.Width, itemSize.Width);
                preferredSize.Height = Math.Max(preferredSize.Height, itemSize.Height);
            }

            preferredSize.Width *= _lineItems;
            preferredSize.Height *= (Count + (_lineItems - 1)) / _lineItems;
        }

        preferredSize.Width += _padding.Horizontal;
        preferredSize.Height += _padding.Vertical;
        return preferredSize;
    }

    /// <inheritdoc />
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        if (context == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        ClientRectangle = context.DisplayRectangle;
        SyncChildren();

        if (Count > 0)
        {
            Rectangle displayRect = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _padding);
            var itemSize = Size.Empty;
            for (var i = 0; i < Count; i++)
            {
                Size measured = this[i]!.GetPreferredSize(context);
                itemSize.Width = Math.Max(itemSize.Width, measured.Width);
                itemSize.Height = Math.Max(itemSize.Height, measured.Height);
            }

            // Stretch tiles across the menu width so captions are not clipped when the Mini Toolbar is wider.
            if (_lineItems > 0 && displayRect.Width > 0)
            {
                var stretchWidth = displayRect.Width / _lineItems;
                if (stretchWidth > itemSize.Width)
                {
                    itemSize.Width = stretchWidth;
                }
            }
            Point nextPoint = displayRect.Location;
            for (var i = 0; i < Count; i++)
            {
                context.DisplayRectangle = new Rectangle(nextPoint, itemSize);
                this[i]?.Layout(context);
                nextPoint.X += itemSize.Width;
                if (((i + 1) % _lineItems) == 0)
                {
                    nextPoint.X = displayRect.X;
                    nextPoint.Y += itemSize.Height;
                }
            }
        }

        context.DisplayRectangle = ClientRectangle;
    }

    #endregion

    #region Private

    private void SyncChildren()
    {
        if (Count < _count)
        {
            var create = _count - Count;
            for (var i = 0; i < create; i++)
            {
                Add(new ViewDrawMenuGalleryItem(_viewManager, _gallery, _layout, _triple, _needPaint));
            }
        }
        else if (Count > _count)
        {
            var remove = Count - _count;
            for (var i = 0; i < remove; i++)
            {
                RemoveAt(0);
            }
        }

        for (var i = 0; i < _count; i++)
        {
            var itemIndex = i + _start;
            if (this[i] is ViewDrawMenuGalleryItem drawItem)
            {
                drawItem.Assign(_items[itemIndex], itemIndex, _itemEnabled && _items[itemIndex].Enabled);
                drawItem.Checked = _gallery.SelectedIndex == itemIndex;
            }
        }
    }

    #endregion
}
