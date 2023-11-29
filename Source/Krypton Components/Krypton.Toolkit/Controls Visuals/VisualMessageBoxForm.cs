#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Local
using ContentAlignment = System.Drawing.ContentAlignment;

namespace Krypton.Toolkit
{
    internal partial class VisualMessageBoxForm : KryptonForm
    {
        #region Static Fields
        private const int GAP = 10;
        private static readonly int OS_MAJOR_VERSION;
        #endregion

        #region Instance Fields

        private readonly bool _showHelpButton;
        private readonly string _text;
        private readonly string _caption;
        private readonly string _applicationPath;
        private readonly KryptonMessageBoxButtons _buttons;
        private readonly KryptonMessageBoxIcon _kryptonMessageBoxIcon;
        private readonly Image? _applicationImage;
        private readonly bool? _forceUseOfOperatingSystemIcons;

        private readonly KryptonMessageBoxDefaultButton _defaultButton;
        private readonly MessageBoxOptions _options; // https://github.com/Krypton-Suite/Standard-Toolkit/issues/313

        // If help information provided or we are not a service/default desktop application then grab an owner for showing the message box
        private readonly IWin32Window? _showOwner;
        private readonly HelpInfo? _helpInfo;

        // Action button features (aka _button5)
        private readonly bool _showActionButton;
        private readonly string _actionButtonText;
        private readonly KryptonCommand? _actionButtonCommand;

        // For the LinkLabel option
        private readonly MessageBoxContentAreaType? _contentAreaType;
        private readonly KryptonCommand? _linkLabelCommand;
        private readonly ProcessStartInfo? _linkLaunchArgument;
        private readonly ContentAlignment? _messageTextAlignment;
        private readonly LinkArea _contentLinkArea;

        #endregion

        #region Identity
        static VisualMessageBoxForm() => OS_MAJOR_VERSION = Environment.OSVersion.Version.Major;

        public VisualMessageBoxForm()
        {
            InitializeComponent();
        }


        internal VisualMessageBoxForm(IWin32Window? showOwner, string text, string caption,
                                       KryptonMessageBoxButtons buttons,
                                       KryptonMessageBoxIcon icon,
                                       KryptonMessageBoxDefaultButton defaultButton,
                                       MessageBoxOptions options,
                                       HelpInfo? helpInfo, bool? showCtrlCopy,
                                       bool? showHelpButton,
                                       bool? showActionButton, string? actionButtonText,
                                       KryptonCommand? actionButtonCommand,
                                       Image? applicationImage,
                                       string? applicationPath,
                                       MessageBoxContentAreaType? contentAreaType,
                                       KryptonCommand? linkLabelCommand,
                                       ProcessStartInfo? linkLaunchArgument,
                                       LinkArea? contentLinkArea,
                                       ContentAlignment? messageTextAlignment,
                                       bool? forceUseOfOperatingSystemIcons)
        {
            // Store incoming values
            _text = text;
            _caption = caption;
            _buttons = buttons;
            _kryptonMessageBoxIcon = icon;
            _defaultButton = defaultButton;
            _options = options;
            _helpInfo = helpInfo;
            _showOwner = showOwner;
            _showHelpButton = showHelpButton ?? (helpInfo != null);
            _showActionButton = showActionButton ?? false;
            _actionButtonText = actionButtonText ?? string.Empty;
            _actionButtonCommand = actionButtonCommand;
            _applicationImage = applicationImage;
            _applicationPath = applicationPath ?? string.Empty;
            _contentAreaType = contentAreaType ?? MessageBoxContentAreaType.Normal;
            _linkLabelCommand = linkLabelCommand ?? new KryptonCommand();
            _contentLinkArea = contentLinkArea ?? new LinkArea(0, text.Length);
            _linkLaunchArgument = linkLaunchArgument ?? new ProcessStartInfo();
            _messageTextAlignment = messageTextAlignment ?? ContentAlignment.MiddleLeft;
            _forceUseOfOperatingSystemIcons = forceUseOfOperatingSystemIcons ?? false;

            // Create the form contents
            InitializeComponent();

            RightToLeftLayout = _options.HasFlag(MessageBoxOptions.RtlReading);

            // Update contents to match requirements
            UpdateText();
            UpdateIcon();
            UpdateButtons();
            UpdateDefault();
            UpdateHelp();
            UpdateTextExtra(showCtrlCopy);
            UpdateContentAreaType(contentAreaType);
            UpdateContentAreaTextAlignment(contentAreaType, messageTextAlignment);
            UpdateContentLinkArea(contentLinkArea);

            SetupActionButtonUI(_showActionButton);

            // Finally calculate and set form sizing
            UpdateSizing(showOwner);
        }

