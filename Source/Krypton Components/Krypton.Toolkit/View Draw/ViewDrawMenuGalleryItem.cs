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
/// View element that represents a single context-menu gallery item.
/// </summary>
internal class ViewDrawMenuGalleryItem : ViewDrawButton, IContentValues
{
    #region Instance Fields

    private readonly KryptonContextMenuGallery _gallery;
    private readonly ViewLayoutMenuGallery _layout;
    private readonly MenuGalleryItemController _controller;
    private readonly NeedPaintHandler _needPaint;
    private KryptonContextMenuGalleryItem? _item;
    private int _itemIndex;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ViewDrawMenuGalleryItem"/> class.
    /// </summary>
    public ViewDrawMenuGalleryItem(ViewContextMenuManager viewManager,
        KryptonContextMenuGallery gallery,
        ViewLayoutMenuGallery layout,
        IPaletteTriple palette,
        NeedPaintHandler needPaint)
        : base(palette, palette, palette, palette, null, null, VisualOrientation.Top, false)
    {
        _gallery = gallery;
        _layout = layout;
        _needPaint = needPaint;
        _itemIndex = -1;
        ButtonValues = this;

        _controller = new MenuGalleryItemController(viewManager, this, layout, needPaint);
        _controller.Click += OnItemClick;
        SourceController = _controller;
        KeyController = _controller;
        MouseController = new ToolTipController(gallery.ToolTipManager!, this, _controller);
    }

    /// <inheritdoc />
    public override string ToString() => $"ViewDrawMenuGalleryItem:{Id}";

    #endregion

    #region Public

    /// <summary>
    /// Gets whether this item is the current tracking target.
    /// </summary>
    public bool IsTracking => _gallery.TrackingIndex == _itemIndex;

    /// <summary>
    /// Marks this item as tracked for live preview.
    /// </summary>
    public void Track()
    {
        if (_gallery.TrackingIndex != _itemIndex)
        {
            _gallery.TrackingIndex = _itemIndex;
        }
    }

    /// <summary>
    /// Clears tracking when this item is the current target.
    /// </summary>
    public void Untrack()
    {
        if (_gallery.TrackingIndex == _itemIndex)
        {
            _gallery.TrackingIndex = -1;
        }
    }

    /// <summary>
    /// Assigns the model item drawn by this view.
    /// </summary>
    /// <param name="item">Gallery item.</param>
    /// <param name="itemIndex">Index in the resolved item list.</param>
    /// <param name="enabled">Enabled state.</param>
    public void Assign(KryptonContextMenuGalleryItem item, int itemIndex, bool enabled)
    {
        _item = item;
        _itemIndex = itemIndex;
        Enabled = enabled;
    }

    #endregion

    #region Layout

    /// <inheritdoc />
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        Size size = base.GetPreferredSize(context!);

        if (_gallery.ShowItemText)
        {
            size.Width = Math.Max(size.Width, 80);
            size.Height = Math.Max(size.Height, 56);
        }
        else
        {
            Image? image = GetImage(PaletteState.Normal);
            if (image != null)
            {
                size.Width = Math.Max(size.Width, image.Width + 8);
                size.Height = Math.Max(size.Height, image.Height + 8);
            }
        }

        return size;
    }

    #endregion

    #region Paint

    /// <inheritdoc />
    public override void Render([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        PaletteState tempState = ElementState;
        if (_gallery.TrackingIndex == _itemIndex)
        {
            ElementState = tempState switch
            {
                PaletteState.Normal => PaletteState.Tracking,
                PaletteState.CheckedNormal => PaletteState.CheckedTracking,
                _ => ElementState
            };
        }

        base.Render(context!);
        ElementState = tempState;
    }

    #endregion

    #region IContentValues

    /// <inheritdoc />
    public virtual Image? GetImage(PaletteState state)
    {
        if (_item?.Image != null)
        {
            return _item.Image;
        }

        if (_gallery.ImageList != null && _item != null && _item.ImageIndex >= 0 && _item.ImageIndex < _gallery.ImageList.Images.Count)
        {
            return _gallery.ImageList.Images[_item.ImageIndex];
        }

        return null;
    }

    /// <inheritdoc />
    public Color GetImageTransparentColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public string GetShortText() => _gallery.ShowItemText ? _item?.Text ?? string.Empty : string.Empty;

    /// <inheritdoc />
    public string GetLongText() => string.Empty;

    /// <inheritdoc />
    public virtual Image? GetOverlayImage(PaletteState state) => null;

    /// <inheritdoc />
    public Color GetOverlayImageTransparentColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    /// <inheritdoc />
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    /// <inheritdoc />
    public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

    /// <inheritdoc />
    public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);

    #endregion

    #region Private

    private void OnItemClick(object? sender, MouseEventArgs e)
    {
        _gallery.SelectedIndex = _itemIndex;
        _gallery.OnClick(e);

        if (_gallery.AutoClose && _layout.CanCloseMenu)
        {
            var cea = new CancelEventArgs();
            _layout.Closing(cea);
            if (!cea.Cancel)
            {
                _layout.Close(new CloseReasonEventArgs(ToolStripDropDownCloseReason.ItemClicked));
            }
        }

        _needPaint(this, new NeedLayoutEventArgs(true));
    }

    #endregion
}
