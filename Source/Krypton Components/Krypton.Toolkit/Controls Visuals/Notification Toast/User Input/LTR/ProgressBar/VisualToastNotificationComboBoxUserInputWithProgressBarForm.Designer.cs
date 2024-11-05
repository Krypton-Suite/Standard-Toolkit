﻿namespace Krypton.Toolkit
{
    partial class VisualToastNotificationComboBoxUserInputWithProgressBarForm
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
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.kwlNotificationTitle = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlNotificationMessage = new Krypton.Toolkit.KryptonWrapLabel();
            this.kcmbUserInput = new Krypton.Toolkit.KryptonComboBox();
            this.kpbCountDown = new Krypton.Toolkit.KryptonProgressBar();
            this.pbxNotificationIcon = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnDismiss = new Krypton.Toolkit.KryptonButton();
            this.klblToastLocation = new Krypton.Toolkit.KryptonLabel();
            this.kchkDoNotShowAgain = new Krypton.Toolkit.KryptonCheckBox();
            this.itbDismiss = new Krypton.Toolkit.InternalToastButton();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbUserInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNotificationIcon)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tableLayoutPanel2);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 369);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(630, 50);
            this.kpnlButtons.TabIndex = 4;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(630, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.tlpMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(630, 369);
            this.kpnlMain.TabIndex = 5;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpContent, 1, 0);
            this.tlpMain.Controls.Add(this.kpbCountDown, 0, 1);
            this.tlpMain.Controls.Add(this.pbxNotificationIcon, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(630, 369);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 1;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.kwlNotificationTitle, 0, 0);
            this.tlpContent.Controls.Add(this.kwlNotificationMessage, 0, 1);
            this.tlpContent.Controls.Add(this.kcmbUserInput, 0, 2);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(141, 3);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 3;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.Size = new System.Drawing.Size(486, 331);
            this.tlpContent.TabIndex = 6;
            // 
            // kwlNotificationTitle
            // 
            this.kwlNotificationTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlNotificationTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.kwlNotificationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlNotificationTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlNotificationTitle.Location = new System.Drawing.Point(3, 0);
            this.kwlNotificationTitle.Name = "kwlNotificationTitle";
            this.kwlNotificationTitle.Size = new System.Drawing.Size(480, 25);
            this.kwlNotificationTitle.Text = "kryptonWrapLabel1";
            this.kwlNotificationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kwlNotificationMessage
            // 
            this.kwlNotificationMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlNotificationMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlNotificationMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlNotificationMessage.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlNotificationMessage.Location = new System.Drawing.Point(3, 25);
            this.kwlNotificationMessage.Name = "kwlNotificationMessage";
            this.kwlNotificationMessage.Size = new System.Drawing.Size(480, 279);
            this.kwlNotificationMessage.Text = "kryptonWrapLabel2";
            this.kwlNotificationMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kcmbUserInput
            // 
            this.kcmbUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbUserInput.DropDownWidth = 480;
            this.kcmbUserInput.IntegralHeight = false;
            this.kcmbUserInput.Location = new System.Drawing.Point(3, 307);
            this.kcmbUserInput.Name = "kcmbUserInput";
            this.kcmbUserInput.Size = new System.Drawing.Size(480, 21);
            this.kcmbUserInput.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbUserInput.TabIndex = 3;
            this.kcmbUserInput.Text = "kryptonComboBox1";
            // 
            // kpbCountDown
            // 
            this.tlpMain.SetColumnSpan(this.kpbCountDown, 2);
            this.kpbCountDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpbCountDown.Location = new System.Drawing.Point(3, 340);
            this.kpbCountDown.Name = "kpbCountDown";
            this.kpbCountDown.Size = new System.Drawing.Size(624, 26);
            this.kpbCountDown.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kpbCountDown.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbCountDown.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbCountDown.TabIndex = 5;
            this.kpbCountDown.Text = "kryptonProgressBar1";
            this.kpbCountDown.Values.Text = "kryptonProgressBar1";
            // 
            // pbxNotificationIcon
            // 
            this.pbxNotificationIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxNotificationIcon.Location = new System.Drawing.Point(5, 5);
            this.pbxNotificationIcon.Margin = new System.Windows.Forms.Padding(5);
            this.pbxNotificationIcon.Name = "pbxNotificationIcon";
            this.pbxNotificationIcon.Size = new System.Drawing.Size(128, 327);
            this.pbxNotificationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxNotificationIcon.TabIndex = 0;
            this.pbxNotificationIcon.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.kbtnDismiss, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.klblToastLocation, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.kchkDoNotShowAgain, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.itbDismiss, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(630, 49);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnDismiss.Location = new System.Drawing.Point(554, 13);
            this.kbtnDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnDismiss.Name = "kbtnDismiss";
            this.kbtnDismiss.Size = new System.Drawing.Size(23, 22);
            this.kbtnDismiss.TabIndex = 2;
            this.kbtnDismiss.Values.Text = "{0}";
            this.kbtnDismiss.Visible = false;
            // 
            // klblToastLocation
            // 
            this.klblToastLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblToastLocation.Location = new System.Drawing.Point(75, 23);
            this.klblToastLocation.Margin = new System.Windows.Forms.Padding(10);
            this.klblToastLocation.Name = "klblToastLocation";
            this.klblToastLocation.Size = new System.Drawing.Size(6, 2);
            this.klblToastLocation.TabIndex = 3;
            this.klblToastLocation.Values.Text = "";
            // 
            // kchkDoNotShowAgain
            // 
            this.kchkDoNotShowAgain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkDoNotShowAgain.Location = new System.Drawing.Point(10, 10);
            this.kchkDoNotShowAgain.Margin = new System.Windows.Forms.Padding(10);
            this.kchkDoNotShowAgain.Name = "kchkDoNotShowAgain";
            this.kchkDoNotShowAgain.Size = new System.Drawing.Size(45, 29);
            this.kchkDoNotShowAgain.TabIndex = 4;
            this.kchkDoNotShowAgain.Values.Text = "CB1";
            this.kchkDoNotShowAgain.Visible = false;
            // 
            // itbDismiss
            // 
            this.itbDismiss.AutoSize = true;
            this.itbDismiss.IsActionButton = false;
            this.itbDismiss.IsDismissButton = true;
            this.itbDismiss.Location = new System.Drawing.Point(597, 13);
            this.itbDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.itbDismiss.Name = "itbDismiss";
            this.itbDismiss.NotificationResult = Krypton.Toolkit.KryptonToastNotificationResult.None;
            this.itbDismiss.Owner = null;
            this.itbDismiss.ProcessPath = "";
            this.itbDismiss.Size = new System.Drawing.Size(23, 22);
            this.itbDismiss.TabIndex = 5;
            this.itbDismiss.Values.Text = "{0}";
            // 
            // VisualToastNotificationComboBoxUserInputWithProgressBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 419);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationComboBoxUserInputWithProgressBarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.VisualToastNotificationComboBoxUserInputWithProgressBarForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbUserInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNotificationIcon)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kpnlMain;
        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpContent;
        private KryptonWrapLabel kwlNotificationTitle;
        private KryptonWrapLabel kwlNotificationMessage;
        private KryptonProgressBar kpbCountDown;
        private PictureBox pbxNotificationIcon;
        private KryptonComboBox kcmbUserInput;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonButton kbtnDismiss;
        private KryptonLabel klblToastLocation;
        private KryptonCheckBox kchkDoNotShowAgain;
        private InternalToastButton itbDismiss;
    }
}