        public VisualMessageBoxForm(KryptonMessageBoxData messageBoxData)
        {
            // Store incoming values
            _text = messageBoxData.MessageText;
            _caption = messageBoxData.Caption;
            _buttons = messageBoxData.Buttons;
            _kryptonMessageBoxIcon = messageBoxData.Icon;
            _defaultButton = messageBoxData.DefaultButton ?? KryptonMessageBoxDefaultButton.Button1;
            _options = messageBoxData.Options;
            _helpInfo = messageBoxData.HelpInfo;
            _showOwner = messageBoxData.Owner;
            _showHelpButton = messageBoxData.ShowHelpButton ?? (messageBoxData.HelpInfo != null);
            _showActionButton = messageBoxData.ShowActionButton ?? false;
            _actionButtonText = messageBoxData.ActionButtonText ?? string.Empty;
            _actionButtonCommand = messageBoxData.ActionButtonCommand;
            _applicationImage = messageBoxData.ApplicationImage;
            _applicationPath = messageBoxData.ApplicationPath ?? string.Empty;
            _contentAreaType = messageBoxData.MessageContentAreaType ?? MessageBoxContentAreaType.Normal;
            _linkLabelCommand = messageBoxData.LinkLabelCommand ?? new KryptonCommand();
            _contentLinkArea = messageBoxData.ContentLinkArea ?? new LinkArea(0, messageBoxData.MessageText.Length);
            _linkLaunchArgument = messageBoxData.LinkLaunchArgument ?? new ProcessStartInfo();
            _messageTextAlignment = messageBoxData.MessageTextAlignment ?? ContentAlignment.MiddleLeft;
            _forceUseOfOperatingSystemIcons = messageBoxData.ForceUseOfOperatingSystemIcons ?? false;

            // Create the form contents
            InitializeComponent();

            RightToLeftLayout = _options.HasFlag(MessageBoxOptions.RtlReading);

            // Update contents to match requirements
            UpdateText();
            UpdateIcon();
            UpdateButtons();
            UpdateDefault();
            UpdateHelp();
            UpdateTextExtra(messageBoxData.ShowCtrlCopy);
            UpdateContentAreaType(messageBoxData.MessageContentAreaType);
            UpdateContentAreaTextAlignment(messageBoxData.MessageContentAreaType, messageBoxData.MessageTextAlignment);
            UpdateContentLinkArea(messageBoxData.ContentLinkArea);

            SetupActionButtonUI(_showActionButton);

            // Finally calculate and set form sizing
            UpdateSizing(messageBoxData.Owner);
        }

        #endregion Identity

        #region Implementation

        private void UpdateText()
        {
            Text = string.IsNullOrEmpty(_caption) ? string.Empty : _caption.Split(Environment.NewLine.ToCharArray())[0];

            if (_contentAreaType == MessageBoxContentAreaType.Normal)
            {
                _messageText.Text = _text;

                _messageText.RightToLeft = _options.HasFlag(MessageBoxOptions.RightAlign)
                    ? RightToLeft.Yes
                    : _options.HasFlag(MessageBoxOptions.RtlReading)
                        ? RightToLeft.Inherit
                        : RightToLeft.No;
            }
            else
            {
                _linkLabelMessageText.Text = _text;

                _linkLabelMessageText.RightToLeft = _options.HasFlag(MessageBoxOptions.RightAlign) ? RightToLeft.Yes :
                    _options.HasFlag(MessageBoxOptions.RtlReading) ? RightToLeft.Inherit : RightToLeft.No;
            }
        }

