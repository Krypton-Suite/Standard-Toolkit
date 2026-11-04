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
/// Lists release notes on their own. Opened from the update prompt it only closes.
/// Opened on demand it can also update or remind later when a newer package exists.
/// </summary>
internal partial class VisualApplicationUpdateNotesForm : KryptonForm
{
    private KryptonUpdatePackage? _offer;
    private KryptonApplicationUpdaterStrings _strings = new KryptonApplicationUpdaterStrings();
    private int _defaultDays = 1;
    private bool _allowSkip;

    /// <summary>Initializes the notes window.</summary>
    public VisualApplicationUpdateNotesForm()
    {
        InitializeComponent();
        btnUpdate.Click += OnUpdateNow;
        btnRemind.Click += OnRemind;
        btnSkip.Click += OnSkip;
        btnClose.Click += OnClose;
    }

    /// <summary>Gets the button the user chose. Close is <see cref="KryptonUpdatePromptChoice.Dismiss"/>.</summary>
    internal KryptonUpdatePromptChoice Choice { get; private set; } = KryptonUpdatePromptChoice.Dismiss;

    /// <summary>Gets the span chosen on the remind-later dialog. Zero unless the choice is postpone.</summary>
    internal TimeSpan PostponeFor { get; private set; }

    /// <summary>Fills the notes and chooses which actions are available.</summary>
    /// <param name="offer">Newest package Update now would install. Null when nothing is newer.</param>
    /// <param name="current">Installed version.</param>
    /// <param name="notes">Packages listed newest first.</param>
    /// <param name="strings">Localised labels.</param>
    /// <param name="allowApply">When true, Update now and Remind later are offered for <paramref name="offer"/>.</param>
    /// <param name="allowPostpone">When false, Remind later is hidden. A package that does not allow postpone passes false.</param>
    /// <param name="allowSkip">When false, Skip this version is hidden. When postpone is hidden, Skip is shown on this window.</param>
    /// <param name="defaultDays">Initial day count for the remind-later dialog.</param>
    /// <param name="failureMessage">When set, replaces the notes and hides Update now.</param>
    internal void Bind(KryptonUpdatePackage? offer, Version current, IReadOnlyList<KryptonUpdatePackage> notes, KryptonApplicationUpdaterStrings strings, bool allowApply, bool allowPostpone, bool allowSkip, int defaultDays, string? failureMessage)
    {
        _offer = offer;
        _strings = strings ?? new KryptonApplicationUpdaterStrings();
        _defaultDays = defaultDays < 1 ? 1 : defaultDays;
        _allowSkip = allowSkip;
        var failed = failureMessage != null && !string.IsNullOrWhiteSpace(failureMessage);
        var offering = allowApply && offer != null && !failed;
        Text = _strings.NotesWindowTitle;
        lblHeading.Text = failed ? _strings.ReleaseNotes : offering ? _strings.Heading : offer == null ? _strings.UpToDateHeading : _strings.ReleaseNotes;
        lblCurrent.Text = _strings.CurrentVersion + " " + current;
        txtNotes.Text = failed ? failureMessage : FormatNotes(notes, _strings);
        txtNotes.DeselectAll();
        btnUpdate.Text = _strings.UpdateNow;
        btnRemind.Text = _strings.RemindLater;
        btnSkip.Text = _strings.SkipVersion;
        btnClose.Text = _strings.Close;
        btnUpdate.Visible = offering;
        btnRemind.Visible = offering && allowPostpone;
        btnSkip.Visible = offering && allowSkip && !allowPostpone;
        btnClose.Visible = true;
        AcceptButton = offering ? btnUpdate : btnClose;
        CancelButton = btnClose;
        ActiveControl = offering ? btnUpdate : btnClose;
    }

    internal static string FormatNotes(IReadOnlyList<KryptonUpdatePackage>? notes, KryptonApplicationUpdaterStrings strings)
    {
        if (notes == null || notes.Count == 0)
        {
            return string.Empty;
        }

        var builder = new StringBuilder();
        for (var index = 0; index < notes.Count; index++)
        {
            var package = notes[index];
            builder.Append(package.Version);
            builder.Append("    ");
            builder.Append(strings.GetSeverityText(package.Severity));
            if (package.ReleasedUtc.HasValue)
            {
                builder.Append("    ");
                builder.Append(FormatReleased(package.ReleasedUtc.Value));
            }

            builder.AppendLine();
            var text = package.ReleaseNotes;
            if (text != null && !string.IsNullOrWhiteSpace(text))
            {
                builder.AppendLine(text.Trim());
            }

            if (index < notes.Count - 1)
            {
                builder.AppendLine();
            }
        }

        return builder.ToString();
    }

    private static string FormatReleased(DateTime value)
    {
        var utc = value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value;
        return utc.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
    }

    private void OnUpdateNow(object? sender, EventArgs e)
    {
        Choice = KryptonUpdatePromptChoice.UpdateNow;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnRemind(object? sender, EventArgs e)
    {
        var offer = _offer;
        if (offer == null)
        {
            return;
        }

        if (!VisualApplicationUpdatePostponeForm.TryShow(this, _strings, _defaultDays, offer.MaximumPostponeDays, _allowSkip, out var choice, out var postponeFor))
        {
            return;
        }

        Choice = choice == KryptonUpdatePostponeChoice.Skip ? KryptonUpdatePromptChoice.Skip : KryptonUpdatePromptChoice.Postpone;
        PostponeFor = postponeFor;
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void OnSkip(object? sender, EventArgs e)
    {
        Choice = KryptonUpdatePromptChoice.Skip;
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void OnClose(object? sender, EventArgs e)
    {
        Choice = KryptonUpdatePromptChoice.Dismiss;
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
