#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class KryptonDesignerMaskedTextBoxTextEditorDropDown
{
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _maskedTextBox = new KryptonMaskedTextBox
        {
            Dock = DockStyle.Fill,
            TextMaskFormat = MaskFormat.IncludePromptAndLiterals,
            ResetOnPrompt = true,
            SkipLiterals = true,
            ResetOnSpace = true
        };

        BackColor = SystemColors.Control;
        BorderStyle = BorderStyle.FixedSingle;
        Padding = new Padding(16);
        Size = new Size(180, 52);
        Controls.Add(_maskedTextBox);
        _maskedTextBox.KeyDown += (_, _) => { };
        Name = nameof(KryptonDesignerMaskedTextBoxTextEditorDropDown);
    }

    #endregion

    private KryptonMaskedTextBox _maskedTextBox;
}
