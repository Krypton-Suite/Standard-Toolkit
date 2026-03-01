#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;
using Timer = System.Windows.Forms.Timer;
using Resources = Krypton.Utilities.Properties.Resources;

namespace Krypton.Utilities;

public partial class VisualRTLMessageBoxExtendedForm : KryptonForm
{
    #region Static Fields

    public const int WH_CALLWNDPROCRET = 12;

    private const int WM_CLOSE = 0x0010;

    #endregion

    #region Instance Fields

    private readonly string _text;
    private readonly string _caption;

    private readonly KryptonMessageBoxDefaultButton _defaultButton;
    // If help information provided or we are not a service/default desktop application then grab an owner for showing the message box
    private static /*readonly*/ IWin32Window? _showOwner;
    private readonly HelpInfo? _helpInfo;
    private readonly ContentAlignment _messageTextAlignment;

    #endregion

    #region Extended Fields

    private readonly bool _showHelpButton;

    private readonly bool _openInExplorer;

    private readonly bool _useTimeOut;

    private readonly bool _showOptionalCheckBox;

    private readonly bool _useOptionalCheckBoxThreeState;

    private bool _optionalCheckBoxChecked;

    private readonly Color _messageTextColour;

    private readonly Color[]? _buttonTextColours = new Color[4];

    private readonly DialogResult _buttonOneCustomDialogResult;

    private readonly DialogResult _buttonTwoCustomDialogResult;

    private readonly DialogResult _buttonThreeCustomDialogResult;

    private readonly DialogResult _buttonFourDialogResult;

    private readonly Font _messageBoxTypeface;

    private readonly ExtendedMessageBoxButtons _buttons;

    private readonly ExtendedKryptonMessageBoxIcon _kryptonMessageBoxIcon;

    private readonly Image? _customKryptonMessageBoxIcon;

    private readonly string _buttonOneCustomText;

    private readonly string _buttonTwoCustomText;

    private readonly string _buttonThreeCustomText;

    private readonly string _buttonFourCustomText;

    private readonly string _applicationPath;

    private readonly string _checkBoxText;

    private readonly ExtendedKryptonMessageBoxMessageContainerType _messageContainerType;

    private readonly KryptonCommand? _linkLabelCommand;

    private readonly LinkArea _contentLinkArea;

    private readonly ProcessStartInfo? _linkLaunchArgument;

    private static PI.HookProc _hookProc;

    private static IntPtr _hHook;

    private Timer _timeOutTimer;

    private int _timeOut;

    private bool _timedOut;

    private DialogResult _result;

    private DialogResult _timerResult;

    private readonly PaletteRelativeAlign _richTextBoxTextAlignment;

    private readonly string? _footerText;

    private readonly bool _footerExpanded;

    private readonly ExtendedKryptonMessageBoxFooterContentType _footerContentType;

    private readonly int? _footerRichTextBoxHeight;

    private readonly ExtendedKryptonMessageBoxCountdownButton _countdownButton;

    private readonly int? _countdownButtonSeconds;

    private readonly DialogResult? _countdownButtonDialogResult;

    #endregion

    #region Identity

    static VisualRTLMessageBoxExtendedForm()
    {
        _hookProc = new PI.HookProc(MessageBoxHookProc);

        _hHook = IntPtr.Zero;
    }

    public VisualRTLMessageBoxExtendedForm()
    {
        InitializeComponent();
    }

