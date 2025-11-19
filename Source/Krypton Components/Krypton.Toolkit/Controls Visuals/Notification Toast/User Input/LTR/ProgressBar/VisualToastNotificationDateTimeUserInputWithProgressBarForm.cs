#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

internal partial class VisualToastNotificationDateTimeUserInputWithProgressBarForm : VisualToastNotificationBaseForm
{
    #region Instance Fields

    private int _time;

    private Timer _timer;

    private readonly KryptonUserInputToastNotificationData _data;

    #endregion

    #region Internal

    internal DateTime UserResponse => kdtpUserInput.Value;

    #endregion

    #region Identity

    public VisualToastNotificationDateTimeUserInputWithProgressBarForm(KryptonUserInputToastNotificationData data)
    {
        InitializeComponent();

        _data = data;

        GotFocus += (sender, args) => kdtpUserInput.Focus();

        Resize += VisualToastNotificationDateTimeUserInputWithProgressBarForm_Resize;

        UpdateBorderColors();
    }

    #endregion

    #region Implementation

    private void UpdateBorderColors()
    {
        StateCommon!.Border.Color1 = _data.BorderColor1 ?? GlobalStaticValues.EMPTY_COLOR;

        StateCommon!.Border.Color2 = _data.BorderColor2 ?? GlobalStaticValues.EMPTY_COLOR;
    }

    private void UpdateText()
    {
        klblHeader.Text = _data.NotificationTitle ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;

        krtbNotificationContentText.Text = _data.NotificationContent ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
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

    private void SetIcon(Bitmap? image) => pbxNotificationIcon.Image = image;

    private void UpdateLocation()
    {
        //Once loaded, position the form, or position it to the bottom left of the screen with added padding
        Location = _data.NotificationLocation ?? new Point(Screen.PrimaryScreen!.WorkingArea.Width - Width - 5,
            Screen.PrimaryScreen.WorkingArea.Height - Height - 5);
    }

    private void UpdateIcon()
    {
        var bitmap = GraphicsExtensions.GetToastNotificationBitmap(
            _data.NotificationIcon,
            _data.ApplicationIcon,
            _data.CustomImage,
            new Size(128, 128));

        SetIcon(bitmap);
    }

    private void VisualToastNotificationDateTimeUserInputWithProgressBarForm_Resize(object? sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Maximized)
        {
            WindowState = FormWindowState.Normal;
        }
    }

    private void VisualToastNotificationDateTimeUserInputWithProgressBarForm_Load(object sender, EventArgs e)
    {
        UpdateIcon();

        UpdateLocation();

        ShowCloseButton();

        kbtnDismiss.Text = KryptonManager.Strings.ToastNotificationStrings.Dismiss;

        _timer.Start();
    }

    private void ShowCloseButton()
    {
        CloseBox = _data.ShowCloseBox ?? false;

        FormBorderStyle = CloseBox ? FormBorderStyle.Fixed3D : FormBorderStyle.FixedSingle;

        ControlBox = _data.ShowCloseBox ?? false;
    }

    public new DialogResult ShowDialog()
    {
        TopMost = _data.TopMost ?? true;

        UpdateText();

        UpdateIcon();

        UpdateInitialValues();

        UpdateLocation();

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

        UpdateText();

        UpdateIcon();

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

    internal static DateTime ShowNotification(KryptonUserInputToastNotificationData data)
    {
        var owner = data.ToastHost ?? FromHandle(PI.GetActiveWindow());

        using var toast = new VisualToastNotificationDateTimeUserInputWithProgressBarForm(data);

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