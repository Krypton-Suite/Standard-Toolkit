#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

internal partial class VisualToastNotificationBasicForm : VisualToastNotificationBaseForm
{
    #region Instance Fields

    private int _time;

    private Timer _timer;

    private SoundPlayer? _soundPlayer;

    private PaletteBase _palette;

    private readonly KryptonBasicToastNotificationData _basicToastNotificationData;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualToastNotificationBasicForm" /> class.</summary>
    /// <param name="data">The data.</param>
    public VisualToastNotificationBasicForm(KryptonBasicToastNotificationData data)
    {
        _basicToastNotificationData = data;

        InitializeComponent();

        GotFocus += VisualToastNotificationBasicForm_GotFocus;

        Resize += VisualToastNotificationBasicForm_Resize;

        LocationChanged += VisualToastNotificationBasicForm_LocationChanged;

        DoubleBuffered = true;

        UpdateBorderColors();

        /* FadeValues disabled and moved to extended until proven stable. Further development in V100
        UpdateFadeValues();
        */

        UpdateFonts();

        ShowDoNotShowAgainOption();
    }

    #endregion

    #region Public

    internal bool ReturnValue => kchkDoNotShowAgain.Checked;

    internal CheckState ReturnCheckBoxStateValue => kchkDoNotShowAgain.CheckState;

    #endregion

    #region Implementation

    private void UpdateText()
    {
        krtbNotificationContentText.Text = _basicToastNotificationData.NotificationContent ?? string.Empty;

        klblHeader.Text = _basicToastNotificationData.NotificationTitle;

        klblHeader.StateCommon.ShortText.TextH = _basicToastNotificationData.NotificationTitleAlignmentH ?? PaletteRelativeAlign.Center;

        klblHeader.StateCommon.ShortText.TextV = _basicToastNotificationData.NotificationTitleAlignmentV ?? PaletteRelativeAlign.Center;
    }

    private void UpdateBorderColors()
    {
        StateCommon!.Border.Color1 = _basicToastNotificationData.BorderColor1 ?? GlobalStaticValues.EMPTY_COLOR;

        StateCommon.Border.Color2 = _basicToastNotificationData.BorderColor2 ?? GlobalStaticValues.EMPTY_COLOR;
    }

    /* FadeValues disabled and moved to extended until proven stable. Further development in V100
    private void UpdateFadeValues() => FadeValues.FadingEnabled = _basicToastNotificationData.UseFade;
    */

    private void UpdateFonts()
    {
        krtbNotificationContentText.StateCommon.Content.Font =
             _basicToastNotificationData.NotificationContentFont ?? KryptonManager.CurrentGlobalPalette.BaseFont;

        if (_basicToastNotificationData.NotificationTitleFont != null)
        {
            krtbNotificationContentText.InputControlStyle = InputControlStyle.PanelClient;

            klblHeader.StateCommon.ShortText.Font =
                _basicToastNotificationData.NotificationTitleFont ?? _palette.Header1ShortFont;
        }
        else
        {
            klblHeader.LabelStyle = LabelStyle.TitleControl;
        }
    }

    private void UpdateIcon()
    {
        var bitmap = GraphicsExtensions.GetToastNotificationBitmap(
            _basicToastNotificationData.NotificationIcon,
            null,
            _basicToastNotificationData.CustomImage,
            new Size(128, 128));

        SetIcon(bitmap);
    }

    private void UpdateDoNotShowAgainOptionChecked() =>
        kchkDoNotShowAgain.Checked = _basicToastNotificationData.IsDoNotShowAgainOptionChecked;

    private void UpdateDoNotShowAgainOptionCheckState() => kchkDoNotShowAgain.CheckState =
        _basicToastNotificationData.DoNotShowAgainOptionCheckState ?? CheckState.Unchecked;

    private void SetIcon(Bitmap? image) => pbxIcon.Image = image;

    private void UpdateLocation() =>
        //Once loaded, position the form, or position it to the bottom left of the screen with added padding
        Location = _basicToastNotificationData.NotificationLocation ?? new Point(Screen.PrimaryScreen!.WorkingArea.Width - Width - 5,
            Screen.PrimaryScreen.WorkingArea.Height - Height - 5);

    private void ReportToastLocation() => klblToastLocation.Text = _basicToastNotificationData.ReportToastLocation ? $"Location: X: {Location.X}, Y: {Location.Y}" : string.Empty;

    private void VisualToastNotificationBasicForm_Load(object sender, EventArgs e)
    {
        UpdateSizing();

        UpdateLocation();

        ReportToastLocation();

        ShowCloseButton();

        _timer?.Start();

        _soundPlayer?.Play();
    }

    private void UpdateSizing()
    {
        if (FormBorderStyle == FormBorderStyle.None)
        {
            // Add some height, if form border style equals 'None'

            var width = Size.Width;

            // ToDo: Use scaling here, to support larger screens

            var height = Size.Height + GlobalStaticValues.DEFAULT_PADDING;

            Size = new Size(width, height);
        }
    }

