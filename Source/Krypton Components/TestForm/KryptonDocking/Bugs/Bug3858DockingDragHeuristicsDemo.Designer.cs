namespace TestForm
{
    partial class Bug3858DockingDragHeuristicsDemo
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
            this.btnResetLayout = new Krypton.Toolkit.KryptonButton();
            this.btnAutoHideToolbox = new Krypton.Toolkit.KryptonButton();
            this.btnAddDocked = new Krypton.Toolkit.KryptonButton();
            this.btnAddDocument = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBoxFeedback = new Krypton.Toolkit.KryptonGroupBox();
            this.radioSolid = new Krypton.Toolkit.KryptonRadioButton();
            this.radioSquare = new Krypton.Toolkit.KryptonRadioButton();
            this.radioRounded = new Krypton.Toolkit.KryptonRadioButton();
            this.kryptonGroupBoxInstructions = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.lblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBoxStatus = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonTextBoxStatus = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).BeginInit();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxFeedback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxFeedback.Panel)).BeginInit();
            this.kryptonGroupBoxFeedback.Panel.SuspendLayout();
            this.kryptonGroupBoxFeedback.SuspendLayout();
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
            this.kryptonPanel1.Size = new System.Drawing.Size(640, 640);
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
            this.kryptonDockableWorkspace1.Root.UniqueName = "Bug3858_Root";
            this.kryptonDockableWorkspace1.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.LowProfile;
            this.kryptonDockableWorkspace1.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace1.Size = new System.Drawing.Size(640, 640);
            this.kryptonDockableWorkspace1.SplitterWidth = 5;
            this.kryptonDockableWorkspace1.TabIndex = 1;
            this.kryptonDockableWorkspace1.TabStop = true;
            //
            // panelControls
            //
            this.panelControls.Controls.Add(this.btnResetLayout);
            this.panelControls.Controls.Add(this.btnAutoHideToolbox);
            this.panelControls.Controls.Add(this.btnAddDocked);
            this.panelControls.Controls.Add(this.btnAddDocument);
            this.panelControls.Controls.Add(this.kryptonGroupBoxFeedback);
            this.panelControls.Controls.Add(this.kryptonGroupBoxInstructions);
            this.panelControls.Controls.Add(this.kryptonGroupBoxStatus);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(640, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(300, 640);
            this.panelControls.TabIndex = 2;
            //
            // btnResetLayout
            //
            this.btnResetLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetLayout.Location = new System.Drawing.Point(10, 598);
            this.btnResetLayout.Name = "btnResetLayout";
            this.btnResetLayout.Size = new System.Drawing.Size(280, 30);
            this.btnResetLayout.TabIndex = 6;
            this.btnResetLayout.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnResetLayout.Values.Text = "Reset Layout";
            this.btnResetLayout.Click += new System.EventHandler(this.BtnResetLayout_Click);
            //
            // btnAutoHideToolbox
            //
            this.btnAutoHideToolbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAutoHideToolbox.Location = new System.Drawing.Point(10, 563);
            this.btnAutoHideToolbox.Name = "btnAutoHideToolbox";
            this.btnAutoHideToolbox.Size = new System.Drawing.Size(280, 30);
            this.btnAutoHideToolbox.TabIndex = 5;
            this.btnAutoHideToolbox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAutoHideToolbox.Values.Text = "Auto-Hide Toolbox";
            this.btnAutoHideToolbox.Click += new System.EventHandler(this.BtnAutoHideToolbox_Click);
            //
            // btnAddDocked
            //
            this.btnAddDocked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDocked.Location = new System.Drawing.Point(10, 528);
            this.btnAddDocked.Name = "btnAddDocked";
            this.btnAddDocked.Size = new System.Drawing.Size(280, 30);
            this.btnAddDocked.TabIndex = 4;
            this.btnAddDocked.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddDocked.Values.Text = "Add Right Docked Page";
            this.btnAddDocked.Click += new System.EventHandler(this.BtnAddDocked_Click);
            //
            // btnAddDocument
            //
            this.btnAddDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDocument.Location = new System.Drawing.Point(10, 493);
            this.btnAddDocument.Name = "btnAddDocument";
            this.btnAddDocument.Size = new System.Drawing.Size(280, 30);
            this.btnAddDocument.TabIndex = 3;
            this.btnAddDocument.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddDocument.Values.Text = "Add Workspace Document";
            this.btnAddDocument.Click += new System.EventHandler(this.BtnAddDocument_Click);
            //
            // kryptonGroupBoxFeedback
            //
            this.kryptonGroupBoxFeedback.Location = new System.Drawing.Point(10, 250);
            this.kryptonGroupBoxFeedback.Name = "kryptonGroupBoxFeedback";
            //
            // kryptonGroupBoxFeedback.Panel
            //
            this.kryptonGroupBoxFeedback.Panel.Controls.Add(this.radioSolid);
            this.kryptonGroupBoxFeedback.Panel.Controls.Add(this.radioSquare);
            this.kryptonGroupBoxFeedback.Panel.Controls.Add(this.radioRounded);
            this.kryptonGroupBoxFeedback.Size = new System.Drawing.Size(280, 110);
            this.kryptonGroupBoxFeedback.TabIndex = 1;
            this.kryptonGroupBoxFeedback.Values.Heading = "Drag Feedback Mode";
            //
            // radioSolid
            //
            this.radioSolid.Location = new System.Drawing.Point(10, 70);
            this.radioSolid.Name = "radioSolid";
            this.radioSolid.Size = new System.Drawing.Size(250, 20);
            this.radioSolid.TabIndex = 2;
            this.radioSolid.Values.Text = "Block (solid hot rectangles)";
            this.radioSolid.CheckedChanged += new System.EventHandler(this.RadioFeedback_CheckedChanged);
            //
            // radioSquare
            //
            this.radioSquare.Location = new System.Drawing.Point(10, 40);
            this.radioSquare.Name = "radioSquare";
            this.radioSquare.Size = new System.Drawing.Size(250, 20);
            this.radioSquare.TabIndex = 1;
            this.radioSquare.Values.Text = "Square docking indicators";
            this.radioSquare.CheckedChanged += new System.EventHandler(this.RadioFeedback_CheckedChanged);
            //
            // radioRounded
            //
            this.radioRounded.Checked = true;
            this.radioRounded.Location = new System.Drawing.Point(10, 10);
            this.radioRounded.Name = "radioRounded";
            this.radioRounded.Size = new System.Drawing.Size(250, 20);
            this.radioRounded.TabIndex = 0;
            this.radioRounded.Values.Text = "Rounded docking indicators";
            this.radioRounded.CheckedChanged += new System.EventHandler(this.RadioFeedback_CheckedChanged);
            //
            // kryptonGroupBoxInstructions
            //
            this.kryptonGroupBoxInstructions.Location = new System.Drawing.Point(10, 10);
            //
            // kryptonGroupBoxInstructions.Panel
            //
            this.kryptonGroupBoxInstructions.Panel.Controls.Add(this.kryptonPanel2);
            this.kryptonGroupBoxInstructions.Size = new System.Drawing.Size(280, 230);
            this.kryptonGroupBoxInstructions.TabIndex = 0;
            this.kryptonGroupBoxInstructions.Values.Heading = "Issue #3858 – Drag Target Heuristics";
            //
            // kryptonPanel2
            //
            this.kryptonPanel2.Controls.Add(this.lblInstructions);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPanel2.Size = new System.Drawing.Size(276, 206);
            this.kryptonPanel2.TabIndex = 0;
            //
            // lblInstructions
            //
            this.lblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInstructions.Location = new System.Drawing.Point(5, 5);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(266, 196);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblInstructions.StateCommon.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblInstructions.Text = "1. Nested left/right cells are preloaded.\r\n\r\n2. Drag a tab: outer diamonds dock to control edges (higher priority); centre diamond transfers into the cell under the mouse.\r\n\r\n3. Auto-Hide Toolbox, then drag near the strip.\r\n\r\n4. Press Escape mid-drag to cancel.\r\n\r\n5. Switch Rounded / Square / Block feedback and repeat.";
            //
            // kryptonGroupBoxStatus
            //
            this.kryptonGroupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonGroupBoxStatus.Location = new System.Drawing.Point(10, 370);
            this.kryptonGroupBoxStatus.Name = "kryptonGroupBoxStatus";
            //
            // kryptonGroupBoxStatus.Panel
            //
            this.kryptonGroupBoxStatus.Panel.Controls.Add(this.kryptonTextBoxStatus);
            this.kryptonGroupBoxStatus.Size = new System.Drawing.Size(280, 110);
            this.kryptonGroupBoxStatus.TabIndex = 2;
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
            this.kryptonTextBoxStatus.Size = new System.Drawing.Size(276, 86);
            this.kryptonTextBoxStatus.TabIndex = 0;
            //
            // Bug3858DockingDragHeuristicsDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 640);
            this.Controls.Add(this.kryptonDockableWorkspace1);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.panelControls);
            this.Name = "Bug3858DockingDragHeuristicsDemo";
            this.Text = "Bug 3858 – Docking Drag Target Heuristics";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Bug3858DockingDragHeuristicsDemo_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControls)).EndInit();
            this.panelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxFeedback.Panel)).EndInit();
            this.kryptonGroupBoxFeedback.Panel.ResumeLayout(false);
            this.kryptonGroupBoxFeedback.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxFeedback)).EndInit();
            this.kryptonGroupBoxFeedback.ResumeLayout(false);
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
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxFeedback;
        private Krypton.Toolkit.KryptonRadioButton radioRounded;
        private Krypton.Toolkit.KryptonRadioButton radioSquare;
        private Krypton.Toolkit.KryptonRadioButton radioSolid;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxStatus;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBoxStatus;
        private Krypton.Toolkit.KryptonButton btnAddDocument;
        private Krypton.Toolkit.KryptonButton btnAddDocked;
        private Krypton.Toolkit.KryptonButton btnAutoHideToolbox;
        private Krypton.Toolkit.KryptonButton btnResetLayout;
    }
}
