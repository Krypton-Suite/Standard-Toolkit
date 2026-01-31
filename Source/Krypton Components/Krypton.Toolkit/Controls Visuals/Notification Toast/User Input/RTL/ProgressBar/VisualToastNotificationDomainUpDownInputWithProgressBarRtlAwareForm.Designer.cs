namespace Krypton.Toolkit
{
    partial class VisualToastNotificationDomainUpDownInputWithProgressBarRtlAwareForm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnDismiss = new Krypton.Toolkit.KryptonButton();
            this.klblToastLocation = new Krypton.Toolkit.KryptonLabel();
            this.kchkDoNotShowAgain = new Krypton.Toolkit.KryptonCheckBox();
            this.itbDismiss = new Krypton.Toolkit.InternalToastButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.kpbCountDown = new Krypton.Toolkit.KryptonProgressBar();
            this.pbxNotificationIcon = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.krtbNotificationContentText = new Krypton.Toolkit.KryptonRichTextBox();
            this.klblHeader = new Krypton.Toolkit.KryptonLabel();
            this.kdudUserInput = new Krypton.Toolkit.KryptonDomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNotificationIcon)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tableLayoutPanel2);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 361);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(642, 50);
            this.kpnlButtons.TabIndex = 7;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(642, 49);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnDismiss.Location = new System.Drawing.Point(53, 13);
            this.kbtnDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnDismiss.Name = "kbtnDismiss";
            this.kbtnDismiss.Size = new System.Drawing.Size(23, 22);
            this.kbtnDismiss.TabIndex = 2;
            this.kbtnDismiss.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnDismiss.Values.Text = "{0}";
            this.kbtnDismiss.Visible = false;
            // 
            // klblToastLocation
            // 
            this.klblToastLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblToastLocation.Location = new System.Drawing.Point(561, 23);
            this.klblToastLocation.Margin = new System.Windows.Forms.Padding(10);
            this.klblToastLocation.Name = "klblToastLocation";
            this.klblToastLocation.Size = new System.Drawing.Size(6, 2);
            this.klblToastLocation.TabIndex = 3;
            this.klblToastLocation.Values.Text = "";
            // 
            // kchkDoNotShowAgain
            // 
            this.kchkDoNotShowAgain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkDoNotShowAgain.Location = new System.Drawing.Point(587, 10);
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
            this.itbDismiss.Location = new System.Drawing.Point(10, 13);
            this.itbDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.itbDismiss.Name = "itbDismiss";
            this.itbDismiss.Size = new System.Drawing.Size(23, 22);
            this.itbDismiss.TabIndex = 5;
            this.itbDismiss.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.itbDismiss.Values.Text = "{0}";
            this.itbDismiss.Click += new System.EventHandler(this.itbDismiss_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(642, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.tlpMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(642, 361);
            this.kpnlMain.TabIndex = 8;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tlpMain.Controls.Add(this.kpbCountDown, 0, 1);
            this.tlpMain.Controls.Add(this.pbxNotificationIcon, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(642, 361);
            this.tlpMain.TabIndex = 0;
            // 
            // kpbCountDown
            // 
            this.tlpMain.SetColumnSpan(this.kpbCountDown, 2);
            this.kpbCountDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpbCountDown.Location = new System.Drawing.Point(3, 332);
            this.kpbCountDown.Name = "kpbCountDown";
            this.kpbCountDown.Size = new System.Drawing.Size(636, 26);
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
            this.pbxNotificationIcon.Location = new System.Drawing.Point(509, 5);
            this.pbxNotificationIcon.Margin = new System.Windows.Forms.Padding(5);
            this.pbxNotificationIcon.Name = "pbxNotificationIcon";
            this.pbxNotificationIcon.Size = new System.Drawing.Size(128, 319);
            this.pbxNotificationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxNotificationIcon.TabIndex = 0;
            this.pbxNotificationIcon.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.krtbNotificationContentText, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.klblHeader, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.kdudUserInput, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(498, 323);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // krtbNotificationContentText
            // 
            this.krtbNotificationContentText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbNotificationContentText.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.krtbNotificationContentText.Location = new System.Drawing.Point(5, 44);
            this.krtbNotificationContentText.Margin = new System.Windows.Forms.Padding(5);
            this.krtbNotificationContentText.Name = "krtbNotificationContentText";
            this.krtbNotificationContentText.ReadOnly = true;
            this.krtbNotificationContentText.Size = new System.Drawing.Size(488, 242);
            this.krtbNotificationContentText.StateCommon.Border.Color1 = System.Drawing.Color.Transparent;
            this.krtbNotificationContentText.StateCommon.Border.Color2 = System.Drawing.Color.Transparent;
            this.krtbNotificationContentText.TabIndex = 5;
            this.krtbNotificationContentText.Text = "kryptonRichTextBox1";
            // 
            // klblHeader
            // 
            this.klblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblHeader.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.klblHeader.Location = new System.Drawing.Point(5, 5);
            this.klblHeader.Margin = new System.Windows.Forms.Padding(5);
            this.klblHeader.Name = "klblHeader";
            this.klblHeader.Size = new System.Drawing.Size(488, 29);
            this.klblHeader.TabIndex = 4;
            this.klblHeader.Values.Text = "kryptonLabel1";
            // 
            // kdudUserInput
            // 
            this.kdudUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdudUserInput.Location = new System.Drawing.Point(5, 296);
            this.kdudUserInput.Margin = new System.Windows.Forms.Padding(5);
            this.kdudUserInput.Name = "kdudUserInput";
            this.kdudUserInput.Size = new System.Drawing.Size(488, 22);
            this.kdudUserInput.TabIndex = 6;
            // 
            // VisualToastNotificationDomainUpDownInputWithProgressBarRtlAwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 411);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationDomainUpDownInputWithProgressBarRtlAwareForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.VisualToastNotificationDomainUpDownInputWithProgressBarRtlAwareForm_Load);
            this.LocationChanged += new System.EventHandler(this.VisualToastNotificationDomainUpDownInputWithProgressBarRtlAwareForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxNotificationIcon)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kpnlMain;
        private TableLayoutPanel tlpMain;
        private KryptonProgressBar kpbCountDown;
        private PictureBox pbxNotificationIcon;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonButton kbtnDismiss;
        private KryptonLabel klblToastLocation;
        private KryptonCheckBox kchkDoNotShowAgain;
        private InternalToastButton itbDismiss;
        private TableLayoutPanel tableLayoutPanel4;
        private KryptonRichTextBox krtbNotificationContentText;
        private KryptonLabel klblHeader;
        private KryptonDomainUpDown kdudUserInput;
    }
}