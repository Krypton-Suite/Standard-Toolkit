#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using Resources = Krypton.Toolkit.Utilities.Properties.Resources;

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// The visual host for <see cref="KryptonFoldableDialog"/>. Presents a message-box style window with a
/// collapsible ("foldable") details region, modelled on the Visual Studio Just-In-Time debugger dialog.
/// </summary>
internal partial class VisualFoldableDialogForm : KryptonForm
{
    #region Constants

    // Down-pointing / up-pointing triangles used as the expander glyph.
    private const char EXPAND_GLYPH = '\u25BC';
    private const char COLLAPSE_GLYPH = '\u25B2';

    // Fixed height (in un-scaled pixels) reserved for the details region while expanded.
    private const int DETAILS_REGION_HEIGHT = 180;

    // Standard message-box minimum action button width (in un-scaled pixels).
    private const int MINIMUM_BUTTON_WIDTH = 78;

    #endregion

    #region Instance Fields

    private readonly KryptonFoldableDialogData _data;

    private readonly bool _hasDetails;

    private SystemSound? _sound;

    private bool _expanded;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualFoldableDialogForm"/> class.</summary>
    /// <param name="data">The data describing the dialog content and behaviour.</param>
    public VisualFoldableDialogForm(KryptonFoldableDialogData data)
    {
        InitializeComponent();

        _data = data;

        _hasDetails = !string.IsNullOrEmpty(_data.DetailsText);

        // Position is applied manually in OnLoad once the final (post-layout) size is known.
        StartPosition = FormStartPosition.Manual;

        SetupUI();
    }

    #endregion

    #region Implementation

    private void SetupUI()
    {
        Text = _data.Caption ?? string.Empty;

        kwlHeading.Text = _data.Heading ?? string.Empty;
        kwlHeading.Visible = !string.IsNullOrEmpty(_data.Heading);

        kwlMessage.Text = _data.Text ?? string.Empty;
        kwlMessage.Visible = !string.IsNullOrEmpty(_data.Text);

        krtbDetails.Text = _data.DetailsText ?? string.Empty;

        // The expander and details region only appear when there is something to fold.
        kbtnExpander.Visible = _hasDetails;
        kpnlDetails.Visible = false;

        SetupIcon();

        SetupButtons();
    }

    private void SetupIcon()
    {
        Image? image = null;

        var win11 = OSUtilities.IsAtLeastWindowsEleven;

        switch (_data.Icon)
        {
            case ExtendedKryptonMessageBoxIcon.Custom:
                image = _data.CustomIcon;
                break;
            case ExtendedKryptonMessageBoxIcon.None:
                break;
            case ExtendedKryptonMessageBoxIcon.Hand:
            case ExtendedKryptonMessageBoxIcon.SystemHand:
            case ExtendedKryptonMessageBoxIcon.Stop:
            case ExtendedKryptonMessageBoxIcon.Error:
                image = win11 ? Resources.Critical_Windows_11 : Resources.Stop;
                _sound = SystemSounds.Hand;
                break;
            case ExtendedKryptonMessageBoxIcon.Question:
            case ExtendedKryptonMessageBoxIcon.SystemQuestion:
                image = win11 ? Resources.Question_Windows_11 : Resources.Question;
                _sound = SystemSounds.Question;
                break;
            case ExtendedKryptonMessageBoxIcon.Exclamation:
            case ExtendedKryptonMessageBoxIcon.Warning:
            case ExtendedKryptonMessageBoxIcon.SystemExclamation:
                image = win11 ? Resources.Warning_Windows_11 : Resources.Warning;
                _sound = SystemSounds.Exclamation;
                break;
            case ExtendedKryptonMessageBoxIcon.Asterisk:
            case ExtendedKryptonMessageBoxIcon.Information:
            case ExtendedKryptonMessageBoxIcon.SystemAsterisk:
                image = win11 ? Resources.Information_Windows_11 : Resources.Asterisk;
                _sound = SystemSounds.Asterisk;
                break;
            case ExtendedKryptonMessageBoxIcon.Shield:
                image = Resources.UAC_Shield_Windows_11;
                break;
            case ExtendedKryptonMessageBoxIcon.WindowsLogo:
                image = Resources.Windows11;
                break;
            case ExtendedKryptonMessageBoxIcon.Application:
            case ExtendedKryptonMessageBoxIcon.SystemApplication:
                image = SystemIcons.Application.ToBitmap();
                break;
        }

        pbxIcon.Image = image;
        pbxIcon.Visible = image != null;
    }

