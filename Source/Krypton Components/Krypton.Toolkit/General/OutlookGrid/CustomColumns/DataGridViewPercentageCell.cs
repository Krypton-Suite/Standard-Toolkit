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
    /// Class for a DataGridViewPercentageCell
    /// </summary>
    public class DataGridViewPercentageCell : KryptonDataGridViewTextBoxCell
    {
        #region Identity

        /// <summary>Initializes a new instance of the <see cref="DataGridViewPercentageCell" /> class.</summary>
        public DataGridViewPercentageCell()
        {
            Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        #endregion

        #region Public Overrides

        /// <summary>Specify the type of object used for editing. This is how the WinForms framework figures out what type of edit control to make.</summary>
        public override Type EditType => typeof(PercentageEditingControl);

        /// <summary>Overrides TypeValue.</summary>
        public override Type ValueType => typeof(double);

        /// <summary>Specify the default cell contents upon creation of a new cell.</summary>
        public override object DefaultNewRowValue => 0;

        #endregion

        #region Implementation

        /// <summary>
        /// Overrides Paint
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value, object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            //Draw the bar
            int barWidth;

            if (value is not null)
            {
                barWidth = (double)value >= 1.0
                    ? cellBounds.Width - 10
                    : (int)((cellBounds.Width - 10) * (double)value);

                if ((double)value > 0 && barWidth > 0)
                {
                    Rectangle r = new(cellBounds.X + 3, cellBounds.Y + 3, barWidth, cellBounds.Height - 8);

                    using (LinearGradientBrush linearBrush = new LinearGradientBrush(r, KryptonManager.CurrentGlobalPalette.GetBackColor1(PaletteBackStyle.GridHeaderColumnList, PaletteState.Normal), KryptonManager.CurrentGlobalPalette.GetBackColor2(PaletteBackStyle.GridHeaderColumnList, PaletteState.Normal), LinearGradientMode.Vertical))
                    {
                        graphics.FillRectangle(linearBrush, r);
                    }

                    using (Pen pen = new Pen(KryptonManager.CurrentGlobalPalette.GetBorderColor1(PaletteBorderStyle.GridHeaderColumnList, PaletteState.Normal)))
                    {
                        graphics.DrawRectangle(pen, r);
                    }

                    //TODO : implement customization like conditional formatting
                    //using (LinearGradientBrush linearBrush = new LinearGradientBrush(r, Color.FromArgb(255, 140, 197, 66), Color.FromArgb(255, 247, 251, 242), LinearGradientMode.Horizontal))
                    //{
                    //    graphics.FillRectangle(linearBrush, r);
                    //}

                    //using (Pen pen = new Pen(Color.FromArgb(255, 140, 197, 66)))
                    //{
                    //    graphics.DrawRectangle(pen, r);

                    //}
                }

                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                    DataGridViewPaintParts.None | DataGridViewPaintParts.ContentForeground);
            }
        }

        #endregion
    }
}