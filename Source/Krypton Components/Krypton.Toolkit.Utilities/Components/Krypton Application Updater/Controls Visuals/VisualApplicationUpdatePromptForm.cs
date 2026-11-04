#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Asks whether to apply the newest package, open its notes, or remind later.</summary>
internal partial class VisualApplicationUpdatePromptForm : KryptonForm
{
    private KryptonUpdatePackage? _offer;
    private Version _current = new Version(0, 0, 0, 0);
    private IReadOnlyList<KryptonUpdatePackage> _notes = Array.Empty<KryptonUpdatePackage>();
    private KryptonApplicationUpdaterStrings _strings = new KryptonApplicationUpdaterStrings();
    private int _defaultDays = 1;
    private bool _allowSkip;

    /// <summary>Initializes the prompt.</summary>
    public VisualApplicationUpdatePromptForm()
    {
        InitializeComponent();
        btnUpdate.Click += OnUpdateNow;
        btnNotes.Click += OnNotes;
        btnRemind.Click += OnRemind;
        btnSkip.Click += OnSkip;
    }

    /// <summary>Gets the button the user chose. Closing the window is <see cref="KryptonUpdatePromptChoice.Dismiss"/>.</summary>
    internal KryptonUpdatePromptChoice Choice { get; private set; } = KryptonUpdatePromptChoice.Dismiss;

    /// <summary>Gets the span chosen on the remind-later dialog. Zero unless the choice is postpone.</summary>
    internal TimeSpan PostponeFor { get; private set; }

    /// <summary>Fills the short decision window. Release notes open in their own window.</summary>
    /// <param name="offer">Newest package that Update now would install.</param>
    /// <param name="current">Installed version.</param>
    /// <param name="notes">Packages listed from the Release notes button, newest first.</param>
    /// <param name="strings">Localised labels.</param>
    /// <param name="allowPostpone">When false, Remind later is hidden.</param>
    /// <param name="allowSkip">When false, Skip this version is hidden. When postpone is hidden, Skip is shown on this window.</param>
    /// <param name="defaultDays">Initial day count for the remind-later dialog.</param>
    internal void Bind(KryptonUpdatePackage offer, Version current, IReadOnlyList<KryptonUpdatePackage> notes, KryptonApplicationUpdaterStrings strings, bool allowPostpone, bool allowSkip, int defaultDays)
    {
        _offer = offer;
        _current = current ?? new Version(0, 0, 0, 0);
        _notes = notes ?? Array.Empty<KryptonUpdatePackage>();
        _strings = strings ?? new KryptonApplicationUpdaterStrings();
        _defaultDays = defaultDays < 1 ? 1 : defaultDays;
        _allowSkip = allowSkip;
        Text = _strings.WindowTitle;
        lblHeading.Text = _strings.Heading;
        lblCurrent.Text = _strings.CurrentVersion + " " + _current;
        lblNew.Text = _strings.NewVersion + " " + offer.Version;
        lblSeverity.Text = _strings.Severity + " " + _strings.GetSeverityText(offer.Severity);
        lblMandatory.Text = _strings.Mandatory;
        lblMandatory.Visible = offer.IsMandatory;
        btnUpdate.Text = _strings.UpdateNow;
        btnNotes.Text = _strings.ReleaseNotes;
        btnRemind.Text = _strings.RemindLater;
        btnRemind.Visible = allowPostpone;
        btnSkip.Text = _strings.SkipVersion;
        btnSkip.Visible = allowSkip && !allowPostpone;
        AcceptButton = btnUpdate;
        CancelButton = null;
        ActiveControl = btnUpdate;
    }

    private void OnUpdateNow(object? sender, EventArgs e)
    {
        Choice = KryptonUpdatePromptChoice.UpdateNow;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnNotes(object? sender, EventArgs e)
    {
        var offer = _offer;
        if (offer == null)
        {
            return;
        }

        using (var notes = new VisualApplicationUpdateNotesForm())
        {
            notes.Bind(offer, _current, _notes, _strings, false, false, false, _defaultDays, null);
            notes.ShowDialog(this);
        }
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
}
