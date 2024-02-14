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
    /// In order to support grouping with the same look and feel as Outlook, the behaviour
    /// of the DataGridViewRow is overridden by the OutlookGridRow.
    /// The OutlookGridRow has 2 main additional properties: the Group it belongs to and
    /// a the IsRowGroup flag that indicates whether the OutlookGridRow object behaves like
    /// a regular row (with data) or should behave like a Group row.
    /// </summary>
    public class OutlookGridRow : DataGridViewRow
    {
        #region "Variables"

        private bool _isGroupRow;
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
            //For the TreeGridView project if the selection mode is FullRow subnodes that where collapsed disappear when parent collapse/expands
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
            if (_isGroupRow)
            {
                KryptonOutlookGrid grid = (KryptonOutlookGrid)DataGridView;
                int rowHeadersWidth = grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;
                int groupLevelIndentation = _group!.Level * GlobalStaticValues.GroupLevelMultiplier;

                int gridwidth = grid.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                Rectangle myRowBounds = rowBounds;
                myRowBounds.Width = gridwidth;

                IPaletteBack paletteBack = grid.StateNormal.DataCell.Back;
                IPaletteBorder paletteBorder = grid.StateNormal.DataCell.Border;

                PaletteState state = PaletteState.Normal;
                if (grid.PreviousSelectedGroupRow == rowIndex && KryptonManager.CurrentGlobalPalette.GetRenderer() != KryptonManager.RenderOffice2013)
                {
                    state = PaletteState.CheckedNormal;
                }

                using (RenderContext renderContext = new(grid, graphics, myRowBounds, grid.Renderer))
                {
                    using (GraphicsPath path = grid.Renderer.RenderStandardBorder.GetBackPath(renderContext, myRowBounds, paletteBorder, VisualOrientation.Top, PaletteState.Normal))
                    {
                        //Back
                        IDisposable? unused = grid.Renderer.RenderStandardBack.DrawBack(renderContext,
                            myRowBounds,
                            path,
                            paletteBack,
                            VisualOrientation.Top,
                            state,
                            null);

                        // We never save the memento for reuse later
                        if (unused != null)
                        {
                            unused.Dispose();
                            unused = null;
                        }
                    }
                }

                // Draw the botton : solid line for 2007 palettes or dot line for 2010 palettes, full background for 2013
                if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010)
                {
                    using (Pen focusPen = new(Color.Gray))
                    {
                        focusPen.DashStyle = DashStyle.Dash;
                        graphics.DrawLine(focusPen, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset, rowBounds.Bottom - 1, gridwidth + 1, rowBounds.Bottom - 1);
                    }
                }
                else if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                {
                    using (SolidBrush br = new(Color.FromArgb(225, 225, 225)))
                    {
                        graphics.FillRectangle(br, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset, rowBounds.Bottom - GlobalStaticValues._2013GroupRowHeight, gridwidth + 1, GlobalStaticValues._2013GroupRowHeight - 1);
                    }
                }
                else
                {
                    using (SolidBrush br = new(paletteBorder.GetBorderColor1(state)))
                    {
                        graphics.FillRectangle(br, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset, rowBounds.Bottom - 2, gridwidth + 1, 2);
                    }
                }

                //Draw right vertical bar 
                if (grid.CellBorderStyle is DataGridViewCellBorderStyle.SingleVertical or DataGridViewCellBorderStyle.Single)
                {
                    using (SolidBrush br = new(paletteBorder.GetBorderColor1(state)))
                    {
                        graphics.FillRectangle(br, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + gridwidth, rowBounds.Top, 1, rowBounds.Height);
                    }
                }

                //Set the icon and lines according to the renderer
                if (_group.Collapsed)
                {
                    if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                    {
                        graphics.DrawImage(GenericImageResources.CollapseIcon2010, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + groupLevelIndentation, rowBounds.Bottom - 18, 11, 11);
                    }
                    else
                    {
                        graphics.DrawImage(GenericImageResources.ExpandIcon, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + groupLevelIndentation, rowBounds.Bottom - 18, 11, 11);
                    }
                }
                else
                {
                    if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                    {
                        graphics.DrawImage(GenericImageResources.ExpandIcon2010, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + groupLevelIndentation, rowBounds.Bottom - 18, 11, 11);
                    }
                    else
                    {
                        graphics.DrawImage(GenericImageResources.CollapseIcon, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + groupLevelIndentation, rowBounds.Bottom - 18, 11, 11);
                    }
                }

                //Draw image group
                int imageoffset = 0;
                if (_group.GroupImage != null)
                {
                    if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                    {
                        graphics.DrawImage(_group.GroupImage, rowHeadersWidth - grid.HorizontalScrollingOffset + GlobalStaticValues.ImageOffsetWidth + groupLevelIndentation, rowBounds.Bottom - GlobalStaticValues.Office2013OffsetHeight, GlobalStaticValues.GroupImageSide, GlobalStaticValues.GroupImageSide);
                        imageoffset = GlobalStaticValues.ImageOffsetWidth;
                    }
                    else
                    {
                        graphics.DrawImage(_group.GroupImage, rowHeadersWidth - grid.HorizontalScrollingOffset + GlobalStaticValues.ImageOffsetWidth + groupLevelIndentation, rowBounds.Bottom - GlobalStaticValues.DefaultOffsetHeight, GlobalStaticValues.GroupImageSide, GlobalStaticValues.GroupImageSide);
                        imageoffset = GlobalStaticValues.ImageOffsetWidth;
                    }
                }

                //Draw text, using the current grid font
                int offsetText = rowHeadersWidth - grid.HorizontalScrollingOffset + 18 + imageoffset + groupLevelIndentation;
                if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                {
                    TextRenderer.DrawText(graphics, _group.Text, grid.GridPalette?.GetContentShortTextFont(PaletteContentStyle.LabelBoldControl, state), new Rectangle(offsetText, rowBounds.Bottom - GlobalStaticValues.Office2013OffsetHeight, rowBounds.Width - offsetText, rowBounds.Height), grid.GridPalette!.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state),
                                 TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.PreserveGraphicsClipping);
                }
                else
                {
                    TextRenderer.DrawText(graphics, _group.Text, grid.GridPalette?.GetContentShortTextFont(PaletteContentStyle.LabelBoldControl, state), new Rectangle(offsetText, rowBounds.Bottom - GlobalStaticValues.DefaultOffsetHeight, rowBounds.Width - offsetText, rowBounds.Height), grid.GridPalette!.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state),
                                   TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.PreserveGraphicsClipping);
                }

                ////Debug Hits
                ////ExpandCollaspe icon
                //graphics.DrawRectangle(new Pen(Color.Red), new Rectangle(rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + group.Level * 15, rowBounds.Bottom - 18, 11, 11));
                ////Image
                //if (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013)
                //    graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle(rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + StaticValues._ImageOffsetwidth + groupLevelIndentation, rowBounds.Bottom - StaticValues._2013OffsetHeight, StaticValues._groupImageSide, StaticValues._groupImageSide));
                //else
                //    graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle(rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + StaticValues._ImageOffsetwidth + groupLevelIndentation, rowBounds.Bottom - StaticValues._defaultOffsetHeight, StaticValues._groupImageSide, StaticValues._groupImageSide));
            }
            else
            {
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
            if (!_isGroupRow)
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

                //this is a workaround to make the grid re-calculate it's contents and backgroun bounds
                // so the background is updated correctly.
                // this will also invalidate the control, so it will redraw itself
                Visible = false;
                Visible = true;

                //When collapsing the first row still seeing it.
                if (Index < DataGridView.FirstDisplayedScrollingRowIndex)
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

                //this is a workaround to make the grid re-calculate it's contents and backgroun bounds
                // so the background is updated correctly.
                // this will also invalidate the control, so it will redraw itself
                Visible = false;
                Visible = true;

                //When collapsing the first row still seeing it.
                if (Index < DataGridView.FirstDisplayedScrollingRowIndex)
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
            ((KryptonOutlookGrid)DataGridView).CollapseNode(this);
        }

        /// <summary>
        /// Expand Node (with events)
        /// </summary>
        public void Expand()
        {
            ((KryptonOutlookGrid)DataGridView).ExpandNode(this);
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
            if (e.ColumnIndex < 0)
            {
                return false;
            }

            if (!_isGroupRow)
            {
                return false;
            }

            KryptonOutlookGrid? grid = DataGridView as KryptonOutlookGrid;
            Rectangle rowBounds = grid!.GetRowDisplayRectangle(Index, false);

            int rowHeadersWidth = grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;
            int l = e.X + grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left;
            if (_isGroupRow &&
                l >= rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + _group!.Level * GlobalStaticValues.GroupLevelMultiplier &&
                l <= rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + _group.Level * GlobalStaticValues.GroupLevelMultiplier + 11 &&
                e.Y >= rowBounds.Height - 18 &&
                e.Y <= rowBounds.Height - 7)
            {
                return true;
            }

            return false;
        }

        //internal bool IsNodeIconHit(DataGridViewCellMouseEventArgs e)
        //{
        //    if (e.ColumnIndex < 0) return false;
        //    if (!this.HasChildren) return false;

        //    DataGridViewCell cell = this.Cells[e.ColumnIndex];
        //    if (cell.GetType() is KryptonDataGridViewTreeTextCell) {
        //    cell.
        //    }
        //    KryptonOutlookGrid grid = (KryptonOutlookGrid)this.DataGridView;


        //    Rectangle glyphRect = new Rectangle(rect.X + this.GlyphMargin, rect.ContentBounds.Y, INDENT_WIDTH, this.ContentBounds.Height - 1);

        //    if ((e.X <= glyphRect.X + 11) &&
        //        (e.X >= glyphRect.X) &&
        //       (e.Y >= glyphRect.Y + (glyphRect.Height / 2) - 4) &&
        //     (e.Y <= glyphRect.Y + (glyphRect.Height / 2) - 4 + 11))


        //    if (this.isGroupRow &&
        //        (l >= rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + group.Level * StaticValues._groupLevelMultiplier) &&
        //        (l <= rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4 + group.Level * StaticValues._groupLevelMultiplier + 11) &&
        //        (e.Y >= rowBounds.Height - 18) &&
        //        (e.Y <= rowBounds.Height - 7))
        //        return true;

        //    return false;
        //}

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