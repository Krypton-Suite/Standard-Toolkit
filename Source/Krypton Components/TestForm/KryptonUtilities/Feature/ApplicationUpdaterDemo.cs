#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TestForm;

/// <summary>
/// Exercises <see cref="KryptonApplicationUpdater"/> against a local manifest.
/// The demo does not exit, and archive packages extract into a temp folder.
/// </summary>
public partial class ApplicationUpdaterDemo : KryptonForm
{
    private readonly string _root;
    private readonly string _installDirectory;
    private readonly string _manifestPath;
    private readonly string _statePath;
    private readonly string _installerPath;
    private readonly string _zipPath;
    private readonly KryptonApplicationUpdater _updater;
    private string _installerHash = string.Empty;
    private string _zipHash = string.Empty;
    private bool _ready;

    public ApplicationUpdaterDemo()
    {
        InitializeComponent();
        _root = Path.Combine(Path.GetTempPath(), "KryptonApplicationUpdaterDemo");
        _installDirectory = Path.Combine(_root, "install");
        _manifestPath = Path.Combine(_root, "manifest.json");
        _statePath = Path.Combine(_root, "demo.state");
        _installerPath = Path.Combine(_root, "packages", "setup.cmd");
        _zipPath = Path.Combine(_root, "packages", "update.zip");

        _updater = new KryptonApplicationUpdater
        {
            CheckOnStartup = false,
            CurrentVersion = "1.0.0",
            FeedKind = KryptonUpdateFeedKind.JsonManifest,
            FeedUrl = _manifestPath,
            Mode = KryptonUpdateMode.Notify,
            ExitApplicationOnApply = false,
            InstallDirectory = _installDirectory,
            StateFilePath = _statePath,
            AllowPostpone = true
        };
        _updater.Failed += OnUpdaterFailed;
        components!.Add(_updater);

        btnCheckNow.Click += OnCheckNow;
        btnScheduled.Click += OnScheduled;
        btnReset.Click += OnReset;
        btnReleaseNotes.Click += OnReleaseNotes;
        btnGitHub.Click += OnGitHub;
        cmbMode.SelectedIndexChanged += OnSelectionChanged;
        cmbSeverity.SelectedIndexChanged += OnSelectionChanged;
        cmbKind.SelectedIndexChanged += OnSelectionChanged;
        nudMaxDays.ValueChanged += OnSelectionChanged;
        chkAllowCritical.CheckedChanged += OnSelectionChanged;
    }

    private void ApplicationUpdaterDemo_Load(object? sender, EventArgs e)
    {
        cmbMode.Items.AddRange(new object[] { KryptonUpdateMode.Manual, KryptonUpdateMode.Notify, KryptonUpdateMode.Automatic });
        cmbSeverity.Items.AddRange(new object[]
        {
            KryptonUpdateSeverity.Optional,
            KryptonUpdateSeverity.Recommended,
            KryptonUpdateSeverity.Important,
            KryptonUpdateSeverity.Critical
        });
        cmbKind.Items.AddRange(new object[] { KryptonUpdatePackageKind.Installer, KryptonUpdatePackageKind.Archive });
        cmbMode.SelectedItem = KryptonUpdateMode.Notify;
        cmbSeverity.SelectedItem = KryptonUpdateSeverity.Important;
        cmbKind.SelectedItem = KryptonUpdatePackageKind.Installer;
        txtOwner.Text = "Krypton-Suite";
        txtRepository.Text = "Standard-Toolkit";
        PreparePackages();
        _ready = true;
        WriteManifest();
        Log("Current version 1.0.0. Manifest versions 1.5.0 and 9.9.9. Install folder: " + _installDirectory);
    }

    private async void OnCheckNow(object? sender, EventArgs e) => await RunLocalCheckAsync(scheduled: false).ConfigureAwait(true);

    private async void OnScheduled(object? sender, EventArgs e) => await RunLocalCheckAsync(scheduled: true).ConfigureAwait(true);

    private void OnReset(object? sender, EventArgs e)
    {
        if (File.Exists(_statePath))
        {
            File.Delete(_statePath);
        }

        Log("Postpone, skip, and last-check state cleared.");
    }

