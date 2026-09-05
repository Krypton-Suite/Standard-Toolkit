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
/// Manual validation for Issues #3859 and #4061: ribbon caption chrome must refresh when the palette changes.
/// </summary>
public class Bug4061RibbonCaptionIconThemeDemo : KryptonForm
{
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonThemeComboBox _themeCombo;
    private readonly KryptonLabel _statusLabel;

    public Bug4061RibbonCaptionIconThemeDemo()
    {
        Text = "Ribbon caption on theme change (#3859 / #4061)";
        Width = 980;
        Height = 460;
        StartPosition = FormStartPosition.CenterScreen;
        ShowIcon = true;
        Icon = SystemIcons.Application;

        _ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            QATLocation = QATLocation.Above
        };
        _ribbon.RibbonFileAppButton.AppButtonVisible = true;
        _ribbon.InsertStandardQATItems();

        var homeTab = new KryptonRibbonTab { Text = "Home" };
        var homeGroup = new KryptonRibbonGroup { TextLine1 = "Clipboard" };
        homeTab.Groups.Add(homeGroup);
        _ribbon.RibbonTabs.Add(homeTab);

        // Context title in the caption so theme swaps are visible without resizing (#3859).
        var reviewContext = new KryptonRibbonContext
        {
            ContextName = "Review",
            ContextTitle = "Review",
            ContextColor = Color.Orange
        };
        _ribbon.RibbonContexts.Add(reviewContext);

        var reviewTab = new KryptonRibbonTab
        {
            Text = "Comments",
            ContextName = "Review"
        };
        var reviewGroup = new KryptonRibbonGroup { TextLine1 = "Markup" };
        reviewTab.Groups.Add(reviewGroup);
        _ribbon.RibbonTabs.Add(reviewTab);
        _ribbon.SelectedContext = "Review";

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
            Height = 128,
            TextAlign = ContentAlignment.TopLeft,
            Text =
                "Issues #3859 / #4061: After a theme change, caption chrome must update without resizing the form.\r\n" +
                "Caption colours, the Review context title, QAT Above, and the File orb vs File tab should follow the new theme immediately.\r\n" +
                "Office 2007 + visible File app button: form icon should hide (app button replaces it).\r\n" +
                "Office 2010 / Microsoft 365 / Visual Studio / macOS / OS X Aqua: icon should show when integrated.\r\n" +
                "QAT Above should hide under macOS shapes after the theme switch."
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