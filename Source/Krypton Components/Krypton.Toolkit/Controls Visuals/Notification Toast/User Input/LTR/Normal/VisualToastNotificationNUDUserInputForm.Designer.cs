namespace Krypton.Toolkit
{
    partial class VisualToastNotificationNUDUserInputForm
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.krtbNotificationContentText = new Krypton.Toolkit.KryptonRichTextBox();
            this.klblHeader = new Krypton.Toolkit.KryptonLabel();
            this.knudUserInput = new Krypton.Toolkit.KryptonNumericUpDown();
            this.pbxNotificationIcon = new System.Windows.Forms.PictureBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNotificationIcon)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tableLayoutPanel2);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 295);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(662, 50);
            this.kpnlButtons.TabIndex = 6;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(662, 49);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnDismiss.Location = new System.Drawing.Point(536, 13);
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
            this.itbDismiss.Location = new System.Drawing.Point(604, 13);
            this.itbDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.itbDismiss.Name = "itbDismiss";
            this.itbDismiss.Size = new System.Drawing.Size(48, 22);
            this.itbDismiss.TabIndex = 5;
            this.itbDismiss.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.itbDismiss.Values.Text = "{0} ({1})";
            this.itbDismiss.Click += new System.EventHandler(this.itbDismiss_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(662, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kryptonPanel1);
            this.kpnlMain.Controls.Add(this.tlpMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(662, 295);
            this.kpnlMain.TabIndex = 7;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel3);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(662, 295);
            this.kryptonPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.pbxNotificationIcon, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(662, 295);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.krtbNotificationContentText, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.klblHeader, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.knudUserInput, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(141, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(518, 289);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // krtbNotificationContentText
            // 
            this.krtbNotificationContentText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbNotificationContentText.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.krtbNotificationContentText.Location = new System.Drawing.Point(5, 44);
            this.krtbNotificationContentText.Margin = new System.Windows.Forms.Padding(5);
            this.krtbNotificationContentText.Name = "krtbNotificationContentText";
            this.krtbNotificationContentText.ReadOnly = true;
            this.krtbNotificationContentText.Size = new System.Drawing.Size(508, 208);
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
            this.klblHeader.Size = new System.Drawing.Size(508, 29);
            this.klblHeader.TabIndex = 4;
            this.klblHeader.Values.Text = "kryptonLabel1";
            // 
            // knudUserInput
            // 
            this.knudUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knudUserInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudUserInput.Location = new System.Drawing.Point(5, 262);
            this.knudUserInput.Margin = new System.Windows.Forms.Padding(5);
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
            this.knudUserInput.Size = new System.Drawing.Size(508, 22);
            this.knudUserInput.TabIndex = 6;
            this.knudUserInput.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // pbxNotificationIcon
            // 
            this.pbxNotificationIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxNotificationIcon.Location = new System.Drawing.Point(5, 5);
            this.pbxNotificationIcon.Margin = new System.Windows.Forms.Padding(5);
            this.pbxNotificationIcon.Name = "pbxNotificationIcon";
            this.pbxNotificationIcon.Size = new System.Drawing.Size(128, 285);
            this.pbxNotificationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxNotificationIcon.TabIndex = 0;
            this.pbxNotificationIcon.TabStop = false;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(662, 295);
            this.tlpMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(656, 289);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // VisualToastNotificationNUDUserInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 345);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationNUDUserInputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.VisualToastNotificationNumericUpDownUserInputForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNotificationIcon)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kpnlMain;
        private KryptonPanel kryptonPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private PictureBox pbxNotificationIcon;
        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonButton kbtnDismiss;
        private KryptonLabel klblToastLocation;
        private KryptonCheckBox kchkDoNotShowAgain;
        private InternalToastButton itbDismiss;
        private TableLayoutPanel tableLayoutPanel4;
        private KryptonRichTextBox krtbNotificationContentText;
        private KryptonLabel klblHeader;
        private KryptonNumericUpDown knudUserInput;
    }
}