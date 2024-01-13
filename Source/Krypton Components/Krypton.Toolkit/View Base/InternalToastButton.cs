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

        private VisualToastNotificationBasicForm? _basicToast;

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

        public VisualToastNotificationBasicForm? BasicToastForm
        {
            get => _basicToast;

            set => _basicToast = value;
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="InternalToastButton" /> class.</summary>
        public InternalToastButton()
        {
            _isActionButton = false;

            _isDismissButton = false;

            _basicToast = null;
        }

        #endregion

        #region Protected

        protected override void OnPaint(PaintEventArgs? e)
        {
            if (_isDismissButton)
            {
                _isActionButton = false;

                if (_basicToast != null)
                {
                    _basicToast.AcceptButton = this;
                }
            }

            base.OnPaint(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_isActionButton && _basicToast != null /*&& _basicToast.ActionButtonCommand != null*/)
            {
                //_basicToast.ActionButtonCommand.PerformExecute();
            }

            if (_isDismissButton)
            {
                _basicToast?.Close();
            }

            base.OnClick(e);
        }

        #endregion

        #region Public Overrides

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override IKryptonCommand? KryptonCommand { get; set; }

        #endregion
    }
}