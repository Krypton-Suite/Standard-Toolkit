#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Defines a KryptonMaskedTextBox cell type for the KryptonDataGridView control
/// </summary>
public class KryptonDataGridViewMaskedTextBoxCell : DataGridViewTextBoxCell
{
    #region Static Fields
    [ThreadStatic]
    private static KryptonMaskedTextBox _paintingMaskedTextBox;

    private const DataGridViewContentAlignment ANY_RIGHT = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
    private const DataGridViewContentAlignment ANY_CENTER = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
    private static readonly Type _defaultEditType = typeof(KryptonDataGridViewMaskedTextBoxEditingControl);
    private static readonly Type _defaultValueType = typeof(string);
    private static readonly Size _sizeLarge = new Size(10000, 10000);
    #endregion

    #region Instance Fields
    private char _promptChar;
    private bool _allowPromptAsInput;
    private bool _asciiOnly;
    private bool _beepOnError;
    private MaskFormat _cutCopyMaskFormat;
    private bool _hidePromptOnLeave;
    private bool _hideSelection;
    private InsertKeyMode _insertKeyMode;
    private string _mask;
    private char _passwordChar;
    private bool _rejectInputOnFirstFailure;
    private bool _resetOnPrompt;
    private bool _resetOnSpace;
    private bool _skipLiterals;
    private MaskFormat _textMaskFormat;
    private bool _useSystemPasswordChar;
    #endregion

    #region Identity
    /// <summary>
    /// Constructor for the KryptonDataGridViewMaskedTextBoxCell cell type
    /// </summary>
    public KryptonDataGridViewMaskedTextBoxCell()
    {
        // Create a thread specific KryptonMaskedTextBox control used for the painting of the non-edited cells
        if (_paintingMaskedTextBox == null)
        {
            _paintingMaskedTextBox = new KryptonMaskedTextBox();
            _paintingMaskedTextBox.SetLayoutDisplayPadding(new Padding(0, 0, 1, -1));
            _paintingMaskedTextBox.StateCommon.Border.Width = 0;
            _paintingMaskedTextBox.StateCommon.Border.Draw = InheritBool.False;
            _paintingMaskedTextBox.StateCommon.Back.Color1 = GlobalStaticValues.EMPTY_COLOR;
        }

        // Set the default values of the properties:
        _promptChar = '_';
        _allowPromptAsInput = true;
        _asciiOnly = false;
        _beepOnError = false;
        _cutCopyMaskFormat = MaskFormat.IncludeLiterals;
        _hidePromptOnLeave = false;
        _hideSelection = true;
        _insertKeyMode = InsertKeyMode.Default;
        _mask = string.Empty;
        _passwordChar = '\0';
        _rejectInputOnFirstFailure = false;
        _resetOnPrompt = true;
        _resetOnSpace = true;
        _skipLiterals = true;
        _textMaskFormat = MaskFormat.IncludeLiterals;
        _useSystemPasswordChar = false;
    }

    /// <summary>
    /// Returns a standard textual representation of the cell.
    /// </summary>
    public override string ToString() =>
        $"DataGridViewMaskedTextBoxCell {{ ColumnIndex={ColumnIndex.ToString(CultureInfo.CurrentCulture)}, RowIndex={RowIndex.ToString(CultureInfo.CurrentCulture)} }}";

    #endregion

    #region Public
    /// <summary>
    /// Define the type of the cell's editing control
    /// </summary>
    public override Type EditType => _defaultEditType;

    /// <summary>
    /// Returns the type of the cell's Value property
    /// </summary>
    public override Type ValueType => base.ValueType ?? _defaultValueType;

