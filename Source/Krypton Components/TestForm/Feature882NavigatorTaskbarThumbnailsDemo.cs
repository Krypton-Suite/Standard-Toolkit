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
    private KryptonNavigator? _secondaryNavigator;
    private KryptonNavigatorTaskbarThumbnails? _secondaryThumbnails;
    private KryptonCheckBox? _kchkIncludeHidden;
    private KryptonCheckBox? _kchkAppPreview;
    private KryptonCheckBox? _kchkHostShell;
    private KryptonCheckBox? _kchkCustomThumbnail;
    private KryptonCheckBox? _kchkSecondNavigator;
    private KryptonCheckBox? _kchkTabGroupThumbnails;
    private KryptonNumericUpDown? _knudMaxThumbnails;
    private KryptonComboBox? _kcmbCloseAction;
    private KryptonButton? _kbtnTheme;
    private KryptonNavigatorFormIntegrator? _formIntegrator;
    private bool _cancelNextClose;

    public Feature882NavigatorTaskbarThumbnailsDemo()
    {
        InitializeComponent();
    }

    private void Feature882NavigatorTaskbarThumbnailsDemo_Load(object? sender, EventArgs e)
    {
        taskbarThumbnails.Navigator = kryptonNavigator;
        taskbarThumbnails.Enabled = kchkEnabled.Checked;
        taskbarThumbnails.AllowCloseFromThumbnail = kchkAllowClose.Checked;
        taskbarThumbnails.ActiveTabUsesAppPreview = true;
        taskbarThumbnails.QueryThumbnail += OnQueryThumbnail;
        taskbarThumbnails.QueryOverlay += OnQueryOverlay;
        taskbarThumbnails.QueryProgress += OnQueryProgress;
        taskbarThumbnails.QueryThumbnailButtons += OnQueryThumbnailButtons;
        kryptonNavigator.CloseAction += OnNavigatorCloseAction;

        ExpandToolbar();
        UpdateInstructions();
        UpdateStatus();
    }

    private void ExpandToolbar()
    {
        _kchkIncludeHidden = new KryptonCheckBox
        {
            Values = { Text = "Include hidden pages" }
        };
        _kchkIncludeHidden.CheckedChanged += (_, _) =>
        {
            taskbarThumbnails.IncludeHiddenPages = _kchkIncludeHidden.Checked;
            UpdateStatus();
        };

        _kchkAppPreview = new KryptonCheckBox
        {
            Checked = true,
            Values = { Text = "Active tab uses app Peek" }
        };
        _kchkAppPreview.CheckedChanged += (_, _) =>
        {
            taskbarThumbnails.ActiveTabUsesAppPreview = _kchkAppPreview.Checked;
            if (_secondaryThumbnails != null)
            {
                _secondaryThumbnails.ActiveTabUsesAppPreview = _kchkAppPreview.Checked;
            }
        };

        _kchkHostShell = new KryptonCheckBox
        {
            Values = { Text = "Selected-page overlay/progress" }
        };
        _kchkHostShell.CheckedChanged += (_, _) =>
        {
            taskbarThumbnails.UseSelectedPageOverlay = _kchkHostShell.Checked;
            taskbarThumbnails.UseSelectedPageProgress = _kchkHostShell.Checked;
            taskbarThumbnails.UseSelectedPageThumbnailButtons = _kchkHostShell.Checked;
            taskbarThumbnails.RefreshThumbnails();
        };

        _kchkCustomThumbnail = new KryptonCheckBox
        {
            Values = { Text = "Custom QueryThumbnail" }
        };

        _kchkSecondNavigator = new KryptonCheckBox
        {
            Values = { Text = "Second navigator (merge)" }
        };
        _kchkSecondNavigator.CheckedChanged += (_, _) => ToggleSecondNavigator(_kchkSecondNavigator.Checked);

        _kchkTabGroupThumbnails = new KryptonCheckBox
        {
            Values = { Text = "Tab group composites (#4129)" }
        };
        _kchkTabGroupThumbnails.CheckedChanged += (_, _) => ToggleTabGroupThumbnails(_kchkTabGroupThumbnails.Checked);

        _knudMaxThumbnails = new KryptonNumericUpDown
        {
            Minimum = 0,
            Maximum = 50,
            Value = 0,
            Width = 60
        };
        _knudMaxThumbnails.ValueChanged += (_, _) =>
        {
            taskbarThumbnails.MaxThumbnails = (int)_knudMaxThumbnails.Value;
            UpdateStatus();
        };

        var maxLabel = new KryptonLabel { Values = { Text = "Max:" } };

        _kcmbCloseAction = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 180
        };
        _kcmbCloseAction.Items.AddRange(new object[]
        {
            "RemovePageAndDispose",
            "HidePage",
            "None (keep tab)",
            "Cancel via CloseAction"
        });
        _kcmbCloseAction.SelectedIndex = 0;
        _kcmbCloseAction.SelectedIndexChanged += (_, _) => ApplyCloseMode();

        _kbtnTheme = new KryptonButton
        {
            Values = { Text = "Swap theme" }
        };
        _kbtnTheme.Click += (_, _) =>
        {
            var manager = new KryptonManager();
            manager.GlobalPaletteMode =
                KryptonManager.CurrentGlobalPaletteMode == PaletteMode.Microsoft365Blue
                    ? PaletteMode.SparkleOrange
                    : PaletteMode.Microsoft365Blue;
        };

        flowToolbar.Controls.Add(_kchkIncludeHidden);
        flowToolbar.Controls.Add(_kchkAppPreview);
        flowToolbar.Controls.Add(_kchkHostShell);
        flowToolbar.Controls.Add(_kchkCustomThumbnail);
        flowToolbar.Controls.Add(_kchkSecondNavigator);
        flowToolbar.Controls.Add(_kchkTabGroupThumbnails);
        flowToolbar.Controls.Add(maxLabel);
        flowToolbar.Controls.Add(_knudMaxThumbnails);
        flowToolbar.Controls.Add(_kcmbCloseAction);
        flowToolbar.Controls.Add(_kbtnTheme);
    }

    private void UpdateInstructions()
    {
        kwlblInstructions.Text =
            "Issue #882 / #4129 — KryptonNavigatorTaskbarThumbnails (Krypton.Navigator.Utilities).\r\n" +
            "Win11 checklist: hover taskbar for per-page thumbnails; click to select; Peek active tab; " +
            "enable Tab group composites for Explorer-like Group | … entries ahead of grouped pages; " +
            "thumbnail X respects close mode; enable Second navigator to merge two navigators; " +
            "wizard page starts with AllowTaskbarThumbnail cleared.";
    }

    private void ToggleTabGroupThumbnails(bool enabled)
    {
        if (enabled)
        {
            if (_formIntegrator == null)
            {
                _formIntegrator = new KryptonNavigatorFormIntegrator(components)
                {
                    Form = this,
                    Navigator = kryptonNavigator,
                    Mode = NavigatorFormIntegrationMode.ClientChrome,
                    Enabled = false,
                    AllowTabGroups = true
                };

                NavigatorTabGroup work = _formIntegrator.CreateGroup("Work", Color.DodgerBlue);
                if (kryptonNavigator.Pages.Count >= 2)
                {
                    _formIntegrator.AssignPageToGroup(kryptonNavigator.Pages[0], work.Id);
                    _formIntegrator.AssignPageToGroup(kryptonNavigator.Pages[1], work.Id);
                }
            }

            taskbarThumbnails.FormIntegrator = _formIntegrator;
            taskbarThumbnails.ShowTabGroupThumbnails = true;
        }
        else
        {
            taskbarThumbnails.ShowTabGroupThumbnails = false;
            taskbarThumbnails.FormIntegrator = null;
        }

        taskbarThumbnails.RefreshThumbnails();
        UpdateStatus();
    }

    private void ToggleSecondNavigator(bool enabled)
    {
        if (enabled)
        {
            if (_secondaryNavigator == null)
            {
                _secondaryNavigator = new KryptonNavigator
                {
                    Dock = DockStyle.Bottom,
                    Height = 140
                };
                _secondaryNavigator.Pages.Add(CreateDocumentPage("Secondary A", Color.Honeydew,
                    "Secondary navigator page A — should merge into the same taskbar flyout."));
                _secondaryNavigator.Pages.Add(CreateDocumentPage("Secondary B", Color.Lavender,
                    "Secondary navigator page B."));
                kryptonPanelMain.Controls.Add(_secondaryNavigator);
                _secondaryNavigator.BringToFront();

                _secondaryThumbnails = new KryptonNavigatorTaskbarThumbnails(components)
                {
                    Navigator = _secondaryNavigator,
                    Enabled = kchkEnabled.Checked,
                    AllowCloseFromThumbnail = kchkAllowClose.Checked,
                    ActiveTabUsesAppPreview = _kchkAppPreview?.Checked ?? true
                };
            }

            _secondaryNavigator.Visible = true;
            if (_secondaryThumbnails != null)
            {
                _secondaryThumbnails.Enabled = kchkEnabled.Checked;
            }
        }
        else if (_secondaryNavigator != null)
        {
            _secondaryNavigator.Visible = false;
            if (_secondaryThumbnails != null)
            {
                _secondaryThumbnails.Enabled = false;
            }
        }

        UpdateStatus();
    }

    private void ApplyCloseMode()
    {
        _cancelNextClose = false;
        switch (_kcmbCloseAction?.SelectedIndex ?? 0)
        {
            case 1:
                kryptonNavigator.Button.CloseButtonAction = CloseButtonAction.HidePage;
                break;
            case 2:
                kryptonNavigator.Button.CloseButtonAction = CloseButtonAction.None;
                break;
            case 3:
                kryptonNavigator.Button.CloseButtonAction = CloseButtonAction.RemovePageAndDispose;
                _cancelNextClose = true;
                break;
            default:
                kryptonNavigator.Button.CloseButtonAction = CloseButtonAction.RemovePageAndDispose;
                break;
        }
    }

    private void OnNavigatorCloseAction(object? sender, CloseActionEventArgs e)
    {
        if (_cancelNextClose)
        {
            e.Action = CloseButtonAction.None;
            _cancelNextClose = false;
            UpdateStatus();
        }
    }

    private void OnQueryThumbnail(object? sender, QueryTaskbarThumbnailEventArgs e)
    {
        if (_kchkCustomThumbnail == null || !_kchkCustomThumbnail.Checked)
        {
            return;
        }

        var bmp = new Bitmap(e.Size.Width, e.Size.Height);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.DarkSlateBlue);
            TextRenderer.DrawText(g, "Custom\r\n" + e.Page.Text, SystemFonts.DefaultFont,
                new Rectangle(Point.Empty, e.Size), Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        e.Thumbnail = bmp;
    }

    private void OnQueryOverlay(object? sender, QueryTaskbarOverlayEventArgs e)
    {
        e.Icon = SystemIcons.Information;
        e.Description = e.Page.Text;
    }

    private void OnQueryProgress(object? sender, QueryTaskbarProgressEventArgs e)
    {
        e.State = TaskbarProgressState.Normal;
        e.Completed = (ulong)(kryptonNavigator.SelectedIndex + 1);
        e.Total = (ulong)Math.Max(1, kryptonNavigator.Pages.Count);
    }

    private void OnQueryThumbnailButtons(object? sender, QueryTaskbarThumbnailButtonsEventArgs e)
    {
        e.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = 1,
            Tooltip = "Selected: " + e.Page.Text,
            Flags = TaskbarThumbnailButtonFlags.Enabled
        });
    }

    private void kchkEnabled_CheckedChanged(object? sender, EventArgs e)
    {
        taskbarThumbnails.Enabled = kchkEnabled.Checked;
        if (_secondaryThumbnails != null)
        {
            _secondaryThumbnails.Enabled = kchkEnabled.Checked && (_kchkSecondNavigator?.Checked ?? false);
        }

        UpdateStatus();
    }

    private void kchkAllowClose_CheckedChanged(object? sender, EventArgs e)
    {
        taskbarThumbnails.AllowCloseFromThumbnail = kchkAllowClose.Checked;
        if (_secondaryThumbnails != null)
        {
            _secondaryThumbnails.AllowCloseFromThumbnail = kchkAllowClose.Checked;
        }
    }

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
            if (page.AreFlagsSet(KryptonPageFlags.AllowTaskbarThumbnail) &&
                (taskbarThumbnails.IncludeHiddenPages || page.LastVisibleSet))
            {
                included++;
            }
        }

        if (_secondaryNavigator != null && _secondaryNavigator.Visible)
        {
            foreach (KryptonPage page in _secondaryNavigator.Pages)
            {
                if (page.AreFlagsSet(KryptonPageFlags.AllowTaskbarThumbnail) && page.LastVisibleSet)
                {
                    included++;
                }
            }
        }

        bool wizardIncluded = pageWizardStep != null &&
                              pageWizardStep.AreFlagsSet(KryptonPageFlags.AllowTaskbarThumbnail);

        klblStatus.Text =
            $"TaskbarThumbnails: {(kchkEnabled.Checked ? "ON" : "OFF")} | " +
            $"Eligible: {included} | Max: {taskbarThumbnails.MaxThumbnails} | " +
            $"TabGroups: {taskbarThumbnails.ShowTabGroupThumbnails} | " +
            $"Selected: {kryptonNavigator.SelectedPage?.Text ?? "(none)"} | " +
            $"Wizard flag: {(wizardIncluded ? "set" : "cleared")} | " +
            $"Merge: {(_kchkSecondNavigator?.Checked == true ? "2 navs" : "1 nav")}";
    }
}
