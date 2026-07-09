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
/// Krypton-themed format-string editor dialog.
/// </summary>
internal partial class KryptonDesignerFormatStringEditorForm : KryptonForm
{
    #region Instance Fields
    private readonly ITypeDescriptorContext _context;
    private readonly DataGridViewCellStyle? _cellStyle;
    private readonly ListControl? _listControl;
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

        InitializeComponent();

        Text = @"Format String Editor";
        ControlBox = false;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(420, 280));
        MinimumSize = ClientSize;
        MaximumSize = new Size(ClientSize.Width + 1, ClientSize.Height + 1);

        AcceptButton = _buttonOk;
        CancelButton = _buttonCancel;

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
