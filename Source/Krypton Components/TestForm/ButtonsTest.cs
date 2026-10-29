#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ButtonsTest : KryptonForm
{
    public ButtonsTest()
    {
        InitializeComponent();
        kcbColorScheme.SelectedItem = "OfficeThemes";
        kcbSortMode.Enabled = false;

        // #1673 sample: progress bar in the shared UAC drop-down context menu
        kryptonContextMenuItems1.Items.Insert(0, new KryptonContextMenuProgressBar
        {
            MinimumWidth = 200,
            Value = 42
        });
    }

    private void kcbtnDropDown_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonButton3.Values.DropDownArrowColor = e.Color;

        kryptonButton4.Values.DropDownArrowColor = e.Color;

        kryptonButton7.Values.DropDownArrowColor = e.Color;

        kryptonButton8.Values.DropDownArrowColor = e.Color;
    }

    private void kbtnButtonStyles_Click(object sender, EventArgs e)
    {
        new ButtonStyleExamples().Show();
    }

    private void kryptonCommand1_Execute(object sender, EventArgs e)
    {
        var typeName = sender.GetType().FullName;
        var parameter = e is KryptonCommandExecuteEventArgs executeArgs
            ? executeArgs.Parameter?.ToString() ?? "<no param>"
            : KryptonCommandContext.GetCommandParameter(sender)?.ToString() ?? "<no param>";

        KryptonMessageBox.Show($"Command executed by:\nType: {typeName}\nParam: {parameter}", "Context Item Called");
    }

    private void kcbSortMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var text = kcbSortMode.SelectedItem?.ToString();
        if (!Enum.TryParse<Krypton.Toolkit.ThemeColorSortMode>(text, true, out var mode))
        {
            mode = Krypton.Toolkit.ThemeColorSortMode.OKLCH;
        }
        kcbtnDropDown.ThemeColorSortMode = mode;
    }

    private void kcbColorScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        var text = kcbColorScheme.SelectedItem?.ToString();
        if (!Enum.TryParse<Krypton.Toolkit.ColorScheme>(text, true, out var scheme))
        {
            scheme = Krypton.Toolkit.ColorScheme.OfficeThemes;
        }

        kcbtnDropDown.SchemeThemes = scheme;
        kcbSortMode.Enabled = scheme == Krypton.Toolkit.ColorScheme.PaletteColors;
    }

    private void kbtnElevatedClick_ElevatedClick(object sender, EventArgs e)
    {
        using (System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent())
        {
            bool alreadyAdmin = new System.Security.Principal.WindowsPrincipal(identity)
                .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            AppendElevatedLog(alreadyAdmin
                ? "ElevatedClick (already Administrator)"
                : "ElevatedClick (RunAs launch requested)");
        }
    }

    private void kbtnElevatedClick_Click(object sender, EventArgs e) =>
        AppendElevatedLog("Click");

    private void kbtnTryHideShield_Click(object sender, EventArgs e)
    {
        kbtnElevatedClick.Values.UseAsUACElevationButton = false;
        kbtnElevatedClick.Values.Image = null;
        kbtnElevatedClick.Values.ResetImage();

        bool shieldStillShown = kbtnElevatedClick.Values.UseAsUACElevationButton
                                && kbtnElevatedClick.Values.Image != null;
        AppendElevatedLog(shieldStillShown
            ? "Hide ignored: shield still shown."
            : "Hide changed the shield.");
    }

    private void AppendElevatedLog(string line) =>
        ktxtElevatedLog.AppendText(line + Environment.NewLine);

    private void KryptonCalcInput1_ButtonSpecClicked(object sender, ButtonSpecEventArgs e)
    {
        if (e.ButtonSpec is ButtonSpecAny { Type: PaletteButtonSpecStyle.Close })
        {
            KryptonCalcInput1.Value = 0m;
        }
    }
}