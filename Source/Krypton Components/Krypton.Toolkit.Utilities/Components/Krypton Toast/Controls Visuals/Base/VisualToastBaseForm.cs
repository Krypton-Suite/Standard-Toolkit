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
        FormBorderStyle = showCloseBox ? FormBorderStyle.Fixed3D : FormBorderStyle.None;
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

    #endregion

    #region Implementation

    private void VisualToastNotificationBaseForm_Load(object sender, EventArgs e)
    {
    }

    #endregion
}