        private void UpdateTextExtra(bool? showCtrlCopy)
        {
            if (!showCtrlCopy.HasValue)
            {
                switch (_kryptonMessageBoxIcon)
                {
                    case KryptonMessageBoxIcon.Error:
                    case KryptonMessageBoxIcon.Exclamation:
                        showCtrlCopy = true;
                        break;
                }
            }

            if (showCtrlCopy == true)
            {
                TextExtra = @"Ctrl+c to copy";
            }
        }

        private void UpdateIcon()
        {
            if (OSUtilities.IsWindowsEleven)
            {
                switch (_kryptonMessageBoxIcon)
                {
                    case KryptonMessageBoxIcon.None:
                        // Windows XP and before will Beep, Vista and above do not!
                        if (OS_MAJOR_VERSION < 6)
                        {
                            SystemSounds.Beep.Play();
                        }
                        break;
                    case KryptonMessageBoxIcon.Hand:
                        _messageIcon.Image = MessageBoxImageResources.GenericHand;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.Question:
                        _messageIcon.Image = MessageBoxImageResources.Question_Windows_11;
                        SystemSounds.Question.Play();
                        break;
                    case KryptonMessageBoxIcon.Exclamation:
                        _messageIcon.Image = MessageBoxImageResources.Warning_Windows_11;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Asterisk:
                        _messageIcon.Image = MessageBoxImageResources.Asterisk_Windows_11;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Stop:
                        _messageIcon.Image = MessageBoxImageResources.GenericStop;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Error:
                        _messageIcon.Image = MessageBoxImageResources.Critical_Windows_11;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Warning:
                        _messageIcon.Image = MessageBoxImageResources.Warning_Windows_11;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Information:
                        _messageIcon.Image = MessageBoxImageResources.Information_Windows_11;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Shield:
                        _messageIcon.Image = UACShieldIconResources.UAC_Shield_Windows_11;
                        break;
                    case KryptonMessageBoxIcon.WindowsLogo:
                        _messageIcon.Image = MessageBoxImageResources.Windows11;
                        break;
                    case KryptonMessageBoxIcon.Application:
                        if (_applicationImage != null)
                        {
                            _messageIcon.Image = _applicationImage;
                        }
                        else if (!string.IsNullOrEmpty(_applicationPath))
                        {
                            Image? sourceImage = GraphicsExtensions.ExtractIconFromFilePath(_applicationPath)?.ToBitmap();
                            Image? scaledImage = GraphicsExtensions.ScaleImage(sourceImage, new Size(32, 32));

                            _messageIcon.Image = scaledImage;
                        }
                        else
                        {
                            // Fall back to defaults
                            _messageIcon.Image = SystemIcons.Application.ToBitmap();
                        }
                        break;
                    case KryptonMessageBoxIcon.SystemApplication:
                        _messageIcon.Image = SystemIcons.Application.ToBitmap();
                        break;
                }
            }
            else
            {
                switch (_kryptonMessageBoxIcon)
                {
                    case KryptonMessageBoxIcon.None:
                        // Windows XP and before will Beep, Vista and above do not!
                        if (OS_MAJOR_VERSION < 6)
                        {
                            SystemSounds.Beep.Play();
                        }
                        break;
                    case KryptonMessageBoxIcon.Hand:
                        _messageIcon.Image = MessageBoxImageResources.GenericHand;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemHand:
                        _messageIcon.Image = SystemIcons.Hand.ToBitmap();
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.Question:
                        _messageIcon.Image = MessageBoxImageResources.GenericQuestion;
                        SystemSounds.Question.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemQuestion:
                        _messageIcon.Image = SystemIcons.Question.ToBitmap();
                        SystemSounds.Question.Play();
                        break;
                    case KryptonMessageBoxIcon.Exclamation:
                        _messageIcon.Image = MessageBoxImageResources.GenericWarning;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemExclamation:
                        _messageIcon.Image = SystemIcons.Warning.ToBitmap();
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Asterisk:
                        _messageIcon.Image = OSUtilities.IsWindowsEleven
                            ? MessageBoxImageResources.Asterisk_Windows_11
                            : MessageBoxImageResources.GenericAsterisk;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemAsterisk:
                        _messageIcon.Image = SystemIcons.Asterisk.ToBitmap();
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Stop:
                        _messageIcon.Image = MessageBoxImageResources.GenericStop;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Error:
                        _messageIcon.Image = MessageBoxImageResources.GenericCritical;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Warning:
                        _messageIcon.Image = MessageBoxImageResources.GenericWarning;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Information:
                        _messageIcon.Image = MessageBoxImageResources.GenericInformation;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Shield:
                        if (OSUtilities.IsWindowsEleven)
                        {
                            _messageIcon.Image = UACShieldIconResources.UAC_Shield_Windows_11;
                        }
                        else if (OSUtilities.IsWindowsTen)
                        {
                            _messageIcon.Image = UACShieldIconResources.UAC_Shield_Windows_10;
                        }
                        else
                        {
                            _messageIcon.Image = UACShieldIconResources.UAC_Shield_Windows_7;
                        }
                        break;
                    case KryptonMessageBoxIcon.WindowsLogo:
                        // Because Windows 11 displays a generic application icon,
                        // we need to rely on a image instead
                        if (OSUtilities.IsWindowsEleven)
                        {
                            _messageIcon.Image = MessageBoxImageResources.Windows11;
                        }
                        // Windows 10
                        else if (OSUtilities.IsWindowsTen)
                        {
                            _messageIcon.Image = MessageBoxImageResources.Windows_8_and_10_Logo;
                        }
                        else
                        {
                            _messageIcon.Image = SystemIcons.WinLogo.ToBitmap();
                        }
                        break;
                    case KryptonMessageBoxIcon.Application:
                        if (_applicationImage != null)
                        {
                            _messageIcon.Image = _applicationImage;
                        }
                        else if (!string.IsNullOrEmpty(_applicationPath))
                        {
                            Image? sourceImage = GraphicsExtensions.ExtractIconFromFilePath(_applicationPath)?.ToBitmap();
                            Image? scaledImage = GraphicsExtensions.ScaleImage(sourceImage, new Size(32, 32));

                            _messageIcon.Image = scaledImage;
                        }
                        else
                        {
                            // Fall back to defaults
                            _messageIcon.Image = SystemIcons.Application.ToBitmap();
                        }
                        break;
                    case KryptonMessageBoxIcon.SystemApplication:
                        _messageIcon.Image = SystemIcons.Application.ToBitmap();
                        break;
                }
            }

            _messageIcon.Visible = (_kryptonMessageBoxIcon != KryptonMessageBoxIcon.None);

        }