    private void SetupButtons()
    {
        GeneralToolkitStrings strings = KryptonManager.Strings.GeneralStrings;

        List<(string Text, DialogResult Result)> definitions = _data.Buttons switch
        {
            KryptonMessageBoxButtons.OKCancel =>
                new List<(string, DialogResult)> { (strings.OK, DialogResult.OK), (strings.Cancel, DialogResult.Cancel) },
            KryptonMessageBoxButtons.YesNo =>
                new List<(string, DialogResult)> { (strings.Yes, DialogResult.Yes), (strings.No, DialogResult.No) },
            KryptonMessageBoxButtons.YesNoCancel =>
                new List<(string, DialogResult)> { (strings.Yes, DialogResult.Yes), (strings.No, DialogResult.No), (strings.Cancel, DialogResult.Cancel) },
            KryptonMessageBoxButtons.RetryCancel =>
                new List<(string, DialogResult)> { (strings.Retry, DialogResult.Retry), (strings.Cancel, DialogResult.Cancel) },
            KryptonMessageBoxButtons.AbortRetryIgnore =>
                new List<(string, DialogResult)> { (strings.Abort, DialogResult.Abort), (strings.Retry, DialogResult.Retry), (strings.Ignore, DialogResult.Ignore) },
            _ => new List<(string, DialogResult)> { (strings.OK, DialogResult.OK) }
        };

        var buttons = new[] { kbtnButton1, kbtnButton2, kbtnButton3 };

        for (var i = 0; i < buttons.Length; i++)
        {
            if (i < definitions.Count)
            {
                buttons[i].Text = definitions[i].Text;
                buttons[i].DialogResult = definitions[i].Result;
                buttons[i].Visible = true;
            }
            else
            {
                buttons[i].Visible = false;
                buttons[i].DialogResult = DialogResult.None;
            }
        }

        // Default (accept) button honours the requested index but is clamped to the visible set.
        var defaultIndex = _data.DefaultButton switch
        {
            KryptonMessageBoxDefaultButton.Button2 => 1,
            KryptonMessageBoxDefaultButton.Button3 => 2,
            _ => 0
        };

        if (defaultIndex >= definitions.Count)
        {
            defaultIndex = 0;
        }

        AcceptButton = buttons[defaultIndex];

        // Escape / close maps to the most cancel-like button so the dialog can always be dismissed.
        CancelButton = FindCancelButton(buttons, definitions);
    }

    private static IButtonControl FindCancelButton(KryptonButton[] buttons, List<(string Text, DialogResult Result)> definitions)
    {
        var cancelIndex = definitions.FindIndex(static d => d.Result == DialogResult.Cancel);

        if (cancelIndex < 0)
        {
            cancelIndex = definitions.FindIndex(static d => d.Result == DialogResult.No);
        }

        if (cancelIndex < 0)
        {
            cancelIndex = definitions.Count - 1;
        }

        return buttons[cancelIndex];
    }

    private void UpdateExpanderText()
    {
        var caption = _expanded
            ? _data.ExpandButtonText ?? string.Empty
            : _data.CollapseButtonText ?? string.Empty;

        var glyph = _expanded ? COLLAPSE_GLYPH : EXPAND_GLYPH;

        kbtnExpander.Text = $@"{glyph} {caption}";
    }

    private void ApplyExpandState(bool expanded)
    {
        _expanded = expanded && _hasDetails;

        kpnlDetails.Visible = _expanded;

        UpdateExpanderText();

        // The header is auto-sized, so read its current height and add the button strip plus (when
        // expanded) the fixed details region to arrive at the required client height.
        var detailsHeight = _expanded ? LogicalToDeviceUnits(DETAILS_REGION_HEIGHT) : 0;

        var clientHeight = tlpHeader.Height + kpnlButtons.Height + detailsHeight;

        ClientSize = new Size(ClientSize.Width, clientHeight);
    }

    /// <summary>
    /// Positions the dialog according to the requested <see cref="KryptonFoldableDialogData.StartPosition"/>.
    /// This is applied manually (rather than via <see cref="Form.StartPosition"/>) because the dialog is
    /// resized in <see cref="OnLoad"/> once the final header/details layout is known, i.e. after WinForms
    /// would already have positioned a <see cref="FormStartPosition.CenterScreen"/> window.
    /// </summary>
    private void ApplyStartPosition()
    {
        Form? ownerForm = _data.Owner is Control control && control.FindForm() is { IsDisposed: false } form
            ? form
            : null;

        switch (_data.StartPosition)
        {
            case FormStartPosition.Manual:
                // The caller (or designer) controls the location; leave it untouched.
                break;
            case FormStartPosition.CenterParent when ownerForm != null:
                CenterWithin(ownerForm.Bounds);
                break;
            default:
                // CenterScreen (the default) and any unsupported value centre on the working area of the
                // screen hosting the owner, falling back to the primary screen when there is no owner.
                CenterWithin((ownerForm != null ? Screen.FromControl(ownerForm) : Screen.PrimaryScreen!).WorkingArea);
                break;
        }
    }

