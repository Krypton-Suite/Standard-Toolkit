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
/// 
/// </summary>
public partial class VisualInputBoxForm : KryptonForm
{
    #region Instance Fields

    private readonly KryptonInputBoxData _inputBoxData;

    #endregion

    #region Identity

    /// <summary>
    /// 
    /// </summary>
    public VisualInputBoxForm()
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();
    }

    /// <summary>Initializes a new instance of the <see cref="VisualInputBoxForm" /> class.</summary>
    /// <param name="inputBoxData">The input box data.</param>
    public VisualInputBoxForm(KryptonInputBoxData inputBoxData)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _inputBoxData = inputBoxData;

        // Update contents to match requirements
        UpdateText();

        UpdateCue();

        UpdateButtons();
    }

    #endregion

    #region Implementation

    internal static string InternalShow(KryptonInputBoxData inputBoxData)
    {
        // If do not have an owner passed in then get the active window and use that instead
        IWin32Window? showOwner = inputBoxData.Owner ?? FromHandle(PI.GetActiveWindow());

        // Show input box window as a modal dialog and then dispose of it afterwards
        using var ib = new VisualInputBoxForm(inputBoxData);
        ib.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

        return ib.ShowDialog(showOwner) == DialogResult.OK
            ? ib.InputResponse
            : string.Empty;
    }

    internal string InputResponse => _textBoxResponse.Text;

    private void UpdateText()
    {
        Text = _inputBoxData.Caption;
        _labelPrompt.Text = _inputBoxData.Prompt;
        _textBoxResponse.Text = _inputBoxData.DefaultResponse;
        _textBoxResponse.UseSystemPasswordChar = _inputBoxData.UsePasswordOption ?? false;
    }

    private void UpdateCue()
    {
        _textBoxResponse.CueHint.CueHintText = _inputBoxData.CueText;

        if (_inputBoxData.CueColor != null || _inputBoxData.CueColor != Color.Transparent || _inputBoxData.CueColor != GlobalStaticValues.EMPTY_COLOR)
        {
            _textBoxResponse.CueHint.Color1 = _inputBoxData.CueColor ?? Color.Gray;
        }

        if (_inputBoxData.CueTypeface != null)
        {
            _textBoxResponse.CueHint.Font = _inputBoxData.CueTypeface ?? KryptonManager.CurrentGlobalPalette.BaseFont;
        }
    }

    private void UpdateButtons()
    {
        _buttonOk.Text = KryptonManager.Strings.GeneralStrings.OK;
        _buttonCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;
    }

    private void Response_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Enter:
                _buttonOk.PerformClick();
                break;
            case Keys.Escape:
                _buttonCancel.PerformClick();
                break;
        }
    }

    #endregion
}