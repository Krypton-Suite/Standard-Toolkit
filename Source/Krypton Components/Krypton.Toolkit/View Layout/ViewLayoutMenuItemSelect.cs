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
/// View element that creates and lays out the image list items.
/// </summary>
internal class ViewLayoutMenuItemSelect : ViewComposite
{
    #region Instance Fields
    private readonly ViewContextMenuManager _viewManager;
    private readonly KryptonContextMenuImageSelect _itemSelect;
    private readonly IContextMenuProvider _provider;
    private readonly PaletteTripleToPalette _triple;
    private readonly NeedPaintHandler _needPaint;
    private readonly ImageList _imageList;
    private int _selectedIndex;
    private readonly int _imageIndexStart;
    private readonly int _imageIndexEnd;
    private readonly int _imageIndexCount;
    private readonly int _imageCount;
    private readonly int _lineItems;
    private Padding _padding;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutMenuItemSelect class.
    /// </summary>
    /// <param name="itemSelect">Reference to owning instance.</param>
    /// <param name="provider">Provider of context menu information.</param>
    public ViewLayoutMenuItemSelect([DisallowNull] KryptonContextMenuImageSelect itemSelect,
        [DisallowNull] IContextMenuProvider provider)
    {
        Debug.Assert(itemSelect != null);
        Debug.Assert(provider != null);

        // Store incoming references
        _itemSelect = itemSelect!;
        _provider = provider!;

        _itemSelect!.TrackingIndex = -1;
        ItemEnabled = provider!.ProviderEnabled;
        _viewManager = provider.ProviderViewManager;

        // Cache the values to use when running
        _imageList = _itemSelect.ImageList!;
        _imageIndexStart = _itemSelect.ImageIndexStart;
        _imageIndexEnd = _itemSelect.ImageIndexEnd;
        _lineItems = _itemSelect.LineItems;
        _needPaint = provider.ProviderNeedPaintDelegate;
        _padding = _itemSelect.Padding;
        _imageCount = _imageList == null ? 0 : _imageList.Images.Count;

        // Limit check the start and end values
        _imageIndexStart = Math.Max(0, _imageIndexStart);
        _imageIndexEnd = Math.Min(_imageIndexEnd, _imageCount - 1);
        _imageIndexCount = Math.Max(0, _imageIndexEnd - _imageIndexStart + 1);

        PaletteBase palette = provider.ProviderPalette ?? KryptonManager.GetPaletteForMode(provider.ProviderPaletteMode);

        // Create triple that can be used by the draw button
        _triple = new PaletteTripleToPalette(palette,
            PaletteBackStyle.ButtonLowProfile,
            PaletteBorderStyle.ButtonLowProfile,
            PaletteContentStyle.ButtonLowProfile);

        // Update with current button style
        _triple.SetStyles(itemSelect!.ButtonStyle);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutMenuItemSelect:{Id}";

    #endregion

    #region ItemEnabled
    /// <summary>
    /// Gets the enabled state of the item.
    /// </summary>
    public bool ItemEnabled { get; }

    #endregion

    #region CanCloseMenu
    /// <summary>
    /// Gets a value indicating if the menu is capable of being closed.
    /// </summary>
    public bool CanCloseMenu => _provider.ProviderCanCloseMenu;

    #endregion

    #region Closing
    /// <summary>
    /// Raises the Closing event on the provider.
    /// </summary>
    /// <param name="cea">A CancelEventArgs containing the event data.</param>
    public void Closing(CancelEventArgs cea) => _provider.OnClosing(cea);

    #endregion

    #region Close
    /// <summary>
    /// Raises the Close event on the provider.
    /// </summary>
    /// <param name="e">A CancelEventArgs containing the event data.</param>
    public void Close(CloseReasonEventArgs e) => _provider.OnClose(e);

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Ensure that the correct number of children are created
        SyncChildren();

        var preferredSize = Size.Empty;

        // Find size of the first item, if there is one
        if (Count > 0)
        {
            // Ask child for it's own preferred size
            preferredSize = this[0]!.GetPreferredSize(context!);

            // Find preferred size from the preferred item size
            var lineItems = Math.Max(1, _lineItems);
            preferredSize.Width *= lineItems;
            preferredSize.Height *= (Count + (lineItems - 1)) / lineItems;
        }

        // Add on the requests padding
        preferredSize.Width += _padding.Horizontal;
        preferredSize.Height += _padding.Vertical;

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Ensure that the correct number of children are created
        SyncChildren();

        // Is there anything to layout?
        if (Count > 0)
        {
            // Reduce the client area by the requested padding before the internal children
            Rectangle displayRect = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _padding);

            // Get size of the first child, assume all others are same size
            Size itemSize = this[0]!.GetPreferredSize(context);

            // Starting position for first item
            Point nextPoint = displayRect.Location;
            for (var i = 0; i < Count; i++)
            {
                // Find rectangle for the child
                context.DisplayRectangle = new Rectangle(nextPoint, itemSize);

                // Layout the child
                this[i]?.Layout(context);

                // Move to next position across
                nextPoint.X += itemSize.Width;

                // Do we need to move to next line?
                if (((i + 1) % _lineItems) == 0)
                {
                    nextPoint.X = displayRect.X;
                    nextPoint.Y += itemSize.Height;
                }
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Private
    public void SyncChildren()
    {
        _selectedIndex = _itemSelect.SelectedIndex;

        // If we do not have enough already
        if (Count < _imageIndexCount)
        {
            // Create and add the number extra needed
            var create = _imageIndexCount - Count;
            for (var i = 0; i < create; i++)
            {
                Add(new ViewDrawMenuImageSelectItem(_viewManager, _itemSelect, _triple, this, _needPaint));
            }
        }
        else if (Count > _imageIndexCount)
        {
            // Destroy the extra ones no longer needed
            var remove = Count - _imageIndexCount;
            for (var i = 0; i < remove; i++)
            {
                RemoveAt(0);
            }
        }

        // Tell each item the image it should be displaying
        for (var i = 0; i < _imageIndexCount; i++)
        {
            var imageIndex = i + _imageIndexStart;
            var item = this[i] as ViewDrawMenuImageSelectItem;
            item!.ImageList = _imageList;
            item.ImageIndex = imageIndex;
            item.Checked = _selectedIndex == imageIndex;
            item.Enabled = ItemEnabled;
        }
    }
    #endregion
}