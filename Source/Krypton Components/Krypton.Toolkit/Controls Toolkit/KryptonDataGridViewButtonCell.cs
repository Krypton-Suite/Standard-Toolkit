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
/// Displays a button-like user interface (UI) for use in a DataGridView control.
/// </summary>
public class KryptonDataGridViewButtonCell : DataGridViewButtonCell
{
    #region Static Fields
    private static PropertyInfo? _piButtonState;
    private static PropertyInfo? _piMouseEnteredCellAddress;
    private static FieldInfo? _fiMouseInContentBounds;
    #endregion

    #region Instance Fields
    private bool _styleSet;
    private ButtonStyle _buttonStyle;
    private PaletteTripleToPalette _palette;
    private ShortTextValue? _shortTextValue;
    private ViewDrawButton _viewButton;
    private Rectangle _contentBounds;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewCheckBoxCell.
    /// </summary>
    public KryptonDataGridViewButtonCell() => _buttonStyle = ButtonStyle.Standalone;

    #endregion

    #region Public
    /// <summary>
    /// This member overrides KryptonDataGridViewButtonCell.Clone.
    /// </summary>
    /// <returns>New object instance.</returns>
    public override object Clone()
    {
        var dataGridViewCell = base.Clone() as KryptonDataGridViewButtonCell ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("dataGridViewCell"));

        dataGridViewCell._styleSet = _styleSet;
        dataGridViewCell._shortTextValue = _shortTextValue;
        dataGridViewCell._buttonStyle = _buttonStyle;

