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
/// Krypton-themed modal dialog for editing standard .NET format strings at design time.
/// </summary>
/// <remarks>
/// <para>
/// Opened by <see cref="KryptonDesignerFormatStringEditor"/> when the property grid edits
/// <see cref="DataGridViewCellStyle.Format"/> (and <see cref="DataGridViewCellStyle.NullValue"/>)
/// or <see cref="ListControl.FormatString"/>. Changes are written back to the live designer
/// instance when the user clicks OK; Cancel leaves the original values untouched.
/// </para>
/// <para>
/// Layout and footer chrome (<c>kpnlContent</c>, <c>kpnlButtonBar</c>) live in the
/// <c>*.Designer.cs</c> partial. The format-type combo indices are:
/// <list type="table">
/// <listheader><term>Index</term><description>Preset</description></listheader>
/// <item><term>0</term><description>No formatting (empty string)</description></item>
/// <item><term>1</term><description>Numeric — <c>N2</c></description></item>
/// <item><term>2</term><description>Decimal — <c>D0</c> (integer, optional digit count)</description></item>
/// <item><term>3</term><description>Currency — <c>C2</c></description></item>
/// <item><term>4</term><description>Date — <c>d</c> (short date pattern)</description></item>
/// <item><term>5</term><description>Time — <c>t</c> (short time pattern)</description></item>
/// <item><term>6</term><description>Percent — <c>P2</c></description></item>
/// <item><term>7</term><description>Custom — user-edited format string, unchanged by the combo</description></item>
/// </list>
/// </para>
/// </remarks>
internal partial class VisualDesignerFormatStringEditorForm : KryptonForm
{
    #region Instance Fields

    // Designer context is only available on the runtime ctor; the parameterless ctor exists so the
    // form can be opened in the WinForms designer for layout work.
    private readonly ITypeDescriptorContext? _context;

