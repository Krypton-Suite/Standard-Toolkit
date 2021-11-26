#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays a check box user interface (UI) to use in a DataGridView control.
    /// </summary>
    public class KryptonDataGridViewCheckBoxCell : DataGridViewCheckBoxCell
    {
        #region Static Fields
        private static PropertyInfo _piButtonState;
        private static PropertyInfo _piMouseEnteredCellAddress;
        private static FieldInfo _fiMouseInContentBounds;
        #endregion

        #region Instance Fields
        private Rectangle _contentBounds;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxCell.
        /// </summary>
        public KryptonDataGridViewCheckBoxCell()
            : this(false)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxCell.
        /// </summary>
        /// <param name="threeState">Enable binary or ternary operation.</param>
        public KryptonDataGridViewCheckBoxCell(bool threeState)
            : base(threeState)
        {
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
                KryptonDataGridView kDGV = (KryptonDataGridView)DataGridView;

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

                // Find out the requested size of the check box drawing
                using ViewLayoutContext viewContent = new(kDGV, kDGV.Renderer);
                Size checkBoxSize = kDGV.Renderer.RenderGlyph.GetCheckBoxPreferredSize(viewContent,
                    kDGV.Redirector,
                    kDGV.Enabled,
                    CheckState.Unchecked,
                    tracking,
                    pressed);

                // Add on the requested cell padding (plus add 1 to counter the -1 that occurs
                // in the painting routine to prevent drawing over the bottom right border)
                checkBoxSize.Width += cellStyle.Padding.Horizontal + 1;
                checkBoxSize.Height += cellStyle.Padding.Vertical + 1;

                return checkBoxSize;
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
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (DataGridView is KryptonDataGridView kDgv)
            {
                // Should we draw the content foreground?
                if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
                {
                    CheckState checkState = CheckState.Unchecked;

                    if (formattedValue is CheckState state)
                    {
                        checkState = state;
                    }
                    else if (formattedValue is bool b)
                    {
                        if (b)
                        {
                            checkState = CheckState.Checked;
                        }
                        else
                        {
                            checkState = CheckState.Unchecked;
                        }
                    }

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

                    using RenderContext renderContext = new(kDgv, graphics, cellBounds, kDgv.Renderer);
                    Size checkBoxSize;

                    // Find out the requested size of the check box drawing
                    using (ViewLayoutContext viewContent = new(kDgv, kDgv.Renderer))
                    {
                        checkBoxSize = renderContext.Renderer.RenderGlyph.GetCheckBoxPreferredSize(viewContent, 
                            kDgv.Redirector,
                            kDgv.Enabled && !base.ReadOnly,
                            checkState,
                            tracking,
                            pressed);
                    }
                    // Remember the original cell bounds
                    Rectangle startBounds = cellBounds;

                    // Prevent check box overlapping the bottom/right border
                    cellBounds.Width--;
                    cellBounds.Height--;

                    // Adjust the horizontal alignment
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.NotSet:
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.BottomCenter:
                            cellBounds.X += (cellBounds.Width - checkBoxSize.Width) / 2;
                            break;
                        case DataGridViewContentAlignment.TopRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.BottomRight:
                            cellBounds.X = cellBounds.Right - checkBoxSize.Width;
                            break;
                    }

                    // Adjust the vertical alignment
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.NotSet:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.MiddleRight:
                            cellBounds.Y += (cellBounds.Height - checkBoxSize.Height) / 2;
                            break;
                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.BottomRight:
                            cellBounds.Y = cellBounds.Bottom - checkBoxSize.Height;
                            break;
                    }

                    // Make the cell the same size as the check box itself
                    cellBounds.Width = checkBoxSize.Width;
                    cellBounds.Height = checkBoxSize.Height;

                    // Remember the current drawing bounds
                    _contentBounds = new Rectangle(cellBounds.X - startBounds.X,
                        cellBounds.Y - startBounds.Y,
                        cellBounds.Width, cellBounds.Height);

                    // Perform actual drawing of the check box
                    renderContext.Renderer.RenderGlyph.DrawCheckBox(renderContext,
                        cellBounds,
                        kDgv.Redirector,
                        kDgv.Enabled && !base.ReadOnly,
                        checkState,
                        tracking,
                        pressed);
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
        private ButtonState ButtonStateInternal
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piButtonState == null)
                {
                    // Cache access to the internal get property 'ButtonState'
                    _piButtonState = typeof(DataGridViewCheckBoxCell).GetProperty("ButtonState", BindingFlags.Instance |
                                                                                                 BindingFlags.NonPublic |
                                                                                                 BindingFlags.GetField);

                }

                // Grab the internal property implemented by base class
                return (ButtonState)_piButtonState.GetValue(this, null);
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
                    _fiMouseInContentBounds = typeof(DataGridViewCheckBoxCell).GetField("mouseInContentBounds", BindingFlags.Static |
                                                                                                                BindingFlags.NonPublic |
                                                                                                                BindingFlags.GetField);
                }

                // Grab the internal property implemented by base class
                return (bool)_fiMouseInContentBounds.GetValue(this);
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
                    _piMouseEnteredCellAddress = typeof(DataGridView).GetProperty("MouseEnteredCellAddress", BindingFlags.Instance |
                                                                                                             BindingFlags.NonPublic |
                                                                                                             BindingFlags.GetField);

                }

                // Grab the internal property implemented by base class
                // ReSharper disable RedundantBaseQualifier
                return (Point)_piMouseEnteredCellAddress.GetValue(base.DataGridView, null);
                // ReSharper restore RedundantBaseQualifier
            }
        }
        #endregion
    }
}