#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Custom list control for Office 2010-style backstage navigation items.
/// </summary>
internal class BackstageNavigationList : Control
{
    #region Instance Fields
    private readonly List<object> _items;
    private int _selectedIndex;
    private int _hoverIndex;
    private int _updateCount;
    private readonly KryptonBackstageView? _parentView;
    private readonly Dictionary<int, int> _itemHeights; // Cache item heights by index
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="BackstageNavigationList"/> class.
    /// </summary>
    /// <param name="parentView">Parent backstage view for color access.</param>
    public BackstageNavigationList(KryptonBackstageView? parentView = null)
    {
        _parentView = parentView;

        SetStyle(ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.Selectable, true);

        _items = [];
        _selectedIndex = -1;
        _hoverIndex = -1;
        _itemHeights = new Dictionary<int, int>();

        UpdateBackColor();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the DPI factor for X axis.
    /// </summary>
    protected float GetDpiFactorX()
    {
        using Graphics g = CreateGraphics();
        return g.DpiX / 96f;
    }

    /// <summary>
    /// Gets the DPI factor for Y axis.
    /// </summary>
    protected float GetDpiFactorY()
    {
        using Graphics g = CreateGraphics();
        return g.DpiY / 96f;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the collection of items.
    /// </summary>
    public IList<object> Items => _items;

    /// <summary>
    /// Gets and sets the selected index.
    /// </summary>
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (_selectedIndex != value)
            {
                _selectedIndex = value;
                Invalidate();
                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets and sets the selected item.
    /// </summary>
    public object? SelectedItem
    {
        get => _selectedIndex >= 0 && _selectedIndex < _items.Count ? _items[_selectedIndex] : null;
        set
        {
            var index = _items.IndexOf(value!);
            SelectedIndex = index;
        }
    }

    /// <summary>
    /// Clears the selected item.
    /// </summary>
    public void ClearSelected()
    {
        SelectedIndex = -1;
    }

    /// <summary>
    /// Begins a batch update operation.
    /// </summary>
    public void BeginUpdate()
    {
        _updateCount++;
    }

    /// <summary>
    /// Ends a batch update operation.
    /// </summary>
    public void EndUpdate()
    {
        _updateCount--;
        if (_updateCount <= 0)
        {
            _updateCount = 0;
            Invalidate();
        }
    }

    /// <summary>
    /// Occurs when the selected index changes.
    /// </summary>
    public event EventHandler? SelectedIndexChanged;
    #endregion

    #region Protected
    /// <summary>
    /// Raises the <see cref="SelectedIndexChanged"/> event.
    /// </summary>
    protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="Control.Paint"/> event.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_updateCount > 0)
        {
            return;
        }

        UpdateBackColor();

        var g = e.Graphics;
        g.Clear(BackColor);

        if (_items.Count == 0)
        {
            return;
        }

        var y = 0;
        _itemHeights.Clear();
        for (var i = 0; i < _items.Count; i++)
        {
            var itemHeight = GetItemHeight(_items[i]);
            _itemHeights[i] = itemHeight;
            var itemRect = new Rectangle(0, y, Width, itemHeight);
            var isSelected = i == _selectedIndex;
            var isHover = i == _hoverIndex && !isSelected;

            DrawItem(g, itemRect, _items[i], isSelected, isHover);

            y += itemHeight;
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseMove"/> event.
    /// </summary>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        var newHoverIndex = GetItemIndexAtY(e.Y);
        if (newHoverIndex >= 0 && newHoverIndex < _items.Count)
        {
            if (_hoverIndex != newHoverIndex)
            {
                _hoverIndex = newHoverIndex;
                Invalidate();
            }
        }
        else if (_hoverIndex != -1)
        {
            _hoverIndex = -1;
            Invalidate();
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseLeave"/> event.
    /// </summary>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (_hoverIndex != -1)
        {
            _hoverIndex = -1;
            Invalidate();
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseClick"/> event.
    /// </summary>
    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);

        if (e.Button == MouseButtons.Left)
        {
            var clickedIndex = GetItemIndexAtY(e.Y);
            if (clickedIndex >= 0 && clickedIndex < _items.Count)
            {
                SelectedIndex = clickedIndex;
            }
        }
    }
    #endregion

    #region Implementation
    private int GetItemHeight(object item)
    {
        var itemSize = GetItemSize(item);
        var baseHeight = itemSize == BackstageItemSize.Large ? 60 : 40;
        return (int)(baseHeight * GetDpiFactorY());
    }

    private BackstageItemSize GetItemSize(object item)
    {
        if (item is KryptonBackstagePage page)
        {
            return page.ItemSize;
        }

        if (item is KryptonBackstageCommand command)
        {
            return command.ItemSize;
        }

        return BackstageItemSize.Small; // Default for Close item
    }

    private Image? GetItemImage(object item)
    {
        if (item is KryptonBackstagePage page)
        {
            return page.Image;
        }

        if (item is KryptonBackstageCommand command)
        {
            return command.Image;
        }

        return null;
    }

    private int GetItemIndexAtY(int y)
    {
        var currentY = 0;
        for (var i = 0; i < _items.Count; i++)
        {
            var itemHeight = _itemHeights.TryGetValue(i, out var height) ? height : GetItemHeight(_items[i]);
            if (y >= currentY && y < currentY + itemHeight)
            {
                return i;
            }
            currentY += itemHeight;
        }
        return -1;
    }

    private void DrawItem(Graphics g, Rectangle rect, object item, bool isSelected, bool isHover)
    {
        // Selected item: use custom or theme highlight color
        if (isSelected)
        {
            var highlightColor = _parentView?.GetSelectedItemHighlightColor() ?? Color.FromArgb(242, 155, 57);
            using var brush = new SolidBrush(highlightColor);
            g.FillRectangle(brush, rect);
        }
        else if (isHover)
        {
            // Light hover effect
            using var brush = new SolidBrush(Color.FromArgb(250, 250, 250));
            g.FillRectangle(brush, rect);
        }

        var itemSize = GetItemSize(item);
        var image = GetItemImage(item);
        var text = GetItemText(item);
        var imageSize = itemSize == BackstageItemSize.Large ? 32 : 16;
        var imagePadding = (int)(12 * GetDpiFactorX());
        var textPadding = image != null ? imageSize + imagePadding * 2 : imagePadding;

        // Draw image if available
        if (image != null)
        {
            var imageRect = new Rectangle(
                rect.X + imagePadding,
                rect.Y + (rect.Height - imageSize) / 2,
                imageSize,
                imageSize);

            // Scale image if needed
            if (image.Width == imageSize && image.Height == imageSize)
            {
                // Image is already the correct size
                g.DrawImage(image, imageRect);
            }
            else
            {
                // Scale image to fit
                using var scaledImage = new Bitmap(image, imageSize, imageSize);
                g.DrawImage(scaledImage, imageRect);
            }
        }

        // Draw item text
        if (!string.IsNullOrEmpty(text))
        {
            var textRect = rect;
            textRect.X += textPadding;
            textRect.Width -= textPadding + imagePadding;

            var textColor = isSelected ? Color.Black : Color.FromArgb(51, 51, 51);
            using var brush = new SolidBrush(textColor);

            // Use slightly larger font for large items
            var fontSize = itemSize == BackstageItemSize.Large ? Font.Size * 1.1f : Font.Size;
            using var font = new Font(Font.FontFamily, fontSize, FontStyle.Regular);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            g.DrawString(text, font, brush, textRect, format);
        }
    }

    private string GetItemText(object item) =>
        item switch
        {
            KryptonBackstagePage page => page.Text,
            KryptonBackstageCommand command => command.Text,
            BackstageCloseItem closeItem => closeItem.Text,
            _ => item?.ToString() ?? string.Empty
        };

    private void UpdateBackColor() => BackColor = _parentView?.GetNavigationBackgroundColor() ?? Color.FromArgb(240, 240, 240);

    #endregion
}