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
    internal partial class VisualToastNotificationComboBoxUserInputForm : KryptonForm
    {
        #region Instance Fields

        private KryptonUserInputToastNotificationData _data;

        #endregion

        #region Internal

        internal string UserResponse => kcmbUserInput.Text ?? string.Empty;

        #endregion

        #region Identity

        public VisualToastNotificationComboBoxUserInputForm(KryptonUserInputToastNotificationData data)
        {
            InitializeComponent();

            _data = data;

            UpdateText();

            UpdateIcon();
        }

        #endregion

        #region Implementation

        private void UpdateText()
        {

        }

        private void UpdateIcon()
        {
            switch (_data.NotificationIcon)
            {
                case KryptonToastNotificationIcon.None:
                    break;
                case KryptonToastNotificationIcon.Hand:
                    break;
                case KryptonToastNotificationIcon.SystemHand:
                    break;
                case KryptonToastNotificationIcon.Question:
                    break;
                case KryptonToastNotificationIcon.SystemQuestion:
                    break;
                case KryptonToastNotificationIcon.Exclamation:
                    break;
                case KryptonToastNotificationIcon.SystemExclamation:
                    break;
                case KryptonToastNotificationIcon.Asterisk:
                    break;
                case KryptonToastNotificationIcon.SystemAsterisk:
                    break;
                case KryptonToastNotificationIcon.Stop:
                    break;
                case KryptonToastNotificationIcon.Error:
                    break;
                case KryptonToastNotificationIcon.Warning:
                    break;
                case KryptonToastNotificationIcon.Information:
                    break;
                case KryptonToastNotificationIcon.Shield:
                    break;
                case KryptonToastNotificationIcon.WindowsLogo:
                    break;
                case KryptonToastNotificationIcon.Application:
                    break;
                case KryptonToastNotificationIcon.SystemApplication:
                    break;
                case KryptonToastNotificationIcon.Ok:
                    break;
                case KryptonToastNotificationIcon.Custom:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public new DialogResult ShowDialog(IWin32Window owner)
        {
            return base.ShowDialog(owner);
        }

        internal static string ShowNotification(KryptonUserInputToastNotificationData data)
        {
            IWin32Window owner = data.Owner ?? FromHandle(PI.GetActiveWindow());

            using var toast = new VisualToastNotificationComboBoxUserInputForm(data);

            //toast.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return toast.ShowDialog(owner) == DialogResult.OK ? toast.UserResponse : string.Empty;
        }

        #endregion
    }
}
