// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Muratoner, Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays an input box for the user.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonInputBoxOld), "ToolboxBitmaps.KryptonInputBox.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonInputBoxOld : KryptonForm
    {
        #region Static Fields
        private const int GAP = 10;
        #endregion

        #region Instance Fields
        private readonly string _prompt;
        private readonly string _caption;
        private readonly string _defaultResponse;
        private readonly char _passwordChar;
        private KryptonPanel _panelMessage;
        private KryptonWrapLabel _labelPrompt;
        private KryptonButton _buttonOK;
        private KryptonTextBox ktxtResponse;
        private KryptonButton _buttonCancel;
        #endregion

        #region Identity
        private KryptonInputBoxOld(string prompt,
                                string caption,
                                string defaultResponse,
                                char passwordChar)
        {
            // Store incoming values
            _prompt = prompt;
            _caption = caption;
            _defaultResponse = defaultResponse;
            _passwordChar = passwordChar;

            // Create the form contents
            InitializeComponent();

            // Update contents to match requirements
            UpdateText();
            UpdateButtons();

            // Finally calculate and set form sizing
            UpdateSizing();
            AcceptButton = _buttonOK;
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
        /// Displays an input box in front of the specified object and with the provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">Default response text..</param>
        /// <param name="passwordChar"></param>
        /// <returns>Input string.</returns>
        public static string Show(IWin32Window owner, string prompt, string caption = @"", string defaultResponse = @"", char passwordChar = ' ')
        {
            return InternalShow(owner, prompt, caption, defaultResponse, passwordChar);
        }
        #endregion

        #region Implementation
        private static string InternalShow(IWin32Window owner,
                                           string prompt,
                                           string caption,
                                           string defaultResponse,
                                           char passwordChar)
        {
            // If do not have an owner passed in then get the active window and use that instead
            IWin32Window showOwner = owner ?? FromHandle(PI.GetActiveWindow());

            // Show input box window as a modal dialog and then dispose of it afterwards
            using (KryptonInputBoxOld ib = new KryptonInputBoxOld(prompt, caption, defaultResponse, passwordChar))
            {
                ib.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return ib.ShowDialog(showOwner) == DialogResult.OK ? ib.InputResponse : string.Empty;
            }
        }

        internal string InputResponse => ktxtResponse.Text;

        private void UpdateText()
        {
            Text = _caption;
            _labelPrompt.Text = _prompt;
           // ktxtResponse.Text = _defaultResponse;
            ktxtResponse.PasswordChar = _passwordChar;
        }

        private void UpdateButtons()
        {
            _buttonOK.Text = KryptonManager.Strings.OK;
            _buttonCancel.Text = KryptonManager.Strings.Cancel;
        }

        private void UpdateSizing()
        {
            Size buttonSize = UpdateButtonSizing();
            Size promptSize = UpdatePromptSizing();
            //Size responseSize = UpdateResponseSizing();
            ClientSize = new Size(_buttonCancel.Right + GAP, ktxtResponse.Bottom + GAP);
        }

        private Size UpdatePromptSizing()
        {
            // Update size of the message label but with a maximum width
            using (Graphics g = CreateGraphics())
            {
                // Find size of the label when it has a maximum length of 250, this tells us the height
                // required to fully show the label with the prompt.
                _labelPrompt.UpdateFont();
                Size messageSize = g.MeasureString(_prompt, _labelPrompt.Font, 250).ToSize();

                // Work out DPI adjustment factor
                float factorX = g.DpiX > 96 ? ((1.0f * g.DpiX) / 96) : 1.0f;
                float factorY = g.DpiY > 96 ? ((1.0f * g.DpiY) / 96) : 1.0f;
                messageSize.Width = (int)(messageSize.Width * factorX);
                messageSize.Height = (int)(messageSize.Height * factorY);

                _labelPrompt.Location = new Point(GAP, GAP);
                _labelPrompt.Size = new Size(255, Math.Max(messageSize.Height, _buttonCancel.Bottom - _buttonOK.Top));

                return new Size(_labelPrompt.Right, _labelPrompt.Bottom);
            }
        }

        private Size UpdateButtonSizing()
        {
            Size buttonOKSize = _buttonOK.GetPreferredSize(Size.Empty);
            Size buttonCancelSize = _buttonCancel.GetPreferredSize(Size.Empty);

            // Make both buttons the size of the largest one
            Size largestButton = new Size(Math.Max(buttonOKSize.Width, buttonCancelSize.Width), Math.Max(buttonOKSize.Height, buttonCancelSize.Height));
            _buttonOK.Size = largestButton;
            _buttonCancel.Size = largestButton;

            // Position the buttons relative to the top left of the owning panel
            _buttonOK.Location = new Point(_panelMessage.Right - _buttonOK.Width - GAP, GAP);
            _buttonCancel.Location = new Point(_panelMessage.Right - _buttonCancel.Width - GAP, _buttonOK.Bottom + (GAP / 2));

            // We need enough space for the buttons and gaps on either size
            return new Size(_buttonOK.Left + GAP, _buttonCancel.Bottom + GAP);
        }

        //private Size UpdateResponseSizing()
        //{
        //    // Position the reponse text box below the prompt
        //    ktxtResponse.Location = new Point(GAP, _labelPrompt.Bottom + GAP);
        //    ktxtResponse.Width = _buttonOK.Right - ktxtResponse.Left;
        //    return ktxtResponse.Size;
        //}

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
            this._labelPrompt = new Krypton.Toolkit.KryptonWrapLabel();
            this._buttonCancel = new Krypton.Toolkit.KryptonButton();
            this._buttonOK = new Krypton.Toolkit.KryptonButton();
            this.ktxtResponse = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this._panelMessage)).BeginInit();
            this._panelMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelMessage
            // 
            this._panelMessage.Controls.Add(this.ktxtResponse);
            this._panelMessage.Controls.Add(this._labelPrompt);
            this._panelMessage.Controls.Add(this._buttonCancel);
            this._panelMessage.Controls.Add(this._buttonOK);
            this._panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMessage.Location = new System.Drawing.Point(0, 0);
            this._panelMessage.Name = "_panelMessage";
            this._panelMessage.Size = new System.Drawing.Size(357, 118);
            this._panelMessage.TabIndex = 0;
            // 
            // _labelPrompt
            // 
            this._labelPrompt.AutoSize = false;
            this._labelPrompt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._labelPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this._labelPrompt.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this._labelPrompt.Location = new System.Drawing.Point(12, 12);
            this._labelPrompt.Margin = new System.Windows.Forms.Padding(0);
            this._labelPrompt.Name = "_labelPrompt";
            this._labelPrompt.Size = new System.Drawing.Size(78, 15);
            this._labelPrompt.Text = "Prompt";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.AutoSize = true;
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(295, 43);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(0);
            this._buttonCancel.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(50, 26);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Values.Text = "Cancel";
            // 
            // _buttonOK
            // 
            this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOK.AutoSize = true;
            this._buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOK.Location = new System.Drawing.Point(295, 12);
            this._buttonOK.Margin = new System.Windows.Forms.Padding(0);
            this._buttonOK.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(50, 26);
            this._buttonOK.TabIndex = 1;
            this._buttonOK.Values.Text = "OK";
            // 
            // ktxtResponse
            // 
            this.ktxtResponse.Location = new System.Drawing.Point(12, 83);
            this.ktxtResponse.Name = "ktxtResponse";
            this.ktxtResponse.Size = new System.Drawing.Size(333, 23);
            this.ktxtResponse.TabIndex = 3;
            // 
            // KryptonInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 118);
            this.Controls.Add(this._panelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonInputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this._panelMessage)).EndInit();
            this._panelMessage.ResumeLayout(false);
            this._panelMessage.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
