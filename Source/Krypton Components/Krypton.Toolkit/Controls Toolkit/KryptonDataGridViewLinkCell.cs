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

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

/// <summary>
/// Displays a link label-like user interface (UI) for use in a DataGridView control.
/// </summary>
public class KryptonDataGridViewLinkCell : DataGridViewLinkCell
{
    #region Static Fields
    private static PropertyInfo _piLinkState;
    #endregion

    #region Instance Fields
    private bool _linkDefined;
    private bool _labelStyleDefined;
    private LabelStyle _labelStyle;
    private PaletteContentToPalette _palette;
    private LinkLabelBehaviorInherit _inheritBehavior;
    private PaletteContentInheritOverride _overrideVisited;
    private PaletteContentInheritOverride _overridePressed;
    private ShortTextValue _shortTextValue;
    private ViewDrawContent _viewLabel;
    private Rectangle _contentBounds;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewLinkCell.
    /// </summary>
    public KryptonDataGridViewLinkCell()
    {
        _labelStyle = LabelStyle.NormalPanel;
        base.LinkBehavior = LinkBehavior.AlwaysUnderline;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets a value that represents the behavior of links.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(LinkBehavior.AlwaysUnderline)]
    public new LinkBehavior LinkBehavior
    {
        get => base.LinkBehavior;

        set
        {
            if (value != base.LinkBehavior)
            {
                base.LinkBehavior = value;
                _linkDefined = true;
            }
        }
    }