    private void CenterWithin(Rectangle bounds)
    {
        var left = bounds.Left + ((bounds.Width - Width) / 2);
        var top = bounds.Top + ((bounds.Height - Height) / 2);

        Location = new Point(Math.Max(bounds.Left, left), Math.Max(bounds.Top, top));
    }

    /// <summary>
    /// Sizes both wrap-labels to their true multi-line height for the current header text column width.
    /// The labels are switched to a fixed size (measured with their resolved palette font) so the
    /// auto-sized header reports a deterministic height regardless of layout/paint ordering; a docked or
    /// auto-size <see cref="Label"/> otherwise measures a single very wide line and clips the wrapped text.
    /// </summary>
    private void LayoutHeaderText()
    {
        var columnWidths = tlpHeader.GetColumnWidths();

        if (columnWidths.Length < 2)
        {
            return;
        }

        var textColumnWidth = columnWidths[1];

        if (textColumnWidth <= 0)
        {
            return;
        }

        MeasureHeaderLabel(kwlHeading, textColumnWidth);
        MeasureHeaderLabel(kwlMessage, textColumnWidth);
    }

    /// <summary>
    /// Fixes the supplied wrap-label to the header text-column width and the height required to render its
    /// (wrapped) text with the resolved palette font, so the auto-sized header height is deterministic.
    /// </summary>
    /// <param name="label">The wrap-label to size.</param>
    /// <param name="textColumnWidth">The full width of the header text column.</param>
    private static void MeasureHeaderLabel(KryptonWrapLabel label, int textColumnWidth)
    {
        if (!label.Visible)
        {
            return;
        }

        var availableWidth = Math.Max(1, textColumnWidth - label.Margin.Horizontal);

        Size measured = TextRenderer.MeasureText(label.Text, label.Font,
            new Size(availableWidth, int.MaxValue),
            TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl | TextFormatFlags.NoPrefix);

        // Fix the size (a couple of extra pixels guard against GDI clipping the final wrapped line).
        label.AutoSize = false;
        label.Size = new Size(availableWidth, measured.Height + 2);
    }

    /// <summary>
    /// Gives every visible action button a common width (the widest preferred width, but never below the
    /// standard message-box minimum) so the button strip reads as a consistent, aligned row.
    /// </summary>
    private void EqualizeButtons()
    {
        var visible = new List<KryptonButton>();

        foreach (var button in new[] { kbtnButton1, kbtnButton2, kbtnButton3 })
        {
            if (button.Visible)
            {
                visible.Add(button);
            }
        }

        if (visible.Count == 0)
        {
            return;
        }

        var widest = LogicalToDeviceUnits(MINIMUM_BUTTON_WIDTH);

        foreach (KryptonButton button in visible)
        {
            widest = Math.Max(widest, button.GetPreferredSize(Size.Empty).Width);
        }

        foreach (KryptonButton button in visible)
        {
            button.MinimumSize = new Size(widest, button.MinimumSize.Height);
        }
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Resolve the palette fonts up-front so the header is measured with the fonts it will actually be
        // painted with (the title/normal label styles differ from the default control font).
        kwlHeading.UpdateFont();
        kwlMessage.UpdateFont();

        // First layout pass establishes the header text column width.
        PerformLayout();

        // Fix each wrap-label to its true multi-line size and give the buttons a common width, then lay
        // out again so the header reports a deterministic height for the client-size calculation.
        LayoutHeaderText();
        EqualizeButtons();
        PerformLayout();

        ApplyExpandState(_data.Expanded);

        ApplyStartPosition();
    }

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        // Play the associated system sound as the dialog appears, matching message-box behaviour.
        _sound?.Play();

        (AcceptButton as Control)?.Focus();
    }

    #endregion

    #region Event Handlers

    private void kbtnExpander_Click(object sender, EventArgs e) => ApplyExpandState(!_expanded);

    #endregion

    #region Show

    /// <summary>Creates, displays and disposes a <see cref="VisualFoldableDialogForm"/> for the supplied data.</summary>
    /// <param name="data">The data describing the dialog content and behaviour.</param>
    /// <returns>The <see cref="DialogResult"/> produced by the button the user clicked.</returns>
    internal static DialogResult Show(KryptonFoldableDialogData data)
    {
        using var form = new VisualFoldableDialogForm(data);

        return data.Owner != null
            ? form.ShowDialog(data.Owner)
            : form.ShowDialog();
    }

    #endregion
}
