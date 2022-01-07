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
    /// Displays an input box for the user.
    /// </summary>
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class KryptonInputBox : KryptonForm
    {
        #region Instance Fields
        private readonly bool _usePasswordOption;
        private readonly Color _cueColour;
        private readonly string _prompt;
        private readonly string _caption;
        private readonly string _defaultResponse;
        private readonly string _cueText;
        private readonly Font _cueTypeface;
        #endregion

        private KryptonInputBox()
        {
            InitializeComponent();
        }

        #region Identity
        private KryptonInputBox(string prompt,
                                string caption,
                                string defaultResponse,
                                string cueText,
                                Color cueColour,
                                Font cueTypeface,
                                bool usePasswordOption)
        {
            // Store incoming values
            _prompt = prompt;
            _caption = caption;
            _defaultResponse = defaultResponse;
            _cueText = cueText;
            _cueColour = cueColour;
            _cueTypeface = cueTypeface;
            _usePasswordOption = usePasswordOption;

            // Create the form contents
            InitializeComponent();

            // Update contents to match requirements
            UpdateText();

            UpdateCue();

            UpdateButtons();
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
        public static string Show(string prompt, 
            string caption = @"",
            string defaultResponse = @"",
            string cueText = @"",
            Color cueColour = new Color(),
            Font cueTypeface = null,
            bool usePasswordOption = false)
            => InternalShow(null, prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);

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
        public static string Show(IWin32Window owner, string prompt, 
            string caption = @"",
            string defaultResponse = @"",
            string cueText = @"",
            Color cueColour = new Color(),
            Font cueTypeface = null,
            bool usePasswordOption = false)
            => InternalShow(owner, prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);

        #endregion

        #region Implementation
        private static string InternalShow(IWin32Window owner,
                                           string prompt,
                                           string caption,
                                           string defaultResponse,
                                           string cueText,
                                           Color cueColour,
                                           Font cueTypeface,
                                           bool usePasswordOption)
        {
            // If do not have an owner passed in then get the active window and use that instead
            IWin32Window showOwner = owner ?? FromHandle(PI.GetActiveWindow());

            // Show input box window as a modal dialog and then dispose of it afterwards
            using KryptonInputBox ib = new(prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);
            ib.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return ib.ShowDialog(showOwner) == DialogResult.OK ? ib.InputResponse : string.Empty;
        }

        private void textBoxResponse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _buttonOk.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                _buttonCancel.PerformClick();
            }
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

            if ( !_cueColour.IsEmpty )
            {
                _textBoxResponse.CueHint.Color1 = _cueColour;
            }

            if ( _cueTypeface != null )
            {
                _textBoxResponse.CueHint.Font = _cueTypeface;
            }
        }

        private void UpdateButtons()
        {
            _buttonOk.Text = KryptonManager.Strings.OK;
            _buttonCancel.Text = KryptonManager.Strings.Cancel;
        }



        private void button_keyDown(object sender, KeyEventArgs e)
        {
            // Escape key kills the dialog if we allow it to be closed
            if ((e.KeyCode == Keys.Escape) && ControlBox)
            {
                Close();
            }
        }

        private void InitializeComponent()
        {
            this._panelMessage = new Krypton.Toolkit.KryptonPanel();
            this._tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._labelPrompt = new Krypton.Toolkit.KryptonWrapLabel();
            this._textBoxResponse = new Krypton.Toolkit.KryptonTextBox();
            this._kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this._kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this._buttonOk = new Krypton.Toolkit.KryptonButton();
            this._buttonCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)this._panelMessage).BeginInit();
            this._panelMessage.SuspendLayout();
            this._tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this._kryptonPanel1).BeginInit();
            this._kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelMessage
            // 
            this._panelMessage.Controls.Add(this._tableLayoutPanel1);
            this._panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMessage.Location = new System.Drawing.Point(0, 0);
            this._panelMessage.Margin = new System.Windows.Forms.Padding(4);
            this._panelMessage.Name = "_panelMessage";
            this._panelMessage.Size = new System.Drawing.Size(466, 131);
            this._panelMessage.TabIndex = 0;
            // 
            // _tableLayoutPanel1
            // 
            this._tableLayoutPanel1.AutoSize = true;
            this._tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._tableLayoutPanel1.ColumnCount = 1;
            this._tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._tableLayoutPanel1.Controls.Add(this._labelPrompt, 0, 0);
            this._tableLayoutPanel1.Controls.Add(this._textBoxResponse, 0, 1);
            this._tableLayoutPanel1.Controls.Add(this._kryptonBorderEdge1, 0, 2);
            this._tableLayoutPanel1.Controls.Add(this._kryptonPanel1, 0, 3);
            this._tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this._tableLayoutPanel1.Name = "_tableLayoutPanel1";
            this._tableLayoutPanel1.RowCount = 4;
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanel1.Size = new System.Drawing.Size(466, 131);
            this._tableLayoutPanel1.TabIndex = 3;
            // 
            // _labelPrompt
            // 
            this._labelPrompt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._labelPrompt.ForeColor = System.Drawing.Color.FromArgb((int)(byte)30, (int)(byte)57, (int)(byte)91);
            this._labelPrompt.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this._labelPrompt.Location = new System.Drawing.Point(5, 5);
            this._labelPrompt.Margin = new System.Windows.Forms.Padding(5);
            this._labelPrompt.Name = "_labelPrompt";
            this._labelPrompt.Size = new System.Drawing.Size(58, 20);
            this._labelPrompt.Text = "Prompt";
            // 
            // _textBoxResponse
            // 
            this._textBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBoxResponse.Location = new System.Drawing.Point(10, 34);
            this._textBoxResponse.Margin = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this._textBoxResponse.MinimumSize = new System.Drawing.Size(444, 27);
            this._textBoxResponse.Name = "_textBoxResponse";
            this._textBoxResponse.Size = new System.Drawing.Size(446, 27);
            this._textBoxResponse.TabIndex = 0;
            this._textBoxResponse.KeyDown += this.textBoxResponse_KeyDown;
            // 
            // _kryptonBorderEdge1
            // 
            this._kryptonBorderEdge1.AutoSize = false;
            this._kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonBorderEdge1.Location = new System.Drawing.Point(0, 75);
            this._kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this._kryptonBorderEdge1.Name = "_kryptonBorderEdge1";
            this._kryptonBorderEdge1.Size = new System.Drawing.Size(466, 1);
            this._kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // _kryptonPanel1
            // 
            this._kryptonPanel1.Controls.Add(this._buttonOk);
            this._kryptonPanel1.Controls.Add(this._buttonCancel);
            this._kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonPanel1.Location = new System.Drawing.Point(3, 89);
            this._kryptonPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._kryptonPanel1.Name = "_kryptonPanel1";
            this._kryptonPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this._kryptonPanel1.Size = new System.Drawing.Size(460, 32);
            this._kryptonPanel1.TabIndex = 2;
            // 
            // _buttonOk
            // 
            this._buttonOk.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this._buttonOk.AutoSize = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(283, 0);
            this._buttonOk.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this._buttonOk.MinimumSize = new System.Drawing.Size(67, 32);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(73, 32);
            this._buttonOk.TabIndex = 1;
            this._buttonOk.Values.Text = "&OK";
            this._buttonOk.KeyDown += this.button_keyDown;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this._buttonCancel.AutoSize = true;
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(378, 0);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(0);
            this._buttonCancel.MinimumSize = new System.Drawing.Size(67, 32);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(73, 32);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Values.Text = "Cance&l";
            this._buttonCancel.KeyDown += this.button_keyDown;
            // 
            // KryptonInputBox
            // 
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(466, 131);
            this.Controls.Add(this._panelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonInputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += this.KryptonInputBox_Load;
            ((System.ComponentModel.ISupportInitialize)this._panelMessage).EndInit();
            this._panelMessage.ResumeLayout(false);
            this._panelMessage.PerformLayout();
            this._tableLayoutPanel1.ResumeLayout(false);
            this._tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this._kryptonPanel1).EndInit();
            this._kryptonPanel1.ResumeLayout(false);
            this._kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private KryptonPanel _panelMessage;
        private KryptonWrapLabel _labelPrompt;
        private KryptonTextBox _textBoxResponse;
        private KryptonBorderEdge _kryptonBorderEdge1;
        private TableLayoutPanel _tableLayoutPanel1;
        private KryptonPanel _kryptonPanel1;
        private KryptonButton _buttonOk;
        private KryptonButton _buttonCancel;

        private void KryptonInputBox_Load(object sender, EventArgs e)
        {
            // Make sure that the "Form" is set to the auto size of the table
            ClientSize = _tableLayoutPanel1.Size;
        }
        #endregion Implementation
    }
}