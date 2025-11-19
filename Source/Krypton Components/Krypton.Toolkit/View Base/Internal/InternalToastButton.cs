#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class InternalToastButton : KryptonButton
{
    #region Instance Fields

    private bool _isActionButton;

    private bool _isDismissButton;

    private string _processPath;

    private VisualToastNotificationBaseForm? _owner;

    private KryptonToastNotificationResult _notificationResult;

    #endregion

    #region Public

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsActionButton
    {
        get => _isActionButton;

        set
        {
            _isActionButton = value;

            Anchor = AnchorStyles.Left;

            Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDismissButton
    {
        get => _isDismissButton;

        set
        {
            _isDismissButton = value;

            Anchor = AnchorStyles.Right;

            Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ProcessPath
    {
        get => _processPath;

        set => _processPath = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VisualToastNotificationBaseForm? Owner
    {
        get => _owner;

        set => _owner = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonToastNotificationResult NotificationResult
    {
        get => _notificationResult;

        set => _notificationResult = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new AnchorStyles Anchor
    {
        get => base.Anchor;

        set => base.Anchor = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DialogResult DialogResult
    {
        get => base.DialogResult;

        set => base.DialogResult = value;
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="InternalToastButton" /> class.</summary>
    public InternalToastButton()
    {
        _isActionButton = false;

        _isDismissButton = false;

        _processPath = string.Empty;

        _owner = null;

        _notificationResult = KryptonToastNotificationResult.None;

        Text = @"{0} ({1})";

        Anchor = AnchorStyles.Right;

        AutoSize = true;

        // Use 10 pixels for padding
        Margin = new Padding(GlobalStaticValues.DEFAULT_PADDING);
    }

    #endregion

    #region Protected

    protected override void OnPaint(PaintEventArgs? e)
    {
        if (_isDismissButton)
        {
            _isActionButton = false;

            if (_owner is not null)
            {
                _owner.AcceptButton = this;
            }
        }

        base.OnPaint(e);
    }

    protected override void OnClick(EventArgs e)
    {
        if (_isDismissButton && _owner != null)
        {
            _owner.Close();
        }

        if (_isActionButton && _owner != null)
        {
            if (!string.IsNullOrEmpty(_processPath))
            {
                LaunchProcess(_processPath);
            }
            else
            {
                _owner.Close();
            }
        }

        base.OnClick(e);
    }

    #endregion

    #region Public Overrides

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IKryptonCommand? KryptonCommand { get; set; }

    #endregion

    #region Protected Overrides

    protected override void OnMouseClick(MouseEventArgs e)
    {
        if (_isDismissButton && _owner != null)
        {
            _owner.Close();
        }

        if (_isActionButton && _owner != null)
        {
            if (!string.IsNullOrEmpty(_processPath))
            {
                LaunchProcess(_processPath);
            }
            else
            {
                _owner.Close();
            }
        }

        base.OnMouseClick(e);
    }

    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        base.OnNeedPaint(sender, e);
    }

    #endregion

    #region Implementation

    private void LaunchProcess(string processPath)
    {
        try
        {
            Process.Start(processPath);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    #endregion
}