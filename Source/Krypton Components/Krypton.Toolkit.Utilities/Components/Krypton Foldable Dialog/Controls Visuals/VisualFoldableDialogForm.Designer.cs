namespace Krypton.Toolkit.Utilities
{
    partial class VisualFoldableDialogForm
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
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnExpander = new Krypton.Toolkit.KryptonButton();
            this.kbtnButton1 = new Krypton.Toolkit.KryptonButton();
            this.kbtnButton2 = new Krypton.Toolkit.KryptonButton();
            this.kbtnButton3 = new Krypton.Toolkit.KryptonButton();
            this.kbrdEdge = new Krypton.Toolkit.KryptonBorderEdge();
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kpnlDetails = new Krypton.Toolkit.KryptonPanel();
            this.krtbDetails = new Krypton.Toolkit.KryptonRichTextBox();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.kwlHeading = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlMessage = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDetails)).BeginInit();
            this.kpnlDetails.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tlpButtons);
            this.kpnlButtons.Controls.Add(this.kbrdEdge);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 251);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(494, 50);
            this.kpnlButtons.TabIndex = 1;
            // 
            // tlpButtons
            // 
            this.tlpButtons.BackColor = System.Drawing.Color.Transparent;
            this.tlpButtons.ColumnCount = 4;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.Controls.Add(this.kbtnExpander, 0, 0);
            this.tlpButtons.Controls.Add(this.kbtnButton1, 1, 0);
            this.tlpButtons.Controls.Add(this.kbtnButton2, 2, 0);
            this.tlpButtons.Controls.Add(this.kbtnButton3, 3, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 1);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(494, 49);
            this.tlpButtons.TabIndex = 0;
            // 
            // kbtnExpander
            // 
            this.kbtnExpander.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnExpander.AutoSize = true;
            this.kbtnExpander.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnExpander.ButtonStyle = Krypton.Toolkit.ButtonStyle.LowProfile;
            this.kbtnExpander.Location = new System.Drawing.Point(10, 12);
            this.kbtnExpander.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.kbtnExpander.Name = "kbtnExpander";
            this.kbtnExpander.Size = new System.Drawing.Size(90, 25);
            this.kbtnExpander.TabIndex = 3;
            this.kbtnExpander.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnExpander.Values.Text = "Show details";
            this.kbtnExpander.Click += new System.EventHandler(this.kbtnExpander_Click);
            // 
            // kbtnButton1
            // 
            this.kbtnButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnButton1.AutoSize = true;
            this.kbtnButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnButton1.Location = new System.Drawing.Point(238, 12);
            this.kbtnButton1.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.kbtnButton1.MinimumSize = new System.Drawing.Size(78, 25);
            this.kbtnButton1.Name = "kbtnButton1";
            this.kbtnButton1.Size = new System.Drawing.Size(78, 25);
            this.kbtnButton1.TabIndex = 0;
            this.kbtnButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnButton1.Values.Text = "B1";
            // 
            // kbtnButton2
            // 
            this.kbtnButton2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnButton2.AutoSize = true;
            this.kbtnButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnButton2.Location = new System.Drawing.Point(324, 12);
            this.kbtnButton2.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.kbtnButton2.MinimumSize = new System.Drawing.Size(78, 25);
            this.kbtnButton2.Name = "kbtnButton2";
            this.kbtnButton2.Size = new System.Drawing.Size(78, 25);
            this.kbtnButton2.TabIndex = 1;
            this.kbtnButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnButton2.Values.Text = "B2";
            // 
            // kbtnButton3
            // 
            this.kbtnButton3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnButton3.AutoSize = true;
            this.kbtnButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnButton3.Location = new System.Drawing.Point(410, 12);
            this.kbtnButton3.Margin = new System.Windows.Forms.Padding(4, 10, 10, 10);
            this.kbtnButton3.MinimumSize = new System.Drawing.Size(78, 25);
            this.kbtnButton3.Name = "kbtnButton3";
            this.kbtnButton3.Size = new System.Drawing.Size(78, 25);
            this.kbtnButton3.TabIndex = 2;
            this.kbtnButton3.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnButton3.Values.Text = "B3";
            // 
            // kbrdEdge
            // 
            this.kbrdEdge.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kbrdEdge.Dock = System.Windows.Forms.DockStyle.Top;
            this.kbrdEdge.Location = new System.Drawing.Point(0, 0);
            this.kbrdEdge.Name = "kbrdEdge";
            this.kbrdEdge.Size = new System.Drawing.Size(494, 1);
            this.kbrdEdge.Text = "kryptonBorderEdge1";
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kpnlDetails);
            this.kpnlMain.Controls.Add(this.tlpHeader);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(494, 251);
            this.kpnlMain.TabIndex = 0;
            // 
            // kpnlDetails
            // 
            this.kpnlDetails.Controls.Add(this.krtbDetails);
            this.kpnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlDetails.Location = new System.Drawing.Point(0, 84);
            this.kpnlDetails.Name = "kpnlDetails";
            this.kpnlDetails.Padding = new System.Windows.Forms.Padding(12, 0, 12, 12);
            this.kpnlDetails.Size = new System.Drawing.Size(494, 167);
            this.kpnlDetails.TabIndex = 1;
            // 
            // krtbDetails
            // 
            this.krtbDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbDetails.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.krtbDetails.Location = new System.Drawing.Point(12, 0);
            this.krtbDetails.Name = "krtbDetails";
            this.krtbDetails.ReadOnly = true;
            this.krtbDetails.Size = new System.Drawing.Size(470, 155);
            this.krtbDetails.TabIndex = 0;
            this.krtbDetails.Text = "";
            // 
            // tlpHeader
            // 
            this.tlpHeader.AutoSize = true;
            this.tlpHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpHeader.BackColor = System.Drawing.Color.Transparent;
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Controls.Add(this.pbxIcon, 0, 0);
            this.tlpHeader.Controls.Add(this.kwlHeading, 1, 0);
            this.tlpHeader.Controls.Add(this.kwlMessage, 1, 1);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpHeader.Location = new System.Drawing.Point(0, 0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 2;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpHeader.Size = new System.Drawing.Size(494, 84);
            this.tlpHeader.TabIndex = 0;
            // 
            // pbxIcon
            // 
            this.pbxIcon.BackColor = System.Drawing.Color.Transparent;
            this.pbxIcon.Location = new System.Drawing.Point(12, 12);
            this.pbxIcon.Margin = new System.Windows.Forms.Padding(12, 12, 8, 12);
            this.pbxIcon.Name = "pbxIcon";
            this.tlpHeader.SetRowSpan(this.pbxIcon, 2);
            this.pbxIcon.Size = new System.Drawing.Size(48, 48);
            this.pbxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxIcon.TabIndex = 0;
            this.pbxIcon.TabStop = false;
            // 
            // kwlHeading
            // 
            this.kwlHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.kwlHeading.AutoSize = true;
            this.kwlHeading.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlHeading.Location = new System.Drawing.Point(71, 12);
            this.kwlHeading.Margin = new System.Windows.Forms.Padding(3, 12, 12, 3);
            this.kwlHeading.Name = "kwlHeading";
            this.kwlHeading.Size = new System.Drawing.Size(411, 25);
            this.kwlHeading.Text = "kryptonWrapLabel1";
            // 
            // kwlMessage
            // 
            this.kwlMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.kwlMessage.AutoSize = true;
            this.kwlMessage.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlMessage.Location = new System.Drawing.Point(71, 40);
            this.kwlMessage.Margin = new System.Windows.Forms.Padding(3, 3, 12, 12);
            this.kwlMessage.Name = "kwlMessage";
            this.kwlMessage.Size = new System.Drawing.Size(411, 32);
            this.kwlMessage.Text = "kryptonWrapLabel2";
            // 
            // VisualFoldableDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 301);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualFoldableDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisualFoldableDialogForm";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.tlpButtons.ResumeLayout(false);
            this.tlpButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDetails)).EndInit();
            this.kpnlDetails.ResumeLayout(false);
            this.kpnlDetails.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private TableLayoutPanel tlpButtons;
        private KryptonButton kbtnExpander;
        private KryptonButton kbtnButton1;
        private KryptonButton kbtnButton2;
        private KryptonButton kbtnButton3;
        private KryptonBorderEdge kbrdEdge;
        private KryptonPanel kpnlMain;
        private KryptonPanel kpnlDetails;
        private KryptonRichTextBox krtbDetails;
        private TableLayoutPanel tlpHeader;
        private PictureBox pbxIcon;
        private KryptonWrapLabel kwlHeading;
        private KryptonWrapLabel kwlMessage;
    }
}
