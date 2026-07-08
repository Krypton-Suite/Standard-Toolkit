#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Krypton-themed designer editor for format strings on data-bound controls.
/// </summary>
public sealed class KryptonDesignerFormatStringEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        UITypeEditorEditStyle.Modal;

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        using var form = new KryptonDesignerFormatStringEditorForm(context);
        if (editorService.ShowDialog(form) == DialogResult.OK)
        {
            context.OnComponentChanged();
        }

        return value;
    }
    #endregion
}

/// <summary>
/// Krypton-themed format-string editor dialog.
/// </summary>
internal sealed class KryptonDesignerFormatStringEditorForm : KryptonForm
{
    #region Instance Fields
    private readonly ITypeDescriptorContext _context;
    private readonly DataGridViewCellStyle? _cellStyle;
    private readonly ListControl? _listControl;
    private readonly KryptonComboBox _formatTypeCombo;
    private readonly KryptonTextBox _formatStringTextBox;
    private readonly KryptonTextBox _nullValueTextBox;
    private readonly KryptonLabel _previewLabel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerFormatStringEditorForm"/> class.
    /// </summary>
    /// <param name="context">Designer context.</param>
    public KryptonDesignerFormatStringEditorForm(ITypeDescriptorContext context)
    {
        _context = context;
        _cellStyle = context.Instance as DataGridViewCellStyle;
        _listControl = context.Instance as ListControl;

        Text = @"Format String Editor";
        ControlBox = false;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(420, 280));
        MinimumSize = ClientSize;
        MaximumSize = new Size(ClientSize.Width + 1, ClientSize.Height + 1);

        var formatTypeLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Format type:" } };
        _formatTypeCombo = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Top };
        _formatTypeCombo.Items.AddRange(
        [
            @"No formatting",
            @"Numeric",
            @"Currency",
            @"Date",
            @"Time",
            @"Percent",
            @"Custom"
        ]);

        var formatStringLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Format string:" } };
        _formatStringTextBox = new KryptonTextBox { Dock = DockStyle.Top };
        _formatStringTextBox.TextChanged += (_, _) => UpdatePreview();

        var nullValueLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Null value:" } };
        _nullValueTextBox = new KryptonTextBox { Dock = DockStyle.Top };

        _previewLabel = new KryptonLabel
        {
            AutoSize = false,
            Height = KryptonDesignerEditorDpi.Scale(this, 40),
            Values = { Text = @"Preview:" }
        };

        var okButton = new KryptonButton
        {
            DialogResult = DialogResult.OK,
            Values = { Text = KryptonManager.Strings.GeneralStrings.OK }
        };
        okButton.Click += (_, _) => PushChanges();

        var cancelButton = new KryptonButton
        {
            DialogResult = DialogResult.Cancel,
            Values = { Text = KryptonManager.Strings.GeneralStrings.Cancel }
        };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 7
        };
        layout.RowStyles.Add(new RowStyle());
        layout.RowStyles.Add(new RowStyle());
        layout.RowStyles.Add(new RowStyle());
        layout.RowStyles.Add(new RowStyle());
        layout.RowStyles.Add(new RowStyle());
        layout.RowStyles.Add(new RowStyle());
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        layout.Controls.Add(formatTypeLabel, 0, 0);
        layout.Controls.Add(_formatTypeCombo, 0, 1);
        layout.Controls.Add(formatStringLabel, 0, 2);
        layout.Controls.Add(_formatStringTextBox, 0, 3);
        layout.Controls.Add(nullValueLabel, 0, 4);
        layout.Controls.Add(_nullValueTextBox, 0, 5);
        layout.Controls.Add(_previewLabel, 0, 6);

        var buttonBar = KryptonDesignerEditorButtonBar.Create(this, okButton, cancelButton);
        Controls.Add(KryptonDesignerEditorContentPanel.Create(this, layout));
        Controls.Add(buttonBar);

        AcceptButton = okButton;
        CancelButton = cancelButton;

        LoadInitialValues();
        KryptonDesignerEditorPalette.ApplyDesignerPalette(this, context);
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        KryptonDesignerEditorDpi.Configure(this);
        base.OnLoad(e);
    }

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        KryptonDesignerEditorDpi.ApplyOnShown(this);
    }
    #endregion

    #region Implementation
    private void LoadInitialValues()
    {
        var formatString = _cellStyle is not null ? _cellStyle.Format : _listControl?.FormatString ?? string.Empty;
        _formatStringTextBox.Text = formatString;
        _nullValueTextBox.Text = _cellStyle?.NullValue?.ToString() ?? string.Empty;
        _nullValueTextBox.Enabled = _cellStyle is not null;

        _formatTypeCombo.SelectedIndex = GuessFormatTypeIndex(formatString);
        _formatTypeCombo.SelectedIndexChanged += (_, _) => ApplyPresetFormat();
        UpdatePreview();
    }

    private static int GuessFormatTypeIndex(string formatString)
    {
        if (string.IsNullOrEmpty(formatString))
        {
            return 0;
        }

        if (formatString is "C" or "c" or "C0" or "C2")
        {
            return 2;
        }

        if (formatString is "N" or "n" or "N0" or "N2")
        {
            return 1;
        }

        if (formatString is "D" or "d" or "d" or "D")
        {
            return 3;
        }

        if (formatString is "T" or "t")
        {
            return 4;
        }

        if (formatString is "P" or "p" or "P0" or "P2")
        {
            return 5;
        }

        return 6;
    }

    private void ApplyPresetFormat()
    {
        _formatStringTextBox.Text = _formatTypeCombo.SelectedIndex switch
        {
            0 => string.Empty,
            1 => "N2",
            2 => "C2",
            3 => "d",
            4 => "t",
            5 => "P2",
            _ => _formatStringTextBox.Text
        };
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        var sample = 12345.6789;
        try
        {
            _previewLabel.Values.Text = string.Format(
                CultureInfo.CurrentCulture,
                @"Preview: {0:" + _formatStringTextBox.Text + "}",
                sample);
        }
        catch (FormatException)
        {
            _previewLabel.Values.Text = @"Preview: (invalid format)";
        }
    }

    private void PushChanges()
    {
        if (_cellStyle is not null)
        {
            _cellStyle.Format = _formatStringTextBox.Text;
            _cellStyle.NullValue = _nullValueTextBox.Text;
        }
        else if (_listControl is not null)
        {
            _listControl.FormatString = _formatStringTextBox.Text;
        }
    }
    #endregion
}
