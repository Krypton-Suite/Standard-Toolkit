﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Local

namespace Krypton.Toolkit
{
    internal partial class VisualMessageBoxForm : KryptonForm
    {

        #region Instance Fields

        private readonly bool _showHelpButton;
        private readonly string? _text;
        private readonly string? _caption;
        private readonly KryptonMessageBoxButtons _buttons;
        private readonly KryptonMessageBoxIcon _kryptonMessageBoxIcon;
        private readonly KryptonMessageBoxDefaultButton _defaultButton;

        // If help information provided, or we are not a service/default desktop application then grab an owner for showing the message box
        private readonly IWin32Window? _showOwner;
        private readonly HelpInfo? _helpInfo;
        private readonly KryptonMessageBoxNativeWindow _krtbNativeWindow;

        #endregion

        #region Public

        public KryptonMessageBoxResult MessageBoxResult { get; set; }

        #endregion

        #region Identity

        public VisualMessageBoxForm()
        {
            InitializeComponent();
        }

        internal VisualMessageBoxForm(IWin32Window? showOwner, string? text, string? caption,
                                       KryptonMessageBoxButtons buttons,
                                       KryptonMessageBoxIcon icon,
                                       KryptonMessageBoxDefaultButton defaultButton,
                                       HelpInfo? helpInfo, bool? showCtrlCopy,
                                       bool? showHelpButton,
                                       bool? showCloseButton)
        {
            // Store incoming values
            _text = CommonHelper.NormalizeLineBreaks(text ?? string.Empty);
            _caption = caption;
            _buttons = buttons;
            _kryptonMessageBoxIcon = icon;
            _defaultButton = defaultButton;
            _helpInfo = helpInfo;
            _showOwner = showOwner;
            _showHelpButton = showHelpButton ?? (helpInfo != null);
            _krtbNativeWindow = new();

            // Create the form contents
            InitializeComponent();

            // Hookup the native window on the KRTB, only after IntializeComponent().
            _krtbNativeWindow.AssignHandle(krtbMessageText.RichTextBox.Handle);

            // Default Cursor for the KRTB when the cursors hovers over it
            krtbMessageText.RichTextBox.Cursor = Cursors.Arrow;

            // Update contents to match requirements
            UpdateText();
            UpdateIcon();
            UpdateButtons();
            UpdateDefault();
            UpdateHelp();
            UpdateTextExtra(showCtrlCopy);
            // Finally calculate and set form sizing
            UpdateSizing(showOwner);

            ShowCloseButton(showCloseButton);
        }

        #endregion Identity

        #region Implementation

