#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

using System.IO;

namespace TestForm;

public partial class FileSystemWatcherTest : KryptonForm
{
    private KryptonFileSystemWatcher? _watcher;

    public FileSystemWatcherTest()
    {
        InitializeComponent();
        InitializeWatcher();
        UpdateUIState();
    }

    private void InitializeWatcher()
    {
        _watcher = new KryptonFileSystemWatcher(this.components)
        {
            SynchronizingObject = this,  // Marshal events to UI thread
            Filter = "*.*",
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite,
            IncludeSubdirectories = false,
            InternalBufferSize = 8192
        };

        // Wire up all events
        _watcher.Created += OnFileCreated;
        _watcher.Changed += OnFileChanged;
        _watcher.Deleted += OnFileDeleted;
        _watcher.Renamed += OnFileRenamed;
        _watcher.Error += OnWatcherError;

        // Set default path to temp directory
        ktxtWatchPath.Text = Path.GetTempPath();

        // Populate combo boxes
        InitializeComboBoxes();

        UpdateEventCounts();
    }

    private void InitializeComboBoxes()
    {
        // Populate NotifyFilter combo
        kcmbNotifyFilter.Items.Clear();
        kcmbNotifyFilter.Items.Add("FileName");
        kcmbNotifyFilter.Items.Add("DirectoryName");
        kcmbNotifyFilter.Items.Add("Attributes");
        kcmbNotifyFilter.Items.Add("Size");
        kcmbNotifyFilter.Items.Add("LastWrite");
        kcmbNotifyFilter.Items.Add("LastAccess");
        kcmbNotifyFilter.Items.Add("CreationTime");
        kcmbNotifyFilter.Items.Add("Security");
        kcmbNotifyFilter.Items.Add("All");
        kcmbNotifyFilter.Items.Add("FileName | LastWrite");
        kcmbNotifyFilter.SelectedIndex = 9; // "FileName | LastWrite"

        // Populate PaletteMode combo
        kcmbPaletteMode.Items.Clear();
        kcmbPaletteMode.Items.Add("Global");
        kcmbPaletteMode.Items.Add("Professional System");
        kcmbPaletteMode.Items.Add("Office 2003");
        kcmbPaletteMode.Items.Add("Office 2007");
        kcmbPaletteMode.Items.Add("Office 2010");
        kcmbPaletteMode.Items.Add("Office 2013");
        kcmbPaletteMode.Items.Add("Sparkle Blue");
        kcmbPaletteMode.Items.Add("Sparkle Orange");
        kcmbPaletteMode.Items.Add("Sparkle Purple");
        kcmbPaletteMode.SelectedIndex = 0; // "Global"
    }

    private void OnFileCreated(object? sender, FileSystemEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Created: {e.Name}";
        AddEventToList(message);
        UpdateEventCounts();
    }

    private void OnFileChanged(object? sender, FileSystemEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Changed: {e.Name}";
        AddEventToList(message);
        UpdateEventCounts();
    }

    private void OnFileDeleted(object? sender, FileSystemEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Deleted: {e.Name}";
        AddEventToList(message);
        UpdateEventCounts();
    }

