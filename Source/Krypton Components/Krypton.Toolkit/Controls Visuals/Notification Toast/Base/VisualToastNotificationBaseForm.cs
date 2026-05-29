#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualToastNotificationBaseForm : KryptonForm
{
    #region Instance Fields

    private KryptonToastNotificationResult _notificationResult;

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
    [DefaultValue(KryptonToastNotificationResult.None)]
    public KryptonToastNotificationResult NotificationResult
    {
        get => _notificationResult;

        set => _notificationResult = value;
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualToastNotificationBaseForm" /> class.</summary>
    public VisualToastNotificationBaseForm()
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _notificationResult = KryptonToastNotificationResult.None;

        Text = string.Empty;
    }

    #endregion

    #region Protected

    protected KryptonToastNotificationResult ShowToastNotificationResult(IWin32Window? owner)
    {
        var result = _notificationResult;

        switch (result)
        {
            case KryptonToastNotificationResult.None:
                DialogResult = DialogResult.None;
                break;
            case KryptonToastNotificationResult.Ok:
                break;
            case KryptonToastNotificationResult.Cancel:
                break;
            case KryptonToastNotificationResult.Abort:
                break;
            case KryptonToastNotificationResult.Retry:
                break;
            case KryptonToastNotificationResult.Ignore:
                break;
            case KryptonToastNotificationResult.Yes:
                break;
            case KryptonToastNotificationResult.No:
                break;
            case KryptonToastNotificationResult.Close:
                break;
            case KryptonToastNotificationResult.Help:
                break;
            case KryptonToastNotificationResult.TryAgain:
                break;
            case KryptonToastNotificationResult.Continue:
                break;
            case KryptonToastNotificationResult.TimeOut:
                break;
            case KryptonToastNotificationResult.DoNotShowAgain:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }

    protected KryptonToastNotificationResult ShowToastNotificationResult() => ShowToastNotificationResult(null);

    #endregion

    #region Implementation

    private void VisualToastNotificationBaseForm_Load(object sender, EventArgs e)
    {

    }

    #endregion
}