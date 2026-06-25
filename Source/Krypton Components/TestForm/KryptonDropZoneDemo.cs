#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of <see cref="KryptonDropZone"/>: Card and Classic layouts,
/// drag-and-drop, click-to-browse, validation, limits, preview thumbnails, undo, and programmatic API.
/// </summary>
public sealed partial class KryptonDropZoneDemo : KryptonForm
{
    private KryptonCheckBox _chkCustomValidation = null!;
    private KryptonCheckBox _chkShowFileList = null!;
    private KryptonCheckBox _chkShowBrowse = null!;
    private KryptonCheckBox _chkShowStatus = null!;
    private KryptonCheckBox _chkShowClear = null!;
    private KryptonCheckBox _chkAllowDirectories = null!;
    private KryptonCheckBox _chkSearchSubdirectories = null!;
    private KryptonCheckBox _chkEnableUndo = null!;
    private KryptonCheckBox _chkShowUploadQuota = null!;
    private KryptonNumericUpDown _numMaxFileCount = null!;
    private KryptonNumericUpDown _numMaxFileSizeKb = null!;
    private KryptonNumericUpDown _numUploadQuotaKb = null!;
    private KryptonTextBox _txtAllowedExtensions = null!;
    private KryptonTextBox _txtDropZoneText = null!;
    private KryptonTextBox _txtHeaderText = null!;
    private KryptonTextBox _txtPreviewHeader = null!;
    private KryptonComboBox _cmbLayout = null!;
    private KryptonComboBox _cmbExtensionFilter = null!;
    private KryptonCheckBox _chkShowUploadIcon = null!;
    private KryptonCheckBox _chkShowStripedDragFeedback = null!;
    private KryptonCheckBox _chkShowActionButtons = null!;
    private KryptonCheckBox _chkUsePaletteColors = null!;

    public KryptonDropZoneDemo()
    {
        InitializeComponent();
        ApplyCardDemoDefaults(kdzDropZone);
        WireDropZoneEvents(kdzDropZone);
        BuildSettingsPanel();
        ApplyPresetDefault();
        RefreshSummary();
        Log(@"Demo opened. Card layout: drag images onto the zone, click to browse, then Submit. Use Strict limits to try quota stripes.");
    }

    private void kbtnClearLog_Click(object? sender, EventArgs e) => ktbxEventLog.Clear();

    private void BuildSettingsPanel()
    {
        var stack = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 1,
            Padding = new Padding(0, 0, 8, 8)
        };
        stack.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        stack.Controls.Add(BuildAppearanceGroup(), 0, stack.RowCount++);
        stack.Controls.Add(BuildBehaviorGroup(), 0, stack.RowCount++);
        stack.Controls.Add(BuildLimitsGroup(), 0, stack.RowCount++);
        stack.Controls.Add(BuildPresetsGroup(), 0, stack.RowCount++);
        stack.Controls.Add(BuildApiGroup(), 0, stack.RowCount++);

