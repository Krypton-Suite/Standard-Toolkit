#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// OutlookGridRow - subclasses the DataGridView's DataGridViewRow class
    /// In order to support grouping with the same look and feel as Outlook, the behavior
    /// of the DataGridViewRow is overridden by the OutlookGridRow.
    /// The OutlookGridRow has 2 main additional properties: the Group it belongs to and
    /// a the IsRowGroup flag that indicates whether the OutlookGridRow object behaves like
    /// a regular row (with data) or should behave like a Group row.
    /// </summary>
    public class OutlookGridRow : DataGridViewRow
    {
        #region "Variables"

        private bool _isGroupRow;
        private bool _isSummaryRow;
        private IOutlookGridGroup? _group;
        private bool _collapsed; //For TreeNode
        private OutlookGridRowNodeCollection _nodeCollection; //For TreeNode
        private int _nodeLevel; //For TreeNode
        private OutlookGridRow? _parentNode; //for TreeNode
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the group to the row belongs to.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IOutlookGridGroup? Group
        {
            get => _group;
            set => _group = value;
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is a group row.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a group row; otherwise, <c>false</c>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGroupRow
        {
            get => _isGroupRow;
            set => _isGroupRow = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this row is an aggregation summary row.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSummaryRow
        {
            get => _isSummaryRow;
            set => _isSummaryRow = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OutlookGridRow"/> is collapsed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if collapsed; otherwise, <c>false</c>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Collapsed { get => _collapsed; set => _collapsed = value; }

        /// <summary>
        /// Gets or sets the nodes.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OutlookGridRowNodeCollection Nodes { get => _nodeCollection; set => _nodeCollection = value; }

        /// <summary>
        /// Gets a value indicating whether this instance is first sibling.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is first sibling; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFirstSibling => NodeIndex == 0;

        /// <summary>
        /// Gets a value indicating whether this instance is last sibling.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is last sibling; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastSibling
        {
            get
            {
                OutlookGridRow? parent = _parentNode;
                if (parent != null && parent.HasChildren)
                {
                    return NodeIndex == parent.Nodes.Count - 1;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has children; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasChildren => _nodeCollection.Count > 0;

        /// <summary>
        /// Gets or sets the node level.
        /// </summary>
        /// <value>
        /// The node level.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int NodeLevel { get => _nodeLevel; set => _nodeLevel = value; }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        /// <value>
        /// The parent node.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OutlookGridRow? ParentNode { get => _parentNode; set => _parentNode = value; }

        /// <summary>
        /// Gets the index of the node.
        /// </summary>
        /// <value>
        /// The index of the node.
        /// </value>
        public int NodeIndex
        {
            get
            {
                if (_parentNode != null)
                {
                    return _parentNode.Nodes.IndexOf(this);
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region "Constructors"

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OutlookGridRow()
            : this(null, false)
        {
            //nodeCollection = new OutlookGridRowNodeCollection(this);
            //NodeLevel = 0;
            //Collapsed = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="group">The group the row is associated to.</param>
        public OutlookGridRow(IOutlookGridGroup? group)
            : this(group, false)
        {
            //nodeCollection = new OutlookGridRowNodeCollection(this);
            //NodeLevel = 0;
            //Collapsed = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="group">The group the row is associated to.</param>
        /// <param name="isGroupRow">Determines if it is a group row.</param>
        public OutlookGridRow(IOutlookGridGroup? group, bool isGroupRow)
        {
            _group = group;
            _isGroupRow = isGroupRow;
            _nodeCollection = new(this);
            NodeLevel = 0;
            Collapsed = true;
        }

        #endregion

        #region "Overrides"

        /// <summary>
        /// Overrides the GetState method
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public override DataGridViewElementStates GetState(int rowIndex)
        {
            //yes its readable at least it was ;)
            if ((IsGroupRow && IsAParentCollapsed(_group, 0)) || (!IsGroupRow && _group != null && (_group.Collapsed || IsAParentCollapsed(_group, 0))) || (!IsGroupRow && IsAParentNodeOrGroupCollapsed(this, 0)))
            {
                return base.GetState(rowIndex) & DataGridViewElementStates.Selected;
            }
            //For the TreeGridView project if the selection mode is FullRow sub nodes that where collapsed disappear when parent collapse/expands
            //because for an unknown reason the state becomes None instead of at least visible.
            if (base.GetState(rowIndex) == DataGridViewElementStates.None)
            {
                return DataGridViewElementStates.Visible;
            }
            else
            {
                return base.GetState(rowIndex);
            }
        }

        /// <summary>
        /// Calculates the preferred height of the row, primarily used for automatic row sizing.
        /// </summary>
        /// <param name="rowIndex">The index of the row.</param>
        /// <param name="autoSizeRowMode">A <see cref="DataGridViewAutoSizeRowMode"/> value that specifies how the row's height is to be calculated.</param>
        /// <param name="fixedWidth">true to calculate the new height based on the current width; otherwise, false.</param>
        /// <returns>The preferred height of the row in pixels.</returns>
        /// <remarks>
        /// This method is overridden to provide custom height calculation for summary rows (<see cref="_isSummaryRow"/>).
        /// For summary rows, it iterates through all visible cells in the row, measures the required height
        /// for their formatted text (assuming multiline display with word wrapping), and returns the maximum
        /// height found among all cells, plus additional padding. This ensures that summary rows
        /// automatically adjust their height to accommodate wrapped text content.
        /// <para>
        /// For all other row types (group rows or regular data rows), this method defers to the
        /// base <see cref="DataGridViewRow.GetPreferredHeight"/> implementation, allowing the default
        /// DataGridView sizing behavior to apply.
        /// </para>
        /// <para>
        /// To enable this method to be called, the <see cref="DataGridView.AutoSizeRowsMode"/> property
        /// of the parent <see cref="KryptonOutlookGrid"/> must be set to a value that enables
        /// content-based automatic sizing (e.g., <see cref="DataGridViewAutoSizeRowsMode.AllCells"/>).
        /// Additionally, the <see cref="DataGridViewCellStyle.WrapMode"/> for the cells in summary rows
        /// should be set to <see cref="DataGridViewTriState.True"/> to allow text wrapping.
        /// </para>
        /// </remarks>
        public override int GetPreferredHeight(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
        {
            KryptonOutlookGrid grid = (KryptonOutlookGrid)this.DataGridView!;
            int topContentPadding = 6; // Space above the text/icon/image
            int bottomContentPadding = 4; // Space between text/icon/image bottom and the custom bottom line area

            if (grid == null) return base.GetPreferredHeight(rowIndex, autoSizeRowMode, fixedWidth);

            if (_isGroupRow)
            {
                // Determine the font
                PaletteState overallRowRenderingState = grid.SelectedRows.Contains(this) ? PaletteState.CheckedNormal : PaletteState.Normal;
                Font groupFont = grid.GridPalette?.GetContentShortTextFont(PaletteContentStyle.LabelBoldControl, overallRowRenderingState) ??
                                 new Font(grid.DefaultCellStyle.Font!, FontStyle.Bold);

                var textToMeasure = _group!.Text; // Primary group text

                if (_group.Collapsed)
                {
                    string summaryText = _group.SummaryText;
                    // If the main group text exists, add a newline before the summary text.
                    if (!string.IsNullOrEmpty(textToMeasure) && !string.IsNullOrEmpty(summaryText))
                    {
                        textToMeasure += Environment.NewLine;
                        textToMeasure += summaryText; // summaryText now contains the multi-line, indented summary.
                    }
                }

                // Calculate the precise measureWidth (horizontal space for text)
                int groupRowLevelIndentation = _group.Level * GlobalStaticValues.GroupLevelMultiplier;
                int imageOffsetUsed = (_group.GroupImage != null) ? GlobalStaticValues.GroupImageSide : 0;
                int fixedLeftTextPad = 18; // From Paint: offset for icon and initial text indent

                int totalVisibleColumnsWidth = grid.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                int measureWidth = totalVisibleColumnsWidth - (fixedLeftTextPad + imageOffsetUsed + groupRowLevelIndentation);

                // Ensure a positive width for measurement
                if (measureWidth <= 0)
                {
                    measureWidth = 50; // Fallback
                }

                // Determine TextFormatFlags (consistent with Paint)
                TextFormatFlags flags = TextFormatFlags.PreserveGraphicsClipping;
                if (_group.Collapsed)
                {
                    flags |= TextFormatFlags.WordBreak;
                    flags &= ~TextFormatFlags.SingleLine;
                }
                else
                {
                    flags |= TextFormatFlags.SingleLine;
                    flags |= TextFormatFlags.EndEllipsis;
                }

                // Measure the required text height
                Size textSize;
                using (Graphics tempGraphics = grid.CreateGraphics())
                {
                    textSize = TextRenderer.MeasureText(tempGraphics,
                                                        text: textToMeasure,
                                                        font: groupFont,
                                                        proposedSize: new Size(measureWidth, int.MaxValue), // Allow infinite height
                                                        flags: flags);
                }

                // Calculate total preferred height (Vertical components)
                int measuredTextHeight = textSize.Height;
                int iconHeight = 11; // From Paint
                int groupImageHeight = GlobalStaticValues.GroupImageSide; // From Paint

                // The height of the custom bottom line/area
                int customBottomLineAreaHeight = (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013) ?
                                                  GlobalStaticValues.Office2013GroupRowHeight :
                                                  2; // From Paint

                // Max height required by the visual content (text, icon, image)
                int maxVisualContentHeight = Math.Max(measuredTextHeight, Math.Max(iconHeight, groupImageHeight));

                // Total calculated height:
                int calculatedHeight = topContentPadding + maxVisualContentHeight + bottomContentPadding + customBottomLineAreaHeight;

                // Ensure a minimum height (e.g., the default row template height)
                // This prevents the row from becoming too small if content is minimal.
                return Math.Max(calculatedHeight, grid.RowTemplate.Height);
            }
            else if (_isSummaryRow)
            {
                int maxContentHeight = 0;
                var boldFont = grid.GridPalette?.GetContentShortTextFont(PaletteContentStyle.LabelBoldControl, PaletteState.Normal);
                boldFont ??= new Font(grid.DefaultCellStyle.Font!, FontStyle.Bold);
                TextFormatFlags summaryFlags = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.WordBreak;
                int cellPadding = 3;

                foreach (DataGridViewCell cell in this.Cells)
                {
                    if (cell.Visible && cell.OwningColumn != null && cell.OwningColumn.Visible)
                    {
                        string cellText = cell.FormattedValue?.ToString() ?? string.Empty;
                        int columnWidth = cell.OwningColumn.Width;
                        int availableTextWidth = columnWidth - (2 * cellPadding);
                        if (availableTextWidth <= 0) continue;

                        Size textSize;
                        using (Graphics tempGraphics = grid.CreateGraphics())
                        {
                            textSize = TextRenderer.MeasureText(tempGraphics,
                                                             text: cellText,
                                                             font: boldFont,
                                                             proposedSize: new Size(availableTextWidth, int.MaxValue),
                                                             flags: summaryFlags);
                        }
                        maxContentHeight = Math.Max(maxContentHeight, textSize.Height + (2 * cellPadding));
                    }
                }

                int extraRowPadding = 0;
                int calculatedSummaryRowHeight = maxContentHeight + extraRowPadding + topContentPadding + bottomContentPadding - (2 * cellPadding);
                return Math.Max(grid.RowTemplate.Height, calculatedSummaryRowHeight);
            }
            else // Normal data row
            {
                return base.GetPreferredHeight(rowIndex, autoSizeRowMode, fixedWidth);
            }
        }

        /// <summary>
        /// the main difference with a Group row and a regular row is the way it is painted on the control.
        /// the Paint method is therefore overridden and specifies how the Group row is painted.
        /// Note: this method is not implemented optimally. It is merely used for demonstration purposes
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="rowBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="rowState"></param>
        /// <param name="isFirstDisplayedRow"></param>
        /// <param name="isLastVisibleRow"></param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
        {
            KryptonOutlookGrid grid = (KryptonOutlookGrid)DataGridView!;
            if (_isGroupRow)
            {
                // --- Define consistent vertical padding and offsets for drawing ---
                // (These are primarily for TEXT alignment, icon will be centered)
                int topContentPadding = 6;  // Padding from rowBounds.Top to top of TEXT content
                int bottomContentPadding = 4; // Padding from bottom of TEXT content to top of custom bottom line
                int customBottomLineAreaHeight;

                if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                {
                    customBottomLineAreaHeight = GlobalStaticValues.Office2013GroupRowHeight;
                }
                else
                {
                    customBottomLineAreaHeight = 2; // Default for other renderers
                }

                // --- Horizontal Calculations ---
                int rowHeadersWidth = grid!.RowHeadersVisible ? grid.RowHeadersWidth : 0;
                int groupLevelIndentation = _group!.Level * GlobalStaticValues.GroupLevelMultiplier;
                int totalVisibleColumnsWidth = grid.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);

                Rectangle contentBounds = new(rowBounds.Left + rowHeadersWidth, rowBounds.Top, totalVisibleColumnsWidth, rowBounds.Height);
                contentBounds.X -= grid.HorizontalScrollingOffset;
                // --- Determine Selection State and Corresponding PaletteState ---
                bool isSelected = (rowState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
                if (!isSelected && grid.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
                {
                    isSelected = grid.CurrentCell?.RowIndex == rowIndex;
                }

                PaletteState overallRowRenderingState = isSelected ? PaletteState.CheckedNormal : PaletteState.Normal;
                // --- RowHeader painting ---
                if (grid.RowHeadersVisible)
                {
                    Rectangle rowHeaderPaintArea = new(rowBounds.Left, rowBounds.Top, rowHeadersWidth, rowBounds.Height);
                    rowHeaderPaintArea.X -= grid.HorizontalScrollingOffset;

                    PaletteState rowHeaderRenderingState = isSelected ? PaletteState.CheckedNormal : PaletteState.Normal;
                    IPaletteBack rowHeaderPaletteBack = isSelected ? grid.StateSelected.HeaderRow.Back : grid.StateNormal.HeaderRow.Back;
                    IPaletteBorder rowHeaderPaletteBorder = isSelected ? grid.StateSelected.HeaderRow.Border : grid.StateNormal.HeaderRow.Border;

                    using (RenderContext rhRenderContext = new(grid, graphics, rowHeaderPaintArea, grid.Renderer!))
                    {
                        using (GraphicsPath rhPath = grid.Renderer!.RenderStandardBorder.GetBackPath(rhRenderContext, rowHeaderPaintArea, rowHeaderPaletteBorder, VisualOrientation.Top, rowHeaderRenderingState))
                        {
                            grid.Renderer.RenderStandardBack.DrawBack(rhRenderContext, rowHeaderPaintArea, rhPath, rowHeaderPaletteBack, VisualOrientation.Top, rowHeaderRenderingState, null);
                        }
                        grid.Renderer.RenderStandardBorder.DrawBorder(rhRenderContext, rowHeaderPaintArea, rowHeaderPaletteBorder, VisualOrientation.Top, rowHeaderRenderingState);
                    }
                }

                // --- Paint the Group Row's Content Area (Background and Border) ---
                IPaletteBack contentPaletteBack = isSelected ? grid.StateSelected.DataCell.Back : grid.StateNormal.DataCell.Back;
                IPaletteBorder contentPaletteBorder = isSelected ? grid.StateSelected.DataCell.Border : grid.StateNormal.DataCell.Border;
                PaletteState contentRenderingState = overallRowRenderingState;

                if (!isSelected && grid.PreviousSelectedGroupRow == rowIndex && KryptonManager.CurrentGlobalPalette.GetRenderer() != KryptonManager.RenderOffice2013)
                {
                    contentRenderingState = PaletteState.CheckedNormal;
                }

                using (RenderContext renderContext = new(grid, graphics, contentBounds, grid.Renderer!))
                {
                    using (GraphicsPath path = grid.Renderer!.RenderStandardBorder.GetBackPath(renderContext, contentBounds, contentPaletteBorder, VisualOrientation.Top, contentRenderingState))
                    {
                        grid.Renderer.RenderStandardBack.DrawBack(renderContext, contentBounds, path, contentPaletteBack, VisualOrientation.Top, contentRenderingState, null);
                    }
                    grid.Renderer.RenderStandardBorder.DrawBorder(renderContext, contentBounds, contentPaletteBorder, VisualOrientation.Top, contentRenderingState);
                }

                // --- Draw the custom bottom line ---
                if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010)
                {
                    using (Pen focusPen = new(Color.Gray))
                    {
                        focusPen.DashStyle = DashStyle.Dash;
                        graphics.DrawLine(focusPen, contentBounds.Left, rowBounds.Bottom - 1, contentBounds.Right + 1, rowBounds.Bottom - 1);
                    }
                }
                else if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                {
                    using (SolidBrush br = new(Color.FromArgb(225, 225, 225)))
                    {
                        graphics.FillRectangle(br, contentBounds.Left, rowBounds.Bottom - customBottomLineAreaHeight, contentBounds.Width + 1, customBottomLineAreaHeight - 1);
                    }
                }
                else
                {
                    using (SolidBrush br = new(contentPaletteBorder.GetBorderColor1(contentRenderingState)))
                    {
                        graphics.FillRectangle(br, contentBounds.Left, rowBounds.Bottom - customBottomLineAreaHeight, contentBounds.Width + 1, customBottomLineAreaHeight);
                    }
                }

                // --- Calculate the Y-coordinate for vertically centered icon and image ---
                int iconHeight = 11; // Standard icon height
                int centeredIconY = rowBounds.Y + (rowBounds.Height / 2) - (iconHeight / 2);

                // --- Calculate the common Y-coordinate for TEXT (still top-aligned for wrapping) ---
                // Text will remain top-aligned as per your working height calculation,
                // unless you want to vertically center text too (which is complex with wrapping).
                int textDrawingTopY = rowBounds.Top + topContentPadding;

                // --- Set the icon ---
                int iconX = contentBounds.Left + 4 + groupLevelIndentation;
                int iconY = centeredIconY; // Vertically centered

                if (_group.Collapsed)
                {
                    if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                    {
                        graphics.DrawImage(GenericImageResources.CollapseIcon2010, iconX, iconY, 11, 11);
                    }
                    else
                    {
                        graphics.DrawImage(GenericImageResources.ExpandIcon, iconX, iconY, 11, 11);
                    }
                }
                else
                {
                    if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                    {
                        graphics.DrawImage(GenericImageResources.ExpandIcon2010, iconX, iconY, 11, 11);
                    }
                    else
                    {
                        graphics.DrawImage(GenericImageResources.CollapseIcon, iconX, iconY, 11, 11);
                    }
                }

                // --- Draw image group ---
                int imageOffset = 0;
                if (_group.GroupImage != null)
                {
                    int groupImageHeight = GlobalStaticValues.GroupImageSide; // Height of the group image
                    int imageX = contentBounds.Left + GlobalStaticValues.ImageOffsetWidth + groupLevelIndentation;
                    int imageY = rowBounds.Y + (rowBounds.Height / 2) - (groupImageHeight / 2); // Vertically centered

                    graphics.DrawImage(_group.GroupImage, imageX, imageY, GlobalStaticValues.GroupImageSide, GlobalStaticValues.GroupImageSide);
                    imageOffset = GlobalStaticValues.GroupImageSide;
                }

                // --- Draw text, using the current grid font ---
                int offsetText = contentBounds.Left + 18 + imageOffset + groupLevelIndentation; // This is the X-start for text
                int textRectWidth = contentBounds.Right - offsetText;

                Color groupTextForeColor;
                if (isSelected)
                {
                    groupTextForeColor = grid.DefaultCellStyle.SelectionForeColor;
                }
                else
                {
                    groupTextForeColor = grid.GridPalette!.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, overallRowRenderingState);
                }

                Font groupFont = grid.GridPalette?.GetContentShortTextFont(PaletteContentStyle.LabelBoldControl, overallRowRenderingState) ??
                                 new Font(grid.DefaultCellStyle.Font!, FontStyle.Bold);

                Rectangle textDrawingRect = new(offsetText, textDrawingTopY, textRectWidth,
                    rowBounds.Height - (textDrawingTopY - rowBounds.Top) - customBottomLineAreaHeight - bottomContentPadding
                );

                if (textDrawingRect.Width <= 0) textDrawingRect.Width = 1;
                if (textDrawingRect.Height <= 0) textDrawingRect.Height = 1;

                var text = _group.Text;

                if (_group.Collapsed)
                {
                    string summaryText = _group.SummaryText;
                    // If the main group text exists, add a newline before the summary text.
                    // This creates a visual separation between the group name and its summary details.
                    if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(summaryText))
                    {
                        text += Environment.NewLine;
                        text += summaryText;
                    }
                }

                TextFormatFlags flags = TextFormatFlags.PreserveGraphicsClipping;
                if (_group.Collapsed)
                {
                    flags |= TextFormatFlags.WordBreak;
                    flags &= ~TextFormatFlags.SingleLine;
                }
                else
                {
                    flags |= TextFormatFlags.SingleLine;
                    flags |= TextFormatFlags.EndEllipsis;
                }

                TextRenderer.DrawText(graphics, text, groupFont, textDrawingRect, groupTextForeColor, flags);

                // This line is necessary to trigger GetPreferredHeight updates
                grid.AutoResizeRow(rowIndex, DataGridViewAutoSizeRowMode.AllCells);
                if (grid.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
                {
                    grid.InvalidateRow(rowIndex);
                }
            }
            else // Not a group row
            {
                // when row change using arrow keys.
                if (grid.PreviousSelectedGroupRow > -1 && grid.PreviousSelectedGroupRow != rowIndex)
                {
                    grid.InvalidateRow(grid.PreviousSelectedGroupRow);
                }
                base.Paint(graphics, clipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow);
            }
        }

        /// <summary>
        /// Paints the cells.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="clipBounds">The clip bounds.</param>
        /// <param name="rowBounds">The row bounds.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="rowState">State of the row.</param>
        /// <param name="isFirstDisplayedRow">if set to <c>true</c> [is first displayed row].</param>
        /// <param name="isLastVisibleRow">if set to <c>true</c> [is last visible row].</param>
        /// <param name="paintParts">The paint parts.</param>
        /// <remarks>Will not execute if it is a group row.)</remarks>
        protected override void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
        {
            if (_isGroupRow)
            {
                return;
            }

            if (_isSummaryRow)
            {
                KryptonOutlookGrid grid = (KryptonOutlookGrid)DataGridView!;
                var boldFont = grid.GridPalette?.GetContentShortTextFont(PaletteContentStyle.LabelBoldControl, PaletteState.Normal);
                boldFont ??= new Font(grid.DefaultCellStyle.Font!, FontStyle.Bold);
                int cellPadding = 3;
                foreach (DataGridViewCell cell in this.Cells)
                {
                    bool isSelected = cell.Selected;
                    PaletteState overallCellRenderingState = isSelected ? PaletteState.CheckedNormal : PaletteState.Normal;
                    Rectangle cellBounds = grid.GetCellDisplayRectangle(cell.ColumnIndex, rowIndex, false);

                    if (cellBounds.IntersectsWith(clipBounds))
                    {
                        DataGridViewCellStyle cellStyle = cell.InheritedStyle;

                        IPaletteBack cellBackPalette;
                        PaletteBorder cellBorderPalette;
                        Color currentCellTextColor;

                        if (isSelected)
                        {
                            cellBackPalette = grid.StateSelected.DataCell.Back;
                            cellBorderPalette = grid.StateSelected.DataCell.Border;
                            currentCellTextColor = grid.DefaultCellStyle.SelectionForeColor;
                        }
                        else
                        {
                            cellBackPalette = grid.StateNormal.DataCell.Back;
                            cellBorderPalette = grid.StateNormal.DataCell.Border;
                            currentCellTextColor = grid.GridPalette!.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);
                        }

                        using (RenderContext cellRenderContext = new(grid, graphics, cellBounds, grid.Renderer!))
                        {
                            using (GraphicsPath cellPath = grid.Renderer!.RenderStandardBorder.GetBackPath(cellRenderContext, cellBounds, cellBorderPalette, VisualOrientation.Top, overallCellRenderingState))
                            {
                                grid.Renderer.RenderStandardBack.DrawBack(cellRenderContext, cellBounds, cellPath, cellBackPalette, VisualOrientation.Top, overallCellRenderingState, null);
                            }
                        }

                        TextFormatFlags flags = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.WordBreak;
                        flags |= cellStyle.Alignment switch
                        {
                            DataGridViewContentAlignment.BottomLeft => TextFormatFlags.Bottom | TextFormatFlags.Left,
                            DataGridViewContentAlignment.BottomCenter => TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter,
                            DataGridViewContentAlignment.BottomRight => TextFormatFlags.Bottom | TextFormatFlags.Right,
                            DataGridViewContentAlignment.MiddleLeft => TextFormatFlags.VerticalCenter | TextFormatFlags.Left,
                            DataGridViewContentAlignment.MiddleCenter => TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                            DataGridViewContentAlignment.MiddleRight => TextFormatFlags.VerticalCenter | TextFormatFlags.Right,
                            DataGridViewContentAlignment.TopLeft => TextFormatFlags.Top | TextFormatFlags.Left,
                            DataGridViewContentAlignment.TopCenter => TextFormatFlags.Top | TextFormatFlags.HorizontalCenter,
                            DataGridViewContentAlignment.TopRight => TextFormatFlags.Top | TextFormatFlags.Right,
                            _ => TextFormatFlags.VerticalCenter | TextFormatFlags.Left,
                        };
                        string cellText = cell.FormattedValue?.ToString() ?? string.Empty;
                        if (cell.ValueType == typeof(bool) || cell.ValueType == typeof(Image))
                            cellText = string.Empty;

                        int absoluteCellX = 0;
                        for (int i = 0; i < cell.ColumnIndex; i++)
                        {
                            if (grid.Columns[i].Visible) // Only include visible columns
                            {
                                absoluteCellX += grid.Columns[i].Width;
                            }
                        }
                        // Add the RowHeadersWidth if visible, as cells start after row headers.
                        if (grid.RowHeadersVisible)
                        {
                            absoluteCellX += grid.RowHeadersWidth;
                        }

                        int columnWidth = cell.OwningColumn!.Width;
                        Rectangle textRect = new(
                            absoluteCellX - grid.HorizontalScrollingOffset + cellPadding,   // <--- Change for X
                            cellBounds.Y,                                                   // Y is relative to visible row top
                            columnWidth - (2 * cellPadding),                                // Width is full column width for layout
                            cellBounds.Height                                               // Height is visible cell height
                        );

                        TextRenderer.DrawText(graphics, cellText, boldFont, textRect, currentCellTextColor, flags);

                        // --- PAINT ALL SINGLE CELL BORDERS ---
                        int borderThickness = 1; // You can make this 1, 2, or whatever you prefer
                        Color borderColor = cellBorderPalette.GetBorderColor1(overallCellRenderingState);

                        using (Pen cellBorderPen = new(borderColor, borderThickness))
                        {
                            // Right Border
                            graphics.DrawLine(cellBorderPen, cellBounds.Right - borderThickness, cellBounds.Top, cellBounds.Right - borderThickness, cellBounds.Bottom);
                            // Left Border
                            graphics.DrawLine(cellBorderPen, cellBounds.Left, cellBounds.Top, cellBounds.Left, cellBounds.Bottom);
                            // Top Border
                            graphics.DrawLine(cellBorderPen, cellBounds.Left, cellBounds.Top, cellBounds.Right, cellBounds.Top);
                            // Bottom Border
                            graphics.DrawLine(cellBorderPen, cellBounds.Left, cellBounds.Bottom - borderThickness, cellBounds.Right, cellBounds.Bottom - borderThickness);
                        }

                        // --- PAINT ALL DOUBLE CELL BORDERS ---
                        // Get the primary border color from the palette
                        /*Color primaryBorderColor = cellBorderPalette.GetBorderColor1(overallCellRenderingState);

                        // Define colors for the double border. You can use different shades or entirely different colors.
                        // You might want to make these static or define them once if they don't change per cell.
                        Color outerBorderColor = primaryBorderColor;
                        Color innerBorderColor = Color.LightGray; // Or a slightly darker/lighter shade of primaryBorderColor

                        // Adjust these based on your design preference
                        int outerBorderThickness = 1;
                        int innerBorderThickness = 1;
                        int gapBetweenBorders = 1; // Gap between the two lines

                        // Reusable Pen objects for efficiency (create outside loop if painting many cells)
                        // However, for clarity in this example, they are inside.
                        using (Pen outerPen = new(outerBorderColor, outerBorderThickness))
                        using (Pen innerPen = new(innerBorderColor, innerBorderThickness))
                        {
                            // --- Right Border ---
                            int outerRightX = cellBounds.Right - outerBorderThickness;
                            graphics.DrawLine(outerPen, outerRightX, cellBounds.Top, outerRightX, cellBounds.Bottom);

                            int innerRightX = cellBounds.Right - outerBorderThickness - gapBetweenBorders - innerBorderThickness;
                            graphics.DrawLine(innerPen, innerRightX, cellBounds.Top, innerRightX, cellBounds.Bottom);

                            // --- Left Border ---
                            int outerLeftX = cellBounds.Left;
                            graphics.DrawLine(outerPen, outerLeftX, cellBounds.Top, outerLeftX, cellBounds.Bottom);

                            int innerLeftX = cellBounds.Left + outerBorderThickness + gapBetweenBorders;
                            graphics.DrawLine(innerPen, innerLeftX, cellBounds.Top, innerLeftX, cellBounds.Bottom);

                            // --- Top Border ---
                            int outerTopY = cellBounds.Top;
                            graphics.DrawLine(outerPen, cellBounds.Left, outerTopY, cellBounds.Right, outerTopY);

                            int innerTopY = cellBounds.Top + outerBorderThickness + gapBetweenBorders;
                            graphics.DrawLine(innerPen, cellBounds.Left, innerTopY, cellBounds.Right, innerTopY);

                            // --- Bottom Border ---
                            int outerBottomY = cellBounds.Bottom - outerBorderThickness;
                            graphics.DrawLine(outerPen, cellBounds.Left, outerBottomY, cellBounds.Right, outerBottomY);

                            int innerBottomY = cellBounds.Bottom - outerBorderThickness - gapBetweenBorders - innerBorderThickness;
                            graphics.DrawLine(innerPen, cellBounds.Left, innerBottomY, cellBounds.Right, innerBottomY);
                        }*/

                    }
                }
                // This line is necessary to trigger GetPreferredHeight updates
                grid.AutoResizeRow(rowIndex, DataGridViewAutoSizeRowMode.AllCells); // Manually resize
            }
            else // It's a regular data row
            {
                base.PaintCells(graphics, clipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string res = "";
            try
            {
                res += "OutlookGridRow ";
                foreach (DataGridViewCell c in Cells)
                {
                    if (c.Value != null)
                    {
                        res += c.Value.ToString();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            return res;
        }

        #endregion

        #region "Public methods"

        /// <summary>
        /// Gets if the row has one parent that is collapsed
        /// </summary>
        /// <param name="gr">The group to look at.</param>
        /// <param name="i">Fill 0 to first this method (used for recursive).</param>
        /// <returns>True or false.</returns>
        public bool IsAParentCollapsed(IOutlookGridGroup? gr, int i)
        {
            i++;
            if (gr?.ParentGroup != null)
            {
                //if it is not the original group but it is one parent and if it is collapsed just stop here
                //no need to look further to the parents (one of the parents can be expanded...)
                //if (i > 1 && gr.Collapsed)
                if (gr.ParentGroup.Collapsed)
                {
                    return true;
                }
                else
                {
                    return IsAParentCollapsed(gr.ParentGroup, i);
                }
            }
            else
            {
                return i switch
                {
                    //if 1 that means there is no parent
                    1 => false,
                    _ => gr!.Collapsed
                };
            }
        }


        /// <summary>
        /// Determines if there is a parent node or a parent group collapsed.
        /// </summary>
        /// <param name="row">The specified row.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public bool IsAParentNodeOrGroupCollapsed(OutlookGridRow? row, int i)
        {
            i++;
            //Console.WriteLine(row.ToString());
            if (row?.ParentNode != null)
            {
                //if it is not the original group, but it is one parent and if it is collapsed just stop here
                //no need to look further to the parents (one of the parents can be expanded...)
                if (row.ParentNode.Collapsed)
                {
                    return true;
                }
                else
                {
                    return IsAParentNodeOrGroupCollapsed(row.ParentNode, i);
                }
            }
            else //no parent
            {
                if (i == 1) //if 1 that means there is no parent
                { return false; }
                else //return the final parent collapsed state
                {
                    if (row?._group != null)
                    {
                        return row.Collapsed || row._group.Collapsed || IsAParentCollapsed(row._group, 0);
                    }
                    else
                    {
                        return row!.Collapsed;
                    }

                }
            }
        }

        /// <summary>
        /// Expand the group the row belongs to.
        /// </summary>
        public void ExpandGroup()
        {
            SetGroupCollapse(false);
        }

        /// <summary>
        /// Collapse the group the row belongs to.
        /// </summary>
        public void CollapseGroup()
        {
            SetGroupCollapse(true);
        }

        internal void SetGroupCollapse(bool collapsed)
        {
            if (IsGroupRow)
            {
                Group!.Collapsed = collapsed;

                //this is a workaround to make the grid re-calculate it's contents and background bounds
                // so the background is updated correctly.
                // this will also invalidate the control, so it will redraw itself
                Visible = false;
                Visible = true;

                //When collapsing the first row still seeing it.
                if (Index < DataGridView!.FirstDisplayedScrollingRowIndex)
                {
                    DataGridView.FirstDisplayedScrollingRowIndex = Index;
                }
            }
        }

        internal void SetNodeCollapse(bool collapsed)
        {
            if (HasChildren)
            {
                Collapsed = collapsed;

                //this is a workaround to make the grid re-calculate it's contents and background bounds
                // so the background is updated correctly.
                // this will also invalidate the control, so it will redraw itself
                Visible = false;
                Visible = true;

                //When collapsing the first row still seeing it.
                if (Index < DataGridView!.FirstDisplayedScrollingRowIndex)
                {
                    DataGridView.FirstDisplayedScrollingRowIndex = Index;
                }
            }
        }

        /// <summary>
        /// Collapse Node (with events)
        /// </summary>
        public void Collapse()
        {
            ((KryptonOutlookGrid)DataGridView!).CollapseNode(this);
        }

        /// <summary>
        /// Expand Node (with events)
        /// </summary>
        public void Expand()
        {
            ((KryptonOutlookGrid)DataGridView!).ExpandNode(this);
        }

        #endregion

        #region "Private methods"

        /// <summary>
        /// this function checks if the user hit the expand (+) or collapse (-) icon.
        /// if it was hit it will return true
        /// </summary>
        /// <param name="e">mouse click event arguments</param>
        /// <returns>returns true if the icon was hit, false otherwise</returns>
        internal bool IsIconHit(DataGridViewCellMouseEventArgs e)
        {
            // Basic checks first
            if (e.ColumnIndex < 0)
            {
                return false;
            }
            if (!_isGroupRow)
            {
                return false;
            }

            if (DataGridView is not KryptonOutlookGrid grid)
            {
                return false;
            }

            // Get the display rectangle of the row and the cell that received the event.
            // rowBounds are relative to the DataGridView client area.
            Rectangle rowBounds = grid.GetRowDisplayRectangle(Index, false);
            Rectangle cellBounds = grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            // Calculate the mouse click's X and Y coordinates relative to the DataGridView's client area.
            // e.X is relative to the cell. cellBounds.Left is relative to the grid.
            int mouseXRelativeToGrid = cellBounds.Left + e.X;
            int mouseYRelativeToGrid = cellBounds.Top + e.Y;

            // --- Icon Position Calculation (MUST match Paint method's logic) ---
            int rowHeadersWidth = grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;
            int groupLevelIndentation = _group!.Level * GlobalStaticValues.GroupLevelMultiplier;

            int iconWidth = 11; // Width of the icon
            int iconHeight = 11; // Height of the icon

            // Calculate the icon's X-coordinate relative to the grid's client area.
            // This is the X where the icon *starts* drawing.
            // Matches iconX from Paint: contentBounds.Left + 4 + groupLevelIndentation,
            // where contentBounds.Left is (rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset)
            int iconDisplayX = rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + groupLevelIndentation;

            // Calculate the icon's Y-coordinate relative to the grid's client area.
            // This is the Y where the icon *starts* drawing.
            // Matches iconY from Paint: rowBounds.Y + (rowBounds.Height / 2) - (iconHeight / 2)
            int iconDisplayY = rowBounds.Y + (rowBounds.Height / 2) - (iconHeight / 2);

            // Create a rectangle representing the icon's drawn area in grid coordinates.
            Rectangle iconHitRect = new(
                iconDisplayX,
                iconDisplayY,
                iconWidth,
                iconHeight
            );

            // Check if the adjusted mouse coordinates fall within the icon's hit rectangle.
            if (iconHitRect.Contains(mouseXRelativeToGrid, mouseYRelativeToGrid))
            {
                return true;
            }

            return false;
        }

        internal bool IsGroupImageHit(DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return false;
            }

            if (!_isGroupRow || _group?.GroupImage == null)
            {
                return false;
            }


            KryptonOutlookGrid? grid = DataGridView as KryptonOutlookGrid;
            Rectangle rowBounds = grid!.GetRowDisplayRectangle(Index, false);

            int rowHeadersWidth = grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;
            int l = e.X + grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left;
            int offsetHeight;
            if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
            {
                offsetHeight = GlobalStaticValues.Office2013OffsetHeight;
            }
            else
            {
                offsetHeight = GlobalStaticValues.DefaultOffsetHeight;
            }

            if (_isGroupRow &&
                l >= rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 18 + _group.Level * GlobalStaticValues.GroupLevelMultiplier &&
                l <= rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 18 + _group.Level * GlobalStaticValues.GroupLevelMultiplier + 16 &&
                e.Y >= rowBounds.Height - offsetHeight &&
                e.Y <= rowBounds.Height - 6)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}