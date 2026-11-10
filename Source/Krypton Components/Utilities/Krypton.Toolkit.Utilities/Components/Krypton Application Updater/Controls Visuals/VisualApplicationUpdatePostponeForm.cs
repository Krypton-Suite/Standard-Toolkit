#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Asks which date to wait until, or whether to skip this version.</summary>
internal partial class VisualApplicationUpdatePostponeForm : KryptonForm
{
    /// <summary>Initializes the remind-later dialog.</summary>
    public VisualApplicationUpdatePostponeForm()
    {
        InitializeComponent();
        btnRemind.Click += OnRemind;
        btnSkip.Click += OnSkip;
        btnCancel.Click += OnCancel;
    }

    /// <summary>Gets the button the user chose. The title-bar close is cancel.</summary>
    internal KryptonUpdatePostponeChoice Choice { get; private set; } = KryptonUpdatePostponeChoice.Cancel;

    /// <summary>Gets how long to wait when <see cref="Choice"/> is postpone. Otherwise zero.</summary>
    internal TimeSpan PostponeFor { get; private set; }

    /// <summary>Shows the dialog. Returns false when the user cancels.</summary>
    /// <param name="owner">Window to center on.</param>
    /// <param name="strings">Localised labels.</param>
    /// <param name="defaultDays">How many days ahead the date starts.</param>
    /// <param name="maximumDays">Latest selectable date, in days from today. Values below 1 become 1.</param>
    /// <param name="allowSkip">When false, Skip this version is hidden.</param>
    /// <param name="choice">The button the user chose.</param>
    /// <param name="postponeFor">The chosen span, or zero when the choice is not postpone.</param>
    /// <returns>True when the user postponed or skipped.</returns>
    internal static bool TryShow(IWin32Window owner, KryptonApplicationUpdaterStrings strings, int defaultDays, int maximumDays, bool allowSkip, out KryptonUpdatePostponeChoice choice, out TimeSpan postponeFor)
    {
        using (var dialog = new VisualApplicationUpdatePostponeForm())
        {
            dialog.Bind(strings, defaultDays, maximumDays, allowSkip);
            if (owner != null)
            {
                dialog.ShowDialog(owner);
            }
            else
            {
                dialog.ShowDialog();
            }

            choice = dialog.Choice;
            postponeFor = dialog.PostponeFor;
            return dialog.Choice != KryptonUpdatePostponeChoice.Cancel;
        }
    }

    /// <summary>Applies labels and the initial date.</summary>
    /// <param name="strings">Localised labels.</param>
    /// <param name="defaultDays">How many days ahead the date starts.</param>
    /// <param name="maximumDays">Latest selectable date, in days from today. Values below 1 become 1.</param>
    /// <param name="allowSkip">When false, Skip this version is hidden.</param>
    internal void Bind(KryptonApplicationUpdaterStrings strings, int defaultDays, int maximumDays, bool allowSkip)
    {
        var limit = maximumDays < 1 ? 1 : maximumDays;
        var days = defaultDays < 1 ? 1 : defaultDays;
        if (days > limit)
        {
            days = limit;
        }

        var today = DateTime.Today;
        Text = strings.RemindLater;
        lblPrompt.Text = strings.PostponePrompt;
        btnRemind.Text = strings.RemindLater;
        btnSkip.Text = strings.SkipVersion;
        btnSkip.Visible = allowSkip;
        btnCancel.Text = strings.Cancel;
        dtpRemind.Format = DateTimePickerFormat.Short;
        dtpRemind.MaxDate = today.AddDays(limit);
        dtpRemind.Value = today.AddDays(days);
        dtpRemind.MinDate = today.AddDays(1);
        AcceptButton = btnRemind;
        CancelButton = btnCancel;
        ActiveControl = dtpRemind;
    }

    private void OnRemind(object? sender, EventArgs e)
    {
        Choice = KryptonUpdatePostponeChoice.Postpone;
        var selected = DateTime.SpecifyKind(dtpRemind.Value.Date, DateTimeKind.Local);
        var span = selected.ToUniversalTime() - DateTime.UtcNow;
        PostponeFor = span > TimeSpan.Zero ? span : TimeSpan.FromDays(1);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnSkip(object? sender, EventArgs e)
    {
        Choice = KryptonUpdatePostponeChoice.Skip;
        PostponeFor = TimeSpan.Zero;
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void OnCancel(object? sender, EventArgs e)
    {
        Choice = KryptonUpdatePostponeChoice.Cancel;
        PostponeFor = TimeSpan.Zero;
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