    private void OnFileRenamed(object? sender, RenamedEventArgs e)
    {
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] Renamed: {e.OldName} -> {e.Name}";
        AddEventToList(message);
        UpdateEventCounts();
    }

    private void OnWatcherError(object? sender, ErrorEventArgs e)
    {
        var ex = e.GetException();
        var message = $"[{DateTime.Now:HH:mm:ss.fff}] ERROR: {ex.Message}";
        AddEventToList(message, isError: true);

        // Show error in status
        klblStatus.Text = $"Error: {ex.Message}";
        klblStatus.StateCommon.ShortText.Color1 = Color.Red;

        // Auto-increase buffer size on error
        if (_watcher != null)
        {
            _watcher.InternalBufferSize = Math.Min(_watcher.InternalBufferSize * 2, 65536);
            klblBufferSizeStatus.Text = $"Buffer Size: {_watcher.InternalBufferSize} bytes";
        }
    }

    private void AddEventToList(string message, bool isError = false)
    {
        if (klstEvents.InvokeRequired)
        {
            klstEvents.Invoke(new Action(() => AddEventToList(message, isError)));
            return;
        }

        klstEvents.Items.Add(message);

        // Auto-scroll to bottom
        if (klstEvents.Items.Count > 0)
        {
            klstEvents.SelectedIndex = klstEvents.Items.Count - 1;
        }

        // Limit to 1000 items to prevent memory issues
        if (klstEvents.Items.Count > 1000)
        {
            klstEvents.Items.RemoveAt(0);
        }
    }

    private void UpdateEventCounts()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateEventCounts));
            return;
        }

        var created = klstEvents.Items.Cast<string>().Count(s => s.Contains("Created:"));
        var changed = klstEvents.Items.Cast<string>().Count(s => s.Contains("Changed:"));
        var deleted = klstEvents.Items.Cast<string>().Count(s => s.Contains("Deleted:"));
        var renamed = klstEvents.Items.Cast<string>().Count(s => s.Contains("Renamed:"));

        klblCreatedCount.Text = $"Created: {created}";
        klblChangedCount.Text = $"Changed: {changed}";
        klblDeletedCount.Text = $"Deleted: {deleted}";
        klblRenamedCount.Text = $"Renamed: {renamed}";
    }

    private void UpdateUIState()
    {
        var isWatching = _watcher?.EnableRaisingEvents ?? false;

        kbtnStart.Enabled = !isWatching && !string.IsNullOrWhiteSpace(ktxtWatchPath.Text);
        kbtnStop.Enabled = isWatching;
        kbtnClearEvents.Enabled = klstEvents.Items.Count > 0;

        klblStatus.Text = isWatching ? "Watching..." : "Stopped";
        klblStatus.StateCommon.ShortText.Color1 = isWatching ? Color.Green : Color.Gray;

        if (_watcher != null)
        {
            klblPath.Text = $"Path: {_watcher.Path}";
            klblFilterStatus.Text = $"Filter: {_watcher.Filter}";
            klblNotifyFilterStatus.Text = $"Notify Filter: {_watcher.NotifyFilter}";
            klblIncludeSubdirs.Text = $"Include Subdirectories: {_watcher.IncludeSubdirectories}";
            klblBufferSizeStatus.Text = $"Buffer Size: {_watcher.InternalBufferSize} bytes";
        }
    }

    private void kbtnBrowsePath_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select folder to watch",
            SelectedPath = ktxtWatchPath.Text
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            ktxtWatchPath.Text = dialog.SelectedPath;
            UpdateUIState();
        }
    }

    private void kbtnStart_Click(object sender, EventArgs e)
    {
        if (_watcher == null || string.IsNullOrWhiteSpace(ktxtWatchPath.Text))
        {
            return;
        }

        try
        {
            if (!Directory.Exists(ktxtWatchPath.Text))
            {
                KryptonMessageBox.Show(this,
                    $"Directory does not exist:\n{ktxtWatchPath.Text}",
                    "Invalid Path",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Warning);
                return;
            }

            _watcher.Path = ktxtWatchPath.Text;
            _watcher.Filter = ktxtFilter.Text;
            _watcher.IncludeSubdirectories = kchkIncludeSubdirs.Checked;
            _watcher.EnableRaisingEvents = true;

            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Started watching: {_watcher.Path}");
            UpdateUIState();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this,
                $"Error starting watcher:\n{ex.Message}",
                "Error",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private void kbtnStop_Click(object sender, EventArgs e)
    {
        if (_watcher != null)
        {
            _watcher.EnableRaisingEvents = false;
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Stopped watching");
            UpdateUIState();
        }
    }

    private void kbtnClearEvents_Click(object sender, EventArgs e)
    {
        klstEvents.Items.Clear();
        UpdateEventCounts();
        UpdateUIState();
    }

    private void ktxtWatchPath_TextChanged(object sender, EventArgs e)
    {
        UpdateUIState();
    }

    private void ktxtFilter_TextChanged(object sender, EventArgs e)
    {
        if (_watcher != null && !_watcher.EnableRaisingEvents)
        {
            _watcher.Filter = ktxtFilter.Text;
            UpdateUIState();
        }
    }

    private void kchkIncludeSubdirs_CheckedChanged(object sender, EventArgs e)
    {
        if (_watcher != null && !_watcher.EnableRaisingEvents)
        {
            _watcher.IncludeSubdirectories = kchkIncludeSubdirs.Checked;
            UpdateUIState();
        }
    }

    private void kcmbNotifyFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_watcher == null || _watcher.EnableRaisingEvents)
        {
            return;
        }

        var selected = kcmbNotifyFilter.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        _watcher.NotifyFilter = selected switch
        {
            "FileName" => NotifyFilters.FileName,
            "DirectoryName" => NotifyFilters.DirectoryName,
            "Attributes" => NotifyFilters.Attributes,
            "Size" => NotifyFilters.Size,
            "LastWrite" => NotifyFilters.LastWrite,
            "LastAccess" => NotifyFilters.LastAccess,
            "CreationTime" => NotifyFilters.CreationTime,
            "Security" => NotifyFilters.Security,
            "All" => NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime | NotifyFilters.Security,
            "FileName | LastWrite" => NotifyFilters.FileName | NotifyFilters.LastWrite,
            _ => NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite
        };

        UpdateUIState();
    }

    private void knudBufferSize_ValueChanged(object sender, EventArgs e)
    {
        if (_watcher != null && !_watcher.EnableRaisingEvents)
        {
            var bufferSize = (int)knudBufferSize.Value;
            // Round to nearest multiple of 4096
            bufferSize = (bufferSize / 4096) * 4096;
            if (bufferSize < 4096)
            {
                bufferSize = 4096;
            }

            _watcher.InternalBufferSize = bufferSize;
            knudBufferSize.Value = bufferSize;
            UpdateUIState();
        }
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_watcher == null)
        {
            return;
        }

        var selected = kcmbPaletteMode.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        _watcher.PaletteMode = selected switch
        {
            "Global" => PaletteMode.Global,
            "Professional System" => PaletteMode.ProfessionalSystem,
            "Office 2003" => PaletteMode.ProfessionalOffice2003,
            "Office 2007" => PaletteMode.Office2007Blue,
            "Office 2010" => PaletteMode.Office2010Blue,
            "Office 2013" => PaletteMode.Office2013White,
            "Sparkle Blue" => PaletteMode.SparkleBlue,
            "Sparkle Orange" => PaletteMode.SparkleOrange,
            "Sparkle Purple" => PaletteMode.SparklePurple,
            _ => PaletteMode.Global
        };
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _watcher?.Dispose();
        base.OnFormClosing(e);
    }
}
