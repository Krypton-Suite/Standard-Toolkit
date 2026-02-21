#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Defines a KryptonDomainUpDown cell type for the KryptonDataGridView control
/// </summary>
public class KryptonDataGridViewDomainUpDownCell : KryptonDataGridViewTextBoxCell
{
    #region Static Fields
    private const DataGridViewContentAlignment ANY_RIGHT = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
    private const DataGridViewContentAlignment ANY_CENTER = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
    private static readonly Type _defaultEditType = typeof(KryptonDataGridViewDomainUpDownEditingControl);
    private static readonly Type _defaultValueType = typeof(string);
    #endregion

    #region Identity
    /// <summary>
    /// Constructor for the KryptonDataGridViewDomainUpDownCell cell type
    /// </summary>
    public KryptonDataGridViewDomainUpDownCell()
    {
    }

    /// <summary>
    /// Returns a standard textual representation of the cell.
    /// </summary>
    public override string ToString() =>
        $"DataGridViewDomainUpDownCell {{ ColumnIndex={ColumnIndex.ToString(CultureInfo.CurrentCulture)}, RowIndex={RowIndex.ToString(CultureInfo.CurrentCulture)} }}";

    #endregion

    #region Public

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Multiline
    {
        get => base.Multiline;
        set => base.Multiline = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool MultilineStringEditor
    {
        get => base.MultilineStringEditor;
        set => base.MultilineStringEditor = value;
    }

    /// <summary>
    /// Define the type of the cell's editing control
    /// </summary>
    public override Type EditType => _defaultEditType;

    /// <summary>
    /// Returns the type of the cell's Value property
    /// </summary>
    public override Type ValueType => base.ValueType ?? _defaultValueType;

    /// <summary>
    /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override void DetachEditingControl()
    {
        DataGridView? dataGridView = DataGridView;
        if (dataGridView?.EditingControl == null)
        {
            throw new InvalidOperationException(@"Cell is detached or its grid has no editing control.");
        }

        if (dataGridView.EditingControl is KryptonDomainUpDown domainUpDown)
        {
            domainUpDown.Items.Clear();

            if (domainUpDown.Controls[0].Controls[1] is TextBox textBox)
            {
                textBox.ClearUndo();
            }
        }

        base.DetachEditingControl();
    }

    /// <summary>
    /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control
    /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are
    /// set according to the cell properties.
    /// </summary>
    public override void InitializeEditingControl(int rowIndex,
        object? initialFormattedValue,
        DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

        if (DataGridView!.EditingControl is KryptonDomainUpDown domainUpDown)
        {
            domainUpDown.Items.Clear();
            domainUpDown.ButtonSpecs.Clear();
            domainUpDown.ReadOnly = KryptonOwningColumn?.ReadOnlyItemsList ?? false;
            domainUpDown.RightToLeft = DataGridView!.RightToLeft;

            if (KryptonOwningColumn is not null)
            {
                domainUpDown.Items.AddRange(KryptonOwningColumn.Items);
            }

            domainUpDown.Text = initialFormattedValue as string ?? string.Empty;

            if (KryptonOwningColumn is KryptonDataGridViewDomainUpDownColumn dudColumn)
            {
                KryptonDataGridViewUtilities.SyncEditorButtonSpecs(DataGridView as KryptonDataGridView, dudColumn, domainUpDown.ButtonSpecs);
                foreach (var spec in domainUpDown.ButtonSpecs.Enumerate().OfType<ButtonSpecAny>())
                {
                    spec.Click += (s, e) =>
                        dudColumn.RaiseButtonSpecClick(new DataGridViewButtonSpecClickEventArgs(dudColumn, this, spec));
                }
                domainUpDown.PerformLayout();
                domainUpDown.Invalidate();
            }
        }
    }

    #endregion

    #region Protected
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value,
        object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        if (DataGridView is not null
            && KryptonOwningColumn?.CellIndicatorImage is Image image)
        {
            int pos;
            Rectangle textArea;
            var righToLeft = DataGridView.RightToLeft == RightToLeft.Yes;

            // Use the same button width as the editor so renderer output matches
            int buttonWidth = SystemInformation.VerticalScrollBarWidth - 2;
            int reservedStrip = buttonWidth + IndicatorGap;

            if (righToLeft)
            {
                pos = cellBounds.Left;

                // The WinForms cell content always receives padding of one by default, custom padding is added to that.
                // Reserve the indicator strip on the left; padding contributes first via reservedStrip semantics
                int reserveLeft = Math.Max(reservedStrip - cellStyle.Padding.Left, 0);
                textArea = new Rectangle(
                    cellBounds.Left + cellStyle.Padding.Left + reserveLeft,
                    cellBounds.Top  + cellStyle.Padding.Top,
                    cellBounds.Width  - cellStyle.Padding.Left - cellStyle.Padding.Right  - reserveLeft,
                    cellBounds.Height - cellStyle.Padding.Top  - cellStyle.Padding.Bottom - 2);
            }
            else
            {
                pos = cellBounds.Right - buttonWidth;

                // The WinForms cell content always receives padding of one by default, custom padding is added to that.
                // Reserve the indicator strip on the right; padding contributes first via reservedStrip semantics
                int reserveRight = Math.Max(reservedStrip - cellStyle.Padding.Right, 0);
                textArea = new Rectangle(
                    cellBounds.Left + cellStyle.Padding.Left,
                    cellBounds.Top  + cellStyle.Padding.Top,
                    cellBounds.Width  - cellStyle.Padding.Left - cellStyle.Padding.Right  - reserveRight,
                    cellBounds.Height - cellStyle.Padding.Top  - cellStyle.Padding.Bottom - 2);
            }

            // When the Krypton column is part of a WinForms DataGridView let the default paint routine paint the cell.
            // Afterwards we paint the text and drop down image.
            if (DataGridView is DataGridView)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null,
                    string.Empty, errorText, cellStyle, advancedBorderStyle, paintParts);
            }

