#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit
{
    public partial class VisualToastNotificationTextBoxUserInputForm : KryptonForm
    {
        #region Instance Fields

        private int _time;

        private Timer _timer;

        private readonly KryptonUserInputToastNotificationData _data;

        #endregion

        #region Internal

        internal string UserResponse => ktxtUserInput.Text ?? string.Empty;

        #endregion

        #region Identity

        public VisualToastNotificationTextBoxUserInputForm(KryptonUserInputToastNotificationData data)
        {
            InitializeComponent();
        }

        #endregion

        internal string ShowNotification(KryptonUserInputToastNotificationData data)
        {
            throw new NotImplementedException();
        }
    }
}
