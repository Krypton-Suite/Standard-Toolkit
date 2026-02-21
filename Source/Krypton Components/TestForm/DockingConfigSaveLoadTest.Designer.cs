namespace TestForm
{
    partial class DockingConfigSaveLoadTest
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
            this.kryptonDockableWorkspace1 = new Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonDockingManager1 = new Krypton.Docking.KryptonDockingManager();
            this.panelControls = new Krypton.Toolkit.KryptonPanel();
            this.btnTestIssue2516 = new Krypton.Toolkit.KryptonButton();
            this.btnClearAll = new Krypton.Toolkit.KryptonButton();
            this.btnLoadConfig = new Krypton.Toolkit.KryptonButton();
            this.btnSaveConfig = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBoxEdgeDocking = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.btnDockToBottom = new Krypton.Toolkit.KryptonButton();
            this.btnDockToTop = new Krypton.Toolkit.KryptonButton();
            this.btnDockToRight = new Krypton.Toolkit.KryptonButton();
            this.btnDockToLeft = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBoxPageCreation = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.btnCreateGenericPage = new Krypton.Toolkit.KryptonButton();
            this.btnCreateDeviceLogPage = new Krypton.Toolkit.KryptonButton();
            this.btnCreateLoggingPage = new Krypton.Toolkit.KryptonButton();
            this.btnCreateChartPage = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBoxStatus = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonTextBoxStatus = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).BeginInit();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxEdgeDocking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxEdgeDocking.Panel)).BeginInit();
            this.kryptonGroupBoxEdgeDocking.Panel.SuspendLayout();
            this.kryptonGroupBoxEdgeDocking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxPageCreation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxPageCreation.Panel)).BeginInit();
            this.kryptonGroupBoxPageCreation.Panel.SuspendLayout();
            this.kryptonGroupBoxPageCreation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxStatus.Panel)).BeginInit();
            this.kryptonGroupBoxStatus.Panel.SuspendLayout();
            this.kryptonGroupBoxStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(650, 606);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonDockableWorkspace1
            // 
            this.kryptonDockableWorkspace1.ActivePage = null;
            this.kryptonDockableWorkspace1.CompactFlags = ((Krypton.Workspace.CompactFlags)(((Krypton.Workspace.CompactFlags.RemoveEmptyCells | Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | Krypton.Workspace.CompactFlags.PromoteLeafs)));
            this.kryptonDockableWorkspace1.ContainerBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonDockableWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.kryptonDockableWorkspace1.Name = "kryptonDockableWorkspace1";
            // 
            // 
            // 
            this.kryptonDockableWorkspace1.Root.UniqueName = "6a481df63c0d459d96b73538ca8abc63";
            this.kryptonDockableWorkspace1.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.LowProfile;
            this.kryptonDockableWorkspace1.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace1.Size = new System.Drawing.Size(650, 606);
            this.kryptonDockableWorkspace1.SplitterWidth = 5;
            this.kryptonDockableWorkspace1.TabIndex = 1;
            this.kryptonDockableWorkspace1.TabStop = true;
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.btnTestIssue2516);
            this.panelControls.Controls.Add(this.btnClearAll);
            this.panelControls.Controls.Add(this.btnLoadConfig);
            this.panelControls.Controls.Add(this.btnSaveConfig);
            this.panelControls.Controls.Add(this.kryptonGroupBoxEdgeDocking);
            this.panelControls.Controls.Add(this.kryptonGroupBoxPageCreation);
            this.panelControls.Controls.Add(this.kryptonGroupBoxStatus);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(650, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(250, 606);
            this.panelControls.TabIndex = 2;
            // 
            // btnTestIssue2516
            // 
            this.btnTestIssue2516.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestIssue2516.Location = new System.Drawing.Point(10, 564);
            this.btnTestIssue2516.Name = "btnTestIssue2516";
            this.btnTestIssue2516.Size = new System.Drawing.Size(230, 30);
            this.btnTestIssue2516.TabIndex = 6;
            this.btnTestIssue2516.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTestIssue2516.Values.Text = "Test Issue #2516 Scenario";
            this.btnTestIssue2516.Click += new System.EventHandler(this.BtnTestIssue2516_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearAll.Location = new System.Drawing.Point(10, 529);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(230, 30);
            this.btnClearAll.TabIndex = 5;
            this.btnClearAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClearAll.Values.Text = "Clear All Pages";
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Enabled = false;
            this.btnLoadConfig.Location = new System.Drawing.Point(125, 370);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(115, 30);
            this.btnLoadConfig.TabIndex = 4;
            this.btnLoadConfig.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnLoadConfig.Values.Text = "Load Config";
            this.btnLoadConfig.Click += new System.EventHandler(this.BtnLoadConfig_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(10, 370);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(115, 30);
            this.btnSaveConfig.TabIndex = 3;
            this.btnSaveConfig.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveConfig.Values.Text = "Save Config";
            this.btnSaveConfig.Click += new System.EventHandler(this.BtnSaveConfig_Click);
            // 
            // kryptonGroupBoxEdgeDocking
            // 
            this.kryptonGroupBoxEdgeDocking.Location = new System.Drawing.Point(10, 170);
            // 
            // kryptonGroupBoxEdgeDocking.Panel
            // 
            this.kryptonGroupBoxEdgeDocking.Panel.Controls.Add(this.kryptonPanel2);
            this.kryptonGroupBoxEdgeDocking.Size = new System.Drawing.Size(230, 115);
            this.kryptonGroupBoxEdgeDocking.TabIndex = 2;
            this.kryptonGroupBoxEdgeDocking.Values.Heading = "Edge Docking (Issue #2516)";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.btnDockToBottom);
            this.kryptonPanel2.Controls.Add(this.btnDockToTop);
            this.kryptonPanel2.Controls.Add(this.btnDockToRight);
            this.kryptonPanel2.Controls.Add(this.btnDockToLeft);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPanel2.Size = new System.Drawing.Size(226, 91);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // btnDockToBottom
            // 
            this.btnDockToBottom.Location = new System.Drawing.Point(117, 50);
            this.btnDockToBottom.Name = "btnDockToBottom";
            this.btnDockToBottom.Size = new System.Drawing.Size(104, 25);
            this.btnDockToBottom.TabIndex = 3;
            this.btnDockToBottom.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDockToBottom.Values.Text = "Bottom";
            this.btnDockToBottom.Click += new System.EventHandler(this.BtnDockToBottom_Click);
            // 
            // btnDockToTop
            // 
            this.btnDockToTop.Location = new System.Drawing.Point(117, 19);
            this.btnDockToTop.Name = "btnDockToTop";
            this.btnDockToTop.Size = new System.Drawing.Size(104, 25);
            this.btnDockToTop.TabIndex = 2;
            this.btnDockToTop.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDockToTop.Values.Text = "Top";
            this.btnDockToTop.Click += new System.EventHandler(this.BtnDockToTop_Click);
            // 
            // btnDockToRight
            // 
            this.btnDockToRight.Location = new System.Drawing.Point(5, 50);
            this.btnDockToRight.Name = "btnDockToRight";
            this.btnDockToRight.Size = new System.Drawing.Size(104, 25);
            this.btnDockToRight.TabIndex = 1;
            this.btnDockToRight.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDockToRight.Values.Text = "Right";
            this.btnDockToRight.Click += new System.EventHandler(this.BtnDockToRight_Click);
            // 
            // btnDockToLeft
            // 
            this.btnDockToLeft.Location = new System.Drawing.Point(5, 19);
            this.btnDockToLeft.Name = "btnDockToLeft";
            this.btnDockToLeft.Size = new System.Drawing.Size(104, 25);
            this.btnDockToLeft.TabIndex = 0;
            this.btnDockToLeft.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDockToLeft.Values.Text = "Left";
            this.btnDockToLeft.Click += new System.EventHandler(this.BtnDockToLeft_Click);
            // 
            // kryptonGroupBoxPageCreation
            // 
            this.kryptonGroupBoxPageCreation.Location = new System.Drawing.Point(10, 10);
            // 
            // kryptonGroupBoxPageCreation.Panel
            // 
            this.kryptonGroupBoxPageCreation.Panel.Controls.Add(this.kryptonPanel3);
            this.kryptonGroupBoxPageCreation.Size = new System.Drawing.Size(230, 150);
            this.kryptonGroupBoxPageCreation.TabIndex = 1;
            this.kryptonGroupBoxPageCreation.Values.Heading = "Create Pages";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.btnCreateGenericPage);
            this.kryptonPanel3.Controls.Add(this.btnCreateDeviceLogPage);
            this.kryptonPanel3.Controls.Add(this.btnCreateLoggingPage);
            this.kryptonPanel3.Controls.Add(this.btnCreateChartPage);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPanel3.Size = new System.Drawing.Size(226, 126);
            this.kryptonPanel3.TabIndex = 0;
            // 
            // btnCreateGenericPage
            // 
            this.btnCreateGenericPage.Location = new System.Drawing.Point(5, 95);
            this.btnCreateGenericPage.Name = "btnCreateGenericPage";
            this.btnCreateGenericPage.Size = new System.Drawing.Size(216, 25);
            this.btnCreateGenericPage.TabIndex = 3;
            this.btnCreateGenericPage.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCreateGenericPage.Values.Text = "Create Generic Page";
            this.btnCreateGenericPage.Click += new System.EventHandler(this.BtnCreateGenericPage_Click);
            // 
            // btnCreateDeviceLogPage
            // 
            this.btnCreateDeviceLogPage.Location = new System.Drawing.Point(5, 64);
            this.btnCreateDeviceLogPage.Name = "btnCreateDeviceLogPage";
            this.btnCreateDeviceLogPage.Size = new System.Drawing.Size(216, 25);
            this.btnCreateDeviceLogPage.TabIndex = 2;
            this.btnCreateDeviceLogPage.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCreateDeviceLogPage.Values.Text = "Create DeviceLog Page";
            this.btnCreateDeviceLogPage.Click += new System.EventHandler(this.BtnCreateDeviceLogPage_Click);
            // 
            // btnCreateLoggingPage
            // 
            this.btnCreateLoggingPage.Location = new System.Drawing.Point(5, 33);
            this.btnCreateLoggingPage.Name = "btnCreateLoggingPage";
            this.btnCreateLoggingPage.Size = new System.Drawing.Size(216, 25);
            this.btnCreateLoggingPage.TabIndex = 1;
            this.btnCreateLoggingPage.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCreateLoggingPage.Values.Text = "Create Logging Page";
            this.btnCreateLoggingPage.Click += new System.EventHandler(this.BtnCreateLoggingPage_Click);
            // 
            // btnCreateChartPage
            // 
            this.btnCreateChartPage.Location = new System.Drawing.Point(5, 2);
            this.btnCreateChartPage.Name = "btnCreateChartPage";
            this.btnCreateChartPage.Size = new System.Drawing.Size(216, 25);
            this.btnCreateChartPage.TabIndex = 0;
            this.btnCreateChartPage.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCreateChartPage.Values.Text = "Create Chart Page";
            this.btnCreateChartPage.Click += new System.EventHandler(this.BtnCreateChartPage_Click);
            // 
            // kryptonGroupBoxStatus
            // 
            this.kryptonGroupBoxStatus.Location = new System.Drawing.Point(10, 291);
            // 
            // kryptonGroupBoxStatus.Panel
            // 
            this.kryptonGroupBoxStatus.Panel.Controls.Add(this.kryptonTextBoxStatus);
            this.kryptonGroupBoxStatus.Size = new System.Drawing.Size(230, 75);
            this.kryptonGroupBoxStatus.TabIndex = 0;
            this.kryptonGroupBoxStatus.Values.Heading = "Status Log";
            // 
            // kryptonTextBoxStatus
            // 
            this.kryptonTextBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonTextBoxStatus.Location = new System.Drawing.Point(0, 0);
            this.kryptonTextBoxStatus.Multiline = true;
            this.kryptonTextBoxStatus.Name = "kryptonTextBoxStatus";
            this.kryptonTextBoxStatus.ReadOnly = true;
            this.kryptonTextBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.kryptonTextBoxStatus.Size = new System.Drawing.Size(226, 51);
            this.kryptonTextBoxStatus.TabIndex = 0;
            // 
            // DockingConfigSaveLoadTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 606);
            this.Controls.Add(this.kryptonDockableWorkspace1);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.panelControls);
            this.Name = "DockingConfigSaveLoadTest";
            this.Text = "Docking Configuration Save/Load Test - Issue #2516";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).EndInit();
            this.panelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxEdgeDocking.Panel)).EndInit();
            this.kryptonGroupBoxEdgeDocking.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxEdgeDocking)).EndInit();
            this.kryptonGroupBoxEdgeDocking.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxPageCreation.Panel)).EndInit();
            this.kryptonGroupBoxPageCreation.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxPageCreation)).EndInit();
            this.kryptonGroupBoxPageCreation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxStatus.Panel)).EndInit();
            this.kryptonGroupBoxStatus.Panel.ResumeLayout(false);
            this.kryptonGroupBoxStatus.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxStatus)).EndInit();
            this.kryptonGroupBoxStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace1;
        private Krypton.Docking.KryptonDockingManager kryptonDockingManager1;
        private Krypton.Toolkit.KryptonPanel panelControls;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxStatus;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBoxStatus;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxPageCreation;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private Krypton.Toolkit.KryptonButton btnCreateChartPage;
        private Krypton.Toolkit.KryptonButton btnCreateLoggingPage;
        private Krypton.Toolkit.KryptonButton btnCreateDeviceLogPage;
        private Krypton.Toolkit.KryptonButton btnCreateGenericPage;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxEdgeDocking;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonButton btnDockToLeft;
        private Krypton.Toolkit.KryptonButton btnDockToRight;
        private Krypton.Toolkit.KryptonButton btnDockToTop;
        private Krypton.Toolkit.KryptonButton btnDockToBottom;
        private Krypton.Toolkit.KryptonButton btnSaveConfig;
        private Krypton.Toolkit.KryptonButton btnLoadConfig;
        private Krypton.Toolkit.KryptonButton btnClearAll;
        private Krypton.Toolkit.KryptonButton btnTestIssue2516;
    }
}