        private void UpdateText()
        {
            Text = string.IsNullOrEmpty(_caption)
                ? string.Empty
                : _caption!.Split(Environment.NewLine.ToCharArray())[0];

            krtbMessageText.StateCommon.Content.Color1 = GlobalStaticFunctions.IsUsingDefaultBlackThemes() ? Color.White : GlobalStaticValues.EMPTY_COLOR;

            krtbMessageText.Text = _text;
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
            if (OSUtilities.IsAtLeastWindowsEleven)
            {
                switch (_kryptonMessageBoxIcon)
                {
                    case KryptonMessageBoxIcon.None:
                        // Windows XP and before will Beep, Vista and above do not!
                        break;
                    case KryptonMessageBoxIcon.Hand:
                        _messageIcon.Image = MessageBoxImageResources.GenericHand;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemHand:
                        //case KryptonMessageBoxIcon.SystemStop:
                        //case KryptonMessageBoxIcon.SystemError:
                        _messageIcon.Image = SystemIcons.Hand.ToBitmap();
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.Question:
                        _messageIcon.Image = MessageBoxImageResources.Question_Windows_11;
                        SystemSounds.Question.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemQuestion:
                        _messageIcon.Image = SystemIcons.Question.ToBitmap();
                        SystemSounds.Question.Play();
                        break;
                    case KryptonMessageBoxIcon.Exclamation:
                        _messageIcon.Image = MessageBoxImageResources.Warning_Windows_11;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemExclamation:
                        _messageIcon.Image = SystemIcons.Warning.ToBitmap();
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Asterisk:
                        _messageIcon.Image = MessageBoxImageResources.Asterisk_Windows_11;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemAsterisk:
                        _messageIcon.Image = SystemIcons.Asterisk.ToBitmap();
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Stop:
                        _messageIcon.Image = MessageBoxImageResources.GenericStop;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.Error:
                        _messageIcon.Image = MessageBoxImageResources.Critical_Windows_11;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.Warning:
                        _messageIcon.Image = MessageBoxImageResources.Warning_Windows_11;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Information:
                        _messageIcon.Image = MessageBoxImageResources.Information_Windows_11;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Shield:
                        _messageIcon.Image = UACShieldIconResources.UAC_Shield_Windows_11;
                        break;
                    case KryptonMessageBoxIcon.WindowsLogo:
                        // Because Windows 11 displays a generic application icon,
                        // we need to rely on an image instead
                        _messageIcon.Image = MessageBoxImageResources.Windows11;
                        break;
                    case KryptonMessageBoxIcon.Application:
                        {
                            var applicationPath = Application.ExecutablePath;
                            Image? sourceImage = GraphicsExtensions.ExtractIconFromFilePath(applicationPath)?.ToBitmap();
                            Image? scaledImage = GraphicsExtensions.ScaleImage(sourceImage, new Size(32, 32));
                            _messageIcon.Image = scaledImage ??
                                                 // Fall back to defaults
                                                 SystemIcons.Application.ToBitmap();
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
                        break;
                    case KryptonMessageBoxIcon.Hand:
                        _messageIcon.Image = MessageBoxImageResources.GenericHand;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemHand:
                    //case KryptonMessageBoxIcon.SystemStop:
                    //case KryptonMessageBoxIcon.SystemError:
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
                        _messageIcon.Image = MessageBoxImageResources.GenericAsterisk;
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.SystemAsterisk:
                        _messageIcon.Image = SystemIcons.Asterisk.ToBitmap();
                        SystemSounds.Asterisk.Play();
                        break;
                    case KryptonMessageBoxIcon.Stop:
                        _messageIcon.Image = MessageBoxImageResources.GenericStop;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.Error:
                        _messageIcon.Image = MessageBoxImageResources.GenericCritical;
                        SystemSounds.Hand.Play();
                        break;
                    case KryptonMessageBoxIcon.Warning:
                        _messageIcon.Image = MessageBoxImageResources.GenericWarning;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Information:
                        _messageIcon.Image = MessageBoxImageResources.GenericInformation;
                        SystemSounds.Exclamation.Play();
                        break;
                    case KryptonMessageBoxIcon.Shield:
                        _messageIcon.Image = OSUtilities.IsWindowsTen 
                            ? UACShieldIconResources.UAC_Shield_Windows_10 
                            : UACShieldIconResources.UAC_Shield_Windows_7;
                        break;
                    case KryptonMessageBoxIcon.WindowsLogo:
                        _messageIcon.Image = OSUtilities.IsWindowsTen 
                            ? MessageBoxImageResources.Windows_8_and_10_Logo 
                            : SystemIcons.WinLogo.ToBitmap();
                        break;
                    case KryptonMessageBoxIcon.Application:
                        {
                            var applicationPath = Application.ExecutablePath;
                            Image? sourceImage = GraphicsExtensions.ExtractIconFromFilePath(applicationPath)?.ToBitmap();
                            Image? scaledImage = GraphicsExtensions.ScaleImage(sourceImage, new Size(32, 32));
                            _messageIcon.Image = scaledImage ??
                                                 // Fall back to defaults
                                                 SystemIcons.Application.ToBitmap();
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

            // Do we ignore the Alt+F4 on the buttons?
            if (!ControlBox)
            {
                _button1.IgnoreAltF4 = true;
                _button2.IgnoreAltF4 = true;
                _button3.IgnoreAltF4 = true;
                _button4.IgnoreAltF4 = true;
            }
        }

        private void UpdateDefault()
        {
            AcceptButton = _defaultButton switch
            {
                KryptonMessageBoxDefaultButton.Button1 =>
                    //_button1.Select();
                    _button1,
                KryptonMessageBoxDefaultButton.Button2 =>
                    //_button2.Select();
                    _button2,
                KryptonMessageBoxDefaultButton.Button3 =>
                    //_button3.Select();
                    _button3,
                KryptonMessageBoxDefaultButton.Button4 => _showHelpButton ? _button4 : _button1,
                _ => _showHelpButton ? _button4 : _button1
            };
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
                KryptonMessageBoxButtons.OKCancel
                    or KryptonMessageBoxButtons.YesNo
                    or KryptonMessageBoxButtons.RetryCancel => _button3,
                KryptonMessageBoxButtons.AbortRetryIgnore
                    or KryptonMessageBoxButtons.YesNoCancel
                    or KryptonMessageBoxButtons.CancelTryContinue => _button4,
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
                    Control? control = FromHandle(_showOwner.Handle);

                    var mInfoMethod = control!.GetType().GetMethod(nameof(OnHelpRequested), BindingFlags.Instance | BindingFlags.NonPublic,
                        Type.DefaultBinder, [typeof(HelpEventArgs)], null);
                    mInfoMethod?.Invoke(control, [new HelpEventArgs(MousePosition)]);
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
                // Do nothing
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
                Screen screen = showOwner is IWin32Window window
                    ? Screen.FromHandle(window.Handle)
                    : Screen.PrimaryScreen ?? throw new NullReferenceException("Screen.PrimaryScreen returned null");

                Size scaledMonitorSize = screen.WorkingArea.Size;
                scaledMonitorSize.Width = (int)(scaledMonitorSize.Width * 2 / 3.0f);
                scaledMonitorSize.Height = (int)(scaledMonitorSize.Height * 0.95f);
                
                Font textFont = krtbMessageText.StateCommon.Content.GetContentShortTextFont(PaletteState.Normal) ?? KryptonManager.CurrentGlobalPalette!.BaseFont;
                Font captionFont = KryptonManager.CurrentGlobalPalette.BaseFont;

                // Measure the string
                SizeF messageSize = g.MeasureString(_text, textFont, new SizeF(scaledMonitorSize.Width, scaledMonitorSize.Height));

                // SKC: Don't forget to add the TextExtra into the calculation
                SizeF captionSize = TextRenderer.MeasureText($"{_caption} {TextExtra}", captionFont, scaledMonitorSize);

                var messageXSize = Math.Max(messageSize.Width, captionSize.Width);
                // Work out DPI adjustment factor
                var factorX = g.DpiX > 96 ? (1.0f * g.DpiX / 96) : 1.0f;
                var factorY = g.DpiY > 96 ? (1.0f * g.DpiY / 96) : 1.0f;
                messageSize.Width = messageXSize * factorX;
                messageSize.Height *= factorY;

                textSize = Size.Ceiling(messageSize);
            }

            // Calculate the size of the icon area and text area including margins
            Padding textPadding = krtbMessageText.StateCommon.Content.GetContentPadding(PaletteState.Normal);
            Padding textAreaAllMargin = Padding.Add(textPadding, kpnlContentArea.Margin);

            Size iconArea = new Size(_messageIcon.Width + _messageIcon.Margin.Left + _messageIcon.Margin.Right,
                _messageIcon.Height + _messageIcon.Margin.Top + _messageIcon.Margin.Bottom);

            Size textArea = new Size(textSize.Width + textAreaAllMargin.Left + textAreaAllMargin.Right,
                textSize.Height + textAreaAllMargin.Top + textAreaAllMargin.Bottom);

            return new Size(textArea.Width + iconArea.Width,
                Math.Max(iconArea.Height, textArea.Height));
        }

        private Size UpdateButtonsSizing()
        {
            var numButtons = 1;

            // Button1 is always visible
            Size button1Size = _button1.GetPreferredSize(Size.Empty);
            var maxButtonSize = button1Size with { Width = button1Size.Width + GlobalStaticValues.GLOBAL_BUTTON_PADDING };

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

            //var left = _panelButtons.Left - GlobalStaticValues.GLOBAL_BUTTON_PADDING;

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
            _panelButtons.Size = new Size((maxButtonSize.Width * numButtons) + (GlobalStaticValues.GLOBAL_BUTTON_PADDING * (numButtons + 1)), maxButtonSize.Height + (GlobalStaticValues.GLOBAL_BUTTON_PADDING * 2));

            // Button area is the number of buttons with gaps between them and 10 pixels around all edges
            return new Size((maxButtonSize.Width * numButtons) + (GlobalStaticValues.GLOBAL_BUTTON_PADDING * (numButtons + 1)), maxButtonSize.Height + (GlobalStaticValues.GLOBAL_BUTTON_PADDING * 2));
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _krtbNativeWindow.ReleaseHandle();
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
            else if (e.KeyData == (Keys.Control | Keys.C))
            {
                const string DIVIDER = @"---------------------------";
                const string BUTTON_TEXT_SPACER = @"   ";
                // Pressing Ctrl+C should copy message text into the clipboard
                var sb = new StringBuilder();

                sb.AppendLine(DIVIDER);
                sb.AppendLine(Text);
                sb.AppendLine(DIVIDER);
                sb.AppendLine(krtbMessageText.Text);
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
        }

        private void ShowCloseButton(bool? showCloseButton) => CloseBox = showCloseButton ?? true;

        #endregion
    }

    #region Types
    /// <summary>
    /// 
    /// </summary>
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
