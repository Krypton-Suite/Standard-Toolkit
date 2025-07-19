#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class VisualTaskDialogForm : KryptonForm
{
    #region Static Fields

    private const int BUTTON_GAP = 10;

    #endregion

    #region Instance Fields

    private KryptonTaskDialog? _taskDialog;
    private readonly string _windowTitle;
    private readonly string _mainInstruction;
    private readonly string _content;
    private readonly KryptonMessageBoxIcon _mainIcon;
    private readonly Image? _customMainIcon;
    private readonly KryptonTaskDialogCommandCollection _radioButtons;
    private readonly KryptonTaskDialogCommandCollection _commandButtons;
    private KryptonTaskDialogCommand? _defaultRadioButton;
    private readonly TaskDialogButtons _commonButtons;
    private readonly TaskDialogButtons _defaultButton;
    private readonly KryptonMessageBoxIcon _footerIcon;
    private readonly Image? _customFooterIcon;
    private readonly string _footerText;
    private readonly string _footerHyperlink;
    private readonly string _checkboxText;
    private bool _checkboxState;
    private readonly bool _allowDialogClose;
    private readonly bool _useNativeOSIcons;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualTaskDialogForm" /> class.</summary>
    /// <param name="taskDialog">The task dialog.</param>
    /// <exception cref="System.ArgumentNullException">taskDialog</exception>
    public VisualTaskDialogForm(KryptonTaskDialog taskDialog)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        // Must provide a valid reference

        _taskDialog = taskDialog ??
                      throw new ArgumentNullException(nameof(taskDialog));

        // Initialize with task dialog values
        _windowTitle = taskDialog.WindowTitle;
        _mainInstruction = taskDialog.MainInstruction;
        _content = taskDialog.Content;
        _mainIcon = taskDialog.Icon;
        _customMainIcon = taskDialog.CustomIcon;
        _radioButtons = taskDialog.RadioButtons;
        _commandButtons = taskDialog.CommandButtons;
        _commonButtons = taskDialog.CommonButtons;
        _defaultRadioButton = taskDialog.DefaultRadioButton;
        _defaultButton = taskDialog.DefaultButton;
        _footerIcon = taskDialog.FooterIcon;
        _customFooterIcon = taskDialog.CustomFooterIcon;
        _footerText = taskDialog.FooterText;
        _footerHyperlink = taskDialog.FooterHyperlink;
        _checkboxText = taskDialog.CheckboxText;
        _checkboxState = taskDialog.CheckboxState;
        _allowDialogClose = taskDialog.AllowDialogClose;
        _useNativeOSIcons = taskDialog.UseNativeOSIcons;

        TextExtra = taskDialog.TextExtra;

        InitializeComponent();

        UpdateContents();
    }

    #endregion

    #region Implementation

    private void UpdateContents()
    {
        // Update contents to match requirements
        UpdateText();
        UpdateIcon();
        UpdateRadioButtons();
        UpdateCommandButtons();
        UpdateButtons();
        UpdateCheckbox();
        UpdateFooter();
        UpdateChrome();

        // Finally calculate and set form sizing
        UpdateSizing();
    }

    private void UpdateText()
    {
        Text = _windowTitle;
        _messageText.Text = _mainInstruction;
        // If the content has more than 20 lines, use the multi-line text control
        if (string.IsNullOrEmpty(_content))
        {
            _messageContent.Text = string.Empty;
        }
        else if (_content.Length - _content.Replace("\n", string.Empty).Length > 20)
        {
            _messageContentMultiline.Text = _content;
            _messageContentMultiline.Visible = true;
            _messageContent.Visible = false;
        }
        else
        {
            _messageContent.Text = _content;
            _messageContentMultiline.Visible = false;
            _messageContent.Visible = true;
        }
    }

    private void UpdateIcon()
    {
        _panelIcon.Visible = true;

        // Always use the custom icon as the preferred option
        if (_customMainIcon != null)
        {
            _messageIcon.Image = _customMainIcon;
        }
        else
        {
            switch (_mainIcon)
            {
                case KryptonMessageBoxIcon.None:
                    _panelIcon.Visible = false;
                    _panelMainText.Left -= _messageIcon.Right;
                    break;
                case KryptonMessageBoxIcon.Hand:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogHandGeneric;
                    SystemSounds.Hand.Play();
                    break;
                case KryptonMessageBoxIcon.Question:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogQuestionGeneric;
                    SystemSounds.Question.Play();
                    break;
                case KryptonMessageBoxIcon.Exclamation:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogWarningGeneric;
                    SystemSounds.Exclamation.Play();
                    break;
                case KryptonMessageBoxIcon.Asterisk:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogAsteriskGeneric;
                    SystemSounds.Asterisk.Play();
                    break;
                case KryptonMessageBoxIcon.Stop:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogStopGeneric;
                    SystemSounds.Asterisk.Play();
                    break;
                case KryptonMessageBoxIcon.Information:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogInformationGeneric;
                    SystemSounds.Asterisk.Play();
                    break;
                case KryptonMessageBoxIcon.Warning:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogWarningGeneric;
                    SystemSounds.Exclamation.Play();
                    break;
                case KryptonMessageBoxIcon.Error:
                    _messageIcon.Image = TaskDialogImageResources.TaskDialogCriticalGeneric;
                    SystemSounds.Hand.Play();
                    break;
                case KryptonMessageBoxIcon.Shield:
                    _messageIcon.Image = GraphicsExtensions.ScaleImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));
                    break;
                case KryptonMessageBoxIcon.WindowsLogo:
                    // Because Windows 11 displays a generic application icon,
                    // we need to rely on a image instead
                    if (OSUtilities.IsAtLeastWindowsEleven)
                    {
                        _messageIcon.Image = TaskDialogImageResources.TaskDialog_Windows_11_Logo;
                    }
                    // Windows 10
                    else if (OSUtilities.IsWindowsTen)
                    {
                        _messageIcon.Image = TaskDialogImageResources.TaskDialog_Windows_8_and_10_Logo;
                    }
                    else
                    {
                        _messageIcon.Image = GraphicsExtensions.ScaleImage(SystemIcons.WinLogo.ToBitmap(), new Size(16, 16));
                    }
                    break;
            }
        }
    }

    private void UpdateRadioButtons()
    {
        if (_radioButtons.Count == 0)
        {
            _panelMainRadio.Visible = false;
        }
        else
        {
            _panelMainRadio.Controls.Clear();
            _panelMainRadio.Visible = true;

            var maxButtonSize = Size.Empty;
            foreach (KryptonTaskDialogCommand command in _radioButtons)
            {
                // Create and add a new radio button instance
                var button = new KryptonRadioButton
                {
                    LabelStyle = LabelStyle.NormalPanel
                };
                button.Values.Text = command.Text;
                button.Values.ExtraText = command.ExtraText;
                button.Values.Image = command.Image;
                button.Values.ImageTransparentColor = command.ImageTransparentColor;
                button.Enabled = command.Enabled;
                button.CheckedChanged += OnRadioButtonCheckedChanged;
                button.Tag = command;
                if (_defaultRadioButton == command)
                {
                    button.Checked = true;
                }

                _panelMainRadio.Controls.Add(button);

                // Note that largest radio button encountered
                Size buttonSize = button.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonSize.Width);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonSize.Height);
            }

            // Enforce a maximum width to the commands
            maxButtonSize.Width = Math.Min(Math.Max(maxButtonSize.Width, 150), 400);

            // Position the radio buttons in a vertical stack and size owning panel
            var offset = new Point(BUTTON_GAP - 1, 2);
            foreach (KryptonRadioButton button in _panelMainRadio.Controls)
            {
                button.Location = offset;
                button.Size = maxButtonSize;
                offset.Y += maxButtonSize.Height;
            }

            // Size to the contained command controls
            _panelMainRadio.Size = maxButtonSize with { Height = offset.Y };
        }
    }

    private void UpdateCommandButtons()
    {
        if (_commandButtons.Count == 0)
        {
            _panelMainCommands.Visible = false;
        }
        else
        {
            _panelMainCommands.Controls.Clear();
            _panelMainCommands.Visible = true;

            var maxButtonSize = Size.Empty;
            foreach (KryptonTaskDialogCommand command in _commandButtons)
            {
                // Create and add a new button instance
                var button = new KryptonButton
                {
                    ButtonStyle = ButtonStyle.Command
                };
                button.StateCommon.Content.Image!.ImageH = PaletteRelativeAlign.Near;
                button.StateCommon.Content.ShortText.TextH = PaletteRelativeAlign.Near;
                button.StateCommon.Content.LongText.TextH = PaletteRelativeAlign.Near;
                button.Values.Text = command.Text;
                button.Values.ExtraText = command.ExtraText;
                button.Values.Image = command.Image;
                button.Values.ImageTransparentColor = command.ImageTransparentColor;
                button.Enabled = command.Enabled;
                button.DialogResult = command.DialogResult;
                button.Tag = command;
                button.Click += OnCommandClicked;
                _panelMainCommands.Controls.Add(button);

                // Note that largest button encountered
                Size buttonSize = button.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonSize.Width);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonSize.Height);
            }

            // Enforce a maximum width to the commands
            maxButtonSize.Width = Math.Min(Math.Max(maxButtonSize.Width, 150), 400);

            // Position the buttons in a vertical stack and size owning panel
            var offset = new Point(BUTTON_GAP - 1, 2);
            foreach (KryptonButton button in _panelMainCommands.Controls)
            {
                button.Location = offset;
                button.Size = maxButtonSize;
                offset.Y += maxButtonSize.Height;
            }

            // Size to the contained command controls
            _panelMainCommands.Size = maxButtonSize with { Height = offset.Y };
        }
    }

    private void UpdateButtons()
    {
        TaskDialogMessageButton? firstButton = null;
        TaskDialogMessageButton? defaultButton = null;

        if ((_commonButtons & TaskDialogButtons.OK) == TaskDialogButtons.OK)
        {
            if ((_defaultButton & TaskDialogButtons.OK) == TaskDialogButtons.OK)
            {
                defaultButton = _buttonOK;
            }

            firstButton = _buttonOK;
            _buttonOK.Text = KryptonManager.Strings.GeneralStrings.OK;
            _buttonOK.Visible = true;
        }
        else
        {
            _buttonOK.Visible = false;
        }

        if ((_commonButtons & TaskDialogButtons.Yes) == TaskDialogButtons.Yes)
        {
            if ((_defaultButton & TaskDialogButtons.Yes) == TaskDialogButtons.Yes)
            {
                defaultButton = _buttonYes;
            }

            firstButton ??= _buttonYes;

            _buttonYes.Text = KryptonManager.Strings.GeneralStrings.Yes;
            _buttonYes.Visible = true;
        }
        else
        {
            _buttonYes.Visible = false;
        }

        if ((_commonButtons & TaskDialogButtons.No) == TaskDialogButtons.No)
        {
            if ((_defaultButton & TaskDialogButtons.No) == TaskDialogButtons.No)
            {
                defaultButton = _buttonNo;
            }

            firstButton ??= _buttonNo;

            _buttonNo.Text = KryptonManager.Strings.GeneralStrings.No;
            _buttonNo.Visible = true;
        }
        else
        {
            _buttonNo.Visible = false;
        }

        if ((_commonButtons & TaskDialogButtons.Cancel) == TaskDialogButtons.Cancel)
        {
            if ((_defaultButton & TaskDialogButtons.Cancel) == TaskDialogButtons.Cancel)
            {
                defaultButton = _buttonCancel;
            }

            firstButton ??= _buttonCancel;

            _buttonCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;
            _buttonCancel.Visible = true;
        }
        else
        {
            _buttonCancel.Visible = false;
        }

        if ((_commonButtons & TaskDialogButtons.Retry) == TaskDialogButtons.Retry)
        {
            if ((_defaultButton & TaskDialogButtons.Retry) == TaskDialogButtons.Retry)
            {
                defaultButton = _buttonRetry;
            }

            firstButton ??= _buttonRetry;

            _buttonRetry.Text = KryptonManager.Strings.GeneralStrings.Retry;
            _buttonRetry.Visible = true;
        }
        else
        {
            _buttonRetry.Visible = false;
        }

        if ((_commonButtons & TaskDialogButtons.Close) == TaskDialogButtons.Close)
        {
            if ((_defaultButton & TaskDialogButtons.Close) == TaskDialogButtons.Close)
            {
                defaultButton = _buttonClose;
            }

            firstButton ??= _buttonClose;

            _buttonClose.Text = KryptonManager.Strings.GeneralStrings.Close;
            _buttonClose.Visible = true;
        }
        else
        {
            _buttonClose.Visible = false;
        }

        if (defaultButton != null)
        {
            defaultButton.Select();
        }
        else
        {
            firstButton?.Select();
        }
    }

    private void UpdateCheckbox()
    {
        _checkBox.Checked = _checkboxState;
        _checkBox.Text = _checkboxText;
        _checkBox.Visible = !string.IsNullOrEmpty(_checkboxText);
    }

    private void UpdateFooter()
    {
        _iconFooter.Visible = true;

        // Always use the custom icon as the preferred option
        if (_customFooterIcon != null)
        {
            _iconFooter.Image = _customFooterIcon;
        }
        else
        {
            switch (_footerIcon)
            {
                case KryptonMessageBoxIcon.None:
                    _iconFooter.Visible = false;
                    break;
                case KryptonMessageBoxIcon.Question:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Question, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Information:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Information, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Warning:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Warning, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Error:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Error, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Hand:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Hand, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Exclamation:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Exclamation, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Asterisk:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Asterisk, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Stop:
                    ChangeFooterIcon(KryptonMessageBoxIcon.Stop, _useNativeOSIcons);
                    break;
                case KryptonMessageBoxIcon.Shield:
                    _iconFooter.Image = GraphicsExtensions.ScaleImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));
                    break;
                case KryptonMessageBoxIcon.WindowsLogo:
                    // Because Windows 11 displays a generic application icon,
                    // we need to rely on a image instead
                    if (OSUtilities.IsAtLeastWindowsEleven)
                    {
                        _iconFooter.Image = TaskDialogImageResources.TaskDialog_Windows_11_Logo;
                    }
                    // Windows 10
                    else if (OSUtilities.IsWindowsTen)
                    {
                        _iconFooter.Image = TaskDialogImageResources.TaskDialog_Windows_8_and_10_Logo;
                    }
                    else
                    {
                        _iconFooter.Image =
                            GraphicsExtensions.ScaleImage(SystemIcons.WinLogo.ToBitmap(), new Size(16, 16));
                    }

                    break;
            }
        }

        _footerLabel.Text = _footerText;
        _linkLabelFooter.Text = _footerHyperlink;
    }

    private void ChangeFooterIcon(KryptonMessageBoxIcon icon, bool useNativeOsIcons)
    {
        switch (icon)
        {
            case KryptonMessageBoxIcon.Hand:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Hand.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogHandGeneric;
                }
                break;
            case KryptonMessageBoxIcon.Question:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Question.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogQuestionGeneric;
                }
                break;
            case KryptonMessageBoxIcon.Exclamation:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Exclamation.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogWarningGeneric;
                }
                break;
            case KryptonMessageBoxIcon.Asterisk:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogAsteriskGeneric;
                }
                break;
            case KryptonMessageBoxIcon.Stop:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Asterisk.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogStopGeneric;
                }
                break;
            case KryptonMessageBoxIcon.Error:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Error.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogAsteriskGeneric;
                }
                break;
            case KryptonMessageBoxIcon.Warning:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Warning.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogWarningGeneric;
                }
                break;
            case KryptonMessageBoxIcon.Information:
                if (useNativeOsIcons)
                {
                    _iconFooter.Image =
                        GraphicsExtensions.ScaleImage(SystemIcons.Information.ToBitmap(), new Size(16, 16));
                }
                else
                {
                    _iconFooter.Image = TaskDialogImageResources.TaskDialogInformationGeneric;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(icon), icon, null);
        }
    }

    private void UpdateChrome()
    {
        if (((_commonButtons & TaskDialogButtons.Cancel) == TaskDialogButtons.Cancel) || _allowDialogClose)
        {
            ControlBox = true;
        }
        else
        {
            ControlBox = false;
        }

        _buttonOK.IgnoreAltF4 = !ControlBox;
        _buttonYes.IgnoreAltF4 = !ControlBox;
        _buttonNo.IgnoreAltF4 = !ControlBox;
        _buttonCancel.IgnoreAltF4 = !ControlBox;
        _buttonRetry.IgnoreAltF4 = !ControlBox;
        _buttonClose.IgnoreAltF4 = !ControlBox;
    }

    private void UpdateSizing()
    {
        Size messageSizing = UpdateMainTextSizing();
        Size radioSizing = UpdateRadioSizing();
        Size commandSizing = UpdateCommandSizing();
        Size spacerSizing = UpdateSpacerSizing();
        Size iconSizing = UpdateIconSizing();
        Size buttonsSizing = UpdateButtonsSizing();
        Size footerSizing = UpdateFooterSizing();

        // Size of window is calculated from the client area
        ClientSize = new Size(iconSizing.Width + Math.Max(messageSizing.Width, Math.Max(commandSizing.Width, Math.Max(radioSizing.Width, Math.Max(buttonsSizing.Width, footerSizing.Width)))),
            Math.Max(iconSizing.Height, messageSizing.Height + radioSizing.Height + commandSizing.Height + spacerSizing.Height) + buttonsSizing.Height + footerSizing.Height);
    }

    private Size UpdateMainTextSizing()
    {
        Size messageContentSize;

        // Update size of the main instruction and content labels but applying a sensible maximum
        using (Graphics g = CreateGraphics())
        {
            // Find size of the labels when it has a maximum length of 400
            _messageText.UpdateFont();
            _messageContent.UpdateFont();
            _messageContentMultiline.Font = _messageContent.Font;
            var messageMainSize = g.MeasureString(_mainInstruction, _messageText.Font, 400).ToSize();
            messageContentSize = g.MeasureString(_content, _messageContent.Font, 400).ToSize();

            // Get the display size and make sure that the content size is not greater than 0.6 of display size
            Rectangle dispSize = Screen.GetWorkingArea(Location);

            var h = (int)Math.Min(messageContentSize.Height, dispSize.Height * 0.6);
            var w = (int)Math.Min(messageContentSize.Width, dispSize.Width * 0.6);
            var sz = new Size(w, h);
            if (messageContentSize != sz)
            {
                messageContentSize = sz;
            }

            // Work out DPI adjustment factor
            messageMainSize.Width = (int)(messageMainSize.Width * FactorDpiX);
            messageMainSize.Height = (int)(messageMainSize.Height * FactorDpiY);
            messageContentSize.Width = (int)(messageContentSize.Width * FactorDpiX);
            messageContentSize.Height = (int)(messageContentSize.Height * FactorDpiY);

            // Always add on an extra 5 pixels as sometimes the measure size does not draw the last
            // character it contains, this ensures there is always definitely enough space for it all
            messageMainSize.Width += 5;
            messageContentSize.Width += 5;
            _messageText.Size = messageMainSize;
            _messageContent.Size = messageContentSize;
            _messageContentMultiline.Size = messageContentSize;
        }

        // Resize panel containing the main text
        Padding panelMessagePadding = _panelMainText.Padding;
        _panelMainText.Width = Math.Max(_messageText.Size.Width, messageContentSize.Width) + panelMessagePadding.Horizontal;
        _panelMainText.Height = _messageText.Size.Height + messageContentSize.Height + panelMessagePadding.Vertical + BUTTON_GAP;

        // Position the content label below the main label
        _messageContent.Location = new Point(_messageText.Left + 2, _messageText.Bottom);
        _messageContentMultiline.Location = _messageContent.Location;
        return _panelMainText.Size;
    }

    private Size UpdateIconSizing() => _messageIcon.Image == null ? Size.Empty : _panelIcon.Size;

    private Size UpdateRadioSizing()
    {
        _panelMainRadio.Location = new Point(_panelMainText.Left, _panelMainText.Top + _panelMainText.Height);

        if (_radioButtons.Count == 0)
        {
            _panelMainRadio.Size = Size.Empty;
            return Size.Empty;
        }

        return _panelMainRadio.Size with { Width = _panelMainRadio.Size.Width + BUTTON_GAP + 2 };
    }

    private Size UpdateCommandSizing()
    {
        _panelMainCommands.Location = new Point(_panelMainRadio.Left, _panelMainRadio.Top + _panelMainRadio.Height);

        if (_commandButtons.Count == 0)
        {
            _panelMainCommands.Size = Size.Empty;
            return Size.Empty;
        }

        return _panelMainCommands.Size with { Width = _panelMainCommands.Size.Width + BUTTON_GAP + 2 };
    }

    private Size UpdateSpacerSizing()
    {
        _panelMainSpacer.Location = new Point(_panelMainCommands.Left, _panelMainCommands.Top + _panelMainCommands.Height);
        return _panelMainSpacer.Size;
    }

    private Size UpdateButtonsSizing()
    {
        var numButtons = 0;
        var maxButtonSize = Size.Empty;

        // Find the size of the largest button we need
        if ((_commonButtons & TaskDialogButtons.Close) == TaskDialogButtons.Close)
        {
            numButtons++;
            Size buttonCancelSize = _buttonClose.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonCancelSize.Width + BUTTON_GAP);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonCancelSize.Height);
        }

        if ((_commonButtons & TaskDialogButtons.Retry) == TaskDialogButtons.Retry)
        {
            numButtons++;
            Size buttonRetrySize = _buttonRetry.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonRetrySize.Width + BUTTON_GAP);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonRetrySize.Height);
        }

        if ((_commonButtons & TaskDialogButtons.Cancel) == TaskDialogButtons.Cancel)
        {
            numButtons++;
            Size buttonCancelSize = _buttonCancel.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonCancelSize.Width + BUTTON_GAP);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonCancelSize.Height);
        }

        if ((_commonButtons & TaskDialogButtons.No) == TaskDialogButtons.No)
        {
            numButtons++;
            Size buttonNoSize = _buttonNo.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonNoSize.Width + BUTTON_GAP);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonNoSize.Height);
        }

        if ((_commonButtons & TaskDialogButtons.Yes) == TaskDialogButtons.Yes)
        {
            numButtons++;
            Size buttonYesSize = _buttonYes.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonYesSize.Width + BUTTON_GAP);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonYesSize.Height);
        }

        if ((_commonButtons & TaskDialogButtons.OK) == TaskDialogButtons.OK)
        {
            numButtons++;
            Size buttonOKSize = _buttonOK.GetPreferredSize(Size.Empty);
            maxButtonSize.Width = Math.Max(maxButtonSize.Width, buttonOKSize.Width + BUTTON_GAP);
            maxButtonSize.Height = Math.Max(maxButtonSize.Height, buttonOKSize.Height);
        }


        // Start positioning buttons from right edge
        var right = _panelButtons.Right - BUTTON_GAP;

        if ((_commonButtons & TaskDialogButtons.Close) == TaskDialogButtons.Close)
        {
            _buttonClose.Location = new Point(right - maxButtonSize.Width, BUTTON_GAP);
            _buttonClose.Size = maxButtonSize;
            right -= maxButtonSize.Width + BUTTON_GAP;
        }

        if ((_commonButtons & TaskDialogButtons.Retry) == TaskDialogButtons.Retry)
        {
            _buttonRetry.Location = new Point(right - maxButtonSize.Width, BUTTON_GAP);
            _buttonRetry.Size = maxButtonSize;
            right -= maxButtonSize.Width + BUTTON_GAP;
        }

        if ((_commonButtons & TaskDialogButtons.Cancel) == TaskDialogButtons.Cancel)
        {
            _buttonCancel.Location = new Point(right - maxButtonSize.Width, BUTTON_GAP);
            _buttonCancel.Size = maxButtonSize;
            right -= maxButtonSize.Width + BUTTON_GAP;
        }

        if ((_commonButtons & TaskDialogButtons.No) == TaskDialogButtons.No)
        {
            _buttonNo.Location = new Point(right - maxButtonSize.Width, BUTTON_GAP);
            _buttonNo.Size = maxButtonSize;
            right -= maxButtonSize.Width + BUTTON_GAP;
        }

        if ((_commonButtons & TaskDialogButtons.Yes) == TaskDialogButtons.Yes)
        {
            _buttonYes.Location = new Point(right - maxButtonSize.Width, BUTTON_GAP);
            _buttonYes.Size = maxButtonSize;
            right -= maxButtonSize.Width + BUTTON_GAP;
        }

        if ((_commonButtons & TaskDialogButtons.OK) == TaskDialogButtons.OK)
        {
            _buttonOK.Location = new Point(right - maxButtonSize.Width, BUTTON_GAP);
            _buttonOK.Size = maxButtonSize;
            right -= maxButtonSize.Width + BUTTON_GAP;
        }

        var checkboxSize = Size.Empty;
        if (!string.IsNullOrEmpty(_checkboxText))
        {
            checkboxSize = _checkBox.GetPreferredSize(Size.Empty);
        }

        if (numButtons == 0)
        {
            if (checkboxSize.IsEmpty)
            {
                _panelButtons.Visible = false;
                return Size.Empty;
            }
            else
            {
                _panelButtons.Visible = true;
                _checkBox.Location = new Point(BUTTON_GAP, BUTTON_GAP);
                return new Size(checkboxSize.Width + (BUTTON_GAP * 2), checkboxSize.Height + (BUTTON_GAP * 2));
            }
        }
        else
        {
            _panelButtons.Visible = true;

            var panelButtonSize = new Size((maxButtonSize.Width * numButtons) + (BUTTON_GAP * (numButtons + 1)),
                maxButtonSize.Height + (BUTTON_GAP * 2));

            if (!checkboxSize.IsEmpty)
            {
                panelButtonSize.Width += checkboxSize.Width;
                panelButtonSize.Height = Math.Max(panelButtonSize.Height, checkboxSize.Height + (BUTTON_GAP * 2));
                _checkBox.Location = new Point(BUTTON_GAP, (panelButtonSize.Height - checkboxSize.Height) / 2);
            }

            return panelButtonSize;
        }
    }

    private Size UpdateFooterSizing()
    {
        // Update size of the footer but applying a sensible maximum
        using Graphics g = CreateGraphics();
        // Find size of the labels when it has a maximum length of 400
        _footerLabel.UpdateFont();
        var footerTextSize = g.MeasureString(_footerText, _footerLabel.Font, 200).ToSize();
        var footerHyperlinkSize = g.MeasureString(_footerHyperlink, _footerLabel.Font, 200).ToSize();

        // Always add on an extra 5 pixels as sometimes the measure size does not draw the last
        // character it contains, this ensures there is always definitely enough space for it all
        footerTextSize.Width += 5;
        footerHyperlinkSize.Width += 5;
        _footerLabel.Size = footerTextSize;
        _linkLabelFooter.Size = footerHyperlinkSize;

        // Find required size of the footer panel
        var requiredSize = Size.Empty;

        if (!string.IsNullOrEmpty(_footerText))
        {
            requiredSize.Width += footerTextSize.Width;
            requiredSize.Height = footerTextSize.Height;
        }

        if (!string.IsNullOrEmpty(_footerHyperlink))
        {
            requiredSize.Width += footerHyperlinkSize.Width;
            requiredSize.Height = Math.Max(requiredSize.Height, footerHyperlinkSize.Height);
        }

        if ((_footerIcon != KryptonMessageBoxIcon.None) || (_customFooterIcon != null))
        {
            requiredSize.Width += _iconFooter.Width + BUTTON_GAP;
            requiredSize.Height = Math.Max(requiredSize.Height, _iconFooter.Size.Height);
        }

        if (requiredSize.Width > 0)
        {
            requiredSize.Width += BUTTON_GAP * 2;
            requiredSize.Height += BUTTON_GAP * 2;
        }

        // Do we have anything to show?
        _panelFooter.Visible = requiredSize.Width > 0;

        // Position the footer elements
        if (requiredSize.Width > 0)
        {
            _panelFooter.Size = requiredSize;
            var offset = BUTTON_GAP;

            if ((_footerIcon != KryptonMessageBoxIcon.None) || (_customFooterIcon != null))
            {
                _iconFooter.Location = new Point(offset, (requiredSize.Height - _iconFooter.Height) / 2);
                offset += _iconFooter.Width + (BUTTON_GAP / 2);
            }

            if (!string.IsNullOrEmpty(_footerText))
            {
                _footerLabel.Location = new Point(offset, (requiredSize.Height - footerTextSize.Height) / 2);
                offset += _footerLabel.Width - 8;
            }

            if (!string.IsNullOrEmpty(_footerHyperlink))
            {
                _linkLabelFooter.Location = !string.IsNullOrEmpty(_footerText)
                    ? new Point(offset, _footerLabel.Location.Y - 1)
                    : new Point(offset, (requiredSize.Height - footerHyperlinkSize.Height) / 2);

                offset += _footerLabel.Width;
            }
        }

        return requiredSize;
    }

    private void OnRadioButtonCheckedChanged(object? sender, EventArgs e)
    {
        var button = sender as KryptonRadioButton;
        _defaultRadioButton = button?.Tag as KryptonTaskDialogCommand;
        if (_taskDialog != null)
        {
            _taskDialog.DefaultRadioButton = _defaultRadioButton;
        }
    }

    private void OnCommandClicked(object? sender, EventArgs e)
    {
        Close();

        // Update the result code from the command button
        var button = sender as KryptonButton;
        DialogResult = button!.DialogResult;

        // Invoke any event handlers from the command button
        var command = button.Tag as KryptonTaskDialogCommand;
        command?.PerformExecute();
    }

    private void OnTaskDialogFormClosing(object sender, FormClosingEventArgs e)
    {
        // If the dialog is being closed because of a user event then it would be either
        // Alt+F4, Hit the close chrome button or the Close common dialog button
        if (e.CloseReason == CloseReason.UserClosing)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

#pragma warning disable IDE1006 // Naming Styles
    private void checkBox_CheckedChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
    {
        _checkboxState = _checkBox.Checked;
        if (_taskDialog != null)
        {
            _taskDialog.CheckboxState = _checkboxState;
        }
    }

    private void LinkLabelFooter_LinkClicked(object sender, EventArgs e) => _taskDialog?.RaiseFooterHyperlinkClicked();

    private void _buttonClose_Click(object sender, EventArgs e) => Close();

#pragma warning disable IDE1006 // Naming Styles
    private void button_keyDown(object sender, KeyEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
    {
        // Escape key kills the dialog if we allow it to be closed
        if ((e.KeyCode == Keys.Escape) && ControlBox)
        {
            Close();
        }
        else
        {
            // Pressing Ctrl+C should copy message text into the clipboard
            if (e is { Modifiers: Keys.Control, KeyCode: Keys.C })
            {
                var sb = new StringBuilder();

                sb.AppendLine("---------------------------");
                sb.AppendLine(_windowTitle);
                sb.AppendLine("---------------------------");
                sb.AppendLine(_mainInstruction);
                sb.AppendLine("---------------------------");
                sb.AppendLine(_content);
                sb.AppendLine("---------------------------");
                if (_buttonOK.Visible)
                {
                    sb.Append(_buttonOK.Text);
                    sb.Append("   ");
                }
                if (_buttonYes.Visible)
                {
                    sb.Append(_buttonYes.Text);
                    sb.Append("   ");
                }
                if (_buttonNo.Visible)
                {
                    sb.Append(_buttonNo.Text);
                    sb.Append("   ");
                }
                if (_buttonCancel.Visible)
                {
                    sb.Append(_buttonCancel.Text);
                    sb.Append("   ");
                }
                if (_buttonRetry.Visible)
                {
                    sb.Append(_buttonRetry.Text);
                    sb.Append("   ");
                }
                if (_buttonClose.Visible)
                {
                    sb.Append(_buttonClose.Text);
                    sb.Append("   ");
                }
                sb.AppendLine("");
                sb.AppendLine("---------------------------");

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
                Clipboard.SetText(sb.ToString(), TextDataFormat.UnicodeText);
            }
        }
    }

    #endregion
}