    // Exactly one of these is set from context.Instance when the editor is invoked at design time.
    private readonly DataGridViewCellStyle? _cellStyle;
    private readonly ListControl? _listControl;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="VisualDesignerFormatStringEditorForm"/> class for the WinForms designer.
    /// </summary>
    public VisualDesignerFormatStringEditorForm()
    {
        SetInheritedControlOverride();
        InitializeComponent();
        ConfigureDesignerChrome();
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualDesignerFormatStringEditorForm"/> class.
    /// </summary>
    /// <param name="context">Designer context.</param>
    public VisualDesignerFormatStringEditorForm(ITypeDescriptorContext context)
    {
        SetInheritedControlOverride();
        _context = context;
        _cellStyle = context.Instance as DataGridViewCellStyle;
        _listControl = context.Instance as ListControl;

        InitializeComponent();
        ConfigureDesignerChrome();

        // Fixed dialog: size is set in design units then scaled for the hosting monitor DPI.
        Text = @"Format String Editor";
        ControlBox = false;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(420, 280));
        MinimumSize = ClientSize;
        MaximumSize = new Size(ClientSize.Width + 1, ClientSize.Height + 1);

        LoadInitialValues();

        // Match the edited control's palette (owner local palette, else global manager palette).
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

    /// <summary>
    /// Wires shared editor footer chrome (OK/Cancel, optional theme selector, DPI sizing).
    /// </summary>
    private void ConfigureDesignerChrome()
    {
        InternalDesignerEditorFormChrome.Apply(this, kpnlContent, kpnlButtonBar);
        kpnlButtonBar.OkButton.Values.Text = KryptonManager.Strings.GeneralStrings.OK;
        kpnlButtonBar.CancelButton.Values.Text = KryptonManager.Strings.GeneralStrings.Cancel;

        // Commit edits before the dialog closes with DialogResult.OK (CancelButton uses DialogResult.Cancel).
        kpnlButtonBar.OkButton.Click += (_, _) => PushChanges();
    }

    /// <summary>
    /// Hydrates controls from the designer target and selects the best-matching format-type preset.
    /// </summary>
    private void LoadInitialValues()
    {
        var formatString = _cellStyle is not null ? _cellStyle.Format : _listControl?.FormatString ?? string.Empty;
        _formatStringTextBox.Text = formatString;

        // NullValue is only meaningful for DataGridView cell styles, not ListControl.FormatString.
        _nullValueTextBox.Text = _cellStyle?.NullValue?.ToString() ?? string.Empty;
        _nullValueTextBox.Enabled = _cellStyle is not null;

        _formatTypeCombo.SelectedIndex = GuessFormatTypeIndex(formatString);

        // Changing the combo replaces the text box with a standard preset (Custom leaves text unchanged).
        _formatTypeCombo.SelectedIndexChanged += (_, _) => ApplyPresetFormat();
        UpdatePreview();
    }

    /// <summary>
    /// Maps an existing format string to a <see cref="_formatTypeCombo"/> index for display only.
    /// Unrecognized strings select Custom (index 7) so the raw format is preserved in the text box.
    /// </summary>
    /// <param name="formatString">Current format from the edited property.</param>
    /// <returns>Combo index; see class remarks for the index catalog.</returns>
    private static int GuessFormatTypeIndex(string formatString)
    {
        if (string.IsNullOrEmpty(formatString))
        {
            return 0;
        }

        // Decimal is checked before the switch because "D"/"d" overlap with date patterns — see
        // IsDecimalFormatSpecifier.
        if (IsDecimalFormatSpecifier(formatString))
        {
            return 2;
        }

        return formatString switch
        {
            "N" or "n" or "N0" or "N2" => 1,
            "C" or "c" or "C0" or "C2" => 3,
            "d" => 4,
            "T" or "t" => 5,
            "P" or "p" or "P0" or "P2" => 6,
            _ => 7
        };
    }

    /// <summary>
    /// Returns <c>true</c> when <paramref name="formatString"/> is a standard decimal (integer) format.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In composite format strings, <c>d</c>/<c>D</c> mean different things depending on the
    /// formatted value's type: short date / long date for <see cref="DateTime"/>, or fixed-point
    /// integer formatting for numeric types. This editor only sees the format specifier string, not
    /// the column/value type, so guessing is heuristic.
    /// </para>
    /// <para>
    /// Rules used here:
    /// <list type="bullet">
    /// <item><description><c>d</c> alone → treated as short date (Date preset), not decimal.</description></item>
    /// <item><description><c>D</c> alone, or <c>D</c>/<c>d</c> followed only by digits (<c>D0</c>, <c>D2</c>, …) → decimal.</description></item>
    /// </list>
    /// Long-date <c>D</c> on a <see cref="DateTime"/> column therefore falls through to Custom unless
    /// the user picks the Date preset manually.
    /// </para>
    /// </remarks>
    private static bool IsDecimalFormatSpecifier(string formatString)
    {
        if (formatString.Length == 0)
        {
            return false;
        }

        var specifier = formatString[0];
        if (specifier != 'D' && specifier != 'd')
        {
            return false;
        }

        // Lowercase "d" alone is the short-date preset; "D", "D0", "D2", etc. are integer decimal formats.
        if (formatString.Length == 1)
        {
            return specifier == 'D';
        }

        // Optional precision digits after the specifier (e.g. D0, D5).
        for (var i = 1; i < formatString.Length; i++)
        {
            if (!char.IsDigit(formatString[i]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Applies the standard preset for the selected format type into the format-string text box.
    /// </summary>
    private void ApplyPresetFormat()
    {
        _formatStringTextBox.Text = _formatTypeCombo.SelectedIndex switch
        {
            0 => string.Empty,
            1 => "N2",
            2 => "D0",
            3 => "C2",
            4 => "d",
            5 => "t",
            6 => "P2",
            _ => _formatStringTextBox.Text
        };
        UpdatePreview();
    }

    /// <summary>
    /// Renders a live preview using the current culture and a fixed numeric sample value.
    /// </summary>
    /// <remarks>
    /// Uses <c>12345.6789</c> so N/C/P formats show fractional digits; D formats truncate toward
    /// integer display. Date/time presets may look incorrect in preview because the sample is not a
    /// <see cref="DateTime"/> — that limitation is acceptable for a quick validity check only.
    /// </remarks>
    private void UpdatePreview()
    {
        var sample = 12345.6789;
        try
        {
            _previewLabel.Values.Text = string.Format(
                CultureInfo.CurrentCulture,
                @"Preview: {0:" + _formatStringTextBox.Text + @"}",
                sample);
        }
        catch (FormatException)
        {
            _previewLabel.Values.Text = @"Preview: (invalid format)";
        }
    }

    /// <summary>
    /// Writes the edited format string (and optional null value) back to the designer instance.
    /// </summary>
    /// <remarks>
    /// Called from the OK button click handler. <see cref="KryptonDesignerFormatStringEditor"/>
    /// raises <see cref="ITypeDescriptorContext.OnComponentChanged"/> after the dialog returns OK.
    /// </remarks>
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