            // Draw the drop down button, only if no ErrorText has been set.
            // If the ErrorText is set, only the error icon is shown. Otherwise both are painted on the same spot.
            if (ErrorText.Length == 0)
            {
                var buttonRect = new Rectangle(
                    righToLeft
                        ? cellBounds.Left + IndicatorGap
                        : cellBounds.Right - buttonWidth - IndicatorGap,
                    textArea.Top,
                    buttonWidth,
                    textArea.Height);

                // Use crisp cached glyph rendered by renderer if available
                var sized = KryptonOwningColumn?.GetIndicatorImageForSize(Math.Min(buttonRect.Width, buttonRect.Height)) ?? image;
                int y = textArea.Top + (textArea.Height - buttonWidth) / 2;
                graphics.DrawImage(sized, new Rectangle(buttonRect.X, y, buttonWidth, buttonWidth));
            }
            else
            {
                formattedValue = errorText;
            }

            var displayForeColor = (cellState & DataGridViewElementStates.Selected) != 0
                ? cellStyle.SelectionForeColor
                : cellStyle.ForeColor;

            TextRenderer.DrawText(graphics, formattedValue?.ToString() ?? string.Empty, cellStyle.Font, textArea, displayForeColor,
                KryptonDataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(righToLeft, cellStyle.Alignment, cellStyle.WrapMode));
        }
    }

    /// <summary>
    /// Customized implementation of the GetErrorIconBounds function in order to draw the potential
    /// error icon next to the up/down buttons and not on top of them.
    /// </summary>
    protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
    {
        Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
        errorIconBounds.X = errorIconBounds.Left;

        return errorIconBounds;
    }

    /// <summary>
    /// Custom implementation of the GetPreferredSize function.
    /// </summary>
    protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
    {
        return DataGridView == null
            ? new Size(-1, -1)
            : base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
    }
    #endregion

    #region Private

    private KryptonDataGridViewDomainUpDownEditingControl EditingDomainUpDown => DataGridView!.EditingControl as KryptonDataGridViewDomainUpDownEditingControl
        ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(DataGridView.EditingControl)));

    private void OnCommonChange()
    {
        if (DataGridView is { IsDisposed: false, Disposing: false })
        {
            if (RowIndex == -1)
            {
                DataGridView.InvalidateColumn(ColumnIndex);
            }
            else
            {
                DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }
    }

    private bool OwnsEditingDomainUpDown(int rowIndex) =>
        rowIndex != -1 && DataGridView is { EditingControl: KryptonDataGridViewDomainUpDownEditingControl control }
                       && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

    private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart) => (paintParts & paintPart) != 0;

    #endregion

    #region Internal
    internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
    {
        if ((align & ANY_RIGHT) != 0)
        {
            return HorizontalAlignment.Right;
        }
        else
        {
            return (align & ANY_CENTER) != 0 ? HorizontalAlignment.Center : HorizontalAlignment.Left;
        }
    }

    /// <summary>
    /// Type casted version of OwningColumn
    /// </summary>
    internal KryptonDataGridViewDomainUpDownColumn? KryptonOwningColumn => OwningColumn as KryptonDataGridViewDomainUpDownColumn;
    #endregion
}