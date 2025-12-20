namespace TestForm
{
    partial class RibbonMergerDemo
    {
        private System.ComponentModel.IContainer components = null;
        private Krypton.Ribbon.KryptonRibbon mainRibbon;
        private Krypton.Toolkit.KryptonPanel contentPanel;
        private Krypton.Toolkit.KryptonPanel controlPanel;
        private Krypton.Toolkit.KryptonLabel lblStatus;
        private Krypton.Toolkit.KryptonLabel lblPlugins;
        private Krypton.Toolkit.KryptonButton btnLoadImageEditor;
        private Krypton.Toolkit.KryptonButton btnUnloadImageEditor;
        private Krypton.Toolkit.KryptonButton btnLoadTextTools;
        private Krypton.Toolkit.KryptonButton btnUnloadTextTools;
        private Krypton.Toolkit.KryptonButton btnLoadDataManager;
        private Krypton.Toolkit.KryptonButton btnUnloadDataManager;
        private Krypton.Toolkit.KryptonButton btnUnloadAll;
        private Krypton.Toolkit.KryptonLabel lblLog;
        private Krypton.Toolkit.KryptonRichTextBox logTextBox;
        private Krypton.Toolkit.KryptonButton btnClearLog;
        private Krypton.Toolkit.KryptonSplitContainer splitContainer;

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
            this.mainRibbon = new Krypton.Ribbon.KryptonRibbon();
            this.splitContainer = new Krypton.Toolkit.KryptonSplitContainer();
            this.contentPanel = new Krypton.Toolkit.KryptonPanel();
            this.controlPanel = new Krypton.Toolkit.KryptonPanel();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            this.btnUnloadAll = new Krypton.Toolkit.KryptonButton();
            this.btnClearLog = new Krypton.Toolkit.KryptonButton();
            this.lblLog = new Krypton.Toolkit.KryptonLabel();
            this.logTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.btnUnloadDataManager = new Krypton.Toolkit.KryptonButton();
            this.btnLoadDataManager = new Krypton.Toolkit.KryptonButton();
            this.btnUnloadTextTools = new Krypton.Toolkit.KryptonButton();
            this.btnLoadTextTools = new Krypton.Toolkit.KryptonButton();
            this.btnUnloadImageEditor = new Krypton.Toolkit.KryptonButton();
            this.btnLoadImageEditor = new Krypton.Toolkit.KryptonButton();
            this.lblPlugins = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contentPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlPanel)).BeginInit();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainRibbon
            // 
            this.mainRibbon.AutoSize = true;
            this.mainRibbon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainRibbon.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainRibbon.Name = "mainRibbon";
            this.mainRibbon.Size = new System.Drawing.Size(1200, 150);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 150);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.contentPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.controlPanel);
            this.splitContainer.Panel2MinSize = 200;
            this.splitContainer.Size = new System.Drawing.Size(1200, 450);
            this.splitContainer.SplitterDistance = 250;
            this.splitContainer.TabIndex = 1;
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1200, 250);
            this.contentPanel.TabIndex = 0;
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.lblStatus);
            this.controlPanel.Controls.Add(this.btnUnloadAll);
            this.controlPanel.Controls.Add(this.btnClearLog);
            this.controlPanel.Controls.Add(this.lblLog);
            this.controlPanel.Controls.Add(this.logTextBox);
            this.controlPanel.Controls.Add(this.btnUnloadDataManager);
            this.controlPanel.Controls.Add(this.btnLoadDataManager);
            this.controlPanel.Controls.Add(this.btnUnloadTextTools);
            this.controlPanel.Controls.Add(this.btnLoadTextTools);
            this.controlPanel.Controls.Add(this.btnUnloadImageEditor);
            this.controlPanel.Controls.Add(this.btnLoadImageEditor);
            this.controlPanel.Controls.Add(this.lblPlugins);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(1200, 196);
            this.controlPanel.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 23);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Values.Text = "Status: Ready";
            // 
            // btnUnloadAll
            // 
            this.btnUnloadAll.Location = new System.Drawing.Point(330, 12);
            this.btnUnloadAll.Name = "btnUnloadAll";
            this.btnUnloadAll.Size = new System.Drawing.Size(100, 25);
            this.btnUnloadAll.TabIndex = 10;
            this.btnUnloadAll.Values.Text = "Unload All";
            this.btnUnloadAll.Click += new System.EventHandler(this.BtnUnloadAll_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(1100, 12);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(88, 25);
            this.btnClearLog.TabIndex = 9;
            this.btnClearLog.Values.Text = "Clear Log";
            this.btnClearLog.Click += new System.EventHandler(this.BtnClearLog_Click);
            // 
            // lblLog
            // 
            this.lblLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLog.Location = new System.Drawing.Point(450, 12);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(644, 23);
            this.lblLog.TabIndex = 8;
            this.lblLog.Values.Text = "Activity Log:";
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Location = new System.Drawing.Point(450, 41);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(738, 143);
            this.logTextBox.TabIndex = 7;
            this.logTextBox.Text = "";
            // 
            // btnUnloadDataManager
            // 
            this.btnUnloadDataManager.Enabled = false;
            this.btnUnloadDataManager.Location = new System.Drawing.Point(230, 72);
            this.btnUnloadDataManager.Name = "btnUnloadDataManager";
            this.btnUnloadDataManager.Size = new System.Drawing.Size(100, 25);
            this.btnUnloadDataManager.TabIndex = 6;
            this.btnUnloadDataManager.Values.Text = "Unload";
            this.btnUnloadDataManager.Click += new System.EventHandler(this.BtnUnloadDataManager_Click);
            // 
            // btnLoadDataManager
            // 
            this.btnLoadDataManager.Location = new System.Drawing.Point(12, 72);
            this.btnLoadDataManager.Name = "btnLoadDataManager";
            this.btnLoadDataManager.Size = new System.Drawing.Size(212, 25);
            this.btnLoadDataManager.TabIndex = 5;
            this.btnLoadDataManager.Values.Text = "Load Data Manager Plugin";
            this.btnLoadDataManager.Click += new System.EventHandler(this.BtnLoadDataManager_Click);
            // 
            // btnUnloadTextTools
            // 
            this.btnUnloadTextTools.Enabled = false;
            this.btnUnloadTextTools.Location = new System.Drawing.Point(230, 41);
            this.btnUnloadTextTools.Name = "btnUnloadTextTools";
            this.btnUnloadTextTools.Size = new System.Drawing.Size(100, 25);
            this.btnUnloadTextTools.TabIndex = 4;
            this.btnUnloadTextTools.Values.Text = "Unload";
            this.btnUnloadTextTools.Click += new System.EventHandler(this.BtnUnloadTextTools_Click);
            // 
            // btnLoadTextTools
            // 
            this.btnLoadTextTools.Location = new System.Drawing.Point(12, 41);
            this.btnLoadTextTools.Name = "btnLoadTextTools";
            this.btnLoadTextTools.Size = new System.Drawing.Size(212, 25);
            this.btnLoadTextTools.TabIndex = 3;
            this.btnLoadTextTools.Values.Text = "Load Text Tools Plugin";
            this.btnLoadTextTools.Click += new System.EventHandler(this.BtnLoadTextTools_Click);
            // 
            // btnUnloadImageEditor
            // 
            this.btnUnloadImageEditor.Enabled = false;
            this.btnUnloadImageEditor.Location = new System.Drawing.Point(230, 103);
            this.btnUnloadImageEditor.Name = "btnUnloadImageEditor";
            this.btnUnloadImageEditor.Size = new System.Drawing.Size(100, 25);
            this.btnUnloadImageEditor.TabIndex = 2;
            this.btnUnloadImageEditor.Values.Text = "Unload";
            this.btnUnloadImageEditor.Click += new System.EventHandler(this.BtnUnloadImageEditor_Click);
            // 
            // btnLoadImageEditor
            // 
            this.btnLoadImageEditor.Location = new System.Drawing.Point(12, 103);
            this.btnLoadImageEditor.Name = "btnLoadImageEditor";
            this.btnLoadImageEditor.Size = new System.Drawing.Size(212, 25);
            this.btnLoadImageEditor.TabIndex = 1;
            this.btnLoadImageEditor.Values.Text = "Load Image Editor Plugin";
            this.btnLoadImageEditor.Click += new System.EventHandler(this.BtnLoadImageEditor_Click);
            // 
            // lblPlugins
            // 
            this.lblPlugins.Location = new System.Drawing.Point(12, 134);
            this.lblPlugins.Name = "lblPlugins";
            this.lblPlugins.Size = new System.Drawing.Size(318, 50);
            this.lblPlugins.TabIndex = 0;
            this.lblPlugins.Values.Text = "Click buttons above to load/unload plugins.\r\nEach plugin has its own ribbon that\r\nwill be merged into the main ribbon.";
            // 
            // RibbonMergerDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mainRibbon);
            this.Name = "RibbonMergerDemo";
            this.Text = "Ribbon Merger Demo - UserControl Hosting & Plugin Architecture";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).EndInit();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contentPanel)).EndInit();
            this.contentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.controlPanel)).EndInit();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

