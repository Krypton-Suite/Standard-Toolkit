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
    /// Class for a rating celle
    /// </summary>
    public class RatingCell : DataGridViewImageCell
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RatingCell()
        {
            //Value type is an integer. 
            //Formatted value type is an image since we derive from the ImageCell 
            ValueType = typeof(int);
        }

        /// <summary>
        /// Overrides GetFormattedValue
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellStyle"></param>
        /// <param name="valueTypeConverter"></param>
        /// <param name="formattedValueTypeConverter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override object? GetFormattedValue(object? value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter? valueTypeConverter, TypeConverter? formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value == null)
            {
                return null; //For example it is also the case for group row...
            }
            else
            {
                return _starImages[(int)value];
            }
        }

        /// <summary>
        /// Overrides DefaultNewRowValue
        /// </summary>
        public override object DefaultNewRowValue =>
            //default new row to 3 stars 
            3;

        /// <summary>
        /// Overrides Paint
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="elementState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object? value, object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            Image? cellImage = formattedValue as Image;
            if (!ReadOnly)
            {
                int starNumber = GetStarFromMouse(cellBounds, DataGridView!.PointToClient(Control.MousePosition));

                if (starNumber != -1)
                {
                    cellImage = _starHotImages[starNumber];
                }
            }
            //supress painting of selection 
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, cellImage, errorText, cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.SelectionBackground);
        }

        /// <summary>
        /// Update cell's value when the user clicks on a star 
        /// </summary>
        /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
        protected override void OnContentClick(DataGridViewCellEventArgs e)
        {
            base.OnContentClick(e);
            if (!ReadOnly)
            {
                int starNumber = GetStarFromMouse(DataGridView!.GetCellDisplayRectangle(DataGridView.CurrentCellAddress.X, DataGridView.CurrentCellAddress.Y, false), DataGridView.PointToClient(Control.MousePosition));

                if (starNumber != -1)
                {
                    Value = starNumber;
                }
            }
        }

        #region Invalidate cells when mouse moves or leaves the cell

        /// <summary>
        /// Overrides OnMouseLeave
        /// </summary>
        /// <param name="rowIndex">the row that contains the cell.</param>
        protected override void OnMouseLeave(int rowIndex)
        {
            base.OnMouseLeave(rowIndex);
            DataGridView!.InvalidateCell(this);
        }

        /// <summary>
        /// Overrides OnMouseMove
        /// </summary>
        /// <param name="e">A DataGridViewCellMouseEventArgs that contains the event data.</param>
        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseMove(e);
            DataGridView!.InvalidateCell(this);
        }
        #endregion

        #region Private Implementation

        static Image?[] _starImages;
        static Image?[] _starHotImages;
        const int IMAGE_WIDTH = 58;

        private int GetStarFromMouse(Rectangle cellBounds, Point mouseLocation)
        {
            if (cellBounds.Contains(mouseLocation))
            {
                int mouseXRelativeToCell = mouseLocation.X - cellBounds.X;
                int imageXArea = cellBounds.Width / 2 - IMAGE_WIDTH / 2;
                if (mouseXRelativeToCell + 4 < imageXArea || mouseXRelativeToCell >= imageXArea + IMAGE_WIDTH)
                {
                    return -1;
                }
                else
                {
                    int oo = (int)Math.Round((float)(mouseXRelativeToCell - imageXArea + 2) / IMAGE_WIDTH * 10f, MidpointRounding.AwayFromZero);
                    if (oo is > 10 or < 0)
                    {
                        Debugger.Break();
                    }

                    return oo;
                }
            }
            else
            {
                return -1;
            }
        }

        //setup star images 
        #region Load star images

        static RatingCell()
        {
            _starImages = new Image[11];
            _starHotImages = new Image[11];
            // load normal stars 
            for (int i = 0; i <= 10; i++)
            {
                _starImages[i] = (GenericImageResources.ResourceManager.GetObject($"star{i}") as Image)!;
            }

            // load hot normal stars 
            for (int i = 0; i <= 10; i++)
            {
                _starHotImages[i] = GenericImageResources.ResourceManager.GetObject($"starhot{i}") as Image;
            }
        }
        #endregion

        #endregion

    }
}