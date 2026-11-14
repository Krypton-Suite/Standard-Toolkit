#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Choice made on <see cref="VisualApplicationUpdatePromptForm"/>.</summary>
internal enum KryptonUpdatePromptChoice
{
    Dismiss = 0,
    UpdateNow = 1,
    Postpone = 2,
    Skip = 3
}

/// <summary>Choice made on the remind-later dialog.</summary>
internal enum KryptonUpdatePostponeChoice
{
    /// <summary>Return to the window that opened the dialog.</summary>
    Cancel = 0,

    /// <summary>Hide this version until the chosen date.</summary>
    Postpone = 1,

    /// <summary>Ignore this version until a newer one is published.</summary>
    Skip = 2
}

/// <summary>Choice made on <see cref="VisualApplicationUpdateProgressForm"/> after a download stops early.</summary>
internal enum KryptonUpdateDownloadRecovery
{
    /// <summary>Continue the bytes already in the temp file.</summary>
    Resume = 0,

    /// <summary>Replace the temp file with a new download from the first byte.</summary>
    Restart = 1,

    /// <summary>Leave the update without another transfer.</summary>
    Close = 2
}

/// <summary>Where <see cref="KryptonApplicationUpdater"/> looks for a new version when <see cref="KryptonApplicationUpdater.Provider"/> is not set.</summary>
public enum KryptonUpdateFeedKind
{
    /// <summary>An HTTPS JSON manifest, or a local manifest file.</summary>
    JsonManifest = 0,

    /// <summary>The latest GitHub release for <see cref="KryptonApplicationUpdater.GitHubOwner"/> and <see cref="KryptonApplicationUpdater.GitHubRepository"/>.</summary>
    GitHubReleases = 1
}

/// <summary>When a check runs and whether a matching update is applied without a prompt.</summary>
public enum KryptonUpdateMode
{
    /// <summary>Check only when <see cref="KryptonApplicationUpdater.CheckNow"/> is called.</summary>
    Manual = 0,

    /// <summary>Check on the schedule and ask before downloading.</summary>
    Notify = 1,

    /// <summary>Check on the schedule and apply when the severity is at or below <see cref="KryptonApplicationUpdater.AutomaticApplyMaximumSeverity"/>.</summary>
    Automatic = 2
}

/// <summary>How strongly the feed recommends the update. Higher values outrank the automatic-apply threshold.</summary>
public enum KryptonUpdateSeverity
{
    /// <summary>The user can skip or postpone the update.</summary>
    Optional = 0,

    /// <summary>The usual update. Automatic mode applies it when the threshold is <see cref="Recommended"/> or higher.</summary>
    Recommended = 1,

    /// <summary>Shown as a prompt unless the automatic threshold includes important updates.</summary>
    Important = 2,

    /// <summary>Required. Remind later and Skip this version are hidden.</summary>
    Critical = 3
}

/// <summary>How a downloaded package is applied.</summary>
public enum KryptonUpdatePackageKind
{
    /// <summary>An executable or MSI that is started and, by default, followed by exiting the application.</summary>
    Installer = 0,

    /// <summary>A zip extracted by the update helper after this process exits, or immediately when the host stays open.</summary>
    Archive = 1
}

/// <summary>Outcome of <see cref="KryptonApplicationUpdater.CheckAsync"/> or <see cref="KryptonApplicationUpdater.CheckNowAsync"/>.</summary>
public enum KryptonUpdateCheckStatus
{
    /// <summary>The call did not contact the feed because the mode is manual.</summary>
    Idle = 0,

    /// <summary>A scheduled check was skipped because the last check is still inside <see cref="KryptonApplicationUpdater.CheckInterval"/>.</summary>
    IgnoredInterval = 1,

    /// <summary>The feed has no version newer than the current one.</summary>
    UpToDate = 2,

    /// <summary>The package was verified and the installer or helper was started.</summary>
    Applied = 3,

    /// <summary>The user asked to be reminded later, or a scheduled check is still inside that window.</summary>
    Postponed = 4,

    /// <summary>This version was skipped. A newer version is still reported.</summary>
    Skipped = 5,

    /// <summary>The user or a cancelling event handler stopped the check, download, or apply.</summary>
    Cancelled = 6,

    /// <summary>The feed, download, verification, or apply failed. See <see cref="KryptonUpdateCheckResult.Error"/>.</summary>
    Failed = 7
}

/// <summary>Primary progress control shown while a package downloads.</summary>
public enum KryptonUpdateProgressDisplay
{
    /// <summary>A percent (or marquee) progress bar.</summary>
    ProgressBar = 0,

    /// <summary>A <see cref="Krypton.Graphs.KryptonTransferProgressPanel"/> (speed chart plus optional progress chart).</summary>
    TransferSpeedGraph = 1
}
