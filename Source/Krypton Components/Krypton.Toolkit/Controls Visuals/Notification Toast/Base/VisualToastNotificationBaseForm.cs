#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
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
            InitializeComponent();

            _notificationResult = KryptonToastNotificationResult.None;

            Text = string.Empty;
        }

        #endregion
    }
}