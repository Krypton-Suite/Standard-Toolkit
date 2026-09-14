namespace TestForm
{
    partial class DockingRedockDemo
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
            this.btnAddMultiple = new Krypton.Toolkit.KryptonButton();
            this.btnAddDocument = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBoxInstructions = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.lblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBoxStatus = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonTextBoxStatus = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).BeginInit();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions.Panel)).BeginInit();
            this.kryptonGroupBoxInstructions.Panel.SuspendLayout();
            this.kryptonGroupBoxInstructions.SuspendLayout();
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
            this.kryptonDockableWorkspace1.Root.UniqueName = "DockingRedockDemo_Root";
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
            this.panelControls.Controls.Add(this.btnAddMultiple);
            this.panelControls.Controls.Add(this.btnAddDocument);
            this.panelControls.Controls.Add(this.kryptonGroupBoxInstructions);
            this.panelControls.Controls.Add(this.kryptonGroupBoxStatus);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(650, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(280, 606);
            this.panelControls.TabIndex = 2;
            //
            // btnClearAll
            //
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearAll.Location = new System.Drawing.Point(10, 564);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(260, 30);
            this.btnClearAll.TabIndex = 4;
            this.btnClearAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClearAll.Values.Text = "Clear All";
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            //
            // btnAddMultiple
            //
            this.btnAddMultiple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddMultiple.Location = new System.Drawing.Point(10, 529);
            this.btnAddMultiple.Name = "btnAddMultiple";
            this.btnAddMultiple.Size = new System.Drawing.Size(260, 30);
            this.btnAddMultiple.TabIndex = 3;
            this.btnAddMultiple.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddMultiple.Values.Text = "Add 3 Documents";
            this.btnAddMultiple.Click += new System.EventHandler(this.BtnAddMultiple_Click);
            //
            // btnAddDocument
            //
            this.btnAddDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDocument.Location = new System.Drawing.Point(10, 494);
            this.btnAddDocument.Name = "btnAddDocument";
            this.btnAddDocument.Size = new System.Drawing.Size(260, 30);
            this.btnAddDocument.TabIndex = 2;
            this.btnAddDocument.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddDocument.Values.Text = "Add Document";
            this.btnAddDocument.Click += new System.EventHandler(this.BtnAddDocument_Click);
            //
            // kryptonGroupBoxInstructions
            //
            this.kryptonGroupBoxInstructions.Location = new System.Drawing.Point(10, 10);
            //
            // kryptonGroupBoxInstructions.Panel
            //
            this.kryptonGroupBoxInstructions.Panel.Controls.Add(this.kryptonPanel2);
            this.kryptonGroupBoxInstructions.Size = new System.Drawing.Size(260, 220);
            this.kryptonGroupBoxInstructions.TabIndex = 0;
            this.kryptonGroupBoxInstructions.Values.Heading = "Issue #2933 – Undock / Redock Demo";
            //
            // kryptonPanel2
            //
            this.kryptonPanel2.Controls.Add(this.lblInstructions);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPanel2.Size = new System.Drawing.Size(256, 196);
            this.kryptonPanel2.TabIndex = 0;
            //
            // lblInstructions
            //
            this.lblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInstructions.Location = new System.Drawing.Point(5, 5);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(246, 186);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblInstructions.StateCommon.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblInstructions.Text = "1. Add one or more documents.\r\n\r\n2. Right-click a document tab and choose \"Float\" to undock it.\r\n\r\n3. Drag the floating window by its title bar back over the workspace and drop to redock.\r\n\r\n4. Expected: The document returns to the workspace and the floating window closes. No empty \"left behind\" window (fix for #2933).";
            //
            // kryptonGroupBoxStatus
            //
            this.kryptonGroupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonGroupBoxStatus.Location = new System.Drawing.Point(10, 240);
            this.kryptonGroupBoxStatus.Name = "kryptonGroupBoxStatus";
            //
            // kryptonGroupBoxStatus.Panel
            //
            this.kryptonGroupBoxStatus.Panel.Controls.Add(this.kryptonTextBoxStatus);
            this.kryptonGroupBoxStatus.Size = new System.Drawing.Size(260, 248);
            this.kryptonGroupBoxStatus.TabIndex = 1;
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
            this.kryptonTextBoxStatus.Size = new System.Drawing.Size(256, 224);
            this.kryptonTextBoxStatus.TabIndex = 0;
            //
            // DockingRedockDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 606);
            this.Controls.Add(this.kryptonDockableWorkspace1);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.panelControls);
            this.Name = "DockingRedockDemo";
            this.Text = "Docking Redock Demo – Issue #2933";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).EndInit();
            this.panelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions.Panel)).EndInit();
            this.kryptonGroupBoxInstructions.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions)).EndInit();
            this.kryptonGroupBoxInstructions.ResumeLayout(false);
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
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxInstructions;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonLabel lblInstructions;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxStatus;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBoxStatus;
        private Krypton.Toolkit.KryptonButton btnAddDocument;
        private Krypton.Toolkit.KryptonButton btnAddMultiple;
        private Krypton.Toolkit.KryptonButton btnClearAll;
    }
}
