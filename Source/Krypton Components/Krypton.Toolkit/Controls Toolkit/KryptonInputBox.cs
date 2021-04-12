#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// The <see cref="KryptonInputBox"/> class.
    /// </summary>
    /// <seealso cref="Krypton.Toolkit.KryptonForm" />
    public class KryptonInputBox : KryptonForm
    {
        #region Design Code
        private KryptonButton kbtnOk;
        private KryptonButton kbtnCancel;
        private KryptonPanel kryptonPanel2;
        private KryptonTextBox ktxtPrompt;
        private KryptonWrapLabel kwlMessage;
        private System.Windows.Forms.Panel panel1;
        private KryptonPanel kryptonPanel1;

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.ktxtPrompt = new Krypton.Toolkit.KryptonTextBox();
            this.kwlMessage = new Krypton.Toolkit.KryptonWrapLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnOk);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 69);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(357, 49);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(159, 12);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 1;
            this.kbtnOk.Values.Text = "&OK";
            this.kbtnOk.Click += new System.EventHandler(this.kbtnOk_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(255, 12);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 0;
            this.kbtnCancel.Values.Text = "&Cancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.ktxtPrompt);
            this.kryptonPanel2.Controls.Add(this.kwlMessage);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(357, 69);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // ktxtPrompt
            // 
            this.ktxtPrompt.Location = new System.Drawing.Point(12, 37);
            this.ktxtPrompt.Name = "ktxtPrompt";
            this.ktxtPrompt.Size = new System.Drawing.Size(333, 23);
            this.ktxtPrompt.TabIndex = 1;
            this.ktxtPrompt.TextChanged += new System.EventHandler(this.ktxtPrompt_TextChanged);
            this.ktxtPrompt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ktxtPrompt_KeyDown);
            // 
            // kwlMessage
            // 
            this.kwlMessage.AutoSize = false;
            this.kwlMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlMessage.Location = new System.Drawing.Point(0, 0);
            this.kwlMessage.Name = "kwlMessage";
            this.kwlMessage.Size = new System.Drawing.Size(357, 31);
            this.kwlMessage.Text = "{0}";
            this.kwlMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 3);
            this.panel1.TabIndex = 2;
            // 
            // KryptonInputBox
            // 
            this.AcceptButton = this.kbtnOk;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(357, 118);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonInputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KryptonInputBoxTest_FormClosing);
            this.Load += new System.EventHandler(this.KryptonInputBoxTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="KryptonInputBox"/> class.</summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="okText">The ok text.</param>
        /// <param name="cancelText">The cancel text.</param>
        /// <param name="passwordEnabled">if set to <c>true</c> [password enabled].</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="inputTextAlignment">The input text alignment.</param>
        public KryptonInputBox(string title, string message, string prompt = "", string okText = "O&k", string cancelText = "&Cancel", bool passwordEnabled = false, FormStartPosition startPosition = FormStartPosition.Manual, HorizontalAlignment inputTextAlignment = HorizontalAlignment.Left)        
        {
            InitializeComponent();

            SetMessage(message);

            SetPrompt(prompt);

            SetOkText(okText);

            SetCancelText(cancelText);

            SetPasswordEnabled(passwordEnabled);

            SetTitle(title);

            SetStartPosition(startPosition);

            SetPromptTextAlignment(inputTextAlignment);
        }
        #endregion

        private void KryptonInputBoxTest_Load(object sender, EventArgs e)
        {

        }

        private void KryptonInputBoxTest_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ktxtPrompt_TextChanged(object sender, EventArgs e) => EnableOkButton(ktxtPrompt.Text != "");

        private void ktxtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) kbtnOk.PerformClick();
        }

        private void kbtnCancel_Click(object sender, EventArgs e)
        {

        }

        private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        #region Methods
        private void SetPasswordEnabled(bool passwordEnabled) => ktxtPrompt.UseSystemPasswordChar = passwordEnabled;

        private void SetOkText(string okText) => kbtnOk.Text = okText;

        private void SetCancelText(string cancelText) => kbtnCancel.Text = cancelText;

        private void SetPrompt(string prompt) => ktxtPrompt.Hint = prompt;

        private void SetMessage(string message) => kwlMessage.Text = message;

        private void SetStartPosition(FormStartPosition startPosition) => StartPosition = startPosition;

        private void SetTitle(string title) => Text = title;

        /// <summary>Retrieves the user response.</summary>
        /// <returns>The user's response.</returns>
        public string RetrieveUserResponse() => ktxtPrompt.Text;

        /// <summary>Enables the ok button.</summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void EnableOkButton(bool value)
        {
            if (value)
            {
                kbtnOk.Enabled = true;
            }
            else
            {
                kbtnOk.Enabled = false;
            }
        }

        /// <summary>Internals the show.</summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="okText">The ok text.</param>
        /// <param name="cancelText">The cancel text.</param>
        /// <param name="passwordEnabled">if set to <c>true</c> [password enabled].</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="inputTextAlignment">The input text alignment.</param>
        /// <returns></returns>
        private static string InternalShow(IWin32Window owner, string title, string message, string prompt = "", string okText = "O&k", string cancelText = "&Cancel", bool passwordEnabled = false, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation, HorizontalAlignment inputTextAlignment = HorizontalAlignment.Left)
        {
            IWin32Window showOwner = owner ?? FromHandle(PI.GetActiveWindow());

            using (KryptonInputBox kib = new KryptonInputBox(title, message, prompt, okText, cancelText, passwordEnabled, startPosition, inputTextAlignment))
            {
                kib.StartPosition = showOwner == null ? startPosition : startPosition;

                return kib.ShowDialog(showOwner) == DialogResult.OK ? kib.GetUserResponse() : string.Empty;
            }
        }

        private string GetUserResponse() => ktxtPrompt.Text;


        /// <summary>Shows the specified owner.</summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="okText">The ok text.</param>
        /// <param name="cancelText">The cancel text.</param>
        /// <param name="passwordEnabled">if set to <c>true</c> [password enabled].</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="inputTextAlignment">The input text alignment.</param>
        /// <returns>A new KryptonInputBox</returns>
        public static string Show(IWin32Window owner, string title, string message, string prompt = "", string okText = "O&k", string cancelText = "&Cancel", bool passwordEnabled = false, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation, HorizontalAlignment inputTextAlignment = HorizontalAlignment.Left) => InternalShow(owner, title, message, prompt, okText, cancelText, passwordEnabled, startPosition, inputTextAlignment);

        private void SetPromptTextAlignment(HorizontalAlignment alignment) => ktxtPrompt.TextAlign = alignment;
        #endregion
    }
}