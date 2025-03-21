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
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnDismiss = new Krypton.Toolkit.KryptonButton();
            this.klblToastLocation = new Krypton.Toolkit.KryptonLabel();
            this.kchkDoNotShowAgain = new Krypton.Toolkit.KryptonCheckBox();
            this.itbDismiss = new Krypton.Toolkit.InternalToastButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kpnlUserInput = new Krypton.Toolkit.KryptonPanel();
            this.krtbNotificationContent = new Krypton.Toolkit.KryptonRichTextBox();
            this.kcmbUserInput = new Krypton.Toolkit.KryptonComboBox();
            this.kdtpUserInput = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kdudUserInput = new Krypton.Toolkit.KryptonDomainUpDown();
            this.kmtxtUserInput = new Krypton.Toolkit.KryptonMaskedTextBox();
            this.knudUserInput = new Krypton.Toolkit.KryptonNumericUpDown();
            this.ktxtUserInput = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).BeginInit();
            this.kpnlContent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlUserInput)).BeginInit();
            this.kpnlUserInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbUserInput)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tableLayoutPanel2);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 305);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(636, 50);
            this.kpnlButtons.TabIndex = 5;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(636, 49);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnDismiss.Location = new System.Drawing.Point(510, 13);
            this.kbtnDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnDismiss.Name = "kbtnDismiss";
            this.kbtnDismiss.Size = new System.Drawing.Size(48, 22);
            this.kbtnDismiss.TabIndex = 2;
            this.kbtnDismiss.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnDismiss.Values.Text = "{0} ({1})";
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
            this.itbDismiss.Location = new System.Drawing.Point(578, 13);
            this.itbDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.itbDismiss.Name = "itbDismiss";
            this.itbDismiss.Size = new System.Drawing.Size(48, 22);
            this.itbDismiss.TabIndex = 5;
            this.itbDismiss.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.itbDismiss.Values.Text = "{0} ({1})";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(636, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kpnlContent
            // 
            this.kpnlContent.Controls.Add(this.tableLayoutPanel1);
            this.kpnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContent.Location = new System.Drawing.Point(0, 0);
            this.kpnlContent.Name = "kpnlContent";
            this.kpnlContent.Size = new System.Drawing.Size(636, 305);
            this.kpnlContent.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbxIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(636, 305);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pbxIcon
            // 
            this.pbxIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxIcon.Location = new System.Drawing.Point(5, 5);
            this.pbxIcon.Margin = new System.Windows.Forms.Padding(5);
            this.pbxIcon.Name = "pbxIcon";
            this.pbxIcon.Size = new System.Drawing.Size(128, 295);
            this.pbxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxIcon.TabIndex = 0;
            this.pbxIcon.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.kryptonWrapLabel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.kpnlUserInput, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.krtbNotificationContent, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(143, 5);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(488, 295);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonWrapLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(5, 5);
            this.kryptonWrapLabel1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(478, 25);
            this.kryptonWrapLabel1.Text = "kryptonWrapLabel1";
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
            this.kpnlUserInput.Location = new System.Drawing.Point(5, 265);
            this.kpnlUserInput.Margin = new System.Windows.Forms.Padding(5);
            this.kpnlUserInput.Name = "kpnlUserInput";
            this.kpnlUserInput.Size = new System.Drawing.Size(478, 25);
            this.kpnlUserInput.TabIndex = 1;
            // 
            // krtbNotificationContent
            // 
            this.krtbNotificationContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbNotificationContent.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.krtbNotificationContent.Location = new System.Drawing.Point(5, 40);
            this.krtbNotificationContent.Margin = new System.Windows.Forms.Padding(5);
            this.krtbNotificationContent.Name = "krtbNotificationContent";
            this.krtbNotificationContent.ReadOnly = true;
            this.krtbNotificationContent.Size = new System.Drawing.Size(478, 215);
            this.krtbNotificationContent.TabIndex = 2;
            this.krtbNotificationContent.Text = "kryptonRichTextBox1";
            // 
            // kcmbUserInput
            // 
            this.kcmbUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbUserInput.DropDownWidth = 478;
            this.kcmbUserInput.Location = new System.Drawing.Point(0, 0);
            this.kcmbUserInput.Name = "kcmbUserInput";
            this.kcmbUserInput.Size = new System.Drawing.Size(478, 25);
            this.kcmbUserInput.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbUserInput.TabIndex = 0;
            this.kcmbUserInput.Text = "kryptonComboBox1";
            // 
            // kdtpUserInput
            // 
            this.kdtpUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdtpUserInput.Location = new System.Drawing.Point(0, 0);
            this.kdtpUserInput.Name = "kdtpUserInput";
            this.kdtpUserInput.Size = new System.Drawing.Size(478, 25);
            this.kdtpUserInput.TabIndex = 1;
            // 
            // kdudUserInput
            // 
            this.kdudUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdudUserInput.Location = new System.Drawing.Point(0, 0);
            this.kdudUserInput.Name = "kdudUserInput";
            this.kdudUserInput.Size = new System.Drawing.Size(478, 25);
            this.kdudUserInput.TabIndex = 2;
            this.kdudUserInput.Text = "kryptonDomainUpDown1";
            // 
            // kmtxtUserInput
            // 
            this.kmtxtUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kmtxtUserInput.Location = new System.Drawing.Point(0, 0);
            this.kmtxtUserInput.Name = "kmtxtUserInput";
            this.kmtxtUserInput.Size = new System.Drawing.Size(478, 23);
            this.kmtxtUserInput.TabIndex = 3;
            this.kmtxtUserInput.Text = "kryptonMaskedTextBox1";
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
            this.knudUserInput.Size = new System.Drawing.Size(478, 25);
            this.knudUserInput.TabIndex = 4;
            this.knudUserInput.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // ktxtUserInput
            // 
            this.ktxtUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtUserInput.Location = new System.Drawing.Point(0, 0);
            this.ktxtUserInput.Name = "ktxtUserInput";
            this.ktxtUserInput.Size = new System.Drawing.Size(478, 23);
            this.ktxtUserInput.TabIndex = 5;
            this.ktxtUserInput.Text = "kryptonTextBox1";
            // 
            // VisualToastNotificationUserInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 355);
            this.Controls.Add(this.kpnlContent);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationUserInputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "VisualToastNotificationUserInputForm";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).EndInit();
            this.kpnlContent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlUserInput)).EndInit();
            this.kpnlUserInput.ResumeLayout(false);
            this.kpnlUserInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbUserInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonButton kbtnDismiss;
        private KryptonLabel klblToastLocation;
        private KryptonCheckBox kchkDoNotShowAgain;
        private InternalToastButton itbDismiss;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kpnlContent;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pbxIcon;
        private TableLayoutPanel tableLayoutPanel3;
        private KryptonWrapLabel kryptonWrapLabel1;
        private KryptonPanel kpnlUserInput;
        private KryptonRichTextBox krtbNotificationContent;
        private KryptonComboBox kcmbUserInput;
        private KryptonDateTimePicker kdtpUserInput;
        private KryptonDomainUpDown kdudUserInput;
        private KryptonMaskedTextBox kmtxtUserInput;
        private KryptonNumericUpDown knudUserInput;
        private KryptonTextBox ktxtUserInput;
    }
}