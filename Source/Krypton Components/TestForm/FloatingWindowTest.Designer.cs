namespace TestForm
{
    partial class FloatingWindowTest
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
            this.btnClearAll = new Krypton.Toolkit.KryptonButton();
            this.btnVerifyFloatspaceControl = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBoxTests = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.btnTestIssue2721Scenario = new Krypton.Toolkit.KryptonButton();
            this.btnTestFloatingWindowWithMultiplePages = new Krypton.Toolkit.KryptonButton();
            this.btnTestMultipleFloatingWindows = new Krypton.Toolkit.KryptonButton();
            this.btnTestSingleFloatingWindow = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBoxStatus = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonTextBoxStatus = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).BeginInit();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxTests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxTests.Panel)).BeginInit();
            this.kryptonGroupBoxTests.Panel.SuspendLayout();
            this.kryptonGroupBoxTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
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
            this.panelControls.Controls.Add(this.btnClearAll);
            this.panelControls.Controls.Add(this.btnVerifyFloatspaceControl);
            this.panelControls.Controls.Add(this.kryptonGroupBoxTests);
            this.panelControls.Controls.Add(this.kryptonGroupBoxStatus);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(650, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(250, 606);
            this.panelControls.TabIndex = 2;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearAll.Location = new System.Drawing.Point(10, 564);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(230, 30);
            this.btnClearAll.TabIndex = 3;
            this.btnClearAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClearAll.Values.Text = "Clear All";
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // btnVerifyFloatspaceControl
            // 
            this.btnVerifyFloatspaceControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVerifyFloatspaceControl.Location = new System.Drawing.Point(10, 529);
            this.btnVerifyFloatspaceControl.Name = "btnVerifyFloatspaceControl";
            this.btnVerifyFloatspaceControl.Size = new System.Drawing.Size(230, 30);
            this.btnVerifyFloatspaceControl.TabIndex = 2;
            this.btnVerifyFloatspaceControl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnVerifyFloatspaceControl.Values.Text = "Verify FloatspaceControl";
            this.btnVerifyFloatspaceControl.Click += new System.EventHandler(this.BtnVerifyFloatspaceControl_Click);
            // 
            // kryptonGroupBoxTests
            // 
            this.kryptonGroupBoxTests.Location = new System.Drawing.Point(10, 10);
            // 
            // kryptonGroupBoxTests.Panel
            // 
            this.kryptonGroupBoxTests.Panel.Controls.Add(this.kryptonPanel2);
            this.kryptonGroupBoxTests.Size = new System.Drawing.Size(230, 210);
            this.kryptonGroupBoxTests.TabIndex = 1;
            this.kryptonGroupBoxTests.Values.Heading = "Floating Window Tests";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.btnTestIssue2721Scenario);
            this.kryptonPanel2.Controls.Add(this.btnTestFloatingWindowWithMultiplePages);
            this.kryptonPanel2.Controls.Add(this.btnTestMultipleFloatingWindows);
            this.kryptonPanel2.Controls.Add(this.btnTestSingleFloatingWindow);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPanel2.Size = new System.Drawing.Size(226, 186);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // btnTestIssue2721Scenario
            // 
            this.btnTestIssue2721Scenario.Location = new System.Drawing.Point(5, 141);
            this.btnTestIssue2721Scenario.Name = "btnTestIssue2721Scenario";
            this.btnTestIssue2721Scenario.Size = new System.Drawing.Size(216, 30);
            this.btnTestIssue2721Scenario.TabIndex = 3;
            this.btnTestIssue2721Scenario.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTestIssue2721Scenario.Values.Text = "Test Issue #2721 Scenario";
            this.btnTestIssue2721Scenario.Click += new System.EventHandler(this.BtnTestIssue2721Scenario_Click);
            // 
            // btnTestFloatingWindowWithMultiplePages
            // 
            this.btnTestFloatingWindowWithMultiplePages.Location = new System.Drawing.Point(5, 95);
            this.btnTestFloatingWindowWithMultiplePages.Name = "btnTestFloatingWindowWithMultiplePages";
            this.btnTestFloatingWindowWithMultiplePages.Size = new System.Drawing.Size(216, 30);
            this.btnTestFloatingWindowWithMultiplePages.TabIndex = 2;
            this.btnTestFloatingWindowWithMultiplePages.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTestFloatingWindowWithMultiplePages.Values.Text = "Test Multiple Pages in Window";
            this.btnTestFloatingWindowWithMultiplePages.Click += new System.EventHandler(this.BtnTestFloatingWindowWithMultiplePages_Click);
            // 
            // btnTestMultipleFloatingWindows
            // 
            this.btnTestMultipleFloatingWindows.Location = new System.Drawing.Point(5, 49);
            this.btnTestMultipleFloatingWindows.Name = "btnTestMultipleFloatingWindows";
            this.btnTestMultipleFloatingWindows.Size = new System.Drawing.Size(216, 30);
            this.btnTestMultipleFloatingWindows.TabIndex = 1;
            this.btnTestMultipleFloatingWindows.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTestMultipleFloatingWindows.Values.Text = "Test Multiple Windows";
            this.btnTestMultipleFloatingWindows.Click += new System.EventHandler(this.BtnTestMultipleFloatingWindows_Click);
            // 
            // btnTestSingleFloatingWindow
            // 
            this.btnTestSingleFloatingWindow.Location = new System.Drawing.Point(5, 3);
            this.btnTestSingleFloatingWindow.Name = "btnTestSingleFloatingWindow";
            this.btnTestSingleFloatingWindow.Size = new System.Drawing.Size(216, 30);
            this.btnTestSingleFloatingWindow.TabIndex = 0;
            this.btnTestSingleFloatingWindow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTestSingleFloatingWindow.Values.Text = "Test Single Window";
            this.btnTestSingleFloatingWindow.Click += new System.EventHandler(this.BtnTestSingleFloatingWindow_Click);
            // 
            // kryptonGroupBoxStatus
            // 
            this.kryptonGroupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonGroupBoxStatus.Location = new System.Drawing.Point(10, 230);
            this.kryptonGroupBoxStatus.Name = "kryptonGroupBoxStatus";
            // 
            // kryptonGroupBoxStatus.Panel
            // 
            this.kryptonGroupBoxStatus.Panel.Controls.Add(this.kryptonTextBoxStatus);
            this.kryptonGroupBoxStatus.Size = new System.Drawing.Size(230, 293);
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
            this.kryptonTextBoxStatus.Size = new System.Drawing.Size(226, 269);
            this.kryptonTextBoxStatus.TabIndex = 0;
            // 
            // FloatingWindowTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 606);
            this.Controls.Add(this.kryptonDockableWorkspace1);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.panelControls);
            this.Name = "FloatingWindowTest";
            this.Text = "Floating Window Test - Issue #2721";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).EndInit();
            this.panelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxTests.Panel)).EndInit();
            this.kryptonGroupBoxTests.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxTests)).EndInit();
            this.kryptonGroupBoxTests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
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
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxTests;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonButton btnTestSingleFloatingWindow;
        private Krypton.Toolkit.KryptonButton btnTestMultipleFloatingWindows;
        private Krypton.Toolkit.KryptonButton btnTestFloatingWindowWithMultiplePages;
        private Krypton.Toolkit.KryptonButton btnTestIssue2721Scenario;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxStatus;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBoxStatus;
        private Krypton.Toolkit.KryptonButton btnVerifyFloatspaceControl;
        private Krypton.Toolkit.KryptonButton btnClearAll;
    }
}