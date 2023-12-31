#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit
{
    public partial class VisualBasicToastForm : KryptonForm
    {
        #region Instance Fields

        private readonly bool _showActionButton;

        private readonly bool _showCloseButton;

        private Font? _notificationContentFont;

        private Font? _notificationTitleFont;

        private int _time;

        private int? _countDownSeconds;

        private readonly string _notificationContentText;

        private readonly string? _notificationTitleText;

        private readonly string? _notificationSoundPath;

        private readonly string? _processPath;

        private object? _optionalParameters;

        private Timer _timer;

        private readonly Image? _customIcon;

        private SoundPlayer? _soundPlayer;

        private Stream? _customSoundStream;

        private SystemSound? _systemSound;

        private KryptonCommand? _actionButtonCommand;

        private KryptonToastNotificationIcon? _toastNotificationIcon;

        private InputControlStyle? _inputControlStyle;

        private KryptonToastNotificationActionType? _actionType;

        private KryptonToastNotificationActionButton? _actionButton;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="VisualBasicToastForm" /> class.</summary>
        /// <param name="notificationContentText">The notification content text.</param>
        /// <param name="notificationTitleText">The notification title text.</param>
        /// <param name="notificationSoundPath">The notification sound path.</param>
        /// <param name="notificationTitleFont">The notification title font.</param>
        /// <param name="notificationContentFont">The notification content font.</param>
        /// <param name="countDownSeconds">The count-down seconds.</param>
        /// <param name="processPath">The process path.</param>
        /// <param name="optionalParameters">The optional parameters.</param>
        /// <param name="showActionButton">The show action button.</param>
        /// <param name="showCloseButton">The show close button.</param>
        /// <param name="customSoundStream">The custom sound stream.</param>
        /// <param name="systemSound">The system sound.</param>
        /// <param name="actionButtonCommand">The action button command.</param>
        /// <param name="toastNotificationIcon">The toast notification icon.</param>
        /// <param name="customIcon">The custom icon.</param>
        /// <param name="inputControlStyle">The input control style.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="actionButton">The action button.</param>
        public VisualBasicToastForm(string notificationContentText, string? notificationTitleText, 
                                    string? notificationSoundPath, Font? notificationTitleFont, 
                                    Font? notificationContentFont, int? countDownSeconds,
                                    string? processPath, 
                                    object? optionalParameters, bool? showActionButton,
                                    bool? showCloseButton, Stream? customSoundStream,
                                    SystemSound? systemSound, KryptonCommand? actionButtonCommand,
                                    KryptonToastNotificationIcon? toastNotificationIcon,
                                    Image? customIcon,
                                    InputControlStyle? inputControlStyle,
                                    KryptonToastNotificationActionType? actionType,
                                    KryptonToastNotificationActionButton? actionButton)
        {
            _showActionButton = showActionButton ?? false;
            _showCloseButton = showCloseButton ?? false;
            _notificationTitleFont = notificationTitleFont ?? null;
            _notificationContentFont = notificationContentFont ?? KryptonManager.CurrentGlobalPalette.BaseFont;
            _countDownSeconds = countDownSeconds ?? 60;
            _notificationContentText = notificationContentText;
            _notificationTitleText = notificationTitleText ?? null;
            _notificationSoundPath = notificationSoundPath ?? null;
            _processPath = processPath ?? null;
            _optionalParameters = optionalParameters ?? null;
            _customSoundStream = customSoundStream ?? null;
            _systemSound = systemSound ?? null;
            _actionButtonCommand = actionButtonCommand ?? null;
            _actionButton = actionButton ?? KryptonToastNotificationActionButton.Button1;
            _actionType = actionType ?? KryptonToastNotificationActionType.Default;
            _toastNotificationIcon = toastNotificationIcon ?? KryptonToastNotificationIcon.None;
            _customIcon = customIcon ?? null;
            _inputControlStyle = inputControlStyle ?? InputControlStyle.Standalone;

            InitializeComponent();
        }

        #endregion

        #region Implementaton

        private void SetupBase()
        {
            CancelButton = itbtnButton3;

            CloseBox = _showCloseButton;

            kwlblNotificationTitle.Text = _notificationTitleText;

            krtxNotificationContent.Text = _notificationContentText;

            if (_showActionButton)
            {
                switch (_actionButton)
                {
                    case KryptonToastNotificationActionButton.Button1:
                        itbtnButton1.IsActionButton = true;

                        itbtnButton2.IsActionButton = false;

                        AcceptButton = itbtnButton1;
                        break;
                    case KryptonToastNotificationActionButton.Button2:
                        itbtnButton1.IsActionButton = false;

                        itbtnButton2.IsActionButton = true;

                        AcceptButton = itbtnButton2;
                        break;
                    case null:
                        itbtnButton1.IsActionButton = true;

                        itbtnButton2.IsActionButton = false;

                        AcceptButton = itbtnButton1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SetupContentArea() => krtxNotificationContent.InputControlStyle =
            _inputControlStyle ?? InputControlStyle.Standalone;

        private void SetupIcon(Image? customImage)
        {
            switch (_toastNotificationIcon)
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

        private void SetupActionButtonCommand()
        {
            if (_actionButtonCommand != null)
            {
                _actionButtonCommand.Execute += ActionButtonCommand_Execute;
            }
        }

        private void ActionButtonCommand_Execute(object sender, EventArgs e)
        {
            switch (_actionType)
            {
                case KryptonToastNotificationActionType.Default:
                    break;
                case KryptonToastNotificationActionType.Dismiss:
                    Close();
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
        }

        public new void Show()
        {
            Opacity = 0;

            SetupBase();

            SetupIcon(_customIcon);

            SetupContentArea();

            SetupActionButtonCommand();

            Resize += VisualBasicToastForm_Resize;

            GotFocus += VisualBasicToastForm_GotFocus;

            if (_countDownSeconds != 0)
            {
                itbtnButton3.Text = $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                _timer = new Timer();

                _timer.Interval = 1000;

                _timer.Tick += (sender, args) =>
                {
                    _time++;

                    itbtnButton3.Text = $@"{KryptonManager.Strings.CustomStrings.Dismiss} ({_countDownSeconds - _time})";

                    if (_time == _countDownSeconds)
                    {
                        _timer.Stop();
                    }
                };
            }

            _soundPlayer = _notificationSoundPath != null ? new SoundPlayer(_notificationSoundPath) : null;

            _soundPlayer = _customSoundStream != null ? new SoundPlayer(_customSoundStream) : null;

            base.Show();
        }

        private void VisualBasicToastForm_GotFocus(object sender, EventArgs e)
        {
            itbtnButton3.Focus();
        }

        private void VisualBasicToastForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        #endregion
    }
}
