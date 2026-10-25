#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
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
namespace Krypton.Toolkit;

/// <summary>
/// Class for a DataGridViewProgressCell
/// The width of the bar is always 
/// </summary>
public class KryptonDataGridViewProgressCell : KryptonDataGridViewTextBoxCell
{
    // stores a reference to the DataGridView property when it changes
    private DataGridView? _cachedDataGridView;
    // if disposal has take place
    private bool _disposed;

    #region Identity
    /// <summary>Initializes a new instance of the KryptonDataGridViewProgressCell class.</summary>
    public KryptonDataGridViewProgressCell() 
    {
        _disposed = false;
        _cachedDataGridView = null;
    }

    private void OnDataGridViewDataError(object? sender, DataGridViewDataErrorEventArgs e)
    {
        // Invalid input is not excepted but does not need to throw an error
        // Attaching this routine also requires a check when the cell is being disposed
        e.ThrowException = false;
        e.Cancel = false;
    }
    #endregion

    #region Public Overrides
    /// <summary>Specify the type of object used for editing. This is how the WinForms framework figures out what type of edit control to make.</summary>
    public override Type EditType => typeof(KryptonDataGridViewProgressEditingControl);

    /// <summary>Overrides TypeValue.</summary>
    public override Type ValueType => base.ValueType;

    /// <summary>Specify the default cell contents upon creation of a new cell.</summary>
    public override object DefaultNewRowValue => DBNull.Value;
    #endregion

    #region Protected Overrides
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            // Unsubscribe from event
            if (_cachedDataGridView is not null)
            {
                _cachedDataGridView.DataError -= OnDataGridViewDataError;
            }

            if (DataGridView is not null)
            {
                DataGridView.DataError -= OnDataGridViewDataError;
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }

    protected override void OnDataGridViewChanged()
    {
        if (_cachedDataGridView is not null)
        {
            _cachedDataGridView.DataError -= OnDataGridViewDataError;
        }

        _cachedDataGridView = DataGridView;

        if (_cachedDataGridView is not null)
        {
            _cachedDataGridView.DataError += OnDataGridViewDataError;
        }
    }

    /// <inheritdoc/>
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value, 
        object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        // By taking the string representation of the value any underlying type can be converted
        if (DataGridView is KryptonDataGridView dataGridView
            && KryptonOwningColumn is not null
            && value != DBNull.Value
            && value is not null
            && decimal.TryParse(value.ToString(), out decimal decValue))
        {
            if (ErrorText.Length == 0)
            {
                if (DataGridView.Rows.SharedRow(rowIndex).Index != -1)
                {
                    // progress bar width
                    int barwidthCorrected = cellBounds.Width - InheritedStyle.Padding.Left - InheritedStyle.Padding.Right - 5;

                    int barWidth = decValue >= 1
                        ? barwidthCorrected
                        : (int)(barwidthCorrected * decValue);

                    if ((KryptonOwningColumn.ProgressBar.ShowBorder || KryptonOwningColumn.ProgressBar.ShowBar)
                        && barWidth >= 0
                        && barWidth <= barwidthCorrected)
                    {
                        // Draw the bar
                        DrawProgressBar(graphics, cellBounds, barWidth, barwidthCorrected);
                    }

                    // Custom text color for the progress cell
                    if (KryptonOwningColumn.ProgressBar.TextColor != GlobalStaticValues.EMPTY_COLOR)
                    {
                        cellStyle.ForeColor = KryptonOwningColumn.ProgressBar.TextColor;
                    }
                }
            }
            else
            {
                formattedValue = ErrorText;
            }

            // Offset the text 1 pixel so it is not drawn against the border
            Rectangle rect2 = KryptonOwningColumn.ProgressBar.ShowBorder
                ? new Rectangle(cellBounds.Left + 1, cellBounds.Top, cellBounds.Width - 1, cellBounds.Height)
                : cellBounds;

            // Draw the cell text
            base.Paint(graphics, clipBounds, rect2, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                DataGridViewPaintParts.None | DataGridViewPaintParts.ContentForeground);
        }
        else
        {
            // Progress features only work within a KryptonDataGridView
            // Pass the paint request back
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }
    #endregion

