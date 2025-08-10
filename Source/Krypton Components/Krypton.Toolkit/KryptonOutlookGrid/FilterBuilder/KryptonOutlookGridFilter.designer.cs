namespace Krypton.Toolkit
{
    partial class KryptonOutlookGridFilter
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
            this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.FilterItemGroup1 = new Krypton.Toolkit.KryptonOutlookGridFilterItemGroup();
            this.FlowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.arkTableLayoutPanelEx1 = new System.Windows.Forms.TableLayoutPanel();
            this.TpActions = new System.Windows.Forms.TableLayoutPanel();
            this.BtnOk = new Krypton.Toolkit.KryptonButton();
            this.BtnClear = new Krypton.Toolkit.KryptonButton();
            this.BtnCancel = new Krypton.Toolkit.KryptonButton();
            this.PnlMain = new Krypton.Toolkit.KryptonPanel();
            this.newGroup = new Krypton.Toolkit.KryptonOutlookGridFilterItemGroup();
            this.FlowLayoutPanel1.SuspendLayout();
            this.FlowLayoutPanel2.SuspendLayout();
            this.arkTableLayoutPanelEx1.SuspendLayout();
            this.TpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).BeginInit();
            this.PnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FlowLayoutPanel1.AutoSize = true;
            this.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlowLayoutPanel1.Controls.Add(this.FilterItemGroup1);
            this.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(199, 50);
            this.FlowLayoutPanel1.TabIndex = 1;
            // 
            // FilterItemGroup1
            // 
            this.FilterItemGroup1.AutoSize = true;
            this.FilterItemGroup1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FilterItemGroup1.Location = new System.Drawing.Point(3, 3);
            this.FilterItemGroup1.Name = "FilterItemGroup1";
            this.FilterItemGroup1.PrimaryGroup = true;
            this.FilterItemGroup1.Size = new System.Drawing.Size(193, 44);
            this.FilterItemGroup1.TabIndex = 0;
            // 
            // FlowLayoutPanel2
            // 
            this.FlowLayoutPanel2.AutoSize = true;
            this.FlowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.FlowLayoutPanel2.Controls.Add(this.arkTableLayoutPanelEx1);
            this.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.FlowLayoutPanel2.Name = "FlowLayoutPanel2";
            this.FlowLayoutPanel2.Size = new System.Drawing.Size(264, 103);
            this.FlowLayoutPanel2.TabIndex = 3;
            // 
            // arkTableLayoutPanelEx1
            // 
            this.arkTableLayoutPanelEx1.AutoSize = true;
            this.arkTableLayoutPanelEx1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.arkTableLayoutPanelEx1.BackColor = System.Drawing.Color.Transparent;
            this.arkTableLayoutPanelEx1.ColumnCount = 1;
            this.arkTableLayoutPanelEx1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.arkTableLayoutPanelEx1.Controls.Add(this.FlowLayoutPanel1, 0, 0);
            this.arkTableLayoutPanelEx1.Controls.Add(this.TpActions, 0, 1);
            this.arkTableLayoutPanelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arkTableLayoutPanelEx1.Location = new System.Drawing.Point(3, 3);
            this.arkTableLayoutPanelEx1.Name = "arkTableLayoutPanelEx1";
            this.arkTableLayoutPanelEx1.RowCount = 2;
            this.arkTableLayoutPanelEx1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.arkTableLayoutPanelEx1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.arkTableLayoutPanelEx1.Size = new System.Drawing.Size(249, 93);
            this.arkTableLayoutPanelEx1.TabIndex = 4;
            // 
            // TpActions
            // 
            this.TpActions.AutoSize = true;
            this.TpActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TpActions.BackColor = System.Drawing.Color.Transparent;
            this.TpActions.ColumnCount = 4;
            this.TpActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TpActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpActions.Controls.Add(this.BtnOk, 2, 0);
            this.TpActions.Controls.Add(this.BtnClear, 0, 0);
            this.TpActions.Controls.Add(this.BtnCancel, 3, 0);
            this.TpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TpActions.Location = new System.Drawing.Point(3, 59);
            this.TpActions.Name = "TpActions";
            this.TpActions.RowCount = 1;
            this.TpActions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TpActions.Size = new System.Drawing.Size(243, 31);
            this.TpActions.TabIndex = 7;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(84, 3);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 25);
            this.BtnOk.TabIndex = 4;
            this.BtnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.BtnOk.Values.Text = "&Ok";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(3, 3);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 25);
            this.BtnClear.TabIndex = 6;
            this.BtnClear.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.BtnClear.Values.Text = "Clea&r";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(165, 3);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 25);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.BtnCancel.Values.Text = "&Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PnlMain
            // 
            this.PnlMain.AutoSize = true;
            this.PnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PnlMain.Controls.Add(this.FlowLayoutPanel2);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(264, 103);
            this.PnlMain.TabIndex = 4;
            // 
            // newGroup
            // 
            this.newGroup.AutoSize = true;
            this.newGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.newGroup.Location = new System.Drawing.Point(3, 3);
            this.newGroup.Name = "newGroup";
            this.newGroup.PrimaryGroup = false;
            this.newGroup.Size = new System.Drawing.Size(481, 56);
            this.newGroup.TabIndex = 0;
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(264, 103);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Filter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.FlowLayoutPanel1.ResumeLayout(false);
            this.FlowLayoutPanel1.PerformLayout();
            this.FlowLayoutPanel2.ResumeLayout(false);
            this.FlowLayoutPanel2.PerformLayout();
            this.arkTableLayoutPanelEx1.ResumeLayout(false);
            this.arkTableLayoutPanelEx1.PerformLayout();
            this.TpActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).EndInit();
            this.PnlMain.ResumeLayout(false);
            this.PnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
        internal KryptonOutlookGridFilterItemGroup FilterItemGroup1;
        internal KryptonOutlookGridFilterItemGroup newGroup;
        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel2;

        #endregion

        private KryptonPanel PnlMain;
        private TableLayoutPanel arkTableLayoutPanelEx1;
        private TableLayoutPanel TpActions;
        private KryptonButton BtnOk;
        private KryptonButton BtnCancel;
        private KryptonButton BtnClear;
    }
}