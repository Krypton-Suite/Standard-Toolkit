#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonFolderBrowserDialogDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (disposing)
            {
                kryptonFolderBrowserDialog1.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowStandard = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowKrypton = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            this.ktxtResultSummary = new Krypton.Toolkit.KryptonTextBox();
            this.klblResultSummary = new Krypton.Toolkit.KryptonLabel();
            this.ktxtResultSelectedPath = new Krypton.Toolkit.KryptonTextBox();
            this.klblResultSelectedPath = new Krypton.Toolkit.KryptonLabel();
            this.ktxtDialogResult = new Krypton.Toolkit.KryptonTextBox();
            this.klblDialogResult = new Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.kbtnPresetBranded = new Krypton.Toolkit.KryptonButton();
            this.kbtnPresetTemp = new Krypton.Toolkit.KryptonButton();
            this.kbtnPresetDesktop = new Krypton.Toolkit.KryptonButton();
            this.kbtnPresetMyDocuments = new Krypton.Toolkit.KryptonButton();
            this.kbtnPresetDefault = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.klblInitialDirectoryNote = new Krypton.Toolkit.KryptonLabel();
            this.kcbShowWithOwner = new Krypton.Toolkit.KryptonCheckBox();
            this.kcbUseFormComponent = new Krypton.Toolkit.KryptonCheckBox();
            this.kcbUseCustomIcon = new Krypton.Toolkit.KryptonCheckBox();
            this.kcmbRootFolder = new Krypton.Toolkit.KryptonComboBox();
            this.klblRootFolder = new Krypton.Toolkit.KryptonLabel();
            this.ktxtInitialDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.klblInitialDirectory = new Krypton.Toolkit.KryptonLabel();
            this.ktxtSelectedPath = new Krypton.Toolkit.KryptonTextBox();
            this.klblSelectedPath = new Krypton.Toolkit.KryptonLabel();
            this.ktxtTitle = new Krypton.Toolkit.KryptonTextBox();
            this.klblTitle = new Krypton.Toolkit.KryptonLabel();
            this.klblDescription = new Krypton.Toolkit.KryptonLabel();
            this.kryptonFolderBrowserDialog1 = new Krypton.Toolkit.KryptonFolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            this.kryptonGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbRootFolder)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Controls.Add(this.kbtnReset);
            this.kryptonPanel1.Controls.Add(this.kbtnShowStandard);
            this.kryptonPanel1.Controls.Add(this.kbtnShowKrypton);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 491);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(784, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(682, 13);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(90, 25);
            this.kbtnClose.TabIndex = 3;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kbtnReset
            // 
            this.kbtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnReset.Location = new System.Drawing.Point(586, 13);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(90, 25);
            this.kbtnReset.TabIndex = 2;
            this.kbtnReset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnReset.Values.Text = "Reset";
            this.kbtnReset.Click += new System.EventHandler(this.kbtnReset_Click);
            // 
            // kbtnShowStandard
            // 
            this.kbtnShowStandard.Location = new System.Drawing.Point(178, 13);
            this.kbtnShowStandard.Name = "kbtnShowStandard";
            this.kbtnShowStandard.Size = new System.Drawing.Size(160, 25);
            this.kbtnShowStandard.TabIndex = 1;
            this.kbtnShowStandard.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowStandard.Values.Text = "Show Standard Dialog";
            this.kbtnShowStandard.Click += new System.EventHandler(this.kbtnShowStandard_Click);
            // 
            // kbtnShowKrypton
            // 
            this.kbtnShowKrypton.Location = new System.Drawing.Point(12, 13);
            this.kbtnShowKrypton.Name = "kbtnShowKrypton";
            this.kbtnShowKrypton.Size = new System.Drawing.Size(160, 25);
            this.kbtnShowKrypton.TabIndex = 0;
            this.kbtnShowKrypton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowKrypton.Values.Text = "Show Krypton Dialog";
            this.kbtnShowKrypton.Click += new System.EventHandler(this.kbtnShowKrypton_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 490);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(784, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox3);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel2.Controls.Add(this.klblDescription);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(784, 490);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // kryptonGroupBox3
            // 
            this.kryptonGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox3.Location = new System.Drawing.Point(12, 360);
            this.kryptonGroupBox3.Name = "kryptonGroupBox3";
            this.kryptonGroupBox3.Size = new System.Drawing.Size(760, 120);
            this.kryptonGroupBox3.TabIndex = 3;
            this.kryptonGroupBox3.Values.Heading = "Last Result";
            // 
            // kryptonGroupBox3.Panel
            // 
            this.kryptonGroupBox3.Panel.Controls.Add(this.ktxtResultSummary);
            this.kryptonGroupBox3.Panel.Controls.Add(this.klblResultSummary);
            this.kryptonGroupBox3.Panel.Controls.Add(this.ktxtResultSelectedPath);
            this.kryptonGroupBox3.Panel.Controls.Add(this.klblResultSelectedPath);
            this.kryptonGroupBox3.Panel.Controls.Add(this.ktxtDialogResult);
            this.kryptonGroupBox3.Panel.Controls.Add(this.klblDialogResult);
            // 
            // ktxtResultSummary
            // 
            this.ktxtResultSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtResultSummary.Location = new System.Drawing.Point(118, 74);
            this.ktxtResultSummary.Name = "ktxtResultSummary";
            this.ktxtResultSummary.ReadOnly = true;
            this.ktxtResultSummary.Size = new System.Drawing.Size(626, 23);
            this.ktxtResultSummary.TabIndex = 5;
            this.ktxtResultSummary.Text = "Run a scenario to see the dialog result here.";
            // 
            // klblResultSummary
            // 
            this.klblResultSummary.Location = new System.Drawing.Point(16, 76);
            this.klblResultSummary.Name = "klblResultSummary";
            this.klblResultSummary.Size = new System.Drawing.Size(63, 20);
            this.klblResultSummary.TabIndex = 4;
            this.klblResultSummary.Values.Text = "Summary:";
            // 
            // ktxtResultSelectedPath
            // 
            this.ktxtResultSelectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtResultSelectedPath.Location = new System.Drawing.Point(118, 45);
            this.ktxtResultSelectedPath.Name = "ktxtResultSelectedPath";
            this.ktxtResultSelectedPath.ReadOnly = true;
            this.ktxtResultSelectedPath.Size = new System.Drawing.Size(626, 23);
            this.ktxtResultSelectedPath.TabIndex = 3;
            // 
            // klblResultSelectedPath
            // 
            this.klblResultSelectedPath.Location = new System.Drawing.Point(16, 47);
            this.klblResultSelectedPath.Name = "klblResultSelectedPath";
            this.klblResultSelectedPath.Size = new System.Drawing.Size(89, 20);
            this.klblResultSelectedPath.TabIndex = 2;
            this.klblResultSelectedPath.Values.Text = "Selected Path:";
            // 
            // ktxtDialogResult
            // 
            this.ktxtDialogResult.Location = new System.Drawing.Point(118, 16);
            this.ktxtDialogResult.Name = "ktxtDialogResult";
            this.ktxtDialogResult.ReadOnly = true;
            this.ktxtDialogResult.Size = new System.Drawing.Size(160, 23);
            this.ktxtDialogResult.TabIndex = 1;
            // 
            // klblDialogResult
            // 
            this.klblDialogResult.Location = new System.Drawing.Point(16, 18);
            this.klblDialogResult.Name = "klblDialogResult";
            this.klblDialogResult.Size = new System.Drawing.Size(84, 20);
            this.klblDialogResult.TabIndex = 0;
            this.klblDialogResult.Values.Text = "Dialog Result:";
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox2.Location = new System.Drawing.Point(520, 92);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            this.kryptonGroupBox2.Size = new System.Drawing.Size(252, 258);
            this.kryptonGroupBox2.TabIndex = 2;
            this.kryptonGroupBox2.Values.Heading = "Presets";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnPresetBranded);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnPresetTemp);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnPresetDesktop);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnPresetMyDocuments);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kbtnPresetDefault);
            // 
            // kbtnPresetBranded
            // 
            this.kbtnPresetBranded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnPresetBranded.Location = new System.Drawing.Point(16, 164);
            this.kbtnPresetBranded.Name = "kbtnPresetBranded";
            this.kbtnPresetBranded.Size = new System.Drawing.Size(220, 25);
            this.kbtnPresetBranded.TabIndex = 4;
            this.kbtnPresetBranded.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetBranded.Values.Text = "Branded (title + icon)";
            this.kbtnPresetBranded.Click += new System.EventHandler(this.kbtnPresetBranded_Click);
            // 
            // kbtnPresetTemp
            // 
            this.kbtnPresetTemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnPresetTemp.Location = new System.Drawing.Point(16, 133);
            this.kbtnPresetTemp.Name = "kbtnPresetTemp";
            this.kbtnPresetTemp.Size = new System.Drawing.Size(220, 25);
            this.kbtnPresetTemp.TabIndex = 3;
            this.kbtnPresetTemp.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetTemp.Values.Text = "Start in Temp folder";
            this.kbtnPresetTemp.Click += new System.EventHandler(this.kbtnPresetTemp_Click);
            // 
            // kbtnPresetDesktop
            // 
            this.kbtnPresetDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnPresetDesktop.Location = new System.Drawing.Point(16, 102);
            this.kbtnPresetDesktop.Name = "kbtnPresetDesktop";
            this.kbtnPresetDesktop.Size = new System.Drawing.Size(220, 25);
            this.kbtnPresetDesktop.TabIndex = 2;
            this.kbtnPresetDesktop.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetDesktop.Values.Text = "Desktop root";
            this.kbtnPresetDesktop.Click += new System.EventHandler(this.kbtnPresetDesktop_Click);
            // 
            // kbtnPresetMyDocuments
            // 
            this.kbtnPresetMyDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnPresetMyDocuments.Location = new System.Drawing.Point(16, 71);
            this.kbtnPresetMyDocuments.Name = "kbtnPresetMyDocuments";
            this.kbtnPresetMyDocuments.Size = new System.Drawing.Size(220, 25);
            this.kbtnPresetMyDocuments.TabIndex = 1;
            this.kbtnPresetMyDocuments.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetMyDocuments.Values.Text = "My Documents";
            this.kbtnPresetMyDocuments.Click += new System.EventHandler(this.kbtnPresetMyDocuments_Click);
            // 
            // kbtnPresetDefault
            // 
            this.kbtnPresetDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnPresetDefault.Location = new System.Drawing.Point(16, 16);
            this.kbtnPresetDefault.Name = "kbtnPresetDefault";
            this.kbtnPresetDefault.Size = new System.Drawing.Size(220, 25);
            this.kbtnPresetDefault.TabIndex = 0;
            this.kbtnPresetDefault.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPresetDefault.Values.Text = "Default settings";
            this.kbtnPresetDefault.Click += new System.EventHandler(this.kbtnPresetDefault_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox1.Location = new System.Drawing.Point(12, 92);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            this.kryptonGroupBox1.Size = new System.Drawing.Size(502, 258);
            this.kryptonGroupBox1.TabIndex = 1;
            this.kryptonGroupBox1.Values.Heading = "Dialog Properties";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblInitialDirectoryNote);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kcbShowWithOwner);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kcbUseFormComponent);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kcbUseCustomIcon);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kcmbRootFolder);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblRootFolder);
            this.kryptonGroupBox1.Panel.Controls.Add(this.ktxtInitialDirectory);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblInitialDirectory);
            this.kryptonGroupBox1.Panel.Controls.Add(this.ktxtSelectedPath);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblSelectedPath);
            this.kryptonGroupBox1.Panel.Controls.Add(this.ktxtTitle);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblTitle);
            // 
            // klblInitialDirectoryNote
            // 
            this.klblInitialDirectoryNote.Location = new System.Drawing.Point(118, 134);
            this.klblInitialDirectoryNote.Name = "klblInitialDirectoryNote";
            this.klblInitialDirectoryNote.Size = new System.Drawing.Size(364, 20);
            this.klblInitialDirectoryNote.TabIndex = 11;
            this.klblInitialDirectoryNote.Values.Text = "InitialDirectory is available on .NET 8+; on .NET Framework use SelectedPath.";
            // 
            // kcbShowWithOwner
            // 
            this.kcbShowWithOwner.Location = new System.Drawing.Point(16, 226);
            this.kcbShowWithOwner.Name = "kcbShowWithOwner";
            this.kcbShowWithOwner.Size = new System.Drawing.Size(198, 20);
            this.kcbShowWithOwner.TabIndex = 10;
            this.kcbShowWithOwner.Values.Text = "Show dialog with owner window";
            // 
            // kcbUseFormComponent
            // 
            this.kcbUseFormComponent.Location = new System.Drawing.Point(16, 200);
            this.kcbUseFormComponent.Name = "kcbUseFormComponent";
            this.kcbUseFormComponent.Size = new System.Drawing.Size(286, 20);
            this.kcbUseFormComponent.TabIndex = 9;
            this.kcbUseFormComponent.Values.Text = "Use form component (kryptonFolderBrowserDialog1)";
            // 
            // kcbUseCustomIcon
            // 
            this.kcbUseCustomIcon.Location = new System.Drawing.Point(16, 174);
            this.kcbUseCustomIcon.Name = "kcbUseCustomIcon";
            this.kcbUseCustomIcon.Size = new System.Drawing.Size(286, 20);
            this.kcbUseCustomIcon.TabIndex = 8;
            this.kcbUseCustomIcon.Values.Text = "Use this form icon in the Krypton dialog";
            // 
            // kcmbRootFolder
            // 
            this.kcmbRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kcmbRootFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbRootFolder.DropDownWidth = 360;
            this.kcmbRootFolder.Location = new System.Drawing.Point(118, 102);
            this.kcmbRootFolder.Name = "kcmbRootFolder";
            this.kcmbRootFolder.Size = new System.Drawing.Size(364, 22);
            this.kcmbRootFolder.TabIndex = 7;
            // 
            // klblRootFolder
            // 
            this.klblRootFolder.Location = new System.Drawing.Point(16, 104);
            this.klblRootFolder.Name = "klblRootFolder";
            this.klblRootFolder.Size = new System.Drawing.Size(76, 20);
            this.klblRootFolder.TabIndex = 6;
            this.klblRootFolder.Values.Text = "Root Folder:";
            // 
            // ktxtInitialDirectory
            // 
            this.ktxtInitialDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtInitialDirectory.Location = new System.Drawing.Point(118, 73);
            this.ktxtInitialDirectory.Name = "ktxtInitialDirectory";
            this.ktxtInitialDirectory.Size = new System.Drawing.Size(364, 23);
            this.ktxtInitialDirectory.TabIndex = 5;
            // 
            // klblInitialDirectory
            // 
            this.klblInitialDirectory.Location = new System.Drawing.Point(16, 75);
            this.klblInitialDirectory.Name = "klblInitialDirectory";
            this.klblInitialDirectory.Size = new System.Drawing.Size(96, 20);
            this.klblInitialDirectory.TabIndex = 4;
            this.klblInitialDirectory.Values.Text = "Initial Directory:";
            // 
            // ktxtSelectedPath
            // 
            this.ktxtSelectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtSelectedPath.Location = new System.Drawing.Point(118, 44);
            this.ktxtSelectedPath.Name = "ktxtSelectedPath";
            this.ktxtSelectedPath.Size = new System.Drawing.Size(364, 23);
            this.ktxtSelectedPath.TabIndex = 3;
            // 
            // klblSelectedPath
            // 
            this.klblSelectedPath.Location = new System.Drawing.Point(16, 46);
            this.klblSelectedPath.Name = "klblSelectedPath";
            this.klblSelectedPath.Size = new System.Drawing.Size(89, 20);
            this.klblSelectedPath.TabIndex = 2;
            this.klblSelectedPath.Values.Text = "Selected Path:";
            // 
            // ktxtTitle
            // 
            this.ktxtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtTitle.Location = new System.Drawing.Point(118, 16);
            this.ktxtTitle.Name = "ktxtTitle";
            this.ktxtTitle.Size = new System.Drawing.Size(364, 23);
            this.ktxtTitle.TabIndex = 1;
            this.ktxtTitle.Text = "Select a folder";
            // 
            // klblTitle
            // 
            this.klblTitle.Location = new System.Drawing.Point(16, 18);
            this.klblTitle.Name = "klblTitle";
            this.klblTitle.Size = new System.Drawing.Size(35, 20);
            this.klblTitle.TabIndex = 0;
            this.klblTitle.Values.Text = "Title:";
            // 
            // klblDescription
            // 
            this.klblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblDescription.Location = new System.Drawing.Point(12, 12);
            this.klblDescription.Name = "klblDescription";
            this.klblDescription.Size = new System.Drawing.Size(760, 74);
            this.klblDescription.TabIndex = 0;
            this.klblDescription.Values.Text = "Configure KryptonFolderBrowserDialog properties below, try a preset, then show the Krypton dialog or compare against the standard FolderBrowserDialog. Title, Icon, SelectedPath, RootFolder, and InitialDirectory (.NET 8+) are all exercised here.";
            // 
            // kryptonFolderBrowserDialog1
            // 
            this.kryptonFolderBrowserDialog1.Title = "Select a folder";
            // 
            // KryptonFolderBrowserDialogDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 541);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(800, 580);
            this.Name = "KryptonFolderBrowserDialogDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonFolderBrowserDialog Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            this.kryptonGroupBox3.Panel.PerformLayout();
            this.kryptonGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kcmbRootFolder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnShowKrypton;
        private KryptonButton kbtnShowStandard;
        private KryptonButton kbtnReset;
        private KryptonButton kbtnClose;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private KryptonLabel klblDescription;
        private KryptonGroupBox kryptonGroupBox1;
        private KryptonTextBox ktxtTitle;
        private KryptonLabel klblTitle;
        private KryptonTextBox ktxtSelectedPath;
        private KryptonLabel klblSelectedPath;
        private KryptonTextBox ktxtInitialDirectory;
        private KryptonLabel klblInitialDirectory;
        private KryptonComboBox kcmbRootFolder;
        private KryptonLabel klblRootFolder;
        private KryptonCheckBox kcbUseCustomIcon;
        private KryptonCheckBox kcbUseFormComponent;
        private KryptonCheckBox kcbShowWithOwner;
        private KryptonLabel klblInitialDirectoryNote;
        private KryptonGroupBox kryptonGroupBox2;
        private KryptonButton kbtnPresetDefault;
        private KryptonButton kbtnPresetMyDocuments;
        private KryptonButton kbtnPresetDesktop;
        private KryptonButton kbtnPresetTemp;
        private KryptonButton kbtnPresetBranded;
        private KryptonGroupBox kryptonGroupBox3;
        private KryptonTextBox ktxtDialogResult;
        private KryptonLabel klblDialogResult;
        private KryptonTextBox ktxtResultSelectedPath;
        private KryptonLabel klblResultSelectedPath;
        private KryptonTextBox ktxtResultSummary;
        private KryptonLabel klblResultSummary;
        private KryptonFolderBrowserDialog kryptonFolderBrowserDialog1;
    }
}
