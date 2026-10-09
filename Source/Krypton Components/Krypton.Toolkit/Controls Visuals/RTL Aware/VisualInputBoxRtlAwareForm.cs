#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualInputBoxRtlAwareForm : KryptonForm
{
    #region Instance Fields

    private readonly KryptonInputBoxData _inputBoxData;

    #endregion

    #region Identity

    public VisualInputBoxRtlAwareForm()
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();
    }

    public VisualInputBoxRtlAwareForm(KryptonInputBoxData inputBoxData)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        _inputBoxData = inputBoxData;

        InitializeComponent();

        // Update contents to match requirements
        UpdateText();

        UpdateCue();

        UpdateButtons();
    }

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        UpdatePromptLayout();
    }

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        UpdatePromptLayout();
    }

    #endregion

    #region Implementation

    internal static string InternalShow(KryptonInputBoxData inputBoxData)
    {
        // If do not have an owner passed in then get the active window and use that instead
        IWin32Window? showOwner = inputBoxData.Owner ?? FromHandle(PI.GetActiveWindow());

        // Show input box window as a modal dialog and then dispose of it afterwards
        using var ib = new VisualInputBoxRtlAwareForm(inputBoxData);
        ib.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

        return ib.ShowDialog(showOwner) == DialogResult.OK
            ? ib.InputResponse
            : string.Empty;
    }

    internal string InputResponse => ktxtUserResponse.Text;

    private void UpdateText()
    {
        Text = _inputBoxData.Caption;
        kwlblPrompt.Text = _inputBoxData.Prompt;
        ktxtUserResponse.Text = _inputBoxData.DefaultResponse;
        ktxtUserResponse.UseSystemPasswordChar = _inputBoxData.UsePasswordOption ?? false;
    }

    private void UpdateCue()
    {
        ktxtUserResponse.CueHint.CueHintText = _inputBoxData.CueText;

        if (_inputBoxData.CueColor != null || _inputBoxData.CueColor != Color.Transparent || _inputBoxData.CueColor != GlobalStaticValues.EMPTY_COLOR)
        {
            ktxtUserResponse.CueHint.Color1 = _inputBoxData.CueColor ?? Color.Gray;
        }

        if (_inputBoxData.CueTypeface != null)
        {
            ktxtUserResponse.CueHint.Font = _inputBoxData.CueTypeface ?? KryptonManager.CurrentGlobalPalette.BaseFont;
        }
    }

    private void UpdateButtons()
    {
        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;
        kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;
    }

    /// <summary>
    /// Sizes the prompt to its wrapped text. The button strip is docked below the table, so its height
    /// is added after the prompt is measured.
    /// </summary>
    private void UpdatePromptLayout()
    {
        IWin32Window? owner = _inputBoxData.Owner;
        var maxClient = KryptonDialogLayout.GetMaximumClientSize(this, owner, true);
        if (ktxtUserResponse.MinimumSize.Width > maxClient.Width - 24)
        {
            ktxtUserResponse.MinimumSize = new Size(Math.Max(120, maxClient.Width - 24), ktxtUserResponse.MinimumSize.Height);
        }

        var buttonWidth = kbtnOk.GetPreferredSize(Size.Empty).Width
                          + kbtnCancel.GetPreferredSize(Size.Empty).Width
                          + 32;
        var minimum = Math.Max(ktxtUserResponse.Width, buttonWidth);
        KryptonDialogLayout.FitWrappedPrompt(this, kwlblPrompt, tableLayoutPanel2, owner, minimum);

        // The button strip is docked to the bottom and is not a child of the prompt table.
        var extra = kryptonPanel1.Height;
        if (extra > 0 && ClientSize.Height < kwlblPrompt.Bottom + ktxtUserResponse.Height + extra)
        {
            ClientSize = new Size(ClientSize.Width, ClientSize.Height + extra);
        }
    }

    private void ktxtUserResponse_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Enter:
                kbtnOk.PerformClick();
                break;
            case Keys.Escape:
                kbtnCancel.PerformClick();
                break;
        }
    }

    #endregion
}