    internal VisualRTLMessageBoxExtendedForm(IWin32Window? showOwner, string text, string caption,
        ExtendedMessageBoxButtons buttons,
        ExtendedKryptonMessageBoxIcon icon,
        KryptonMessageBoxDefaultButton defaultButton,
        HelpInfo? helpInfo, bool? showCtrlCopy,
        Font? messageBoxTypeface,
        Image? customKryptonMessageBoxIcon, bool? showHelpButton,
        Color? messageTextColour, Color[]? buttonTextColours,
        DialogResult? buttonOneCustomDialogResult,
        DialogResult? buttonTwoCustomDialogResult,
        DialogResult? buttonThreeCustomDialogResult,
        DialogResult? buttonFourDialogResult,
        string? buttonOneCustomText, string? buttonTwoCustomText,
        string? buttonThreeCustomText, string? buttonFourCustomText,
        string? applicationPath,
        ExtendedKryptonMessageBoxMessageContainerType? messageContainerType,
        KryptonCommand? linkLabelCommand,
        LinkArea? contentLinkArea,
        ProcessStartInfo? linkLaunchArgument,
        bool? openInExplorer,
        ContentAlignment? messageTextAlignment,
        PaletteRelativeAlign? richTextBoxTextAlignment,
        HorizontalAlignment? messageTextBoxAlignment,
        bool? showOptionalCheckBox,
        bool? optionalCheckBoxChecked,
        string? optionalCheckBoxText,
        bool? useOptionalCheckBoxThreeState,
        bool? useTimeOut,
        int? timeOut,
        DialogResult? timerResult,
        string? footerText = null,
        bool footerExpanded = false,
        ExtendedKryptonMessageBoxFooterContentType footerContentType = ExtendedKryptonMessageBoxFooterContentType.Text,
        int? footerRichTextBoxHeight = null,
        ExtendedKryptonMessageBoxCountdownButton countdownButton = ExtendedKryptonMessageBoxCountdownButton.None,
        int? countdownButtonSeconds = null,
        DialogResult? countdownButtonDialogResult = null)
    {
        // Store incoming values
        _text = text;

        _caption = useTimeOut is { } or false ? $"{caption} [{timeOut}]" : caption;

        _buttons = buttons;
        _kryptonMessageBoxIcon = icon;
        _defaultButton = defaultButton;
        _helpInfo = helpInfo ?? new HelpInfo(string.Empty, HelpNavigator.AssociateIndex, null);
        _showOwner = showOwner;
        _messageTextAlignment = messageTextAlignment ?? ContentAlignment.MiddleLeft;

        // Extended values
        _messageBoxTypeface = messageBoxTypeface ?? KryptonManager.CurrentGlobalPalette.BaseFont;
        _customKryptonMessageBoxIcon = customKryptonMessageBoxIcon;
        _showHelpButton = showHelpButton ?? false;
        _messageTextColour = messageTextColour ?? Color.Empty;
        _buttonTextColours = buttonTextColours;
        _buttonOneCustomDialogResult = buttonOneCustomDialogResult ?? DialogResult.Yes;
        _buttonTwoCustomDialogResult = buttonTwoCustomDialogResult ?? DialogResult.No;
        _buttonThreeCustomDialogResult = buttonThreeCustomDialogResult ?? DialogResult.Cancel;
        _buttonFourDialogResult = buttonFourDialogResult ?? DialogResult.Retry;
        _buttonOneCustomText = buttonOneCustomText ?? KryptonManager.Strings.GeneralStrings.Yes;
        _buttonTwoCustomText = buttonTwoCustomText ?? KryptonManager.Strings.GeneralStrings.No;
        _buttonThreeCustomText = buttonThreeCustomText ?? KryptonManager.Strings.GeneralStrings.Cancel;
        _buttonFourCustomText = buttonFourCustomText ?? KryptonManager.Strings.GeneralStrings.Retry;
        _applicationPath = applicationPath ?? string.Empty;
        _messageContainerType = messageContainerType ?? ExtendedKryptonMessageBoxMessageContainerType.Normal;
        _linkLabelCommand = linkLabelCommand ?? new KryptonCommand();
        _contentLinkArea = contentLinkArea ?? new LinkArea(0, text.Length);
        _linkLaunchArgument = linkLaunchArgument ?? new ProcessStartInfo();
        _openInExplorer = openInExplorer ?? false;
        _richTextBoxTextAlignment = richTextBoxTextAlignment ?? PaletteRelativeAlign.Inherit;
        _useTimeOut = useTimeOut ?? false;
        _timeOut = timeOut ?? 60;
        //_timeOutTimer = new Timer(OnTimerElapsed, null, _timeOut, Timeout.Infinite);
        _timerResult = timerResult ?? DialogResult.None;
        //_openInExplorer = openInExplorer ?? false;

        // Optional checkbox
        _showOptionalCheckBox = showOptionalCheckBox ?? false;
        _optionalCheckBoxChecked = optionalCheckBoxChecked ?? false;
        _checkBoxText = optionalCheckBoxText ?? string.Empty;
        _useOptionalCheckBoxThreeState = useOptionalCheckBoxThreeState ?? false;
        _footerText = footerText;
        _footerExpanded = footerExpanded;
        _footerContentType = footerContentType;
        _footerRichTextBoxHeight = footerRichTextBoxHeight;
        _countdownButton = countdownButton;
        _countdownButtonSeconds = countdownButtonSeconds;
        _countdownButtonDialogResult = countdownButtonDialogResult;

        // Create the form contents
        InitializeComponent();

        // Update contents to match requirements
        UpdateText();
        UpdateIcon();
        UpdateButtons();
        UpdateDefault();
        UpdateHelp();
        UpdateTextExtra(showCtrlCopy);
        
        // Apply countdown to selected button if specified
        ApplyCountdownToButton();

        UpdateContentAreaType(messageContainerType, messageTextAlignment, richTextBoxTextAlignment, messageTextBoxAlignment);

        UpdateContentLinkArea(contentLinkArea);

        SetupOptionalCheckBox();

        SetupFooter(_footerText, _footerExpanded, _footerContentType, _footerRichTextBoxHeight);

        // Finally calculate and set form sizing
        UpdateSizing(showOwner);

        if (_useTimeOut)
        {
            using (_timeOutTimer)
            {
                _result = KryptonMessageBoxExtended.Show(text, caption, buttons, icon, showCtrlCopy,
                    messageTextAlignment, messageTextBoxAlignment, useTimeOut, null,
                    null);
            }

            if (_timedOut)
            {
                _result = _timerResult;
            }
        }
    }

    #endregion

    #region Implementation

