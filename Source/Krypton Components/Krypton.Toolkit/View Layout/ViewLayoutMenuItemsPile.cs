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
/// Extends the ViewLayoutPile so that menu items are laid out in columns.
/// </summary>
public class ViewLayoutMenuItemsPile : ViewLayoutPile
{
    #region Type Definitions
    private class ColumnToWidth : Dictionary<int, int>;
    #endregion

    #region Instance Fields
    private readonly PaletteDoubleMetricRedirect? _paletteItemHighlight;
    private readonly ViewDrawMenuImageColumn _imageColumn;
    private ColumnToWidth _columnToWidth;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutMenuItemsPile class.
    /// </summary>
    /// <param name="provider">Provider of context menu values.</param>
    /// <param name="items">Reference to the owning collection.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    public ViewLayoutMenuItemsPile(IContextMenuProvider provider,
        KryptonContextMenuItems items,
        bool standardStyle,
        bool imageColumn)
    {
        // Cache access to the highlight item palette
        _paletteItemHighlight = provider.ProviderStateCommon.ItemHighlight;

        // Create and place an image column inside a docker so it appears on the left side
        _imageColumn = new ViewDrawMenuImageColumn(items, provider.ProviderStateCommon.ItemImageColumn);
        var imageDocker = new ViewLayoutDocker
        {
            { _imageColumn, ViewDockStyle.Left }
        };

        // Only show the image column when in a standard collection of items
        imageDocker.Visible = imageColumn;

        // Create a vertical stack that contains each individual menu item
        ItemStack = new ViewLayoutStack(false)
        {
            FillLastChild = false
        };

        // Use a docker with the item stack as the fill
        var stackDocker = new ViewLayoutDocker
        {
            { ItemStack, ViewDockStyle.Fill }
        };

        // Grab the padding for around the item stack
        Padding itemsPadding = _paletteItemHighlight!.GetMetricPadding(null, PaletteState.Normal, PaletteMetricPadding.ContextMenuItemsCollection);
        stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Left), ViewDockStyle.Left);
        stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Right), ViewDockStyle.Right);
        stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Top), ViewDockStyle.Top);
        stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Bottom), ViewDockStyle.Bottom);

        // The background of the pile is the image column
        Add(imageDocker);

        // The foreground of the pile is the stack of menu items
        Add(stackDocker);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutMenuItemsPile:{Id}";

    #endregion

    #region ItemStack
    /// <summary>
    /// Gets access to the stack containing individual menu items
    /// </summary>
    public ViewLayoutStack ItemStack { get; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
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

        // Reset the column size information
        _columnToWidth = new ColumnToWidth();

        // Remove any override currently in place for columns
        ClearMenuItemColumns(this);

        base.GetPreferredSize(context);

        // Gather the largest size of each column instance
        GatherMenuItemColumns(this);

        // Tell each column to use the largest size for that column
        OverrideMenuItemColumns(this);

        // Modify the first (image) column for the padding of the item highlight
        UpdateImageColumnWidth(context.Renderer);

        return base.GetPreferredSize(context);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        base.Layout(context!);
    }
    #endregion  
  
    #region Implementation
    private void GatherMenuItemColumns(ViewBase element)
    {
        // Does this element expose the column interface?
        if (element is IContextMenuItemColumn column)
        {
            var columnIndex = column.ColumnIndex;
            Size columnPreferredSize = column.LastPreferredSize;

            // If the first entry for this column...
            if (!_columnToWidth.ContainsKey(columnIndex))
            {
                _columnToWidth.Add(columnIndex, columnPreferredSize.Width);
            }
            else
            {
                // Grab the current preferred size
                var preferredWidth = _columnToWidth[columnIndex];

                // Find the largest sizing
                preferredWidth = Math.Max(preferredWidth, columnPreferredSize.Width);

                // Put modified value back
                _columnToWidth[columnIndex] = preferredWidth;
            }
        }

        // Process child elements
        foreach (ViewBase child in element)
        {
            GatherMenuItemColumns(child);
        }
    }

    private void OverrideMenuItemColumns(ViewBase element)
    {
        // Does this element expose the column interface?
        if (element is IContextMenuItemColumn column)
        {
            column.OverridePreferredWidth = _columnToWidth[column.ColumnIndex];
        }

        // Process child elements
        foreach (ViewBase child in element)
        {
            OverrideMenuItemColumns(child);
        }
    }

    private void ClearMenuItemColumns(ViewBase element)
    {
        // Does this element expose the column interface?
        if (element is IContextMenuItemColumn column)
        {
            column.OverridePreferredWidth = 0;
        }

        // Process child elements
        foreach (ViewBase child in element)
        {
            ClearMenuItemColumns(child);
        }
    }

    private void UpdateImageColumnWidth(IRenderer renderer)
    {
        // If there is an image column then we will have a entry for index 0
        if (_columnToWidth.TryGetValue(0, out var imageColumnWidth))
        {
            // Find the border padding that is applied to the content of the menu item
            Padding borderPadding = renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteItemHighlight?.Border!,
                PaletteState.Normal,
                VisualOrientation.Top);

            // Add double the left edge to the right edge of the image background coumn
            imageColumnWidth += borderPadding.Left * 3;

            // Add double the metric padding that occurs outside the item highlight
            Padding itemMetricPadding = _paletteItemHighlight!.GetMetricPadding(null, PaletteState.Normal, PaletteMetricPadding.ContextMenuItemHighlight);
            imageColumnWidth += itemMetricPadding.Left * 2;

            _imageColumn.ColumnWidth = imageColumnWidth;
        }
    }
    #endregion
}