    private void VisualToastNotificationBasicForm_Resize(object? sender, EventArgs e)
    {
        WindowState = WindowState switch
        {
            FormWindowState.Maximized => FormWindowState.Normal,
            _ => WindowState
        };
    }

    private void VisualToastNotificationBasicForm_GotFocus(object? sender, EventArgs e) => kbtnDismiss.Focus();

    private void VisualToastNotificationBasicForm_LocationChanged(object? sender, EventArgs e)
    {
        if (_basicToastNotificationData.ReportToastLocation)
        {
            ReportToastLocation();
        }
    }

    private void kbtnDismiss_Click(object sender, EventArgs e) => Close();

    private void ShowCloseButton()
    {
        CloseBox = _basicToastNotificationData.ShowCloseBox ?? false;

        FormBorderStyle = CloseBox ? FormBorderStyle.Fixed3D : FormBorderStyle.FixedSingle;

        ControlBox = _basicToastNotificationData.ShowCloseBox ?? false;
    }

    private void ShowDoNotShowAgainOption()
    {
        kchkDoNotShowAgain.Visible = _basicToastNotificationData.ShowDoNotShowAgainOption ?? false;

        kchkDoNotShowAgain.Checked = _basicToastNotificationData.IsDoNotShowAgainOptionChecked;

        kchkDoNotShowAgain.CheckState = _basicToastNotificationData.DoNotShowAgainOptionCheckState ?? CheckState.Unchecked;

        kchkDoNotShowAgain.ThreeState = _basicToastNotificationData.UseDoNotShowAgainOptionThreeState ?? false;

        kchkDoNotShowAgain.Text = _basicToastNotificationData.OptionalCheckBoxText ?? KryptonManager.Strings.CustomStrings.DoNotShowAgain;
    }

    private void itbDismiss_Click(object sender, EventArgs e) => Close();

    #region Show

    public new void Show()
    {
        TopMost = _basicToastNotificationData.TopMost ?? true;

        UpdateText();

        UpdateIcon();

        if (_basicToastNotificationData.CountDownSeconds != 0)
        {
            kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

            itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

            _timer = new Timer();

            _timer.Interval = _basicToastNotificationData.CountDownTimerInterval ?? 1000;

            _timer.Tick += (sender, args) =>
            {
                _time++;

                kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

                itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

                if (_time == _basicToastNotificationData.CountDownSeconds)
                {
                    _timer.Stop();

                    Close();
                }
            };
        }

        base.Show();
    }

    public new DialogResult ShowDialog()
    {
        TopMost = _basicToastNotificationData.TopMost ?? true;

        UpdateText();

        UpdateIcon();

        if (_basicToastNotificationData.IsDoNotShowAgainOptionChecked)
        {
            UpdateDoNotShowAgainOptionChecked();
        }

        if (_basicToastNotificationData.DoNotShowAgainOptionCheckState != null)
        {
            UpdateDoNotShowAgainOptionCheckState();
        }

        if (_basicToastNotificationData.CountDownSeconds != 0)
        {
            kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

            itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

            _timer = new Timer();

            _timer.Interval = _basicToastNotificationData.CountDownTimerInterval ?? 1000;

            _timer.Tick += (sender, args) =>
            {
                _time++;

                kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

                itbDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss} ({_basicToastNotificationData.CountDownSeconds - _time})";

                if (_time == _basicToastNotificationData.CountDownSeconds)
                {
                    _timer.Stop();

                    Close();
                }
            };
        }

        return base.ShowDialog();
    }

    #endregion

    #region Internal Show Methods

    internal static bool InternalShowWithBooleanReturnValue(KryptonBasicToastNotificationData toastNotificationData)
    {
        using var toast = new VisualToastNotificationBasicForm(toastNotificationData);

        if (toast.ShowDialog() == DialogResult.OK)
        {
            return toast.ReturnValue;
        }
        else
        {
            return false;
        }
    }

    internal static CheckState InternalShowWithCheckStateReturnValue(
        KryptonBasicToastNotificationData toastNotificationData)
    {
        using var toast = new VisualToastNotificationBasicForm(toastNotificationData);

        return toast.ShowDialog() == DialogResult.OK
            ? toast.ReturnCheckBoxStateValue
            : CheckState.Unchecked;
    }

    internal static void InternalShow(KryptonBasicToastNotificationData toastNotificationData)
    {
        var toast = new VisualToastNotificationBasicForm(toastNotificationData);

        toast.Show();
    }

    #endregion

    #endregion

    #region Protected Overrides

    protected override void OnLoad(EventArgs e)
    {
        if (_basicToastNotificationData.DoNotShowAgainOptionCheckState == CheckState.Checked || _basicToastNotificationData.IsDoNotShowAgainOptionChecked)
        {
            Hide();
        }

        base.OnLoad(e);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _timer?.Stop();

        _timer?.Dispose();

        _soundPlayer?.Stop();

        _soundPlayer?.Dispose();

        _soundPlayer = null;

        base.OnFormClosing(e);
    }

    #endregion
}