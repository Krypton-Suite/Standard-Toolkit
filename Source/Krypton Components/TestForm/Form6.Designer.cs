namespace TestForm
{
    partial class Form6
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
            this.kryptonWorkspace1 = new Krypton.Workspace.KryptonWorkspace();
            this.kryptonWorkspaceCell1 = new Krypton.Workspace.KryptonWorkspaceCell();
            this.kryptonPage1 = new Krypton.Navigator.KryptonPage();
            this.kryptonPage2 = new Krypton.Navigator.KryptonPage();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace1)).BeginInit();
            this.kryptonWorkspace1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).BeginInit();
            this.kryptonWorkspaceCell1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonWorkspace1
            // 
            this.kryptonWorkspace1.ActivePage = this.kryptonPage1;
            this.kryptonWorkspace1.CompactFlags = ((Krypton.Workspace.CompactFlags)((((Krypton.Workspace.CompactFlags.RemoveEmptyCells | Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | Krypton.Workspace.CompactFlags.PromoteLeafs) 
            | Krypton.Workspace.CompactFlags.AtLeastOneVisibleCell)));
            this.kryptonWorkspace1.ContainerBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.kryptonWorkspace1.Name = "kryptonWorkspace1";
            // 
            // 
            // 
            this.kryptonWorkspace1.Root.Children.AddRange(new System.ComponentModel.Component[] {
            this.kryptonWorkspaceCell1});
            this.kryptonWorkspace1.Root.UniqueName = "8ac2f2d4f2c042f1a35aa98139c3004f";
            this.kryptonWorkspace1.Root.WorkspaceControl = this.kryptonWorkspace1;
            this.kryptonWorkspace1.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.LowProfile;
            this.kryptonWorkspace1.Size = new System.Drawing.Size(250, 250);
            this.kryptonWorkspace1.SplitterWidth = 5;
            this.kryptonWorkspace1.TabIndex = 1;
            this.kryptonWorkspace1.TabStop = true;
            // 
            // kryptonWorkspaceCell1
            // 
            this.kryptonWorkspaceCell1.AllowPageDrag = true;
            this.kryptonWorkspaceCell1.AllowTabFocus = false;
            this.kryptonWorkspaceCell1.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.Context;
            this.kryptonWorkspaceCell1.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.RemovePageAndDispose;
            this.kryptonWorkspaceCell1.Button.CloseButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonWorkspaceCell1.Button.ContextButtonAction = Krypton.Navigator.ContextButtonAction.SelectPage;
            this.kryptonWorkspaceCell1.Button.ContextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonWorkspaceCell1.Button.ContextMenuMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.kryptonWorkspaceCell1.Button.ContextMenuMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.kryptonWorkspaceCell1.Button.NextButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.kryptonWorkspaceCell1.Button.NextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonWorkspaceCell1.Button.PreviousButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.kryptonWorkspaceCell1.Button.PreviousButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonWorkspaceCell1.ControlKryptonFormFeatures = false;
            this.kryptonWorkspaceCell1.Name = "kryptonWorkspaceCell1";
            this.kryptonWorkspaceCell1.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.kryptonWorkspaceCell1.Owner = null;
            this.kryptonWorkspaceCell1.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonWorkspaceCell1.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2});
            this.kryptonWorkspaceCell1.SelectedIndex = 0;
            this.kryptonWorkspaceCell1.UniqueName = "b1763473ef7e40baa2dfc9b994da33c9";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(150, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(248, 223);
            this.kryptonPage1.Text = "kryptonPage1";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "aed2e6aebcc7499b982d2814091e4ed1";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(150, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(150, 100);
            this.kryptonPage2.Text = "kryptonPage2";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "3e5b6985a9a147adbe5164524c645131";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonWorkspace1);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.Form6_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace1)).EndInit();
            this.kryptonWorkspace1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).EndInit();
            this.kryptonWorkspaceCell1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Workspace.KryptonWorkspace kryptonWorkspace1;
        private Krypton.Navigator.KryptonPage kryptonPage1;
        private Krypton.Workspace.KryptonWorkspaceCell kryptonWorkspaceCell1;
        private Krypton.Navigator.KryptonPage kryptonPage2;
    }
}