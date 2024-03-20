namespace Krypton.Toolkit
{
    partial class VisualToastNotificationUserInputForm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.klblToastLocation = new Krypton.Toolkit.KryptonLabel();
            this.kbtnDismiss = new Krypton.Toolkit.KryptonButton();
            this.kchkDoNotShowAgain = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlNotificationTitle = new Krypton.Toolkit.KryptonWrapLabel();
            this.kpnlUserInput = new Krypton.Toolkit.KryptonPanel();
            this.ktxtUserInput = new Krypton.Toolkit.KryptonTextBox();
            this.knudUserInput = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kmtxtUserInput = new Krypton.Toolkit.KryptonMaskedTextBox();
            this.kdudUserInput = new Krypton.Toolkit.KryptonDomainUpDown();
            this.kdtpUserInput = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kcmbUserInput = new Krypton.Toolkit.KryptonComboBox();
            this.kwlNotificationContent = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlUserInput)).BeginInit();
            this.kpnlUserInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbUserInput)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel2);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 327);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(614, 50);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.klblToastLocation, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.kbtnDismiss, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.kchkDoNotShowAgain, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(614, 49);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // klblToastLocation
            // 
            this.klblToastLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblToastLocation.Location = new System.Drawing.Point(75, 23);
            this.klblToastLocation.Margin = new System.Windows.Forms.Padding(10);
            this.klblToastLocation.Name = "klblToastLocation";
            this.klblToastLocation.Size = new System.Drawing.Size(6, 2);
            this.klblToastLocation.TabIndex = 4;
            this.klblToastLocation.Values.Text = "";
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnDismiss.Location = new System.Drawing.Point(510, 13);
            this.kbtnDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnDismiss.Name = "kbtnDismiss";
            this.kbtnDismiss.Size = new System.Drawing.Size(94, 22);
            this.kbtnDismiss.TabIndex = 2;
            this.kbtnDismiss.Values.Text = "kryptonButton1";
            this.kbtnDismiss.Click += new System.EventHandler(this.kbtnDismiss_Click);
            // 
            // kchkDoNotShowAgain
            // 
            this.kchkDoNotShowAgain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkDoNotShowAgain.Location = new System.Drawing.Point(10, 10);
            this.kchkDoNotShowAgain.Margin = new System.Windows.Forms.Padding(10);
            this.kchkDoNotShowAgain.Name = "kchkDoNotShowAgain";
            this.kchkDoNotShowAgain.Size = new System.Drawing.Size(45, 29);
            this.kchkDoNotShowAgain.TabIndex = 5;
            this.kchkDoNotShowAgain.Values.Text = "CB1";
            this.kchkDoNotShowAgain.CheckedChanged += new System.EventHandler(this.kchkDoNotShowAgain_CheckedChanged);
            this.kchkDoNotShowAgain.CheckStateChanged += new System.EventHandler(this.kchkDoNotShowAgain_CheckStateChanged);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(614, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tlpMain);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(614, 327);
            this.kryptonPanel2.TabIndex = 3;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pbxIcon, 0, 0);
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(614, 327);
            this.tlpMain.TabIndex = 0;
            // 
            // pbxIcon
            // 
            this.pbxIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxIcon.Location = new System.Drawing.Point(5, 5);
            this.pbxIcon.Margin = new System.Windows.Forms.Padding(5);
            this.pbxIcon.Name = "pbxIcon";
            this.pbxIcon.Size = new System.Drawing.Size(128, 317);
            this.pbxIcon.TabIndex = 0;
            this.pbxIcon.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlNotificationTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kpnlUserInput, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.kwlNotificationContent, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(141, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 321);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // kwlNotificationTitle
            // 
            this.kwlNotificationTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlNotificationTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.kwlNotificationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlNotificationTitle.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlNotificationTitle.Location = new System.Drawing.Point(3, 0);
            this.kwlNotificationTitle.Name = "kwlNotificationTitle";
            this.kwlNotificationTitle.Size = new System.Drawing.Size(464, 25);
            this.kwlNotificationTitle.Text = "kryptonWrapLabel1";
            this.kwlNotificationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kpnlUserInput
            // 
            this.kpnlUserInput.Controls.Add(this.ktxtUserInput);
            this.kpnlUserInput.Controls.Add(this.knudUserInput);
            this.kpnlUserInput.Controls.Add(this.kmtxtUserInput);
            this.kpnlUserInput.Controls.Add(this.kdudUserInput);
            this.kpnlUserInput.Controls.Add(this.kdtpUserInput);
            this.kpnlUserInput.Controls.Add(this.kcmbUserInput);
            this.kpnlUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlUserInput.Location = new System.Drawing.Point(3, 292);
            this.kpnlUserInput.Name = "kpnlUserInput";
            this.kpnlUserInput.Size = new System.Drawing.Size(464, 26);
            this.kpnlUserInput.TabIndex = 1;
            // 
            // ktxtUserInput
            // 
            this.ktxtUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtUserInput.Location = new System.Drawing.Point(0, 0);
            this.ktxtUserInput.Name = "ktxtUserInput";
            this.ktxtUserInput.Size = new System.Drawing.Size(464, 23);
            this.ktxtUserInput.TabIndex = 5;
            this.ktxtUserInput.Text = "kryptonTextBox1";
            this.ktxtUserInput.TextChanged += new System.EventHandler(this.ktxtUserInput_TextChanged);
            // 
            // knudUserInput
            // 
            this.knudUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knudUserInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudUserInput.Location = new System.Drawing.Point(0, 0);
            this.knudUserInput.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.knudUserInput.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudUserInput.Name = "knudUserInput";
            this.knudUserInput.Size = new System.Drawing.Size(464, 22);
            this.knudUserInput.TabIndex = 4;
            this.knudUserInput.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudUserInput.ValueChanged += new System.EventHandler(this.knudUserInput_ValueChanged);
            // 
            // kmtxtUserInput
            // 
            this.kmtxtUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kmtxtUserInput.Location = new System.Drawing.Point(0, 0);
            this.kmtxtUserInput.Name = "kmtxtUserInput";
            this.kmtxtUserInput.Size = new System.Drawing.Size(464, 23);
            this.kmtxtUserInput.TabIndex = 3;
            this.kmtxtUserInput.Text = "kryptonMaskedTextBox1";
            this.kmtxtUserInput.TextChanged += new System.EventHandler(this.kmtxtUserInput_TextChanged);
            // 
            // kdudUserInput
            // 
            this.kdudUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdudUserInput.Location = new System.Drawing.Point(0, 0);
            this.kdudUserInput.Name = "kdudUserInput";
            this.kdudUserInput.Size = new System.Drawing.Size(464, 22);
            this.kdudUserInput.TabIndex = 2;
            this.kdudUserInput.Text = "kryptonDomainUpDown1";
            this.kdudUserInput.SelectedItemChanged += new System.EventHandler(this.kdudUserInput_SelectedItemChanged);
            this.kdudUserInput.TextChanged += new System.EventHandler(this.kdudUserInput_TextChanged);
            // 
            // kdtpUserInput
            // 
            this.kdtpUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdtpUserInput.Location = new System.Drawing.Point(0, 0);
            this.kdtpUserInput.Name = "kdtpUserInput";
            this.kdtpUserInput.Size = new System.Drawing.Size(464, 21);
            this.kdtpUserInput.TabIndex = 1;
            this.kdtpUserInput.ValueChanged += new System.EventHandler(this.kdtpUserInput_ValueChanged);
            this.kdtpUserInput.TextChanged += new System.EventHandler(this.kdtpUserInput_TextChanged);
            // 
            // kcmbUserInput
            // 
            this.kcmbUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbUserInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbUserInput.DropDownWidth = 460;
            this.kcmbUserInput.IntegralHeight = false;
            this.kcmbUserInput.Location = new System.Drawing.Point(0, 0);
            this.kcmbUserInput.Name = "kcmbUserInput";
            this.kcmbUserInput.Size = new System.Drawing.Size(464, 21);
            this.kcmbUserInput.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbUserInput.TabIndex = 0;
            this.kcmbUserInput.SelectedIndexChanged += new System.EventHandler(this.kcmbUserInput_SelectedIndexChanged);
            this.kcmbUserInput.TextChanged += new System.EventHandler(this.kcmbUserInput_TextChanged);
            // 
            // kwlNotificationContent
            // 
            this.kwlNotificationContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlNotificationContent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlNotificationContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlNotificationContent.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlNotificationContent.Location = new System.Drawing.Point(3, 25);
            this.kwlNotificationContent.Name = "kwlNotificationContent";
            this.kwlNotificationContent.Size = new System.Drawing.Size(464, 264);
            this.kwlNotificationContent.Text = "kryptonWrapLabel2";
            this.kwlNotificationContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VisualToastNotificationUserInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 377);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationUserInputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.VisualToastNotificationUserInputForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlUserInput)).EndInit();
            this.kpnlUserInput.ResumeLayout(false);
            this.kpnlUserInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbUserInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonButton kbtnDismiss;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tlpMain;
        private PictureBox pbxIcon;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kwlNotificationTitle;
        private KryptonPanel kpnlUserInput;
        private KryptonComboBox kcmbUserInput;
        private KryptonWrapLabel kwlNotificationContent;
        private KryptonTextBox ktxtUserInput;
        private KryptonNumericUpDown knudUserInput;
        private KryptonMaskedTextBox kmtxtUserInput;
        private KryptonDomainUpDown kdudUserInput;
        private KryptonDateTimePicker kdtpUserInput;
        private KryptonLabel klblToastLocation;
        private KryptonCheckBox kchkDoNotShowAgain;
    }
}