    private void UpdateText()
    {
        Text = string.IsNullOrEmpty(_caption) ? string.Empty : _caption.Split(Environment.NewLine.ToCharArray())[0];

        if (_messageContainerType == ExtendedKryptonMessageBoxMessageContainerType.Normal)
        {
            kwlblMessageText.Visible = true;
            kwlblMessageText.Text = _text;
            kwlblMessageText.StateCommon.Content.Font = _messageBoxTypeface;

            kwlblMessageText.StateCommon.Content.Color1 = _messageTextColour;

            krtbMessageText.Visible = false;

            klwlblMessageText.Visible = false;
        }
        else if (_messageContainerType == ExtendedKryptonMessageBoxMessageContainerType.RichTextBox)
        {
            krtbMessageText.Visible = true;

            krtbMessageText.Text = _text;

            krtbMessageText.StateCommon.Content.Color1 = _messageTextColour;

            krtbMessageText.StateCommon.Content.Font = _messageBoxTypeface;

            kwlblMessageText.Visible = false;

            klwlblMessageText.Visible = false;
        }
        else if (_messageContainerType == ExtendedKryptonMessageBoxMessageContainerType.HyperLink)
        {
            klwlblMessageText.Visible = true;

            klwlblMessageText.Text = _text;

            klwlblMessageText.StateCommon.TextColor = _messageTextColour;

            klwlblMessageText.StateCommon.Font = _messageBoxTypeface;

            kwlblMessageText.Visible = false;

            krtbMessageText.Visible = false;
        }

        kcbOptionalCheckBox.StateCommon.ShortText.Color1 = _messageTextColour;

        kcbOptionalCheckBox.StateCommon.ShortText.Color2 = _messageTextColour;

        kcbOptionalCheckBox.StateCommon.ShortText.Font = _messageBoxTypeface;
    }

    private void UpdateTextExtra(bool? showCtrlCopy)
    {
        if (!showCtrlCopy.HasValue)
        {
            switch (_kryptonMessageBoxIcon)
            {
                case ExtendedKryptonMessageBoxIcon.Error:
                case ExtendedKryptonMessageBoxIcon.Exclamation:
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
        switch (_kryptonMessageBoxIcon)
        {
            case ExtendedKryptonMessageBoxIcon.Custom:
                if (_customKryptonMessageBoxIcon != null)
                {
                    _messageIcon.Image = _customKryptonMessageBoxIcon;
                }
                else
                {
                    _messageIcon.Image = SystemIcons.Application.ToBitmap();
                }
                break;
            case ExtendedKryptonMessageBoxIcon.None:
                // Windows XP and before will Beep, Vista and above do not!
                if (GlobalStaticValues.OS_MAJOR_VERSION < 6)
                {
                    SystemSounds.Beep.Play();
                }
                break;
            case ExtendedKryptonMessageBoxIcon.Hand:
                _messageIcon.Image = Resources.Hand;
                SystemSounds.Hand.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.SystemHand:
                _messageIcon.Image = SystemIcons.Hand.ToBitmap();
                SystemSounds.Hand.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Question:
                _messageIcon.Image = Resources.Question;
                SystemSounds.Question.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.SystemQuestion:
                _messageIcon.Image = SystemIcons.Question.ToBitmap();
                SystemSounds.Question.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Exclamation:
                _messageIcon.Image = Resources.Warning;
                SystemSounds.Exclamation.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.SystemExclamation:
                _messageIcon.Image = SystemIcons.Exclamation.ToBitmap();
                SystemSounds.Exclamation.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Asterisk:
                _messageIcon.Image = Resources.Asterisk;
                SystemSounds.Asterisk.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.SystemAsterisk:
                _messageIcon.Image = SystemIcons.Asterisk.ToBitmap();
                SystemSounds.Asterisk.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Stop:
                _messageIcon.Image = Resources.Stop;
                SystemSounds.Hand.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Error:
                _messageIcon.Image = Resources.Critical;
                SystemSounds.Hand.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Warning:
                _messageIcon.Image = Resources.Warning;
                SystemSounds.Exclamation.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Information:
                _messageIcon.Image = Resources.Information;
                SystemSounds.Asterisk.Play();
                break;
            case ExtendedKryptonMessageBoxIcon.Shield:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    _messageIcon.Image = Resources.UAC_Shield_Windows_11;
                }
                // Windows 10
                else if (OSUtilities.IsWindowsTen)
                {
                    _messageIcon.Image = Resources.UAC_Shield_Windows_10;
                }
                else
                {
                    _messageIcon.Image = Resources.UAC_Shield_Windows_7;
                }
                break;
            case ExtendedKryptonMessageBoxIcon.WindowsLogo:
                // Because Windows 11 displays a generic application icon,
                // we need to rely on a image instead
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    _messageIcon.Image = Resources.Windows11;
                }
                // Windows 10, 8.1 & 8
                else if (OSUtilities.IsWindowsTen || OSUtilities.IsWindowsEightPointOne || OSUtilities.IsWindowsEight)
                {
                    _messageIcon.Image = Resources.Windows_8_and_10_Logo;
                }
                else
                {
                    _messageIcon.Image = SystemIcons.WinLogo.ToBitmap();
                }
                break;
            case ExtendedKryptonMessageBoxIcon.Application:
                if (!string.IsNullOrEmpty(_applicationPath))
                {
                    Image tempImage = GraphicsExtensions.ExtractIconFromFilePath(_applicationPath)?.ToBitmap()!;
                    Bitmap? scaledImage = GraphicsExtensions.ScaleImage(tempImage, new Size(32, 32));

                    _messageIcon.Image = scaledImage;
                }
                else
                {
                    _messageIcon.Image = SystemIcons.Application.ToBitmap();
                }
                break;
            case ExtendedKryptonMessageBoxIcon.SystemApplication:
                _messageIcon.Image = SystemIcons.Application.ToBitmap();
                break;
        }

        _messageIcon.Visible = _kryptonMessageBoxIcon != ExtendedKryptonMessageBoxIcon.None;

    }

