#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal partial class VisualToastBaseForm : KryptonForm
{
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
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }

    protected KryptonToastResult ShowToastNotificationResult() => ShowToastNotificationResult(null);

    #endregion

    #region Implementation

    private void VisualToastNotificationBaseForm_Load(object sender, EventArgs e)
    {

    }

    #endregion
}