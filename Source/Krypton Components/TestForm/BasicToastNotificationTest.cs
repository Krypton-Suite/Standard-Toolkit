#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class BasicToastNotificationTest : KryptonForm
{
    #region Instance Fields

    private bool _showCloseBox;
    private bool _topMost;
    private bool _useFade;
    private bool _reportToastLocation;
    private bool _showDoNotShowAgainOption;
    private bool _useRtlReading;
    private Color _borderColor1;
    private Color _borderColor2;
    private PaletteRelativeAlign _titleAlignmentH;
    private PaletteRelativeAlign _titleAlignmentV;
    private Font _contentFont;
    private Font? _titleFont;
    private int _countDownSeconds;
    private KryptonToastNotificationIcon? _notificationIcon;
    private string _notificationTitleText;
    private string _notificationContentText;

    #endregion

    public BasicToastNotificationTest()
    {
        InitializeComponent();
    }

    private void kbtnShow_Click(object sender, EventArgs e)
    {
        var notificationData = new KryptonBasicToastNotificationData()
        {
            CountDownSeconds = _countDownSeconds,
            CustomImage = null,
            NotificationContent = _notificationContentText,
            NotificationTitle = _notificationTitleText,
            NotificationContentFont = _contentFont,
            NotificationTitleFont = _titleFont,
            NotificationIcon = _notificationIcon,
            NotificationLocation = null,
            ShowDoNotShowAgainOption = _showDoNotShowAgainOption,
            NotificationTitleAlignmentH = _titleAlignmentH,
            TopMost = _topMost,
            UseFade = _useFade,
            ShowCloseBox = _showCloseBox,
            ReportToastLocation = _reportToastLocation,
            BorderColor1 = _borderColor1,
            BorderColor2 = _borderColor2,
            UseRtlReading = _useRtlReading
        };

        var notificationDataWithLocation = new KryptonBasicToastNotificationData()
        {
            CountDownSeconds = _countDownSeconds,
            CustomImage = null,
            NotificationContent = _notificationContentText,
            NotificationTitle = _notificationTitleText,
            NotificationContentFont = _contentFont,
            NotificationTitleFont = _titleFont,
            NotificationIcon = _notificationIcon,
            NotificationLocation = new Point((int)knudStartLocationX.Value, (int)knudStartLocationY.Value),
            NotificationTitleAlignmentH = _titleAlignmentH,
            TopMost = _topMost,
            UseFade = _useFade,
            ShowCloseBox = _showCloseBox
        };

        if (kchkShowProgressBar.Checked)
        {
            KryptonToastNotification.ShowBasicProgressBarNotification(notificationData);
        }
        else
        {
            KryptonToastNotification.ShowBasicNotification(notificationData);
        }
    }

    private void ToastNotificationTest_Load(object sender, EventArgs e)
    {
        // Set defaults
        _showCloseBox = false;
        _topMost = true;
        _useFade = false;
        _reportToastLocation = false;
        _showDoNotShowAgainOption = false;
        _titleAlignmentH = PaletteRelativeAlign.Center;
        _countDownSeconds = 60;
        _notificationIcon = KryptonToastNotificationIcon.Information;
        _notificationTitleText = ktxtToastTitle.Text;
        _notificationContentText = ktxtToastContent.Text;
        _borderColor1 = Color.Empty;
        _borderColor2 = Color.Empty;
        _useRtlReading = false;

        kcbtnBorderColor1.SelectedColor = Color.Empty;
        kcbtnBorderColor2.SelectedColor = Color.Empty;

        foreach (var value in Enum.GetValues(typeof(KryptonToastNotificationIcon)))
        {
            kcmbToastIcon.Items.Add(value!.ToString()!);
        }

        kcmbToastIcon.SelectedIndex = 8;

        foreach (var value in Enum.GetValues(typeof(PaletteRelativeAlign)))
        {
            kcmbToastTitleAlignmentH.Items.Add(value!.ToString()!);
        }

        foreach (var value in Enum.GetValues(typeof(PaletteRelativeAlign)))
        {
            kcmbToastTitleAlignmentV.Items.Add(value);
        }

        kcmbToastTitleAlignmentH.SelectedIndex = 1;

        kcmbToastTitleAlignmentV.SelectedIndex = 1;

        knudStartLocationX.Maximum = GraphicsExtensions.GetWorkingArea().Width;

        knudStartLocationX.Value = GraphicsExtensions.GetWorkingArea().Width - Width - 5;

        knudStartLocationY.Maximum = GraphicsExtensions.GetWorkingArea().Height;

        knudStartLocationY.Value = GraphicsExtensions.GetWorkingArea().Height - Height - 5;
    }

    private void kbtnContentFont_Click(object sender, EventArgs e)
    {
        var contentFontDialog = new KryptonFontDialog();

        if (contentFontDialog.ShowDialog() == DialogResult.OK)
        {
            _contentFont = contentFontDialog.Font;
        }
    }

    private void kbtnTitleFont_Click(object sender, EventArgs e)
    {
        KryptonFontDialog titleFontDialog = new KryptonFontDialog();

        if (titleFontDialog.ShowDialog() == DialogResult.OK)
        {
            _titleFont = titleFontDialog.Font;
        }
    }

    private void ktxtToastTitle_TextChanged(object sender, EventArgs e)
    {
        _notificationTitleText = ktxtToastTitle.Text;
    }

    private void ktxtToastContent_TextChanged(object sender, EventArgs e)
    {
        _notificationContentText = $"{ktxtToastContent.Text}\n\nLocation:";
    }

    private void kcmbToastIcon_SelectedIndexChanged(object sender, EventArgs e)
    {
        _notificationIcon = (KryptonToastNotificationIcon)Enum.Parse(typeof(KryptonToastNotificationIcon), kcmbToastIcon.Text);
    }

    private void kcmbToastTitleAlignmentH_SelectedIndexChanged(object sender, EventArgs e)
    {
        _titleAlignmentH = (PaletteRelativeAlign)Enum.Parse(typeof(PaletteRelativeAlign), kcmbToastTitleAlignmentH.Text);
    }

    private void knudCountdownSeconds_ValueChanged(object sender, EventArgs e)
    {
        _countDownSeconds = (int)knudCountdownSeconds.Value;
    }

    private void kchkReportLocation_CheckedChanged(object sender, EventArgs e)
    {
        _reportToastLocation = kchkReportLocation.Checked;
    }

    private void kchkShowCloseBox_CheckedChanged(object sender, EventArgs e)
    {
        _showCloseBox = kchkShowCloseBox.Checked;
    }

    private void kchkUseFade_CheckedChanged(object sender, EventArgs e)
    {
        _useFade = kchkUseFade.Checked;
    }

    private void kcbtnBorderColor1_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        _borderColor1 = e.Color;
    }

    private void kcbtnBorderColor2_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        _borderColor2 = e.Color;
    }

    private void kchkUseRTL_CheckedChanged(object sender, EventArgs e)
    {
        _useRtlReading = kchkUseRTL.Checked;
    }

    private void kchkShowDoNotShowAgain_CheckedChanged(object sender, EventArgs e)
    {
        _showDoNotShowAgainOption = kchkShowDoNotShowAgain.Checked;
    }

    private void kbtnSampleText_Click(object sender, EventArgs e)
    {
        ktxtToastContent.Text = GlobalStaticValues.DEFAULT_LONG_SEED_TEXT;
    }

    private void kcmbToastTitleAlignment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void kcmbToastTitleAlignmentV_SelectedIndexChanged(object sender, EventArgs e)
    {
        _titleAlignmentV = (PaletteRelativeAlign)Enum.Parse(typeof(PaletteRelativeAlign), kcmbToastTitleAlignmentV.Text);
    }
}