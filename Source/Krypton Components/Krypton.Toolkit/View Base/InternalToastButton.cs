#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
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

        private VisualToastForm? _owner;

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

        public VisualToastForm? Owner
        {
            get => _owner;

            set => _owner = value;
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="InternalToastButton" /> class.</summary>
        public InternalToastButton()
        {
            _isActionButton = false;

            _isDismissButton = false;

            _owner = null;
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
            if (_isActionButton && _owner != null && _owner.ActionButtonCommand != null)
            {
                _owner.ActionButtonCommand.PerformExecute();
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
    }
}