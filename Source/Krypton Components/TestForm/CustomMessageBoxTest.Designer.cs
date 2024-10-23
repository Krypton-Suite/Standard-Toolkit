#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class CustomMessageBoxTest
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kchkShowCtrlCopyText = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnTestText = new Krypton.Toolkit.KryptonButton();
            this.kcbShowHelp = new Krypton.Toolkit.KryptonCheckBox();
            this.kcbMessageBoxOptionsRtlReading = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkMessageBoxOptionsRightAlign = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.krbButtonsYesNo = new Krypton.Toolkit.KryptonRadioButton();
            this.krbButtonsCancelTryContinue = new Krypton.Toolkit.KryptonRadioButton();
            this.krbButtonsYesNoCancel = new Krypton.Toolkit.KryptonRadioButton();
            this.krbButtonsAbortRetryIgnore = new Krypton.Toolkit.KryptonRadioButton();
            this.krbButtonsRetryCancel = new Krypton.Toolkit.KryptonRadioButton();
            this.krbButtonsOkCancel = new Krypton.Toolkit.KryptonRadioButton();
            this.krbButtonsOk = new Krypton.Toolkit.KryptonRadioButton();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.krbIconShield = new Krypton.Toolkit.KryptonRadioButton();
            this.krbIconWinLogo = new Krypton.Toolkit.KryptonRadioButton();
            this.krbIconWarning = new Krypton.Toolkit.KryptonRadioButton();
            this.krbIconInformation = new Krypton.Toolkit.KryptonRadioButton();
            this.krbIconQuestion = new Krypton.Toolkit.KryptonRadioButton();
            this.krbIconError = new Krypton.Toolkit.KryptonRadioButton();
            this.krbIconNone = new Krypton.Toolkit.KryptonRadioButton();
            this.krtbMessageBody = new Krypton.Toolkit.KryptonRichTextBox();
            this.ktxtCaption = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnShow);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 407);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(749, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnShow
            // 
            this.kbtnShow.Location = new System.Drawing.Point(550, 13);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(90, 25);
            this.kbtnShow.TabIndex = 1;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "Show";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kryptonButton1.Location = new System.Drawing.Point(646, 13);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "Cancel";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 406);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(749, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kchkShowCtrlCopyText);
            this.kryptonPanel2.Controls.Add(this.kbtnTestText);
            this.kryptonPanel2.Controls.Add(this.kcbShowHelp);
            this.kryptonPanel2.Controls.Add(this.kcbMessageBoxOptionsRtlReading);
            this.kryptonPanel2.Controls.Add(this.kchkMessageBoxOptionsRightAlign);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel2.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel2.Controls.Add(this.krtbMessageBody);
            this.kryptonPanel2.Controls.Add(this.ktxtCaption);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(749, 406);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // kchkShowCtrlCopyText
            // 
            this.kchkShowCtrlCopyText.Location = new System.Drawing.Point(347, 300);
            this.kchkShowCtrlCopyText.Name = "kchkShowCtrlCopyText";
            this.kchkShowCtrlCopyText.Size = new System.Drawing.Size(146, 22);
            this.kchkShowCtrlCopyText.TabIndex = 11;
            this.kchkShowCtrlCopyText.Values.Text = "Show \"Ctrl+Copy\" text";
            // 
            // kbtnTestText
            // 
            this.kbtnTestText.Location = new System.Drawing.Point(14, 71);
            this.kbtnTestText.Name = "kbtnTestText";
            this.kbtnTestText.Size = new System.Drawing.Size(64, 50);
            this.kbtnTestText.TabIndex = 10;
            this.kbtnTestText.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTestText.Values.Text = "Fill\r\nText";
            this.kbtnTestText.Click += new System.EventHandler(this.kbtnTestText_Click);
            // 
            // kcbShowHelp
            // 
            this.kcbShowHelp.Location = new System.Drawing.Point(347, 272);
            this.kcbShowHelp.Name = "kcbShowHelp";
            this.kcbShowHelp.Size = new System.Drawing.Size(82, 22);
            this.kcbShowHelp.TabIndex = 9;
            this.kcbShowHelp.Values.Text = "Show Help";
            // 
            // kcbMessageBoxOptionsRtlReading
            // 
            this.kcbMessageBoxOptionsRtlReading.Location = new System.Drawing.Point(347, 243);
            this.kcbMessageBoxOptionsRtlReading.Name = "kcbMessageBoxOptionsRtlReading";
            this.kcbMessageBoxOptionsRtlReading.Size = new System.Drawing.Size(197, 22);
            this.kcbMessageBoxOptionsRtlReading.TabIndex = 8;
            this.kcbMessageBoxOptionsRtlReading.Values.Text = "MessageBoxOptions.RtlReading";
            this.kcbMessageBoxOptionsRtlReading.CheckedChanged += new System.EventHandler(this.kcbMessageBoxOptionsRtlReading_CheckedChanged);
            // 
            // kchkMessageBoxOptionsRightAlign
            // 
            this.kchkMessageBoxOptionsRightAlign.Location = new System.Drawing.Point(347, 214);
            this.kchkMessageBoxOptionsRightAlign.Name = "kchkMessageBoxOptionsRightAlign";
            this.kchkMessageBoxOptionsRightAlign.Size = new System.Drawing.Size(195, 22);
            this.kchkMessageBoxOptionsRightAlign.TabIndex = 7;
            this.kchkMessageBoxOptionsRightAlign.Values.Text = "MessageBoxOptions.RightAlign";
            this.kchkMessageBoxOptionsRightAlign.CheckedChanged += new System.EventHandler(this.kchkMessageBoxOptionsRightAlign_CheckedChanged);
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(347, 13);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.krbButtonsYesNo);
            this.kryptonGroupBox2.Panel.Controls.Add(this.krbButtonsCancelTryContinue);
            this.kryptonGroupBox2.Panel.Controls.Add(this.krbButtonsYesNoCancel);
            this.kryptonGroupBox2.Panel.Controls.Add(this.krbButtonsAbortRetryIgnore);
            this.kryptonGroupBox2.Panel.Controls.Add(this.krbButtonsRetryCancel);
            this.kryptonGroupBox2.Panel.Controls.Add(this.krbButtonsOkCancel);
            this.kryptonGroupBox2.Panel.Controls.Add(this.krbButtonsOk);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(390, 194);
            this.kryptonGroupBox2.TabIndex = 6;
            this.kryptonGroupBox2.Values.Heading = "Buttons";
            // 
            // krbButtonsYesNo
            // 
            this.krbButtonsYesNo.Location = new System.Drawing.Point(199, 64);
            this.krbButtonsYesNo.Name = "krbButtonsYesNo";
            this.krbButtonsYesNo.Size = new System.Drawing.Size(62, 22);
            this.krbButtonsYesNo.TabIndex = 6;
            this.krbButtonsYesNo.Values.Text = "Yes No";
            this.krbButtonsYesNo.CheckedChanged += new System.EventHandler(this.buttons_CheckedChanged);
            // 
            // krbButtonsCancelTryContinue
            // 
            this.krbButtonsCancelTryContinue.Location = new System.Drawing.Point(15, 92);
            this.krbButtonsCancelTryContinue.Name = "krbButtonsCancelTryContinue";
            this.krbButtonsCancelTryContinue.Size = new System.Drawing.Size(133, 22);
            this.krbButtonsCancelTryContinue.TabIndex = 5;
            this.krbButtonsCancelTryContinue.Values.Text = "Cancel Try Continue";
            this.krbButtonsCancelTryContinue.CheckedChanged += new System.EventHandler(this.buttons_CheckedChanged);
            // 
            // krbButtonsYesNoCancel
            // 
            this.krbButtonsYesNoCancel.Location = new System.Drawing.Point(199, 36);
            this.krbButtonsYesNoCancel.Name = "krbButtonsYesNoCancel";
            this.krbButtonsYesNoCancel.Size = new System.Drawing.Size(101, 22);
            this.krbButtonsYesNoCancel.TabIndex = 4;
            this.krbButtonsYesNoCancel.Values.Text = "Yes No Cancel";
            this.krbButtonsYesNoCancel.CheckedChanged += new System.EventHandler(this.buttons_CheckedChanged);
            // 
            // krbButtonsAbortRetryIgnore
            // 
            this.krbButtonsAbortRetryIgnore.Location = new System.Drawing.Point(199, 8);
            this.krbButtonsAbortRetryIgnore.Name = "krbButtonsAbortRetryIgnore";
            this.krbButtonsAbortRetryIgnore.Size = new System.Drawing.Size(125, 22);
            this.krbButtonsAbortRetryIgnore.TabIndex = 3;
            this.krbButtonsAbortRetryIgnore.Values.Text = "Abort Retry Ignore";
            this.krbButtonsAbortRetryIgnore.CheckedChanged += new System.EventHandler(this.buttons_CheckedChanged);
            // 
            // krbButtonsRetryCancel
            // 
            this.krbButtonsRetryCancel.Location = new System.Drawing.Point(15, 64);
            this.krbButtonsRetryCancel.Name = "krbButtonsRetryCancel";
            this.krbButtonsRetryCancel.Size = new System.Drawing.Size(91, 22);
            this.krbButtonsRetryCancel.TabIndex = 2;
            this.krbButtonsRetryCancel.Values.Text = "Retry Cancel";
            this.krbButtonsRetryCancel.CheckedChanged += new System.EventHandler(this.buttons_CheckedChanged);
            // 
            // krbButtonsOkCancel
            // 
            this.krbButtonsOkCancel.Location = new System.Drawing.Point(15, 36);
            this.krbButtonsOkCancel.Name = "krbButtonsOkCancel";
            this.krbButtonsOkCancel.Size = new System.Drawing.Size(79, 22);
            this.krbButtonsOkCancel.TabIndex = 1;
            this.krbButtonsOkCancel.Values.Text = "OK Cancel";
            this.krbButtonsOkCancel.CheckedChanged += new System.EventHandler(this.buttons_CheckedChanged);
            // 
            // krbButtonsOk
            // 
            this.krbButtonsOk.Checked = true;
            this.krbButtonsOk.Location = new System.Drawing.Point(15, 8);
            this.krbButtonsOk.Name = "krbButtonsOk";
            this.krbButtonsOk.Size = new System.Drawing.Size(40, 22);
            this.krbButtonsOk.TabIndex = 0;
            this.krbButtonsOk.Values.Text = "OK";
            this.krbButtonsOk.CheckedChanged += new System.EventHandler(this.buttons_CheckedChanged);
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonThemeComboBox1.DropDownWidth = 256;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(84, 369);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(256, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 5;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(84, 214);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbIconShield);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbIconWinLogo);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbIconWarning);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbIconInformation);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbIconQuestion);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbIconError);
            this.kryptonGroupBox1.Panel.Controls.Add(this.krbIconNone);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(256, 149);
            this.kryptonGroupBox1.TabIndex = 4;
            this.kryptonGroupBox1.Values.Heading = "Icon";
            // 
            // krbIconShield
            // 
            this.krbIconShield.Location = new System.Drawing.Point(118, 36);
            this.krbIconShield.Name = "krbIconShield";
            this.krbIconShield.Size = new System.Drawing.Size(57, 22);
            this.krbIconShield.TabIndex = 6;
            this.krbIconShield.Values.Text = "Shield";
            this.krbIconShield.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // krbIconWinLogo
            // 
            this.krbIconWinLogo.Location = new System.Drawing.Point(118, 64);
            this.krbIconWinLogo.Name = "krbIconWinLogo";
            this.krbIconWinLogo.Size = new System.Drawing.Size(73, 22);
            this.krbIconWinLogo.TabIndex = 5;
            this.krbIconWinLogo.Values.Text = "WinLogo";
            this.krbIconWinLogo.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // krbIconWarning
            // 
            this.krbIconWarning.Location = new System.Drawing.Point(13, 92);
            this.krbIconWarning.Name = "krbIconWarning";
            this.krbIconWarning.Size = new System.Drawing.Size(70, 22);
            this.krbIconWarning.TabIndex = 4;
            this.krbIconWarning.Values.Text = "Warning";
            this.krbIconWarning.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // krbIconInformation
            // 
            this.krbIconInformation.Location = new System.Drawing.Point(118, 7);
            this.krbIconInformation.Name = "krbIconInformation";
            this.krbIconInformation.Size = new System.Drawing.Size(88, 22);
            this.krbIconInformation.TabIndex = 3;
            this.krbIconInformation.Values.Text = "Information";
            this.krbIconInformation.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // krbIconQuestion
            // 
            this.krbIconQuestion.Location = new System.Drawing.Point(13, 64);
            this.krbIconQuestion.Name = "krbIconQuestion";
            this.krbIconQuestion.Size = new System.Drawing.Size(73, 22);
            this.krbIconQuestion.TabIndex = 2;
            this.krbIconQuestion.Values.Text = "Question";
            this.krbIconQuestion.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // krbIconError
            // 
            this.krbIconError.Location = new System.Drawing.Point(13, 36);
            this.krbIconError.Name = "krbIconError";
            this.krbIconError.Size = new System.Drawing.Size(50, 22);
            this.krbIconError.TabIndex = 1;
            this.krbIconError.Values.Text = "Error";
            this.krbIconError.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // krbIconNone
            // 
            this.krbIconNone.Checked = true;
            this.krbIconNone.Location = new System.Drawing.Point(13, 7);
            this.krbIconNone.Name = "krbIconNone";
            this.krbIconNone.Size = new System.Drawing.Size(53, 22);
            this.krbIconNone.TabIndex = 0;
            this.krbIconNone.Values.Text = "None";
            this.krbIconNone.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // krtbMessageBody
            // 
            this.krtbMessageBody.Location = new System.Drawing.Point(84, 43);
            this.krtbMessageBody.Name = "krtbMessageBody";
            this.krtbMessageBody.Size = new System.Drawing.Size(256, 164);
            this.krtbMessageBody.TabIndex = 3;
            this.krtbMessageBody.Text = "This is a sample messagebox...";
            // 
            // ktxtCaption
            // 
            this.ktxtCaption.Location = new System.Drawing.Point(84, 13);
            this.ktxtCaption.Name = "ktxtCaption";
            this.ktxtCaption.Size = new System.Drawing.Size(256, 23);
            this.ktxtCaption.TabIndex = 2;
            this.ktxtCaption.Text = "Caption";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel2.Location = new System.Drawing.Point(14, 43);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(64, 22);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Message:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(14, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(59, 22);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Caption:";
            // 
            // CustomMessageBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kryptonButton1;
            this.ClientSize = new System.Drawing.Size(749, 457);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CustomMessageBoxTest";
            this.Text = "CustomMessageBoxTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private KryptonLabel kryptonLabel1;
        private KryptonLabel kryptonLabel2;
        private KryptonTextBox ktxtCaption;
        private KryptonRichTextBox krtbMessageBody;
        private KryptonGroupBox kryptonGroupBox1;
        private KryptonThemeComboBox kryptonThemeComboBox1;
        private KryptonGroupBox kryptonGroupBox2;
        private KryptonCheckBox kchkMessageBoxOptionsRightAlign;
        private KryptonCheckBox kcbMessageBoxOptionsRtlReading;
        private KryptonCheckBox kcbShowHelp;
        private KryptonRadioButton krbIconNone;
        private KryptonRadioButton krbIconShield;
        private KryptonRadioButton krbIconWinLogo;
        private KryptonRadioButton krbIconWarning;
        private KryptonRadioButton krbIconInformation;
        private KryptonRadioButton krbIconQuestion;
        private KryptonRadioButton krbIconError;
        private KryptonRadioButton krbButtonsOk;
        private KryptonRadioButton krbButtonsOkCancel;
        private KryptonRadioButton krbButtonsRetryCancel;
        private KryptonRadioButton krbButtonsAbortRetryIgnore;
        private KryptonRadioButton krbButtonsYesNoCancel;
        private KryptonRadioButton krbButtonsCancelTryContinue;
        private KryptonRadioButton krbButtonsYesNo;
        private KryptonButton kbtnTestText;
        private KryptonButton kryptonButton1;
        private KryptonButton kbtnShow;
        private KryptonCheckBox kchkShowCtrlCopyText;
    }
}