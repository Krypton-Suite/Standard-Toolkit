namespace Krypton.Toolkit
{
    partial class VisualToolkitInformationForm
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
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnMoreDetails = new Krypton.Toolkit.KryptonButton();
            this.kbtnSystemInformation = new Krypton.Toolkit.KryptonButton();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.klblHeading = new Krypton.Toolkit.KryptonLabel();
            this.klblToolkitVersion = new Krypton.Toolkit.KryptonLabel();
            this.klblToolkitBuildDate = new Krypton.Toolkit.KryptonLabel();
            this.klblToolkitCopyright = new Krypton.Toolkit.KryptonLabel();
            this.krtbLicense = new Krypton.Toolkit.KryptonRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tlpContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kbtnMoreDetails);
            this.kryptonPanel2.Controls.Add(this.kbtnSystemInformation);
            this.kryptonPanel2.Controls.Add(this.kbtnOk);
            this.kryptonPanel2.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 432);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel2.Size = new System.Drawing.Size(941, 50);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // kbtnMoreDetails
            // 
            this.kbtnMoreDetails.Location = new System.Drawing.Point(13, 13);
            this.kbtnMoreDetails.Name = "kbtnMoreDetails";
            this.kbtnMoreDetails.Size = new System.Drawing.Size(133, 25);
            this.kbtnMoreDetails.TabIndex = 5;
            this.kbtnMoreDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnMoreDetails.Values.Text = "kryptonButton1";
            this.kbtnMoreDetails.Visible = false;
            this.kbtnMoreDetails.Click += new System.EventHandler(this.kbtnMoreDetails_Click);
            // 
            // kbtnSystemInformation
            // 
            this.kbtnSystemInformation.Location = new System.Drawing.Point(712, 13);
            this.kbtnSystemInformation.Name = "kbtnSystemInformation";
            this.kbtnSystemInformation.Size = new System.Drawing.Size(115, 25);
            this.kbtnSystemInformation.TabIndex = 3;
            this.kbtnSystemInformation.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnSystemInformation.Values.Text = "kryptonButton1";
            this.kbtnSystemInformation.Click += new System.EventHandler(this.kbtnSystemInformation_Click);
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(839, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 1;
            this.kbtnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOk.Values.Text = "O&K";
            this.kbtnOk.Values.UseAsADialogButton = true;
            this.kbtnOk.Click += new System.EventHandler(this.kbtnOk_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(941, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tlpContent);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(941, 432);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // tlpContent
            // 
            this.tlpContent.BackColor = System.Drawing.Color.Transparent;
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.pbxLogo, 0, 0);
            this.tlpContent.Controls.Add(this.klblHeading, 1, 0);
            this.tlpContent.Controls.Add(this.klblToolkitVersion, 1, 1);
            this.tlpContent.Controls.Add(this.klblToolkitBuildDate, 1, 2);
            this.tlpContent.Controls.Add(this.klblToolkitCopyright, 1, 3);
            this.tlpContent.Controls.Add(this.krtbLicense, 1, 4);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(0, 0);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 5;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Size = new System.Drawing.Size(941, 432);
            this.tlpContent.TabIndex = 1;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxLogo.Location = new System.Drawing.Point(5, 5);
            this.pbxLogo.Margin = new System.Windows.Forms.Padding(5);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Padding = new System.Windows.Forms.Padding(5);
            this.tlpContent.SetRowSpan(this.pbxLogo, 4);
            this.pbxLogo.Size = new System.Drawing.Size(128, 128);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLogo.TabIndex = 0;
            this.pbxLogo.TabStop = false;
            // 
            // klblHeading
            // 
            this.klblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblHeading.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.klblHeading.Location = new System.Drawing.Point(143, 5);
            this.klblHeading.Margin = new System.Windows.Forms.Padding(5);
            this.klblHeading.Name = "klblHeading";
            this.klblHeading.Size = new System.Drawing.Size(793, 29);
            this.klblHeading.TabIndex = 1;
            this.klblHeading.Values.Text = "kryptonLabel1";
            // 
            // klblToolkitVersion
            // 
            this.klblToolkitVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblToolkitVersion.Location = new System.Drawing.Point(143, 44);
            this.klblToolkitVersion.Margin = new System.Windows.Forms.Padding(5);
            this.klblToolkitVersion.Name = "klblToolkitVersion";
            this.klblToolkitVersion.Size = new System.Drawing.Size(793, 20);
            this.klblToolkitVersion.TabIndex = 2;
            this.klblToolkitVersion.Values.Text = "kryptonLabel2";
            // 
            // klblToolkitBuildDate
            // 
            this.klblToolkitBuildDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblToolkitBuildDate.Location = new System.Drawing.Point(143, 74);
            this.klblToolkitBuildDate.Margin = new System.Windows.Forms.Padding(5);
            this.klblToolkitBuildDate.Name = "klblToolkitBuildDate";
            this.klblToolkitBuildDate.Size = new System.Drawing.Size(793, 20);
            this.klblToolkitBuildDate.TabIndex = 3;
            this.klblToolkitBuildDate.Values.Text = "kryptonLabel3";
            // 
            // klblToolkitCopyright
            // 
            this.klblToolkitCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblToolkitCopyright.Location = new System.Drawing.Point(143, 104);
            this.klblToolkitCopyright.Margin = new System.Windows.Forms.Padding(5);
            this.klblToolkitCopyright.Name = "klblToolkitCopyright";
            this.klblToolkitCopyright.Size = new System.Drawing.Size(793, 29);
            this.klblToolkitCopyright.TabIndex = 4;
            this.klblToolkitCopyright.Values.Text = "kryptonLabel4";
            // 
            // krtbLicense
            // 
            this.krtbLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbLicense.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.krtbLicense.Location = new System.Drawing.Point(143, 143);
            this.krtbLicense.Margin = new System.Windows.Forms.Padding(5);
            this.krtbLicense.Name = "krtbLicense";
            this.krtbLicense.ReadOnly = true;
            this.krtbLicense.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedHorizontal;
            this.krtbLicense.Size = new System.Drawing.Size(793, 284);
            this.krtbLicense.TabIndex = 5;
            this.krtbLicense.Text = "kryptonRichTextBox1";
            // 
            // VisualToolkitInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 482);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToolkitInformationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Krypton Toolkit";
            this.Load += new System.EventHandler(this.VisualToolkitInformationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private KryptonPanel kryptonPanel2;
        private KryptonButton kbtnOk;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonButton kbtnSystemInformation;
        private KryptonPanel kryptonPanel1;
        private TableLayoutPanel tlpContent;
        private PictureBox pbxLogo;
        private KryptonLabel klblHeading;
        private KryptonLabel klblToolkitVersion;
        private KryptonLabel klblToolkitBuildDate;
        private KryptonLabel klblToolkitCopyright;
        private KryptonRichTextBox krtbLicense;
        private KryptonButton kbtnMoreDetails;
    }
}