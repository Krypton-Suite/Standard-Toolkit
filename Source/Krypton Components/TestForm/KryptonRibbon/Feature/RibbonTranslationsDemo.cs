#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;

using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Demo for Issue #4369: save/load KryptonRibbon captions via RibbonTranslations.xml, including Auto Discovery.
/// </summary>
public partial class RibbonTranslationsDemo : KryptonForm
{
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonTab _tabHome;
    private readonly KryptonRibbonGroup _groupClipboard;
    private readonly KryptonRibbonGroupButton _buttonPaste;
    private readonly KryptonRibbonQATButton _qatUndo;
    private readonly KryptonWrapLabel _lblStatus;
    private string? _lastFilePath;

    public RibbonTranslationsDemo()
    {
        InitializeComponent();

        _ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            TranslationId = @"demoRibbon",
            EnableAutoDiscoverTranslations = false,
            Name = @"kryptonRibbonDemo"
        };

        _tabHome = new KryptonRibbonTab
        {
            Text = @"Home",
            KeyTip = @"H",
            TranslationId = @"home"
        };

        _groupClipboard = new KryptonRibbonGroup
        {
            TextLine1 = @"Clipboard",
            TranslationId = @"clipboard"
        };

        _buttonPaste = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Paste",
            TextLine2 = @"Clipboard",
            KeyTip = @"V",
            TranslationId = @"paste"
        };
        _buttonPaste.ToolTipValues.Heading = @"Paste";
        _buttonPaste.ToolTipValues.Description = @"Paste from the clipboard";

        var triple = new KryptonRibbonGroupTriple();
        triple.Items!.Add(_buttonPaste);
        _groupClipboard.Items.Add(triple);
        _tabHome.Groups.Add(_groupClipboard);
        _ribbon.RibbonTabs.Add(_tabHome);

        _qatUndo = new KryptonRibbonQATButton
        {
            Text = @"Undo",
            ToolTipTitle = @"Undo",
            TranslationId = @"qatUndo"
        };
        _ribbon.QATButtons.Add(_qatUndo);

        var context = new KryptonRibbonContext
        {
            ContextName = @"Highlight",
            ContextTitle = @"Highlight Tools"
        };
        _ribbon.RibbonContexts.Add(context);

        var menuItem = new KryptonContextMenuItem(@"Open");
        _ribbon.RibbonFileAppButton.AppButtonMenuItems.Add(menuItem);
        _ribbon.RibbonFileAppTab.FileAppTabText = @"File";

        Controls.Add(_ribbon);

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        Controls.Add(panel);
        panel.BringToFront();

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 90,
            Text =
                @"Issue #4369: RibbonTranslations.xml overlays tab/group/button captions (and optional chrome)." +
                Environment.NewLine +
                @"1) Export (template includes defaults). 2) Import a translated file. 3) Analyze coverage." +
                Environment.NewLine +
                @"4) Apply German writes RibbonTranslations.de.xml into a temp folder and Auto Discovers it." +
                Environment.NewLine +
                @"Designer Smart Tag on KryptonRibbon also import/exports. Toolkit chrome remains on KryptonManager (see Translations XML demo)."
        };

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 40,
            Padding = new Padding(8, 8, 8, 0)
        };

        buttons.Controls.Add(CreateButton(@"Export XML…", OnExport));
        buttons.Controls.Add(CreateButton(@"Import XML…", OnImport));
        buttons.Controls.Add(CreateButton(@"Analyze…", OnAnalyze));
        buttons.Controls.Add(CreateButton(@"Apply German (Auto Discover)", OnApplyGerman));
        buttons.Controls.Add(CreateButton(@"Reset English", OnResetEnglish));

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Text = @"Ready. Tab='Home', Group='Clipboard', Button='Paste'."
        };

        panel.Controls.Add(_lblStatus);
        panel.Controls.Add(buttons);
        panel.Controls.Add(instructions);
    }

    private static KryptonButton CreateButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            Text = text,
            AutoSize = true,
            MinimumSize = new Size(120, 28)
        };
        button.Click += onClick;
        return button;
    }

    private void OnExport(object? sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            OverwritePrompt = true,
            DefaultExt = @"xml",
            FileName = @"RibbonTranslations",
            Filter = @"Ribbon translations (*.xml)|*.xml|JSON (*.json)|*.json|All files (*.*)|(*.*)",
            Title = @"Export Ribbon Translations"
        };

        if (sfd.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var options = new RibbonTranslationOptions { IncludeDefaults = true, IncludeChrome = false };
        if (string.Equals(Path.GetExtension(sfd.FileName), @".json", StringComparison.OrdinalIgnoreCase))
        {
            _ribbon.ExportTranslationsToJsonFile(sfd.FileName, options);
        }
        else
        {
            _ribbon.ExportTranslationsToXmlFile(sfd.FileName, options);
        }

        _lastFilePath = sfd.FileName;
        _lblStatus.Text = $@"Exported '{sfd.FileName}'.";
    }

    private void OnImport(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            CheckFileExists = true,
            FileName = @"RibbonTranslations",
            Filter = @"Ribbon translations (*.xml;*.json)|*.xml;*.json|XML (*.xml)|*.xml|JSON (*.json)|*.json|All files (*.*)|(*.*)",
            Title = @"Import Ribbon Translations"
        };

        if (ofd.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var options = new RibbonTranslationOptions { ResetFirst = true };
        if (string.Equals(Path.GetExtension(ofd.FileName), @".json", StringComparison.OrdinalIgnoreCase))
        {
            _ribbon.ImportTranslationsFromJsonFile(ofd.FileName, options);
        }
        else
        {
            _ribbon.ImportTranslationsFromXmlFile(ofd.FileName, options);
        }

        _lastFilePath = ofd.FileName;
        UpdateStatus(@"Imported");
    }

    private void OnAnalyze(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_lastFilePath) || !File.Exists(_lastFilePath))
        {
            using var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = @"Ribbon translations (*.xml;*.json)|*.xml;*.json|All files (*.*)|(*.*)",
                Title = @"Analyze Ribbon Translations"
            };
            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _lastFilePath = ofd.FileName;
        }

        var path = _lastFilePath ?? string.Empty;
        if (path.Length == 0)
        {
            return;
        }

        var coverage = _ribbon.AnalyzeTranslationsFromFile(path);
        _lblStatus.Text =
            $@"Coverage: {coverage}. Missing={coverage.MissingInFile.Count}, Extra={coverage.ExtraInFile.Count}, Applied={coverage.Applied.Count}.";
    }

    private void OnApplyGerman(object? sender, EventArgs e)
    {
        var folder = Path.Combine(Path.GetTempPath(), @"KryptonRibbonTranslationsDemo");
        Directory.CreateDirectory(folder);

        _tabHome.Text = @"Start";
        _groupClipboard.TextLine1 = @"Zwischenablage";
        _buttonPaste.TextLine1 = @"Einfügen";
        _buttonPaste.TextLine2 = @"Zwischenablage";
        _buttonPaste.ToolTipValues.Heading = @"Einfügen";
        _buttonPaste.ToolTipValues.Description = @"Aus der Zwischenablage einfügen";
        _qatUndo.Text = @"Rückgängig";
        _ribbon.RibbonFileAppTab.FileAppTabText = @"Datei";

        var filePath = Path.Combine(folder, @"RibbonTranslations.de.xml");
        _ribbon.ExportTranslationsToXmlFile(filePath, new RibbonTranslationOptions { IncludeDefaults = true });

        OnResetEnglish(sender, e);

        _ribbon.TranslationsSearchPath = folder;
        _ribbon.EnableAutoDiscoverTranslations = true;
        var loaded = _ribbon.TryAutoDiscoverTranslations(folder, new CultureInfo(@"de"));
        UpdateStatus(loaded
            ? $@"Auto Discover loaded '{filePath}'"
            : @"Auto Discover did not find a German file");
    }

    private void OnResetEnglish(object? sender, EventArgs e)
    {
        _tabHome.Text = @"Home";
        _tabHome.KeyTip = @"H";
        _groupClipboard.TextLine1 = @"Clipboard";
        _buttonPaste.TextLine1 = @"Paste";
        _buttonPaste.TextLine2 = @"Clipboard";
        _buttonPaste.ToolTipValues.Heading = @"Paste";
        _buttonPaste.ToolTipValues.Description = @"Paste from the clipboard";
        _qatUndo.Text = @"Undo";
        _ribbon.RibbonFileAppTab.FileAppTabText = @"File";
        _ribbon.PerformNeedPaint(true);
        UpdateStatus(@"Reset English");
    }

    private void UpdateStatus(string action) =>
        _lblStatus.Text =
            $@"{action}. Tab='{_tabHome.Text}', Group='{_groupClipboard.TextLine1}', Button='{_buttonPaste.TextLine1}'.";
}
