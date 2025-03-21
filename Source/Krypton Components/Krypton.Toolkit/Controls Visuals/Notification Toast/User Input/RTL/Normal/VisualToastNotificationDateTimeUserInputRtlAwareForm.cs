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
    internal partial class VisualToastNotificationDateTimeUserInputRtlAwareForm : VisualToastNotificationBaseForm
    {
        #region Instance Fields

        private int _time;

        private Timer _timer;

        private readonly KryptonUserInputToastNotificationData _data;

        #endregion

        #region Internal

        internal DateTime UserResponse => kdtpUserInput.Value;

        #endregion

        public VisualToastNotificationDateTimeUserInputRtlAwareForm(KryptonUserInputToastNotificationData data)
        {
            InitializeComponent();

            _data = data;

            GotFocus += (sender, args) => kdtpUserInput.Focus();

            Resize += VisualToastNotificationDateTimeUserInputRtlAwareForm_Resize;

            UpdateBorderColors();
        }

        #region Implementation

        private void UpdateBorderColors()
        {
            StateCommon!.Border.Color1 = _data.BorderColor1 ?? GlobalStaticValues.EMPTY_COLOR;

            StateCommon!.Border.Color2 = _data.BorderColor2 ?? GlobalStaticValues.EMPTY_COLOR;
        }

        private void UpdateText()
        {
            //kwlNotificationTitle.Text = _data.NotificationTitle;

            //kwlNotificationMessage.Text = _data.NotificationContent;
        }

        private void UpdateInitialValues()
        {
            // Set initial date and time values
            kdtpUserInput.Value = _data.InitialDateTimeValue ?? GlobalStaticValues.DEFAULT_DATE_TIME_VALUE;

            kdtpUserInput.Format = _data.DateTimeFormat ?? DateTimePickerFormat.Long;

            kdtpUserInput.CustomFormat = _data.CustomDateTimeFormat ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;

            kdtpUserInput.MaxDate = _data.MaximumDateTimeValue ?? DateTime.MaxValue;

            kdtpUserInput.MinDate = _data.MinimumDateTimeValue ?? DateTime.MinValue;
        }

        private void itbDismiss_Click(object sender, EventArgs e) => Close();

        private void VisualToastNotificationDateTimeUserInputRtlAwareForm_Load(object sender, EventArgs e)
        {
            CommonToastNotificationFunctions.UpdateUserInputToastIcon(pbxNotificationIcon);

            CommonToastNotificationFunctions.UpdateLocation(this);

            CommonToastNotificationFunctions.ShowCloseButton(this);

            _timer.Start();
        }

        private void ReportToastLocation() { }

        private void VisualToastNotificationDateTimeUserInputRtlAwareForm_LocationChanged(object sender, EventArgs e)
        {
            if (_data.ReportToastLocation)
            {
                ReportToastLocation();
            }
        }

        private void VisualToastNotificationDateTimeUserInputRtlAwareForm_Resize(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        public new DialogResult ShowDialog()
        {
            TopMost = _data.TopMost ?? true;

            UpdateText();

            CommonToastNotificationFunctions.UpdateUserInputToastIcon(pbxNotificationIcon);

            UpdateInitialValues();

            CommonToastNotificationFunctions.UpdateLocation(this);

            if (_data.CountDownSeconds != 0)
            {
                kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                _timer = new Timer();

                _timer.Interval = _data.CountDownTimerInterval ?? 1000;

                _timer.Tick += (sender, args) =>
                {
                    _time++;

                    kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                    itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                    if (_time == _data.CountDownSeconds)
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

            UpdateText();

            CommonToastNotificationFunctions.UpdateUserInputToastIcon(pbxNotificationIcon);

            UpdateInitialValues();

            if (_data.CountDownSeconds != 0)
            {
                kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                _timer = new Timer();

                _timer.Interval = _data.CountDownTimerInterval ?? 1000;

                _timer.Tick += (sender, args) =>
                {
                    _time++;

                    kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                    itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_data.CountDownSeconds - _time})";

                    if (_time == _data.CountDownSeconds)
                    {
                        _timer.Stop();

                        Close();
                    }
                };
            }

            return base.ShowDialog(owner);
        }

        internal static DateTime ShowNotification(KryptonUserInputToastNotificationData data)
        {
            var owner = data.ToastHost ?? FromHandle(PI.GetActiveWindow());

            using var toast = new VisualToastNotificationDateTimeUserInputRtlAwareForm(data);

            if (owner != null)
            {
                toast.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return toast.ShowDialog(owner!) == DialogResult.OK ? toast.UserResponse : GlobalStaticValues.DEFAULT_DATE_TIME_VALUE;
            }
            else
            {
                return toast.ShowDialog() == DialogResult.OK ? toast.UserResponse : GlobalStaticValues.DEFAULT_DATE_TIME_VALUE;
            }
        }

        #endregion
    }
}
