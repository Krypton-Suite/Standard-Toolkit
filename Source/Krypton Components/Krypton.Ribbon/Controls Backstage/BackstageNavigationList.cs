#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
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
    private int _columns;
    private float _dpiFactorX;
    private float _dpiFactorY;
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
        _columns = 1;

        UpdateDpiFactors();
        UpdateBackColor();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the DPI factor for X axis.
    /// </summary>
    protected float GetDpiFactorX() => _dpiFactorX;

    /// <summary>
    /// Gets the DPI factor for Y axis.
    /// </summary>
    protected float GetDpiFactorY() => _dpiFactorY;
    #endregion

    #region Public
    /// <summary>
    /// Gets the collection of items.
    /// </summary>
    public IList<object> Items => _items;

    /// <summary>
    /// Gets and sets the selected index.
    /// </summary>
    [DefaultValue(-1)]
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    /// Gets and sets the number of columns for displaying items.
    /// </summary>
    [DefaultValue(1)]
    public int Columns
    {
        get => _columns;
        set
        {
            if (_columns != value && value > 0)
            {
                _columns = value;
                Invalidate();
            }
        }
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

        UpdateDpiFactors();
        UpdateBackColor();

        var g = e.Graphics;
        g.Clear(BackColor);

        if (_items.Count == 0)
        {
            return;
        }

        if (IsOffice2024Navigation())
        {
            PaintOffice2024(g);
            return;
        }

        // Calculate column width
        var columnWidth = _columns > 1 ? Math.Max(Width / _columns, 1) : Width;
        var rowsPerColumn = (int)Math.Ceiling((double)_items.Count / _columns);

        _itemHeights.Clear();
        
        // First pass: calculate all item heights and row heights
        // Items fill row-major: across rows first (index 0,1,2 in row 0, then 3,4,5 in row 1, etc.)
        var rowHeights = new List<int>();
        for (var row = 0; row < rowsPerColumn; row++)
        {
            var maxHeight = 0;
            for (var col = 0; col < _columns; col++)
            {
                var index = row * _columns + col;
                if (index < _items.Count)
                {
                    var itemHeight = GetItemHeight(_items[index]);
                    _itemHeights[index] = itemHeight;
                    maxHeight = Math.Max(maxHeight, itemHeight);
                }
            }
            rowHeights.Add(maxHeight);
        }

        // Second pass: draw items in grid layout
        var currentY = 0;
        for (var row = 0; row < rowsPerColumn; row++)
        {
            var rowHeight = rowHeights[row];
            for (var col = 0; col < _columns; col++)
            {
                var index = row * _columns + col;
                if (index < _items.Count)
                {
                    var itemRect = new Rectangle(col * columnWidth, currentY, columnWidth, rowHeight);
                    var isSelected = index == _selectedIndex;
                    var isHover = index == _hoverIndex && !isSelected;

                    DrawItem(g, itemRect, _items[index], isSelected, isHover);
                }
            }
            currentY += rowHeight;
        }
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseMove"/> event.
    /// </summary>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        var newHoverIndex = GetItemIndexAtPoint(e.Location);
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
            var clickedIndex = GetItemIndexAtPoint(e.Location);
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

    private int GetItemIndexAtPoint(Point point)
    {
        if (IsOffice2024Navigation())
        {
            foreach (NavSlot slot in BuildOffice2024Slots())
            {
                if (slot.ItemRect.Contains(point))
                {
                    return slot.Index;
                }
            }

            return -1;
        }

        if (_columns <= 1)
        {
            // Single column: use original logic
            var currentY = 0;
            for (var i = 0; i < _items.Count; i++)
            {
                var itemHeight = _itemHeights.TryGetValue(i, out var height) ? height : GetItemHeight(_items[i]);
                if (point.Y >= currentY && point.Y < currentY + itemHeight)
                {
                    return i;
                }
                currentY += itemHeight;
            }
            return -1;
        }

        // Multi-column: calculate row and column from position
        // Items fill row-major: across rows first (index 0,1,2 in row 0, then 3,4,5 in row 1, etc.)
        var columnWidth = Math.Max(Width / _columns, 1);
        var column = Math.Max(0, Math.Min(point.X / columnWidth, _columns - 1));
        var rowsPerColumn = (int)Math.Ceiling((double)_items.Count / _columns);

        // Calculate row heights to find which row we're in
        var rowY = 0;
        for (var row = 0; row < rowsPerColumn; row++)
        {
            var maxHeight = 0;
            for (var col = 0; col < _columns; col++)
            {
                var index = row * _columns + col;
                if (index < _items.Count)
                {
                    var itemHeight = _itemHeights.TryGetValue(index, out var height) ? height : GetItemHeight(_items[index]);
                    maxHeight = Math.Max(maxHeight, itemHeight);
                }
            }

            if (point.Y >= rowY && point.Y < rowY + maxHeight)
            {
                // Found the row, now get the item index for this column and row
                var index = row * _columns + column;
                if (index >= 0 && index < _items.Count)
                {
                    return index;
                }
                return -1;
            }
            rowY += maxHeight;
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
            // Theme-aware hover effect
            var hoverColor = _parentView?.GetHoverItemHighlightColor() ?? Color.FromArgb(250, 250, 250);
            using var brush = new SolidBrush(hoverColor);
            g.FillRectangle(brush, rect);
        }

        var itemSize = GetItemSize(item);
        var image = GetItemImage(item);
        var text = GetItemText(item);
        var baseImageSize = itemSize == BackstageItemSize.Large ? 32 : 16;
        var imageSize = (int)(baseImageSize * GetDpiFactorX());
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

            // Theme-aware text color
            Color textColor;
            if (isSelected)
            {
                // Calculate text color based on highlight color luminance for proper contrast
                var highlightColor = _parentView?.GetSelectedItemHighlightColor() ?? Color.FromArgb(242, 155, 57);
                textColor = _parentView?.GetTextColorForBackground(highlightColor) ?? Color.FromArgb(51, 51, 51);
            }
            else
            {
                // Use theme-aware text color based on background
                textColor = _parentView?.GetNavigationTextColor() ?? Color.FromArgb(51, 51, 51);
            }
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

    private void UpdateDpiFactors()
    {
        using Graphics g = CreateGraphics();
        _dpiFactorX = g.DpiX / 96f;
        _dpiFactorY = g.DpiY / 96f;
    }

    private bool IsOffice2024Navigation() => _parentView?.IsOffice2024Navigation == true;

    private void PaintOffice2024(Graphics g)
    {
        int footerLineY = -1;
        List<NavSlot> slots = BuildOffice2024Slots(ref footerLineY);
        Color back = _parentView?.GetNavigationBackgroundColor() ?? BackColor;
        if (footerLineY >= 0)
        {
            int inset = Math.Max(8, (int)Math.Round(12f * GetDpiFactorX()));
            using var pen = new Pen(ShiftColor(back, IsDark(back) ? 40 : -28));
            g.DrawLine(pen, inset, footerLineY, Math.Max(inset, Width - inset), footerLineY);
        }

        foreach (NavSlot slot in slots)
        {
            if (slot.HasSeparator)
            {
                int inset = Math.Max(8, (int)Math.Round(12f * GetDpiFactorX()));
                using var pen = new Pen(ShiftColor(back, IsDark(back) ? 40 : -28));
                g.DrawLine(pen, inset, slot.SeparatorY, Math.Max(inset, Width - inset), slot.SeparatorY);
            }

            var isSelected = slot.Index == _selectedIndex;
            var isHover = slot.Index == _hoverIndex && !isSelected;
            DrawOffice2024Item(g, slot.ItemRect, _items[slot.Index], isSelected, isHover);
        }
    }

    private List<NavSlot> BuildOffice2024Slots()
    {
        int ignored = -1;
        return BuildOffice2024Slots(ref ignored);
    }

    private List<NavSlot> BuildOffice2024Slots(ref int footerLineY)
    {
        var slots = new List<NavSlot>();
        footerLineY = -1;
        int row = Math.Max(24, (int)Math.Round(32f * GetDpiFactorY()));
        int gap = Math.Max(8, (int)Math.Round(14f * GetDpiFactorY()));
        int pad = Math.Max(4, (int)Math.Round(8f * GetDpiFactorY()));
        var main = new List<int>();
        var footer = new List<int>();
        for (var i = 0; i < _items.Count; i++)
        {
            if (IsPlaceAtBottom(_items[i]))
            {
                footer.Add(i);
            }
            else
            {
                main.Add(i);
            }
        }

        var y = pad;
        foreach (int index in main)
        {
            AddOffice2024Slot(slots, index, ref y, row, gap);
        }

        if (footer.Count == 0)
        {
            return slots;
        }

        var footerBlock = pad + gap;
        foreach (int index in footer)
        {
            footerBlock += row;
            if (HasSeparator(_items[index]))
            {
                footerBlock += gap;
            }
        }

        var fy = Math.Max(y, Height - footerBlock);
        footerLineY = fy + (gap / 2);
        fy += gap;
        foreach (int index in footer)
        {
            AddOffice2024Slot(slots, index, ref fy, row, gap);
        }

        return slots;
    }

    private void AddOffice2024Slot(List<NavSlot> slots, int index, ref int y, int row, int gap)
    {
        var slot = new NavSlot { Index = index };
        if (HasSeparator(_items[index]))
        {
            slot.HasSeparator = true;
            slot.SeparatorY = y + (gap / 2);
            y += gap;
        }

        slot.ItemRect = new Rectangle(0, y, Width, row);
        y += row;
        slots.Add(slot);
    }

    private void DrawOffice2024Item(Graphics g, Rectangle rect, object item, bool isSelected, bool isHover)
    {
        Color back = _parentView?.GetNavigationBackgroundColor() ?? BackColor;
        if (isSelected || isHover)
        {
            int insetX = Math.Max(6, (int)Math.Round(8f * GetDpiFactorX()));
            int insetY = Math.Max(2, (int)Math.Round(3f * GetDpiFactorY()));
            var pill = new Rectangle(rect.X + insetX, rect.Y + insetY, Math.Max(1, rect.Width - (insetX * 2)), Math.Max(1, rect.Height - (insetY * 2)));
            Color fill = isSelected && _parentView != null && _parentView.TryGetCustomSelectedHighlight(out Color custom)
                ? custom
                : ShiftColor(back, IsDark(back) ? (isSelected ? 32 : 16) : (isSelected ? -22 : -10));
            Color border = ShiftColor(back, IsDark(back) ? 56 : -40);
            SmoothingMode smoothing = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = Office2024PillPath(pill))
            {
                using var brush = new SolidBrush(fill);
                g.FillPath(brush, path);
                using var pen = new Pen(border);
                g.DrawPath(pen, path);
            }

            g.SmoothingMode = smoothing;
        }

        var image = GetItemImage(item);
        var text = GetItemText(item);
        var imageSize = Math.Max(12, (int)Math.Round(16f * GetDpiFactorX()));
        var imagePadding = Math.Max(8, (int)Math.Round(12f * GetDpiFactorX()));
        var textPadding = image != null ? imageSize + (imagePadding * 2) : imagePadding;
        if (image != null)
        {
            var imageRect = new Rectangle(rect.X + imagePadding, rect.Y + ((rect.Height - imageSize) / 2), imageSize, imageSize);
            g.DrawImage(image, imageRect);
        }

        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        var textRect = rect;
        textRect.X += textPadding;
        textRect.Width -= textPadding + imagePadding;
        Color textColor = _parentView?.GetNavigationTextColor() ?? ForeColor;
        if (isSelected && _parentView != null && _parentView.TryGetCustomSelectedHighlight(out Color highlight))
        {
            textColor = _parentView.GetTextColorForBackground(highlight);
        }

        using var textBrush = new SolidBrush(textColor);
        using var format = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };
        g.DrawString(text, Font, textBrush, textRect, format);
    }

    private static GraphicsPath Office2024PillPath(Rectangle rect)
    {
        var path = new GraphicsPath();
        if (rect.Width <= rect.Height)
        {
            path.AddEllipse(rect);
            return path;
        }

        float diameter = rect.Height;
        var bounds = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 90f, 180f);
        path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270f, 180f);
        path.CloseFigure();
        return path;
    }

    private static bool IsPlaceAtBottom(object item) =>
        item switch
        {
            KryptonBackstagePage page => page.PlaceAtBottom,
            KryptonBackstageCommand command => command.PlaceAtBottom,
            _ => false
        };

    private static bool HasSeparator(object item) =>
        item switch
        {
            KryptonBackstagePage page => page.SeparatorBefore,
            KryptonBackstageCommand command => command.SeparatorBefore,
            _ => false
        };

    private static bool IsDark(Color color) =>
        ((0.2126 * color.R) + (0.7152 * color.G) + (0.0722 * color.B)) / 255.0 < 0.5;

    private static Color ShiftColor(Color color, int delta) =>
        Color.FromArgb(ClampChannel(color.R + delta), ClampChannel(color.G + delta), ClampChannel(color.B + delta));

    private static int ClampChannel(int channel)
    {
        if (channel < 0)
        {
            return 0;
        }

        return channel > 255 ? 255 : channel;
    }

    private struct NavSlot
    {
        public int Index;
        public Rectangle ItemRect;
        public bool HasSeparator;
        public int SeparatorY;
    }

    #endregion
}