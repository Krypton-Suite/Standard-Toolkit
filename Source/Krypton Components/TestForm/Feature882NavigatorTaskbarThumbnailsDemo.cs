#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Navigator.Utilities;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Demo for Issue #882: KryptonNavigator individual Windows taskbar thumbnail views via KryptonNavigatorTaskbarThumbnails.
/// Hover the taskbar button to see one thumbnail per document page; click a thumbnail to select that page.
/// </summary>
public partial class Feature882NavigatorTaskbarThumbnailsDemo : KryptonForm
{
    private int _documentCounter = 3;

    public Feature882NavigatorTaskbarThumbnailsDemo()
    {
        InitializeComponent();
    }

    private void Feature882NavigatorTaskbarThumbnailsDemo_Load(object? sender, EventArgs e)
    {
        taskbarThumbnails.Navigator = kryptonNavigator;
        taskbarThumbnails.Enabled = kchkEnabled.Checked;
        taskbarThumbnails.AllowCloseFromThumbnail = kchkAllowClose.Checked;
        UpdateStatus();
    }

    private void kchkEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        taskbarThumbnails.Enabled = kchkEnabled.Checked;
        UpdateStatus();
    }

    private void kchkAllowClose_CheckedChanged(object? sender, EventArgs e) =>
        taskbarThumbnails.AllowCloseFromThumbnail = kchkAllowClose.Checked;

    private void kbtnAddPage_Click(object? sender, EventArgs e)
    {
        _documentCounter++;
        var page = CreateDocumentPage($"Document {_documentCounter}",
            Color.FromArgb(255, 200 + (_documentCounter * 20) % 55, 220, 255 - (_documentCounter * 15) % 80),
            $"This is document {_documentCounter}.\r\n\r\nHover the application taskbar button to see a separate thumbnail for this page.");
        kryptonNavigator.Pages.Add(page);
        kryptonNavigator.SelectedPage = page;
        UpdateStatus();
    }

    private void kbtnToggleWizardExclude_Click(object? sender, EventArgs e)
    {
        if (pageWizardStep == null)
        {
            return;
        }

        if (pageWizardStep.AreFlagsSet(KryptonPageFlags.AllowTaskbarThumbnail))
        {
            pageWizardStep.ClearFlags(KryptonPageFlags.AllowTaskbarThumbnail);
        }
        else
        {
            pageWizardStep.SetFlags(KryptonPageFlags.AllowTaskbarThumbnail);
        }

        UpdateStatus();
    }

    private void kryptonNavigator_SelectedPageChanged(object? sender, EventArgs e) => UpdateStatus();

    private static KryptonPage CreateDocumentPage(string title, Color backColor, string body)
    {
        var page = new KryptonPage
        {
            Text = title,
            TextTitle = title,
            UniqueName = Guid.NewGuid().ToString()
        };

        var label = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.TitlePanel
        };
        label.Values.Text = body;
        label.StateCommon.ShortText.TextH = PaletteRelativeAlign.Near;
        label.StateCommon.ShortText.TextV = PaletteRelativeAlign.Near;

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill
        };
        panel.StateCommon.Color1 = backColor;
        panel.Controls.Add(label);
        page.Controls.Add(panel);
        return page;
    }

    private void UpdateStatus()
    {
        int included = 0;
        foreach (KryptonPage page in kryptonNavigator.Pages)
        {
            if (page.AreFlagsSet(KryptonPageFlags.AllowTaskbarThumbnail) && page.LastVisibleSet)
            {
                included++;
            }
        }

        bool wizardIncluded = pageWizardStep != null &&
                              pageWizardStep.AreFlagsSet(KryptonPageFlags.AllowTaskbarThumbnail);

        klblStatus.Text =
            $"TaskbarThumbnails component: {(kchkEnabled.Checked ? "ON" : "OFF")} | " +
            $"Eligible pages: {included} | " +
            $"Selected: {kryptonNavigator.SelectedPage?.Text ?? "(none)"} | " +
            $"Wizard step flag AllowTaskbarThumbnail: {(wizardIncluded ? "set" : "cleared")}";
    }
}
