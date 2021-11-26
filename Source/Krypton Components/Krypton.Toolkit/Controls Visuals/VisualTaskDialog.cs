#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays a task dialog that allows the user to select a task based on the presented options.
    /// </summary>
    [ToolboxItem(false)]
    public class VisualTaskDialog : KryptonForm
    {
        #region Types
        /// <summary>
        /// Internal button used by the VisualTaskDialog
        /// </summary>
        [ToolboxItem(false)]
        public class MessageButton : KryptonButton
        {
            #region Instance Fields

            #endregion

            #region Identity
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
                            Keys keys = (Keys)(int)m.WParam.ToInt64();

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
        #endregion

        #region Static Fields

        private const int BUTTON_GAP = 10;

        #endregion

        #region Instance Fields
        private KryptonTaskDialog _taskDialog;
        private readonly string _windowTitle;
        private readonly string _mainInstruction;
        private readonly string _content;
        private readonly MessageBoxIcon _mainIcon;
        private readonly Image _customMainIcon;
        private readonly KryptonTaskDialogCommandCollection _radioButtons;
        private readonly KryptonTaskDialogCommandCollection _commandButtons;
        private KryptonTaskDialogCommand _defaultRadioButton;
        private readonly TaskDialogButtons _commonButtons;
        private readonly TaskDialogButtons _defaultButton;
        private readonly MessageBoxIcon _footerIcon;
        private readonly Image _customFooterIcon;
        private readonly string _footerText;
        private readonly string _footerHyperlink;
        private readonly string _checkboxText;
        private bool _checkboxState;
        private readonly bool _allowDialogClose;

        // User Interface
        private KryptonPanel _panelMain;
        private KryptonPanel _panelIcon;
        private PictureBox _messageIcon;
        private KryptonPanel _panelMainText;
        private KryptonWrapLabel _messageText;
        private KryptonWrapLabel _messageContent;
        private KryptonTextBox _messageContentMultiline;
        private KryptonPanel _panelButtons;
        private MessageButton _buttonOK;
        private MessageButton _buttonYes;
        private MessageButton _buttonNo;
        private MessageButton _buttonCancel;
        private MessageButton _buttonRetry;
        private KryptonBorderEdge _panelButtonsBorderTop;
        private KryptonPanel _panelFooter;
        private KryptonLinkLabel _linkLabelFooter;
        private PictureBox _iconFooter;
        private KryptonWrapLabel _footerLabel;
        private KryptonBorderEdge _panelFooterBorderTop;
        private KryptonCheckBox _checkBox;
        private KryptonPanel _panelMainRadio;
        private KryptonPanel _panelMainCommands;
        private KryptonPanel _panelMainSpacer;
        private MessageButton _buttonClose;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the VisualTaskDialog class.
        /// </summary>
        /// <param name="taskDialog">Reference to component with definition of content.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public VisualTaskDialog(KryptonTaskDialog taskDialog)
        {
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

            InitializeComponent();
            TextExtra = taskDialog.TextExtra;
            UpdateContents();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_taskDialog != null)
                {
                    _taskDialog = null;
                }
            }

            base.Dispose(disposing);
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
            else if (_content.Length - _content.Replace("\n", "").Length > 20)
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
                    case MessageBoxIcon.None:
                        _panelIcon.Visible = false;
                        _panelMainText.Left -= _messageIcon.Right;
                        break;
                    case MessageBoxIcon.Question:
                        _messageIcon.Image = VisualTaskDialogImageResources.QuestionSmall;
                        SystemSounds.Question.Play();
                        break;
                    case MessageBoxIcon.Information:
                        _messageIcon.Image = VisualTaskDialogImageResources.InformationSmall;
                        SystemSounds.Asterisk.Play();
                        break;
                    case MessageBoxIcon.Warning:
                        _messageIcon.Image = VisualTaskDialogImageResources.WarningSmall;
                        SystemSounds.Exclamation.Play();
                        break;
                    case MessageBoxIcon.Error:
                        _messageIcon.Image = VisualTaskDialogImageResources.CriticalSmall;
                        SystemSounds.Hand.Play();
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

                Size maxButtonSize = Size.Empty;
                foreach (KryptonTaskDialogCommand command in _radioButtons)
                {
                    // Create and add a new radio button instance
                    KryptonRadioButton button = new()
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
                Point offset = new(BUTTON_GAP - 1, 2);
                foreach (KryptonRadioButton button in _panelMainRadio.Controls)
                {
                    button.Location = offset;
                    button.Size = maxButtonSize;
                    offset.Y += maxButtonSize.Height;
                }

                // Size to the contained command controls
                _panelMainRadio.Size = new Size(maxButtonSize.Width, offset.Y);
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

                Size maxButtonSize = Size.Empty;
                foreach (KryptonTaskDialogCommand command in _commandButtons)
                {
                    // Create and add a new button instance
                    KryptonButton button = new()
                    {
                        ButtonStyle = ButtonStyle.Command
                    };
                    button.StateCommon.Content.Image.ImageH = PaletteRelativeAlign.Near;
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
                Point offset = new(BUTTON_GAP - 1, 2);
                foreach (KryptonButton button in _panelMainCommands.Controls)
                {
                    button.Location = offset;
                    button.Size = maxButtonSize;
                    offset.Y += maxButtonSize.Height;
                }

                // Size to the contained command controls
                _panelMainCommands.Size = new Size(maxButtonSize.Width, offset.Y);
            }
        }

        private void UpdateButtons()
        {
            MessageButton firstButton = null;
            MessageButton defaultButton = null;

            if ((_commonButtons & TaskDialogButtons.OK) == TaskDialogButtons.OK)
            {
                if ((_defaultButton & TaskDialogButtons.OK) == TaskDialogButtons.OK)
                {
                    defaultButton = _buttonOK;
                }

                firstButton = _buttonOK;
                _buttonOK.Text = KryptonManager.Strings.OK;
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

                if (firstButton == null)
                {
                    firstButton = _buttonYes;
                }

                _buttonYes.Text = KryptonManager.Strings.Yes;
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

                if (firstButton == null)
                {
                    firstButton = _buttonNo;
                }

                _buttonNo.Text = KryptonManager.Strings.No;
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

                if (firstButton == null)
                {
                    firstButton = _buttonCancel;
                }

                _buttonCancel.Text = KryptonManager.Strings.Cancel;
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

                if (firstButton == null)
                {
                    firstButton = _buttonRetry;
                }

                _buttonRetry.Text = KryptonManager.Strings.Retry;
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

                if (firstButton == null)
                {
                    firstButton = _buttonClose;
                }

                _buttonClose.Text = KryptonManager.Strings.Close;
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
                // TODO: These icons may need to be 16 x 16
                switch (_footerIcon)
                {
                    case MessageBoxIcon.None:
                        _iconFooter.Visible = false;
                        break;
                    case MessageBoxIcon.Question:
                        _iconFooter.Image = MessageBoxResources.Question;
                        break;
                    case MessageBoxIcon.Information:
                        _iconFooter.Image = MessageBoxResources.Information;
                        break;
                    case MessageBoxIcon.Warning:
                        _iconFooter.Image = MessageBoxResources.Warning;
                        break;
                    case MessageBoxIcon.Error:
                        _iconFooter.Image = MessageBoxResources.Critical;
                        break;
                }
            }

            _footerLabel.Text = _footerText;
            _linkLabelFooter.Text = _footerHyperlink;
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
                Size messageMainSize = g.MeasureString(_mainInstruction, _messageText.Font, 400).ToSize();
                messageContentSize = g.MeasureString(_content, _messageContent.Font, 400).ToSize();

                // Get the display size and make sure that the content size is not greater than 0.6 of display size
                Rectangle dispSize = Screen.GetWorkingArea(Location);

                var h = (int)Math.Min(messageContentSize.Height, dispSize.Height * 0.6);
                var w = (int)Math.Min(messageContentSize.Width, dispSize.Width * 0.6);
                Size sz = new(w, h);
                if (messageContentSize != sz)
                {
                    messageContentSize = sz;
                }

                // Work out DPI adjustment factor
                var factorX = g.DpiX > 96 ? (1.0f * g.DpiX / 96) : 1.0f;
                var factorY = g.DpiY > 96 ? (1.0f * g.DpiY / 96) : 1.0f;
                messageMainSize.Width = (int)(messageMainSize.Width * factorX);
                messageMainSize.Height = (int)(messageMainSize.Height * factorY);
                messageContentSize.Width = (int)(messageContentSize.Width * factorX);
                messageContentSize.Height = (int)(messageContentSize.Height * factorY);

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

            return new Size(_panelMainRadio.Size.Width + BUTTON_GAP + 2, _panelMainRadio.Size.Height);
        }

        private Size UpdateCommandSizing()
        {
            _panelMainCommands.Location = new Point(_panelMainRadio.Left, _panelMainRadio.Top + _panelMainRadio.Height);

            if (_commandButtons.Count == 0)
            {
                _panelMainCommands.Size = Size.Empty;
                return Size.Empty;
            }

            return new Size(_panelMainCommands.Size.Width + BUTTON_GAP + 2, _panelMainCommands.Size.Height);
        }

        private Size UpdateSpacerSizing()
        {
            _panelMainSpacer.Location = new Point(_panelMainCommands.Left, _panelMainCommands.Top + _panelMainCommands.Height);
            return _panelMainSpacer.Size;
        }

        private Size UpdateButtonsSizing()
        {
            var numButtons = 0;
            Size maxButtonSize = Size.Empty;

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

            Size checkboxSize = Size.Empty;
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

                Size panelButtonSize = new((maxButtonSize.Width * numButtons) + (BUTTON_GAP * (numButtons + 1)), maxButtonSize.Height + (BUTTON_GAP * 2));

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
            Size footerTextSize = g.MeasureString(_footerText, _footerLabel.Font, 200).ToSize();
            Size footerHyperlinkSize = g.MeasureString(_footerHyperlink, _footerLabel.Font, 200).ToSize();

            // Always add on an extra 5 pixels as sometimes the measure size does not draw the last 
            // character it contains, this ensures there is always definitely enough space for it all
            footerTextSize.Width += 5;
            footerHyperlinkSize.Width += 5;
            _footerLabel.Size = footerTextSize;
            _linkLabelFooter.Size = footerHyperlinkSize;

            // Find required size of the footer panel
            Size requiredSize = Size.Empty;

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

            if ((_footerIcon != MessageBoxIcon.None) || (_customFooterIcon != null))
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

                if ((_footerIcon != MessageBoxIcon.None) || (_customFooterIcon != null))
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

        private void OnRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            KryptonRadioButton button = (KryptonRadioButton)sender;
            _defaultRadioButton = (KryptonTaskDialogCommand)button.Tag;
            _taskDialog.DefaultRadioButton = _defaultRadioButton;
        }

        private void OnCommandClicked(object sender, EventArgs e)
        {
            Close();

            // Update the result code from the command button
            KryptonButton button = (KryptonButton)sender;
            DialogResult = button.DialogResult;

            // Invoke any event handlers from the command button
            KryptonTaskDialogCommand command = (KryptonTaskDialogCommand)button.Tag;
            command.PerformExecute();
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

        private void _linkLabelFooter_LinkClicked(object sender, EventArgs e) => _taskDialog?.RaiseFooterHyperlinkClicked();

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
                if ((e.Modifiers == Keys.Control)
                    && (e.KeyCode == Keys.C))
                {
                    StringBuilder sb = new();

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

        private void InitializeComponent()
        {
            _panelMain = new KryptonPanel();
            _panelMainSpacer = new KryptonPanel();
            _panelMainCommands = new KryptonPanel();
            _panelMainRadio = new KryptonPanel();
            _panelMainText = new KryptonPanel();
            _messageContent = new KryptonWrapLabel();
            _messageContentMultiline = new KryptonTextBox();
            _messageText = new KryptonWrapLabel();
            _panelIcon = new KryptonPanel();
            _messageIcon = new PictureBox();
            _panelButtons = new KryptonPanel();
            _checkBox = new KryptonCheckBox();
            _panelButtonsBorderTop = new KryptonBorderEdge();
            _buttonOK = new MessageButton();
            _buttonYes = new MessageButton();
            _buttonNo = new MessageButton();
            _buttonRetry = new MessageButton();
            _buttonCancel = new MessageButton();
            _buttonClose = new MessageButton();
            _panelFooter = new KryptonPanel();
            _linkLabelFooter = new KryptonLinkLabel();
            _iconFooter = new PictureBox();
            _footerLabel = new KryptonWrapLabel();
            _panelFooterBorderTop = new KryptonBorderEdge();
            ((ISupportInitialize)_panelMain).BeginInit();
            _panelMain.SuspendLayout();
            ((ISupportInitialize)_panelMainSpacer).BeginInit();
            ((ISupportInitialize)_panelMainCommands).BeginInit();
            ((ISupportInitialize)_panelMainRadio).BeginInit();
            ((ISupportInitialize)_panelMainText).BeginInit();
            _panelMainText.SuspendLayout();
            ((ISupportInitialize)_panelIcon).BeginInit();
            _panelIcon.SuspendLayout();
            ((ISupportInitialize)_messageIcon).BeginInit();
            ((ISupportInitialize)_panelButtons).BeginInit();
            _panelButtons.SuspendLayout();
            ((ISupportInitialize)_panelFooter).BeginInit();
            _panelFooter.SuspendLayout();
            ((ISupportInitialize)_iconFooter).BeginInit();
            SuspendLayout();
            // 
            // _panelMain
            // 
            _panelMain.AutoSize = true;
            _panelMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _panelMain.Controls.Add(_panelMainSpacer);
            _panelMain.Controls.Add(_panelMainCommands);
            _panelMain.Controls.Add(_panelMainRadio);
            _panelMain.Controls.Add(_panelMainText);
            _panelMain.Controls.Add(_panelIcon);
            _panelMain.Dock = DockStyle.Top;
            _panelMain.Location = new Point(0, 0);
            _panelMain.Name = "_panelMain";
            _panelMain.Size = new Size(544, 72);
            _panelMain.TabIndex = 0;
            // 
            // _panelMainSpacer
            // 
            _panelMainSpacer.Location = new Point(42, 59);
            _panelMainSpacer.Name = "_panelMainSpacer";
            _panelMainSpacer.Size = new Size(10, 10);
            _panelMainSpacer.TabIndex = 3;
            // 
            // _panelMainCommands
            // 
            _panelMainCommands.AutoSize = true;
            _panelMainCommands.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _panelMainCommands.Location = new Point(208, 10);
            _panelMainCommands.Name = "_panelMainCommands";
            _panelMainCommands.Size = new Size(0, 0);
            _panelMainCommands.TabIndex = 2;
            // 
            // _panelMainRadio
            // 
            _panelMainRadio.AutoSize = true;
            _panelMainRadio.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _panelMainRadio.Location = new Point(208, 32);
            _panelMainRadio.Name = "_panelMainRadio";
            _panelMainRadio.Size = new Size(0, 0);
            _panelMainRadio.TabIndex = 1;
            // 
            // _panelMainText
            // 
            _panelMainText.AutoSize = true;
            _panelMainText.Controls.Add(_messageContent);
            _panelMainText.Controls.Add(_messageContentMultiline);
            _panelMainText.Controls.Add(_messageText);
            _panelMainText.Location = new Point(42, 0);
            _panelMainText.Margin = new Padding(0);
            _panelMainText.Name = "_panelMainText";
            _panelMainText.Padding = new Padding(5, 5, 5, 0);
            _panelMainText.Size = new Size(357, 60);
            _panelMainText.TabIndex = 0;
            // 
            // _messageContent
            // 
            _messageContent.AutoSize = false;
            _messageContent.Font = new Font("Segoe UI", 9F);
            _messageContent.ForeColor = Color.FromArgb(30, 57, 91);
            _messageContent.LabelStyle = LabelStyle.NormalPanel;
            _messageContent.Location = new Point(6, 34);
            _messageContent.Margin = new Padding(0);
            _messageContent.Name = "_messageContent";
            _messageContent.Size = new Size(78, 15);
            _messageContent.Text = "Content";
            //
            // _messageContentMultiline
            //
            _messageContentMultiline.Location = new Point(48, 45);
            _messageContentMultiline.Multiline = true;
            _messageContentMultiline.Name = "_messageContentMultiline";
            _messageContentMultiline.ReadOnly = true;
            _messageContentMultiline.ScrollBars = ScrollBars.Both;
            _messageContentMultiline.Size = new Size(351, 10);
            _messageContentMultiline.TabIndex = 4;
            // 
            // _messageText
            // 
            _messageText.AutoSize = false;
            _messageText.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            _messageText.ForeColor = Color.FromArgb(30, 57, 91);
            _messageText.LabelStyle = LabelStyle.TitlePanel;
            _messageText.Location = new Point(5, 5);
            _messageText.Margin = new Padding(0);
            _messageText.Name = "_messageText";
            _messageText.Size = new Size(139, 27);
            _messageText.Text = "Message Text";
            // 
            // _panelIcon
            // 
            _panelIcon.AutoSize = true;
            _panelIcon.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _panelIcon.Controls.Add(_messageIcon);
            _panelIcon.Location = new Point(0, 0);
            _panelIcon.Margin = new Padding(0);
            _panelIcon.Name = "_panelIcon";
            _panelIcon.Padding = new Padding(10, 10, 0, 10);
            _panelIcon.Size = new Size(42, 52);
            _panelIcon.TabIndex = 0;
            // 
            // _messageIcon
            // 
            _messageIcon.BackColor = Color.Transparent;
            _messageIcon.Location = new Point(10, 10);
            _messageIcon.Margin = new Padding(0);
            _messageIcon.Name = "_messageIcon";
            _messageIcon.Size = new Size(32, 32);
            _messageIcon.TabIndex = 0;
            _messageIcon.TabStop = false;
            // 
            // _panelButtons
            // 
            _panelButtons.Controls.Add(_checkBox);
            _panelButtons.Controls.Add(_panelButtonsBorderTop);
            _panelButtons.Controls.Add(_buttonOK);
            _panelButtons.Controls.Add(_buttonYes);
            _panelButtons.Controls.Add(_buttonNo);
            _panelButtons.Controls.Add(_buttonRetry);
            _panelButtons.Controls.Add(_buttonCancel);
            _panelButtons.Controls.Add(_buttonClose);
            _panelButtons.Dock = DockStyle.Top;
            _panelButtons.Location = new Point(0, 72);
            _panelButtons.Margin = new Padding(0);
            _panelButtons.Name = "_panelButtons";
            _panelButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
            _panelButtons.Size = new Size(544, 46);
            _panelButtons.TabIndex = 1;
            // 
            // _checkBox
            // 
            _checkBox.LabelStyle = LabelStyle.NormalPanel;
            _checkBox.Location = new Point(12, 12);
            _checkBox.Name = "_checkBox";
            _checkBox.Size = new Size(75, 20);
            _checkBox.TabIndex = 0;
            _checkBox.Values.Text = "checkBox";
            _checkBox.CheckedChanged += checkBox_CheckedChanged;
            // 
            // _panelButtonsBorderTop
            // 
            _panelButtonsBorderTop.BorderStyle = PaletteBorderStyle.HeaderPrimary;
            _panelButtonsBorderTop.Dock = DockStyle.Top;
            _panelButtonsBorderTop.Location = new Point(0, 0);
            _panelButtonsBorderTop.Name = "_panelButtonsBorderTop";
            _panelButtonsBorderTop.Size = new Size(544, 1);
            _panelButtonsBorderTop.Text = "kryptonBorderEdge1";
            // 
            // _buttonOK
            // 
            _buttonOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonOK.AutoSize = true;
            _buttonOK.DialogResult = DialogResult.OK;
            _buttonOK.IgnoreAltF4 = false;
            _buttonOK.Location = new Point(435, 9);
            _buttonOK.Margin = new Padding(0);
            _buttonOK.MinimumSize = new Size(50, 26);
            _buttonOK.Name = "_buttonOK";
            _buttonOK.Size = new Size(50, 26);
            _buttonOK.TabIndex = 1;
            _buttonOK.Values.Text = "OK";
            _buttonOK.KeyDown += button_keyDown;
            // 
            // _buttonYes
            // 
            _buttonYes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonYes.AutoSize = true;
            _buttonYes.DialogResult = DialogResult.Yes;
            _buttonYes.IgnoreAltF4 = false;
            _buttonYes.Location = new Point(335, 9);
            _buttonYes.Margin = new Padding(0);
            _buttonYes.MinimumSize = new Size(50, 26);
            _buttonYes.Name = "_buttonYes";
            _buttonYes.Size = new Size(50, 26);
            _buttonYes.TabIndex = 2;
            _buttonYes.Values.Text = "Yes";
            _buttonYes.KeyDown += button_keyDown;
            // 
            // _buttonNo
            // 
            _buttonNo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonNo.AutoSize = true;
            _buttonNo.DialogResult = DialogResult.No;
            _buttonNo.IgnoreAltF4 = false;
            _buttonNo.Location = new Point(285, 9);
            _buttonNo.Margin = new Padding(0);
            _buttonNo.MinimumSize = new Size(50, 26);
            _buttonNo.Name = "_buttonNo";
            _buttonNo.Size = new Size(50, 26);
            _buttonNo.TabIndex = 3;
            _buttonNo.Values.Text = "No";
            _buttonNo.KeyDown += button_keyDown;
            // 
            // _buttonRetry
            // 
            _buttonRetry.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonRetry.AutoSize = true;
            _buttonRetry.DialogResult = DialogResult.Retry;
            _buttonRetry.IgnoreAltF4 = false;
            _buttonRetry.Location = new Point(385, 9);
            _buttonRetry.Margin = new Padding(0);
            _buttonRetry.MinimumSize = new Size(50, 26);
            _buttonRetry.Name = "_buttonRetry";
            _buttonRetry.Size = new Size(50, 26);
            _buttonRetry.TabIndex = 5;
            _buttonRetry.Values.Text = "Retry";
            _buttonRetry.KeyDown += button_keyDown;
            // 
            // _buttonCancel
            // 
            _buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonCancel.AutoSize = true;
            _buttonCancel.DialogResult = DialogResult.Cancel;
            _buttonCancel.IgnoreAltF4 = false;
            _buttonCancel.Location = new Point(228, 9);
            _buttonCancel.Margin = new Padding(0);
            _buttonCancel.MinimumSize = new Size(50, 26);
            _buttonCancel.Name = "_buttonCancel";
            _buttonCancel.Size = new Size(57, 26);
            _buttonCancel.TabIndex = 4;
            _buttonCancel.Values.Text = "Cancel";
            _buttonCancel.KeyDown += button_keyDown;
            // 
            // _buttonClose
            // 
            _buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonClose.AutoSize = true;
            _buttonClose.IgnoreAltF4 = false;
            _buttonClose.Location = new Point(485, 9);
            _buttonClose.Margin = new Padding(0);
            _buttonClose.MinimumSize = new Size(50, 26);
            _buttonClose.Name = "_buttonClose";
            _buttonClose.Size = new Size(50, 26);
            _buttonClose.TabIndex = 6;
            _buttonClose.Values.Text = "Close";
            _buttonClose.Click += _buttonClose_Click;
            _buttonClose.KeyDown += button_keyDown;
            // 
            // _panelFooter
            // 
            _panelFooter.Controls.Add(_linkLabelFooter);
            _panelFooter.Controls.Add(_iconFooter);
            _panelFooter.Controls.Add(_footerLabel);
            _panelFooter.Controls.Add(_panelFooterBorderTop);
            _panelFooter.Dock = DockStyle.Top;
            _panelFooter.Location = new Point(0, 118);
            _panelFooter.Name = "_panelFooter";
            _panelFooter.PanelBackStyle = PaletteBackStyle.PanelAlternate;
            _panelFooter.Size = new Size(544, 49);
            _panelFooter.TabIndex = 2;
            // 
            // _linkLabelFooter
            // 
            _linkLabelFooter.LabelStyle = LabelStyle.NormalPanel;
            _linkLabelFooter.Location = new Point(127, 11);
            _linkLabelFooter.Name = "_linkLabelFooter";
            _linkLabelFooter.Size = new Size(110, 20);
            _linkLabelFooter.TabIndex = 0;
            _linkLabelFooter.Values.Text = "kryptonLinkLabel1";
            _linkLabelFooter.LinkClicked += _linkLabelFooter_LinkClicked;
            // 
            // _iconFooter
            // 
            _iconFooter.BackColor = Color.Transparent;
            _iconFooter.Location = new Point(10, 10);
            _iconFooter.Margin = new Padding(0);
            _iconFooter.Name = "_iconFooter";
            _iconFooter.Size = new Size(16, 16);
            _iconFooter.TabIndex = 4;
            _iconFooter.TabStop = false;
            // 
            // _footerLabel
            // 
            _footerLabel.AutoSize = false;
            _footerLabel.Font = new Font("Segoe UI", 9F);
            _footerLabel.ForeColor = Color.FromArgb(30, 57, 91);
            _footerLabel.LabelStyle = LabelStyle.NormalPanel;
            _footerLabel.Location = new Point(36, 11);
            _footerLabel.Margin = new Padding(0);
            _footerLabel.Name = "_footerLabel";
            _footerLabel.Size = new Size(78, 15);
            _footerLabel.Text = "Content";
            // 
            // _panelFooterBorderTop
            // 
            _panelFooterBorderTop.BorderStyle = PaletteBorderStyle.HeaderPrimary;
            _panelFooterBorderTop.Dock = DockStyle.Top;
            _panelFooterBorderTop.Location = new Point(0, 0);
            _panelFooterBorderTop.Name = "_panelFooterBorderTop";
            _panelFooterBorderTop.Size = new Size(544, 1);
            _panelFooterBorderTop.Text = "kryptonBorderEdge1";
            // 
            // VisualTaskDialog
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(561, 164);
            Controls.Add(_panelFooter);
            Controls.Add(_panelButtons);
            Controls.Add(_panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VisualTaskDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            FormClosing += OnTaskDialogFormClosing;
            ((ISupportInitialize)_panelMain).EndInit();
            _panelMain.ResumeLayout(false);
            _panelMain.PerformLayout();
            ((ISupportInitialize)_panelMainSpacer).EndInit();
            ((ISupportInitialize)_panelMainCommands).EndInit();
            ((ISupportInitialize)_panelMainRadio).EndInit();
            ((ISupportInitialize)_panelMainText).EndInit();
            _panelMainText.ResumeLayout(false);
            ((ISupportInitialize)_panelIcon).EndInit();
            _panelIcon.ResumeLayout(false);
            ((ISupportInitialize)_messageIcon).EndInit();
            ((ISupportInitialize)_panelButtons).EndInit();
            _panelButtons.ResumeLayout(false);
            _panelButtons.PerformLayout();
            ((ISupportInitialize)_panelFooter).EndInit();
            _panelFooter.ResumeLayout(false);
            _panelFooter.PerformLayout();
            ((ISupportInitialize)_iconFooter).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion
    }
}
