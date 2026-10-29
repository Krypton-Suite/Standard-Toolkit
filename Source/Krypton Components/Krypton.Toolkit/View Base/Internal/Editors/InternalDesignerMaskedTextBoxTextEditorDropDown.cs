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
/// Drop-down host for editing masked text at design time.
/// </summary>
internal partial class InternalDesignerMaskedTextBoxTextEditorDropDown : UserControl
{
    #region Instance Fields
    private bool _cancel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="InternalDesignerMaskedTextBoxTextEditorDropDown"/> class for the WinForms designer.
    /// </summary>
    public InternalDesignerMaskedTextBoxTextEditorDropDown()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="InternalDesignerMaskedTextBoxTextEditorDropDown"/> class.
    /// </summary>
    /// <param name="instance">Component being edited.</param>
    /// <param name="value">Current property value.</param>
    public InternalDesignerMaskedTextBoxTextEditorDropDown(object instance, string? value)
    {
        InitializeComponent();
        ConfigureFromInstance(instance, value);
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
