#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion


#nullable enable
namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays an input box for the user.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonInputBox), "ToolboxBitmaps.KryptonInputBox.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonInputBox : KryptonForm
    {
        #region Static Fields
        private static readonly int GAP = 10;
        #endregion

        #region Instance Fields
        private readonly bool _usePasswordOption;
        private readonly Color _cueColour;
        private readonly string _prompt;
        private readonly string _caption;
        private readonly string _defaultResponse;
        private readonly string _cueText;
        private readonly Font _cueTypeface;
        private KryptonPanel _panelMessage;
        private KryptonWrapLabel _labelPrompt;
        private KryptonTextBox _textBoxResponse;
        private KryptonButton _buttonOk;
        private KryptonButton _buttonCancel;
        #endregion

        #region Identity
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private KryptonInputBox(string prompt,
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
                                string caption,
                                string defaultResposne,
                                string cueText,
                                Color? cueColour,
                                Font? cueTypeface,
                                bool? usePasswordOption)
        {
            // Store incoming values
            _prompt = prompt;
            _caption = caption;
            _defaultResponse = defaultResposne;
            _cueText = cueText;
            _cueColour = cueColour ?? Color.Gray;
            _cueTypeface = cueTypeface ?? new Font("Segoe UI", 9f);
            _usePasswordOption = usePasswordOption ?? false;

            // Create the form contents
            InitializeComponent();

            // Update contents to match requirements
            UpdateText();

            UpdateCue();

            UpdateButtons();

            // Finally calculate and set form sizing
            UpdateSizing();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Displays an input box with the provided prompt.
        /// </summary>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <returns>Input string.</returns>
        public static string Show(string prompt) => InternalShow(null, prompt, string.Empty, string.Empty, string.Empty, null, null, null);

        /// <summary>
        /// Displays an input box in front of the specified object and with the provided prompt.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <returns>Input string.</returns>
        public static string Show(IWin32Window owner, string prompt) => InternalShow(owner, prompt, string.Empty, string.Empty, string.Empty, null, null, null);

        /// <summary>
        /// Displays an input box with provided prompt and caption.
        /// </summary>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <returns>Input string.</returns>
        public static string Show(string prompt, string caption) => InternalShow(null, prompt, caption, string.Empty, string.Empty, null, null, null);

        /// <summary>
        /// Displays an input box in front of the specified object and with the provided prompt and caption.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <returns>Input string.</returns>
        public static string Show(IWin32Window owner, string prompt, string caption) => InternalShow(owner, prompt, caption, string.Empty, string.Empty, null, null, null);

        /// <summary>
        /// Displays an input box with provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">Default response text..</param>
        /// <returns>Input string.</returns>
        public static string Show(string prompt, string caption, string defaultResponse) => InternalShow(null, prompt, caption, defaultResponse, string.Empty, null, null, null);

        /// <summary>
        /// Displays an input box in front of the specified object and with the provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">Default response text..</param>
        /// <returns>Input string.</returns>
        public static string Show(IWin32Window owner, string prompt, string caption, string defaultResponse) => InternalShow(owner, prompt, caption, defaultResponse, string.Empty, null, null, null);

        /// <summary>
        /// Displays an input box with provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">Default response text..</param>
        /// <param name="cueText">The cue text.</param>
        /// <param name="cueColour">The colour of the cue.</param>
        /// <param name="cueTypeface">The cue font.</param>
        /// <param name="usePasswordOption">Enables the password option.</param>
        /// <returns>Input string.</returns>
        public static string Show(string prompt, string caption, string defaultResponse, string cueText, Color cueColour, Font cueTypeface, bool usePasswordOption) => InternalShow(null, prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);

        /// <summary>
        /// DDisplays an input box in front of the specified object and with the provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">Default response text..</param>
        /// <param name="cueText">The cue text.</param>
        /// <param name="cueColour">The colour of the cue.</param>
        /// <param name="cueTypeface">The cue font.</param>
        /// <param name="usePasswordOption">Enables the password option.</param>
        /// <returns>Input string.</returns>
        public static string Show(IWin32Window owner, string prompt, string caption, string defaultResponse, string cueText, Color cueColour, Font cueTypeface, bool usePasswordOption) => InternalShow(owner, prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);

        #endregion

        #region Implementation
        private static string InternalShow(IWin32Window? owner,
                                           string prompt,
                                           string caption,
                                           string defaultResponse,
                                           string cueText,
                                           Color? cueColour,
                                           Font? cueTypeface,
                                           bool? usePasswordOption)
        {
            // If do not have an owner passed in then get the active window and use that instead
            IWin32Window showOwner = owner ?? FromHandle(PI.GetActiveWindow());

            // Show input box window as a modal dialog and then dispose of it afterwards
            using KryptonInputBox ib = new(prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);
            if (showOwner == null)
                ib.StartPosition = FormStartPosition.CenterScreen;
            else
                ib.StartPosition = FormStartPosition.CenterParent;

            return ib.ShowDialog(showOwner) == DialogResult.OK ? ib.InputResponse : string.Empty;
        }

        internal string InputResponse => _textBoxResponse.Text;

        private void UpdateText()
        {
            Text = _caption;
            _labelPrompt.Text = _prompt;
            _textBoxResponse.Text = _defaultResponse;
            _textBoxResponse.UseSystemPasswordChar = _usePasswordOption;
        }

        private void UpdateCue()
        {
            _textBoxResponse.CueHint.CueHintText = _cueText;

            _textBoxResponse.CueHint.Color1 = _cueColour;

            _textBoxResponse.CueHint.Font = _cueTypeface;
        }

        private void UpdateButtons()
        {
            _buttonOk.Text = KryptonManager.Strings.OK;
            _buttonCancel.Text = KryptonManager.Strings.Cancel;
        }

        private void UpdateSizing()
        {
            UpdateButtonSizing();
            UpdatePromptSizing();
            UpdateResponseSizing();
            ClientSize = new Size(_buttonCancel.Right + GAP, _textBoxResponse.Bottom + GAP);
        }

        private void UpdatePromptSizing()
        {
            // Update size of the message label but with a maximum width
            using Graphics g = CreateGraphics();
            // Find size of the label when it has a maximum length of 250, this tells us the height
            // required to fully show the label with the prompt.
            _labelPrompt.UpdateFont();
            Size messageSize = g.MeasureString(_prompt, _labelPrompt.Font, 250).ToSize();

            // Work out DPI adjustment factor
            float factorX = g.DpiX > 96 ? (1.0f * g.DpiX / 96) : 1.0f;
            float factorY = g.DpiY > 96 ? (1.0f * g.DpiY / 96) : 1.0f;
            messageSize.Width = (int)(messageSize.Width * factorX);
            messageSize.Height = (int)(messageSize.Height * factorY);

            _labelPrompt.Location = new Point(GAP, GAP);
            _labelPrompt.Size = new Size(255, Math.Max(messageSize.Height, _buttonCancel.Bottom - _buttonOk.Top));

        }

        private void UpdateButtonSizing()
        {
            Size buttonOKSize = _buttonOk.GetPreferredSize(Size.Empty);
            Size buttonCancelSize = _buttonCancel.GetPreferredSize(Size.Empty);

            // Make both buttons the size of the largest one
            Size largestButton = new(Math.Max(buttonOKSize.Width, buttonCancelSize.Width), Math.Max(buttonOKSize.Height, buttonCancelSize.Height));
            _buttonOk.Size = largestButton;
            _buttonCancel.Size = largestButton;

            // Position the buttons relative to the top left of the owning panel
            _buttonOk.Location = new Point(_panelMessage.Right - _buttonOk.Width - GAP, GAP);
            _buttonCancel.Location = new Point(_panelMessage.Right - _buttonCancel.Width - GAP, _buttonOk.Bottom + GAP / 2);

        }

        private void UpdateResponseSizing()
        {
            // Position the response text box below the prompt
            _textBoxResponse.Location = new Point(GAP, _labelPrompt.Bottom + GAP);
            _textBoxResponse.Width = _buttonOk.Right - _textBoxResponse.Left;
        }

        private void button_keyDown(object sender, KeyEventArgs e)
        {
            // Escape key kills the dialog if we allow it to be closed
            if ((e.KeyCode == Keys.Escape) && ControlBox)
                Close();
        }

        private void InitializeComponent()
        {
            _panelMessage = new KryptonPanel();
            _textBoxResponse = new KryptonTextBox();
            _labelPrompt = new KryptonWrapLabel();
            _buttonCancel = new KryptonButton();
            _buttonOk = new KryptonButton();
            ((ISupportInitialize)(_panelMessage)).BeginInit();
            _panelMessage.SuspendLayout();
            SuspendLayout();
            // 
            // _panelMessage
            // 
            _panelMessage.Controls.Add(_textBoxResponse);
            _panelMessage.Controls.Add(_labelPrompt);
            _panelMessage.Controls.Add(_buttonCancel);
            _panelMessage.Controls.Add(_buttonOk);
            _panelMessage.Dock = DockStyle.Fill;
            _panelMessage.Location = new Point(0, 0);
            _panelMessage.Name = "_panelMessage";
            _panelMessage.Size = new Size(357, 118);
            _panelMessage.TabIndex = 0;
            // 
            // _textBoxResponse
            // 
            _textBoxResponse.Location = new Point(12, 86);
            _textBoxResponse.Name = "_textBoxResponse";
            _textBoxResponse.Size = new Size(333, 20);
            _textBoxResponse.TabIndex = 0;
            // 
            // _labelPrompt
            // 
            _labelPrompt.AutoSize = false;
            _labelPrompt.Font = new Font("Segoe UI", 9F);
            _labelPrompt.ForeColor = Color.FromArgb(30, 57, 91);
            _labelPrompt.LabelStyle = LabelStyle.NormalPanel;
            _labelPrompt.Location = new Point(12, 12);
            _labelPrompt.Margin = new Padding(0);
            _labelPrompt.Name = "_labelPrompt";
            _labelPrompt.Size = new Size(78, 15);
            _labelPrompt.Text = @"Prompt";
            // 
            // _buttonCancel
            // 
            _buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonCancel.AutoSize = true;
            _buttonCancel.DialogResult = DialogResult.Cancel;
            _buttonCancel.Location = new Point(295, 43);
            _buttonCancel.Margin = new Padding(0);
            _buttonCancel.MinimumSize = new Size(50, 26);
            _buttonCancel.Name = "_buttonCancel";
            _buttonCancel.Size = new Size(50, 26);
            _buttonCancel.TabIndex = 2;
            _buttonCancel.Values.Text = KryptonManager.GlobalStrings.Cancel;
            _buttonCancel.KeyDown += button_keyDown;
            // 
            // _buttonOK
            // 
            _buttonOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonOk.AutoSize = true;
            _buttonOk.DialogResult = DialogResult.OK;
            _buttonOk.Location = new Point(295, 12);
            _buttonOk.Margin = new Padding(0);
            _buttonOk.MinimumSize = new Size(50, 26);
            _buttonOk.Name = "_buttonOk";
            _buttonOk.Size = new Size(50, 26);
            _buttonOk.TabIndex = 1;
            _buttonOk.Values.Text = KryptonManager.GlobalStrings.OK;
            _buttonOk.KeyDown += button_keyDown;
            // 
            // KryptonInputBox
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(357, 118);
            Controls.Add(_panelMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KryptonInputBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            ((ISupportInitialize)(_panelMessage)).EndInit();
            _panelMessage.ResumeLayout(false);
            _panelMessage.PerformLayout();
            ResumeLayout(false);

        }
        #endregion
    }
}