        kpnlSettings.Controls.Add(stack);
    }

    private KryptonHeaderGroup BuildAppearanceGroup()
    {
        _txtDropZoneText = CreateTextBox(@"Drag and drop files here or click");
        _txtDropZoneText.TextChanged += (_, _) =>
        {
            kdzDropZone.DropZoneText = _txtDropZoneText.Text;
            Log($@"DropZoneText = '{kdzDropZone.DropZoneText}'");
        };

        _cmbLayout = new KryptonComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _cmbLayout.Items.AddRange(new object[] { @"Classic", @"Card" });
        _cmbLayout.SelectedIndex = 1;
        _cmbLayout.SelectedIndexChanged += (_, _) =>
        {
            bool card = _cmbLayout.SelectedIndex == 1;
            kdzDropZone.Appearance.Layout = card ? DropZoneLayout.Card : DropZoneLayout.Classic;
            SetCardAppearanceControlsEnabled(card);
            RefreshListChrome();
            Log($@"Appearance.Layout = {kdzDropZone.Appearance.Layout}");
        };

        _txtHeaderText = CreateTextBox(@"Upload File");
        _txtHeaderText.TextChanged += (_, _) =>
        {
            kdzDropZone.Strings.HeaderText = _txtHeaderText.Text;
            Log($@"Strings.HeaderText = '{kdzDropZone.Strings.HeaderText}'");
        };

        _txtPreviewHeader = CreateTextBox(@"Preview:");
        _txtPreviewHeader.TextChanged += (_, _) =>
        {
            kdzDropZone.Strings.PreviewHeader = _txtPreviewHeader.Text;
            Log($@"Strings.PreviewHeader = '{kdzDropZone.Strings.PreviewHeader}'");
        };

        _chkShowFileList = CreateCheckBox(@"Show file list / preview", true, c => { kdzDropZone.ShowFileListView = c.Checked; Log($@"ShowFileListView = {c.Checked}"); });
        _chkShowBrowse = CreateCheckBox(@"Show browse button (Classic)", false, c => { kdzDropZone.ShowBrowseButton = c.Checked; Log($@"ShowBrowseButton = {c.Checked}"); });
        _chkShowStatus = CreateCheckBox(@"Show status label (Classic)", false, c => { kdzDropZone.ShowStatusLabel = c.Checked; Log($@"ShowStatusLabel = {c.Checked}"); });
        _chkShowClear = CreateCheckBox(@"Show clear button (Classic)", false, c =>
        {
            kdzDropZone.ShowClearButton = c.Checked;
            RefreshListChrome();
            Log($@"ShowClearButton = {c.Checked}");
        });

        _chkShowUploadIcon = CreateCheckBox(@"Show upload icon (Card)", true, c =>
        {
            kdzDropZone.Appearance.ShowUploadIcon = c.Checked;
            Log($@"Appearance.ShowUploadIcon = {c.Checked}");
        });
        _chkShowStripedDragFeedback = CreateCheckBox(@"Show striped drag feedback (Card)", true, c =>
        {
            kdzDropZone.Appearance.ShowStripedDragFeedback = c.Checked;
            Log($@"Appearance.ShowStripedDragFeedback = {c.Checked}");
        });
        _chkShowActionButtons = CreateCheckBox(@"Show Cancel / Submit buttons (Card)", true, c =>
        {
            kdzDropZone.Appearance.ShowActionButtons = c.Checked;
            Log($@"Appearance.ShowActionButtons = {c.Checked}");
        });
        _chkUsePaletteColors = CreateCheckBox(@"Use Krypton palette colors (Card)", true, c =>
        {
            kdzDropZone.Appearance.UsePaletteColors = c.Checked;
            Log($@"Appearance.UsePaletteColors = {c.Checked}");
        });

        var chkEnableAnimation = CreateCheckBox(@"Enable drop-zone animations", true, c =>
        {
            kdzDropZone.Animation.Enabled = c.Checked;
            Log($@"Animation.Enabled = {c.Checked}");
        });

        SetCardAppearanceControlsEnabled(card: true);

        var uploadIconFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true
        };
        uploadIconFlow.Controls.Add(CreateActionButton(@"Browse for icon…", BrowseForUploadIcon));
        uploadIconFlow.Controls.Add(CreateActionButton(@"Use built-in icon", () =>
        {
            kdzDropZone.Appearance.UploadIcon = null;
            Log(@"Appearance.UploadIcon = null (built-in icon).");
        }));

        return WrapGroup(@"Appearance",
            FieldLabel(@"Layout:"), _cmbLayout,
            FieldLabel(@"Drop zone text:"), _txtDropZoneText,
            FieldLabel(@"Header text (Card):"), _txtHeaderText,
            FieldLabel(@"Preview header (Card):"), _txtPreviewHeader,
            FieldLabel(@"Upload icon (Card):"), uploadIconFlow,
            _chkShowFileList, _chkShowBrowse, _chkShowStatus, _chkShowClear,
            _chkShowUploadIcon, _chkShowStripedDragFeedback, _chkShowActionButtons, _chkUsePaletteColors,
            chkEnableAnimation);
    }

    private KryptonHeaderGroup BuildBehaviorGroup()
    {
        _txtAllowedExtensions = CreateTextBox(@".png, .jpg, .jpeg, .gif, .bmp");
        _txtAllowedExtensions.Leave += (_, _) => ApplyAllowedExtensionsFromText();

        _chkAllowDirectories = CreateCheckBox(@"Allow dropping folders", true, c => { kdzDropZone.AllowDirectories = c.Checked; Log($@"AllowDirectories = {c.Checked}"); });
        _chkSearchSubdirectories = CreateCheckBox(@"Search subdirectories when dropping folders", false, c => { kdzDropZone.SearchSubdirectories = c.Checked; Log($@"SearchSubdirectories = {c.Checked}"); });
        _chkEnableUndo = CreateCheckBox(@"Enable undo (Ctrl+Z)", true, c => { kdzDropZone.EnableUndo = c.Checked; RefreshSummary(); Log($@"EnableUndo = {c.Checked}"); });
        _chkCustomValidation = CreateCheckBox(@"Reject files whose name contains 'demo' (FileValidating)", false, c => Log($@"Custom FileValidating = {c.Checked}"));

        return WrapGroup(@"Behavior", FieldLabel(@"Allowed extensions (comma-separated):"), _txtAllowedExtensions,
            _chkAllowDirectories, _chkSearchSubdirectories, _chkEnableUndo, _chkCustomValidation);
    }

    private KryptonHeaderGroup BuildLimitsGroup()
    {
        _numMaxFileCount = CreateNumeric(0, 0, 999, 0);
        _numMaxFileCount.ValueChanged += (_, _) =>
        {
            kdzDropZone.MaxFileCount = (int)_numMaxFileCount.Value;
            Log($@"MaxFileCount = {kdzDropZone.MaxFileCount} (0 = unlimited)");
        };

        _numMaxFileSizeKb = CreateNumeric(0, 0, 1024 * 1024, 0);
        _numMaxFileSizeKb.ValueChanged += (_, _) =>
        {
            kdzDropZone.MaxFileSize = (long)_numMaxFileSizeKb.Value * 1024L;
            Log($@"MaxFileSize = {kdzDropZone.MaxFileSize} bytes ({_numMaxFileSizeKb.Value} KB, 0 = unlimited)");
        };

        _numUploadQuotaKb = CreateNumeric(0, 0, 1024 * 1024, 0);
        _numUploadQuotaKb.ValueChanged += (_, _) =>
        {
            kdzDropZone.UploadSizeQuota = (long)_numUploadQuotaKb.Value * 1024L;
            RefreshSummary();
            Log($@"UploadSizeQuota = {kdzDropZone.UploadSizeQuota} bytes ({_numUploadQuotaKb.Value} KB, 0 = unlimited)");
        };

        _chkShowUploadQuota = CreateCheckBox(@"Show upload quota progress bar", false, c =>
        {
            kdzDropZone.ShowUploadQuotaProgressBar = c.Checked;
            RefreshSummary();
            Log($@"ShowUploadQuotaProgressBar = {c.Checked}");
        });

        var limitsHost = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 2,
            RowCount = 3
        };
        limitsHost.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        limitsHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        limitsHost.Controls.Add(new KryptonLabel { Text = @"Max file count (0 = unlimited):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        limitsHost.Controls.Add(_numMaxFileCount, 1, 0);
        limitsHost.Controls.Add(new KryptonLabel { Text = @"Max file size (KB, 0 = unlimited):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        limitsHost.Controls.Add(_numMaxFileSizeKb, 1, 1);
        limitsHost.Controls.Add(new KryptonLabel { Text = @"Upload quota (KB, 0 = unlimited):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
        limitsHost.Controls.Add(_numUploadQuotaKb, 1, 2);

        return WrapGroup(@"Limits", limitsHost, _chkShowUploadQuota);
    }

    private KryptonHeaderGroup BuildPresetsGroup()
    {
        var flow = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true
        };

        flow.Controls.Add(CreatePresetButton(@"Default (Card)", ApplyPresetDefault));
        flow.Controls.Add(CreatePresetButton(@"Classic layout", ApplyPresetClassic));
        flow.Controls.Add(CreatePresetButton(@"Images only", ApplyPresetImagesOnly));
        flow.Controls.Add(CreatePresetButton(@"Strict limits", ApplyPresetStrictLimits));
        flow.Controls.Add(CreatePresetButton(@"Recursive folders", ApplyPresetRecursiveFolders));

        return WrapGroup(@"Presets", flow);
    }

    private KryptonHeaderGroup BuildApiGroup()
    {
        _cmbExtensionFilter = new KryptonComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _cmbExtensionFilter.Items.AddRange(new object[] { @".txt", @".png", @".jpg", @".pdf", @".doc", @".docx" });
        _cmbExtensionFilter.SelectedIndex = 0;

        var flow = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true
        };

        flow.Controls.Add(CreateActionButton(@"Undo", () =>
        {
            if (!kdzDropZone.CanUndo)
            {
                Log(@"Undo: nothing to undo.");
                return;
            }

            kdzDropZone.Undo();
            RefreshSummary();
            Log(@"Undo() called.");
        }));
        flow.Controls.Add(CreateActionButton(@"Clear all", () => kdzDropZone.ClearFiles()));
        flow.Controls.Add(CreateActionButton(@"Remove selected", () => kdzDropZone.RemoveSelectedFiles()));
        flow.Controls.Add(CreateActionButton(@"Save list…", SaveListToFile));
        flow.Controls.Add(CreateActionButton(@"Load list…", LoadListFromFile));
        flow.Controls.Add(CreateActionButton(@"List by extension", ListByExtension));

        return WrapGroup(@"Programmatic API", FieldLabel(@"Extension filter:"), _cmbExtensionFilter, flow);
    }

    private void WireDropZoneEvents(KryptonDropZone dropZone)
    {
        dropZone.FilesDropped += OnFilesDropped;
        dropZone.FileValidating += OnFileValidating;
        dropZone.FilesCleared += OnFilesCleared;
        dropZone.FilesSubmit += OnFilesSubmit;
        dropZone.PropertyChanged += OnDropZonePropertyChanged;
    }

    private void OnFilesSubmit(object? sender, EventArgs e)
    {
        Log($@"FilesSubmit: {kdzDropZone.FileCount} file(s) ready — {string.Join(@"; ", kdzDropZone.DroppedFiles.Take(3))}"
            + (kdzDropZone.FileCount > 3 ? @" …" : string.Empty));
    }

    private static void ApplyCardDemoDefaults(KryptonDropZone dropZone)
    {
        dropZone.Appearance.Layout = DropZoneLayout.Card;
        dropZone.Appearance.ShowUploadIcon = true;
        dropZone.Appearance.ShowStripedDragFeedback = true;
        dropZone.Appearance.ShowActionButtons = true;
        dropZone.Appearance.UsePaletteColors = true;
        dropZone.Strings.HeaderText = @"Upload File";
        dropZone.DropZoneText = @"Drag and drop files here or click";
        dropZone.Strings.PreviewHeader = @"Preview:";
        dropZone.Strings.CancelButton = @"Cancel";
        dropZone.Strings.SubmitButton = @"Submit";
        dropZone.ShowBrowseButton = false;
        dropZone.ShowClearButton = false;
        dropZone.ShowStatusLabel = false;
        dropZone.AllowedExtensions = [".png", ".jpg", ".jpeg", ".gif", ".bmp"];
    }

    private static void ApplyClassicDemoDefaults(KryptonDropZone dropZone)
    {
        dropZone.Appearance.Layout = DropZoneLayout.Classic;
        dropZone.Strings.HeaderText = string.Empty;
        dropZone.DropZoneText = @"Drag files here, or browse";
        dropZone.Strings.PreviewHeader = @"Preview:";
        dropZone.ShowBrowseButton = true;
        dropZone.ShowClearButton = true;
        dropZone.ShowStatusLabel = true;
        dropZone.AllowedExtensions =
            [".txt", ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".xls", ".xlsx", ".csv", ".pdf", ".doc", ".docx"];
    }

    private void SetCardAppearanceControlsEnabled(bool card)
    {
        _txtHeaderText.Enabled = card;
        _txtPreviewHeader.Enabled = card;
        _chkShowUploadIcon.Enabled = card;
        _chkShowStripedDragFeedback.Enabled = card;
        _chkShowActionButtons.Enabled = card;
        _chkUsePaletteColors.Enabled = card;
        _chkShowBrowse.Enabled = !card;
        _chkShowStatus.Enabled = !card;
        _chkShowClear.Enabled = !card;
    }

    private void BrowseForUploadIcon()
    {
        using var dialog = new OpenFileDialog
        {
            Title = @"Select upload icon",
            Filter = @"Image files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.ico)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.ico|All files (*.*)|*.*"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            using Image loaded = Image.FromFile(dialog.FileName);
            kdzDropZone.Appearance.UploadIcon = new Bitmap(loaded);
            Log($@"Appearance.UploadIcon set from '{dialog.FileName}' (palette-tinted).");
        }
        catch (Exception ex)
        {
            Log($@"Failed to load icon: {ex.Message}");
        }
    }

    private void OnFilesDropped(object? sender, KryptonDropZone.FilesDroppedEventArgs e)
    {
        RefreshSummary();
        Log($@"FilesDropped: {e.ValidFiles.Count} accepted, {e.InvalidFiles.Count} rejected, {e.AllFiles.Count} total in batch. Scenario: {kdzDropZone.CurrentAnimationScenario}.");
        if (e.ValidFiles.Count > 0)
        {
            Log(@"  Accepted: " + string.Join(@"; ", e.ValidFiles.Take(5)) + (e.ValidFiles.Count > 5 ? @" …" : string.Empty));
        }

        if (e.InvalidFiles.Count > 0)
        {
            Log(@"  Rejected: " + string.Join(@"; ", e.InvalidFiles.Take(5)) + (e.InvalidFiles.Count > 5 ? @" …" : string.Empty));
        }
    }

    private void OnFileValidating(object? sender, KryptonDropZone.FileValidationEventArgs e)
    {
        if (!_chkCustomValidation.Checked)
        {
            return;
        }

        if (System.IO.Path.GetFileName(e.FilePath).IndexOf(@"demo", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            e.Cancel = true;
            e.ValidationMessage = @"Demo rule: file names containing 'demo' are rejected.";
            Log($@"FileValidating cancelled: {e.FilePath} ({e.ValidationMessage})");
        }
    }

    private void OnFilesCleared(object? sender, EventArgs e)
    {
        RefreshSummary();
        Log(@"FilesCleared.");
    }

    private void OnDropZonePropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        Log($@"PropertyChanged: {e.PropertyName}");

    private void ApplyPresetDefault()
    {
        SuspendSettingsEvents();
        _txtDropZoneText.Text = @"Drag and drop files here or click";
        _txtHeaderText.Text = @"Upload File";
        _txtPreviewHeader.Text = @"Preview:";
        _cmbLayout.SelectedIndex = 1;
        _chkShowFileList.Checked = true;
        _chkShowBrowse.Checked = false;
        _chkShowStatus.Checked = false;
        _chkShowClear.Checked = false;
        _chkShowUploadIcon.Checked = true;
        _chkShowStripedDragFeedback.Checked = true;
        _chkShowActionButtons.Checked = true;
        _chkUsePaletteColors.Checked = true;
        _chkAllowDirectories.Checked = true;
        _chkSearchSubdirectories.Checked = false;
        _chkEnableUndo.Checked = true;
        _chkCustomValidation.Checked = false;
        _numMaxFileCount.Value = 0;
        _numMaxFileSizeKb.Value = 0;
        _numUploadQuotaKb.Value = 0;
        _chkShowUploadQuota.Checked = false;
        _txtAllowedExtensions.Text = @".png, .jpg, .jpeg, .gif, .bmp";
        ResumeSettingsEvents();

        ApplyCardDemoDefaults(kdzDropZone);
        kdzDropZone.DropZoneText = _txtDropZoneText.Text;
        kdzDropZone.ShowFileListView = true;
        kdzDropZone.AllowDirectories = true;
        kdzDropZone.SearchSubdirectories = false;
        kdzDropZone.EnableUndo = true;
        kdzDropZone.MaxFileCount = 0;
        kdzDropZone.MaxFileSize = 0;
        kdzDropZone.UploadSizeQuota = 0;
        kdzDropZone.ShowUploadQuotaProgressBar = false;
        ApplyAllowedExtensionsFromText(silent: true);
        SetCardAppearanceControlsEnabled(card: true);
        RefreshListChrome();
        RefreshSummary();
        Log(@"Preset applied: Default (Card).");
    }

    private void ApplyPresetClassic()
    {
        SuspendSettingsEvents();
        _txtDropZoneText.Text = @"Drag files here, or browse";
        _txtHeaderText.Text = string.Empty;
        _txtPreviewHeader.Text = @"Preview:";
        _cmbLayout.SelectedIndex = 0;
        _chkShowFileList.Checked = true;
        _chkShowBrowse.Checked = true;
        _chkShowStatus.Checked = true;
        _chkShowClear.Checked = true;
        _chkAllowDirectories.Checked = true;
        _chkSearchSubdirectories.Checked = false;
        _chkEnableUndo.Checked = true;
        _chkCustomValidation.Checked = false;
        _numMaxFileCount.Value = 0;
        _numMaxFileSizeKb.Value = 0;
        _numUploadQuotaKb.Value = 0;
        _chkShowUploadQuota.Checked = false;
        _txtAllowedExtensions.Text = @".txt, .png, .jpg, .jpeg, .gif, .bmp, .xls, .xlsx, .csv, .pdf, .doc, .docx";
        ResumeSettingsEvents();

        ApplyClassicDemoDefaults(kdzDropZone);
        kdzDropZone.DropZoneText = _txtDropZoneText.Text;
        kdzDropZone.ShowFileListView = true;
        kdzDropZone.AllowDirectories = true;
        kdzDropZone.SearchSubdirectories = false;
        kdzDropZone.EnableUndo = true;
        kdzDropZone.MaxFileCount = 0;
        kdzDropZone.MaxFileSize = 0;
        kdzDropZone.UploadSizeQuota = 0;
        kdzDropZone.ShowUploadQuotaProgressBar = false;
        ApplyAllowedExtensionsFromText(silent: true);
        SetCardAppearanceControlsEnabled(card: false);
        RefreshListChrome();
        RefreshSummary();
        Log(@"Preset applied: Classic layout.");
    }

    private void ApplyPresetImagesOnly()
    {
        SuspendSettingsEvents();
        _txtAllowedExtensions.Text = @".png, .jpg, .jpeg, .gif, .bmp";
        _chkAllowDirectories.Checked = false;
        _chkSearchSubdirectories.Checked = false;
        _numMaxFileCount.Value = 0;
        _numMaxFileSizeKb.Value = 0;
        ResumeSettingsEvents();

        kdzDropZone.AllowDirectories = false;
        kdzDropZone.SearchSubdirectories = false;
        kdzDropZone.MaxFileCount = 0;
        kdzDropZone.MaxFileSize = 0;
        ApplyAllowedExtensionsFromText(silent: true);
        RefreshSummary();
        Log(@"Preset applied: Images only (.png, .jpg, .jpeg, .gif, .bmp).");
    }

    private void ApplyPresetStrictLimits()
    {
        SuspendSettingsEvents();
        _numMaxFileCount.Value = 5;
        _numMaxFileSizeKb.Value = 1024;
        _numUploadQuotaKb.Value = 5 * 1024;
        _chkShowUploadQuota.Checked = true;
        ResumeSettingsEvents();

        kdzDropZone.MaxFileCount = 5;
        kdzDropZone.MaxFileSize = 1024L * 1024L;
        kdzDropZone.UploadSizeQuota = 5L * 1024L * 1024L;
        kdzDropZone.ShowUploadQuotaProgressBar = true;
        kdzDropZone.AllowDirectories = false;
        kdzDropZone.SearchSubdirectories = false;
        RefreshSummary();
        Log(@"Preset applied: Strict limits (max 5 files, 1 MB each, 5 MB total quota with progress bar, no folders).");
    }

    private void ApplyPresetRecursiveFolders()
    {
        SuspendSettingsEvents();
        _chkAllowDirectories.Checked = true;
        _chkSearchSubdirectories.Checked = true;
        _txtDropZoneText.Text = @"Drop a folder to collect all files (including subfolders)";
        ResumeSettingsEvents();

        kdzDropZone.AllowDirectories = true;
        kdzDropZone.SearchSubdirectories = true;
        kdzDropZone.DropZoneText = _txtDropZoneText.Text;
        RefreshSummary();
        Log(@"Preset applied: Recursive folders (AllowDirectories + SearchSubdirectories).");
    }

    private void ApplyAllowedExtensionsFromText(bool silent = false)
    {
        var extensions = _txtAllowedExtensions.Text
            .Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => s.Length > 0)
            .Select(s => s.StartsWith(@".", StringComparison.Ordinal) ? s : @"." + s)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        kdzDropZone.AllowedExtensions.Clear();
        kdzDropZone.AllowedExtensions.AddRange(extensions);

        if (!silent)
        {
            Log(@"AllowedExtensions = [" + string.Join(@", ", extensions) + @"]");
        }
    }

    private void SaveListToFile()
    {
        using var dialog = new SaveFileDialog
        {
            Title = @"Save dropped file list",
            Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*",
            FileName = @"KryptonDropZone-files.txt"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        kdzDropZone.SaveToFile(dialog.FileName);
        Log($@"SaveToFile('{dialog.FileName}') — {kdzDropZone.FileCount} path(s).");
    }

    private void LoadListFromFile()
    {
        using var dialog = new OpenFileDialog
        {
            Title = @"Load file list",
            Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        kdzDropZone.LoadFromFile(dialog.FileName);
        RefreshSummary();
        Log($@"LoadFromFile('{dialog.FileName}') — {kdzDropZone.FileCount} path(s) now in the list.");
    }

    private void ListByExtension()
    {
        string ext = _cmbExtensionFilter.SelectedItem?.ToString() ?? @".txt";
        var matches = kdzDropZone.GetFilesByExtension(ext).ToList();
        Log($@"GetFilesByExtension('{ext}'): {matches.Count} match(es).");
        if (matches.Count > 0)
        {
            Log(@"  " + string.Join(Environment.NewLine + @"  ", matches.Take(8)) + (matches.Count > 8 ? Environment.NewLine + @"  …" : string.Empty));
        }
    }

    private void RefreshListChrome() => kdzDropZone.AddFiles(Array.Empty<string>());

    private void RefreshSummary()
    {
        string quota = kdzDropZone.UploadSizeQuota > 0
            ? $@"  |  Quota remaining: {FormatBytes(kdzDropZone.RemainingUploadSize)} / {FormatBytes(kdzDropZone.UploadSizeQuota)}"
            : string.Empty;
        klblSummary.Text = $@"Files: {kdzDropZone.FileCount}  |  CanUndo: {kdzDropZone.CanUndo}  |  Undo: {(kdzDropZone.EnableUndo ? @"enabled" : @"disabled")}{quota}";
    }

    private static string FormatBytes(long bytes)
    {
        string[] units = { @"B", @"KB", @"MB", @"GB", @"TB" };
        double size = bytes;
        int unitIndex = 0;
        while (size >= 1024 && unitIndex < units.Length - 1)
        {
            size /= 1024;
            unitIndex++;
        }

        return $@"{size:0.#} {units[unitIndex]}";
    }

    private void Log(string message)
    {
        string line = $@"[{DateTime.Now:HH:mm:ss}] {message}";
        if (ktbxEventLog.TextLength > 0)
        {
            ktbxEventLog.AppendText(Environment.NewLine);
        }

        ktbxEventLog.AppendText(line);
    }

    private bool _suspendSettingsEvents;

    private void SuspendSettingsEvents() => _suspendSettingsEvents = true;

    private void ResumeSettingsEvents() => _suspendSettingsEvents = false;

    private static KryptonTextBox CreateTextBox(string text) =>
        new() { Dock = DockStyle.Top, Text = text };

    private static KryptonNumericUpDown CreateNumeric(decimal value, decimal min, decimal max, int decimalPlaces) =>
        new()
        {
            Dock = DockStyle.Top,
            Minimum = min,
            Maximum = max,
            DecimalPlaces = decimalPlaces,
            Value = value,
            Width = 120
        };

    private KryptonCheckBox CreateCheckBox(string text, bool isChecked, Action<KryptonCheckBox> onChanged)
    {
        var box = new KryptonCheckBox
        {
            Text = text,
            Checked = isChecked,
            AutoSize = true,
            Margin = new Padding(0, 4, 0, 0)
        };
        box.CheckedChanged += (s, e) =>
        {
            if (_suspendSettingsEvents || s is not KryptonCheckBox checkBox)
            {
                return;
            }

            onChanged(checkBox);
        };
        return box;
    }

    private static KryptonLabel FieldLabel(string text) =>
        new()
        {
            Text = text,
            AutoSize = true,
            Dock = DockStyle.Top,
            Margin = new Padding(0, 6, 0, 2)
        };

    private static KryptonButton CreatePresetButton(string text, Action apply) =>
        CreateActionButton(text, apply);

    private static KryptonButton CreateActionButton(string text, Action action)
    {
        var button = new KryptonButton
        {
            Text = text,
            AutoSize = true,
            Margin = new Padding(0, 4, 6, 4)
        };
        button.Click += (_, _) => action();
        return button;
    }

    private static KryptonHeaderGroup WrapGroup(string heading, params Control[] children)
    {
        var group = new KryptonHeaderGroup
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Margin = new Padding(0, 0, 0, 8)
        };
        group.ValuesPrimary.Heading = heading;
        group.ValuesPrimary.Image = null;

        var panel = (KryptonPanel)group.Panel;
        panel.Padding = new Padding(8);
        var inner = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 1
        };
        inner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        foreach (Control child in children)
        {
            if (child is KryptonLabel label && label.Text.Contains(':'))
            {
                label.Dock = DockStyle.Top;
                label.AutoSize = true;
                label.Margin = new Padding(0, inner.Controls.Count > 0 ? 6 : 0, 0, 2);
            }
            else
            {
                child.Dock = DockStyle.Top;
                child.Margin = new Padding(0, child is FlowLayoutPanel ? 4 : 0, 0, 0);
            }

            inner.Controls.Add(child, 0, inner.RowCount++);
        }

        panel.Controls.Add(inner);
        return group;
    }
}
