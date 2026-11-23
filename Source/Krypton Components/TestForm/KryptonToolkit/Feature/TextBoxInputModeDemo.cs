#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonTextBox.InputMode"/> digit / letter / alphanumeric filtering (issue #4417).
/// </summary>
public partial class TextBoxInputModeDemo : KryptonForm
{
    public TextBoxInputModeDemo()
    {
        InitializeComponent();
    }

    private void TextBoxInputModeDemo_Load(object? sender, EventArgs e)
    {
        kcmbMode.DataSource = Enum.GetValues(typeof(KryptonTextBoxInputMode));
        kcmbMode.SelectedItem = KryptonTextBoxInputMode.Digits;
        ApplyLiveMode();
        Log(@"Ready. Type or paste into each box. Digits / Letters / Alphanumeric reject other characters; Any accepts everything.");
    }

    private void kcmbMode_SelectedIndexChanged(object? sender, EventArgs e) => ApplyLiveMode();

    private void btnPasteSample_Click(object? sender, EventArgs e)
    {
        Clipboard.SetText(@"Ab12-Xy! 99");
        ktbLive.Paste();
        Log(@"Clipboard set to 'Ab12-Xy! 99' and Paste() called on the live InputMode box.");
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        ktbAny.Text = string.Empty;
        ktbDigits.Text = string.Empty;
        ktbLetters.Text = string.Empty;
        ktbAlphanumeric.Text = string.Empty;
        ktbLive.Text = string.Empty;
        tbNative.Text = string.Empty;
        Log(@"Cleared all text boxes.");
    }

    private void ApplyLiveMode()
    {
        if (kcmbMode.SelectedItem is KryptonTextBoxInputMode mode)
        {
            ktbLive.InputMode = mode;
            Log($"Live InputMode = {mode}");
        }
    }

    private void Log(string message)
    {
        string line = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
        krtbLog.AppendText(line);
    }
}
