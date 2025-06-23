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
/// Hosts a collection of KryptonDataGridViewMaskedTextBoxCell cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewMaskedTextBoxColumn), "ToolboxBitmaps.KryptonMaskedTextBox.bmp")]
public class KryptonDataGridViewMaskedTextBoxColumn : KryptonDataGridViewIconColumn
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewMaskedTextBoxColumn class.
    /// </summary>
    public KryptonDataGridViewMaskedTextBoxColumn()
        : base(new KryptonDataGridViewMaskedTextBoxCell())
    {
    }

    /// <summary>
    /// Returns a standard compact string representation of the column.
    /// </summary>
    public override string ToString()
    {
        var builder = new StringBuilder(0x40);
        builder.Append("KryptonDataGridViewMaskedTextBoxColumn { Name=");
        // ReSharper disable RedundantBaseQualifier
        builder.Append(base.Name);
        builder.Append(", Index=");
        builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
        // ReSharper restore RedundantBaseQualifier
        builder.Append(" }");
        return builder.ToString();
    }

    /// <summary>
    /// Create a cloned copy of the column.
    /// </summary>
    /// <returns></returns>
    public override object Clone()
    {
        var cloned = base.Clone() as KryptonDataGridViewMaskedTextBoxColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("cloned"));

        return cloned;
    }
    #endregion

    #region Public
    /// <summary>
    /// Represents the implicit cell that gets cloned when adding rows to the grid.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate
    {
        get => base.CellTemplate;
        set
        {
            if ((value != null) && (value is not KryptonDataGridViewMaskedTextBoxCell cell))
            {
                throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewMaskedTextBoxCell or derive from it.");
            }

            base.CellTemplate = value;
        }
    }

    /// <summary>
    /// Replicates the PromptChar property of the KryptonDataGridViewMaskedTextBoxCell cell type.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates the character used as the placeholder.")]
    [DefaultValue('_')]
    public char PromptChar
    {
        get =>
            MaskedTextBoxCellTemplate?.PromptChar ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.PromptChar = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetPromptChar(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether PromptChar can be entered as valid data by the user.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the prompt character is valid as input.")]
    [DefaultValue(true)]
    public bool AllowPromptAsInput
    {
        get =>
            MaskedTextBoxCellTemplate?.AllowPromptAsInput ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.AllowPromptAsInput = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetAllowPromptAsInput(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the MaskedTextBox control accepts characters outside of the ASCII character set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether only Ascii characters are valid as input.")]
    [DefaultValue(false)]
    public bool AsciiOnly
    {
        get =>
            MaskedTextBoxCellTemplate?.AsciiOnly ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.AsciiOnly = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetAsciiOnly(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control will beep when an invalid character is typed.")]
    [DefaultValue(false)]
    public bool BeepOnError
    {
        get =>
            MaskedTextBoxCellTemplate?.BeepOnError ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.BeepOnError = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetBeepOnError(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the text to be copied to the clipboard includes literals and/or prompt characters.")]
    [DefaultValue(MaskFormat.IncludeLiterals)]
    public MaskFormat CutCopyMaskFormat
    {
        get =>
            MaskedTextBoxCellTemplate?.CutCopyMaskFormat ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.CutCopyMaskFormat = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetCutCopyMaskFormat(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether prompt characters are Displayed when the control does not have focus.")]
    [DefaultValue(false)]
    public bool HidePromptOnLeave
    {
        get =>
            MaskedTextBoxCellTemplate?.HidePromptOnLeave ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.HidePromptOnLeave = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetHidePromptOnLeave(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates that the selection should be hidden when the edit control loses focus.")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get =>
            MaskedTextBoxCellTemplate?.HideSelection ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.HideSelection = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetHideSelection(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets the text insertion mode of the masked text box control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates the masked text box input character typing mode.")]
    [DefaultValue(InsertKeyMode.Default)]
    public InsertKeyMode InsertKeyMode
    {
        get =>
            MaskedTextBoxCellTemplate?.InsertKeyMode ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.InsertKeyMode = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetInsertKeyMode(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets the input mask to use at run time. 
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Sets the string governing the input allowed for the control.")]
    [DefaultValue("")]
    public string Mask
    {
        get =>
            MaskedTextBoxCellTemplate == null
                ? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                : MaskedTextBoxCellTemplate.Mask;
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.Mask = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetMask(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a the character to display for password input for single-line edit controls.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates the character to display for password input for single-line edit controls.")]
    [DefaultValue('\0')]
    public char PasswordChar
    {
        get =>
            MaskedTextBoxCellTemplate?.PasswordChar ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.PasswordChar = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetPasswordChar(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"If true, the input is rejected whenever a character fails to comply with the mask; otherwise, characters in the text area are processed one by one as individual inputs.")]
    [DefaultValue(false)]
    public bool RejectInputOnFirstFailure
    {
        get =>
            MaskedTextBoxCellTemplate?.RejectInputOnFirstFailure ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.RejectInputOnFirstFailure = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetRejectInputOnFirstFailure(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that determines how an input character that matches the prompt character should be handled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether to reset and skip the current position if editable, when the input characters has the same value as the prompt.")]
    [DefaultValue(true)]
    public bool ResetOnPrompt
    {
        get =>
            MaskedTextBoxCellTemplate?.ResetOnPrompt ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.ResetOnPrompt = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetResetOnPrompt(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that determines how a space input character should be handled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether to reset and skip the current position if editable, when the input is the space character.")]
    [DefaultValue(true)]
    public bool ResetOnSpace
    {
        get =>
            MaskedTextBoxCellTemplate?.ResetOnSpace ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.ResetOnSpace = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetResetOnSpace(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user is allowed to reenter literal values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Specifies whether to skip the current position if non-editable and the input character has the same value as the literal at that position.")]
    [DefaultValue(true)]
    public bool SkipLiterals
    {
        get =>
            MaskedTextBoxCellTemplate?.SkipLiterals ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.SkipLiterals = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetSkipLiterals(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the string returned from the Text property includes literal and/or prompt characters.")]
    [DefaultValue(MaskFormat.IncludeLiterals)]
    public MaskFormat TextMaskFormat
    {
        get =>
            MaskedTextBoxCellTemplate?.TextMaskFormat ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.TextMaskFormat = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetTextMaskFormat(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the text in the edit control should appear as the default password character.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the text in the edit control should appear as the default password character.")]
    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
        get =>
            MaskedTextBoxCellTemplate?.UseSystemPasswordChar ?? throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        set
        {
            if (MaskedTextBoxCellTemplate == null)
            {
                throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
            }

            // Update the template cell so that subsequent cloned cells use the new value.
            MaskedTextBoxCellTemplate.UseSystemPasswordChar = value;
            if (DataGridView != null)
            {
                // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Be careful not to unshare rows unnecessarily. 
                    // This could have severe performance repercussions.
                    DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is KryptonDataGridViewMaskedTextBoxCell dataGridViewCell)
                    {
                        dataGridViewCell.SetUseSystemPasswordChar(rowIndex, value);
                    }
                }

                DataGridView.InvalidateColumn(Index);
            }
        }
    }
    #endregion

    #region Private
    /// <summary>
    /// Small utility function that returns the template cell as a KryptonDataGridViewMaskedTextBoxCell
    /// </summary>
    private KryptonDataGridViewMaskedTextBoxCell? MaskedTextBoxCellTemplate => CellTemplate as KryptonDataGridViewMaskedTextBoxCell;

    #endregion

}