    private void UpdateButtons()
    {
        switch (_buttons)
        {
            case ExtendedMessageBoxButtons.OK:
                _button1.Text = KryptonManager.Strings.GeneralStrings.OK;
                _button1.DialogResult = DialogResult.OK;
                _button1.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button1.Visible = true;
                _button1.Enabled = true;
                break;
            case ExtendedMessageBoxButtons.OKCancel:
                _button1.Text = KryptonManager.Strings.GeneralStrings.OK;
                _button2.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                _button1.DialogResult = DialogResult.OK;
                _button2.DialogResult = DialogResult.Cancel;
                _button1.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button2.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button1.Visible = true;
                _button1.Enabled = true;
                _button2.Visible = true;
                _button2.Enabled = true;
                break;
            case ExtendedMessageBoxButtons.YesNo:
                _button1.Text = KryptonManager.Strings.GeneralStrings.Yes;
                _button2.Text = KryptonManager.Strings.GeneralStrings.No;
                _button1.DialogResult = DialogResult.Yes;
                _button2.DialogResult = DialogResult.No;
                _button1.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button2.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button1.Visible = true;
                _button1.Enabled = true;
                _button2.Visible = true;
                _button2.Enabled = true;
                ControlBox = false;
                break;
            case ExtendedMessageBoxButtons.YesNoCancel:
                _button1.Text = KryptonManager.Strings.GeneralStrings.Yes;
                _button2.Text = KryptonManager.Strings.GeneralStrings.No;
                _button3.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                _button1.DialogResult = DialogResult.Yes;
                _button2.DialogResult = DialogResult.No;
                _button3.DialogResult = DialogResult.Cancel;
                _button1.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button2.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button3.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button1.Visible = true;
                _button1.Enabled = true;
                _button2.Visible = true;
                _button2.Enabled = true;
                _button3.Visible = true;
                _button3.Enabled = true;
                break;
            case ExtendedMessageBoxButtons.RetryCancel:
                _button1.Text = KryptonManager.Strings.GeneralStrings.Retry;
                _button2.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                _button1.DialogResult = DialogResult.Retry;
                _button2.DialogResult = DialogResult.Cancel;
                _button1.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button2.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button1.Visible = true;
                _button1.Enabled = true;
                _button2.Visible = true;
                _button2.Enabled = true;
                break;
            case ExtendedMessageBoxButtons.AbortRetryIgnore:
                _button1.Text = KryptonManager.Strings.GeneralStrings.Abort;
                _button2.Text = KryptonManager.Strings.GeneralStrings.Retry;
                _button3.Text = KryptonManager.Strings.GeneralStrings.Ignore;
                _button1.DialogResult = DialogResult.Abort;
                _button2.DialogResult = DialogResult.Retry;
                _button3.DialogResult = DialogResult.Ignore;
                _button1.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button2.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button3.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button1.Visible = true;
                _button1.Enabled = true;
                _button2.Visible = true;
                _button2.Enabled = true;
                _button3.Visible = true;
                _button3.Enabled = true;
                ControlBox = false;
                break;
            case ExtendedMessageBoxButtons.CancelTryContinue:
                _button1.Text = KryptonManager.Strings.GeneralStrings.Cancel;
                _button2.Text = KryptonManager.Strings.GeneralStrings.TryAgain;
                _button3.Text = KryptonManager.Strings.GeneralStrings.Continue;
                _button1.DialogResult = DialogResult.Cancel;
#if NET6_0_OR_GREATER
                    _button2.DialogResult = DialogResult.TryAgain;
                    _button3.DialogResult = DialogResult.Continue;
#else
                _button2.DialogResult = (DialogResult)10;
                _button2.DialogResult = (DialogResult)11;
#endif
                _button1.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button2.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button3.StateCommon.Content.ShortText.Font = _messageBoxTypeface;
                _button1.Visible = true;
                _button1.Enabled = true;
                _button2.Visible = true;
                _button2.Enabled = true;
                _button3.Visible = true;
                _button3.Enabled = true;
                break;
        }

        // Do we ignore the Alt+F4 on the buttons?
        if (!ControlBox)
        {
            _button1.IgnoreAltF4 = true;
            _button2.IgnoreAltF4 = true;
            _button3.IgnoreAltF4 = true;
            _button4.IgnoreAltF4 = true;
        }
    }

