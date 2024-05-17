﻿namespace Krypton.Toolkit
{
    partial class VisualToastNotificationBasicWithProgressBarForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kpbCountDown = new Krypton.Toolkit.KryptonProgressBar();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.kwlblNotificationTitle = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlblNotificationContent = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnDismiss = new Krypton.Toolkit.KryptonButton();
            this.klblToastLocation = new Krypton.Toolkit.KryptonLabel();
            this.kchkDoNotShowAgain = new Krypton.Toolkit.KryptonCheckBox();
            this.itbDismiss = new Krypton.Toolkit.InternalToastButton();
            this.kpnlFooter = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlFooter)).BeginInit();
            this.kpnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.tableLayoutPanel1);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(589, 250);
            this.kpnlMain.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kpbCountDown, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pbxImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kwlblNotificationTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kwlblNotificationContent, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(589, 250);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kpbCountDown
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.kpbCountDown, 2);
            this.kpbCountDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpbCountDown.Location = new System.Drawing.Point(3, 221);
            this.kpbCountDown.Name = "kpbCountDown";
            this.kpbCountDown.Size = new System.Drawing.Size(583, 26);
            this.kpbCountDown.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kpbCountDown.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbCountDown.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbCountDown.TabIndex = 0;
            this.kpbCountDown.Text = "kryptonProgressBar1";
            this.kpbCountDown.Values.Text = "kryptonProgressBar1";
            // 
            // pbxImage
            // 
            this.pbxImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxImage.Location = new System.Drawing.Point(5, 5);
            this.pbxImage.Margin = new System.Windows.Forms.Padding(5);
            this.pbxImage.Name = "pbxImage";
            this.tableLayoutPanel1.SetRowSpan(this.pbxImage, 2);
            this.pbxImage.Size = new System.Drawing.Size(128, 208);
            this.pbxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxImage.TabIndex = 1;
            this.pbxImage.TabStop = false;
            // 
            // kwlblNotificationTitle
            // 
            this.kwlblNotificationTitle.AutoSize = false;
            this.kwlblNotificationTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblNotificationTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.kwlblNotificationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblNotificationTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblNotificationTitle.Location = new System.Drawing.Point(141, 0);
            this.kwlblNotificationTitle.Name = "kwlblNotificationTitle";
            this.kwlblNotificationTitle.Size = new System.Drawing.Size(445, 49);
            this.kwlblNotificationTitle.Text = "kryptonWrapLabel1";
            this.kwlblNotificationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kwlblNotificationContent
            // 
            this.kwlblNotificationContent.AutoSize = false;
            this.kwlblNotificationContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblNotificationContent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblNotificationContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblNotificationContent.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblNotificationContent.Location = new System.Drawing.Point(141, 49);
            this.kwlblNotificationContent.Name = "kwlblNotificationContent";
            this.kwlblNotificationContent.Size = new System.Drawing.Size(445, 169);
            this.kwlblNotificationContent.Text = "kryptonWrapLabel2";
            this.kwlblNotificationContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(589, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(589, 49);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnDismiss.Location = new System.Drawing.Point(463, 13);
            this.kbtnDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnDismiss.Name = "kbtnDismiss";
            this.kbtnDismiss.Size = new System.Drawing.Size(48, 22);
            this.kbtnDismiss.TabIndex = 2;
            this.kbtnDismiss.Values.Text = "{0} ({1})";
            this.kbtnDismiss.Visible = false;
            this.kbtnDismiss.Click += new System.EventHandler(this.kbtnDismiss_Click);
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
            this.itbDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.itbDismiss.AutoSize = true;
            this.itbDismiss.BaseToastForm = this;
            this.itbDismiss.IsActionButton = false;
            this.itbDismiss.IsDismissButton = false;
            this.itbDismiss.Location = new System.Drawing.Point(531, 13);
            this.itbDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.itbDismiss.Name = "itbDismiss";
            this.itbDismiss.Size = new System.Drawing.Size(48, 22);
            this.itbDismiss.TabIndex = 5;
            this.itbDismiss.Values.Text = "{0} ({1})";
            // 
            // kpnlFooter
            // 
            this.kpnlFooter.Controls.Add(this.tableLayoutPanel2);
            this.kpnlFooter.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlFooter.Location = new System.Drawing.Point(0, 250);
            this.kpnlFooter.Name = "kpnlFooter";
            this.kpnlFooter.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlFooter.Size = new System.Drawing.Size(589, 50);
            this.kpnlFooter.TabIndex = 1;
            // 
            // VisualToastNotificationBasicWithProgressBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 300);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationBasicWithProgressBarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "";
            this.Load += new System.EventHandler(this.VisualToastNotificationBasicWithProgressBarForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlFooter)).EndInit();
            this.kpnlFooter.ResumeLayout(false);
            this.kpnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private KryptonPanel kpnlMain;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonProgressBar kpbCountDown;
        private PictureBox pbxImage;
        private KryptonWrapLabel kwlblNotificationTitle;
        private KryptonWrapLabel kwlblNotificationContent;
        private KryptonBorderEdge kryptonBorderEdge1;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonButton kbtnDismiss;
        private KryptonPanel kpnlFooter;
        private KryptonLabel klblToastLocation;
        private KryptonCheckBox kchkDoNotShowAgain;
        private InternalToastButton itbDismiss;
    }
}