    #region Private
    private Rectangle GetRectangleFullBar(Rectangle cellBounds, int barwidthCorrected)
    {
        // Rectangle for the complete bar
        // It is cached on the column so it is only calculated when the width of the column changes or RTL changes.
        return DataGridView!.RightToLeft == RightToLeft.No
            // Left to right
            ? new(
                cellBounds.X + 2 + InheritedStyle.Padding.Left,
                cellBounds.Y + 1 + InheritedStyle.Padding.Top,
                barwidthCorrected,
                cellBounds.Height - 4 - InheritedStyle.Padding.Bottom - InheritedStyle.Padding.Top)
            // Right to left
            : new(
                cellBounds.X + 2 + InheritedStyle.Padding.Left,
                cellBounds.Y + 1 + InheritedStyle.Padding.Top,
                barwidthCorrected,
                cellBounds.Height - 4 - InheritedStyle.Padding.Bottom - InheritedStyle.Padding.Top);
    }

    private Rectangle GetRectangleCompleted(Rectangle cellBounds, int barWidth)
    {
        return DataGridView!.RightToLeft == RightToLeft.No
            // Left to right
            ? new(
                cellBounds.X + 2 + InheritedStyle.Padding.Left,
                cellBounds.Y + 1 + InheritedStyle.Padding.Top,
                barWidth,
                cellBounds.Height - 4 - InheritedStyle.Padding.Bottom - InheritedStyle.Padding.Top)
            // Right to left
            : new(
                cellBounds.X + 2 + (cellBounds.Width - 5 - barWidth - InheritedStyle.Padding.Left),
                cellBounds.Y + 1 + InheritedStyle.Padding.Top,
                barWidth,
                cellBounds.Height - 4 - InheritedStyle.Padding.Bottom - InheritedStyle.Padding.Top);
    }

    private void DrawProgressBar(Graphics graphics, Rectangle cellBounds, int barWidth, int barwidthCorrected)
    {
        // Rectangle for the complete bar & border
        Rectangle rectFullBar = GetRectangleFullBar(cellBounds, barwidthCorrected);

        // Draw the bar
        if (KryptonOwningColumn!.ProgressBar.ShowBar)
        {
            // Rectangle for the completed progress
            Rectangle rectCompleted = GetRectangleCompleted(cellBounds, barWidth);

            // Draw the full bar
            if (KryptonOwningColumn.ProgressBar.UseSolidColor)
            {
                using var brush = new SolidBrush(KryptonOwningColumn.RemainingColor1);
                graphics.FillRectangle(brush, rectFullBar);
            }
            else
            {
                using var brush = new LinearGradientBrush(rectFullBar, KryptonOwningColumn.RemainingColor1, KryptonOwningColumn.RemainingColor2, KryptonOwningColumn.ProgressBar.LinearGradientMode);
                graphics.FillRectangle(brush, rectFullBar);
            }

            // Draw the completed portion if the barWidth greater than zero
            if (barWidth > 0)
            {
                if (KryptonOwningColumn.ProgressBar.UseSolidColor)
                {
                    using var brush = new SolidBrush(KryptonOwningColumn.CompletedColor1);
                    graphics.FillRectangle(brush, rectCompleted);
                }
                else
                {
                    using var brush = new LinearGradientBrush(rectCompleted, KryptonOwningColumn.CompletedColor1, KryptonOwningColumn.CompletedColor2, KryptonOwningColumn.ProgressBar.LinearGradientMode);
                    graphics.FillRectangle(brush, rectCompleted);
                }
            }
        }

        // Draw the border
        if (KryptonOwningColumn.ProgressBar.ShowBorder)
        {
            using Pen pen = new Pen(KryptonOwningColumn.BorderColor);
            graphics.DrawRectangle(pen, rectFullBar);
        }
    }
    #endregion
    #region Internal
    /// <summary>
    /// Type casted version of OwningColumn
    /// </summary>
    internal KryptonDataGridViewProgressColumn? KryptonOwningColumn => OwningColumn as KryptonDataGridViewProgressColumn;
    #endregion
}