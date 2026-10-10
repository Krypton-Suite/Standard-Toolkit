#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for exporting/importing KryptonManager toolkit strings through a versioned ToolkitTranslations.xml file.
/// </summary>
public sealed class TranslationsXmlDemoForm : KryptonForm
{
    private readonly KryptonTextBox _txtOk;
    private readonly KryptonTextBox _txtCancel;
    private readonly KryptonWrapLabel _lblOkValue;
    private readonly KryptonWrapLabel _lblCancelValue;
    private readonly KryptonWrapLabel _lblMoreDetailsValue;
    private readonly KryptonWrapLabel _lblCommonValue;
    private readonly KryptonWrapLabel _lblControlBoxValue;
    private readonly KryptonWrapLabel _lblRawLoaderValue;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonButton _btnApply;
    private readonly KryptonButton _btnExport;
    private readonly KryptonButton _btnImport;
    private readonly KryptonButton _btnReset;
    private readonly KryptonButton _btnValidate;
    private readonly KryptonButton _btnAnalyze;
    private readonly KryptonButton _btnMerge;
    private readonly KryptonButton _btnExportReport;
    private readonly KryptonCheckBox _chkIncludeDefaults;
    private readonly KryptonCheckBox _chkUseWindowsLanguagePack;
    private readonly KryptonCheckBox _chkStrictCatalog;
    private readonly KryptonCheckBox _chkAutoTranslate;
    private readonly KryptonComboBox _cmbCulture;
    private readonly IKryptonStringTranslator _demoTranslator = new DemoPrefixTranslator();
    private bool _installedDemoTranslator;

    // Path of the most recently imported/exported file, used for round-trip validation.
    private string? _lastFilePath;

    public TranslationsXmlDemoForm()
    {
        Text = @"ToolkitTranslations.xml Demo";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(860, 600);
        MinimumSize = new Size(720, 480);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 110,
            Text =
                @"1) Edit OK/Cancel and Apply. 2) Use Windows language pack as needed." + Environment.NewLine +
                @"3) Export/Import/Validate round-trips. Analyze groups missing/extra keys by section." + Environment.NewLine +
                @"4) Merge Missing upgrades an older file with new English placeholders while preserving translations." + Environment.NewLine +
                @"Auto-translate is off unless checked. It only fills new keys, and only through a translator you supply." + Environment.NewLine +
                @"Strict catalog fails import when keys are missing or unknown. Default import stays tolerant." + Environment.NewLine +
                @"Ribbon tab/group/button captions use RibbonTranslations.xml — open Ribbon Translations (#4369) from the start screen."
        };