        private void UpdateButtons()
        {
            switch (_buttons)
            {
                case KryptonMessageBoxButtons.OK:
                    _button1.Text = KryptonManager.Strings.GeneralStrings.OK;
                    _button1.DialogResult = DialogResult.OK;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    break;
                case KryptonMessageBoxButtons.OKCancel:
                    _button1.Text = KryptonManager.Strings.GeneralStrings.OK;
                    _button2.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                    _button1.DialogResult = DialogResult.OK;
                    _button2.DialogResult = DialogResult.Cancel;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    break;
                case KryptonMessageBoxButtons.YesNo:
                    _button1.Text = KryptonManager.Strings.GeneralStrings.Yes;
                    _button2.Text = KryptonManager.Strings.GeneralStrings.No;
                    _button1.DialogResult = DialogResult.Yes;
                    _button2.DialogResult = DialogResult.No;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    ControlBox = false;
                    break;
                case KryptonMessageBoxButtons.YesNoCancel:
                    _button1.Text = KryptonManager.Strings.GeneralStrings.Yes;
                    _button2.Text = KryptonManager.Strings.GeneralStrings.No;
                    _button3.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                    _button1.DialogResult = DialogResult.Yes;
                    _button2.DialogResult = DialogResult.No;
                    _button3.DialogResult = DialogResult.Cancel;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    _button3.Visible = true;
                    _button3.Enabled = true;
                    break;
                case KryptonMessageBoxButtons.RetryCancel:
                    _button1.Text = KryptonManager.Strings.GeneralStrings.Retry;
                    _button2.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                    _button1.DialogResult = DialogResult.Retry;
                    _button2.DialogResult = DialogResult.Cancel;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    break;
                case KryptonMessageBoxButtons.AbortRetryIgnore:
                    _button1.Text = KryptonManager.Strings.GeneralStrings.Abort;
                    _button2.Text = KryptonManager.Strings.GeneralStrings.Retry;
                    _button3.Text = KryptonManager.Strings.GeneralStrings.Ignore;
                    _button1.DialogResult = DialogResult.Abort;
                    _button2.DialogResult = DialogResult.Retry;
                    _button3.DialogResult = DialogResult.Ignore;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    _button3.Visible = true;
                    _button3.Enabled = true;
                    ControlBox = false;
                    break;
                case KryptonMessageBoxButtons.CancelTryContinue:
                    _button1.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                    _button2.Text = KryptonManager.Strings.GeneralStrings.TryAgain;
                    _button3.Text = KryptonManager.Strings.GeneralStrings.Continue;
                    _button1.DialogResult = DialogResult.Cancel;
#if NET6_0_OR_GREATER
                    _button2.DialogResult = DialogResult.TryAgain;
                    _button3.DialogResult = DialogResult.Continue;
#else
                    _button2.DialogResult = (DialogResult)10;
                    _button3.DialogResult = (DialogResult)11;
#endif
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    _button3.Visible = true;
                    _button3.Enabled = true;
                    break;
            }

            if (_showActionButton)
            {
                _button5.Text = _actionButtonText;
                _button5.Visible = true;
                _button5.Enabled = true;
                _button5.KryptonCommand = _actionButtonCommand;
            }

            // Do we ignore the Alt+F4 on the buttons?
            if (!ControlBox)
            {
                _button1.IgnoreAltF4 = true;
                _button2.IgnoreAltF4 = true;
                _button3.IgnoreAltF4 = true;
                _button4.IgnoreAltF4 = true;
                _button5.IgnoreAltF4 = true;
            }
        }

