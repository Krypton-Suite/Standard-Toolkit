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
/// Krypton-themed designer editor for masked text properties.
/// </summary>
public sealed class KryptonDesignerMaskedTextBoxTextEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance is not null ? UITypeEditorEditStyle.DropDown : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        using var dropDown = new KryptonDesignerMaskedTextBoxTextEditorDropDown(context.Instance, value as string);
        editorService.DropDownControl(dropDown);
        return dropDown.Value ?? value;
    }

    /// <inheritdoc />
    public override bool GetPaintValueSupported(ITypeDescriptorContext? context) => false;
    #endregion
}

/// <summary>
/// Drop-down host for editing masked text at design time.
/// </summary>
internal sealed class KryptonDesignerMaskedTextBoxTextEditorDropDown : UserControl
{
    #region Instance Fields
    private readonly KryptonMaskedTextBox _maskedTextBox;
    private bool _cancel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerMaskedTextBoxTextEditorDropDown"/> class.
    /// </summary>
    /// <param name="instance">Component being edited.</param>
    /// <param name="value">Current property value.</param>
    public KryptonDesignerMaskedTextBoxTextEditorDropDown(object instance, string? value)
    {
        _maskedTextBox = new KryptonMaskedTextBox
        {
            Dock = DockStyle.Fill,
            TextMaskFormat = MaskFormat.IncludePromptAndLiterals,
            ResetOnPrompt = true,
            SkipLiterals = true,
            ResetOnSpace = true
        };

        ConfigureFromInstance(instance, value);

        BackColor = SystemColors.Control;
        BorderStyle = BorderStyle.FixedSingle;
        Padding = new Padding(16);
        Size = new Size(180, 52);
        Controls.Add(_maskedTextBox);
        _maskedTextBox.KeyDown += (_, _) => { };
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the edited value, or <see langword="null"/> when editing was cancelled.
    /// </summary>
    public string? Value
    {
        get
        {
            if (_cancel)
            {
                return null;
            }

            return _maskedTextBox.Text;
        }
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override bool ProcessDialogKey(Keys keyData)
    {
        if (keyData == Keys.Escape)
        {
            _cancel = true;
        }

        return base.ProcessDialogKey(keyData);
    }
    #endregion

    #region Implementation
    private void ConfigureFromInstance(object instance, string? value)
    {
        switch (instance)
        {
            case KryptonMaskedTextBox maskedTextBox:
                _maskedTextBox.Mask = maskedTextBox.Mask;
                _maskedTextBox.Culture = maskedTextBox.Culture;
                _maskedTextBox.Text = value ?? maskedTextBox.Text;
                break;
            case MaskedTextBox maskedTextBox:
                _maskedTextBox.Mask = maskedTextBox.Mask;
                _maskedTextBox.Culture = maskedTextBox.Culture;
                _maskedTextBox.Text = value ?? maskedTextBox.Text;
                break;
            default:
                _maskedTextBox.Text = value ?? string.Empty;
                break;
        }
    }
    #endregion
}
