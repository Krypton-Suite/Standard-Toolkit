#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Manual validation for Issue #4061: ribbon caption form-icon visibility must update when the palette (RibbonShape) changes.
/// </summary>
public class Bug4061RibbonCaptionIconThemeDemo : KryptonForm
{
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonThemeComboBox _themeCombo;
    private readonly KryptonLabel _statusLabel;

    public Bug4061RibbonCaptionIconThemeDemo()
    {
        Text = "Bug 4061 — Ribbon caption icon on theme change";
        Width = 900;
        Height = 420;
        StartPosition = FormStartPosition.CenterScreen;
        ShowIcon = true;
        Icon = SystemIcons.Application;

        _ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            QATLocation = QATLocation.Above
        };
        _ribbon.RibbonFileAppButton.AppButtonVisible = true;

        var homeTab = new KryptonRibbonTab { Text = "Home" };
        var homeGroup = new KryptonRibbonGroup { TextLine1 = "Clipboard" };
        homeTab.Groups.Add(homeGroup);
        _ribbon.RibbonTabs.Add(homeTab);

        Controls.Add(_ribbon);

        var root = new KryptonPanel { Dock = DockStyle.Fill };
        Controls.Add(root);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(12)
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.Controls.Add(layout);

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            Height = 88,
            TextAlign = ContentAlignment.TopLeft,
            Text =
                "Issue #4061: After a theme change, the form caption icon must update from RibbonShape without resizing the form.\r\n" +
                "Office 2007 + visible File app button: icon should hide (app button replaces it).\r\n" +
                "Office 2010 / Microsoft 365 / Visual Studio / macOS / OS X Aqua: icon should show when integrated.\r\n" +
                "Also confirm QAT Above hides under macOS shapes after the theme switch."
        };
        layout.Controls.Add(instructions, 0, 0);

        var themeRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            WrapContents = false
        };
        themeRow.Controls.Add(new KryptonLabel { Values = { Text = "Theme:" }, Padding = new Padding(0, 6, 8, 0) });
        _themeCombo = new KryptonThemeComboBox { Width = 300 };
        themeRow.Controls.Add(_themeCombo);
        layout.Controls.Add(themeRow, 0, 1);

        _statusLabel = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Values = { Text = "Status" }
        };
        layout.Controls.Add(_statusLabel, 0, 2);

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        FormClosed += (_, _) => KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        Shown += (_, _) => RefreshStatus();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e) => RefreshStatus();

    private void RefreshStatus()
    {
        // AllowIconDisplay is set by ViewDrawRibbonCaptionArea during PerformFormChromeCheck.
        _statusLabel.Values.Text =
            $"RibbonShape={_ribbon.StateCommon.RibbonGeneral.GetRibbonShape()}; AllowIconDisplay={AllowIconDisplay}; " +
            $"AppButtonVisible={_ribbon.RibbonFileAppButton.AppButtonVisible}; QATLocation={_ribbon.QATLocation}";
    }
}