        private void UpdateDefault()
        {
            switch (_defaultButton)
            {
                case KryptonMessageBoxDefaultButton.Button1:
                    //_button1.Select();
                    AcceptButton = _button1;
                    break;
                case KryptonMessageBoxDefaultButton.Button2:
                    //_button2.Select();
                    AcceptButton = _button2;
                    break;
                case KryptonMessageBoxDefaultButton.Button3:
                    //_button3.Select();
                    AcceptButton = _button3;
                    break;
                case KryptonMessageBoxDefaultButton.Button4:
                    AcceptButton = _showHelpButton ? _button4 : _button1;
                    break;
                case KryptonMessageBoxDefaultButton.Button5:
                    AcceptButton = _showActionButton ? _button5 : _button1;

                    break;
                default:
                    AcceptButton = _showHelpButton ? _button4 : _button1;
                    break;
            }
        }

        private void UpdateHelp()
        {
            if (!_showHelpButton)
            {
                return;
            }

            MessageButton helpButton = _buttons switch
            {
                KryptonMessageBoxButtons.OK => _button2,
                KryptonMessageBoxButtons.OKCancel or KryptonMessageBoxButtons.YesNo or KryptonMessageBoxButtons.RetryCancel => _button3,
                KryptonMessageBoxButtons.AbortRetryIgnore or KryptonMessageBoxButtons.YesNoCancel => _button4,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (helpButton != null)
            {
                helpButton.Visible = true;
                helpButton.Enabled = true;
                helpButton.Text = KryptonManager.Strings.GeneralStrings.Help;
                helpButton.KeyPress += (_, _) => LaunchHelp();
                helpButton.Click += (_, _) => LaunchHelp();
            }
        }

        /// <summary>
        /// When the user clicks the Help button, the Help file specified in the helpFilePath parameter
        /// is opened and the Help keyword topic identified by the keyword parameter is Displayed.
        /// The form that owns the message box (or the active form) also receives the HelpRequested event.
        /// </summary>
        private void LaunchHelp()
        {
            try
            {
                if (_showOwner != null)
                {
                    Control control = FromHandle(_showOwner.Handle);

                    MethodInfo? mInfoMethod = control.GetType().GetMethod(nameof(OnHelpRequested), BindingFlags.Instance | BindingFlags.NonPublic,
                        Type.DefaultBinder, new[] { typeof(HelpEventArgs) }, null)!;
                    mInfoMethod?.Invoke(control, new object[] { new HelpEventArgs(MousePosition) });
                    if (_helpInfo != null)
                    {
                        if (string.IsNullOrWhiteSpace(_helpInfo.HelpFilePath))
                        {
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(_helpInfo.Keyword))
                        {
                            Help.ShowHelp(control, _helpInfo.HelpFilePath, _helpInfo.Keyword);
                        }
                        else
                        {
                            Help.ShowHelp(control, _helpInfo.HelpFilePath, _helpInfo.Navigator, _helpInfo.Param);
                        }
                    }
                }
            }
            catch
            {
                // Do nothing if failure to send to Parent
            }

        }

        private void UpdateSizing(IWin32Window? showOwner)
        {
            Size messageSizing = UpdateMessageSizing(showOwner);
            Size buttonsSizing = UpdateButtonsSizing();

            // Size of window is calculated from the client area
            ClientSize = new Size(Math.Max(messageSizing.Width, buttonsSizing.Width),
                messageSizing.Height + buttonsSizing.Height);
        }

        private Size UpdateMessageSizing(IWin32Window? showOwner)
        {
            // Update size of the message label but with a maximum width
            Size textSize;
            using (Graphics g = CreateGraphics())
            {
                // Find size of the label, with a max of 2/3 screen width
                Screen? screen = showOwner != null ? Screen.FromHandle(showOwner.Handle) : Screen.PrimaryScreen;
                SizeF scaledMonitorSize = screen.Bounds.Size;
                scaledMonitorSize.Width *= 2 / 3.0f;
                scaledMonitorSize.Height *= 0.95f;
                _messageText.UpdateFont();
                SizeF messageSize = g.MeasureString(_text, _messageText.Font, scaledMonitorSize);
                // SKC: Don't forget to add the TextExtra into the calculation
                SizeF captionSize = g.MeasureString($@"{_caption} {TextExtra}", _messageText.Font, scaledMonitorSize);

                var messageXSize = Math.Max(messageSize.Width, captionSize.Width);
                // Work out DPI adjustment factor
                var factorX = g.DpiX > 96 ? (1.0f * g.DpiX / 96) : 1.0f;
                var factorY = g.DpiY > 96 ? (1.0f * g.DpiY / 96) : 1.0f;
                messageSize.Width = messageXSize * factorX;
                messageSize.Height *= factorY;

                // Always add on ad extra 5 pixels as sometimes the measure size does not draw the last 
                // character it contains, this ensures there is always definitely enough space for it all
                messageSize.Width += 5;
                textSize = Size.Ceiling(messageSize);
            }

            return new Size(textSize.Width + _messageIcon.Width + _messageIcon.Margin.Left + _messageIcon.Margin.Right +
                            _messageText.Margin.Left + _messageText.Margin.Right,
                Math.Max(_messageIcon.Height + 10, textSize.Height));
        }

        private Size UpdateButtonsSizing()
        {
            var numButtons = 1;

            // Button1 is always visible
            Size button1Size = _button1.GetPreferredSize(Size.Empty);
            var maxButtonSize = button1Size with { Width = button1Size.Width + GAP };

            // If Button2 is visible
            if (_button2.Enabled)
            {
                numButtons++;
                Size button2Size = _button2.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, button2Size.Width + GAP);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, button2Size.Height);
            }

