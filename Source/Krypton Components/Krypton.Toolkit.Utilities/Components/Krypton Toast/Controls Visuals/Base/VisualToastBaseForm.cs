#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal partial class VisualToastBaseForm : KryptonForm
{
    #region Constants

    /// <summary>Logical pixel inset from the working-area edge when auto-positioning a toast.</summary>
    protected const int ToastScreenEdgeMargin = 5;

    /// <summary>Logical pixel inset between toast chrome and the dismiss/action button strip.</summary>
    protected const int ToastButtonEdgePadding = 16;

    #endregion

    #region Instance Fields

    private KryptonToastResult _notificationResult;

    #endregion

    #region Public

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DialogResult DialogResult
    {
        get => base.DialogResult;

        set => base.DialogResult = value;
    }

    /// <summary>Gets or sets the notification result.</summary>
    /// <value>The notification result.</value>
    [Category(@"Behaviour")]
    [Description(@"")]
    [DefaultValue(KryptonToastResult.None)]
    public KryptonToastResult NotificationResult
    {
        get => _notificationResult;

        set => _notificationResult = value;
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualToastBaseForm" /> class.</summary>
    public VisualToastBaseForm()
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _notificationResult = KryptonToastResult.None;

        Text = string.Empty;
    }

    #endregion

    #region Protected

    protected KryptonToastResult ShowToastNotificationResult(IWin32Window? owner)
    {
        var result = _notificationResult;

        switch (result)
        {
            case KryptonToastResult.None:
                DialogResult = DialogResult.None;
                break;
            case KryptonToastResult.Ok:
                break;
            case KryptonToastResult.Cancel:
                break;
            case KryptonToastResult.Abort:
                break;
            case KryptonToastResult.Retry:
                break;
            case KryptonToastResult.Ignore:
                break;
            case KryptonToastResult.Yes:
                break;
            case KryptonToastResult.No:
                break;
            case KryptonToastResult.Close:
                break;
            case KryptonToastResult.Help:
                break;
            case KryptonToastResult.TryAgain:
                break;
            case KryptonToastResult.Continue:
                break;
            case KryptonToastResult.TimeOut:
                break;
            case KryptonToastResult.DoNotShowAgain:
                break;
            default:
                ThrowHelper.ThrowArgumentOutOfRangeException();
                return result;
        }

        return result;
    }

    protected KryptonToastResult ShowToastNotificationResult() => ShowToastNotificationResult(null);

    /// <summary>Converts a logical padding value to device pixels for the monitor hosting this form.</summary>
    /// <param name="logical">Padding in logical (96 DPI) pixels.</param>
    /// <returns>The scaled padding in device pixels.</returns>
    protected int GetScaledPadding(int logical) => LogicalToDeviceUnits(logical);

    /// <summary>
    /// Default bottom-right toast location on the primary working area, with a DPI-scaled edge margin.
    /// </summary>
    /// <returns>A screen location for the toast's top-left corner.</returns>
    protected Point GetDefaultBottomRightLocation()
    {
        var margin = GetScaledPadding(ToastScreenEdgeMargin);
        var workingArea = Screen.PrimaryScreen!.WorkingArea;

        return new Point(workingArea.Width - Width - margin, workingArea.Height - Height - margin);
    }

    /// <summary>
    /// Applies close-box / control-box chrome. When there is no close box the toast is borderless
    /// so <see cref="ApplyBorderlessHeightPadding"/> can add DPI-scaled height compensation.
    /// </summary>
    /// <param name="showCloseBox">Whether the system close box should be shown.</param>
    protected void ApplyCloseBoxChrome(bool showCloseBox)
    {
        CloseBox = showCloseBox;
        ControlBox = showCloseBox;
        FormBorderStyle = showCloseBox ? FormBorderStyle.Sizable : FormBorderStyle.None;

        // Design size is the minimum. AutoScale scales it; a later clamp can only shrink it
        // when the scaled window is larger than the working area.
        if (showCloseBox && MinimumSize.IsEmpty)
        {
            MinimumSize = Size;
        }
    }

    /// <summary>
    /// When the toast is borderless, add DPI-scaled padding to the height so content is not clipped
    /// on high-DPI displays.
    /// </summary>
    protected void ApplyBorderlessHeightPadding()
    {
        if (FormBorderStyle != FormBorderStyle.None)
        {
            return;
        }

        // Compensate for missing non-client chrome with a DPI-aware pad.
        Height += GetScaledPadding(SharedStaticConstants.DEFAULT_PADDING);
    }

    /// <summary>
    /// Applies DPI-scaled insets so dismiss/action buttons are not flush against the form edge.
    /// </summary>
    protected void ApplyScaledButtonStripInsets()
    {
        var pad = GetScaledPadding(ToastButtonEdgePadding);
        var padding = new Padding(pad);

        // KryptonPanel does not reliably inset Dock.Fill children via Padding; form Padding does.
        if (Padding.All < pad)
        {
            // Grow the window so existing content keeps its size after the inset.
            Width += pad * 2;
            Height += pad * 2;
            Padding = padding;
        }

        foreach (var panel in Controls.Find("kpnlButtons", true))
        {
            var minHeight = GetScaledPadding(50) + pad;
            if (panel.Height < minHeight)
            {
                var delta = minHeight - panel.Height;
                panel.Height = minHeight;
                Height += delta;
            }

            foreach (Control child in panel.Controls)
            {
                if (!(child is TableLayoutPanel tlp))
                {
                    continue;
                }

                EnsureTrailingSpacerColumn(tlp, pad);

                foreach (Control cellChild in tlp.Controls)
                {
                    if (cellChild.Anchor == AnchorStyles.Right || cellChild.Anchor == AnchorStyles.Left)
                    {
                        cellChild.Anchor = AnchorStyles.None;
                    }

                    if (cellChild.Margin.All < pad)
                    {
                        cellChild.Margin = padding;
                    }
                }
            }

            panel.PerformLayout();
        }

        PerformLayout();
    }

    /// <summary>
    /// Adds a fixed trailing column so AutoSize dismiss text cannot overflow the right edge.
    /// </summary>
    /// <param name="tlp">Button strip table.</param>
    /// <param name="pad">Spacer width in device pixels.</param>
    private static void EnsureTrailingSpacerColumn(TableLayoutPanel tlp, int pad)
    {
        if (tlp.ColumnCount < 1)
        {
            return;
        }

        var last = tlp.ColumnStyles[tlp.ColumnCount - 1];
        if (last.SizeType == SizeType.Absolute && Math.Abs(last.Width - pad) < 0.5f)
        {
            return;
        }

        tlp.ColumnCount++;
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, pad));
    }

    /// <summary>
    /// Applies borderless height compensation and DPI-scaled button-strip insets.
    /// </summary>
    protected void ApplyToastDpiLayout()
    {
        ApplyBorderlessHeightPadding();
        ApplyScaledButtonStripInsets();
        ApplyToastContentBounds();
    }

    /// <summary>
    /// Widens the toast when a label's text does not fit, then keeps the window on the working area.
    /// Safe to call more than once: a second pass lays out first, so docked labels are not grown twice.
    /// </summary>
    private void ApplyToastContentBounds()
    {
        PerformLayout();

        var desiredWidth = Width;
        GrowLabelsToText(this, ref desiredWidth);

        if (desiredWidth > Width)
        {
            Width = desiredWidth;
        }

        KryptonDialogLayout.EnableResizable(this);
    }

    /// <summary>
    /// Grows <see cref="KryptonLabel"/> controls that clip their text, and reports the form width that contains them.
    /// </summary>
    /// <param name="root">Control tree to walk.</param>
    /// <param name="desiredWidth">Running form width required to show the text.</param>
    private void GrowLabelsToText(Control root, ref int desiredWidth)
    {
        foreach (Control child in root.Controls)
        {
            if (child is KryptonLabel label && label.Visible && !string.IsNullOrEmpty(label.Text))
            {
                var measured = TextRenderer.MeasureText(label.Text, label.Font).Width + label.Margin.Horizontal;
                var overflow = measured - label.Width;

                if (overflow > 2)
                {
                    if (label.Dock != DockStyle.None)
                    {
                        desiredWidth = Math.Max(desiredWidth, Width + overflow);
                    }
                    else
                    {
                        label.Width += overflow;
                        desiredWidth = Math.Max(desiredWidth, label.Right + label.Margin.Right + 8);
                    }
                }
            }

            if (child.HasChildren)
            {
                GrowLabelsToText(child, ref desiredWidth);
            }
        }
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        // Dismiss text (with countdown) is often applied in Show() after Load; re-inset once visible.
        ApplyScaledButtonStripInsets();
        ApplyToastContentBounds();
    }

    #endregion

    #region Implementation

    private void VisualToastNotificationBaseForm_Load(object sender, EventArgs e)
    {
    }

    #endregion
}