    /// <summary>
    /// Gets or sets a display style for drawing link cell.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(LabelStyle.NormalPanel)]
    public LabelStyle LabelStyle
    {
        get => _labelStyle;

        set
        {
            if (value != _labelStyle)
            {
                _labelStyle = value;
                _labelStyleDefined = true;
                DataGridView!.InvalidateCell(this);
            }
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Returns the bounding rectangle that encloses the cell's content area.
    /// </summary>
    /// <param name="graphics">Graphics instance for calculations.</param>
    /// <param name="cellStyle">Cell style to use in calculations.</param>
    /// <param name="rowIndex">The index of the cell's parent row.</param>
    /// <returns></returns>
    protected override Rectangle GetContentBounds(Graphics graphics,
        DataGridViewCellStyle cellStyle,
        int rowIndex) =>
        // Return the cached bounds from last drawing cycle
        _contentBounds;

    /// <summary>
    /// This member overrides DataGridViewCell.GetPreferredSize. 
    /// </summary>
    /// <param name="graphics">Graphics instance used for calculations.</param>
    /// <param name="cellStyle">Individual cell style to apply.</param>
    /// <param name="rowIndex">Row of column being processed.</param>
    /// <param name="constraintSize">Maximum allowed size.</param>
    /// <returns>Requested ideal size for the cell.</returns>
    protected override Size GetPreferredSize(Graphics graphics,
        DataGridViewCellStyle cellStyle,
        int rowIndex,
        Size constraintSize)
    {
        try
        {
            var kDGV = DataGridView as KryptonDataGridView;

            // Ensure the view classes are created and hooked up
            CreateViewAndPalettes(kDGV);

            // Update the element with the correct state and used palette
            SetElementStateAndPalette();

            // Update the display text
            if ((rowIndex >= 0) && !string.IsNullOrEmpty(FormattedValue?.ToString()))
            {
                _shortTextValue.ShortText = FormattedValue!.ToString();
            }
            else
            {
                if ((kDGV?.Columns[ColumnIndex] is KryptonDataGridViewButtonColumn
                    {
                        UseColumnTextForButtonValue: true
                    } col) && !kDGV.Rows[rowIndex].IsNewRow)
                {
                    _shortTextValue.ShortText = col.Text!;
                }
                else
                {
                    _shortTextValue.ShortText = string.Empty;
                }
            }

            // Position the button element inside the available cell area
            using var layoutContext = new ViewLayoutContext(kDGV!, kDGV?.Renderer!);
            // Define the available area for layout
            layoutContext.DisplayRectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);

            // Get the ideal size of the label
            Size labelSize = _viewLabel.GetPreferredSize(layoutContext);

            // Add on the requested cell padding (plus add 1 to counter the -1 that occurs
            // in the painting routine to prevent drawing over the bottom right border)
            labelSize.Width += cellStyle.Padding.Horizontal + 1;
            labelSize.Height += cellStyle.Padding.Vertical + 1;

            return labelSize;
        }
        catch
        {
            return Size.Empty;
        }
    }

    /// <summary>
    /// This member overrides DataGridViewCell.Paint.
    /// </summary>
    /// <param name="graphics">The Graphics used to paint the DataGridViewCell.</param>
    /// <param name="clipBounds">A Rectangle that represents the area of the DataGridView that needs to be repainted.</param>
    /// <param name="cellBounds">A Rectangle that contains the bounds of the DataGridViewCell that is being painted.</param>
    /// <param name="rowIndex">The row index of the cell that is being painted.</param>
    /// <param name="cellState">A bitwise combination of DataGridViewElementStates values that specifies the state of the cell.</param>
    /// <param name="value">The data of the DataGridViewCell that is being painted.</param>
    /// <param name="formattedValue">The formatted data of the DataGridViewCell that is being painted.</param>
    /// <param name="errorText">An error message that is associated with the cell.</param>
    /// <param name="cellStyle">A DataGridViewCellStyle that contains formatting and style information about the cell.</param>
    /// <param name="advancedBorderStyle">A DataGridViewAdvancedBorderStyle that contains border styles for the cell that is being painted.</param>
    /// <param name="paintParts">A bitwise combination of the DataGridViewPaintParts values that specifies which parts of the cell need to be painted.</param>
    protected override void Paint(Graphics graphics,
        Rectangle clipBounds,
        Rectangle cellBounds,
        int rowIndex,
        DataGridViewElementStates cellState,
        object? value,
        object? formattedValue,
        string? errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
    {
        if (DataGridView is KryptonDataGridView kDgv)
        {
            // Should we draw the content foreground?
            if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
            {
                using var renderContext = new RenderContext(kDgv, graphics, cellBounds, kDgv.Renderer!);
                // Cache the starting cell bounds
                Rectangle startBounds = cellBounds;

                // Ensure the view classes are created and hooked up
                CreateViewAndPalettes(kDgv);

                // Update the element with the correct state and used palette
                SetElementStateAndPalette();

                // Update the display text
                if (!string.IsNullOrEmpty(formattedValue?.ToString())!)
                {
                    _shortTextValue.ShortText = formattedValue!.ToString();
                }
                else
                {
                    if ((kDgv.Columns[ColumnIndex] is KryptonDataGridViewButtonColumn
                        {
                            UseColumnTextForButtonValue: true
                        } col) && !kDgv.Rows[rowIndex].IsNewRow)
                    {
                        _shortTextValue.ShortText = col.Text!;
                    }
                    else
                    {
                        _shortTextValue.ShortText = string.Empty;
                    }
                }

                // Prevent button overlapping the bottom/right border
                cellBounds.Width--;
                cellBounds.Height--;

                // Apply the padding
                if (kDgv.RightToLeftInternal)
                {
                    cellBounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Bottom);
                }
                else
                {
                    cellBounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                }

                cellBounds.Width -= cellStyle.Padding.Horizontal;
                cellBounds.Height -= cellStyle.Padding.Vertical;

                // Position the button element inside the available cell area
                using (var layoutContext = new ViewLayoutContext(kDgv, kDgv.Renderer!))
                {
                    // Define the available area for calculating layout
                    layoutContext.DisplayRectangle = cellBounds;

                    // Get the requests size for the label
                    Size contentSize = _viewLabel.GetPreferredSize(layoutContext);

                    // Adjust the horizontal alignment
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.NotSet:
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.BottomCenter:
                            cellBounds.X += (cellBounds.Width - contentSize.Width) / 2;
                            break;
                        case DataGridViewContentAlignment.TopRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.BottomRight:
                            cellBounds.X = cellBounds.Right - contentSize.Width;
                            break;
                    }

                    // Adjust the vertical alignment
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.NotSet:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.MiddleRight:
                            cellBounds.Y += (cellBounds.Height - contentSize.Height) / 2;
                            break;
                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.BottomRight:
                            cellBounds.Y = cellBounds.Bottom - contentSize.Height;
                            break;
                    }

                    // Make the cell the same size as the check box itself
                    cellBounds.Width = Math.Min(contentSize.Width, cellBounds.Width);
                    cellBounds.Height = Math.Min(contentSize.Height, cellBounds.Height);

                    // Perform actual layout inside that area
                    layoutContext.DisplayRectangle = cellBounds;
                    _viewLabel.Layout(layoutContext);
                }

                // Ask the element to draw now
                _viewLabel.Render(renderContext);

                // Remember the current drawing bounds
                _contentBounds = cellBounds with { X = cellBounds.X - startBounds.X, Y = cellBounds.Y - startBounds.Y };
            }
        }
        else
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                cellState, value, formattedValue, errorText,
                cellStyle, advancedBorderStyle, paintParts);
        }
    }
    #endregion

    #region Internal
    internal LinkBehavior LinkBehaviorInternal
    {
        set
        {
            if (!_linkDefined)
            {
                base.LinkBehavior = value;
            }
        }
    }

    internal LabelStyle LabelStyleInternal
    {
        set
        {
            if (!_labelStyleDefined)
            {
                _labelStyle = value;
            }
        }
    }
    #endregion

    #region Private
    private void CreateViewAndPalettes(KryptonDataGridView? kDGV)
    {
        // Create the view element when first needed
        if (_viewLabel is null)
        {
            // Create helper object to get all values from the DGV redirector
            _palette = new PaletteContentToPalette(kDGV!.Redirector, PaletteContentStyle.LabelNormalPanel);
            _inheritBehavior = new LinkLabelBehaviorInherit(_palette, KryptonLinkBehavior.AlwaysUnderline);
            _overrideVisited = new PaletteContentInheritOverride(_palette, _inheritBehavior, PaletteState.LinkNotVisitedOverride, true);
            _overridePressed = new PaletteContentInheritOverride(_palette, _overrideVisited, PaletteState.LinkPressedOverride, false);

            // Provider of values for the button element
            _shortTextValue = new ShortTextValue();

            // Create view element for drawing the actual button
            _viewLabel = new ViewDrawContent(_overridePressed, _shortTextValue, VisualOrientation.Top);
        }
    }

    private void SetElementStateAndPalette()
    {
        LinkState? linkState = LinkStateInternal;

        // Has the item been visited
        _overrideVisited.OverrideState = LinkVisited ? PaletteState.LinkVisitedOverride : PaletteState.LinkNotVisitedOverride;

        // Is the item being pressed?
        _overridePressed.Apply = (linkState & LinkState.Active) == LinkState.Active;

        _viewLabel.ElementState = (linkState & LinkState.Hover) == LinkState.Hover
            ? PaletteState.Tracking
            : PaletteState.Normal;

        // Update with latest cell setting for the link behavior
        _inheritBehavior.LinkBehavior = base.LinkBehavior switch
        {
            LinkBehavior.HoverUnderline => KryptonLinkBehavior.HoverUnderline,
            LinkBehavior.NeverUnderline => KryptonLinkBehavior.NeverUnderline,
            _ => KryptonLinkBehavior.AlwaysUnderline
        };

        // Use the latest defined label style
        _palette.ContentStyle = CommonHelper.ContentStyleFromLabelStyle(_labelStyle);
    }

    private LinkState? LinkStateInternal
    {
        get
        {
            // Only need to cache reflection info the first time around
            if (_piLinkState is null)
            {
                // Cache access to the internal get property 'LinkState'
                _piLinkState = typeof(DataGridViewLinkCell).GetProperty(nameof(LinkState), BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.GetField)!;
            }

            // Grab the internal property implemented by base class
            return (LinkState?)_piLinkState!.GetValue(this, null);
        }
    }
    #endregion
}