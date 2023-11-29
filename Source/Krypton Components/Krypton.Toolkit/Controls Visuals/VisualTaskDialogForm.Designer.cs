namespace Krypton.Toolkit
{
    partial class VisualTaskDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._panelMain = new Krypton.Toolkit.KryptonPanel();
            this._panelMainSpacer = new Krypton.Toolkit.KryptonPanel();
            this._panelMainCommands = new Krypton.Toolkit.KryptonPanel();
            this._panelMainRadio = new Krypton.Toolkit.KryptonPanel();
            this._panelMainText = new Krypton.Toolkit.KryptonPanel();
            this._messageContent = new Krypton.Toolkit.KryptonWrapLabel();
            this._messageContentMultiline = new Krypton.Toolkit.KryptonTextBox();
            this._messageText = new Krypton.Toolkit.KryptonWrapLabel();
            this._panelIcon = new Krypton.Toolkit.KryptonPanel();
            this._messageIcon = new System.Windows.Forms.PictureBox();
            this._panelButtons = new Krypton.Toolkit.KryptonPanel();
            this._checkBox = new Krypton.Toolkit.KryptonCheckBox();
            this._panelButtonsBorderTop = new Krypton.Toolkit.KryptonBorderEdge();
            this._buttonOK = new Krypton.Toolkit.TaskDialogMessageButton();
            this._buttonYes = new Krypton.Toolkit.TaskDialogMessageButton();
            this._buttonNo = new Krypton.Toolkit.TaskDialogMessageButton();
            this._buttonRetry = new Krypton.Toolkit.TaskDialogMessageButton();
            this._buttonCancel = new Krypton.Toolkit.TaskDialogMessageButton();
            this._buttonClose = new Krypton.Toolkit.TaskDialogMessageButton();
            this._panelFooter = new Krypton.Toolkit.KryptonPanel();
            this._linkLabelFooter = new Krypton.Toolkit.KryptonLinkLabel();
            this._iconFooter = new System.Windows.Forms.PictureBox();
            this._footerLabel = new Krypton.Toolkit.KryptonWrapLabel();
            this._panelFooterBorderTop = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this._panelMain)).BeginInit();
            this._panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainSpacer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainRadio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainText)).BeginInit();
            this._panelMainText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelIcon)).BeginInit();
            this._panelIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._messageIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).BeginInit();
            this._panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelFooter)).BeginInit();
            this._panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._iconFooter)).BeginInit();
            this.SuspendLayout();
            // 
            // _panelMain
            // 
            this._panelMain.AutoSize = true;
            this._panelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panelMain.Controls.Add(this._panelMainSpacer);
            this._panelMain.Controls.Add(this._panelMainCommands);
            this._panelMain.Controls.Add(this._panelMainRadio);
            this._panelMain.Controls.Add(this._panelMainText);
            this._panelMain.Controls.Add(this._panelIcon);
            this._panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelMain.Location = new System.Drawing.Point(0, 0);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(782, 72);
            this._panelMain.TabIndex = 1;
            // 
            // _panelMainSpacer
            // 
            this._panelMainSpacer.Location = new System.Drawing.Point(42, 59);
            this._panelMainSpacer.Name = "_panelMainSpacer";
            this._panelMainSpacer.Size = new System.Drawing.Size(10, 10);
            this._panelMainSpacer.TabIndex = 3;
            // 
            // _panelMainCommands
            // 
            this._panelMainCommands.AutoSize = true;
            this._panelMainCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panelMainCommands.Location = new System.Drawing.Point(208, 10);
            this._panelMainCommands.Name = "_panelMainCommands";
            this._panelMainCommands.Size = new System.Drawing.Size(0, 0);
            this._panelMainCommands.TabIndex = 2;
            // 
            // _panelMainRadio
            // 
            this._panelMainRadio.AutoSize = true;
            this._panelMainRadio.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panelMainRadio.Location = new System.Drawing.Point(208, 32);
            this._panelMainRadio.Name = "_panelMainRadio";
            this._panelMainRadio.Size = new System.Drawing.Size(0, 0);
            this._panelMainRadio.TabIndex = 1;
            // 
            // _panelMainText
            // 
            this._panelMainText.AutoSize = true;
            this._panelMainText.Controls.Add(this._messageContent);
            this._panelMainText.Controls.Add(this._messageContentMultiline);
            this._panelMainText.Controls.Add(this._messageText);
            this._panelMainText.Location = new System.Drawing.Point(42, 0);
            this._panelMainText.Margin = new System.Windows.Forms.Padding(0);
            this._panelMainText.Name = "_panelMainText";
            this._panelMainText.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this._panelMainText.Size = new System.Drawing.Size(407, 60);
            this._panelMainText.TabIndex = 0;
            // 
            // _messageContent
            // 
            this._messageContent.AutoSize = false;
            this._messageContent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._messageContent.ForeColor = System.Drawing.Color.White;
            this._messageContent.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this._messageContent.Location = new System.Drawing.Point(6, 34);
            this._messageContent.Margin = new System.Windows.Forms.Padding(0);
            this._messageContent.Name = "_messageContent";
            this._messageContent.Size = new System.Drawing.Size(78, 15);
            this._messageContent.Text = "Content";
            // 
            // _messageContentMultiline
            // 
            this._messageContentMultiline.Location = new System.Drawing.Point(48, 45);
            this._messageContentMultiline.Multiline = true;
            this._messageContentMultiline.Name = "_messageContentMultiline";
            this._messageContentMultiline.ReadOnly = true;
            this._messageContentMultiline.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._messageContentMultiline.Size = new System.Drawing.Size(351, 10);
            this._messageContentMultiline.TabIndex = 4;
            // 
            // _messageText
            // 
            this._messageText.AutoSize = false;
            this._messageText.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this._messageText.ForeColor = System.Drawing.Color.White;
            this._messageText.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this._messageText.Location = new System.Drawing.Point(5, 5);
            this._messageText.Margin = new System.Windows.Forms.Padding(0);
            this._messageText.Name = "_messageText";
            this._messageText.Size = new System.Drawing.Size(139, 27);
            this._messageText.Text = "Message Text";
            // 
            // _panelIcon
            // 
            this._panelIcon.AutoSize = true;
            this._panelIcon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panelIcon.Controls.Add(this._messageIcon);
            this._panelIcon.Location = new System.Drawing.Point(0, 0);
            this._panelIcon.Margin = new System.Windows.Forms.Padding(0);
            this._panelIcon.Name = "_panelIcon";
            this._panelIcon.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this._panelIcon.Size = new System.Drawing.Size(42, 52);
            this._panelIcon.TabIndex = 0;
            // 
            // _messageIcon
            // 
            this._messageIcon.BackColor = System.Drawing.Color.Transparent;
            this._messageIcon.Location = new System.Drawing.Point(10, 10);
            this._messageIcon.Margin = new System.Windows.Forms.Padding(0);
            this._messageIcon.Name = "_messageIcon";
            this._messageIcon.Size = new System.Drawing.Size(32, 32);
            this._messageIcon.TabIndex = 0;
            this._messageIcon.TabStop = false;
            // 
            // _panelButtons
            // 
            this._panelButtons.Controls.Add(this._checkBox);
            this._panelButtons.Controls.Add(this._panelButtonsBorderTop);
            this._panelButtons.Controls.Add(this._buttonOK);
            this._panelButtons.Controls.Add(this._buttonYes);
            this._panelButtons.Controls.Add(this._buttonNo);
            this._panelButtons.Controls.Add(this._buttonRetry);
            this._panelButtons.Controls.Add(this._buttonCancel);
            this._panelButtons.Controls.Add(this._buttonClose);
            this._panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelButtons.Location = new System.Drawing.Point(0, 72);
            this._panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this._panelButtons.Name = "_panelButtons";
            this._panelButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this._panelButtons.Size = new System.Drawing.Size(782, 46);
            this._panelButtons.TabIndex = 2;
            // 
            // _checkBox
            // 
            this._checkBox.Location = new System.Drawing.Point(12, 12);
            this._checkBox.Name = "_checkBox";
            this._checkBox.Size = new System.Drawing.Size(75, 20);
            this._checkBox.TabIndex = 0;
            this._checkBox.Values.Text = "checkBox";
            // 
            // _panelButtonsBorderTop
            // 
            this._panelButtonsBorderTop.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this._panelButtonsBorderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelButtonsBorderTop.Location = new System.Drawing.Point(0, 0);
            this._panelButtonsBorderTop.Name = "_panelButtonsBorderTop";
            this._panelButtonsBorderTop.Size = new System.Drawing.Size(782, 1);
            this._panelButtonsBorderTop.Text = "kryptonBorderEdge1";
            // 
            // _buttonOK
            // 
            this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOK.AutoSize = true;
            this._buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOK.IgnoreAltF4 = false;
            this._buttonOK.Location = new System.Drawing.Point(673, 9);
            this._buttonOK.Margin = new System.Windows.Forms.Padding(0);
            this._buttonOK.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(50, 26);
            this._buttonOK.TabIndex = 1;
            this._buttonOK.Values.Text = "OK";
            // 
            // _buttonYes
            // 
            this._buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonYes.AutoSize = true;
            this._buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this._buttonYes.IgnoreAltF4 = false;
            this._buttonYes.Location = new System.Drawing.Point(573, 9);
            this._buttonYes.Margin = new System.Windows.Forms.Padding(0);
            this._buttonYes.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonYes.Name = "_buttonYes";
            this._buttonYes.Size = new System.Drawing.Size(50, 26);
            this._buttonYes.TabIndex = 2;
            this._buttonYes.Values.Text = "Yes";
            // 
            // _buttonNo
            // 
            this._buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonNo.AutoSize = true;
            this._buttonNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this._buttonNo.IgnoreAltF4 = false;
            this._buttonNo.Location = new System.Drawing.Point(523, 9);
            this._buttonNo.Margin = new System.Windows.Forms.Padding(0);
            this._buttonNo.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonNo.Name = "_buttonNo";
            this._buttonNo.Size = new System.Drawing.Size(50, 26);
            this._buttonNo.TabIndex = 3;
            this._buttonNo.Values.Text = "No";
            // 
            // _buttonRetry
            // 
            this._buttonRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonRetry.AutoSize = true;
            this._buttonRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this._buttonRetry.IgnoreAltF4 = false;
            this._buttonRetry.Location = new System.Drawing.Point(623, 9);
            this._buttonRetry.Margin = new System.Windows.Forms.Padding(0);
            this._buttonRetry.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonRetry.Name = "_buttonRetry";
            this._buttonRetry.Size = new System.Drawing.Size(50, 26);
            this._buttonRetry.TabIndex = 5;
            this._buttonRetry.Values.Text = "Retry";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.AutoSize = true;
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.IgnoreAltF4 = false;
            this._buttonCancel.Location = new System.Drawing.Point(466, 9);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(0);
            this._buttonCancel.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(57, 26);
            this._buttonCancel.TabIndex = 4;
            this._buttonCancel.Values.Text = "Cancel";
            // 
            // _buttonClose
            // 
            this._buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonClose.AutoSize = true;
            this._buttonClose.IgnoreAltF4 = false;
            this._buttonClose.Location = new System.Drawing.Point(723, 9);
            this._buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this._buttonClose.MinimumSize = new System.Drawing.Size(50, 26);
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(50, 26);
            this._buttonClose.TabIndex = 6;
            this._buttonClose.Values.Text = "Close";
            // 
            // _panelFooter
            // 
            this._panelFooter.Controls.Add(this._linkLabelFooter);
            this._panelFooter.Controls.Add(this._iconFooter);
            this._panelFooter.Controls.Add(this._footerLabel);
            this._panelFooter.Controls.Add(this._panelFooterBorderTop);
            this._panelFooter.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelFooter.Location = new System.Drawing.Point(0, 118);
            this._panelFooter.Name = "_panelFooter";
            this._panelFooter.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this._panelFooter.Size = new System.Drawing.Size(782, 49);
            this._panelFooter.TabIndex = 3;
            // 
            // _linkLabelFooter
            // 
            this._linkLabelFooter.Location = new System.Drawing.Point(127, 11);
            this._linkLabelFooter.Name = "_linkLabelFooter";
            this._linkLabelFooter.Size = new System.Drawing.Size(110, 20);
            this._linkLabelFooter.TabIndex = 0;
            this._linkLabelFooter.Values.Text = "kryptonLinkLabel1";
            // 
            // _iconFooter
            // 
            this._iconFooter.BackColor = System.Drawing.Color.Transparent;
            this._iconFooter.Location = new System.Drawing.Point(10, 10);
            this._iconFooter.Margin = new System.Windows.Forms.Padding(0);
            this._iconFooter.Name = "_iconFooter";
            this._iconFooter.Size = new System.Drawing.Size(16, 16);
            this._iconFooter.TabIndex = 4;
            this._iconFooter.TabStop = false;
            // 
            // _footerLabel
            // 
            this._footerLabel.AutoSize = false;
            this._footerLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._footerLabel.ForeColor = System.Drawing.Color.White;
            this._footerLabel.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this._footerLabel.Location = new System.Drawing.Point(36, 11);
            this._footerLabel.Margin = new System.Windows.Forms.Padding(0);
            this._footerLabel.Name = "_footerLabel";
            this._footerLabel.Size = new System.Drawing.Size(78, 15);
            this._footerLabel.Text = "Content";
            // 
            // _panelFooterBorderTop
            // 
            this._panelFooterBorderTop.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this._panelFooterBorderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelFooterBorderTop.Location = new System.Drawing.Point(0, 0);
            this._panelFooterBorderTop.Name = "_panelFooterBorderTop";
            this._panelFooterBorderTop.Size = new System.Drawing.Size(782, 1);
            this._panelFooterBorderTop.Text = "kryptonBorderEdge1";
            // 
            // VisualTaskDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(799, 164);
            this.Controls.Add(this._panelFooter);
            this.Controls.Add(this._panelButtons);
            this.Controls.Add(this._panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.FormMain;
            this.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.FormMain;
            this.HeaderStyle = Krypton.Toolkit.HeaderStyle.Form;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualTaskDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TitleStyle = Krypton.Toolkit.KryptonFormTitleStyle.Inherit;
            ((System.ComponentModel.ISupportInitialize)(this._panelMain)).EndInit();
            this._panelMain.ResumeLayout(false);
            this._panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainSpacer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainRadio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelMainText)).EndInit();
            this._panelMainText.ResumeLayout(false);
            this._panelMainText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelIcon)).EndInit();
            this._panelIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._messageIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).EndInit();
            this._panelButtons.ResumeLayout(false);
            this._panelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelFooter)).EndInit();
            this._panelFooter.ResumeLayout(false);
            this._panelFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._iconFooter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonPanel _panelMain;
        private KryptonPanel _panelMainSpacer;
        private KryptonPanel _panelMainCommands;
        private KryptonPanel _panelMainRadio;
        private KryptonPanel _panelMainText;
        private KryptonWrapLabel _messageContent;
        private KryptonTextBox _messageContentMultiline;
        private KryptonWrapLabel _messageText;
        private KryptonPanel _panelIcon;
        private PictureBox _messageIcon;
        private KryptonPanel _panelButtons;
        private KryptonCheckBox _checkBox;
        private KryptonBorderEdge _panelButtonsBorderTop;
        private TaskDialogMessageButton _buttonOK;
        private TaskDialogMessageButton _buttonYes;
        private TaskDialogMessageButton _buttonNo;
        private TaskDialogMessageButton _buttonRetry;
        private TaskDialogMessageButton _buttonCancel;
        private TaskDialogMessageButton _buttonClose;
        private KryptonPanel _panelFooter;
        private KryptonLinkLabel _linkLabelFooter;
        private PictureBox _iconFooter;
        private KryptonWrapLabel _footerLabel;
        private KryptonBorderEdge _panelFooterBorderTop;
    }
}