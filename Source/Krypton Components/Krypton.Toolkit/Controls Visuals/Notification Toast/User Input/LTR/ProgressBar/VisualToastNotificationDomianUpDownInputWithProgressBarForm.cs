#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit
{
    internal partial class VisualToastNotificationDomianUpDownInputWithProgressBarForm : VisualToastNotificationBaseForm
    {
        #region Instance Fields

        private int _time;

        private Timer _timer;

        private readonly KryptonUserInputToastNotificationData _data;

        #endregion

        #region Internal

        internal string UserResponse => kdudUserInput.Text;

        #endregion

        #region Identity

        public VisualToastNotificationDomianUpDownInputWithProgressBarForm(KryptonUserInputToastNotificationData data)
        {
            InitializeComponent();

            _data = data;

            GotFocus += (sender, args) => kdudUserInput.Focus();

            Resize += VisualToastNotificationDomianUpDownInputWithProgressBarForm_Resize;

            CommonToastNotificationFunctions.UpdateBorderColors(this);
        }

        #endregion

        #region Implementation

        private void UpdateInitialValues()
        {
            // Set initial date and time values
            if (_data.UserInputList.Count > 0)
            {
                foreach (var item in _data.UserInputList)
                {
                    kdudUserInput.Items.Add(item);
                }

                kdudUserInput.SelectedIndex = _data.SelectedIndex ?? 0;
            }
        }

       private void VisualToastNotificationDomianUpDownInputWithProgressBarForm_Resize(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void VisualToastNotificationDomianUpDownInputWithProgressBarForm_Load(object sender, EventArgs e)
        {
            CommonToastNotificationFunctions.UpdateIcon(pbxNotificationIcon);

            CommonToastNotificationFunctions.UpdateLocation(this);

            CommonToastNotificationFunctions.ShowCloseButton(this);

            kbtnDismiss.Text = KryptonManager.Strings.ToastNotificationStrings.Dismiss;

            _timer.Start();
        }

        public new DialogResult ShowDialog()
        {
            TopMost = _data.TopMost ?? true;

            CommonToastNotificationFunctions.UpdateNotificationText(kwlNotificationTitle, null, _data.NotificationTitle, _data.NotificationContent);

            CommonToastNotificationFunctions.UpdateIcon(pbxNotificationIcon);

            UpdateInitialValues();

            CommonToastNotificationFunctions.UpdateLocation(this);

            if (_data.CountDownSeconds != 0)
            {
                kpbCountDown.Maximum = _data.CountDownSeconds ?? 100;

                kpbCountDown.Value = kpbCountDown.Maximum;

                kpbCountDown.Text = $@"{_data.CountDownSeconds - _time}";

                _timer = new Timer();

                _timer.Interval = _data.CountDownTimerInterval ?? 1000;

                _timer.Tick += (sender, args) =>
                {
                    _time++;

                    kpbCountDown.Value -= 1;

                    kpbCountDown.Text = $@"{_data.CountDownSeconds - _time}";

                    if (kpbCountDown.Value == kpbCountDown.Minimum)
                    {
                        _timer.Stop();

                        Close();
                    }
                };
            }

            return base.ShowDialog();
        }

        public new DialogResult ShowDialog(IWin32Window owner)
        {
            TopMost = _data.TopMost ?? true;

            CommonToastNotificationFunctions.UpdateNotificationText(kwlNotificationTitle, null, _data.NotificationTitle, _data.NotificationContent);

            CommonToastNotificationFunctions.UpdateIcon(pbxNotificationIcon);

            UpdateInitialValues();

            if (_data.CountDownSeconds != 0)
            {
                kpbCountDown.Maximum = _data.CountDownSeconds ?? 100;

                kpbCountDown.Value = kpbCountDown.Maximum;

                kpbCountDown.Text = $@"{_data.CountDownSeconds - _time}";

                _timer = new Timer();

                _timer.Interval = _data.CountDownTimerInterval ?? 1000;

                _timer.Tick += (sender, args) =>
                {
                    _time++;

                    kpbCountDown.Value -= 1;

                    kpbCountDown.Text = $@"{_data.CountDownSeconds - _time}";

                    if (kpbCountDown.Value == kpbCountDown.Minimum)
                    {
                        _timer.Stop();

                        Close();
                    }
                };
            }

            return base.ShowDialog(owner);
        }

        internal static string ShowNotification(KryptonUserInputToastNotificationData data)
        {
            var owner = data.ToastHost ?? FromHandle(PI.GetActiveWindow());

            using var toast = new VisualToastNotificationDomianUpDownInputWithProgressBarForm(data);

            if (owner != null)
            {
                toast.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return toast.ShowDialog(owner!) == DialogResult.OK ? toast.UserResponse : GlobalStaticValues.DEFAULT_EMPTY_STRING;
            }
            else
            {
                return toast.ShowDialog() == DialogResult.OK ? toast.UserResponse : GlobalStaticValues.DEFAULT_EMPTY_STRING;
            }
        }

        #endregion
    }
}
