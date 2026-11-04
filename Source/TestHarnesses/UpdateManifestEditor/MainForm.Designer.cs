#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace UpdateManifestEditor
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.left = new System.Windows.Forms.TableLayoutPanel();
            this.lstPackages = new System.Windows.Forms.ListBox();
            this.flowPackages = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new Krypton.Toolkit.KryptonButton();
            this.btnRemove = new Krypton.Toolkit.KryptonButton();
            this.editor = new System.Windows.Forms.TableLayoutPanel();
            this.lblHint = new Krypton.Toolkit.KryptonWrapLabel();
            this.lblVersion = new Krypton.Toolkit.KryptonLabel();
            this.txtVersion = new Krypton.Toolkit.KryptonTextBox();
            this.lblSeverity = new Krypton.Toolkit.KryptonLabel();
            this.cmbSeverity = new Krypton.Toolkit.KryptonComboBox();
            this.lblKind = new Krypton.Toolkit.KryptonLabel();
            this.cmbKind = new Krypton.Toolkit.KryptonComboBox();
            this.lblPackageFile = new Krypton.Toolkit.KryptonLabel();
            this.fileRow = new System.Windows.Forms.TableLayoutPanel();
            this.txtPackageFile = new Krypton.Toolkit.KryptonTextBox();
            this.btnBrowse = new Krypton.Toolkit.KryptonButton();
            this.lblPackageUrl = new Krypton.Toolkit.KryptonLabel();
            this.txtPackageUrl = new Krypton.Toolkit.KryptonTextBox();
            this.lblSha256 = new Krypton.Toolkit.KryptonLabel();
            this.hashRow = new System.Windows.Forms.TableLayoutPanel();
            this.txtSha256 = new Krypton.Toolkit.KryptonTextBox();
            this.btnHash = new Krypton.Toolkit.KryptonButton();
            this.lblThumbprint = new Krypton.Toolkit.KryptonLabel();
            this.thumbRow = new System.Windows.Forms.TableLayoutPanel();
            this.txtThumbprint = new Krypton.Toolkit.KryptonTextBox();
            this.btnSignature = new Krypton.Toolkit.KryptonButton();
            this.lblArguments = new Krypton.Toolkit.KryptonLabel();
            this.txtArguments = new Krypton.Toolkit.KryptonTextBox();
            this.lblReleased = new Krypton.Toolkit.KryptonLabel();
            this.releasedRow = new System.Windows.Forms.TableLayoutPanel();
            this.txtReleasedUtc = new Krypton.Toolkit.KryptonTextBox();
            this.btnNow = new Krypton.Toolkit.KryptonButton();
            this.lblMaximumPostponeDays = new Krypton.Toolkit.KryptonLabel();
            this.txtMaximumPostponeDays = new Krypton.Toolkit.KryptonTextBox();
            this.lblAllowPostpone = new Krypton.Toolkit.KryptonLabel();
            this.cmbAllowPostpone = new Krypton.Toolkit.KryptonComboBox();
            this.lblAllowSkip = new Krypton.Toolkit.KryptonLabel();
            this.cmbAllowSkip = new Krypton.Toolkit.KryptonComboBox();
            this.lblAllowResume = new Krypton.Toolkit.KryptonLabel();
            this.cmbAllowResume = new Krypton.Toolkit.KryptonComboBox();
            this.lblAllowRetry = new Krypton.Toolkit.KryptonLabel();
            this.cmbAllowRetry = new Krypton.Toolkit.KryptonComboBox();
            this.lblNotes = new Krypton.Toolkit.KryptonLabel();
            this.txtNotes = new Krypton.Toolkit.KryptonTextBox();
            this.txtPreview = new Krypton.Toolkit.KryptonTextBox();
            this.bottom = new System.Windows.Forms.TableLayoutPanel();
            this.flowCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpen = new Krypton.Toolkit.KryptonButton();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnSaveAs = new Krypton.Toolkit.KryptonButton();
            this.btnCopy = new Krypton.Toolkit.KryptonButton();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.root.SuspendLayout();
            this.left.SuspendLayout();
            this.flowPackages.SuspendLayout();
            this.editor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSeverity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowPostpone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowResume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowRetry)).BeginInit();
            this.fileRow.SuspendLayout();
            this.hashRow.SuspendLayout();
            this.thumbRow.SuspendLayout();
            this.releasedRow.SuspendLayout();
            this.bottom.SuspendLayout();
            this.flowCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.root);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Padding = new System.Windows.Forms.Padding(8);
            this.kryptonPanel.Size = new System.Drawing.Size(980, 680);
            this.kryptonPanel.TabIndex = 0;
            // 
            // root
            // 
            this.root.BackColor = System.Drawing.Color.Transparent;
            this.root.ColumnCount = 2;
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.Controls.Add(this.left, 0, 0);
            this.root.Controls.Add(this.editor, 1, 0);
            this.root.Controls.Add(this.txtPreview, 0, 1);
            this.root.Controls.Add(this.bottom, 0, 2);
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.Location = new System.Drawing.Point(8, 8);
            this.root.Name = "root";
            this.root.RowCount = 3;
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.root.Size = new System.Drawing.Size(964, 664);
            this.root.TabIndex = 0;
            // 
            // left
            // 
            this.left.ColumnCount = 1;
            this.left.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.left.Controls.Add(this.lstPackages, 0, 0);
            this.left.Controls.Add(this.flowPackages, 0, 1);
            this.left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.left.Location = new System.Drawing.Point(3, 3);
            this.left.Name = "left";
            this.left.RowCount = 2;
            this.left.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.left.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.left.Size = new System.Drawing.Size(214, 444);
            this.left.TabIndex = 0;
            // 
            // lstPackages
            // 
            this.lstPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPackages.IntegralHeight = false;
            this.lstPackages.Location = new System.Drawing.Point(3, 3);
            this.lstPackages.Name = "lstPackages";
            this.lstPackages.Size = new System.Drawing.Size(208, 398);
            this.lstPackages.TabIndex = 0;
            this.lstPackages.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // flowPackages
            // 
            this.flowPackages.Controls.Add(this.btnAdd);
            this.flowPackages.Controls.Add(this.btnRemove);
            this.flowPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPackages.Location = new System.Drawing.Point(3, 407);
            this.flowPackages.Name = "flowPackages";
            this.flowPackages.Size = new System.Drawing.Size(208, 34);
            this.flowPackages.TabIndex = 1;
            this.flowPackages.WrapContents = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Values.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.OnAdd);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(99, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(90, 28);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Values.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.OnRemove);
            // 
            // editor
            // 
            this.editor.AutoScroll = true;
            this.editor.ColumnCount = 2;
            this.editor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.editor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.editor.Controls.Add(this.lblHint, 0, 0);
            this.editor.Controls.Add(this.lblVersion, 0, 1);
            this.editor.Controls.Add(this.txtVersion, 1, 1);
            this.editor.Controls.Add(this.lblSeverity, 0, 2);
            this.editor.Controls.Add(this.cmbSeverity, 1, 2);
            this.editor.Controls.Add(this.lblKind, 0, 3);
            this.editor.Controls.Add(this.cmbKind, 1, 3);
            this.editor.Controls.Add(this.lblPackageFile, 0, 4);
            this.editor.Controls.Add(this.fileRow, 1, 4);
            this.editor.Controls.Add(this.lblPackageUrl, 0, 5);
            this.editor.Controls.Add(this.txtPackageUrl, 1, 5);
            this.editor.Controls.Add(this.lblSha256, 0, 6);
            this.editor.Controls.Add(this.hashRow, 1, 6);
            this.editor.Controls.Add(this.lblThumbprint, 0, 7);
            this.editor.Controls.Add(this.thumbRow, 1, 7);
            this.editor.Controls.Add(this.lblArguments, 0, 8);
            this.editor.Controls.Add(this.txtArguments, 1, 8);
            this.editor.Controls.Add(this.lblReleased, 0, 9);
            this.editor.Controls.Add(this.releasedRow, 1, 9);
            this.editor.Controls.Add(this.lblMaximumPostponeDays, 0, 10);
            this.editor.Controls.Add(this.txtMaximumPostponeDays, 1, 10);
            this.editor.Controls.Add(this.lblAllowPostpone, 0, 11);
            this.editor.Controls.Add(this.cmbAllowPostpone, 1, 11);
            this.editor.Controls.Add(this.lblAllowSkip, 0, 12);
            this.editor.Controls.Add(this.cmbAllowSkip, 1, 12);
            this.editor.Controls.Add(this.lblAllowResume, 0, 13);
            this.editor.Controls.Add(this.cmbAllowResume, 1, 13);
            this.editor.Controls.Add(this.lblAllowRetry, 0, 14);
            this.editor.Controls.Add(this.cmbAllowRetry, 1, 14);
            this.editor.Controls.Add(this.lblNotes, 0, 15);
            this.editor.Controls.Add(this.txtNotes, 1, 15);
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.Location = new System.Drawing.Point(223, 3);
            this.editor.Name = "editor";
            this.editor.RowCount = 16;
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.editor.Size = new System.Drawing.Size(738, 444);
            this.editor.TabIndex = 1;
            // 
            // lblHint
            // 
            this.editor.SetColumnSpan(this.lblHint, 2);
            this.lblHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHint.Location = new System.Drawing.Point(3, 0);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(732, 48);
            this.lblHint.Text = "The package file is hashed locally. Package URL is what the feed publishes: use h" +
    "ttps, or a local path for an offline manifest.";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(3, 51);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(51, 20);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Values.Text = "Version";
            // 
            // txtVersion
            // 
            this.txtVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVersion.Location = new System.Drawing.Point(123, 51);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(612, 23);
            this.txtVersion.TabIndex = 2;
            this.txtVersion.TextChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblSeverity
            // 
            this.lblSeverity.Location = new System.Drawing.Point(3, 83);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(53, 20);
            this.lblSeverity.TabIndex = 3;
            this.lblSeverity.Values.Text = "Severity";
            // 
            // cmbSeverity
            // 
            this.cmbSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeverity.DropDownWidth = 160;
            this.cmbSeverity.Location = new System.Drawing.Point(123, 83);
            this.cmbSeverity.Name = "cmbSeverity";
            this.cmbSeverity.Size = new System.Drawing.Size(160, 22);
            this.cmbSeverity.TabIndex = 4;
            this.cmbSeverity.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblKind
            // 
            this.lblKind.Location = new System.Drawing.Point(3, 115);
            this.lblKind.Name = "lblKind";
            this.lblKind.Size = new System.Drawing.Size(82, 20);
            this.lblKind.TabIndex = 5;
            this.lblKind.Values.Text = "Package kind";
            // 
            // cmbKind
            // 
            this.cmbKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKind.DropDownWidth = 160;
            this.cmbKind.Location = new System.Drawing.Point(123, 115);
            this.cmbKind.Name = "cmbKind";
            this.cmbKind.Size = new System.Drawing.Size(160, 22);
            this.cmbKind.TabIndex = 6;
            this.cmbKind.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblPackageFile
            // 
            this.lblPackageFile.Location = new System.Drawing.Point(3, 147);
            this.lblPackageFile.Name = "lblPackageFile";
            this.lblPackageFile.Size = new System.Drawing.Size(75, 20);
            this.lblPackageFile.TabIndex = 7;
            this.lblPackageFile.Values.Text = "Package file";
            // 
            // fileRow
            // 
            this.fileRow.ColumnCount = 2;
            this.fileRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fileRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.fileRow.Controls.Add(this.txtPackageFile, 0, 0);
            this.fileRow.Controls.Add(this.btnBrowse, 1, 0);
            this.fileRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileRow.Location = new System.Drawing.Point(123, 147);
            this.fileRow.Name = "fileRow";
            this.fileRow.RowCount = 1;
            this.fileRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fileRow.Size = new System.Drawing.Size(612, 26);
            this.fileRow.TabIndex = 8;
            // 
            // txtPackageFile
            // 
            this.txtPackageFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPackageFile.Location = new System.Drawing.Point(3, 3);
            this.txtPackageFile.Name = "txtPackageFile";
            this.txtPackageFile.Size = new System.Drawing.Size(516, 23);
            this.txtPackageFile.TabIndex = 0;
            this.txtPackageFile.TextChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowse.Location = new System.Drawing.Point(525, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(84, 20);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Values.Text = "Browse";
            this.btnBrowse.Click += new System.EventHandler(this.OnBrowse);
            // 
            // lblPackageUrl
            // 
            this.lblPackageUrl.Location = new System.Drawing.Point(3, 179);
            this.lblPackageUrl.Name = "lblPackageUrl";
            this.lblPackageUrl.Size = new System.Drawing.Size(81, 20);
            this.lblPackageUrl.TabIndex = 9;
            this.lblPackageUrl.Values.Text = "Package URL";
            // 
            // txtPackageUrl
            // 
            this.txtPackageUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPackageUrl.Location = new System.Drawing.Point(123, 179);
            this.txtPackageUrl.Name = "txtPackageUrl";
            this.txtPackageUrl.Size = new System.Drawing.Size(612, 23);
            this.txtPackageUrl.TabIndex = 10;
            this.txtPackageUrl.TextChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblSha256
            // 
            this.lblSha256.Location = new System.Drawing.Point(3, 211);
            this.lblSha256.Name = "lblSha256";
            this.lblSha256.Size = new System.Drawing.Size(59, 20);
            this.lblSha256.TabIndex = 11;
            this.lblSha256.Values.Text = "SHA-256";
            // 
            // hashRow
            // 
            this.hashRow.ColumnCount = 2;
            this.hashRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.hashRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.hashRow.Controls.Add(this.txtSha256, 0, 0);
            this.hashRow.Controls.Add(this.btnHash, 1, 0);
            this.hashRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hashRow.Location = new System.Drawing.Point(123, 211);
            this.hashRow.Name = "hashRow";
            this.hashRow.RowCount = 1;
            this.hashRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.hashRow.Size = new System.Drawing.Size(612, 26);
            this.hashRow.TabIndex = 12;
            // 
            // txtSha256
            // 
            this.txtSha256.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSha256.Location = new System.Drawing.Point(3, 3);
            this.txtSha256.Name = "txtSha256";
            this.txtSha256.ReadOnly = true;
            this.txtSha256.Size = new System.Drawing.Size(516, 23);
            this.txtSha256.TabIndex = 0;
            // 
            // btnHash
            // 
            this.btnHash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHash.Location = new System.Drawing.Point(525, 3);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(84, 20);
            this.btnHash.TabIndex = 1;
            this.btnHash.Values.Text = "Hash";
            this.btnHash.Click += new System.EventHandler(this.OnHash);
            // 
            // lblThumbprint
            // 
            this.lblThumbprint.Location = new System.Drawing.Point(3, 243);
            this.lblThumbprint.Name = "lblThumbprint";
            this.lblThumbprint.Size = new System.Drawing.Size(75, 20);
            this.lblThumbprint.TabIndex = 13;
            this.lblThumbprint.Values.Text = "Thumbprint";
            // 
            // thumbRow
            // 
            this.thumbRow.ColumnCount = 2;
            this.thumbRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.thumbRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.thumbRow.Controls.Add(this.txtThumbprint, 0, 0);
            this.thumbRow.Controls.Add(this.btnSignature, 1, 0);
            this.thumbRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thumbRow.Location = new System.Drawing.Point(123, 243);
            this.thumbRow.Name = "thumbRow";
            this.thumbRow.RowCount = 1;
            this.thumbRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.thumbRow.Size = new System.Drawing.Size(612, 26);
            this.thumbRow.TabIndex = 14;
            // 
            // txtThumbprint
            // 
            this.txtThumbprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtThumbprint.Location = new System.Drawing.Point(3, 3);
            this.txtThumbprint.Name = "txtThumbprint";
            this.txtThumbprint.ReadOnly = true;
            this.txtThumbprint.Size = new System.Drawing.Size(516, 23);
            this.txtThumbprint.TabIndex = 0;
            // 
            // btnSignature
            // 
            this.btnSignature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSignature.Location = new System.Drawing.Point(525, 3);
            this.btnSignature.Name = "btnSignature";
            this.btnSignature.Size = new System.Drawing.Size(84, 20);
            this.btnSignature.TabIndex = 1;
            this.btnSignature.Values.Text = "Signature";
            this.btnSignature.Click += new System.EventHandler(this.OnSignature);
            // 
            // lblArguments
            // 
            this.lblArguments.Location = new System.Drawing.Point(3, 275);
            this.lblArguments.Name = "lblArguments";
            this.lblArguments.Size = new System.Drawing.Size(71, 20);
            this.lblArguments.TabIndex = 15;
            this.lblArguments.Values.Text = "Arguments";
            // 
            // txtArguments
            // 
            this.txtArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArguments.Location = new System.Drawing.Point(123, 275);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(612, 23);
            this.txtArguments.TabIndex = 16;
            this.txtArguments.TextChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblReleased
            // 
            this.lblReleased.Location = new System.Drawing.Point(3, 307);
            this.lblReleased.Name = "lblReleased";
            this.lblReleased.Size = new System.Drawing.Size(85, 20);
            this.lblReleased.TabIndex = 17;
            this.lblReleased.Values.Text = "Released UTC";
            // 
            // releasedRow
            // 
            this.releasedRow.ColumnCount = 2;
            this.releasedRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.releasedRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.releasedRow.Controls.Add(this.txtReleasedUtc, 0, 0);
            this.releasedRow.Controls.Add(this.btnNow, 1, 0);
            this.releasedRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.releasedRow.Location = new System.Drawing.Point(123, 307);
            this.releasedRow.Name = "releasedRow";
            this.releasedRow.RowCount = 1;
            this.releasedRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.releasedRow.Size = new System.Drawing.Size(612, 26);
            this.releasedRow.TabIndex = 18;
            // 
            // txtReleasedUtc
            // 
            this.txtReleasedUtc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReleasedUtc.Location = new System.Drawing.Point(3, 3);
            this.txtReleasedUtc.Name = "txtReleasedUtc";
            this.txtReleasedUtc.Size = new System.Drawing.Size(516, 23);
            this.txtReleasedUtc.TabIndex = 0;
            this.txtReleasedUtc.TextChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // btnNow
            // 
            this.btnNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNow.Location = new System.Drawing.Point(525, 3);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(84, 20);
            this.btnNow.TabIndex = 1;
            this.btnNow.Values.Text = "Now";
            this.btnNow.Click += new System.EventHandler(this.OnNow);
            // 
            // lblMaximumPostponeDays
            // 
            this.lblMaximumPostponeDays.Location = new System.Drawing.Point(3, 339);
            this.lblMaximumPostponeDays.Name = "lblMaximumPostponeDays";
            this.lblMaximumPostponeDays.Size = new System.Drawing.Size(62, 20);
            this.lblMaximumPostponeDays.TabIndex = 19;
            this.lblMaximumPostponeDays.Values.Text = "Max days";
            // 
            // txtMaximumPostponeDays
            // 
            this.txtMaximumPostponeDays.Location = new System.Drawing.Point(123, 339);
            this.txtMaximumPostponeDays.Name = "txtMaximumPostponeDays";
            this.txtMaximumPostponeDays.Size = new System.Drawing.Size(80, 23);
            this.txtMaximumPostponeDays.TabIndex = 20;
            this.txtMaximumPostponeDays.TextChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblAllowPostpone
            // 
            this.lblAllowPostpone.Location = new System.Drawing.Point(3, 371);
            this.lblAllowPostpone.Name = "lblAllowPostpone";
            this.lblAllowPostpone.Size = new System.Drawing.Size(62, 20);
            this.lblAllowPostpone.TabIndex = 21;
            this.lblAllowPostpone.Values.Text = "Postpone";
            // 
            // cmbAllowPostpone
            // 
            this.cmbAllowPostpone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllowPostpone.DropDownWidth = 160;
            this.cmbAllowPostpone.Location = new System.Drawing.Point(123, 371);
            this.cmbAllowPostpone.Name = "cmbAllowPostpone";
            this.cmbAllowPostpone.Size = new System.Drawing.Size(160, 22);
            this.cmbAllowPostpone.TabIndex = 22;
            this.cmbAllowPostpone.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblAllowSkip
            // 
            this.lblAllowSkip.Location = new System.Drawing.Point(3, 403);
            this.lblAllowSkip.Name = "lblAllowSkip";
            this.lblAllowSkip.Size = new System.Drawing.Size(32, 20);
            this.lblAllowSkip.TabIndex = 23;
            this.lblAllowSkip.Values.Text = "Skip";
            // 
            // cmbAllowSkip
            // 
            this.cmbAllowSkip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllowSkip.DropDownWidth = 160;
            this.cmbAllowSkip.Location = new System.Drawing.Point(123, 403);
            this.cmbAllowSkip.Name = "cmbAllowSkip";
            this.cmbAllowSkip.Size = new System.Drawing.Size(160, 22);
            this.cmbAllowSkip.TabIndex = 24;
            this.cmbAllowSkip.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblAllowResume
            // 
            this.lblAllowResume.Location = new System.Drawing.Point(3, 435);
            this.lblAllowResume.Name = "lblAllowResume";
            this.lblAllowResume.Size = new System.Drawing.Size(52, 20);
            this.lblAllowResume.TabIndex = 25;
            this.lblAllowResume.Values.Text = "Resume";
            // 
            // cmbAllowResume
            // 
            this.cmbAllowResume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllowResume.DropDownWidth = 160;
            this.cmbAllowResume.Location = new System.Drawing.Point(123, 435);
            this.cmbAllowResume.Name = "cmbAllowResume";
            this.cmbAllowResume.Size = new System.Drawing.Size(160, 22);
            this.cmbAllowResume.TabIndex = 26;
            this.cmbAllowResume.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblAllowRetry
            // 
            this.lblAllowRetry.Location = new System.Drawing.Point(3, 467);
            this.lblAllowRetry.Name = "lblAllowRetry";
            this.lblAllowRetry.Size = new System.Drawing.Size(36, 20);
            this.lblAllowRetry.TabIndex = 27;
            this.lblAllowRetry.Values.Text = "Retry";
            // 
            // cmbAllowRetry
            // 
            this.cmbAllowRetry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllowRetry.DropDownWidth = 160;
            this.cmbAllowRetry.Location = new System.Drawing.Point(123, 467);
            this.cmbAllowRetry.Name = "cmbAllowRetry";
            this.cmbAllowRetry.Size = new System.Drawing.Size(160, 22);
            this.cmbAllowRetry.TabIndex = 28;
            this.cmbAllowRetry.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(3, 339);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(85, 20);
            this.lblNotes.TabIndex = 19;
            this.lblNotes.Values.Text = "Release notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Location = new System.Drawing.Point(123, 339);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(612, 102);
            this.txtNotes.TabIndex = 20;
            this.txtNotes.TextChanged += new System.EventHandler(this.OnFieldChanged);
            // 
            // txtPreview
            // 
            this.root.SetColumnSpan(this.txtPreview, 2);
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Location = new System.Drawing.Point(3, 453);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(958, 144);
            this.txtPreview.TabIndex = 2;
            // 
            // bottom
            // 
            this.bottom.ColumnCount = 1;
            this.root.SetColumnSpan(this.bottom, 2);
            this.bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottom.Controls.Add(this.flowCommands, 0, 0);
            this.bottom.Controls.Add(this.lblStatus, 0, 1);
            this.bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottom.Location = new System.Drawing.Point(3, 603);
            this.bottom.Name = "bottom";
            this.bottom.RowCount = 2;
            this.bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottom.Size = new System.Drawing.Size(958, 58);
            this.bottom.TabIndex = 3;
            // 
            // flowCommands
            // 
            this.flowCommands.Controls.Add(this.btnOpen);
            this.flowCommands.Controls.Add(this.btnSave);
            this.flowCommands.Controls.Add(this.btnSaveAs);
            this.flowCommands.Controls.Add(this.btnCopy);
            this.flowCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCommands.Location = new System.Drawing.Point(3, 3);
            this.flowCommands.Name = "flowCommands";
            this.flowCommands.Size = new System.Drawing.Size(952, 30);
            this.flowCommands.TabIndex = 0;
            this.flowCommands.WrapContents = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(3, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 28);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Values.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.OnOpen);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(99, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(195, 3);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(90, 28);
            this.btnSaveAs.TabIndex = 2;
            this.btnSaveAs.Values.Text = "Save as";
            this.btnSaveAs.Click += new System.EventHandler(this.OnSaveAs);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(291, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(110, 28);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Values.Text = "Copy JSON";
            this.btnCopy.Click += new System.EventHandler(this.OnCopy);
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(3, 39);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(952, 16);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Values.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 680);
            this.Controls.Add(this.kryptonPanel);
            this.MinimumSize = new System.Drawing.Size(860, 560);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update manifest editor";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.root.ResumeLayout(false);
            this.root.PerformLayout();
            this.left.ResumeLayout(false);
            this.flowPackages.ResumeLayout(false);
            this.editor.ResumeLayout(false);
            this.editor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSeverity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowPostpone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowResume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAllowRetry)).EndInit();
            this.fileRow.ResumeLayout(false);
            this.fileRow.PerformLayout();
            this.hashRow.ResumeLayout(false);
            this.hashRow.PerformLayout();
            this.thumbRow.ResumeLayout(false);
            this.thumbRow.PerformLayout();
            this.releasedRow.ResumeLayout(false);
            this.releasedRow.PerformLayout();
            this.bottom.ResumeLayout(false);
            this.bottom.PerformLayout();
            this.flowCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.TableLayoutPanel root;
        private System.Windows.Forms.TableLayoutPanel left;
        private System.Windows.Forms.ListBox lstPackages;
        private System.Windows.Forms.FlowLayoutPanel flowPackages;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnRemove;
        private System.Windows.Forms.TableLayoutPanel editor;
        private Krypton.Toolkit.KryptonWrapLabel lblHint;
        private Krypton.Toolkit.KryptonLabel lblVersion;
        private Krypton.Toolkit.KryptonTextBox txtVersion;
        private Krypton.Toolkit.KryptonLabel lblSeverity;
        private Krypton.Toolkit.KryptonComboBox cmbSeverity;
        private Krypton.Toolkit.KryptonLabel lblKind;
        private Krypton.Toolkit.KryptonComboBox cmbKind;
        private Krypton.Toolkit.KryptonLabel lblPackageFile;
        private System.Windows.Forms.TableLayoutPanel fileRow;
        private Krypton.Toolkit.KryptonTextBox txtPackageFile;
        private Krypton.Toolkit.KryptonButton btnBrowse;
        private Krypton.Toolkit.KryptonLabel lblPackageUrl;
        private Krypton.Toolkit.KryptonTextBox txtPackageUrl;
        private Krypton.Toolkit.KryptonLabel lblSha256;
        private System.Windows.Forms.TableLayoutPanel hashRow;
        private Krypton.Toolkit.KryptonTextBox txtSha256;
        private Krypton.Toolkit.KryptonButton btnHash;
        private Krypton.Toolkit.KryptonLabel lblThumbprint;
        private System.Windows.Forms.TableLayoutPanel thumbRow;
        private Krypton.Toolkit.KryptonTextBox txtThumbprint;
        private Krypton.Toolkit.KryptonButton btnSignature;
        private Krypton.Toolkit.KryptonLabel lblArguments;
        private Krypton.Toolkit.KryptonTextBox txtArguments;
        private Krypton.Toolkit.KryptonLabel lblReleased;
        private System.Windows.Forms.TableLayoutPanel releasedRow;
        private Krypton.Toolkit.KryptonTextBox txtReleasedUtc;
        private Krypton.Toolkit.KryptonButton btnNow;
        private Krypton.Toolkit.KryptonLabel lblMaximumPostponeDays;
        private Krypton.Toolkit.KryptonTextBox txtMaximumPostponeDays;
        private Krypton.Toolkit.KryptonLabel lblAllowPostpone;
        private Krypton.Toolkit.KryptonComboBox cmbAllowPostpone;
        private Krypton.Toolkit.KryptonLabel lblAllowSkip;
        private Krypton.Toolkit.KryptonComboBox cmbAllowSkip;
        private Krypton.Toolkit.KryptonLabel lblAllowResume;
        private Krypton.Toolkit.KryptonComboBox cmbAllowResume;
        private Krypton.Toolkit.KryptonLabel lblAllowRetry;
        private Krypton.Toolkit.KryptonComboBox cmbAllowRetry;
        private Krypton.Toolkit.KryptonLabel lblNotes;
        private Krypton.Toolkit.KryptonTextBox txtNotes;
        private Krypton.Toolkit.KryptonTextBox txtPreview;
        private System.Windows.Forms.TableLayoutPanel bottom;
        private System.Windows.Forms.FlowLayoutPanel flowCommands;
        private Krypton.Toolkit.KryptonButton btnOpen;
        private Krypton.Toolkit.KryptonButton btnSave;
        private Krypton.Toolkit.KryptonButton btnSaveAs;
        private Krypton.Toolkit.KryptonButton btnCopy;
        private Krypton.Toolkit.KryptonLabel lblStatus;
    }
}
