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
    /// Class for a TextAndImage cell
    /// </summary>
    public class KryptonDataGridViewTreeTextCell : KryptonDataGridViewTextBoxCell
    {
        #region Static Fields

        private const int INDENT_WIDTH = 20;
        private const int INDENT_MARGIN = 5;

        #endregion

        #region Instance Fields

        private Padding _defaultPadding;
        // private int glyphWidth;

        #endregion

        #region Identity

        /// <summary>
        /// Constructor
        /// </summary>
        public KryptonDataGridViewTreeTextCell()
        {
            _defaultPadding = Style.Padding;
        }

        #endregion

        #region Public Overrides

        /// <summary>
        /// Overrides Clone
        /// </summary>
        /// <returns>The cloned KryptonDataGridViewTextAndImageCell</returns>
        public override object Clone()
        {
            KryptonDataGridViewTreeTextCell? c = base.Clone() as KryptonDataGridViewTreeTextCell;
            return c!;
        }

        #endregion

        #region Protected Virtual

        /// <summary>
        /// Gets the glyph margin.
        /// </summary>
        /// <value>
        /// The glyph margin.
        /// </value>
        protected virtual int GlyphMargin => (Level - 1) * INDENT_WIDTH + INDENT_MARGIN;

        #endregion

        #region Public

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level
        {
            get
            {
                OutlookGridRow row = (OutlookGridRow)OwningRow!;
                return row.NodeLevel + 1; //during calculation 0 level must be 1 for multiplication
            }
        }

        /// <summary>
        /// Gets the owning node.
        /// </summary>
        /// <value>
        /// The owning node.
        /// </value>
        public OutlookGridRow? OwningNode => OwningRow as OutlookGridRow;

        #endregion

        #region Implementation

        /// <summary>
        /// Updates the style.
        /// </summary>
        /// <remarks>padding especially.</remarks>
        public void UpdateStyle()
        {
            OutlookGridRow? node = OwningNode;
            //Console.WriteLine(DateTime.Now.ToString() + " " + node.ToString());
            bool hasChildNodes = node != null && node.HasChildren;
            int level = Level;
            int plus = 0;
            //if (hasChildNodes)
            //    plus = 15;
            Style.Padding = new Padding(_defaultPadding.Left + level * INDENT_WIDTH + INDENT_MARGIN + plus,
                _defaultPadding.Top, _defaultPadding.Right, _defaultPadding.Bottom);
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Paints the specified graphics.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="clipBounds">The clip bounds.</param>
        /// <param name="cellBounds">The cell bounds.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="cellState">State of the cell.</param>
        /// <param name="value">The value.</param>
        /// <param name="formattedValue">The formatted value.</param>
        /// <param name="errorText">The error text.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="advancedBorderStyle">The advanced border style.</param>
        /// <param name="paintParts">The paint parts.</param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value, object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            OutlookGridRow? node = OwningNode;
            // Console.WriteLine(DateTime.Now.ToString() + " " + node.ToString());
            //bool hasChildNodes = node.HasChildren;
            //int level = this.Level ;
            //int plus = 0;
            //if (hasChildNodes)
            //    plus = 15;
            //Padding currentPadding = this.InheritedStyle.Padding;
            // this.Style.Padding = new Padding(defaultPadding.Left + (level * INDENT_WIDTH) + INDENT_MARGIN,
            //                                             defaultPadding.Top, defaultPadding.Right, defaultPadding.Bottom);

            //this.Style.Padding = new Padding(currentPadding.Left + (level * INDENT_WIDTH) + _imageWidth + INDENT_MARGIN,
            //                                   currentPadding.Top, currentPadding.Right, currentPadding.Bottom);

            //if (this.Value != null && ((TextAndImage)this.Value).Image != null)
            //{
            //    Padding inheritedPadding = this.InheritedStyle.Padding;
            //    this.Style.Padding = new Padding(18, inheritedPadding.Top, inheritedPadding.Right, inheritedPadding.Bottom);
            //    // Draw the image clipped to the cell.
            //    System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
            //    graphics.SetClip(cellBounds);
            //    graphics.DrawImageUnscaled(((TextAndImage)this.Value).Image, new Point(cellBounds.Location.X + 2, cellBounds.Location.Y + ((cellBounds.Height - 16) / 2) - 1));
            //    graphics.EndContainer(container);
            //}

            //if (node == null) return;

            //Image image = node.Image;

            //if (this._imageHeight == 0 && image != null) this.UpdateStyle();

            // paint the cell normally
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // TODO: Indent width needs to take image size into account
            Rectangle glyphRect = new(cellBounds.X + GlyphMargin, cellBounds.Y, INDENT_WIDTH, cellBounds.Height - 1);


            ////TODO: This painting code needs to be rehashed to be cleaner
            //int level = this.Level;

            ////TODO: Rehash this to take different Imagelayouts into account. This will speed up drawing
            ////		for images of the same size (ImageLayout.None)
            //if (image != null)
            //{
            //    Point pp;
            //    if (_imageHeight > cellBounds.Height)
            //        pp = new Point(glyphRect.X + this.glyphWidth, cellBounds.Y + _imageHeightOffset);
            //    else
            //        pp = new Point(glyphRect.X + this.glyphWidth, (cellBounds.Height / 2 - _imageHeight / 2) + cellBounds.Y);

            //    // Graphics container to push/pop changes. This enables us to set clipping when painting
            //    // the cell's image -- keeps it from bleeding outsize of cells.
            //    System.Drawing.Drawing2D.GraphicsContainer gc = graphics.BeginContainer();
            //    {
            //        graphics.SetClip(cellBounds);
            //        graphics.DrawImageUnscaled(image, pp);
            //    }
            //    graphics.EndContainer(gc);
            //}

            // Paint tree lines			
            if (node != null 
                && node.DataGridView is not null
                && ((KryptonOutlookGrid)node.DataGridView).ShowLines)
            {
                using (Pen linePen = new(SystemBrushes.ControlDark, 1.0f))
                {
                    linePen.DashStyle = DashStyle.Dot;
                    bool isLastSibling = node.IsLastSibling;
                    bool isFirstSibling = node.IsFirstSibling;

                    if (node.NodeLevel == 0)
                    {
                        // the Root nodes display their lines differently
                        if (isFirstSibling && isLastSibling)
                        {
                            // only node, both first and last. Just draw horizontal line
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else if (isLastSibling)
                        {
                            // last sibling doesn't draw the line extended below. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else if (isFirstSibling)
                        {
                            // first sibling doesn't draw the line extended above. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.X + 4, cellBounds.Bottom);
                        }
                        else
                        {
                            // normal drawing draws extended from top to bottom. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Bottom);
                        }
                    }
                    else
                    {
                        if (isLastSibling)
                        {
                            // last sibling doesn't draw the line extended below. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else
                        {
                            // normal drawing draws extended from top to bottom. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Bottom);
                        }

                        // paint lines of previous levels to the root
                        OutlookGridRow? previousNode = node.ParentNode;
                        int horizontalStop = glyphRect.X + 4 - INDENT_WIDTH;

                        while (previousNode != null)//.IsRoot)
                        {
                            if (previousNode.HasChildren && !previousNode.IsLastSibling)
                            {
                                // paint vertical line
                                graphics.DrawLine(linePen, horizontalStop, cellBounds.Top, horizontalStop, cellBounds.Bottom);
                            }
                            previousNode = previousNode.ParentNode;
                            horizontalStop = horizontalStop - INDENT_WIDTH;
                        }
                    }

                }
            }

            if (node != null && node.HasChildren)
            {
                // Paint node glyphs	
                if (node.Collapsed)
                {
                    if (KryptonManager.CurrentGlobalPalette != null && (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013))
                    {
                        graphics.DrawImage(GenericImageResources.CollapseIcon2010, glyphRect.X, glyphRect.Y + glyphRect.Height / 2 - 4, 11, 11);
                    }
                    else
                    {
                        graphics.DrawImage(GenericImageResources.ExpandIcon, glyphRect.X, glyphRect.Y + glyphRect.Height / 2 - 4, 11, 11);
                    }
                }
                else
                {
                    if (KryptonManager.CurrentGlobalPalette != null && (KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2010 || KryptonManager.CurrentGlobalPalette.GetRenderer() == KryptonManager.RenderOffice2013))
                    {
                        graphics.DrawImage(GenericImageResources.ExpandIcon2010, glyphRect.X, glyphRect.Y + glyphRect.Height / 2 - 4, 11, 11);
                    }
                    else
                    {
                        graphics.DrawImage(GenericImageResources.CollapseIcon, glyphRect.X, glyphRect.Y + glyphRect.Height / 2 - 4, 11, 11);
                    }
                }
            }
            //graphics.DrawRectangle(new Pen(new SolidBrush(Color.Red)), glyphRect);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseUp" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseUp(e);

            OutlookGridRow? node = OwningNode;
            if (node != null)
            {
                (node.DataGridView as KryptonOutlookGrid)!.InExpandCollapseMouseCapture = false;
            }
        }
        /// <summary>
        /// Raises the <see cref="E:MouseDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            Rectangle dis = DataGridView!.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Rectangle glyphRect = new(dis.X + GlyphMargin, dis.Y, INDENT_WIDTH, dis.Height - 1);

            //if (e.X > this.InheritedStyle.Padding.Left)
            if (e.X + dis.X <= glyphRect.X + 11 &&
                e.X + dis.X >= glyphRect.X)
            {

                // Expand the node
                //TODO: Calculate more precise location
                OutlookGridRow? node = OwningNode;
                if (node != null)
                {
                    (node.DataGridView as KryptonOutlookGrid)!.InExpandCollapseMouseCapture = true;

                    if (node.Collapsed)
                    {
                        node.Expand();
                    }
                    else
                    {
                        node.Collapse();
                    }
                }
            }
            else
            {
                base.OnMouseDown(e);
            }
        }

        #endregion
    }
}