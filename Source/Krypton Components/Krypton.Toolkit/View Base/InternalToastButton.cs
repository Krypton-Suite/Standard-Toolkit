#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
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

        public bool IsActionButton
        {
            get => _isActionButton;

            set
            {
                _isActionButton = value;

                Invalidate();
            }
        }

        public bool IsDismissButton
        {
            get => _isDismissButton;

            set
            {
                _isDismissButton = value;

                Invalidate();
            }
        }

        public string ProcessPath
        {
            get => _processPath;

            set
            {
                _processPath = value;

                Invalidate();
            }
        }

        /// <summary>Gets or sets the basic toast form.</summary>
        /// <value>The basic toast form.</value>
        [Category(@"Data")]
        [Description(@"")]
        [DefaultValue(null)]
        public VisualToastNotificationBaseForm? BaseToastForm
        {
            get => _owner;

            set => _owner = value;
        }

        /// <summary>
        /// Gets or sets the notification result.
        /// </summary>
        /// <value>
        /// The notification result.
        /// </value>
        [Category(@"Behavior")]
        [Description(@"")]
        [DefaultValue(KryptonToastNotificationResult.None)]
        public KryptonToastNotificationResult NotificationResult
        {
            get => _notificationResult;

            set => _notificationResult = value;
        }

        /// <summary>
        /// Gets or sets the value returned to the parent form when the button is clicked.
        /// </summary>
        [Browsable(false)]
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

            _owner = null;

            _notificationResult = KryptonToastNotificationResult.None;
        }

        #endregion

        #region Protected

        protected override void OnPaint(PaintEventArgs? e)
        {
            if (_isDismissButton)
            {
                _isActionButton = false;

                if (_owner != null)
                {
                    _owner.AcceptButton = this;
                }
            }

            base.OnPaint(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_isActionButton && _owner != null /*&& _basicToast.ActionButtonCommand != null*/)
            {
                //_basicToast.ActionButtonCommand.PerformExecute();
            }

            if (_isDismissButton)
            {
                _owner?.Close();
            }

            base.OnClick(e);
        }

        #endregion

        #region Public Overrides

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
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
                ExceptionHandler.CaptureException(e);
            }
        }

        #endregion
    }
}