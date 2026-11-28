#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Checks a JSON manifest or GitHub release, then downloads and applies an installer or a zip.
/// Scheduled checks start only after the component is sited outside the designer.
/// A critical package hides postpone and skip unless its manifest allows them. Resume and Restart can be turned off in the same file. A zip is extracted by the embedded helper so this process is not the one overwriting its own files.
/// </summary>
[ToolboxItem(true)]
[DesignerCategory(@"Component")]
[DefaultProperty(nameof(FeedUrl))]
[Description(@"Checks for an application update and can download and apply an installer or a zip.")]
public class KryptonApplicationUpdater : Component
{
    private readonly object _checkGate = new object();
    private readonly System.Windows.Forms.Timer _timer;
    private KryptonApplicationUpdaterStrings _strings;
    private Task<KryptonUpdateCheckResult>? _activeCheck;
    private CancellationTokenSource? _downloadCancellation;
    private bool _startupQueued;

    private KryptonUpdateFeedKind _feedKind = KryptonUpdateFeedKind.JsonManifest;
    private string _feedUrl = string.Empty;
    private string _gitHubOwner = string.Empty;
    private string _gitHubRepository = string.Empty;
    private string _gitHubAssetName = string.Empty;
    private KryptonUpdateSeverity _gitHubDefaultSeverity = KryptonUpdateSeverity.Recommended;
    private string? _gitHubToken;
    private string _currentVersion = string.Empty;
    private KryptonUpdateMode _mode = KryptonUpdateMode.Notify;
    private bool _checkOnStartup = true;
    private TimeSpan _checkInterval = TimeSpan.FromDays(1);
    private KryptonUpdateSeverity _automaticApplyMaximumSeverity = KryptonUpdateSeverity.Recommended;
    private bool _allowPostpone = true;
    private TimeSpan _postponeInterval = TimeSpan.FromDays(1);
    private bool _exitApplicationOnApply = true;
    private string _installDirectory = string.Empty;
    private string _restartCommand = string.Empty;
    private string _stateFilePath = string.Empty;
    private KryptonUpdateProgressDisplay _progressDisplay = KryptonUpdateProgressDisplay.ProgressBar;

    /// <summary>Initializes a component that does not contact a feed until it is sited or <see cref="CheckNow"/> is called.</summary>
    public KryptonApplicationUpdater()
        : this(null)
    {
    }

    /// <summary>Initializes a component and adds it to <paramref name="container"/>.</summary>
    /// <param name="container">Designer container. May be null.</param>
    public KryptonApplicationUpdater(IContainer? container)
    {
        _strings = new KryptonApplicationUpdaterStrings();
        _timer = new System.Windows.Forms.Timer
        {
            Interval = (int)TimeSpan.FromDays(1).TotalMilliseconds
        };
        _timer.Tick += OnTimerTick;
        // Site assignment starts the schedule. Create the timer first so that call is safe.
        container?.Add(this);
    }

    /// <summary>Raised before the feed is contacted. Cancel to skip the check.</summary>
    public event EventHandler<KryptonUpdateCheckingEventArgs>? Checking;

    /// <summary>Raised when a newer package is selected. Cancel to supply your own UI and call <see cref="DownloadAsync(KryptonUpdatePackage, CancellationToken)"/> yourself.</summary>
    public event EventHandler<KryptonUpdateAvailableEventArgs>? UpdateAvailable;

    /// <summary>Raised during a download. Cancel to stop the copy.</summary>
    public event EventHandler<KryptonUpdateDownloadProgressEventArgs>? DownloadProgress;

    /// <summary>Raised after verification and before the installer or helper starts. Cancel to apply the file yourself.</summary>
    public event EventHandler<KryptonUpdateApplyingEventArgs>? Applying;

    /// <summary>Raised when the check, download, or apply fails.</summary>
    public event EventHandler<KryptonUpdateFailedEventArgs>? Failed;

    /// <summary>Raised when the feed has nothing newer, or when an installer or helper has been started.</summary>
    public event EventHandler<KryptonUpdatePackageEventArgs>? Completed;

    /// <summary>Raised when a version is postponed.</summary>
    public event EventHandler<KryptonUpdatePackageEventArgs>? Postponed;

    /// <summary>Raised when a version is skipped or a scheduled check stays quiet for a skipped version.</summary>
    public event EventHandler<KryptonUpdatePackageEventArgs>? Skipped;

    /// <summary>Gets or sets the built-in feed used when <see cref="Provider"/> is null.</summary>
    [Category(@"Behavior")]
    [Description(@"JSON manifest or GitHub Releases. Ignored when Provider is set.")]
    [DefaultValue(KryptonUpdateFeedKind.JsonManifest)]
    public KryptonUpdateFeedKind FeedKind
    {
        get => _feedKind;
        set => _feedKind = value;
    }