    /// <summary>Applies countdown functionality to the selected button.</summary>
    private void ApplyCountdownToButton()
    {
        if (_countdownButton == ExtendedKryptonMessageBoxCountdownButton.None)
        {
            return;
        }

        MessageButton? targetButton = _countdownButton switch
        {
            ExtendedKryptonMessageBoxCountdownButton.Button1 => _button1,
            ExtendedKryptonMessageBoxCountdownButton.Button2 => _button2,
            ExtendedKryptonMessageBoxCountdownButton.Button3 => _button3,
            ExtendedKryptonMessageBoxCountdownButton.Button4 => _button4,
            _ => null
        };

        if (targetButton != null && targetButton.Visible)
        {
            // Configure countdown values
            // Use countdownButtonSeconds if specified, otherwise fall back to timeout value, otherwise default to 60
            int countdownDuration = _countdownButtonSeconds ?? (_useTimeOut ? _timeOut : 60);
            targetButton.CountdownButtonValues.CountdownDuration = countdownDuration;
            targetButton.CountdownButtonValues.CountdownInterval = 1000; // Default interval for RTL form
            
            // Start the countdown
            targetButton.StartCountdown();
            
            // Handle countdown finished event
            targetButton.CountdownFinished += (sender, e) =>
            {
                // If a specific dialog result was specified, close the dialog with that result
                if (_countdownButtonDialogResult.HasValue)
                {
                    DialogResult = _countdownButtonDialogResult.Value;
                    Close();
                }
                // Otherwise, the button is automatically enabled and user can click it normally
            };
        }
    }

