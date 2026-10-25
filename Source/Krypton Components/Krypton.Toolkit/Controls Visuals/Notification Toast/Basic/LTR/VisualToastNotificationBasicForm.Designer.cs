namespace Krypton.Toolkit
{
    partial class VisualToastNotificationBasicForm
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
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.krtbNotificationContentText = new Krypton.Toolkit.KryptonRichTextBox();
            this.klblHeader = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tableLayoutPanel2);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 177);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(575, 50);
            this.kpnlButtons.TabIndex = 0;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(575, 49);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnDismiss.Location = new System.Drawing.Point(449, 13);
            this.kbtnDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnDismiss.Name = "kbtnDismiss";
            this.kbtnDismiss.Size = new System.Drawing.Size(48, 22);
            this.kbtnDismiss.TabIndex = 2;
            this.kbtnDismiss.Values.DropDownArrowColor = System.Drawing.Color.Empty;
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
            this.itbDismiss.AutoSize = true;
            this.itbDismiss.Location = new System.Drawing.Point(517, 13);
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
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(575, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(575, 177);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbxIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.krtbNotificationContentText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.klblHeader, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(575, 177);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pbxIcon
            // 
            this.pbxIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxIcon.Location = new System.Drawing.Point(3, 3);
            this.pbxIcon.Name = "pbxIcon";
            this.pbxIcon.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.SetRowSpan(this.pbxIcon, 2);
            this.pbxIcon.Size = new System.Drawing.Size(128, 171);
            this.pbxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxIcon.TabIndex = 0;
            this.pbxIcon.TabStop = false;
            // 
            // krtbNotificationContentText
            // 
            this.krtbNotificationContentText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbNotificationContentText.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.krtbNotificationContentText.Location = new System.Drawing.Point(139, 44);
            this.krtbNotificationContentText.Margin = new System.Windows.Forms.Padding(5);
            this.krtbNotificationContentText.Name = "krtbNotificationContentText";
            this.krtbNotificationContentText.ReadOnly = true;
            this.krtbNotificationContentText.Size = new System.Drawing.Size(431, 128);
            this.krtbNotificationContentText.StateCommon.Border.Color1 = System.Drawing.Color.Transparent;
            this.krtbNotificationContentText.StateCommon.Border.Color2 = System.Drawing.Color.Transparent;
            this.krtbNotificationContentText.TabIndex = 2;
            this.krtbNotificationContentText.Text = "kryptonRichTextBox1";
            // 
            // klblHeader
            // 
            this.klblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblHeader.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.klblHeader.Location = new System.Drawing.Point(139, 5);
            this.klblHeader.Margin = new System.Windows.Forms.Padding(5);
            this.klblHeader.Name = "klblHeader";
            this.klblHeader.Size = new System.Drawing.Size(431, 29);
            this.klblHeader.TabIndex = 3;
            this.klblHeader.Values.Text = "kryptonLabel1";
            // 
            // VisualToastNotificationBasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 227);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationBasicForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.StateCommon.Border.Width = 2;
            this.UseThemeFormChromeBorderWidth = false;
            this.Load += new System.EventHandler(this.VisualToastNotificationBasicForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pbxIcon;
        private KryptonBorderEdge kryptonBorderEdge1;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonButton kbtnDismiss;
        private KryptonLabel klblToastLocation;
        private KryptonCheckBox kchkDoNotShowAgain;
        private InternalToastButton itbDismiss;
        private KryptonRichTextBox krtbNotificationContentText;
        private KryptonLabel klblHeader;
    }
}