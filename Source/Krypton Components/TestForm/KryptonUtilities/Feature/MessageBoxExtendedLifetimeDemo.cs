#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Threading.Tasks;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Issue #4188: fade in/out, caption timeout, and auto-close on <see cref="KryptonMessageBoxExtended"/>.
/// </summary>
public partial class MessageBoxExtendedLifetimeDemo : KryptonForm
{
    private const string DoNotShowAgainKey = @"lifetime-demo-do-not-show-again";
    public MessageBoxExtendedLifetimeDemo()
    {
        InitializeComponent();
        PopulateCombos();
        LoadSampleContent();
    }

    private void PopulateCombos()
    {
        kcmbButtons.Items.AddRange(new object[]
        {
            ExtendedMessageBoxButtons.OK,
            ExtendedMessageBoxButtons.OKCancel,
            ExtendedMessageBoxButtons.YesNo,
            ExtendedMessageBoxButtons.YesNoCancel
        });
        kcmbButtons.SelectedItem = ExtendedMessageBoxButtons.YesNoCancel;

        kcmbFadeSpeed.Items.AddRange(Enum.GetNames(typeof(FadeSpeedChoice)));
        kcmbFadeSpeed.SelectedItem = nameof(FadeSpeedChoice.Normal);

        kcmbAutoClose.Items.AddRange(new object[] { @"Default", @"Yes", @"No" });
        kcmbAutoClose.SelectedItem = @"Default";

        kcmbTimeOutAction.Items.AddRange(Enum.GetNames(typeof(ExtendedMessageBoxTimeoutAction)));
        kcmbTimeOutAction.SelectedItem = nameof(ExtendedMessageBoxTimeoutAction.Close);

        kcmbCountdownButton.Items.AddRange(Enum.GetNames(typeof(ExtendedKryptonMessageBoxCountdownButton)));
        kcmbCountdownButton.SelectedItem = nameof(ExtendedKryptonMessageBoxCountdownButton.None);

        kcmbTimeOutResult.Items.AddRange(new object[]
        {
            DialogResult.None,
            DialogResult.OK,
            DialogResult.Cancel,
            DialogResult.Yes,
            DialogResult.No
        });
        kcmbTimeOutResult.SelectedItem = DialogResult.OK;
    }

    private void LoadSampleContent()
    {
        ktxtCaption.Text = @"Extended message box";
        ktxtMessage.Text =
            "Issue #4188: fade in/out, caption countdown, optional countdown on a button, auto-close, and optional 'Do not show again'.\r\n\r\n" +
            "Use the check boxes and combos, or try a preset. The last DialogResult is shown below. Tick 'Do not show again' then check the box on the dialog to suppress later shows (Reset clears it).";
    }

    private static T ParseEnum<T>(object? value, T fallback) where T : struct =>
        Enum.TryParse(value?.ToString(), out T parsed) ? parsed : fallback;

    private bool? ReadAutoClose()
    {
        string selected = kcmbAutoClose.SelectedItem?.ToString() ?? @"Default";
        return selected switch
        {
            @"Yes" => true,
            @"No" => false,
            _ => null
        };
    }

    private KryptonMessageBoxExtendedData CreateDataFromUi() =>
        new KryptonMessageBoxExtendedData
        {
            Owner = this,
            Caption = ktxtCaption.Text,
            MessageText = ktxtMessage.Text,
            Buttons = ParseEnum(kcmbButtons.SelectedItem, ExtendedMessageBoxButtons.YesNoCancel),
            Icon = ExtendedKryptonMessageBoxIcon.Information,
            Options = kchkRtl.Checked ? MessageBoxOptions.RtlReading : 0,
            UseFade = kchkUseFade.Checked,
            FadeSpeed = ParseEnum(kcmbFadeSpeed.SelectedItem, FadeSpeedChoice.Normal),
            UseTimeOut = kchkUseTimeOut.Checked,
            TimeOut = (int)knudTimeOut.Value,
            TimeOutInterval = 1000,
            AutoClose = ReadAutoClose(),
            TimeOutResult = ParseEnum(kcmbTimeOutResult.SelectedItem, DialogResult.OK),
            TimeOutAction = ParseEnum(kcmbTimeOutAction.SelectedItem, ExtendedMessageBoxTimeoutAction.Close),
            CountdownButton = ParseEnum(kcmbCountdownButton.SelectedItem, ExtendedKryptonMessageBoxCountdownButton.None),
            CountdownButtonSeconds = (int)knudTimeOut.Value,
            ShowDoNotShowAgainOption = kchkDoNotShowAgain.Checked,
            DoNotShowAgainKey = kchkDoNotShowAgain.Checked ? DoNotShowAgainKey : null
        };

    private void ShowData(KryptonMessageBoxExtendedData data)
    {
        DialogResult result = KryptonMessageBoxExtended.Show(data, out bool doNotShowAgain);
        klblResult.Text = $@"Last result: {result}; Do not show again: {doNotShowAgain}";
    }

    private void kbtnShow_Click(object? sender, EventArgs e) => ShowData(CreateDataFromUi());

    private void kbtnFadeOnly_Click(object? sender, EventArgs e)
    {
        kchkUseFade.Checked = true;
        kchkUseTimeOut.Checked = false;
        kcmbAutoClose.SelectedItem = @"No";
        kcmbCountdownButton.SelectedItem = nameof(ExtendedKryptonMessageBoxCountdownButton.None);
        ShowData(CreateDataFromUi());
    }