    private void UpdateDefault(KryptonMessageBoxDefaultButton? defaultButton)
    {
        switch (defaultButton)
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
            //case KryptonMessageBoxDefaultButton.Button5:
            //    AcceptButton = _showActionButton ? _button5 : _button1;
            //    break;
            case null:
                AcceptButton = _button1;
                break;
            default:
                AcceptButton = _showHelpButton ? _button4 : _button1;
                break;
        }
    }

    private void UpdateDefault()
    {
        switch (_defaultButton)
        {
            case KryptonMessageBoxDefaultButton.Button1:
                _button1.Focus();
                break;
            case KryptonMessageBoxDefaultButton.Button2:
                _button2.Focus();
                break;
            case KryptonMessageBoxDefaultButton.Button3:
                _button3.Focus();
                break;
            case KryptonMessageBoxDefaultButton.Button4:
                _button4.Focus();
                break;
        }
    }

    private void UpdateHelp()
    {
        if (_helpInfo == null)
        {
            return;
        }

        MessageButton helpButton = _buttons switch
        {
            ExtendedMessageBoxButtons.OK => _button2,
            ExtendedMessageBoxButtons.OKCancel or ExtendedMessageBoxButtons.YesNo or ExtendedMessageBoxButtons.RetryCancel => _button3,
            ExtendedMessageBoxButtons.AbortRetryIgnore or ExtendedMessageBoxButtons.YesNoCancel => _button4,
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
            Control? control = FromHandle(_showOwner!.Handle);

            MethodInfo? mInfoMethod = control!.GetType().GetMethod(@"OnHelpRequested", BindingFlags.Instance | BindingFlags.NonPublic,
                Type.DefaultBinder, [typeof(HelpEventArgs)], null)!;
            if (mInfoMethod != null)
            {
                mInfoMethod.Invoke(control, [new HelpEventArgs(MousePosition)]);
            }
            if (_helpInfo != null)
            {
                if (string.IsNullOrWhiteSpace(_helpInfo.HelpFilePath))
                {
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(_helpInfo!.Keyword))
            {
                Help.ShowHelp(control, _helpInfo.HelpFilePath, _helpInfo.Keyword);
            }
            else
            {
                Help.ShowHelp(control, _helpInfo.HelpFilePath, _helpInfo.Navigator, _helpInfo.Param);
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
        Size footerSizing = UpdateFooterSizing();

        // Size of window is calculated from the client area
        ClientSize = new Size(Math.Max(Math.Max(messageSizing.Width, buttonsSizing.Width), footerSizing.Width),
            messageSizing.Height + buttonsSizing.Height + footerSizing.Height);
    }

    /// <summary>
    /// Updates the footer panel sizing based on its content and expanded state.
    /// </summary>
    /// <returns>The size of the footer panel.</returns>
    private Size UpdateFooterSizing()
    {
        if (!_panelFooter.Visible)
        {
            return Size.Empty;
        }

        // Calculate width to match message box width
        int footerWidth = Math.Max(UpdateMessageSizing(Owner).Width, UpdateButtonsSizing().Width);

        // Height is already set in UpdateFooterExpandedState, but ensure minimum width
        _panelFooter.Width = footerWidth;
        int contentWidth = footerWidth - 20; // Account for padding

        // Update width for all content controls
        _footerWrapLabel.Width = contentWidth;
        _footerCheckBox.Width = contentWidth;
        _footerRichTextBox.Width = contentWidth;

        return new Size(footerWidth, _panelFooter.Height);
    }

    private Size UpdateMessageSizing(IWin32Window? showOwner)
    {
        // Update size of the message label but with a maximum width
        Size textSize;
        using (Graphics g = CreateGraphics())
        {
            // Find size of the label, with a max of 2/3 screen width
            Screen? screen = showOwner != null ? Screen.FromHandle(showOwner.Handle) : Screen.PrimaryScreen;
            SizeF scaledMonitorSize = screen!.Bounds.Size;
            scaledMonitorSize.Width *= 2 / 3.0f;
            scaledMonitorSize.Height *= 0.95f;

            //kwlblMessageText.UpdateFont();
            SizeF messageSize = g.MeasureString(_text, kwlblMessageText.Font, scaledMonitorSize);
            // SKC: Don't forget to add the TextExtra into the calculation
            SizeF captionSize = g.MeasureString($@"{_caption} {TextExtra}", kwlblMessageText.Font, scaledMonitorSize);

            var messageXSize = Math.Max(messageSize.Width, captionSize.Width);
            // Work out DPI adjustment factor
            messageSize.Width = messageXSize * FactorDpiX;
            messageSize.Height *= FactorDpiY;

            // Always add on ad extra 5 pixels as sometimes the measure size does not draw the last 
            // character it contains, this ensures there is always definitely enough space for it all
            messageSize.Width += 5;
            textSize = Size.Ceiling(messageSize);
        }

        // Find size of icon area plus the text area added together
        if (_messageIcon.Image != null)
        {
            return new Size(textSize.Width + _messageIcon.Width, Math.Max(_messageIcon.Height + 10, textSize.Height));
        }

        return textSize;
    }

    private Size UpdateButtonsSizing()
    {
        var numButtons = 1;

        // Button1 is always visible
        Size button1Size = _button1.GetPreferredSize(Size.Empty);
        Size maxButtonSize = new(button1Size.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING, button1Size.Height);

        // If Button2 is visible
        if (_button2.Enabled)
        {
            numButtons++;
            Size button2Size = _button2.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, button2Size.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, button2Size.Height);
        }

        // If Button3 is visible
        if (_button3.Enabled)
        {
            numButtons++;
            Size button3Size = _button3.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, button3Size.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, button3Size.Height);
        }
        // If Button4 is visible
        if (_button4.Enabled)
        {
            numButtons++;
            Size button4Size = _button4.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, button4Size.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, button4Size.Height);
        }

        // Start positioning buttons 10 pixels from right edge
        var right = _panelButtons.Right - GlobalStaticValues.GLOBAL_BUTTON_PADDING;

        // If Button4 is visible
        if (_button4.Enabled)
        {
            _button4.Location = new Point(right - maxButtonSize.Width, GlobalStaticValues.GLOBAL_BUTTON_PADDING);
            _button4.Size = maxButtonSize;
            right -= maxButtonSize.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING;
        }

        // If Button3 is visible
        if (_button3.Enabled)
        {
            _button3.Location = new Point(right - maxButtonSize.Width, GlobalStaticValues.GLOBAL_BUTTON_PADDING);
            _button3.Size = maxButtonSize;
            right -= maxButtonSize.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING;
        }

        // If Button2 is visible
        if (_button2.Enabled)
        {
            _button2.Location = new Point(right - maxButtonSize.Width, GlobalStaticValues.GLOBAL_BUTTON_PADDING);
            _button2.Size = maxButtonSize;
            right -= maxButtonSize.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING;
        }

        // Button1 is always visible
        _button1.Location = new Point(right - maxButtonSize.Width, GlobalStaticValues.GLOBAL_BUTTON_PADDING);
        _button1.Size = maxButtonSize;

        // Size the panel for the buttons
        _panelButtons.Size = new Size(maxButtonSize.Width * numButtons + GlobalStaticValues.GLOBAL_BUTTON_PADDING * (numButtons + 1), maxButtonSize.Height + GlobalStaticValues.GLOBAL_BUTTON_PADDING * 2);

        // Button area is the number of buttons with GLOBAL_BUTTON_PADDINGs between them and 10 pixels around all edges
        return new Size(maxButtonSize.Width * numButtons + GlobalStaticValues.GLOBAL_BUTTON_PADDING * (numButtons + 1), maxButtonSize.Height + GlobalStaticValues.GLOBAL_BUTTON_PADDING * 2);
    }

    private void AnyKeyDown(object sender, KeyEventArgs e)
    {
        // Escape key kills the dialog if we allow it to be closed
        if (ControlBox
            && e.KeyCode == Keys.Escape
           )
        {
            Close();
        }
        else if (!e.Control
                 || e.KeyCode != Keys.C
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
        sb.AppendLine(kwlblMessageText.Text);
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

    private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            if (_openInExplorer)
            {
                var linkData = e.Link?.LinkData;
                if (linkData != null)
                {
                    OpenInExplorer(linkData.ToString() ?? string.Empty);
                }
            }
            else
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
        }
        catch (Exception exc)
        {
            KryptonExceptionDialog.Show(exc);
        }
    }

    private void OpenInExplorer(string path)
    {
        try
        {
            Process.Start(@"explorer.exe", path);
        }
        catch (Exception e)
        {
            KryptonExceptionDialog.Show(e);
        }
    }

    private void UpdateContentLinkArea(LinkArea? contentLinkArea)
    {
        if (contentLinkArea != null)
        {
            klwlblMessageText.LinkArea = (LinkArea)contentLinkArea;
        }
    }

    private void UpdateContentAreaType(ExtendedKryptonMessageBoxMessageContainerType? messageContainerType, ContentAlignment? messageTextAlignment, PaletteRelativeAlign? richTextBoxTextAlignment, HorizontalAlignment? messageTextBoxAlignment)
    {
        switch (messageContainerType)
        {
            case ExtendedKryptonMessageBoxMessageContainerType.HyperLink:
                klwlblMessageText.Visible = true;

                kwlblMessageText.Visible = false;

                klwlblMessageText.TextAlign = messageTextAlignment ?? ContentAlignment.MiddleLeft;

                krtbMessageText.Visible = false;
                break;
            case ExtendedKryptonMessageBoxMessageContainerType.Normal:
                klwlblMessageText.Visible = false;

                kwlblMessageText.Visible = true;

                kwlblMessageText.TextAlign = messageTextBoxAlignment ?? HorizontalAlignment.Left;

                krtbMessageText.Visible = false;
                break;
            case ExtendedKryptonMessageBoxMessageContainerType.RichTextBox:
                klwlblMessageText.Visible = false;

                kwlblMessageText.Visible = false;

                krtbMessageText.Visible = true;

                krtbMessageText.StateCommon.Content.TextH = richTextBoxTextAlignment ?? PaletteRelativeAlign.Inherit;
                break;
        }
    }

    private static void Initialize()
    {
        if (_hHook != IntPtr.Zero)
        {
            throw new NotSupportedException("multiple calls are not supported");
        }

        if (_showOwner != null)
        {
            _hHook = PlatformEvents.SetWindowsHookEx(WH_CALLWNDPROCRET, _hookProc, IntPtr.Zero, Thread.CurrentThread.ManagedThreadId);
        }
    }

    private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode < 0)
        {
            return PlatformEvents.CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT))!;
        IntPtr hook = _hHook;

        if (msg.message == (int)CbtHookAction.HCBT_ACTIVATE)
        {
            try
            {
                CenterWindow(msg.hwnd);
            }
            finally
            {
                PlatformEvents.UnhookWindowsHookEx(_hHook);
                _hHook = IntPtr.Zero;
            }
        }

        return PlatformEvents.CallNextHookEx(hook, nCode, wParam, lParam);
    }

    private static void CenterWindow(IntPtr hChildWnd)
    {
        Rectangle recChild = new Rectangle(0, 0, 0, 0);
        bool success = PlatformEvents.GetWindowRect(hChildWnd, ref recChild);

        int width = recChild.Width - recChild.X;
        int height = recChild.Height - recChild.Y;

        Rectangle recParent = new Rectangle(0, 0, 0, 0);
        success = PlatformEvents.GetWindowRect(_showOwner!.Handle, ref recParent);

        Point ptCenter = new Point(0, 0);
        ptCenter.X = recParent.X + (recParent.Width - recParent.X) / 2;
        ptCenter.Y = recParent.Y + (recParent.Height - recParent.Y) / 2;


        Point ptStart = new Point(0, 0);
        ptStart.X = ptCenter.X - width / 2;
        ptStart.Y = ptCenter.Y - height / 2;

        ptStart.X = ptStart.X < 0 ? 0 : ptStart.X;
        ptStart.Y = ptStart.Y < 0 ? 0 : ptStart.Y;

        int result = PlatformEvents.MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width,
            height, false);
    }

    private void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = PlatformEvents.FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
        if (mbWnd != IntPtr.Zero)
        {
            PlatformEvents.SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        _timeOutTimer.Dispose();

        _timedOut = true;
    }

    #region Checkbox
    private void SetupOptionalCheckBox()
    {
        kcbOptionalCheckBox.Visible = _showOptionalCheckBox;

        kcbOptionalCheckBox.Checked = _optionalCheckBoxChecked;

        kcbOptionalCheckBox.Text = _checkBoxText;

        kcbOptionalCheckBox.ThreeState = _useOptionalCheckBoxThreeState;
    }

    /// <summary>
    /// Sets up the expandable footer with the specified content and initial expanded state.
    /// </summary>
    /// <param name="footerText">The text content to display in the footer. If null or empty, footer will not be shown (unless footerContentType is CheckBox).</param>
    /// <param name="expanded">If true, the footer will be expanded initially; otherwise, it will be collapsed.</param>
    /// <param name="contentType">The type of content to display in the footer (Text, CheckBox, or RichTextBox).</param>
    /// <param name="richTextBoxHeight">The height for the RichTextBox when contentType is RichTextBox. If null, uses default height.</param>
    private void SetupFooter(string? footerText, bool expanded, ExtendedKryptonMessageBoxFooterContentType contentType, int? richTextBoxHeight)
    {
        bool showFooter = !string.IsNullOrEmpty(footerText) || contentType == ExtendedKryptonMessageBoxFooterContentType.CheckBox;
        _panelFooter.Visible = showFooter;
        _footerToggleButton.Visible = showFooter;

        if (!showFooter)
        {
            _panelFooter.Height = 0;
            return;
        }

        // Hide all footer content controls initially
        _footerWrapLabel.Visible = false;
        _footerCheckBox.Visible = false;
        _footerRichTextBox.Visible = false;

        // Configure based on content type
        switch (contentType)
        {
            case ExtendedKryptonMessageBoxFooterContentType.Text:
                _footerWrapLabel.Text = footerText ?? string.Empty;
                if (_messageBoxTypeface != null)
                {
                    _footerWrapLabel.StateCommon.Font = _messageBoxTypeface;
                }
                break;

            case ExtendedKryptonMessageBoxFooterContentType.CheckBox:
                _footerCheckBox.Text = footerText ?? string.Empty;
                if (_messageBoxTypeface != null)
                {
                    _footerCheckBox.StateCommon.ShortText.Font = _messageBoxTypeface;
                }
                break;

            case ExtendedKryptonMessageBoxFooterContentType.RichTextBox:
                _footerRichTextBox.Text = footerText ?? string.Empty;
                if (_messageBoxTypeface != null)
                {
                    _footerRichTextBox.StateCommon.Content.Font = _messageBoxTypeface;
                }
                // Set RichTextBox height if specified
                if (richTextBoxHeight.HasValue && richTextBoxHeight.Value > 0)
                {
                    _footerRichTextBox.Height = richTextBoxHeight.Value;
                }
                break;
        }

        // Set initial expanded state
        UpdateFooterExpandedState(expanded, contentType);
    }

    /// <summary>
    /// Updates the footer expanded state, adjusting visibility and toggle button text.
    /// </summary>
    /// <param name="expanded">If true, footer is expanded; otherwise, collapsed.</param>
    /// <param name="contentType">The type of content displayed in the footer.</param>
    private void UpdateFooterExpandedState(bool expanded, ExtendedKryptonMessageBoxFooterContentType contentType)
    {
        if (!_panelFooter.Visible)
        {
            return;
        }

        // Hide all content controls first
        _footerWrapLabel.Visible = false;
        _footerCheckBox.Visible = false;
        _footerRichTextBox.Visible = false;

        // Show the appropriate content control based on type and expanded state
        if (expanded)
        {
            switch (contentType)
            {
                case ExtendedKryptonMessageBoxFooterContentType.Text:
                    _footerWrapLabel.Visible = true;
                    break;
                case ExtendedKryptonMessageBoxFooterContentType.CheckBox:
                    _footerCheckBox.Visible = true;
                    break;
                case ExtendedKryptonMessageBoxFooterContentType.RichTextBox:
                    _footerRichTextBox.Visible = true;
                    break;
            }
        }

        // Update toggle button text
        _footerToggleButton.Values.Text = expanded ? @"Hide details" : @"Show details";

        // Calculate footer height based on expanded state and content type
        if (expanded)
        {
            int contentHeight = 0;
            switch (contentType)
            {
                case ExtendedKryptonMessageBoxFooterContentType.Text:
                    // Measure the footer text to determine required height
                    using (Graphics g = CreateGraphics())
                    {
                        Font footerFont = _footerWrapLabel.Font ?? _messageBoxTypeface ?? KryptonManager.CurrentGlobalPalette.BaseFont;
                        SizeF textSize = g.MeasureString(_footerWrapLabel.Text, footerFont, _footerWrapLabel.Width);
                        contentHeight = (int)Math.Ceiling(textSize.Height);
                    }
                    break;
                case ExtendedKryptonMessageBoxFooterContentType.CheckBox:
                    contentHeight = _footerCheckBox.Height;
                    break;
                case ExtendedKryptonMessageBoxFooterContentType.RichTextBox:
                    contentHeight = _footerRichTextBox.Height;
                    break;
            }
            int footerHeight = contentHeight + 40; // Add padding for toggle button and borders
            _panelFooter.Height = Math.Max(footerHeight, 50); // Minimum height
        }
        else
        {
            // Collapsed state - just show the toggle button
            _panelFooter.Height = 30;
        }

        // Recalculate form size (owner can be null, UpdateSizing handles it)
        IWin32Window? owner = Owner;
        UpdateSizing(owner);
    }

    /// <summary>
    /// Handles the footer toggle button click event to expand or collapse the footer.
    /// </summary>
    private void FooterToggleButton_Click(object sender, EventArgs e)
    {
        bool currentExpanded = _footerWrapLabel.Visible || _footerCheckBox.Visible || _footerRichTextBox.Visible;
        UpdateFooterExpandedState(!currentExpanded, _footerContentType);
    }

    internal static bool ReturnCheckBoxCheckedValue()
    {
        VisualRTLMessageBoxExtendedForm messageBoxExtendedForm = new VisualRTLMessageBoxExtendedForm();

        return messageBoxExtendedForm._optionalCheckBoxChecked;
    }

    internal static CheckState ReturnCheckBoxCheckState()
    {
        VisualRTLMessageBoxExtendedForm messageBoxExtendedForm = new VisualRTLMessageBoxExtendedForm();

        return messageBoxExtendedForm.kcbOptionalCheckBox.CheckState;
    }

    private void OptionalCheckBox_CheckedChanged(object sender, EventArgs e) => _optionalCheckBoxChecked = kcbOptionalCheckBox.Checked;

    private void UpdateCloseButtonVisibility(bool? visible) => CloseBox = visible ?? true;

    #endregion

    #endregion
}
