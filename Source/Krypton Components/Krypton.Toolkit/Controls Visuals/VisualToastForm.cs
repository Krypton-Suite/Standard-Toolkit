#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit
{
    internal partial class VisualToastForm : KryptonForm
    {
        #region Instance Fields

        private readonly bool _showCloseButton;

        private readonly bool _showCountDownProgressBar;

        private readonly bool _showCountDownProgressBarText;

        private readonly bool _showActionButton;

        private readonly bool _showUserResponse;

        private readonly Color? _userResponsePromptColor;

        private readonly ContentAlignment? _labelContentTextAlignment;

        private readonly ContentAlignment? _labelTitleTextAlignment;

        private readonly Font? _userResponsePromptFont;

        private readonly InputControlStyle? _userInputControlStyle;

        private readonly PaletteRelativeAlign? _userResponsePromptAlignHorizontal;

        private readonly PaletteRelativeAlign? _userResponsePromptAlignVertical;

        private readonly PaletteRelativeAlign? _userResponseTextAlignmentHorizontal;

        private readonly PaletteRelativeAlign? _notificationContentRichTextBoxAlignment;

        private readonly HorizontalAlignment? _notificationContentTextBoxAlignment;

        private readonly KryptonToastNotificationActionButton? _actionButton;

        private readonly KryptonToastNotificationActionType? _actionType;

        private readonly KryptonToastNotificationContentAreaType? _toastNotificationContentAreaType;

        private readonly KryptonToastNotificationIcon? _toastNotificationIcon;

        private readonly KryptonToastNotificationInputAreaType? _toastNotificationInputAreaType;

        private readonly int? _countDownSeconds;

        private readonly int? _countDownTimerInterval;

        private int? _time;

        private readonly int? _progressBarMaximum;

        private readonly int? _numericUpDownMaximum;

        private readonly object? _contentLinkDestination;

        private Timer _timer;

        private readonly Image? _customImage;

        private SoundPlayer? _soundPlayer;

        private readonly Stream? _soundStream;

        private readonly RightToLeft? _rightToLeft;

        private readonly string _notificationTitle;

        private readonly string _notificationContentText;

        private readonly string? _soundPath;

        private readonly string _userResponsePromptText;

        private readonly LinkArea? _notificationContentLinkArea;

        private readonly KryptonCommand? _actionButtonCommand;

        #endregion

        #region Internals

        internal KryptonToastNotificationActionType? ActionType =>
            _actionType ?? KryptonToastNotificationActionType.Default;

        internal object? ContentLinkDestination => _contentLinkDestination ?? string.Empty;

        internal KryptonCommand? ActionButtonCommand => _actionButtonCommand ?? null;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="VisualToastForm" /> class.</summary>
        /// <param name="showCloseButton">The show close button.</param>
        /// <param name="showCountDownProgressBar">The show count down progress bar.</param>
        /// <param name="showCountDownProgressBarText">The show count down progress bar text.</param>
        /// <param name="showActionButton">The show action button.</param>
        /// <param name="showUserResponse">The show user response.</param>
        /// <param name="userResponsePromptColor">Color of the user response prompt.</param>
        /// <param name="labelContentTextAlignment">The label content text alignment.</param>
        /// <param name="labelTitleTextAlignment">The label title text alignment.</param>
        /// <param name="userResponsePromptFont">The user response prompt font.</param>
        /// <param name="userInputControlStyle">The user input control style.</param>
        /// <param name="userResponsePromptAlignHorizontal">The user response prompt align horizontal.</param>
        /// <param name="userResponsePromptAlignVertical">The user response prompt align vertical.</param>
        /// <param name="userResponseTextAlignmentHorizontal">The user response text alignment horizontal.</param>
        /// <param name="notificationContentRichTextBoxAlignment">The notification content rich text box alignment.</param>
        /// <param name="notificationContentTextBoxAlignment">The notification content text box alignment.</param>
        /// <param name="actionButton">The action button.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="contentAreaType">Type of the content area.</param>
        /// <param name="toastNotificationIcon">The toast notification icon.</param>
        /// <param name="inputAreaType">Type of the input area.</param>
        /// <param name="countDownSeconds">The count down seconds.</param>
        /// <param name="countDownTimerInterval">The count down timer interval.</param>
        /// <param name="numericUpDownMaximum">The numeric up down maximum.</param>
        /// <param name="progressBarMaximum">The progress bar maximum.</param>
        /// <param name="customImage">The custom image.</param>
        /// <param name="soundStream">The sound stream.</param>
        /// <param name="rightToLeft">The right to left.</param>
        /// <param name="notificationTitle">The notification title.</param>
        /// <param name="notificationContentText">The notification content text.</param>
        /// <param name="soundPath">The sound path.</param>
        /// <param name="userResponsePromptText">The user response prompt text.</param>
        /// <param name="notificationContentLinkArea">The notification content link area.</param>
        /// <param name="contentLinkDestination">The content link destination.</param>
        /// <param name="actionButtonCommand">The action button command.</param>
        public VisualToastForm(bool? showCloseButton, bool? showCountDownProgressBar, bool? showCountDownProgressBarText,
                               bool? showActionButton, bool? showUserResponse, Color? userResponsePromptColor, 
                               ContentAlignment? labelContentTextAlignment, ContentAlignment? labelTitleTextAlignment,
                               Font? userResponsePromptFont, InputControlStyle? userInputControlStyle,
                               PaletteRelativeAlign? userResponsePromptAlignHorizontal, 
                               PaletteRelativeAlign? userResponsePromptAlignVertical,
                               PaletteRelativeAlign? userResponseTextAlignmentHorizontal, 
                               PaletteRelativeAlign? notificationContentRichTextBoxAlignment,
                               HorizontalAlignment? notificationContentTextBoxAlignment,
                               KryptonToastNotificationActionButton? actionButton,
                               KryptonToastNotificationActionType? actionType, KryptonToastNotificationContentAreaType? contentAreaType,
                               KryptonToastNotificationIcon? toastNotificationIcon, KryptonToastNotificationInputAreaType? inputAreaType,
                               int? countDownSeconds, int? countDownTimerInterval, int? numericUpDownMaximum, int? progressBarMaximum,
                               Image? customImage, Stream? soundStream, RightToLeft? rightToLeft, string? notificationTitle, 
                               string notificationContentText, string? soundPath, string? userResponsePromptText, LinkArea? notificationContentLinkArea,
                               object? contentLinkDestination, KryptonCommand? actionButtonCommand)
        {
            // Assign values
            _showCloseButton = showCloseButton ?? false;
            _showCountDownProgressBar = showCountDownProgressBar ?? false;
            _showCountDownProgressBarText = showCountDownProgressBarText ?? false;
            _showActionButton = showActionButton ?? false;
            _showUserResponse = showUserResponse ?? false;
            _userResponsePromptColor = userResponsePromptColor ?? Color.Gray;
            _labelContentTextAlignment = labelContentTextAlignment ?? ContentAlignment.MiddleCenter;
            _labelTitleTextAlignment = labelTitleTextAlignment ?? ContentAlignment.MiddleCenter;
            _userResponsePromptFont = userResponsePromptFont ?? KryptonManager.CurrentGlobalPalette?.BaseFont;
            _userInputControlStyle = userInputControlStyle ?? InputControlStyle.Standalone;
            _userResponsePromptAlignHorizontal = userResponsePromptAlignHorizontal ?? PaletteRelativeAlign.Inherit;
            _userResponsePromptAlignVertical = userResponsePromptAlignVertical ?? PaletteRelativeAlign.Inherit;
            _userResponseTextAlignmentHorizontal = userResponseTextAlignmentHorizontal ?? PaletteRelativeAlign.Inherit;
            _notificationContentRichTextBoxAlignment = notificationContentRichTextBoxAlignment ?? PaletteRelativeAlign.Near;
            _notificationContentTextBoxAlignment = notificationContentTextBoxAlignment ?? HorizontalAlignment.Left;
            _actionButton = actionButton ?? KryptonToastNotificationActionButton.Button1;
            _actionType = actionType ?? KryptonToastNotificationActionType.Default;
            _toastNotificationContentAreaType = contentAreaType ?? KryptonToastNotificationContentAreaType.WrapLabel;
            _toastNotificationIcon = toastNotificationIcon ?? KryptonToastNotificationIcon.None;
            _toastNotificationInputAreaType = inputAreaType ?? KryptonToastNotificationInputAreaType.None;
            _countDownSeconds = countDownSeconds ?? 60;
            _countDownTimerInterval = countDownTimerInterval ?? 1000;
            _numericUpDownMaximum = numericUpDownMaximum ?? 100;
            _progressBarMaximum = progressBarMaximum ?? 100;
            _customImage = customImage ?? null;
            _soundStream = soundStream ?? Stream.Null;
            _rightToLeft = rightToLeft ?? RightToLeft.Inherit;
            _notificationTitle = notificationTitle ?? string.Empty;
            _notificationContentText = notificationContentText;
            _soundPath = soundPath ?? string.Empty;
            _userResponsePromptText = userResponsePromptText ?? string.Empty;
            _notificationContentLinkArea = notificationContentLinkArea ?? new LinkArea(0, klwlblNotificationContent.Text.Length);
            _contentLinkDestination = contentLinkDestination ?? string.Empty;
            _actionButtonCommand = actionButtonCommand ?? null;

            InitializeComponent();
        }

        #endregion

        #region Implementation

        private void SetupBaseElements()
        {
            switch (_toastNotificationContentAreaType)
            {
                case KryptonToastNotificationContentAreaType.RichTextBox:
                    klwlblNotificationContent.Visible = false;
                    kwlblNotificationContent.Visible = false;
                    krtxtNotificationContent.Visible = true;
                    ktxtNotificationContent.Visible = false;
                    krtxtNotificationContent.Text = _notificationContentText;
                    ktxtNotificationContent.StateCommon.Content.TextH = _notificationContentRichTextBoxAlignment ?? PaletteRelativeAlign.Near;
                    break;
                case KryptonToastNotificationContentAreaType.MultiLineTextBox:
                    klwlblNotificationContent.Visible = false;
                    kwlblNotificationContent.Visible = false;
                    krtxtNotificationContent.Visible = false;
                    ktxtNotificationContent.Visible = true;
                    ktxtNotificationContent.Text = _notificationContentText;
                    ktxtNotificationContent.TextAlign = _notificationContentTextBoxAlignment ?? HorizontalAlignment.Left;
                    break;
                case KryptonToastNotificationContentAreaType.WrapLinkLabel:
                    klwlblNotificationContent.Visible = true;
                    kwlblNotificationContent.Visible = false;
                    krtxtNotificationContent.Visible = true;
                    ktxtNotificationContent.Visible = false;
                    klwlblNotificationContent.Text = _notificationContentText;
                    klwlblNotificationContent.TextAlign = _labelContentTextAlignment ?? ContentAlignment.MiddleCenter;
                    SetupLinkArea(_notificationContentLinkArea);
                    break;
                case KryptonToastNotificationContentAreaType.WrapLabel:
                    klwlblNotificationContent.Visible = false;
                    kwlblNotificationContent.Visible = false;
                    krtxtNotificationContent.Visible = true;
                    ktxtNotificationContent.Visible = false;
                    klwlblNotificationContent.Text = _notificationContentText;
                    klwlblNotificationContent.TextAlign = _labelContentTextAlignment ?? ContentAlignment.MiddleCenter;
                    break;
                case null:
                    klwlblNotificationContent.Visible = false;
                    kwlblNotificationContent.Visible = false;
                    krtxtNotificationContent.Visible = true;
                    ktxtNotificationContent.Visible = false;
                    klwlblNotificationContent.Text = _notificationContentText;
                    klwlblNotificationContent.TextAlign = _labelContentTextAlignment ?? ContentAlignment.MiddleCenter;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetupLinkArea(LinkArea? linkArea) => klwlblNotificationContent.LinkArea = linkArea ?? new LinkArea(0, klwlblNotificationContent.Text.Length);

        private void SetStartupLocation() =>
            //Once loaded, position the form to the bottom left of the screen with added padding
            Location = new Point(Screen.PrimaryScreen!.WorkingArea.Width - Width - 5,
                Screen.PrimaryScreen.WorkingArea.Height - Height - 5);

        private void SetupUserResponseArea()
        {
            if (!_showUserResponse)
            {
                TableLayoutRowStyleCollection rowStyleCollection = tlpContent.RowStyles;

            }
            else
            {
                switch (_toastNotificationInputAreaType)
                {
                    case KryptonToastNotificationInputAreaType.None:
                        kpnlUserPromptArea.Visible = false;
                        break;
                    case KryptonToastNotificationInputAreaType.DomainDropDown:
                        break;
                    case KryptonToastNotificationInputAreaType.NumericDropDown:
                        break;
                    case KryptonToastNotificationInputAreaType.MaskedTextBox:
                        break;
                    case KryptonToastNotificationInputAreaType.TextBox:
                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SetIcon()
        {
            if (OSUtilities.IsWindowsEleven)
            {
                
            }
            else if (OSUtilities.IsWindowsTen)
            {
                
            }
            else
            {

            }
        }

        public new void Show()
        {
            Opacity = 0;

            DoubleBuffered = true;

            SetupBaseElements();

            SetupUserResponseArea();

            if (_countDownSeconds != null || _countDownSeconds != 0)
            {
                switch (_rightToLeft)
                {
                    case RightToLeft.No:
                        itbtnButton1.Text =
                            $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                        itbtnButton1.IsDismissButton = true;

                        itbtnButton2.IsDismissButton = false;

                        itbtnButton3.IsDismissButton = false;
                        break;
                    case RightToLeft.Yes:
                        itbtnButton3.Text =
                            $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                        itbtnButton1.IsDismissButton = false;

                        itbtnButton2.IsDismissButton = false;

                        itbtnButton3.IsDismissButton = true;
                        break;
                    case RightToLeft.Inherit:
                        itbtnButton2.Text =
                            $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                        itbtnButton1.IsDismissButton = false;

                        itbtnButton2.IsDismissButton = true;

                        itbtnButton3.IsDismissButton = false;
                        break;
                    case null:
                        itbtnButton2.Text =
                            $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                        itbtnButton1.IsDismissButton = false;

                        itbtnButton2.IsDismissButton = true;

                        itbtnButton3.IsDismissButton = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _timer = new Timer();

                _timer.Interval = _countDownTimerInterval ?? 1000;

                _timer.Tick += (sender, e) =>
                {
                    _time++;

                    switch (_rightToLeft)
                    {
                        case RightToLeft.No:
                            itbtnButton1.Text =
                                $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                            itbtnButton1.IsDismissButton = true;

                            itbtnButton2.IsDismissButton = false;

                            itbtnButton3.IsDismissButton = false;
                            break;
                        case RightToLeft.Yes:
                            itbtnButton3.Text =
                                $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                            itbtnButton1.IsDismissButton = false;

                            itbtnButton2.IsDismissButton = false;

                            itbtnButton3.IsDismissButton = true;
                            break;
                        case RightToLeft.Inherit:
                            itbtnButton2.Text =
                                $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                            itbtnButton1.IsDismissButton = false;

                            itbtnButton2.IsDismissButton = true;

                            itbtnButton3.IsDismissButton = false;
                            break;
                        case null:
                            itbtnButton2.Text =
                                $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                            itbtnButton1.IsDismissButton = false;

                            itbtnButton2.IsDismissButton = true;

                            itbtnButton3.IsDismissButton = false;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (_time == _countDownSeconds)
                    {
                        _timer.Stop();

                        Hide();
                    }
                };
            }

            if (_actionButtonCommand != null)
            {
                _actionButtonCommand.Execute += (sender, e) =>
                {
                    switch (_actionType)
                    {
                        case KryptonToastNotificationActionType.Default:
                            break;
                        case KryptonToastNotificationActionType.Dismiss:
                            break;
                        case KryptonToastNotificationActionType.LaunchProcess:
                            break;
                        case KryptonToastNotificationActionType.Open:
                            break;
                        case null:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                };
            }

            _soundPlayer = _soundPath != null ? new SoundPlayer(_soundPath) : null;

            _soundPlayer = _soundStream != null ? new SoundPlayer(_soundStream) : null;

            base.Show();
        }

        #endregion
    }
}