    /// <summary>
    /// The PromptChar property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue('_')]
    public char PromptChar
    {
        get => _promptChar;

        set
        {
            if (_promptChar != value)
            {
                SetPromptChar(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The AllowPromptAsInput property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(true)]
    public bool AllowPromptAsInput
    {
        get => _allowPromptAsInput;

        set
        {
            if (_allowPromptAsInput != value)
            {
                SetAllowPromptAsInput(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The AsciiOnly property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(false)]
    public bool AsciiOnly
    {
        get => _asciiOnly;

        set
        {
            if (_asciiOnly != value)
            {
                SetAsciiOnly(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The BeepOnError property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(true)]
    public bool BeepOnError
    {
        get => _beepOnError;

        set
        {
            if (_beepOnError != value)
            {
                SetBeepOnError(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The CutCopyMaskFormat property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(MaskFormat.IncludeLiterals)]
    public MaskFormat CutCopyMaskFormat
    {
        get => _cutCopyMaskFormat;

        set
        {
            if (_cutCopyMaskFormat != value)
            {
                SetCutCopyMaskFormat(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The HidePromptOnLeave property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(false)]
    public bool HidePromptOnLeave
    {
        get => _hidePromptOnLeave;

        set
        {
            if (_hidePromptOnLeave != value)
            {
                SetHidePromptOnLeave(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The HideSelection property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => _hideSelection;

        set
        {
            if (_hideSelection != value)
            {
                SetHideSelection(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The InsertKeyMode property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(InsertKeyMode.Default)]
    public InsertKeyMode InsertKeyMode
    {
        get => _insertKeyMode;

        set
        {
            if (_insertKeyMode != value)
            {
                SetInsertKeyMode(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The Mask property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue("")]
    public string Mask
    {
        get => _mask;

        set
        {
            if (_mask != value)
            {
                SetMask(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The PasswordChar property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue('\0')]
    public char PasswordChar
    {
        get => _passwordChar;

        set
        {
            if (_passwordChar != value)
            {
                SetPasswordChar(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The RejectInputOnFirstFailure property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(false)]
    public bool RejectInputOnFirstFailure
    {
        get => _rejectInputOnFirstFailure;

        set
        {
            if (_rejectInputOnFirstFailure != value)
            {
                SetRejectInputOnFirstFailure(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The ResetOnPrompt property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(true)]
    public bool ResetOnPrompt
    {
        get => _resetOnPrompt;

        set
        {
            if (_resetOnPrompt != value)
            {
                SetResetOnPrompt(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The ResetOnSpace property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(true)]
    public bool ResetOnSpace
    {
        get => _resetOnSpace;

        set
        {
            if (_resetOnSpace != value)
            {
                SetResetOnSpace(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The SkipLiterals property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(true)]
    public bool SkipLiterals
    {
        get => _skipLiterals;

        set
        {
            if (_skipLiterals != value)
            {
                SetSkipLiterals(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The TextMaskFormat property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(MaskFormat.IncludeLiterals)]
    public MaskFormat TextMaskFormat
    {
        get => _textMaskFormat;

        set
        {
            if (_textMaskFormat != value)
            {
                SetTextMaskFormat(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// The UseSystemPasswordChar property replicates the one from the KryptonMaskedTextBox control
    /// </summary>
    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
        get => _useSystemPasswordChar;

        set
        {
            if (_useSystemPasswordChar != value)
            {
                SetUseSystemPasswordChar(RowIndex, value);
                OnCommonChange();
            }
        }
    }

    /// <summary>
    /// Clones a DataGridViewMaskedTextBoxCell cell, copies all the custom properties.
    /// </summary>
    public override object Clone()
    {
        var dataGridViewCell = base.Clone() as KryptonDataGridViewMaskedTextBoxCell;
        if (dataGridViewCell != null)
        {
            dataGridViewCell.PromptChar = PromptChar;
            dataGridViewCell.AllowPromptAsInput = AllowPromptAsInput;
            dataGridViewCell.AsciiOnly = AsciiOnly;
            dataGridViewCell.BeepOnError = BeepOnError;
            dataGridViewCell.CutCopyMaskFormat = CutCopyMaskFormat;
            dataGridViewCell.HidePromptOnLeave = HidePromptOnLeave;
            dataGridViewCell.HideSelection = HideSelection;
            dataGridViewCell.InsertKeyMode = InsertKeyMode;
            dataGridViewCell.Mask = Mask;
            dataGridViewCell.PasswordChar = PasswordChar;
            dataGridViewCell.RejectInputOnFirstFailure = RejectInputOnFirstFailure;
            dataGridViewCell.ResetOnPrompt = ResetOnPrompt;
            dataGridViewCell.ResetOnSpace = ResetOnSpace;
            dataGridViewCell.SkipLiterals = SkipLiterals;
            dataGridViewCell.TextMaskFormat = TextMaskFormat;
            dataGridViewCell.UseSystemPasswordChar = UseSystemPasswordChar;
        }
        return dataGridViewCell!;
    }

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
            throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
        }

        if (dataGridView.EditingControl is KryptonMaskedTextBox maskedTextBox)
        {
            if (OwningColumn is KryptonDataGridViewMaskedTextBoxColumn)
            {
                if (maskedTextBox.Controls[0] is TextBox textBox)
                {
                    textBox.ClearUndo();
                }
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

        if (DataGridView!.EditingControl is KryptonMaskedTextBox maskedTextBox)
        {
            maskedTextBox.PromptChar = PromptChar;
            maskedTextBox.AllowPromptAsInput = AllowPromptAsInput;
            maskedTextBox.AsciiOnly = AsciiOnly;
            maskedTextBox.BeepOnError = BeepOnError;
            maskedTextBox.CutCopyMaskFormat = CutCopyMaskFormat;
            maskedTextBox.HidePromptOnLeave = HidePromptOnLeave;
            maskedTextBox.HideSelection = HideSelection;
            maskedTextBox.InsertKeyMode = InsertKeyMode;
            maskedTextBox.Mask = Mask;
            maskedTextBox.PasswordChar = PasswordChar;
            maskedTextBox.RejectInputOnFirstFailure = RejectInputOnFirstFailure;
            maskedTextBox.ResetOnPrompt = ResetOnPrompt;
            maskedTextBox.ResetOnSpace = ResetOnSpace;
            maskedTextBox.SkipLiterals = SkipLiterals;
            maskedTextBox.TextMaskFormat = TextMaskFormat;
            maskedTextBox.UseSystemPasswordChar = UseSystemPasswordChar;
            maskedTextBox.Text = initialFormattedValue as string ?? string.Empty;
        }
    }

    /// <summary>
    /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
    /// needs to relocate and/or resize the editing control.
    /// </summary>
    public override void PositionEditingControl(bool setLocation,
        bool setSize,
        Rectangle cellBounds,
        Rectangle cellClip,
        DataGridViewCellStyle cellStyle,
        bool singleVerticalBorderAdded,
        bool singleHorizontalBorderAdded,
        bool isFirstDisplayedColumn,
        bool isFirstDisplayedRow)
    {
        Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
            singleVerticalBorderAdded, singleHorizontalBorderAdded,
            isFirstDisplayedColumn, isFirstDisplayedRow);

        editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
        DataGridView!.EditingControl!.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
        DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
    }
    #endregion

    #region Protected
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

    private KryptonDataGridViewMaskedTextBoxEditingControl? EditingMaskedTextBox => DataGridView!.EditingControl as KryptonDataGridViewMaskedTextBoxEditingControl;

    private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
        DataGridViewCellStyle cellStyle)
    {
        // Adjust the vertical location of the editing control:
        var preferredHeight = _paintingMaskedTextBox.GetPreferredSize(_sizeLarge).Height + 2;
        if (preferredHeight < editingControlBounds.Height)
        {
            switch (cellStyle.Alignment)
            {
                case DataGridViewContentAlignment.MiddleLeft:
                case DataGridViewContentAlignment.MiddleCenter:
                case DataGridViewContentAlignment.MiddleRight:
                    editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                case DataGridViewContentAlignment.BottomCenter:
                case DataGridViewContentAlignment.BottomRight:
                    editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                    break;
            }
        }

        return editingControlBounds;
    }

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

    private bool OwnsEditingMaskedTextBox(int rowIndex) =>
        rowIndex != -1
        && DataGridView is { EditingControl: KryptonDataGridViewMaskedTextBoxEditingControl control }
        && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);

    private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart) => (paintParts & paintPart) != 0;

    #endregion

    #region Internal
    internal void SetPromptChar(int rowIndex, char value)
    {
        _promptChar = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.PromptChar = value;
        }
    }

    internal void SetAllowPromptAsInput(int rowIndex, bool value)
    {
        _allowPromptAsInput = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.AllowPromptAsInput = value;
        }
    }

    internal void SetAsciiOnly(int rowIndex, bool value)
    {
        _asciiOnly = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.AsciiOnly = value;
        }
    }

    internal void SetBeepOnError(int rowIndex, bool value)
    {
        _beepOnError = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.BeepOnError = value;
        }
    }

    internal void SetCutCopyMaskFormat(int rowIndex, MaskFormat value)
    {
        _cutCopyMaskFormat = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.CutCopyMaskFormat = value;
        }
    }

    internal void SetHidePromptOnLeave(int rowIndex, bool value)
    {
        _hidePromptOnLeave = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.HidePromptOnLeave = value;
        }
    }

    internal void SetHideSelection(int rowIndex, bool value)
    {
        _hideSelection = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.HideSelection = value;
        }
    }

    internal void SetInsertKeyMode(int rowIndex, InsertKeyMode value)
    {
        _insertKeyMode = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.InsertKeyMode = value;
        }
    }

    internal void SetMask(int rowIndex, string value)
    {
        _mask = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.Mask = value;
        }
    }

    internal void SetPasswordChar(int rowIndex, char value)
    {
        _passwordChar = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.PasswordChar = value;
        }
    }

    internal void SetRejectInputOnFirstFailure(int rowIndex, bool value)
    {
        _rejectInputOnFirstFailure = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.RejectInputOnFirstFailure = value;
        }
    }

    internal void SetResetOnPrompt(int rowIndex, bool value)
    {
        _resetOnPrompt = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.ResetOnPrompt = value;
        }
    }

    internal void SetResetOnSpace(int rowIndex, bool value)
    {
        _resetOnSpace = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.ResetOnSpace = value;
        }
    }

    internal void SetSkipLiterals(int rowIndex, bool value)
    {
        _skipLiterals = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.SkipLiterals = value;
        }
    }

    internal void SetTextMaskFormat(int rowIndex, MaskFormat value)
    {
        _textMaskFormat = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.TextMaskFormat = value;
        }
    }

    internal void SetUseSystemPasswordChar(int rowIndex, bool value)
    {
        _useSystemPasswordChar = value;
        if (OwnsEditingMaskedTextBox(rowIndex))
        {
            EditingMaskedTextBox!.UseSystemPasswordChar = value;
        }
    }

    internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align) => ANY_RIGHT.HasFlag(align)
        ? HorizontalAlignment.Right
        : ANY_CENTER.HasFlag(align)
            ? HorizontalAlignment.Center
            : HorizontalAlignment.Left;
    #endregion

}