        var editsPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 140,
            Padding = new Padding(12)
        };

        var editsTable = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2
        };
        editsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        editsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

        var lblOk = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Text = @"GeneralStrings.OK:"
        };
        _txtOk = new KryptonTextBox { Dock = DockStyle.Fill };

        var lblCancel = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Text = @"GeneralStrings.Cancel:"
        };
        _txtCancel = new KryptonTextBox { Dock = DockStyle.Fill };

        editsTable.Controls.Add(lblOk, 0, 0);
        editsTable.Controls.Add(_txtOk, 1, 0);
        editsTable.Controls.Add(lblCancel, 0, 1);
        editsTable.Controls.Add(_txtCancel, 1, 1);
        editsPanel.Controls.Add(editsTable);

        var optionsPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 64,
            Padding = new Padding(12, 4, 12, 4)
        };
        _chkIncludeDefaults = new KryptonCheckBox
        {
            LabelStyle = LabelStyle.NormalPanel,
            Values = { Text = @"Include defaults in export (shows all overridable strings)" },
            Checked = false
        };
        _chkUseWindowsLanguagePack = new KryptonCheckBox
        {
            LabelStyle = LabelStyle.NormalPanel,
            Values = { Text = @"Use Windows language pack strings" },
            Checked = KryptonManager.Strings.UseWindowsLanguagePackStrings
        };
        _chkUseWindowsLanguagePack.CheckedChanged += (_, _) =>
        {
            KryptonManager.Strings.UseWindowsLanguagePackStrings = _chkUseWindowsLanguagePack.Checked;
            UpdateDisplayedStrings();
            _lblStatus?.Text = _chkUseWindowsLanguagePack.Checked
                ? @"Using Windows language-pack strings for matching dialog / Explorer labels."
                : @"Using toolkit / custom translation strings.";
        };

        var cultureLabel = new KryptonWrapLabel
        {
            Text = @"  UI Culture:",
            AutoSize = true
        };

        _cmbCulture = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 130
        };
        _cmbCulture.Items.AddRange(new object[]
        {
            @"en-US", @"en-GB", @"de-DE", @"fr-FR", @"es-ES",
            @"it-IT", @"pt-BR", @"ja-JP", @"zh-CN", @"ko-KR"
        });
        _cmbCulture.SelectedItem = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        if (_cmbCulture.SelectedIndex < 0)
        {
            _cmbCulture.Items.Insert(0, System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
            _cmbCulture.SelectedIndex = 0;
        }

        _cmbCulture.SelectedIndexChanged += OnCultureChanged;

        var optionsFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            AutoSize = true
        };
        optionsFlow.Controls.Add(_chkIncludeDefaults);
        optionsFlow.Controls.Add(_chkUseWindowsLanguagePack);
        _chkStrictCatalog = new KryptonCheckBox
        {
            LabelStyle = LabelStyle.NormalPanel,
            Values = { Text = @"Strict catalog (fail import on drift)" },
            Checked = false
        };
        optionsFlow.Controls.Add(_chkStrictCatalog);
        _chkAutoTranslate = new KryptonCheckBox
        {
            LabelStyle = LabelStyle.NormalPanel,
            Values = { Text = @"Auto-translate missing keys" },
            Checked = false
        };
        _chkAutoTranslate.CheckedChanged += (_, _) =>
        {
            KryptonStringTranslation.AutoTranslateMissingStrings = _chkAutoTranslate.Checked;
            if (_chkAutoTranslate.Checked)
            {
                if (KryptonStringTranslation.Translator == null)
                {
                    KryptonStringTranslation.Translator = _demoTranslator;
                    _installedDemoTranslator = true;
                }
            }
            else if (_installedDemoTranslator)
            {
                KryptonStringTranslation.Translator = null;
                _installedDemoTranslator = false;
            }
        };
        optionsFlow.Controls.Add(_chkAutoTranslate);
        optionsFlow.Controls.Add(cultureLabel);
        optionsFlow.Controls.Add(_cmbCulture);
        optionsPanel.Controls.Add(optionsFlow);

        var buttonsPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 84,
            Padding = new Padding(12, 6, 12, 6)
        };
        var buttonsFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            AutoSize = true
        };

        _btnApply    = new KryptonButton { Values = { Text = @"Apply" } };
        _btnExport   = new KryptonButton { Values = { Text = @"Export..." } };
        _btnImport   = new KryptonButton { Values = { Text = @"Import..." } };
        _btnReset    = new KryptonButton { Values = { Text = @"Reset to Default" } };
        _btnValidate = new KryptonButton { Values = { Text = @"Validate Round-trip" }, Enabled = false };
        _btnAnalyze  = new KryptonButton { Values = { Text = @"Analyze..." } };
        _btnMerge    = new KryptonButton { Values = { Text = @"Merge Missing..." } };
        _btnExportReport = new KryptonButton { Values = { Text = @"Save coverage report..." }, Enabled = false };

        buttonsFlow.Controls.Add(_btnApply);
        buttonsFlow.Controls.Add(_btnExport);
        buttonsFlow.Controls.Add(_btnImport);
        buttonsFlow.Controls.Add(_btnReset);
        buttonsFlow.Controls.Add(_btnValidate);
        buttonsFlow.Controls.Add(_btnAnalyze);
        buttonsFlow.Controls.Add(_btnMerge);
        buttonsFlow.Controls.Add(_btnExportReport);
        buttonsPanel.Controls.Add(buttonsFlow);

        var valuesPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 220,
            Padding = new Padding(12)
        };
        var valuesFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true
        };

        _lblOkValue         = new KryptonWrapLabel { AutoSize = true, Text = @"OK: " };
        _lblCancelValue     = new KryptonWrapLabel { AutoSize = true, Text = @"Cancel: " };
        _lblMoreDetailsValue = new KryptonWrapLabel { AutoSize = true, Text = @"MessageBoxStrings.MoreDetails: " };
        _lblCommonValue = new KryptonWrapLabel { AutoSize = true, Text = @"CommonStrings: " };
        _lblControlBoxValue = new KryptonWrapLabel { AutoSize = true, Text = @"ControlBox: " };
        _lblRawLoaderValue = new KryptonWrapLabel { AutoSize = true, Text = @"Raw loader: " };

        valuesFlow.Controls.Add(_lblOkValue);
        valuesFlow.Controls.Add(_lblCancelValue);
        valuesFlow.Controls.Add(_lblMoreDetailsValue);
        valuesFlow.Controls.Add(_lblCommonValue);
        valuesFlow.Controls.Add(_lblControlBoxValue);
        valuesFlow.Controls.Add(_lblRawLoaderValue);
        valuesPanel.Controls.Add(valuesFlow);

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            Text = string.Empty
        };

        Controls.Add(_lblStatus);
        Controls.Add(valuesPanel);
        Controls.Add(buttonsPanel);
        Controls.Add(optionsPanel);
        Controls.Add(editsPanel);
        Controls.Add(instructions);

        _btnApply.Click += (_, _) =>
        {
            KryptonManager.Strings.GeneralStrings.OK = _txtOk.Text;
            KryptonManager.Strings.GeneralStrings.Cancel = _txtCancel.Text;
            UpdateDisplayedStrings();
        };

        _btnExport.Click += (_, _) =>
        {
            using var sfd = new SaveFileDialog
            {
                OverwritePrompt = true,
                FileName = @"ToolkitTranslations",
                DefaultExt = @"xml",
                Filter = @"Translations files (*.xml)|*.xml|All files (*.*)|(*.*)",
                Title = @"Save Toolkit Translations"
            };

            if (sfd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(sfd.FileName))
            {
                return;
            }

            KryptonManager.Strings.ExportToXmlFile(sfd.FileName, includeDefaults: _chkIncludeDefaults.Checked);
            _lastFilePath = sfd.FileName;
            _btnValidate.Enabled = true;
            _lblStatus.Text = $@"Exported to: {sfd.FileName}";
        };

        _btnImport.Click += (_, _) =>
        {
            using var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                FileName = @"ToolkitTranslations",
                DefaultExt = @"xml",
                Filter = @"Translations files (*.xml)|*.xml|All files (*.*)|(*.*)",
                Title = @"Load Toolkit Translations"
            };

            if (ofd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(ofd.FileName))
            {
                return;
            }

            KryptonManager.Strings.ImportFromXmlFile(ofd.FileName, resetFirst: true, refreshOpenForms: true, strictCatalog: _chkStrictCatalog.Checked);
            _lastFilePath = ofd.FileName;
            _btnValidate.Enabled = true;
            UpdateDisplayedStrings();
            _lblStatus.Text = $@"Imported from: {ofd.FileName}";
        };

        _btnReset.Click += (_, _) =>
        {
            KryptonManager.Strings.Reset();
            _chkUseWindowsLanguagePack.Checked = KryptonManager.Strings.UseWindowsLanguagePackStrings;
            UpdateDisplayedStrings();
            _lblStatus.Text = @"Reset to default translations.";
        };

        _btnValidate.Click += (_, _) => RunRoundTripValidation();
        _btnAnalyze.Click += (_, _) => RunAnalyze();
        _btnMerge.Click += (_, _) => RunMergeMissing();
        _btnExportReport.Click += (_, _) => RunExportReport();

        UpdateDisplayedStrings();
    }

    private void RunAnalyze()
    {
        using var ofd = new OpenFileDialog
        {
            CheckFileExists = true,
            CheckPathExists = true,
            FileName = @"ToolkitTranslations",
            Filter = @"Translations files (*.xml;*.json)|*.xml;*.json|XML (*.xml)|*.xml|JSON (*.json)|*.json|All files (*.*)|(*.*)",
            Title = @"Analyze Translations Coverage"
        };

        if (ofd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(ofd.FileName))
        {
            return;
        }

        try
        {
            var coverage = KryptonManager.AnalyzeTranslationsFromFile(ofd.FileName);
            _lastFilePath = ofd.FileName;
            _btnValidate.Enabled = true;
            _btnExportReport.Enabled = true;

            _lblStatus.Text =
                $@"Analyze '{ofd.FileName}': {coverage}. ToolkitVersion={coverage.ToolkitVersion ?? @"n/a"}.{Environment.NewLine}" +
                @"Missing by section:" + Environment.NewLine +
                ToolkitStringsCoverage.FormatGrouped(coverage.MissingInFile) + Environment.NewLine +
                @"Extra by section:" + Environment.NewLine +
                ToolkitStringsCoverage.FormatGrouped(coverage.ExtraInFile);
        }
        catch (Exception ex)
        {
            _lblStatus.Text = $@"Analyze: ERROR — {ex.Message}";
        }
    }

    private void RunExportReport()
    {
        var path = _lastFilePath;
        if (path is null || string.IsNullOrWhiteSpace(path))
        {
            _lblStatus.Text = @"Analyze a file before saving a coverage report.";
            return;
        }

        using var sfd = new SaveFileDialog
        {
            OverwritePrompt = true,
            FileName = @"ToolkitTranslations-coverage",
            Filter = @"CSV (*.csv)|*.csv|JSON (*.json)|*.json",
            Title = @"Save Coverage Report"
        };

        if (sfd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(sfd.FileName))
        {
            return;
        }

        try
        {
            var coverage = KryptonManager.AnalyzeTranslationsFromFile(path);
            coverage.ExportReport(sfd.FileName);
            _lblStatus.Text = $@"Coverage report saved to {sfd.FileName}.";
        }
        catch (Exception ex)
        {
            _lblStatus.Text = $@"Coverage report: ERROR — {ex.Message}";
        }
    }

    private void RunMergeMissing()
    {
        using var ofd = new OpenFileDialog
        {
            CheckFileExists = true,
            CheckPathExists = true,
            FileName = @"ToolkitTranslations",
            Filter = @"Translations files (*.xml;*.json)|*.xml;*.json|XML (*.xml)|*.xml|JSON (*.json)|*.json|All files (*.*)|(*.*)",
            Title = @"Merge Missing Translations into File"
        };

        if (ofd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(ofd.FileName))
        {
            return;
        }

        try
        {
            var before = KryptonManager.AnalyzeTranslationsFromFile(ofd.FileName);
            var after = KryptonManager.MergeMissingTranslationsToFile(ofd.FileName, includeDefaults: true);
            _lastFilePath = ofd.FileName;
            _btnValidate.Enabled = true;
            UpdateDisplayedStrings();

            _lblStatus.Text =
                $@"Merge '{ofd.FileName}': was missing {before.MissingInFile.Count}, extra {before.ExtraInFile.Count}; " +
                $@"now missing {after.MissingInFile.Count}. File rewritten with English placeholders for new keys.";
        }
        catch (Exception ex)
        {
            _lblStatus.Text = $@"Merge: ERROR — {ex.Message}";
        }
    }

    private void UpdateDisplayedStrings()
    {
        var strings = KryptonManager.Strings;
        var common = strings.CommonStrings;

        var ok          = strings.GeneralStrings.OK;
        var cancel      = strings.GeneralStrings.Cancel;
        var moreDetails = strings.MessageBoxStrings.MoreDetails;

        _txtOk.Text     = ok;
        _txtCancel.Text = cancel;

        _lblOkValue.Text          = $@"GeneralStrings.OK (= CommonStrings.General.OK): {ok}";
        _lblCancelValue.Text      = $@"GeneralStrings.Cancel: {cancel}";
        _lblMoreDetailsValue.Text = $@"MessageBoxStrings.MoreDetails: {moreDetails}";
        _lblCommonValue.Text =
            $@"CommonStrings.Commands.Apply / Next: {common.Commands.Apply} / {common.Commands.Next}; FileSystem.Name: {common.FileSystem.ColumnNameName}";
        _lblControlBoxValue.Text =
            $@"ControlBox Minimize/Maximize/Restore/Close: {common.ControlBox.Minimize} / {common.ControlBox.Maximize} / {common.ControlBox.Restore} / {common.ControlBox.Close}";

        var rawOk = Krypton.Toolkit.Utilities.WindowsSystemStringLoader.TryLoad(@"user32.dll", 800, out var rawOkValue)
            ? rawOkValue
            : @"(unavailable)";
        var rawRejected = Krypton.Toolkit.Utilities.WindowsSystemStringLoader.TryLoad(@"..\evil.dll", 800, out _)
            ? @"accepted (unexpected)"
            : @"rejected";
        _lblRawLoaderValue.Text = $@"Raw loader user32:800 => {rawOk}; path traversal => {rawRejected}";
    }

    private void RunRoundTripValidation()
    {
        if (string.IsNullOrWhiteSpace(_lastFilePath) || !System.IO.File.Exists(_lastFilePath))
        {
            _lblStatus.Text = @"Validate: no file to compare against. Export or Import first.";
            return;
        }

        try
        {
            // Export the current live strings to an in-memory buffer.
            using var memStream = new System.IO.MemoryStream();
            KryptonManager.Strings.ExportToStream(memStream, includeDefaults: true);
            memStream.Position = 0;
            var liveDoc = new System.Xml.XmlDocument();
            liveDoc.Load(memStream);

            // Load the on-disk file.
            var diskDoc = new System.Xml.XmlDocument();
            diskDoc.Load(_lastFilePath);

            // Compare each Value attribute in the disk document against the live document.
            var diskStrings = ExtractValues(diskDoc);
            var liveStrings = ExtractValues(liveDoc);

            var mismatches = new System.Collections.Generic.List<string>();
            foreach (var kv in diskStrings)
            {
                if (!liveStrings.TryGetValue(kv.Key, out var liveValue) || liveValue != kv.Value)
                {
                    mismatches.Add(kv.Key);
                }
            }

            _lblStatus.Text = mismatches.Count == 0
                ? $@"Validate: PASS — {diskStrings.Count} strings match the live toolkit strings."
                : $@"Validate: FAIL — {mismatches.Count} mismatch(es): {string.Join(@", ", mismatches)}";
        }
        catch (System.Exception ex)
        {
            _lblStatus.Text = $@"Validate: ERROR — {ex.Message}";
        }
    }

    private static System.Collections.Generic.Dictionary<string, string?> ExtractValues(System.Xml.XmlDocument doc)
    {
        var result = new System.Collections.Generic.Dictionary<string, string?>(System.StringComparer.Ordinal);
        if (doc.DocumentElement == null)
        {
            return result;
        }

        CollectValues(doc.DocumentElement, string.Empty, result);
        return result;
    }

    private void OnCultureChanged(object? sender, EventArgs e)
    {
        var selected = _cmbCulture.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        var loaded = KryptonManager.TrySwitchTranslationsCulture(selected!, refreshOpenForms: true);
        _lblStatus.Text = loaded
            ? $@"Switched to '{selected}' and loaded matching translations."
            : $@"Switched UI culture to '{selected}' (no matching translations file; restored built-in defaults).";
    }

    private static void CollectValues(System.Xml.XmlElement element, string prefix, System.Collections.Generic.Dictionary<string, string?> result)
    {
        foreach (System.Xml.XmlNode child in element.ChildNodes)
        {
            if (child is not System.Xml.XmlElement childEl)
            {
                continue;
            }

            var key = string.IsNullOrEmpty(prefix) ? childEl.Name : $@"{prefix}.{childEl.Name}";

            if (childEl.HasAttribute(@"Value"))
            {
                var isNull = string.Equals(childEl.GetAttribute(@"IsNull"), @"true", System.StringComparison.OrdinalIgnoreCase);
                result[key] = isNull ? null : childEl.GetAttribute(@"Value");
            }
            else
            {
                CollectValues(childEl, key, result);
            }
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (_installedDemoTranslator && ReferenceEquals(KryptonStringTranslation.Translator, _demoTranslator))
        {
            KryptonStringTranslation.Translator = null;
        }

        KryptonStringTranslation.AutoTranslateMissingStrings = false;
        base.OnFormClosed(e);
    }

    private sealed class DemoPrefixTranslator : IKryptonStringTranslator
    {
        public string? Translate(string text, CultureInfo sourceCulture, CultureInfo targetCulture, string keyPath) =>
            $@"[{targetCulture.TwoLetterISOLanguageName}] {text}";
    }
}

