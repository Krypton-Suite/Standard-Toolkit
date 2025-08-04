#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonDataGridViewRatingCell : KryptonDataGridViewTextBoxCell
{
    #region Identity
    public KryptonDataGridViewRatingCell()
    {
    }
    #endregion

    #region Public override
    /// <inheritdoc/>
    public override Type ValueType 
    {
        get => KryptonDataGridViewRatingColumn._defaultValueType;
    }

    /// <inheritdoc/>
    public override object DefaultNewRowValue 
    {
        get => (byte)0;
    }
    #endregion

    #region Protected override
    /// <inheritdoc/>
    public override object Clone()
    {
        var clone = base.Clone() as KryptonDataGridViewRatingCell ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("clone"));
        return clone;
    }

    /// <inheritdoc/>
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value, 
        object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        // if value is zero no rating image will be displayed
        var currentValue = value is not null 
            ? value.ToString() 
            : "0";

        // we never show the numbers
        value = null;
        formattedValue = null;
        
        if (DataGridView is not null
            && !DataGridView.Rows[rowIndex].IsNewRow
            && DataGridView?.Rows.SharedRow(rowIndex).Index != -1
            && OwningKryptonColumn?.RatingImageCount > 0
            && byte.TryParse(currentValue, out byte cellValue)
            && cellValue > 0)
        {
            // first the cell is painted
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // Get the rating image, if the cellValue is smaller than 1 it will return null
            Image? image = OwningKryptonColumn.GetImage(cellValue);

            if (image is not null)
            {
                int x;
                int y;
                int width;

                PaintGetImagePosition(out x, out y, out width, in image, in cellBounds);
                graphics.DrawImage(image, x, y, new Rectangle(0, 0, width, image.Height), GraphicsUnit.Pixel);
            }
        }
        else
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }
    #endregion

    #region Internal
    internal KryptonDataGridViewRatingColumn? OwningKryptonColumn 
    {
        get => field ??= OwningColumn as KryptonDataGridViewRatingColumn;
    }
    #endregion

    #region Private
    // Determines the factors to position the image in the cell.
    // Used internally only
    private void PaintGetImagePosition(out int x, out int y, out int width, in Image image, in Rectangle cellBounds)
    {
        if (DataGridView!.RightToLeft == RightToLeft.No)
        {
            x = cellBounds.X;
            y = cellBounds.Top + 3;
        }
        else
        {
            x = cellBounds.X + cellBounds.Width - image.Width;
            if (x < cellBounds.X)
            {
                x = cellBounds.X;
            }

            y = cellBounds.Top + 3;
        }

        width = Math.Min(cellBounds.Width, image.Width);
    }
    #endregion
}