    /// <summary>Gets or sets the HTTPS or local JSON manifest used when <see cref="FeedKind"/> is <see cref="KryptonUpdateFeedKind.JsonManifest"/>.</summary>
    [Category(@"Behavior")]
    [Description(@"HTTPS manifest or a local manifest file.")]
    [DefaultValue("")]
    public string FeedUrl
    {
        get => _feedUrl;
        set => _feedUrl = value ?? string.Empty;
    }

    /// <summary>Gets or sets the GitHub repository owner.</summary>
    [Category(@"Behavior")]
    [Description(@"GitHub repository owner.")]
    [DefaultValue("")]
    public string GitHubOwner
    {
        get => _gitHubOwner;
        set => _gitHubOwner = value ?? string.Empty;
    }

    /// <summary>Gets or sets the GitHub repository name.</summary>
    [Category(@"Behavior")]
    [Description(@"GitHub repository name.")]
    [DefaultValue("")]
    public string GitHubRepository
    {
        get => _gitHubRepository;
        set => _gitHubRepository = value ?? string.Empty;
    }

    /// <summary>Gets or sets an exact release asset name used when the release has no krypton-update.json.</summary>
    [Category(@"Behavior")]
    [Description(@"Exact asset name when the release has more than one package and no krypton-update.json.")]
    [DefaultValue("")]
    public string GitHubAssetName
    {
        get => _gitHubAssetName;
        set => _gitHubAssetName = value ?? string.Empty;
    }

    /// <summary>Gets or sets the severity used when a GitHub release is mapped without a manifest.</summary>
    [Category(@"Behavior")]
    [Description(@"Severity used when a GitHub release has no krypton-update.json.")]
    [DefaultValue(KryptonUpdateSeverity.Recommended)]
    public KryptonUpdateSeverity GitHubDefaultSeverity
    {
        get => _gitHubDefaultSeverity;
        set => _gitHubDefaultSeverity = value;
    }

    /// <summary>Gets or sets an optional GitHub token. The designer does not save this value.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? GitHubToken
    {
        get => _gitHubToken;
        set => _gitHubToken = value;
    }

    /// <summary>Gets or sets the installed version. Empty uses the entry assembly version.</summary>
    [Category(@"Behavior")]
    [Description(@"Installed version. Empty uses the entry assembly version.")]
    [DefaultValue("")]
    public string CurrentVersion
    {
        get => _currentVersion;
        set => _currentVersion = value ?? string.Empty;
    }

