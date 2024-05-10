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
    /// Formatting cell
    /// </summary>
    /// <seealso cref="KryptonDataGridViewTextBoxCell" />
    public class FormattingCell : KryptonDataGridViewTextBoxCell
    {
        #region Instance Fields

        private DataGridViewCell _cell;

        #endregion

        #region Public

        /// <summary>
        /// Gets or sets the type of the format.
        /// </summary>
        /// <value>
        /// The type of the format.
        /// </value>
        public EnumConditionalFormatType FormatType { get; set; }
        /// <summary>
        /// Gets or sets the format parameters.
        /// </summary>
        /// <value>
        /// The format parameters.
        /// </value>
        public IFormatParams? FormatParams { get; set; }

        #endregion

        #region Identity

        public FormattingCell()
        {

        }

        public FormattingCell(DataGridViewCell cell) => _cell = cell;

        public FormattingCell(KryptonDataGridViewTextBoxCell textBoxCell)
        {

        }

        #endregion

        #region Implementation

        /// <summary>
        /// Contrasts the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        private Color ContrastColor(Color color)
        {
            int d;
            //  Counting the perceptive luminance - human eye favors green color... 
            double a = 1 - (0.299 * color.R + (0.587 * color.G + 0.114 * color.B)) / 255;

            if (a < 0.5)
            {
                d = 0;
            }
            else
            {
                //  bright colors - black font
                d = 255;
            }

            //  dark colors - white font
            return Color.FromArgb(d, d, d);
        }

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
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value, object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (FormatParams != null)  // null can happen when cell set to Formatting but no condition has been set !
            {
                switch (FormatType)
                {
                    case EnumConditionalFormatType.Bar:
                        int barWidth;
                        BarParams par = (BarParams)FormatParams;
                        barWidth = (int)((cellBounds.Width - 10) * par.ProportionValue);
                        if (DataGridView != null)
                        {
                            Style.BackColor = DataGridView.DefaultCellStyle.BackColor;
                            Style.ForeColor = DataGridView.DefaultCellStyle.ForeColor;
                        }

                        if (barWidth > 0) //(double)value > 0 &&
                        {
                            Rectangle r = new(cellBounds.X + 3, cellBounds.Y + 3, barWidth, cellBounds.Height - 8);
                            if (par.GradientFill)
                            {
                                using (LinearGradientBrush linearBrush = new(r, par.BarColor, Color.White, LinearGradientMode.Horizontal)) //Color.FromArgb(255, 247, 251, 242)
                                {
                                    graphics.FillRectangle(linearBrush, r);
                                }
                            }
                            else
                            {
                                using (SolidBrush solidBrush = new(par.BarColor)) //Color.FromArgb(255, 247, 251, 242)
                                {
                                    graphics.FillRectangle(solidBrush, r);
                                }
                            }

                            using (Pen pen = new(par.BarColor)) //Color.FromArgb(255, 140, 197, 66)))
                            {
                                graphics.DrawRectangle(pen, r);
                            }
                        }

                        break;
                    case EnumConditionalFormatType.TwoColorsRange:
                        TwoColorsParams? twCpar = FormatParams as TwoColorsParams;
                        Style.BackColor = twCpar!.ValueColor;
                        //  if (ContrastTextColor)
                        Style.ForeColor = ContrastColor(twCpar.ValueColor);
                        break;
                    case EnumConditionalFormatType.ThreeColorsRange:
                        ThreeColorsParams? thCpar = FormatParams as ThreeColorsParams;
                        Style.BackColor = thCpar!.ValueColor;
                        Style.ForeColor = ContrastColor(thCpar.ValueColor);
                        break;
                    default:
                        if (DataGridView != null)
                        {
                            Style.BackColor = DataGridView.DefaultCellStyle.BackColor;
                            Style.ForeColor = DataGridView.DefaultCellStyle.ForeColor;
                        }

                        break;
                }
            }
            else
            {
                if (DataGridView != null)
                {
                    Style.BackColor = DataGridView.DefaultCellStyle.BackColor;
                    Style.ForeColor = DataGridView.DefaultCellStyle.ForeColor;
                }
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                DataGridViewPaintParts.None | DataGridViewPaintParts.ContentForeground);
        }

        #endregion
    }
}