#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Localisable text for <see cref="KryptonApplicationUpdater"/> prompts.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonApplicationUpdaterStrings : GlobalId
{
    private const string DEFAULT_WINDOW_TITLE = @"Update available";
    private const string DEFAULT_HEADING = @"A new version is available.";
    private const string DEFAULT_CURRENT_VERSION = @"Current version:";
    private const string DEFAULT_NEW_VERSION = @"New version:";
    private const string DEFAULT_SEVERITY = @"Severity:";
    private const string DEFAULT_RELEASE_NOTES = @"Release notes";
    private const string DEFAULT_UPDATE_NOW = @"Update now";
    private const string DEFAULT_REMIND_LATER = @"Remind me later";
    private const string DEFAULT_SKIP_VERSION = @"Skip this version";
    private const string DEFAULT_DOWNLOADING = @"Downloading update...";
    private const string DEFAULT_VERIFYING = @"Verifying update...";
    private const string DEFAULT_APPLYING = @"Applying update...";
    private const string DEFAULT_CANCEL = @"Cancel";
    private const string DEFAULT_MANDATORY = @"This update is required.";
    private const string DEFAULT_OPTIONAL = @"Optional";
    private const string DEFAULT_RECOMMENDED = @"Recommended";
    private const string DEFAULT_IMPORTANT = @"Important";
    private const string DEFAULT_CRITICAL = @"Critical";
    private const string DEFAULT_NOTES_WINDOW_TITLE = @"Release notes";
    private const string DEFAULT_UP_TO_DATE_HEADING = @"You are up to date.";
    private const string DEFAULT_CLOSE = @"Close";
    private const string DEFAULT_RESUME = @"Resume";
    private const string DEFAULT_RESTART = @"Restart";
    private const string DEFAULT_DOWNLOAD_INTERRUPTED = @"The download was interrupted.";
    private const string DEFAULT_DOWNLOAD_RESTARTING = @"The server cannot resume this download. Starting again.";
    private const string DEFAULT_POSTPONE_PROMPT = @"Remind me on";
    private const string DEFAULT_DETAILS = @"Details";
    private const string DEFAULT_HIDE_DETAILS = @"Hide details";
    private const string DEFAULT_FILE_NAME = @"File name:";
    private const string DEFAULT_DOWNLOADED = @"Downloaded:";
    private const string DEFAULT_DOWNLOAD_OF = @"of";
    private const string DEFAULT_SPEED = @"Speed:";
    private const string DEFAULT_REMAINING = @"Time remaining:";
    private const string DEFAULT_UNKNOWN = @"Unknown";

    /// <summary>Initializes strings to their defaults.</summary>
    public KryptonApplicationUpdaterStrings() => Reset();

    /// <inheritdoc />
    public override string ToString() => IsDefault ? string.Empty : "Modified";

    /// <summary>Gets a value indicating that every string is still the factory value.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault =>
        WindowTitle == DEFAULT_WINDOW_TITLE &&
        Heading == DEFAULT_HEADING &&
        CurrentVersion == DEFAULT_CURRENT_VERSION &&
        NewVersion == DEFAULT_NEW_VERSION &&
        Severity == DEFAULT_SEVERITY &&
        ReleaseNotes == DEFAULT_RELEASE_NOTES &&
        UpdateNow == DEFAULT_UPDATE_NOW &&
        RemindLater == DEFAULT_REMIND_LATER &&
        SkipVersion == DEFAULT_SKIP_VERSION &&
        Downloading == DEFAULT_DOWNLOADING &&
        Verifying == DEFAULT_VERIFYING &&
        Applying == DEFAULT_APPLYING &&
        Cancel == DEFAULT_CANCEL &&
        Mandatory == DEFAULT_MANDATORY &&
        SeverityOptional == DEFAULT_OPTIONAL &&
        SeverityRecommended == DEFAULT_RECOMMENDED &&
        SeverityImportant == DEFAULT_IMPORTANT &&
        SeverityCritical == DEFAULT_CRITICAL &&
        NotesWindowTitle == DEFAULT_NOTES_WINDOW_TITLE &&
        UpToDateHeading == DEFAULT_UP_TO_DATE_HEADING &&
        Close == DEFAULT_CLOSE &&
        Resume == DEFAULT_RESUME &&
        Restart == DEFAULT_RESTART &&
        DownloadInterrupted == DEFAULT_DOWNLOAD_INTERRUPTED &&
        DownloadRestarting == DEFAULT_DOWNLOAD_RESTARTING &&
        PostponePrompt == DEFAULT_POSTPONE_PROMPT &&
        Details == DEFAULT_DETAILS &&
        HideDetails == DEFAULT_HIDE_DETAILS &&
        FileName == DEFAULT_FILE_NAME &&
        Downloaded == DEFAULT_DOWNLOADED &&
        DownloadOf == DEFAULT_DOWNLOAD_OF &&
        Speed == DEFAULT_SPEED &&
        Remaining == DEFAULT_REMAINING &&
        Unknown == DEFAULT_UNKNOWN;

    /// <summary>Restores every string to its factory value.</summary>
    public void Reset()
    {
        WindowTitle = DEFAULT_WINDOW_TITLE;
        Heading = DEFAULT_HEADING;
        CurrentVersion = DEFAULT_CURRENT_VERSION;
        NewVersion = DEFAULT_NEW_VERSION;
        Severity = DEFAULT_SEVERITY;
        ReleaseNotes = DEFAULT_RELEASE_NOTES;
        UpdateNow = DEFAULT_UPDATE_NOW;
        RemindLater = DEFAULT_REMIND_LATER;
        SkipVersion = DEFAULT_SKIP_VERSION;
        Downloading = DEFAULT_DOWNLOADING;
        Verifying = DEFAULT_VERIFYING;
        Applying = DEFAULT_APPLYING;
        Cancel = DEFAULT_CANCEL;
        Mandatory = DEFAULT_MANDATORY;
        SeverityOptional = DEFAULT_OPTIONAL;
        SeverityRecommended = DEFAULT_RECOMMENDED;
        SeverityImportant = DEFAULT_IMPORTANT;
        SeverityCritical = DEFAULT_CRITICAL;
        NotesWindowTitle = DEFAULT_NOTES_WINDOW_TITLE;
        UpToDateHeading = DEFAULT_UP_TO_DATE_HEADING;
        Close = DEFAULT_CLOSE;
        Resume = DEFAULT_RESUME;
        Restart = DEFAULT_RESTART;
        DownloadInterrupted = DEFAULT_DOWNLOAD_INTERRUPTED;
        DownloadRestarting = DEFAULT_DOWNLOAD_RESTARTING;
        PostponePrompt = DEFAULT_POSTPONE_PROMPT;
        Details = DEFAULT_DETAILS;
        HideDetails = DEFAULT_HIDE_DETAILS;
        FileName = DEFAULT_FILE_NAME;
        Downloaded = DEFAULT_DOWNLOADED;
        DownloadOf = DEFAULT_DOWNLOAD_OF;
        Speed = DEFAULT_SPEED;
        Remaining = DEFAULT_REMAINING;
        Unknown = DEFAULT_UNKNOWN;
    }

    /// <summary>Text for a severity value.</summary>
    /// <param name="severity">Severity from the feed.</param>
    /// <returns>The localised label.</returns>
    public string GetSeverityText(KryptonUpdateSeverity severity)
    {
        switch (severity)
        {
            case KryptonUpdateSeverity.Optional:
                return SeverityOptional;
            case KryptonUpdateSeverity.Recommended:
                return SeverityRecommended;
            case KryptonUpdateSeverity.Important:
                return SeverityImportant;
            case KryptonUpdateSeverity.Critical:
                return SeverityCritical;
            default:
                return severity.ToString();
        }
    }

    /// <summary>Prompt window title.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_WINDOW_TITLE)]
    public string WindowTitle { get; set; } = DEFAULT_WINDOW_TITLE;

    /// <summary>First line of the prompt.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_HEADING)]
    public string Heading { get; set; } = DEFAULT_HEADING;

    /// <summary>Label in front of the installed version.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CURRENT_VERSION)]
    public string CurrentVersion { get; set; } = DEFAULT_CURRENT_VERSION;

    /// <summary>Label in front of the feed version.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_NEW_VERSION)]
    public string NewVersion { get; set; } = DEFAULT_NEW_VERSION;

    /// <summary>Label in front of the severity.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SEVERITY)]
    public string Severity { get; set; } = DEFAULT_SEVERITY;

    /// <summary>Caption above the release notes.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_RELEASE_NOTES)]
    public string ReleaseNotes { get; set; } = DEFAULT_RELEASE_NOTES;

    /// <summary>Accept button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_UPDATE_NOW)]
    public string UpdateNow { get; set; } = DEFAULT_UPDATE_NOW;

    /// <summary>Postpone button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_REMIND_LATER)]
    public string RemindLater { get; set; } = DEFAULT_REMIND_LATER;

    /// <summary>Skip button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SKIP_VERSION)]
    public string SkipVersion { get; set; } = DEFAULT_SKIP_VERSION;

    /// <summary>Progress text while the file is copying.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_DOWNLOADING)]
    public string Downloading { get; set; } = DEFAULT_DOWNLOADING;

    /// <summary>Progress text while the hash is checked.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_VERIFYING)]
    public string Verifying { get; set; } = DEFAULT_VERIFYING;

    /// <summary>Progress text while the installer or helper starts.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_APPLYING)]
    public string Applying { get; set; } = DEFAULT_APPLYING;

    /// <summary>Cancel button on the progress window.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CANCEL)]
    public string Cancel { get; set; } = DEFAULT_CANCEL;

    /// <summary>Shown when the severity is critical.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_MANDATORY)]
    public string Mandatory { get; set; } = DEFAULT_MANDATORY;

    /// <summary>Label for <see cref="KryptonUpdateSeverity.Optional"/>.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_OPTIONAL)]
    public string SeverityOptional { get; set; } = DEFAULT_OPTIONAL;

    /// <summary>Label for <see cref="KryptonUpdateSeverity.Recommended"/>.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_RECOMMENDED)]
    public string SeverityRecommended { get; set; } = DEFAULT_RECOMMENDED;

    /// <summary>Label for <see cref="KryptonUpdateSeverity.Important"/>.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_IMPORTANT)]
    public string SeverityImportant { get; set; } = DEFAULT_IMPORTANT;

    /// <summary>Label for <see cref="KryptonUpdateSeverity.Critical"/>.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CRITICAL)]
    public string SeverityCritical { get; set; } = DEFAULT_CRITICAL;

    /// <summary>Window title when the feed has nothing newer, or the feed could not be read.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_NOTES_WINDOW_TITLE)]
    public string NotesWindowTitle { get; set; } = DEFAULT_NOTES_WINDOW_TITLE;

    /// <summary>Heading when the installed version is already the newest in the feed.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_UP_TO_DATE_HEADING)]
    public string UpToDateHeading { get; set; } = DEFAULT_UP_TO_DATE_HEADING;

    /// <summary>Close button on the notes-only window and on the progress window after a download stops.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CLOSE)]
    public string Close { get; set; } = DEFAULT_CLOSE;

    /// <summary>Continues a partial download on the progress window.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_RESUME)]
    public string Resume { get; set; } = DEFAULT_RESUME;

    /// <summary>Downloads the package again from the first byte.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_RESTART)]
    public string Restart { get; set; } = DEFAULT_RESTART;

    /// <summary>Shown when the connection drops or the user cancels while bytes are still moving.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_DOWNLOAD_INTERRUPTED)]
    public string DownloadInterrupted { get; set; } = DEFAULT_DOWNLOAD_INTERRUPTED;

    /// <summary>Shown when a resume is not accepted and the file is downloaded from the start.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_DOWNLOAD_RESTARTING)]
    public string DownloadRestarting { get; set; } = DEFAULT_DOWNLOAD_RESTARTING;

    /// <summary>Label beside the date on the remind-later dialog.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_POSTPONE_PROMPT)]
    public string PostponePrompt { get; set; } = DEFAULT_POSTPONE_PROMPT;

    /// <summary>Expands the progress footer.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_DETAILS)]
    public string Details { get; set; } = DEFAULT_DETAILS;

    /// <summary>Collapses the progress footer.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_HIDE_DETAILS)]
    public string HideDetails { get; set; } = DEFAULT_HIDE_DETAILS;

    /// <summary>Progress footer label for the package file name.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_FILE_NAME)]
    public string FileName { get; set; } = DEFAULT_FILE_NAME;

    /// <summary>Progress footer label for the bytes copied.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_DOWNLOADED)]
    public string Downloaded { get; set; } = DEFAULT_DOWNLOADED;

    /// <summary>Word between the bytes copied and the total size.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_DOWNLOAD_OF)]
    public string DownloadOf { get; set; } = DEFAULT_DOWNLOAD_OF;

    /// <summary>Progress footer label for the transfer rate.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SPEED)]
    public string Speed { get; set; } = DEFAULT_SPEED;

    /// <summary>Progress footer label for the estimated time left.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_REMAINING)]
    public string Remaining { get; set; } = DEFAULT_REMAINING;

    /// <summary>Shown when the rate or the time left cannot be estimated.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_UNKNOWN)]
    public string Unknown { get; set; } = DEFAULT_UNKNOWN;
}
