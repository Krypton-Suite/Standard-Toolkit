namespace Krypton.Toolkit
{
    partial class KryptonAboutToolkitControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.khgMain = new Krypton.Toolkit.KryptonHeaderGroup();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tsControls = new System.Windows.Forms.ToolStrip();
            this.tsbtnGeneralInformation = new System.Windows.Forms.ToolStripButton();
            this.tssDiscord = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDiscord = new System.Windows.Forms.ToolStripButton();
            this.tssDeveloperInformation = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDeveloperInformation = new System.Windows.Forms.ToolStripButton();
            this.tssVersions = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnVersions = new System.Windows.Forms.ToolStripButton();
            this.kpnlVersions = new Krypton.Toolkit.KryptonPanel();
            this.kdgvVersions = new Krypton.Toolkit.KryptonDataGridView();
            this.clmnAssemblyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpnlDeveloperInformation = new Krypton.Toolkit.KryptonPanel();
            this.tlpDeveloperInformation = new System.Windows.Forms.TableLayoutPanel();
            this.klwlblRepositories = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.klwlblDocumentation = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.klwlblDemos = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.kpnlDiscord = new Krypton.Toolkit.KryptonPanel();
            this.klwlblDiscord = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.kpnlGeneralInformation = new Krypton.Toolkit.KryptonPanel();
            this.tlpGeneralInformation = new System.Windows.Forms.TableLayoutPanel();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.klwlblGeneralInformation = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.klblCurrentTheme = new Krypton.Toolkit.KryptonLabel();
            this.ktcmbCurrentTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.khgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khgMain.Panel)).BeginInit();
            this.khgMain.Panel.SuspendLayout();
            this.khgMain.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tsControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlVersions)).BeginInit();
            this.kpnlVersions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDeveloperInformation)).BeginInit();
            this.kpnlDeveloperInformation.SuspendLayout();
            this.tlpDeveloperInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDiscord)).BeginInit();
            this.kpnlDiscord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlGeneralInformation)).BeginInit();
            this.kpnlGeneralInformation.SuspendLayout();
            this.tlpGeneralInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ktcmbCurrentTheme)).BeginInit();
            this.SuspendLayout();
            // 
            // khgMain
            // 
            this.khgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.khgMain.HeaderVisibleSecondary = false;
            this.khgMain.Location = new System.Drawing.Point(0, 0);
            this.khgMain.Name = "khgMain";
            // 
            // khgMain.Panel
            // 
            this.khgMain.Panel.Controls.Add(this.toolStripContainer1);
            this.khgMain.Size = new System.Drawing.Size(709, 371);
            this.khgMain.TabIndex = 0;
            this.khgMain.ValuesPrimary.Image = null;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kpnlGeneralInformation);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kpnlDiscord);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kpnlDeveloperInformation);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kpnlVersions);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(707, 314);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(707, 339);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsControls);
            // 
            // tsControls
            // 
            this.tsControls.Dock = System.Windows.Forms.DockStyle.None;
            this.tsControls.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsControls.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnGeneralInformation,
            this.tssDiscord,
            this.tsbtnDiscord,
            this.tssDeveloperInformation,
            this.tsbtnDeveloperInformation,
            this.tssVersions,
            this.tsbtnVersions});
            this.tsControls.Location = new System.Drawing.Point(3, 0);
            this.tsControls.Name = "tsControls";
            this.tsControls.Size = new System.Drawing.Size(113, 25);
            this.tsControls.TabIndex = 0;
            // 
            // tsbtnGeneralInformation
            // 
            this.tsbtnGeneralInformation.CheckOnClick = true;
            this.tsbtnGeneralInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGeneralInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGeneralInformation.Name = "tsbtnGeneralInformation";
            this.tsbtnGeneralInformation.Size = new System.Drawing.Size(23, 22);
            this.tsbtnGeneralInformation.Text = "toolStripButton1";
            this.tsbtnGeneralInformation.Click += new System.EventHandler(this.tsbtnGeneralInformation_Click);
            // 
            // tssDiscord
            // 
            this.tssDiscord.Name = "tssDiscord";
            this.tssDiscord.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnDiscord
            // 
            this.tsbtnDiscord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDiscord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDiscord.Name = "tsbtnDiscord";
            this.tsbtnDiscord.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDiscord.Text = "toolStripButton2";
            this.tsbtnDiscord.Click += new System.EventHandler(this.tsbtnDiscord_Click);
            // 
            // tssDeveloperInformation
            // 
            this.tssDeveloperInformation.Name = "tssDeveloperInformation";
            this.tssDeveloperInformation.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnDeveloperInformation
            // 
            this.tsbtnDeveloperInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDeveloperInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeveloperInformation.Name = "tsbtnDeveloperInformation";
            this.tsbtnDeveloperInformation.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDeveloperInformation.Text = "toolStripButton3";
            this.tsbtnDeveloperInformation.Click += new System.EventHandler(this.tsbtnDeveloperInformation_Click);
            // 
            // tssVersions
            // 
            this.tssVersions.Name = "tssVersions";
            this.tssVersions.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnVersions
            // 
            this.tsbtnVersions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnVersions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnVersions.Name = "tsbtnVersions";
            this.tsbtnVersions.Size = new System.Drawing.Size(23, 22);
            this.tsbtnVersions.Text = "toolStripButton4";
            this.tsbtnVersions.Click += new System.EventHandler(this.tsbtnVersions_Click);
            // 
            // kpnlVersions
            // 
            this.kpnlVersions.Controls.Add(this.kdgvVersions);
            this.kpnlVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlVersions.Location = new System.Drawing.Point(0, 0);
            this.kpnlVersions.Name = "kpnlVersions";
            this.kpnlVersions.Size = new System.Drawing.Size(707, 314);
            this.kpnlVersions.TabIndex = 0;
            // 
            // kdgvVersions
            // 
            this.kdgvVersions.AllowUserToAddRows = false;
            this.kdgvVersions.AllowUserToDeleteRows = false;
            this.kdgvVersions.AllowUserToOrderColumns = true;
            this.kdgvVersions.AllowUserToResizeColumns = false;
            this.kdgvVersions.AllowUserToResizeRows = false;
            this.kdgvVersions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.kdgvVersions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.kdgvVersions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kdgvVersions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnAssemblyName,
            this.clmnVersion});
            this.kdgvVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdgvVersions.Location = new System.Drawing.Point(0, 0);
            this.kdgvVersions.Name = "kdgvVersions";
            this.kdgvVersions.Size = new System.Drawing.Size(707, 314);
            this.kdgvVersions.TabIndex = 0;
            // 
            // clmnAssemblyName
            // 
            this.clmnAssemblyName.HeaderText = "Assembly Name";
            this.clmnAssemblyName.Name = "clmnAssemblyName";
            this.clmnAssemblyName.Width = 122;
            // 
            // clmnVersion
            // 
            this.clmnVersion.HeaderText = "Version";
            this.clmnVersion.Name = "clmnVersion";
            this.clmnVersion.Width = 74;
            // 
            // kpnlDeveloperInformation
            // 
            this.kpnlDeveloperInformation.Controls.Add(this.tlpDeveloperInformation);
            this.kpnlDeveloperInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlDeveloperInformation.Location = new System.Drawing.Point(0, 0);
            this.kpnlDeveloperInformation.Name = "kpnlDeveloperInformation";
            this.kpnlDeveloperInformation.Size = new System.Drawing.Size(707, 314);
            this.kpnlDeveloperInformation.TabIndex = 1;
            // 
            // tlpDeveloperInformation
            // 
            this.tlpDeveloperInformation.BackColor = System.Drawing.Color.Transparent;
            this.tlpDeveloperInformation.ColumnCount = 1;
            this.tlpDeveloperInformation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDeveloperInformation.Controls.Add(this.klwlblRepositories, 0, 0);
            this.tlpDeveloperInformation.Controls.Add(this.klwlblDocumentation, 0, 1);
            this.tlpDeveloperInformation.Controls.Add(this.klwlblDemos, 0, 2);
            this.tlpDeveloperInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDeveloperInformation.Location = new System.Drawing.Point(0, 0);
            this.tlpDeveloperInformation.Name = "tlpDeveloperInformation";
            this.tlpDeveloperInformation.RowCount = 3;
            this.tlpDeveloperInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpDeveloperInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpDeveloperInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpDeveloperInformation.Size = new System.Drawing.Size(707, 314);
            this.tlpDeveloperInformation.TabIndex = 0;
            // 
            // klwlblRepositories
            // 
            this.klwlblRepositories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klwlblRepositories.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klwlblRepositories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klwlblRepositories.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klwlblRepositories.Location = new System.Drawing.Point(5, 5);
            this.klwlblRepositories.Margin = new System.Windows.Forms.Padding(5);
            this.klwlblRepositories.Name = "klwlblRepositories";
            this.klwlblRepositories.Size = new System.Drawing.Size(697, 94);
            this.klwlblRepositories.Text = "kryptonLinkWrapLabel1";
            this.klwlblRepositories.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.klwlblRepositories.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.klwlblRepositories_LinkClicked);
            // 
            // klwlblDocumentation
            // 
            this.klwlblDocumentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klwlblDocumentation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klwlblDocumentation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klwlblDocumentation.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klwlblDocumentation.Location = new System.Drawing.Point(5, 109);
            this.klwlblDocumentation.Margin = new System.Windows.Forms.Padding(5);
            this.klwlblDocumentation.Name = "klwlblDocumentation";
            this.klwlblDocumentation.Size = new System.Drawing.Size(697, 94);
            this.klwlblDocumentation.Text = "kryptonLinkWrapLabel1";
            this.klwlblDocumentation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.klwlblDocumentation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.klwlblDocumentation_LinkClicked);
            // 
            // klwlblDemos
            // 
            this.klwlblDemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klwlblDemos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klwlblDemos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klwlblDemos.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klwlblDemos.Location = new System.Drawing.Point(5, 213);
            this.klwlblDemos.Margin = new System.Windows.Forms.Padding(5);
            this.klwlblDemos.Name = "klwlblDemos";
            this.klwlblDemos.Size = new System.Drawing.Size(697, 96);
            this.klwlblDemos.Text = "kryptonLinkWrapLabel1";
            this.klwlblDemos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.klwlblDemos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.klwlblDemos_LinkClicked);
            // 
            // kpnlDiscord
            // 
            this.kpnlDiscord.Controls.Add(this.klwlblDiscord);
            this.kpnlDiscord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlDiscord.Location = new System.Drawing.Point(0, 0);
            this.kpnlDiscord.Name = "kpnlDiscord";
            this.kpnlDiscord.Size = new System.Drawing.Size(707, 314);
            this.kpnlDiscord.TabIndex = 2;
            // 
            // klwlblDiscord
            // 
            this.klwlblDiscord.AutoSize = false;
            this.klwlblDiscord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klwlblDiscord.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klwlblDiscord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klwlblDiscord.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klwlblDiscord.Location = new System.Drawing.Point(0, 0);
            this.klwlblDiscord.Name = "klwlblDiscord";
            this.klwlblDiscord.Size = new System.Drawing.Size(707, 314);
            this.klwlblDiscord.Text = "kryptonLinkWrapLabel1";
            this.klwlblDiscord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kpnlGeneralInformation
            // 
            this.kpnlGeneralInformation.Controls.Add(this.tlpGeneralInformation);
            this.kpnlGeneralInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlGeneralInformation.Location = new System.Drawing.Point(0, 0);
            this.kpnlGeneralInformation.Name = "kpnlGeneralInformation";
            this.kpnlGeneralInformation.Size = new System.Drawing.Size(707, 314);
            this.kpnlGeneralInformation.TabIndex = 3;
            // 
            // tlpGeneralInformation
            // 
            this.tlpGeneralInformation.BackColor = System.Drawing.Color.Transparent;
            this.tlpGeneralInformation.ColumnCount = 2;
            this.tlpGeneralInformation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpGeneralInformation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGeneralInformation.Controls.Add(this.pbxLogo, 0, 0);
            this.tlpGeneralInformation.Controls.Add(this.klwlblGeneralInformation, 1, 0);
            this.tlpGeneralInformation.Controls.Add(this.klblCurrentTheme, 1, 1);
            this.tlpGeneralInformation.Controls.Add(this.ktcmbCurrentTheme, 1, 2);
            this.tlpGeneralInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGeneralInformation.Location = new System.Drawing.Point(0, 0);
            this.tlpGeneralInformation.Name = "tlpGeneralInformation";
            this.tlpGeneralInformation.RowCount = 3;
            this.tlpGeneralInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGeneralInformation.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpGeneralInformation.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpGeneralInformation.Size = new System.Drawing.Size(707, 314);
            this.tlpGeneralInformation.TabIndex = 0;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxLogo.Location = new System.Drawing.Point(5, 5);
            this.pbxLogo.Margin = new System.Windows.Forms.Padding(5);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Padding = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this.pbxLogo.Size = new System.Drawing.Size(64, 243);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLogo.TabIndex = 0;
            this.pbxLogo.TabStop = false;
            // 
            // klwlblGeneralInformation
            // 
            this.klwlblGeneralInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klwlblGeneralInformation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klwlblGeneralInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klwlblGeneralInformation.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klwlblGeneralInformation.Location = new System.Drawing.Point(79, 5);
            this.klwlblGeneralInformation.Margin = new System.Windows.Forms.Padding(5);
            this.klwlblGeneralInformation.Name = "klwlblGeneralInformation";
            this.klwlblGeneralInformation.Size = new System.Drawing.Size(623, 243);
            this.klwlblGeneralInformation.Text = "Some of the components used in this application are part of the Krypton Standard " +
    "Toolkit.\r\n\r\nLicense: BSD-3-Clause\r\n\r\nTo learn more, click here.";
            this.klwlblGeneralInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.klwlblGeneralInformation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.klwlblGeneralInformation_LinkClicked);
            // 
            // klblCurrentTheme
            // 
            this.klblCurrentTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblCurrentTheme.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblCurrentTheme.Location = new System.Drawing.Point(79, 258);
            this.klblCurrentTheme.Margin = new System.Windows.Forms.Padding(5);
            this.klblCurrentTheme.Name = "klblCurrentTheme";
            this.klblCurrentTheme.Size = new System.Drawing.Size(623, 20);
            this.klblCurrentTheme.TabIndex = 2;
            this.klblCurrentTheme.Values.Text = "Current Theme:";
            // 
            // ktcmbCurrentTheme
            // 
            this.ktcmbCurrentTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktcmbCurrentTheme.DropDownWidth = 623;
            this.ktcmbCurrentTheme.IntegralHeight = false;
            this.ktcmbCurrentTheme.Location = new System.Drawing.Point(79, 288);
            this.ktcmbCurrentTheme.Margin = new System.Windows.Forms.Padding(5);
            this.ktcmbCurrentTheme.Name = "ktcmbCurrentTheme";
            this.ktcmbCurrentTheme.Size = new System.Drawing.Size(623, 21);
            this.ktcmbCurrentTheme.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ktcmbCurrentTheme.TabIndex = 3;
            // 
            // KryptonAboutToolkitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.khgMain);
            this.Name = "KryptonAboutToolkitControl";
            this.Size = new System.Drawing.Size(709, 371);
            ((System.ComponentModel.ISupportInitialize)(this.khgMain.Panel)).EndInit();
            this.khgMain.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.khgMain)).EndInit();
            this.khgMain.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tsControls.ResumeLayout(false);
            this.tsControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlVersions)).EndInit();
            this.kpnlVersions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kdgvVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDeveloperInformation)).EndInit();
            this.kpnlDeveloperInformation.ResumeLayout(false);
            this.tlpDeveloperInformation.ResumeLayout(false);
            this.tlpDeveloperInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlDiscord)).EndInit();
            this.kpnlDiscord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlGeneralInformation)).EndInit();
            this.kpnlGeneralInformation.ResumeLayout(false);
            this.tlpGeneralInformation.ResumeLayout(false);
            this.tlpGeneralInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ktcmbCurrentTheme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonHeaderGroup khgMain;
        private ToolStripContainer toolStripContainer1;
        private ToolStrip tsControls;
        private ToolStripButton tsbtnGeneralInformation;
        private ToolStripSeparator tssDiscord;
        private ToolStripButton tsbtnDiscord;
        private ToolStripSeparator tssDeveloperInformation;
        private ToolStripButton tsbtnDeveloperInformation;
        private ToolStripSeparator tssVersions;
        private ToolStripButton tsbtnVersions;
        private KryptonPanel kpnlVersions;
        private KryptonDataGridView kdgvVersions;
        private DataGridViewTextBoxColumn clmnAssemblyName;
        private DataGridViewTextBoxColumn clmnVersion;
        private KryptonPanel kpnlDeveloperInformation;
        private TableLayoutPanel tlpDeveloperInformation;
        private KryptonLinkWrapLabel klwlblRepositories;
        private KryptonLinkWrapLabel klwlblDocumentation;
        private KryptonLinkWrapLabel klwlblDemos;
        private KryptonPanel kpnlDiscord;
        private KryptonLinkWrapLabel klwlblDiscord;
        private KryptonPanel kpnlGeneralInformation;
        private TableLayoutPanel tlpGeneralInformation;
        private PictureBox pbxLogo;
        private KryptonLinkWrapLabel klwlblGeneralInformation;
        private KryptonLabel klblCurrentTheme;
        private KryptonThemeComboBox ktcmbCurrentTheme;
    }
}