    private async void OnReleaseNotes(object? sender, EventArgs e)
    {
        SetBusy(true);
        try
        {
            await _updater.ShowReleaseNotesAsync().ConfigureAwait(true);
            Log("Release notes closed.");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void OnGitHub(object? sender, EventArgs e)
    {
        var owner = txtOwner.Text.Trim();
        var repository = txtRepository.Text.Trim();
        if (owner.Length == 0 || repository.Length == 0)
        {
            Log("Enter a GitHub owner and repository.");
            return;
        }

        SetBusy(true);
        try
        {
            _updater.FeedKind = KryptonUpdateFeedKind.GitHubReleases;
            _updater.GitHubOwner = owner;
            _updater.GitHubRepository = repository;
            var result = await _updater.CheckNowAsync().ConfigureAwait(true);
            Log("GitHub " + Describe(result));
        }
        finally
        {
            _updater.FeedKind = KryptonUpdateFeedKind.JsonManifest;
            _updater.FeedUrl = _manifestPath;
            SetBusy(false);
        }
    }

    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        if (!_ready)
        {
            return;
        }

        if (cmbMode.SelectedItem is KryptonUpdateMode mode)
        {
            _updater.Mode = mode;
        }

        WriteManifest();
    }

    private void OnUpdaterFailed(object? sender, KryptonUpdateFailedEventArgs e) => Log("Failed: " + e.Message);

    private async Task RunLocalCheckAsync(bool scheduled)
    {
        WriteManifest();
        SetBusy(true);
        try
        {
            var result = scheduled
                ? await _updater.CheckAsync().ConfigureAwait(true)
                : await _updater.CheckNowAsync().ConfigureAwait(true);
            Log((scheduled ? "Scheduled" : "Check now") + " " + Describe(result));
            if (result.Status == KryptonUpdateCheckStatus.Applied)
            {
                Log(DescribeInstall());
            }
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void PreparePackages()
    {
        Directory.CreateDirectory(Path.Combine(_root, "packages"));
        var payload = Path.Combine(_root, "payload");
        Directory.CreateDirectory(payload);
        Directory.CreateDirectory(_installDirectory);
        File.WriteAllText(Path.Combine(payload, "readme.txt"), "Krypton application updater demo payload.");
        if (File.Exists(_zipPath))
        {
            File.Delete(_zipPath);
        }

        ZipFile.CreateFromDirectory(payload, _zipPath);
        File.WriteAllText(_installerPath, "@echo off\r\necho installed> \"%~dp0installed.txt\"\r\n");
        _installerHash = ComputeSha256(_installerPath);
        _zipHash = ComputeSha256(_zipPath);
    }

    private void WriteManifest()
    {
        var severity = cmbSeverity.SelectedItem is KryptonUpdateSeverity selected
            ? selected
            : KryptonUpdateSeverity.Important;
        var archive = cmbKind.SelectedItem is KryptonUpdatePackageKind.Archive;
        var packagePath = archive ? _zipPath : _installerPath;
        var hash = archive ? _zipHash : _installerHash;
        var kind = archive ? "archive" : "installer";
        var released = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
        var days = (int)nudMaxDays.Value;
        var policy = ",\"maximumPostponeDays\":" + days.ToString(CultureInfo.InvariantCulture);
        if (severity == KryptonUpdateSeverity.Critical && chkAllowCritical.Checked)
        {
            policy += ",\"allowPostpone\":true";
        }

        var json = "{\"packages\":["
            + PackageJson("1.5.0", "recommended", kind, packagePath, hash, "Earlier local package, listed so the prompt shows more than one version.", "2026-06-01T00:00:00Z", string.Empty)
            + ","
            + PackageJson("9.9.9", severity.ToString().ToLowerInvariant(), kind, packagePath, hash, "Local demo package. Severity is " + severity + ".", released, policy)
            + "]}";
        File.WriteAllText(_manifestPath, json);
    }

    private static string PackageJson(string version, string severity, string kind, string packagePath, string hash, string notes, string releasedUtc, string policy) =>
        "{"
        + "\"version\":\"" + version + "\","
        + "\"severity\":\"" + severity + "\","
        + "\"packageKind\":\"" + kind + "\","
        + "\"packageUrl\":\"" + EscapeJson(packagePath) + "\","
        + "\"sha256\":\"" + hash + "\","
        + "\"releaseNotes\":\"" + EscapeJson(notes) + "\","
        + "\"releasedUtc\":\"" + releasedUtc + "\""
        + policy
        + "}";

    private string DescribeInstall()
    {
        var installed = Path.Combine(Path.GetDirectoryName(_installerPath) ?? _root, "installed.txt");
        var extracted = Path.Combine(_installDirectory, "readme.txt");
        if (File.Exists(extracted))
        {
            return "Extracted " + extracted;
        }

        if (File.Exists(installed))
        {
            return "Installer wrote " + installed;
        }

        return "Apply finished. Installer marker not written yet: " + installed;
    }

    private static string Describe(KryptonUpdateCheckResult result)
    {
        var version = result.Package == null ? string.Empty : " " + result.Package.Version;
        var error = result.Error == null ? string.Empty : " " + result.Error.Message;
        return result.Status + version + error;
    }

    private static string ComputeSha256(string path)
    {
        using (var stream = File.OpenRead(path))
        using (var algorithm = SHA256.Create())
        {
            var hash = algorithm.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }
    }

    private static string EscapeJson(string value) =>
        value.Replace("\\", "\\\\").Replace("\"", "\\\"");

    private void SetBusy(bool busy)
    {
        btnCheckNow.Enabled = !busy;
        btnScheduled.Enabled = !busy;
        btnReset.Enabled = !busy;
        btnGitHub.Enabled = !busy;
    }

    private void Log(string message)
    {
        if (txtLog.Text.Length > 0)
        {
            txtLog.AppendText(Environment.NewLine);
        }

        txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture) + "  " + message);
    }
}
