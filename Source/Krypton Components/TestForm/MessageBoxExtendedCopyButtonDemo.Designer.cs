#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class MessageBoxExtendedCopyButtonDemo
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
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.krtbClipboardPreview = new Krypton.Toolkit.KryptonRichTextBox();
            this.kbtnRefreshClipboard = new Krypton.Toolkit.KryptonButton();
            this.klblPreview = new Krypton.Toolkit.KryptonLabel();
            this.kbtnPresetStruct = new Krypton.Toolkit.KryptonButton();
            this.kbtnPresetHyperlink = new Krypton.Toolkit.KryptonButton();
            this.kbtnPresetRichText = new Krypton.Toolkit.KryptonButton();
            this.kbtnPresetError = new Krypton.Toolkit.KryptonButton();
            this.klblPresets = new Krypton.Toolkit.KryptonLabel();
            this.klblResult = new Krypton.Toolkit.KryptonLabel();
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kchkRightToLeft = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowHelpButton = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowCtrlCopy = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowCloseButton = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowCopyButton = new Krypton.Toolkit.KryptonCheckBox();
            this.kcmbContainerType = new Krypton.Toolkit.KryptonComboBox();
            this.klblContainerType = new Krypton.Toolkit.KryptonLabel();
            this.kcmbDefaultButton = new Krypton.Toolkit.KryptonComboBox();
            this.klblDefaultButton = new Krypton.Toolkit.KryptonLabel();
            this.kcmbIcon = new Krypton.Toolkit.KryptonComboBox();
            this.klblIcon = new Krypton.Toolkit.KryptonLabel();
            this.kcmbButtons = new Krypton.Toolkit.KryptonComboBox();
            this.klblButtons = new Krypton.Toolkit.KryptonLabel();
            this.ktxtMessage = new Krypton.Toolkit.KryptonTextBox();
            this.klblMessage = new Krypton.Toolkit.KryptonLabel();
            this.ktxtCaption = new Krypton.Toolkit.KryptonTextBox();
            this.klblCaption = new Krypton.Toolkit.KryptonLabel();
            this.klblTitle = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbContainerType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDefaultButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbButtons)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.krtbClipboardPreview);
            this.kpnlMain.Controls.Add(this.kbtnRefreshClipboard);
            this.kpnlMain.Controls.Add(this.klblPreview);
            this.kpnlMain.Controls.Add(this.kbtnPresetStruct);
            this.kpnlMain.Controls.Add(this.kbtnPresetHyperlink);
            this.kpnlMain.Controls.Add(this.kbtnPresetRichText);
            this.kpnlMain.Controls.Add(this.kbtnPresetError);
            this.kpnlMain.Controls.Add(this.klblPresets);
            this.kpnlMain.Controls.Add(this.klblResult);
            this.kpnlMain.Controls.Add(this.kbtnShow);
            this.kpnlMain.Controls.Add(this.kchkRightToLeft);
            this.kpnlMain.Controls.Add(this.kchkShowHelpButton);
            this.kpnlMain.Controls.Add(this.kchkShowCtrlCopy);
            this.kpnlMain.Controls.Add(this.kchkShowCloseButton);
            this.kpnlMain.Controls.Add(this.kchkShowCopyButton);
            this.kpnlMain.Controls.Add(this.kcmbContainerType);
            this.kpnlMain.Controls.Add(this.klblContainerType);
            this.kpnlMain.Controls.Add(this.kcmbDefaultButton);
            this.kpnlMain.Controls.Add(this.klblDefaultButton);
            this.kpnlMain.Controls.Add(this.kcmbIcon);
            this.kpnlMain.Controls.Add(this.klblIcon);
            this.kpnlMain.Controls.Add(this.kcmbButtons);
            this.kpnlMain.Controls.Add(this.klblButtons);
            this.kpnlMain.Controls.Add(this.ktxtMessage);
            this.kpnlMain.Controls.Add(this.klblMessage);
            this.kpnlMain.Controls.Add(this.ktxtCaption);
            this.kpnlMain.Controls.Add(this.klblCaption);
            this.kpnlMain.Controls.Add(this.klblTitle);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlMain.Size = new System.Drawing.Size(664, 604);
            this.kpnlMain.TabIndex = 0;
            // 
            // krtbClipboardPreview
            // 
            this.krtbClipboardPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbClipboardPreview.Location = new System.Drawing.Point(12, 442);
            this.krtbClipboardPreview.Name = "krtbClipboardPreview";
            this.krtbClipboardPreview.ReadOnly = true;
            this.krtbClipboardPreview.Size = new System.Drawing.Size(640, 150);
            this.krtbClipboardPreview.TabIndex = 27;
            this.krtbClipboardPreview.Text = "";
            // 
            // kbtnRefreshClipboard
            // 
            this.kbtnRefreshClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnRefreshClipboard.Location = new System.Drawing.Point(452, 410);
            this.kbtnRefreshClipboard.Name = "kbtnRefreshClipboard";
            this.kbtnRefreshClipboard.Size = new System.Drawing.Size(200, 26);
            this.kbtnRefreshClipboard.TabIndex = 26;
            this.kbtnRefreshClipboard.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRefreshClipboard.Values.Text = "Refresh preview";
            // 
            // klblPreview
            // 
            this.klblPreview.Location = new System.Drawing.Point(12, 413);
            this.klblPreview.Name = "klblPreview";
            this.klblPreview.Size = new System.Drawing.Size(325, 20);
            this.klblPreview.TabIndex = 25;
            this.klblPreview.Values.Text = "Clipboard contents (after clicking Copy or pressing Ctrl+C):";
            // 
            // kbtnPresetStruct
            // 
            this.kbtnPresetStruct.Location = new System.Drawing.Point(498, 375);
            this.kbtnPresetStruct.Name = "kbtnPresetStruct";
            this.kbtnPresetStruct.Size = new System.Drawing.Size(154, 28);
            this.kbtnPresetStruct.TabIndex = 24;
            this.kbtnPresetStruct.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetStruct.Values.Text = "Struct path + Copy";
            // 
            // kbtnPresetHyperlink
            // 
            this.kbtnPresetHyperlink.Location = new System.Drawing.Point(336, 375);
            this.kbtnPresetHyperlink.Name = "kbtnPresetHyperlink";
            this.kbtnPresetHyperlink.Size = new System.Drawing.Size(156, 28);
            this.kbtnPresetHyperlink.TabIndex = 23;
            this.kbtnPresetHyperlink.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetHyperlink.Values.Text = "HyperLink + Copy";
            // 
            // kbtnPresetRichText
            // 
            this.kbtnPresetRichText.Location = new System.Drawing.Point(174, 375);
            this.kbtnPresetRichText.Name = "kbtnPresetRichText";
            this.kbtnPresetRichText.Size = new System.Drawing.Size(156, 28);
            this.kbtnPresetRichText.TabIndex = 22;
            this.kbtnPresetRichText.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetRichText.Values.Text = "RichTextBox + Copy";
            // 
            // kbtnPresetError
            // 
            this.kbtnPresetError.Location = new System.Drawing.Point(12, 375);
            this.kbtnPresetError.Name = "kbtnPresetError";
            this.kbtnPresetError.Size = new System.Drawing.Size(156, 28);
            this.kbtnPresetError.TabIndex = 21;
            this.kbtnPresetError.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetError.Values.Text = "Error + Copy";
            // 
            // klblPresets
            // 
            this.klblPresets.Location = new System.Drawing.Point(12, 350);
            this.klblPresets.Name = "klblPresets";
            this.klblPresets.Size = new System.Drawing.Size(97, 20);
            this.klblPresets.TabIndex = 20;
            this.klblPresets.Values.Text = "Quick presets:";
            // 
            // klblResult
            // 
            this.klblResult.Location = new System.Drawing.Point(210, 311);
            this.klblResult.Name = "klblResult";
            this.klblResult.Size = new System.Drawing.Size(112, 20);
            this.klblResult.TabIndex = 19;
            this.klblResult.Values.Text = "Last result: (none)";
            // 
            // kbtnShow
            // 
            this.kbtnShow.Location = new System.Drawing.Point(12, 305);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(180, 30);
            this.kbtnShow.TabIndex = 18;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "Show message box";
            // 
            // kchkRightToLeft
            // 
            this.kchkRightToLeft.Location = new System.Drawing.Point(200, 271);
            this.kchkRightToLeft.Name = "kchkRightToLeft";
            this.kchkRightToLeft.Size = new System.Drawing.Size(133, 20);
            this.kchkRightToLeft.TabIndex = 17;
            this.kchkRightToLeft.Values.Text = "Right-to-left (RTL)";
            // 
            // kchkShowHelpButton
            // 
            this.kchkShowHelpButton.Location = new System.Drawing.Point(12, 271);
            this.kchkShowHelpButton.Name = "kchkShowHelpButton";
            this.kchkShowHelpButton.Size = new System.Drawing.Size(122, 20);
            this.kchkShowHelpButton.TabIndex = 16;
            this.kchkShowHelpButton.Values.Text = "Show Help button";
            // 
            // kchkShowCtrlCopy
            // 
            this.kchkShowCtrlCopy.Location = new System.Drawing.Point(390, 245);
            this.kchkShowCtrlCopy.Name = "kchkShowCtrlCopy";
            this.kchkShowCtrlCopy.Size = new System.Drawing.Size(126, 20);
            this.kchkShowCtrlCopy.TabIndex = 15;
            this.kchkShowCtrlCopy.Values.Text = "Show \'Ctrl+C\' hint";
            // 
            // kchkShowCloseButton
            // 
            this.kchkShowCloseButton.Checked = true;
            this.kchkShowCloseButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkShowCloseButton.Location = new System.Drawing.Point(200, 245);
            this.kchkShowCloseButton.Name = "kchkShowCloseButton";
            this.kchkShowCloseButton.Size = new System.Drawing.Size(126, 20);
            this.kchkShowCloseButton.TabIndex = 14;
            this.kchkShowCloseButton.Values.Text = "Show Close button";
            // 
            // kchkShowCopyButton
            // 
            this.kchkShowCopyButton.Checked = true;
            this.kchkShowCopyButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkShowCopyButton.Location = new System.Drawing.Point(12, 245);
            this.kchkShowCopyButton.Name = "kchkShowCopyButton";
            this.kchkShowCopyButton.Size = new System.Drawing.Size(123, 20);
            this.kchkShowCopyButton.TabIndex = 13;
            this.kchkShowCopyButton.Values.Text = "Show Copy button";
            // 
            // kcmbContainerType
            // 
            this.kcmbContainerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbContainerType.DropDownWidth = 180;
            this.kcmbContainerType.IntegralHeight = false;
            this.kcmbContainerType.Location = new System.Drawing.Point(372, 204);
            this.kcmbContainerType.Name = "kcmbContainerType";
            this.kcmbContainerType.Size = new System.Drawing.Size(180, 22);
            this.kcmbContainerType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbContainerType.TabIndex = 12;
            // 
            // klblContainerType
            // 
            this.klblContainerType.Location = new System.Drawing.Point(310, 205);
            this.klblContainerType.Name = "klblContainerType";
            this.klblContainerType.Size = new System.Drawing.Size(62, 20);
            this.klblContainerType.TabIndex = 11;
            this.klblContainerType.Values.Text = "Content:";
            // 
            // kcmbDefaultButton
            // 
            this.kcmbDefaultButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbDefaultButton.DropDownWidth = 180;
            this.kcmbDefaultButton.IntegralHeight = false;
            this.kcmbDefaultButton.Location = new System.Drawing.Point(110, 204);
            this.kcmbDefaultButton.Name = "kcmbDefaultButton";
            this.kcmbDefaultButton.Size = new System.Drawing.Size(180, 22);
            this.kcmbDefaultButton.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbDefaultButton.TabIndex = 10;
            // 
            // klblDefaultButton
            // 
            this.klblDefaultButton.Location = new System.Drawing.Point(12, 205);
            this.klblDefaultButton.Name = "klblDefaultButton";
            this.klblDefaultButton.Size = new System.Drawing.Size(69, 20);
            this.klblDefaultButton.TabIndex = 9;
            this.klblDefaultButton.Values.Text = "Default:";
            // 
            // kcmbIcon
            // 
            this.kcmbIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbIcon.DropDownWidth = 180;
            this.kcmbIcon.IntegralHeight = false;
            this.kcmbIcon.Location = new System.Drawing.Point(360, 172);
            this.kcmbIcon.Name = "kcmbIcon";
            this.kcmbIcon.Size = new System.Drawing.Size(180, 22);
            this.kcmbIcon.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbIcon.TabIndex = 8;
            // 
            // klblIcon
            // 
            this.klblIcon.Location = new System.Drawing.Point(310, 173);
            this.klblIcon.Name = "klblIcon";
            this.klblIcon.Size = new System.Drawing.Size(44, 20);
            this.klblIcon.TabIndex = 7;
            this.klblIcon.Values.Text = "Icon:";
            // 
            // kcmbButtons
            // 
            this.kcmbButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbButtons.DropDownWidth = 180;
            this.kcmbButtons.IntegralHeight = false;
            this.kcmbButtons.Location = new System.Drawing.Point(110, 172);
            this.kcmbButtons.Name = "kcmbButtons";
            this.kcmbButtons.Size = new System.Drawing.Size(180, 22);
            this.kcmbButtons.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbButtons.TabIndex = 6;
            // 
            // klblButtons
            // 
            this.klblButtons.Location = new System.Drawing.Point(12, 173);
            this.klblButtons.Name = "klblButtons";
            this.klblButtons.Size = new System.Drawing.Size(66, 20);
            this.klblButtons.TabIndex = 5;
            this.klblButtons.Values.Text = "Buttons:";
            // 
            // ktxtMessage
            // 
            this.ktxtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtMessage.Location = new System.Drawing.Point(110, 71);
            this.ktxtMessage.Multiline = true;
            this.ktxtMessage.Name = "ktxtMessage";
            this.ktxtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtMessage.Size = new System.Drawing.Size(542, 90);
            this.ktxtMessage.TabIndex = 4;
            // 
            // klblMessage
            // 
            this.klblMessage.Location = new System.Drawing.Point(12, 71);
            this.klblMessage.Name = "klblMessage";
            this.klblMessage.Size = new System.Drawing.Size(71, 20);
            this.klblMessage.TabIndex = 3;
            this.klblMessage.Values.Text = "Message:";
            // 
            // ktxtCaption
            // 
            this.ktxtCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtCaption.Location = new System.Drawing.Point(110, 42);
            this.ktxtCaption.Name = "ktxtCaption";
            this.ktxtCaption.Size = new System.Drawing.Size(542, 23);
            this.ktxtCaption.TabIndex = 2;
            // 
            // klblCaption
            // 
            this.klblCaption.Location = new System.Drawing.Point(12, 43);
            this.klblCaption.Name = "klblCaption";
            this.klblCaption.Size = new System.Drawing.Size(65, 20);
            this.klblCaption.TabIndex = 1;
            this.klblCaption.Values.Text = "Caption:";
            // 
            // klblTitle
            // 
            this.klblTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.klblTitle.Location = new System.Drawing.Point(12, 12);
            this.klblTitle.Name = "klblTitle";
            this.klblTitle.Size = new System.Drawing.Size(500, 25);
            this.klblTitle.TabIndex = 0;
            this.klblTitle.Values.Text = "KryptonMessageBoxExtended - optional Copy button (Issue #3836)";
            // 
            // MessageBoxExtendedCopyButtonDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 604);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(680, 643);
            this.Name = "MessageBoxExtendedCopyButtonDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBox Extended Copy Button Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbContainerType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDefaultButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbButtons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonLabel klblTitle;
        private Krypton.Toolkit.KryptonLabel klblCaption;
        private Krypton.Toolkit.KryptonTextBox ktxtCaption;
        private Krypton.Toolkit.KryptonLabel klblMessage;
        private Krypton.Toolkit.KryptonTextBox ktxtMessage;
        private Krypton.Toolkit.KryptonLabel klblButtons;
        private Krypton.Toolkit.KryptonComboBox kcmbButtons;
        private Krypton.Toolkit.KryptonLabel klblIcon;
        private Krypton.Toolkit.KryptonComboBox kcmbIcon;
        private Krypton.Toolkit.KryptonLabel klblDefaultButton;
        private Krypton.Toolkit.KryptonComboBox kcmbDefaultButton;
        private Krypton.Toolkit.KryptonLabel klblContainerType;
        private Krypton.Toolkit.KryptonComboBox kcmbContainerType;
        private Krypton.Toolkit.KryptonCheckBox kchkShowCopyButton;
        private Krypton.Toolkit.KryptonCheckBox kchkShowCloseButton;
        private Krypton.Toolkit.KryptonCheckBox kchkShowCtrlCopy;
        private Krypton.Toolkit.KryptonCheckBox kchkShowHelpButton;
        private Krypton.Toolkit.KryptonCheckBox kchkRightToLeft;
        private Krypton.Toolkit.KryptonButton kbtnShow;
        private Krypton.Toolkit.KryptonLabel klblResult;
        private Krypton.Toolkit.KryptonLabel klblPresets;
        private Krypton.Toolkit.KryptonButton kbtnPresetError;
        private Krypton.Toolkit.KryptonButton kbtnPresetRichText;
        private Krypton.Toolkit.KryptonButton kbtnPresetHyperlink;
        private Krypton.Toolkit.KryptonButton kbtnPresetStruct;
        private Krypton.Toolkit.KryptonLabel klblPreview;
        private Krypton.Toolkit.KryptonButton kbtnRefreshClipboard;
        private Krypton.Toolkit.KryptonRichTextBox krtbClipboardPreview;
    }
}