    private void ResetCountdownButtonCombo() =>
        kcmbCountdownButton.SelectedItem = nameof(ExtendedKryptonMessageBoxCountdownButton.None);

    private void kbtnTimeoutNoClose_Click(object? sender, EventArgs e)
    {
        kchkUseFade.Checked = false;
        kchkUseTimeOut.Checked = true;
        kcmbAutoClose.SelectedItem = @"No";
        ResetCountdownButtonCombo();
        knudTimeOut.Value = 8;
        ShowData(CreateDataFromUi());
    }

    private void kbtnAutoCloseOk_Click(object? sender, EventArgs e)
    {
        kchkUseFade.Checked = false;
        kchkUseTimeOut.Checked = true;
        kcmbAutoClose.SelectedItem = @"Default";
        kcmbTimeOutAction.SelectedItem = nameof(ExtendedMessageBoxTimeoutAction.Close);
        kcmbTimeOutResult.SelectedItem = DialogResult.OK;
        ResetCountdownButtonCombo();
        knudTimeOut.Value = 5;
        ShowData(CreateDataFromUi());
    }

    private void kbtnAutoCloseButton2_Click(object? sender, EventArgs e)
    {
        kchkUseFade.Checked = false;
        kchkUseTimeOut.Checked = true;
        kcmbButtons.SelectedItem = ExtendedMessageBoxButtons.YesNoCancel;
        kcmbAutoClose.SelectedItem = @"Yes";
        kcmbTimeOutAction.SelectedItem = nameof(ExtendedMessageBoxTimeoutAction.ButtonTwo);
        ResetCountdownButtonCombo();
        knudTimeOut.Value = 5;
        ShowData(CreateDataFromUi());
    }

    private void kbtnCountdownOnButton_Click(object? sender, EventArgs e)
    {
        kchkUseFade.Checked = false;
        kchkUseTimeOut.Checked = false;
        kcmbAutoClose.SelectedItem = @"No";
        kcmbButtons.SelectedItem = ExtendedMessageBoxButtons.OKCancel;
        kcmbCountdownButton.SelectedItem = nameof(ExtendedKryptonMessageBoxCountdownButton.Button1);
        knudTimeOut.Value = 8;
        KryptonMessageBoxExtendedData data = CreateDataFromUi();
        data.CountdownButtonDialogResult = DialogResult.OK;
        ShowData(data);
    }

    private void kbtnFadeAndTimeout_Click(object? sender, EventArgs e)
    {
        kchkUseFade.Checked = true;
        kcmbFadeSpeed.SelectedItem = nameof(FadeSpeedChoice.Fast);
        kchkUseTimeOut.Checked = true;
        kcmbAutoClose.SelectedItem = @"Default";
        kcmbTimeOutResult.SelectedItem = DialogResult.OK;
        ResetCountdownButtonCombo();
        knudTimeOut.Value = 6;
        ShowData(CreateDataFromUi());
    }

    private void kbtnRtlTimeout_Click(object? sender, EventArgs e)
    {
        // Options.RtlReading uses VisualRTLMessageBoxExtendedForm (not the data-struct LTR form).
        // Positional `false` is displayHelpButton; countdownButton picks the timeout-capable overload.
        DialogResult result = KryptonMessageBoxExtended.Show(
            this,
            @"RTL extended message box with caption timeout and auto-close.",
            @"RTL timeout",
            ExtendedMessageBoxButtons.OKCancel,
            ExtendedKryptonMessageBoxIcon.Information,
            KryptonMessageBoxDefaultButton.Button1,
            MessageBoxOptions.RtlReading,
            false,
            countdownButton: ExtendedKryptonMessageBoxCountdownButton.None,
            useTimeOut: true,
            timeOut: 6,
            timeOutInterval: 1000,
            timerResult: DialogResult.OK);

        klblResult.Text = $@"Last result: {result}";
    }

    private void kbtnShowOverload_Click(object? sender, EventArgs e)
    {
        // ContentAlignment after showCtrlCopy selects the timeout-capable Show overload.
        DialogResult result = KryptonMessageBoxExtended.Show(
            @"This uses the existing Show() timeout parameters (useTimeOut / timeOut / timerResult). Fade is off on this path.",
            @"Show() timeout overload",
            ExtendedMessageBoxButtons.OKCancel,
            ExtendedKryptonMessageBoxIcon.Information,
            null,
            ContentAlignment.MiddleLeft,
            HorizontalAlignment.Left,
            true,
            5,
            1000,
            DialogResult.OK);

        klblResult.Text = $@"Last result: {result}";
    }

    private async void kbtnShowAsync_Click(object? sender, EventArgs e)
    {
        kchkUseFade.Checked = true;
        kchkUseTimeOut.Checked = true;
        kcmbAutoClose.SelectedItem = @"Default";
        ResetCountdownButtonCombo();
        knudTimeOut.Value = 5;

        DialogResult result = await KryptonMessageBoxExtended.ShowAsync(CreateDataFromUi()).ConfigureAwait(true);
        klblResult.Text = $@"Last result: {result}; Do not show again set: {KryptonMessageBoxExtended.IsDoNotShowAgainSet(DoNotShowAgainKey)}";
    }

    private void kbtnResetDoNotShowAgain_Click(object? sender, EventArgs e)
    {
        KryptonMessageBoxExtended.ResetDoNotShowAgain(DoNotShowAgainKey);
        klblResult.Text = @"Last result: Do not show again reset for this demo key.";
    }
}
