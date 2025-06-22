#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

internal partial class VisualToastNotificationNUDUserInputWithProgressBarRtlAwareForm : VisualToastNotificationBaseForm
{
    #region Instance Fields

    private int _time;

    private Timer _timer;

    private readonly KryptonUserInputToastNotificationData _data;

    #endregion

    #region Internal

    internal int UserResponse => Convert.ToInt32(knudUserInput.Value);

    #endregion

    #region Identity

    public VisualToastNotificationNUDUserInputWithProgressBarRtlAwareForm(KryptonUserInputToastNotificationData data)
    {
        InitializeComponent();

        _data = data;

        GotFocus += (sender, args) => knudUserInput.Focus();

        Resize += VisualToastNotificationNumericUpDownUserInputWithProgressBarRtlAwareForm_Resize;

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
        knudUserInput.Maximum = _data.MaximumNumericUpDownValue ?? 100;

        knudUserInput.Minimum = _data.MinimumNumericUpDownValue ?? 0;

        knudUserInput.Value = _data.InitialNumericUpDownValue ?? 0;
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
        switch (_data.NotificationIcon)
        {
            case KryptonToastNotificationIcon.None:
                SetIcon(null);
                break;
            case KryptonToastNotificationIcon.Hand:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Hand_128_x_128);
                break;
            case KryptonToastNotificationIcon.SystemHand:
                SetIcon(GraphicsExtensions.ScaleImage(SystemIcons.Hand.ToBitmap(), 128, 128));
                break;
            case KryptonToastNotificationIcon.Question:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Question_128_x_128);
                break;
            case KryptonToastNotificationIcon.SystemQuestion:
                SetIcon(GraphicsExtensions.ScaleImage(SystemIcons.Question.ToBitmap(), 128, 128));
                break;
            case KryptonToastNotificationIcon.Exclamation:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Warning_128_x_115);
                break;
            case KryptonToastNotificationIcon.SystemExclamation:
                SetIcon(GraphicsExtensions.ScaleImage(SystemIcons.Exclamation.ToBitmap(), 128, 128));
                break;
            case KryptonToastNotificationIcon.Asterisk:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Asterisk_128_x_128);
                break;
            case KryptonToastNotificationIcon.SystemAsterisk:
                SetIcon(GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), 128, 128));
                break;
            case KryptonToastNotificationIcon.Stop:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Stop_128_x_128);
                break;
            case KryptonToastNotificationIcon.Error:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Critical_128_x_128);
                break;
            case KryptonToastNotificationIcon.Warning:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Warning_128_x_115);
                break;
            case KryptonToastNotificationIcon.Information:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Information_128_x_128);
                break;
            case KryptonToastNotificationIcon.Shield:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    SetIcon(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_11_128_x_128);
                }
                else if (OSUtilities.IsWindowsTen)
                {
                    SetIcon(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_10_128_x_128);
                }
                else
                {
                    SetIcon(ToastNotificationImageResources.Toast_Notification_UAC_Shield_Windows_7_and_8_128_x_128);
                }
                break;
            case KryptonToastNotificationIcon.WindowsLogo:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    SetIcon(WindowsLogoImageResources.Windows_11_128_128);
                }
                else if (OSUtilities.IsWindowsEight || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsTen)
                {
                    SetIcon(WindowsLogoImageResources.Windows_8_81_10_128_128);
                }
                else
                {
                    SetIcon(GraphicsExtensions.ScaleImage(SystemIcons.WinLogo.ToBitmap(), new Size(128, 128)));
                }
                break;
            case KryptonToastNotificationIcon.Application:
                SetIcon(GraphicsExtensions.ScaleImage(_data.ApplicationIcon.ToBitmap(), new Size(128, 128)));
                break;
            case KryptonToastNotificationIcon.SystemApplication:
                SetIcon(GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), 128, 128));
                break;
            case KryptonToastNotificationIcon.Ok:
                SetIcon(ToastNotificationImageResources.Toast_Notification_Ok_128_x_128);
                break;
            case KryptonToastNotificationIcon.Custom:
                SetIcon(_data.CustomImage != null
                    ? new Bitmap(_data.CustomImage)
                    : null);
                break;
            case null:
                SetIcon(null);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ShowCloseButton()
    {
        CloseBox = _data.ShowCloseBox ?? false;

        FormBorderStyle = CloseBox ? FormBorderStyle.Fixed3D : FormBorderStyle.FixedSingle;

        ControlBox = _data.ShowCloseBox ?? false;
    }

    private void itbDismiss_Click(object sender, EventArgs e) => Close();

    private void VisualToastNotificationNumericUpDownUserInputWithProgressBarRtlAwareForm_Resize(object? sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Maximized)
        {
            WindowState = FormWindowState.Normal;
        }
    }

    private void VisualToastNotificationNumericUpDownUserInputWithProgressBarRtlAwareForm_Load(object sender, EventArgs e)
    {
        UpdateIcon();

        UpdateLocation();

        ShowCloseButton();

        kbtnDismiss.Text = KryptonManager.Strings.ToastNotificationStrings.Dismiss;

        itbDismiss.Text = KryptonManager.Strings.ToastNotificationStrings.Dismiss;

        _timer.Start();
    }

    private void VisualToastNotificationNumericUpDownUserInputWithProgressBarRtlAwareForm_LocationChanged(object sender, EventArgs e) => UpdateLocation();

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


    public static decimal ShowToastNotification(KryptonUserInputToastNotificationData data)
    {
        var owner = data.ToastHost ?? FromHandle(PI.GetActiveWindow());

        using var toast = new VisualToastNotificationNUDUserInputWithProgressBarRtlAwareForm(data);

        if (owner != null)
        {
            toast.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return toast.ShowDialog(owner!) == DialogResult.OK ? toast.UserResponse : 0;
        }
        else
        {
            return toast.ShowDialog() == DialogResult.OK ? toast.UserResponse : 0;
        }
    }

    #endregion
}