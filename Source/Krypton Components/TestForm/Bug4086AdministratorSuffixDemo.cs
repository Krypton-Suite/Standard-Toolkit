#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for issue #4086: empty administrator suffix must not leave bare "()" in the title bar.
/// </summary>
public sealed class Bug4086AdministratorSuffixDemo : KryptonForm
{
    private const string DemoTitle = @"Bug #4086 - Administrator Suffix";

    private readonly bool _savedShowAdministratorSuffix;
    private readonly string _savedAdministrator;
    private readonly KryptonCheckBox _chkShowSuffix;
    private readonly KryptonTextBox _txtAdministrator;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonButton _btnApplyEmpty;
    private readonly KryptonButton _btnApplyDefault;
    private readonly KryptonButton _btnRefresh;

    public Bug4086AdministratorSuffixDemo()
    {
        _savedShowAdministratorSuffix = KryptonManager.UseAdministratorSuffix;
        _savedAdministrator = KryptonManager.Strings.GeneralStrings.Administrator;

        Text = DemoTitle;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(720, 420);
        MinimumSize = new Size(640, 360);

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 130,
            Text =
                @"How to test issue #4086 (TestForm runs elevated via requireAdministrator):" + Environment.NewLine +
                @"1) With ShowAdministratorSuffix checked and Administrator = ""Administrator"", the caption should show "" (Administrator)""." + Environment.NewLine +
                @"2) Clear Administrator (or click Apply empty) — the caption must NOT show bare ""()""." + Environment.NewLine +
                @"3) Uncheck ShowAdministratorSuffix — the caption must show no suffix at all." + Environment.NewLine +
                @"4) Localise Administrator (e.g. Administrador) — the caption should use that text inside the parentheses."
        };

        _chkShowSuffix = new KryptonCheckBox
        {
            Text = @"ShowAdministratorSuffix",
            Checked = KryptonManager.UseAdministratorSuffix,
            AutoSize = true
        };

        var lblAdministrator = new KryptonLabel
        {
            Text = @"Administrator string:",
            AutoSize = true
        };

        _txtAdministrator = new KryptonTextBox
        {
            Text = KryptonManager.Strings.GeneralStrings.Administrator,
            Width = 220
        };

        _btnApplyEmpty = new KryptonButton
        {
            Text = @"Apply empty",
            AutoSize = true
        };

        _btnApplyDefault = new KryptonButton
        {
            Text = @"Apply ""Administrator""",
            AutoSize = true
        };

        _btnRefresh = new KryptonButton
        {
            Text = @"Refresh caption",
            AutoSize = true
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 56,
            Text = string.Empty
        };

        var optionsPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 88,
            Padding = new Padding(12, 8, 12, 8)
        };

        var optionsFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            AutoSize = true
        };
        optionsFlow.Controls.Add(_chkShowSuffix);
        optionsFlow.Controls.Add(lblAdministrator);
        optionsFlow.Controls.Add(_txtAdministrator);
        optionsFlow.Controls.Add(_btnApplyEmpty);
        optionsFlow.Controls.Add(_btnApplyDefault);
        optionsFlow.Controls.Add(_btnRefresh);
        optionsPanel.Controls.Add(optionsFlow);

        var contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };
        contentPanel.Controls.Add(_lblStatus);
        contentPanel.Controls.Add(optionsPanel);
        contentPanel.Controls.Add(lblInfo);
        Controls.Add(contentPanel);

        _chkShowSuffix.CheckedChanged += (_, _) =>
        {
            KryptonManager.UseAdministratorSuffix = _chkShowSuffix.Checked;
            RefreshCaption();
        };

        _txtAdministrator.TextChanged += (_, _) =>
        {
            KryptonManager.Strings.GeneralStrings.Administrator = _txtAdministrator.Text ?? string.Empty;
            RefreshCaption();
        };

        _btnApplyEmpty.Click += (_, _) =>
        {
            _txtAdministrator.Text = string.Empty;
        };

        _btnApplyDefault.Click += (_, _) =>
        {
            _txtAdministrator.Text = @"Administrator";
        };

        _btnRefresh.Click += (_, _) => RefreshCaption();

        FormClosed += (_, _) =>
        {
            KryptonManager.UseAdministratorSuffix = _savedShowAdministratorSuffix;
            KryptonManager.Strings.GeneralStrings.Administrator = _savedAdministrator;
        };

        RefreshCaption();
    }

    private void RefreshCaption()
    {
        // Keep the base Text stable; GetShortText appends the suffix at paint time.
        Text = DemoTitle;
        PerformNeedPaint(true);
        InvalidateNonClient();

        _lblStatus.Text =
            $@"IsInAdministratorMode={IsInAdministratorMode}; UseAdministratorSuffix={KryptonManager.UseAdministratorSuffix}; " +
            $@"Administrator=""{KryptonManager.Strings.GeneralStrings.Administrator}""; GetShortText()=""{GetShortText()}""";
    }
}