    /// <summary>Gets or sets whether checks are manual, prompted, or applied automatically up to <see cref="AutomaticApplyMaximumSeverity"/>.</summary>
    [Category(@"Behavior")]
    [Description(@"Manual checks only from CheckNow. Notify asks first. Automatic applies up to AutomaticApplyMaximumSeverity.")]
    [DefaultValue(KryptonUpdateMode.Notify)]
    public KryptonUpdateMode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            UpdateSchedule();
        }
    }

    /// <summary>Gets or sets whether a sited component checks once on startup, still subject to <see cref="CheckInterval"/>.</summary>
    [Category(@"Behavior")]
    [Description(@"Check when the component is sited, unless the last check is still inside CheckInterval.")]
    [DefaultValue(true)]
    public bool CheckOnStartup
    {
        get => _checkOnStartup;
        set => _checkOnStartup = value;
    }

    /// <summary>Gets or sets how long after a successful check a scheduled check waits.</summary>
    [Category(@"Behavior")]
    [Description(@"Minimum time between scheduled checks.")]
    [DefaultValue(typeof(TimeSpan), "1.00:00:00")]
    public TimeSpan CheckInterval
    {
        get => _checkInterval;
        set
        {
            _checkInterval = value < TimeSpan.Zero ? TimeSpan.Zero : value;
            UpdateSchedule();
        }
    }

    /// <summary>Gets or sets the highest severity <see cref="KryptonUpdateMode.Automatic"/> downloads without a prompt.</summary>
    [Category(@"Behavior")]
    [Description(@"Automatic mode prompts when the package severity is higher than this value.")]
    [DefaultValue(KryptonUpdateSeverity.Recommended)]
    public KryptonUpdateSeverity AutomaticApplyMaximumSeverity
    {
        get => _automaticApplyMaximumSeverity;
        set => _automaticApplyMaximumSeverity = value;
    }

    /// <summary>Gets or sets whether Remind later is offered for a non-critical update.</summary>
    [Category(@"Behavior")]
    [Description(@"Offer Remind later for updates that are not critical.")]
    [DefaultValue(true)]
    public bool AllowPostpone
    {
        get => _allowPostpone;
        set => _allowPostpone = value;
    }

    /// <summary>Gets or sets how long <see cref="Postpone(KryptonUpdatePackage)"/> waits, and the initial day count on the remind-later dialog.</summary>
    [Category(@"Behavior")]
    [Description(@"Initial remind-later day count, and the span used by Postpone(package).")]
    [DefaultValue(typeof(TimeSpan), "1.00:00:00")]
    public TimeSpan PostponeInterval
    {
        get => _postponeInterval;
        set => _postponeInterval = value < TimeSpan.Zero ? TimeSpan.Zero : value;
    }

    /// <summary>Gets or sets whether a successful apply exits the application. Turn this off when the zip destination is not this application's folder.</summary>
    [Category(@"Behavior")]
    [Description(@"Exit after the installer or zip helper starts. A zip helper waits for this process only when this is true.")]
    [DefaultValue(true)]
    public bool ExitApplicationOnApply
    {
        get => _exitApplicationOnApply;
        set => _exitApplicationOnApply = value;
    }

    /// <summary>Gets or sets the folder a zip is extracted into. Empty uses the application base directory.</summary>
    [Category(@"Behavior")]
    [Description(@"Zip extract folder. Empty uses the application base directory.")]
    [DefaultValue("")]
    public string InstallDirectory
    {
        get => _installDirectory;
        set => _installDirectory = value ?? string.Empty;
    }

    /// <summary>Gets or sets the program the helper starts after a zip extract. Empty uses this process. Used only when <see cref="ExitApplicationOnApply"/> is true.</summary>
    [Category(@"Behavior")]
    [Description(@"Program started after a zip extract. Empty uses this process.")]
    [DefaultValue("")]
    public string RestartCommand
    {
        get => _restartCommand;
        set => _restartCommand = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the progress control used while downloading.
    /// A package <c>progressDisplay</c> value overrides this when set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Progress bar or transfer-speed graph while downloading. A package progressDisplay value overrides this.")]
    [DefaultValue(KryptonUpdateProgressDisplay.ProgressBar)]
    public KryptonUpdateProgressDisplay ProgressDisplay
    {
        get => _progressDisplay;
        set => _progressDisplay = value;
    }

    /// <summary>Gets or sets prompt text.</summary>
    [Category(@"Appearance")]
    [Description(@"Prompt and progress text.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonApplicationUpdaterStrings Strings
    {
        get => _strings;
        set => _strings = value ?? new KryptonApplicationUpdaterStrings();
    }

    /// <summary>Gets or sets a custom feed. When set, <see cref="FeedKind"/> is not used. The designer does not save this value.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IKryptonUpdateProvider? Provider { get; set; }

    /// <summary>Gets or sets the postpone file. Empty stores it under LocalAppData\Krypton Toolkit\Updates using the entry assembly name.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StateFilePath
    {
        get => _stateFilePath;
        set => _stateFilePath = value ?? string.Empty;
    }

    /// <summary>Gets the site. Outside design mode, a site starts the schedule.</summary>
    public override ISite? Site
    {
        get => base.Site;
        set
        {
            base.Site = value;
            UpdateSchedule();
        }
    }

    /// <summary>Gets the version compared with the feed.</summary>
    /// <returns>The parsed <see cref="CurrentVersion"/>, or the entry assembly version when that property is empty.</returns>
    /// <exception cref="KryptonUpdateException"><see cref="CurrentVersion"/> is set and is not a <see cref="Version"/>.</exception>
    public Version GetCurrentVersion()
    {
        if (!string.IsNullOrWhiteSpace(CurrentVersion))
        {
            if (!KryptonUpdateVersion.TryParse(CurrentVersion, out var parsed))
            {
                KryptonUpdateThrowHelper.Throw("CurrentVersion is not a System.Version value.");
            }

            return parsed;
        }

        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        return assembly.GetName().Version ?? new Version(0, 0, 0, 0);
    }

    /// <summary>Starts a check that ignores the interval and any postpone. A skipped version stays quiet.</summary>
    public void CheckNow()
    {
        var check = CheckNowAsync();
        Observe(check);
    }

    /// <summary>Checks immediately, ignoring the interval and any postpone.</summary>
    /// <param name="cancellationToken">Cancels the feed request or download.</param>
    /// <returns>The outcome. Failures are also raised through <see cref="Failed"/> and are not thrown.</returns>
    public Task<KryptonUpdateCheckResult> CheckNowAsync(CancellationToken cancellationToken = default) =>
        StartCheck(false, cancellationToken);

    /// <summary>Runs a scheduled check. Manual mode does not contact the feed. The interval and postpone window are honoured.</summary>
    /// <param name="cancellationToken">Cancels the feed request or download.</param>
    /// <returns>The outcome. Failures are also raised through <see cref="Failed"/> and are not thrown.</returns>
    public Task<KryptonUpdateCheckResult> CheckAsync(CancellationToken cancellationToken = default) =>
        StartCheck(true, cancellationToken);

    /// <summary>
    /// Reads the feed and shows release notes in their own window. Does not record a check, so the schedule, postpone, and skip state stay as they are.
    /// When a newer package exists, that window offers update and remind later. Remind later asks for a date or skips this version. A package that does not allow postpone hides both.
    /// When nothing is newer, the window lists the feed and offers Close.
    /// </summary>
    /// <param name="cancellationToken">Cancels the feed request or a download started from this window.</param>
    /// <returns>A task that completes when the window closes, and after an accepted update has been applied.</returns>
    public async Task ShowReleaseNotesAsync(CancellationToken cancellationToken = default)
    {
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            return;
        }

        try
        {
            var catalog = await ResolveProvider().CheckAsync(cancellationToken).ConfigureAwait(true);
            var current = GetCurrentVersion();
            var package = catalog.SelectNewerThan(current);
            if (package == null)
            {
                ShowNotes(null, current, KryptonUpdateCatalog.OrderByVersion(catalog.Packages, null), null, false, out _);
                return;
            }

            var choice = ShowNotes(package, current, catalog.SelectAllNewerThan(current), null, true, out var postponeFor);
            if (choice == KryptonUpdatePromptChoice.UpdateNow)
            {
                await DownloadAndApplyAsync(package, cancellationToken).ConfigureAwait(true);
                return;
            }

            if (choice == KryptonUpdatePromptChoice.Postpone)
            {
                Postpone(package, postponeFor);
                return;
            }

            if (choice == KryptonUpdatePromptChoice.Skip)
            {
                Skip(package);
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception exception)
        {
            OnFailed(new KryptonUpdateFailedEventArgs(exception));
            var current = new Version(0, 0, 0, 0);
            try
            {
                current = GetCurrentVersion();
            }
            catch (Exception)
            {
            }

            ShowNotes(null, current, Array.Empty<KryptonUpdatePackage>(), exception.Message, false, out _);
        }
    }

    /// <summary>Downloads <paramref name="package"/> without applying it.</summary>
    /// <param name="package">Package from the feed.</param>
    /// <param name="cancellationToken">Cancels the copy.</param>
    /// <returns>The local file. The caller verifies and applies it, or passes it to <see cref="ApplyAsync"/>.</returns>
    public Task<string> DownloadAsync(KryptonUpdatePackage package, CancellationToken cancellationToken = default) =>
        DownloadAsync(package, null, cancellationToken);

    /// <summary>Downloads <paramref name="package"/> and reports byte counts.</summary>
    /// <param name="package">Package from the feed.</param>
    /// <param name="progress">Optional byte callback. May be null.</param>
    /// <param name="cancellationToken">Cancels the copy.</param>
    /// <returns>The local file.</returns>
    public async Task<string> DownloadAsync(KryptonUpdatePackage package, IProgress<KryptonUpdateDownloadProgress>? progress, CancellationToken cancellationToken = default)
    {
        ThrowHelper.ThrowIfNull(package);

        using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _downloadCancellation = linked;
        var relay = new Progress<KryptonUpdateDownloadProgress>(value =>
        {
            var args = new KryptonUpdateDownloadProgressEventArgs(package, value);
            Report(() =>
            {
                OnDownloadProgress(args);
                if (args.Cancel)
                {
                    CancelDownload();
                }
            });
            progress?.Report(value);
        });

        try
        {
            return await KryptonUpdateClient.DownloadPackageAsync(package.PackageUrl, relay, linked.Token).ConfigureAwait(false);
        }
        finally
        {
            if (ReferenceEquals(_downloadCancellation, linked))
            {
                _downloadCancellation = null;
            }
        }
    }

    /// <summary>Verifies <paramref name="downloadedPath"/> and starts the installer or the zip helper.</summary>
    /// <param name="package">Package that named the file.</param>
    /// <param name="downloadedPath">File returned by <see cref="DownloadAsync(KryptonUpdatePackage, CancellationToken)"/>.</param>
    /// <param name="cancellationToken">Cancels before the helper is allowed to finish when this process stays open.</param>
    /// <returns>A task that completes when the installer has been started, or when the helper has finished because <see cref="ExitApplicationOnApply"/> is false.</returns>
    public async Task ApplyAsync(KryptonUpdatePackage package, string downloadedPath, CancellationToken cancellationToken = default)
    {
        ThrowHelper.ThrowIfNull(package);
        ThrowHelper.ThrowIfNullOrWhiteSpace(downloadedPath);
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            KryptonUpdateClient.Verify(downloadedPath, package.Sha256, package.CertificateThumbprint);
        }
        catch
        {
            TryDelete(downloadedPath);
            throw;
        }

        var applying = new KryptonUpdateApplyingEventArgs(package, downloadedPath);
        OnApplying(applying);
        if (applying.Cancel || cancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (package.PackageKind == KryptonUpdatePackageKind.Installer)
        {
            KryptonUpdateClient.StartInstaller(downloadedPath, package.InstallerArguments);
            ExitIfRequested();
            return;
        }

        var waitForThisProcess = ExitApplicationOnApply;
        var process = KryptonUpdateClient.StartHelper(
            downloadedPath,
            ResolveInstallDirectory(),
            waitForThisProcess ? ResolveRestartCommand() : string.Empty,
            null,
            waitForThisProcess ? Process.GetCurrentProcess().Id : 0);
        if (!waitForThisProcess)
        {
            var finished = await Task.Run(() => process.WaitForExit(10 * 60 * 1000), cancellationToken).ConfigureAwait(true);
            if (!finished)
            {
                KryptonUpdateThrowHelper.Throw("The update helper did not finish.");
            }

            if (process.ExitCode != 0)
            {
                var directory = Path.GetDirectoryName(process.StartInfo.FileName) ?? string.Empty;
                KryptonUpdateThrowHelper.Throw(KryptonUpdateClient.ReadHelperResult(directory));
            }

            return;
        }

        ExitIfRequested();
    }

    /// <summary>Records a postpone for <paramref name="package"/> and raises <see cref="Postponed"/>.</summary>
    /// <param name="package">Version to hide from scheduled checks until <see cref="PostponeInterval"/> elapses.</param>
    public void Postpone(KryptonUpdatePackage package) => Postpone(package, PostponeInterval);

    /// <summary>Records a postpone for <paramref name="package"/> for <paramref name="duration"/> and raises <see cref="Postponed"/>.</summary>
    /// <param name="package">Version to hide from scheduled checks.</param>
    /// <param name="duration">How long scheduled checks stay quiet for this version. The remind-later dialog passes the time until the chosen date.</param>
    public void Postpone(KryptonUpdatePackage package, TimeSpan duration)
    {
        ThrowHelper.ThrowIfNull(package);
        if (!package.AllowsPostpone || !AllowPostpone)
        {
            return;
        }

        if (duration < TimeSpan.Zero)
        {
            duration = TimeSpan.Zero;
        }

        var maximum = TimeSpan.FromDays(package.MaximumPostponeDays);
        if (duration > maximum)
        {
            duration = maximum;
        }

        var path = ResolveStatePath();
        var state = KryptonUpdateStateStore.Load(path);
        state.Postpone(package.Version, DateTime.UtcNow.Add(duration));
        state.Save(path);
        OnPostponed(new KryptonUpdatePackageEventArgs(package, KryptonUpdateCheckStatus.Postponed));
    }

    /// <summary>Records a skip for <paramref name="package"/> and raises <see cref="Skipped"/>.</summary>
    /// <param name="package">Version that stays quiet until a newer one is published. A package that does not allow skip is left unchanged.</param>
    public void Skip(KryptonUpdatePackage package)
    {
        ThrowHelper.ThrowIfNull(package);
        if (!package.AllowsSkip)
        {
            return;
        }

        var path = ResolveStatePath();
        var state = KryptonUpdateStateStore.Load(path);
        state.Skip(package.Version);
        state.Save(path);
        OnSkipped(new KryptonUpdatePackageEventArgs(package, KryptonUpdateCheckStatus.Skipped));
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer.Dispose();
            CancelDownload();
        }

        base.Dispose(disposing);
    }

    /// <summary>Raises <see cref="Checking"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnChecking(KryptonUpdateCheckingEventArgs e) => Checking?.Invoke(this, e);

    /// <summary>Raises <see cref="UpdateAvailable"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnUpdateAvailable(KryptonUpdateAvailableEventArgs e) => UpdateAvailable?.Invoke(this, e);

    /// <summary>Raises <see cref="DownloadProgress"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnDownloadProgress(KryptonUpdateDownloadProgressEventArgs e) => DownloadProgress?.Invoke(this, e);

    /// <summary>Raises <see cref="Applying"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnApplying(KryptonUpdateApplyingEventArgs e) => Applying?.Invoke(this, e);

    /// <summary>Raises <see cref="Failed"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnFailed(KryptonUpdateFailedEventArgs e) => Failed?.Invoke(this, e);

    /// <summary>Raises <see cref="Completed"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnCompleted(KryptonUpdatePackageEventArgs e) => Completed?.Invoke(this, e);

    /// <summary>Raises <see cref="Postponed"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnPostponed(KryptonUpdatePackageEventArgs e) => Postponed?.Invoke(this, e);

    /// <summary>Raises <see cref="Skipped"/>.</summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnSkipped(KryptonUpdatePackageEventArgs e) => Skipped?.Invoke(this, e);

    private bool ShouldSerializeStrings() => !_strings.IsDefault;

    private void ResetStrings() => _strings.Reset();

    private void OnTimerTick(object? sender, EventArgs e) => Observe(CheckAsync());

    private Task<KryptonUpdateCheckResult> StartCheck(bool scheduled, CancellationToken cancellationToken)
    {
        lock (_checkGate)
        {
            if (_activeCheck != null && !_activeCheck.IsCompleted)
            {
                return _activeCheck;
            }

            _activeCheck = RunCheckAsync(scheduled, cancellationToken);
            return _activeCheck;
        }
    }

    private async Task<KryptonUpdateCheckResult> RunCheckAsync(bool scheduled, CancellationToken cancellationToken)
    {
        try
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Idle);
            }

            if (scheduled && Mode == KryptonUpdateMode.Manual)
            {
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Idle);
            }

            var checking = new KryptonUpdateCheckingEventArgs();
            OnChecking(checking);
            if (checking.Cancel || cancellationToken.IsCancellationRequested)
            {
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Cancelled);
            }

            var statePath = ResolveStatePath();
            var state = KryptonUpdateStateStore.Load(statePath);
            if (scheduled && state.LastCheckUtc.HasValue && DateTime.UtcNow - state.LastCheckUtc.Value < CheckInterval)
            {
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.IgnoredInterval);
            }

            var catalog = await ResolveProvider().CheckAsync(cancellationToken).ConfigureAwait(true);
            state.LastCheckUtc = DateTime.UtcNow;
            state.Save(statePath);

            var current = GetCurrentVersion();
            var package = catalog.SelectNewerThan(current);
            if (package == null)
            {
                var upToDate = new KryptonUpdatePackageEventArgs(null, KryptonUpdateCheckStatus.UpToDate);
                OnCompleted(upToDate);
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.UpToDate);
            }

            if (state.IsSkipped(package.Version))
            {
                OnSkipped(new KryptonUpdatePackageEventArgs(package, KryptonUpdateCheckStatus.Skipped));
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Skipped, package);
            }

            if (scheduled && state.IsPostponeActive(package.Version, DateTime.UtcNow))
            {
                OnPostponed(new KryptonUpdatePackageEventArgs(package, KryptonUpdateCheckStatus.Postponed));
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Postponed, package);
            }

            var available = new KryptonUpdateAvailableEventArgs(package, current);
            OnUpdateAvailable(available);
            if (available.Cancel)
            {
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Cancelled, package);
            }

            if (Mode == KryptonUpdateMode.Automatic && (int)package.Severity <= (int)AutomaticApplyMaximumSeverity)
            {
                await DownloadAndApplyAsync(package, cancellationToken).ConfigureAwait(true);
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Applied, package);
            }

            var choice = ShowPrompt(package, current, catalog.SelectAllNewerThan(current), out var postponeFor);
            if (choice == KryptonUpdatePromptChoice.UpdateNow)
            {
                await DownloadAndApplyAsync(package, cancellationToken).ConfigureAwait(true);
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Applied, package);
            }

            if (choice == KryptonUpdatePromptChoice.Postpone)
            {
                Postpone(package, postponeFor);
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Postponed, package);
            }

            if (choice == KryptonUpdatePromptChoice.Skip)
            {
                Skip(package);
                return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Skipped, package);
            }

            return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Cancelled, package);
        }
        catch (OperationCanceledException)
        {
            return KryptonUpdateCheckResult.Create(KryptonUpdateCheckStatus.Cancelled);
        }
        catch (Exception exception)
        {
            OnFailed(new KryptonUpdateFailedEventArgs(exception));
            return KryptonUpdateCheckResult.Failure(exception);
        }
    }

    private async Task DownloadAndApplyAsync(KryptonUpdatePackage package, CancellationToken cancellationToken)
    {
        var destination = KryptonUpdateClient.CreateDownloadPath(package.PackageUrl);
        var interrupted = Strings.DownloadInterrupted;
        var restarting = Strings.DownloadRestarting;
        var progressDisplay = package.ProgressDisplay ?? ProgressDisplay;
        VisualApplicationUpdateProgressForm? progressForm = null;
        Report(() =>
        {
            progressForm = new VisualApplicationUpdateProgressForm();
            progressForm.Bind(Strings, package.Pulse, progressDisplay);
            progressForm.SetFileName(PackageFileName(package.PackageUrl));
            progressForm.CancelClicked += delegate { CancelDownload(); };
            var owner = TryGetOwnerForm();
            if (owner != null)
            {
                progressForm.Show(owner);
            }
            else
            {
                progressForm.Show();
            }
        });

        var resume = false;
        try
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Exception? failure = null;
                var cancelledByUser = false;
                Report(() => progressForm?.BeginTransfer(Strings.Downloading));

                try
                {
                    var progress = new Progress<KryptonUpdateDownloadProgress>(value =>
                    {
                        progressForm?.SetProgress(value.BytesReceived, value.TotalBytes);
                        if (progressForm != null && progressForm.CancelRequested)
                        {
                            CancelDownload();
                        }
                    });
                    await DownloadAttemptAsync(
                        package,
                        destination,
                        resume,
                        progress,
                        () => Report(() => progressForm?.SetStatus(restarting)),
                        cancellationToken).ConfigureAwait(true);
                    Report(() =>
                    {
                        progressForm?.SetCancelEnabled(false);
                        progressForm?.PauseSpeedGraph();
                        progressForm?.SetStatus(Strings.Verifying);
                    });
                    await ApplyAsync(package, destination, cancellationToken).ConfigureAwait(true);
                    OnCompleted(new KryptonUpdatePackageEventArgs(package, KryptonUpdateCheckStatus.Applied));
                    return;
                }
                catch (OperationCanceledException)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }

                    if (progressForm != null && progressForm.CancelRequested)
                    {
                        cancelledByUser = true;
                    }
                    else
                    {
                        failure = new KryptonUpdateException(interrupted);
                    }
                }
                catch (Exception exception)
                {
                    failure = exception;
                }

                var canResume = package.AllowsResume && HasPartialDownload(destination) && !KryptonUpdateClient.TryGetLocalPath(package.PackageUrl, out _);
                var canRetry = package.AllowsRetry;
                var message = failure is KryptonUpdateException ? failure.Message : interrupted;
                Task<KryptonUpdateDownloadRecovery> choiceTask = Task.FromResult(KryptonUpdateDownloadRecovery.Close);
                Report(() =>
                {
                    if (progressForm == null || progressForm.IsDisposed)
                    {
                        choiceTask = Task.FromResult(KryptonUpdateDownloadRecovery.Close);
                        return;
                    }

                    choiceTask = progressForm.ShowRecovery(message, canResume, canRetry, Strings);
                });

                var choice = await choiceTask.ConfigureAwait(true);
                if (choice == KryptonUpdateDownloadRecovery.Resume && canResume)
                {
                    resume = true;
                    continue;
                }

                if (choice == KryptonUpdateDownloadRecovery.Restart && canRetry)
                {
                    resume = false;
                    continue;
                }

                TryDelete(destination);
                if (cancelledByUser && failure == null)
                {
                    throw new OperationCanceledException();
                }

                if (failure != null)
                {
                    throw failure;
                }

                throw new OperationCanceledException();
            }
        }
        finally
        {
            Report(() => progressForm?.Dismiss());
        }
    }

    private async Task DownloadAttemptAsync(KryptonUpdatePackage package, string destination, bool resume, IProgress<KryptonUpdateDownloadProgress>? progress, Action? startingAgain, CancellationToken cancellationToken)
    {
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _downloadCancellation = linked;
        var relay = new Progress<KryptonUpdateDownloadProgress>(value =>
        {
            var args = new KryptonUpdateDownloadProgressEventArgs(package, value);
            Report(() =>
            {
                OnDownloadProgress(args);
                if (args.Cancel)
                {
                    CancelDownload();
                }
            });
            progress?.Report(value);
        });

        try
        {
            await KryptonUpdateClient.DownloadToAsync(package.PackageUrl, destination, resume, relay, startingAgain, linked.Token).ConfigureAwait(false);
        }
        finally
        {
            if (ReferenceEquals(_downloadCancellation, linked))
            {
                _downloadCancellation = null;
            }
        }
    }

    private KryptonUpdatePromptChoice ShowPrompt(KryptonUpdatePackage offer, Version current, IReadOnlyList<KryptonUpdatePackage> notes, out TimeSpan postponeFor)
    {
        var choice = KryptonUpdatePromptChoice.Dismiss;
        var chosen = TimeSpan.Zero;
        var days = DefaultPostponeDays(offer.MaximumPostponeDays);
        var bannerImage = LoadBannerImage(offer.Banner);
        try
        {
            Report(() =>
            {
                using var form = new VisualApplicationUpdatePromptForm();
                var allowPostpone = AllowPostpone && offer.AllowsPostpone;
                form.Bind(offer, current, notes, Strings, allowPostpone, offer.AllowsSkip, days, bannerImage);
                var owner = TryGetOwnerForm();
                if (owner != null)
                {
                    form.ShowDialog(owner);
                }
                else
                {
                    form.ShowDialog();
                }

                choice = form.Choice;
                chosen = form.PostponeFor;
            });
        }
        finally
        {
            bannerImage?.Dispose();
        }

        postponeFor = chosen;
        return choice;
    }

    private KryptonUpdatePromptChoice ShowNotes(KryptonUpdatePackage? offer, Version current, IReadOnlyList<KryptonUpdatePackage> notes, string? failureMessage, bool allowApply, out TimeSpan postponeFor)
    {
        var choice = KryptonUpdatePromptChoice.Dismiss;
        var chosen = TimeSpan.Zero;
        var days = DefaultPostponeDays(offer != null ? offer.MaximumPostponeDays : KryptonUpdatePackage.DefaultMaximumPostponeDays);
        var allowPostpone = offer != null && AllowPostpone && offer.AllowsPostpone;
        var allowSkip = offer != null && offer.AllowsSkip;
        var bannerImage = LoadBannerImage(offer?.Banner);
        try
        {
            Report(() =>
            {
                using var form = new VisualApplicationUpdateNotesForm();
                form.Bind(offer, current, notes, Strings, allowApply, allowPostpone, allowSkip, days, failureMessage, bannerImage);
                var owner = TryGetOwnerForm();
                if (owner != null)
                {
                    form.ShowDialog(owner);
                }
                else
                {
                    form.ShowDialog();
                }

                choice = form.Choice;
                chosen = form.PostponeFor;
            });
        }
        finally
        {
            bannerImage?.Dispose();
        }

        postponeFor = chosen;
        return choice;
    }

    private static Image? LoadBannerImage(KryptonUpdateBanner? banner)
    {
        if (banner == null || banner.ImageUrl == null || string.IsNullOrWhiteSpace(banner.ImageUrl))
        {
            return null;
        }

        return KryptonUpdatePresentation.LoadBannerImageAsync(banner.ImageUrl).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    private int DefaultPostponeDays(int maximumDays)
    {
        var limit = maximumDays < 1 ? 1 : maximumDays;
        var days = (int)Math.Round(PostponeInterval.TotalDays, MidpointRounding.AwayFromZero);
        if (days < 1)
        {
            return 1;
        }

        return days > limit ? limit : days;
    }

    private static string PackageFileName(string location)
    {
        var leaf = string.Empty;
        if (Uri.TryCreate(location, UriKind.Absolute, out var uri))
        {
            leaf = Path.GetFileName(uri.LocalPath);
        }
        else if (location != null)
        {
            leaf = Path.GetFileName(location);
        }

        if (leaf == null || string.IsNullOrWhiteSpace(leaf))
        {
            return location ?? string.Empty;
        }

        return leaf;
    }

    private IKryptonUpdateProvider ResolveProvider()
    {
        if (Provider != null)
        {
            return Provider;
        }

        if (FeedKind == KryptonUpdateFeedKind.GitHubReleases)
        {
            return new KryptonGitHubReleaseUpdateProvider(GitHubOwner, GitHubRepository, GitHubAssetName, GitHubDefaultSeverity, GitHubToken);
        }

        return new KryptonJsonManifestUpdateProvider(FeedUrl);
    }

    private void UpdateSchedule()
    {
        if (Site == null || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || Mode == KryptonUpdateMode.Manual)
        {
            _timer.Stop();
            return;
        }

        _timer.Interval = ToTimerInterval(CheckInterval);
        if (!_timer.Enabled)
        {
            _timer.Start();
        }

        if (!CheckOnStartup || _startupQueued)
        {
            return;
        }

        _startupQueued = true;
        var owner = TryGetOwnerForm();
        if (owner != null && !owner.IsHandleCreated)
        {
            owner.HandleCreated += OnOwnerHandleCreated;
            return;
        }

        if (owner != null)
        {
            owner.BeginInvoke(new Action(() => Observe(CheckAsync())));
            return;
        }

        Observe(CheckAsync());
    }

    private void OnOwnerHandleCreated(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            form.HandleCreated -= OnOwnerHandleCreated;
            form.BeginInvoke(new Action(() => Observe(CheckAsync())));
        }
    }

    private string ResolveStatePath()
    {
        if (!string.IsNullOrWhiteSpace(StateFilePath))
        {
            return StateFilePath;
        }

        var name = Assembly.GetEntryAssembly()?.GetName().Name ?? "application";
        foreach (var invalid in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(invalid, '_');
        }

        var root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(root, "Krypton Toolkit", "Updates", name + ".state");
    }

    private string ResolveInstallDirectory() =>
        string.IsNullOrWhiteSpace(InstallDirectory) ? AppDomain.CurrentDomain.BaseDirectory : InstallDirectory;

    private string ResolveRestartCommand()
    {
        if (!string.IsNullOrWhiteSpace(RestartCommand))
        {
            return RestartCommand;
        }

        try
        {
            var path = Process.GetCurrentProcess().MainModule?.FileName;
            if (path != null && !string.IsNullOrWhiteSpace(path))
            {
                return path;
            }
        }
        catch (Exception)
        {
            // Some hosts deny access to the process module. Fall back to the entry assembly location.
        }

        return Assembly.GetEntryAssembly()?.Location ?? string.Empty;
    }

    private void ExitIfRequested()
    {
        if (!ExitApplicationOnApply)
        {
            return;
        }

        Report(Application.Exit);
    }

    private void CancelDownload()
    {
        try
        {
            _downloadCancellation?.Cancel();
        }
        catch (ObjectDisposedException)
        {
            // The download already finished and disposed the source.
        }
    }

    private Form? TryGetOwnerForm()
    {
        var container = Site?.Container;
        if (container != null)
        {
            foreach (IComponent component in container.Components)
            {
                if (component is Form form)
                {
                    return form;
                }
            }
        }

        return Form.ActiveForm;
    }

    private void Report(Action action)
    {
        var owner = TryGetOwnerForm();
        if (owner != null && !owner.IsDisposed && owner.InvokeRequired)
        {
            owner.Invoke(action);
            return;
        }

        action();
    }

    private static int ToTimerInterval(TimeSpan interval)
    {
        var milliseconds = interval.TotalMilliseconds;
        if (milliseconds < 1000)
        {
            return 1000;
        }

        if (milliseconds > int.MaxValue)
        {
            return int.MaxValue;
        }

        return (int)milliseconds;
    }

    private static void Observe(Task task)
    {
        task.ContinueWith(
            completed =>
            {
                var ignored = completed.Exception;
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.Default);
    }

    private static bool HasPartialDownload(string path)
    {
        try
        {
            return File.Exists(path) && new FileInfo(path).Length > 0;
        }
        catch (IOException)
        {
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }

    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception)
        {
            // Apply already failed. Leaving the rejected file in temp is safe.
        }
    }
}