        return dataGridViewCell;
    }

    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(ButtonStyle.Standalone)]
    public ButtonStyle ButtonStyle
    {
        get => _buttonStyle;

        set
        {
            _buttonStyle = value;
            _styleSet = true;
            DataGridView?.InvalidateCell(this);
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

            // Create the view elements and palette structure
            CreateViewAndPalettes(kDGV!);

            // Is this cell the currently active cell
            var currentCell = (rowIndex == DataGridView!.CurrentCellAddress.Y) &&
                              (ColumnIndex == DataGridView.CurrentCellAddress.X);

            // Is this cell the same as the one with the mouse inside it
            Point mouseEnteredCellAddress = MouseEnteredCellAddressInternal;
            var mouseCell = (rowIndex == mouseEnteredCellAddress.Y) &&
                            (ColumnIndex == mouseEnteredCellAddress.X);

            // Snoop tracking and pressed status from the base class implementation
            var tracking = mouseCell && MouseInContentBoundsInternal;
            var pressed = currentCell && ((ButtonStateInternal & ButtonState.Pushed) == ButtonState.Pushed);

            // Update the button state to reflect the tracking/pressed values
            if (base.ReadOnly)
            {
                _viewButton.ElementState = PaletteState.Disabled;
            }
            else if (pressed)
            {
                _viewButton.ElementState = PaletteState.Pressed;
            }
            else if (tracking)
            {
                _viewButton.ElementState = PaletteState.Tracking;
            }
            else
            {
                _viewButton.ElementState = PaletteState.Normal;
            }

            // Update the display text
            if ((kDGV!.Columns[ColumnIndex] is KryptonDataGridViewButtonColumn { UseColumnTextForButtonValue: true } col) && !kDGV.Rows[rowIndex].IsNewRow)
            {
                _shortTextValue!.ShortText = col.Text;
            }
            else if (!string.IsNullOrEmpty(FormattedValue?.ToString()))
            {
                _shortTextValue!.ShortText = FormattedValue!.ToString();
            }
            else
            {
                _shortTextValue!.ShortText = string.Empty;
            }

            // Position the button element inside the available cell area
            using var layoutContext = new ViewLayoutContext(kDGV, kDGV.Renderer!);
            // Define the available area for layout
            layoutContext.DisplayRectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);

            // Get the ideal size of the button
            Size buttonSize = _viewButton.GetPreferredSize(layoutContext);

            // Add on the requested cell padding (plus add 1 to counter the -1 that occurs
            // in the painting routine to prevent drawing over the bottom right border)
            buttonSize.Width += cellStyle.Padding.Horizontal + 1;
            buttonSize.Height += cellStyle.Padding.Vertical + 1;

            return buttonSize;
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
                // Create the view elements and palette structure
                CreateViewAndPalettes(kDgv);

                // Cache the starting cell bounds
                Rectangle startBounds = cellBounds;

                // Is this cell the currently active cell
                var currentCell = (rowIndex == DataGridView.CurrentCellAddress.Y) &&
                                  (ColumnIndex == DataGridView.CurrentCellAddress.X);

                // Is this cell the same as the one with the mouse inside it
                Point mouseEnteredCellAddress = MouseEnteredCellAddressInternal;
                var mouseCell = (rowIndex == mouseEnteredCellAddress.Y) &&
                                (ColumnIndex == mouseEnteredCellAddress.X);

                // Snoop tracking and pressed status from the base class implementation
                var tracking = mouseCell && MouseInContentBoundsInternal;
                var pressed = currentCell && ((ButtonStateInternal & ButtonState.Pushed) == ButtonState.Pushed);

                // Update the button state to reflect the tracking/pressed values
                if (base.ReadOnly)
                {
                    _viewButton.ElementState = PaletteState.Disabled;
                }
                else if (pressed)
                {
                    _viewButton.ElementState = PaletteState.Pressed;
                }
                else if (tracking)
                {
                    _viewButton.ElementState = PaletteState.Tracking;
                }
                else
                {
                    _viewButton.ElementState = PaletteState.Normal;
                }

                // Update the display text
                if ((kDgv.Columns[ColumnIndex] is KryptonDataGridViewButtonColumn
                    {
                        UseColumnTextForButtonValue: true
                    } col) && !kDgv.Rows[rowIndex].IsNewRow)
                {
                    _shortTextValue!.ShortText = col.Text;
                }
                else if (!string.IsNullOrEmpty(FormattedValue?.ToString()))
                {
                    _shortTextValue!.ShortText = FormattedValue!.ToString();
                }
                else
                {
                    _shortTextValue!.ShortText = string.Empty;
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
                    // Define the available area for layout
                    layoutContext.DisplayRectangle = cellBounds;

                    // Perform actual layout inside that area
                    _viewButton.Layout(layoutContext);
                }
                            
                // Ask the element to draw now
                _viewButton.Render(renderContext);

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

    #region Private
    private void CreateViewAndPalettes(KryptonDataGridView kDGV)
    {
        // Create the view element when first needed
        if (_viewButton == null)
        {
            // Create helper object to get all values from the DGV redirector
            _palette = new PaletteTripleToPalette(kDGV.Redirector,
                PaletteBackStyle.ButtonStandalone,
                PaletteBorderStyle.ButtonStandalone,
                PaletteContentStyle.ButtonStandalone);

            // Provider of values for the button element
            _shortTextValue = new ShortTextValue();

            // Create view element for drawing the actual button
            _viewButton = new ViewDrawButton(_palette, _palette, _palette, 
                _palette, _palette, _palette, _palette,
                new PaletteMetricRedirect(kDGV.Redirector),
                _shortTextValue, VisualOrientation.Top, false);
        }

        // Update with latest defined style
        _palette.SetStyles(_buttonStyle);
    }

    internal ButtonStyle ButtonStyleInternal
    {
        set
        {
            if (!_styleSet)
            {
                _buttonStyle = value;
            }
        }
    }

    private ButtonState ButtonStateInternal
    {
        get
        {
            // Only need to cache reflection info the first time around
            if (_piButtonState == null)
            {
                // Cache access to the internal get property 'ButtonState'
                _piButtonState = typeof(DataGridViewButtonCell).GetProperty(nameof(ButtonState), BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.GetField);
            }

            // Grab the internal property implemented by base class
            return _piButtonState != null ? (ButtonState)(_piButtonState.GetValue(this, null) ?? ButtonState.Normal) : ButtonState.Normal;
        }
    }

    private bool MouseInContentBoundsInternal
    {
        get
        {
            // Only need to cache reflection info the first time it is needed
            if (_fiMouseInContentBounds == null)
            {
                // Cache field info about the internal 'mouseInContentBounds' instance
                _fiMouseInContentBounds = typeof(DataGridViewButtonCell).GetField(@"mouseInContentBounds", BindingFlags.Static |
                    BindingFlags.NonPublic |
                    BindingFlags.GetField);
                if (_fiMouseInContentBounds == null)
                {
                    // https://github.com/dotnet/winforms/commit/27e010d21c78457113f5be67eeea842499ab5f74#diff-bb5ad249080118c559367691ad27b9a93f8d5324b814f65113ff2e4bd15c9b39
                    // This was changed in netCore8 P1 but when running netcore7 it still wants this new name ??
                    _fiMouseInContentBounds = typeof(DataGridViewButtonCell).GetField(@"s_mouseInContentBounds", BindingFlags.Static |
                        BindingFlags.NonPublic |
                        BindingFlags.GetField);
                }
            }

            // Grab the internal property implemented by base class
            return _fiMouseInContentBounds != null && (bool)(_fiMouseInContentBounds.GetValue(this) ?? false);
        }
    }

    private Point MouseEnteredCellAddressInternal
    {
        get
        {
            // Only need to cache reflection info the first time around
            if (_piMouseEnteredCellAddress == null)
            {
                // Cache access to the internal get property 'MouseEnteredCellAddress'
                _piMouseEnteredCellAddress = typeof(DataGridView).GetProperty(@"MouseEnteredCellAddress", BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.GetField);
            }

            // Grab the internal property implemented by base class
            // ReSharper disable RedundantBaseQualifier
            return _piMouseEnteredCellAddress != null ? (Point)(_piMouseEnteredCellAddress.GetValue(base.DataGridView, null) ?? Point.Empty) : Point.Empty;
            // ReSharper restore RedundantBaseQualifier
        }
    }
    #endregion
}