// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  Created by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2020 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonInputBox), "ToolboxBitmaps.KryptonInputBox.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonInputBoxNew : KryptonForm
    {
        #region Design Code
        private KryptonButton kbtnOK;
        private KryptonButton kbtnCancel;
        private KryptonPanel kryptonPanel2;
        private KryptonTextBox ktxtPrompt;
        private KryptonWrapLabel kwlMessage;
        private System.Windows.Forms.Panel panel1;
        private KryptonPanel kryptonPanel1;

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOK = new Krypton.Toolkit.KryptonButton();
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
            this.kryptonPanel1.Controls.Add(this.kbtnOK);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 212);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(622, 49);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnOK
            // 
            this.kbtnOK.Location = new System.Drawing.Point(424, 12);
            this.kbtnOK.Name = "kbtnOK";
            this.kbtnOK.Size = new System.Drawing.Size(90, 25);
            this.kbtnOK.TabIndex = 1;
            this.kbtnOK.Values.Text = "&OK";
            this.kbtnOK.Enabled = false;
            this.kbtnOK.Click += new System.EventHandler(this.kbtnOK_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Location = new System.Drawing.Point(520, 12);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 0;
            this.kbtnCancel.Values.Text = "C&ancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.ktxtPrompt);
            this.kryptonPanel2.Controls.Add(this.kwlMessage);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(622, 212);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // ktxtPrompt
            // 
            this.ktxtPrompt.Location = new System.Drawing.Point(12, 178);
            this.ktxtPrompt.Name = "ktxtPrompt";
            this.ktxtPrompt.Size = new System.Drawing.Size(598, 24);
            this.ktxtPrompt.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ktxtPrompt.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.ktxtPrompt.TabIndex = 3;
            this.ktxtPrompt.TextChanged += new System.EventHandler(ktxtPrompt_TextChanged);
            // 
            // kwlMessage
            // 
            this.kwlMessage.AutoSize = false;
            this.kwlMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kwlMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlMessage.Location = new System.Drawing.Point(12, 9);
            this.kwlMessage.Name = "kwlMessage";
            this.kwlMessage.Size = new System.Drawing.Size(598, 156);
            this.kwlMessage.StateCommon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kwlMessage.Text = "{0}";
            this.kwlMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 209);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 3);
            this.panel1.TabIndex = 2;
            // 
            // KryptonInputBox
            // 
            this.ClientSize = new System.Drawing.Size(622, 261);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonInputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new FormClosingEventHandler(KryptonInputBox_FormClosing);
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
        public KryptonInputBoxNew(string title, string message, string prompt = "", string okText = "O&k", string cancelText = "&Cancel", bool passwordEnabled = false, FormStartPosition startPosition = FormStartPosition.Manual)
        {
            InitializeComponent();

            SetMessage(message);

            SetPrompt(prompt);

            SetOkText(okText);

            SetCancelText(cancelText);

            SetPasswordEnabled(passwordEnabled);

            SetTitle(title);

            SetStartPosition(startPosition);
        }
        #endregion

        private void kbtnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void kbtnOK_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        private void ktxtPrompt_TextChanged(object sender, EventArgs e)
        {
            EnableOkButton(string.IsNullOrEmpty(ktxtPrompt.Text));
        }

        private void KryptonInputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != null)
            {

            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        #region Methods
        private void SetPasswordEnabled(bool passwordEnabled) => ktxtPrompt.UseSystemPasswordChar = passwordEnabled;

        private void SetOkText(string okText) => kbtnOK.Text = okText;

        private void SetCancelText(string cancelText) => kbtnCancel.Text = cancelText;

        private void SetPrompt(string prompt) => ktxtPrompt.Hint = prompt;

        private void SetMessage(string message) => kwlMessage.Text = message;

        private void SetStartPosition(FormStartPosition startPosition) => StartPosition = startPosition;

        private void SetTitle(string title) => Text = title;

        public string RetrieveUserResponse() => ktxtPrompt.Text;

        private void EnableOkButton(bool value)
        {
            if (value)
            {
                kbtnOK.Enabled = true;
            }
            else
            {
                kbtnOK.Enabled = false;
            }
        }
        #endregion
    }
}