            // If Button3 is visible
            if (_button3.Enabled)
            {
                numButtons++;
                Size button3Size = _button3.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, button3Size.Width + GAP);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, button3Size.Height);
            }
            // If Button4 is visible
            if (_button4.Enabled)
            {
                numButtons++;
                Size button4Size = _button4.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, button4Size.Width + GAP);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, button4Size.Height);
            }

            // If Action button is visible
            if (_button5.Enabled)
            {
                numButtons++;
                Size actionButtonSize = _button5.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, actionButtonSize.Width + GAP);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, actionButtonSize.Height);
            }

            // Start positioning buttons 10 pixels from right edge
            var right = _panelButtons.Right - GAP;

            var left = _panelButtons.Left - GAP;

            // If Action button is visible
            if (_button5.Enabled)
            {
                _button5.Location = new Point(left - maxButtonSize.Width, GAP);
                _button5.Size = maxButtonSize;
                left -= maxButtonSize.Width + GAP;
            }

            // If Button4 is visible
            if (_button4.Enabled)
            {
                _button4.Location = new Point(right - maxButtonSize.Width, GAP);
                _button4.Size = maxButtonSize;
                right -= maxButtonSize.Width + GAP;
            }

            // If Button3 is visible
            if (_button3.Enabled)
            {
                _button3.Location = new Point(right - maxButtonSize.Width, GAP);
                _button3.Size = maxButtonSize;
                right -= maxButtonSize.Width + GAP;
            }

            // If Button2 is visible
            if (_button2.Enabled)
            {
                _button2.Location = new Point(right - maxButtonSize.Width, GAP);
                _button2.Size = maxButtonSize;
                right -= maxButtonSize.Width + GAP;
            }

            // Button1 is always visible
            _button1.Location = new Point(right - maxButtonSize.Width, GAP);
            _button1.Size = maxButtonSize;

            // Size the panel for the buttons
            _panelButtons.Size = new Size((maxButtonSize.Width * numButtons) + (GAP * (numButtons + 1)), maxButtonSize.Height + (GAP * 2));

            // Button area is the number of buttons with gaps between them and 10 pixels around all edges
            return new Size((maxButtonSize.Width * numButtons) + (GAP * (numButtons + 1)), maxButtonSize.Height + (GAP * 2));
        }

        private void AnyKeyDown(object sender, KeyEventArgs e)
        {
            // Escape key kills the dialog if we allow it to be closed
            if (ControlBox
                && (e.KeyCode == Keys.Escape)
               )
            {
                Close();
            }
            else if (!e.Control
                     || (e.KeyCode != Keys.C)
                    )
            {
                return;
            }

            const string DIVIDER = @"---------------------------";
            const string BUTTON_TEXT_SPACER = @"   ";

            // Pressing Ctrl+C should copy message text into the clipboard
            var sb = new StringBuilder();

            sb.AppendLine(DIVIDER);
            sb.AppendLine(Text);
            sb.AppendLine(DIVIDER);
            sb.AppendLine(_messageText.Text);
            sb.AppendLine(DIVIDER);
            sb.Append(_button1.Text).Append(BUTTON_TEXT_SPACER);
            if (_button2.Enabled)
            {
                sb.Append(_button2.Text).Append(BUTTON_TEXT_SPACER);
                if (_button3.Enabled)
                {
                    sb.Append(_button3.Text).Append(BUTTON_TEXT_SPACER);
                }

                if (_button4.Enabled)
                {
                    sb.Append(_button4.Text).Append(BUTTON_TEXT_SPACER);
                }
            }

            sb.AppendLine(string.Empty);
            sb.AppendLine(DIVIDER);

            Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            Clipboard.SetText(sb.ToString(), TextDataFormat.UnicodeText);
        }

        /// <summary>Setups the action button UI.</summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private void SetupActionButtonUI(bool visible)
        {
            _button5.Visible = visible;

            _button5.Enabled = visible;

            _button5.Click += (sender, args) =>
            {
                try
                {
                    _actionButtonCommand?.PerformExecute();
                }
                catch (Exception e)
                {
                    Debug.Assert(true, e.StackTrace);

                    DialogResult = DialogResult.None;
                }
            };
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (_linkLabelCommand != null)
                {
                    _linkLabelCommand.PerformExecute();
                }
                else if (_linkLaunchArgument != null)
                {
                    Process.Start(_linkLaunchArgument);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.CaptureException(exception);
            }
        }

        /// <summary>Updates the type of the content area.</summary>
        /// <param name="contentAreaType">Type of the content area.</param>
        private void UpdateContentAreaType(MessageBoxContentAreaType? contentAreaType)
        {
            switch (contentAreaType)
            {
                case MessageBoxContentAreaType.Normal:
                    _linkLabelMessageText.Visible = false;

                    _messageText.Visible = true;
                    break;
                case MessageBoxContentAreaType.LinkLabel:
                    _linkLabelMessageText.Visible = true;

                    _messageText.Visible = false;
                    break;
            }
        }

        private void UpdateContentAreaTextAlignment(MessageBoxContentAreaType? contentAreaType, ContentAlignment? messageTextAlignment)
        {
            switch (contentAreaType)
            {
                case MessageBoxContentAreaType.Normal:
                    _messageText.TextAlign = messageTextAlignment ?? ContentAlignment.MiddleLeft;
                    break;
                case MessageBoxContentAreaType.LinkLabel:
                    _linkLabelMessageText.TextAlign = messageTextAlignment ?? ContentAlignment.MiddleLeft;
                    break;
                case null:
                    _messageText.TextAlign = messageTextAlignment ?? ContentAlignment.MiddleLeft;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contentAreaType), contentAreaType, null);
            }
        }

        private void UpdateContentLinkArea(LinkArea? contentLinkArea)
        {
            if (contentLinkArea != null)
            {
                _linkLabelMessageText.LinkArea = (LinkArea)contentLinkArea;
            }
        }

        #endregion
    }

    #region Types
    public class HelpInfo
    {
        #region Identity

        /// <summary>
        /// Initialize a new instance of the HelpInfo class.
        /// </summary>
        /// <param name="helpFilePath">Value for HelpFilePath.</param>
        /// <param name="keyword">Value for Keyword</param>
        public HelpInfo(string? helpFilePath = null, string? keyword = null)
        : this(helpFilePath, keyword, !string.IsNullOrWhiteSpace(keyword) ? HelpNavigator.Topic : HelpNavigator.TableOfContents, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the HelpInfo class.
        /// </summary>
        /// <param name="helpFilePath">Value for HelpFilePath.</param>
        /// <param name="navigator">Value for Navigator</param>
        /// <param name="param"></param>
        public HelpInfo(string? helpFilePath, HelpNavigator navigator, object? param = null)
            : this(helpFilePath, null, navigator, param)
        {
        }

        /// <summary>
        /// Initialize a new instance of the HelpInfo class.
        /// </summary>
        /// <param name="helpFilePath">Value for HelpFilePath.</param>
        /// <param name="navigator">Value for Navigator</param>
        /// <param name="keyword">Value for Keyword</param>
        /// <param name="param"></param>
        private HelpInfo(string? helpFilePath, string? keyword, HelpNavigator navigator, object? param)
        {
            HelpFilePath = helpFilePath ?? string.Empty;
            Keyword = keyword ?? string.Empty;
            Navigator = navigator;
            Param = param;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the HelpFilePath property.
        /// </summary>
        public string HelpFilePath { get; }

        /// <summary>
        /// Gets the Keyword property.
        /// </summary>
        public string Keyword { get; }

        /// <summary>
        /// Gets the Navigator property.
        /// </summary>
        public HelpNavigator Navigator { get; }

        /// <summary>
        /// Gets the Param property.
        /// </summary>
        public object? Param { get; }

        #endregion
    }

    #endregion

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    internal class MessageButton : KryptonButton
    {
        #region Identity
        public MessageButton()
        {
            IgnoreAltF4 = false;
            Visible = false;
            Enabled = false;
        }

        /// <summary>
        /// Gets and sets the ignoring of Alt+F4
        /// </summary>
        public bool IgnoreAltF4 { get; set; }

        #endregion

        #region Protected
        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process. </param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.KEYDOWN:
                case PI.WM_.SYSKEYDOWN:
                    if (IgnoreAltF4)
                    {
                        // Extract the keys being pressed
                        var keys = (Keys)(int)m.WParam.ToInt64();

                        // If the user standard combination ALT + F4
                        if ((keys == Keys.F4) && ((ModifierKeys & Keys.Alt) == Keys.Alt))
                        {
                            // Eat the message, so standard window proc does not close the window
                            return;
                        }
                    }
                    break;
            }

            base.WndProc(ref m);
        }
        #endregion
    }
}
