#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Feature3847ListViewVirtualModeDemo
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.klblFilter = new Krypton.Toolkit.KryptonLabel();
            this.ktxtFilter = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnFilter = new Krypton.Toolkit.KryptonButton();
            this.klblFind = new Krypton.Toolkit.KryptonLabel();
            this.ktxtFind = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnFind = new Krypton.Toolkit.KryptonButton();
            this.kbtnGrow = new Krypton.Toolkit.KryptonButton();
            this.kbtnShrink = new Krypton.Toolkit.KryptonButton();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.klblNativeCaption = new Krypton.Toolkit.KryptonLabel();
            this.klblKryptonCaption = new Krypton.Toolkit.KryptonLabel();
            this.lvNative = new System.Windows.Forms.ListView();
            this.klvKrypton = new Krypton.Toolkit.KryptonListView();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(1100, 680);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowToolbar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.klblNativeCaption, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.klblKryptonCaption, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lvNative, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.klvKrypton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.klblStatus, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1076, 656);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.kwlblInstructions, 2);
            this.tableLayoutPanel1.SetColumnSpan(this.flowToolbar, 2);
            this.tableLayoutPanel1.SetColumnSpan(this.klblStatus, 2);
            //
            // kwlblInstructions
            //
            this.kwlblInstructions.AutoSize = false;
            this.kwlblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kwlblInstructions.Location = new System.Drawing.Point(3, 0);
            this.kwlblInstructions.Name = "kwlblInstructions";
            this.kwlblInstructions.Size = new System.Drawing.Size(1070, 88);
            this.kwlblInstructions.Text = "Issue #3847 — KryptonListView virtual mode.\r\n\r\nBoth lists use VirtualMode with 10,000 rows (not stored in Items). Scroll, multi-select, Filter, Find (SearchForVirtualItem), Grow/Shrink VirtualListSize, and switch themes. Krypton headers and overlay scrollbars should follow the palette; retrieve count should stay near the visible window, not 10,000.";
            //
            // flowToolbar
            //
            this.flowToolbar.AutoSize = true;
            this.flowToolbar.Controls.Add(this.klblTheme);
            this.flowToolbar.Controls.Add(this.kcmbTheme);
            this.flowToolbar.Controls.Add(this.klblFilter);
            this.flowToolbar.Controls.Add(this.ktxtFilter);
            this.flowToolbar.Controls.Add(this.kbtnFilter);
            this.flowToolbar.Controls.Add(this.klblFind);
            this.flowToolbar.Controls.Add(this.ktxtFind);
            this.flowToolbar.Controls.Add(this.kbtnFind);
            this.flowToolbar.Controls.Add(this.kbtnGrow);
            this.flowToolbar.Controls.Add(this.kbtnShrink);
            this.flowToolbar.Controls.Add(this.kbtnReset);
            this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowToolbar.Location = new System.Drawing.Point(3, 91);
            this.flowToolbar.Name = "flowToolbar";
            this.flowToolbar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flowToolbar.Size = new System.Drawing.Size(1070, 40);
            this.flowToolbar.TabIndex = 1;
            this.flowToolbar.WrapContents = true;
            //
            // klblTheme
            //
            this.klblTheme.Location = new System.Drawing.Point(3, 10);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(48, 20);
            this.klblTheme.TabIndex = 0;
            this.klblTheme.Values.Text = "Theme";
            //
            // kcmbTheme
            //
            this.kcmbTheme.DefaultPalette = Krypton.Toolkit.PaletteMode.Global;
            this.kcmbTheme.DropDownWidth = 220;
            this.kcmbTheme.IntegralHeight = false;
            this.kcmbTheme.Location = new System.Drawing.Point(57, 7);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(220, 22);
            this.kcmbTheme.TabIndex = 1;
            //
            // klblFilter
            //
            this.klblFilter.Location = new System.Drawing.Point(283, 10);
            this.klblFilter.Name = "klblFilter";
            this.klblFilter.Size = new System.Drawing.Size(40, 20);
            this.klblFilter.TabIndex = 2;
            this.klblFilter.Values.Text = "Filter";
            //
            // ktxtFilter
            //
            this.ktxtFilter.Location = new System.Drawing.Point(329, 7);
            this.ktxtFilter.Name = "ktxtFilter";
            this.ktxtFilter.Size = new System.Drawing.Size(120, 23);
            this.ktxtFilter.TabIndex = 3;
            //
            // kbtnFilter
            //
            this.kbtnFilter.Location = new System.Drawing.Point(455, 7);
            this.kbtnFilter.Name = "kbtnFilter";
            this.kbtnFilter.Size = new System.Drawing.Size(70, 28);
            this.kbtnFilter.TabIndex = 4;
            this.kbtnFilter.Values.Text = "Apply";
            this.kbtnFilter.Click += new System.EventHandler(this.kbtnFilter_Click);
            //
            // klblFind
            //
            this.klblFind.Location = new System.Drawing.Point(531, 10);
            this.klblFind.Name = "klblFind";
            this.klblFind.Size = new System.Drawing.Size(32, 20);
            this.klblFind.TabIndex = 5;
            this.klblFind.Values.Text = "Find";
            //
            // ktxtFind
            //
            this.ktxtFind.Location = new System.Drawing.Point(569, 7);
            this.ktxtFind.Name = "ktxtFind";
            this.ktxtFind.Size = new System.Drawing.Size(120, 23);
            this.ktxtFind.TabIndex = 6;
            //
            // kbtnFind
            //
            this.kbtnFind.Location = new System.Drawing.Point(695, 7);
            this.kbtnFind.Name = "kbtnFind";
            this.kbtnFind.Size = new System.Drawing.Size(70, 28);
            this.kbtnFind.TabIndex = 7;
            this.kbtnFind.Values.Text = "Find";
            this.kbtnFind.Click += new System.EventHandler(this.kbtnFind_Click);
            //
            // kbtnGrow
            //
            this.kbtnGrow.Location = new System.Drawing.Point(771, 7);
            this.kbtnGrow.Name = "kbtnGrow";
            this.kbtnGrow.Size = new System.Drawing.Size(80, 28);
            this.kbtnGrow.TabIndex = 8;
            this.kbtnGrow.Values.Text = "Grow +1k";
            this.kbtnGrow.Click += new System.EventHandler(this.kbtnGrow_Click);
            //
            // kbtnShrink
            //
            this.kbtnShrink.Location = new System.Drawing.Point(857, 7);
            this.kbtnShrink.Name = "kbtnShrink";
            this.kbtnShrink.Size = new System.Drawing.Size(80, 28);
            this.kbtnShrink.TabIndex = 9;
            this.kbtnShrink.Values.Text = "Shrink −1k";
            this.kbtnShrink.Click += new System.EventHandler(this.kbtnShrink_Click);
            //
            // kbtnReset
            //
            this.kbtnReset.Location = new System.Drawing.Point(943, 7);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(70, 28);
            this.kbtnReset.TabIndex = 10;
            this.kbtnReset.Values.Text = "Reset";
            this.kbtnReset.Click += new System.EventHandler(this.kbtnReset_Click);
            //
            // klblNativeCaption
            //
            this.klblNativeCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblNativeCaption.Location = new System.Drawing.Point(3, 138);
            this.klblNativeCaption.Name = "klblNativeCaption";
            this.klblNativeCaption.Size = new System.Drawing.Size(532, 24);
            this.klblNativeCaption.TabIndex = 2;
            this.klblNativeCaption.Values.Text = "Native ListView (baseline)";
            //
            // klblKryptonCaption
            //
            this.klblKryptonCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblKryptonCaption.Location = new System.Drawing.Point(541, 138);
            this.klblKryptonCaption.Name = "klblKryptonCaption";
            this.klblKryptonCaption.Size = new System.Drawing.Size(532, 24);
            this.klblKryptonCaption.TabIndex = 3;
            this.klblKryptonCaption.Values.Text = "KryptonListView";
            //
            // lvNative
            //
            this.lvNative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNative.HideSelection = false;
            this.lvNative.Location = new System.Drawing.Point(3, 165);
            this.lvNative.Name = "lvNative";
            this.lvNative.Size = new System.Drawing.Size(532, 450);
            this.lvNative.TabIndex = 4;
            this.lvNative.UseCompatibleStateImageBehavior = false;
            this.lvNative.View = System.Windows.Forms.View.Details;
            //
            // klvKrypton
            //
            this.klvKrypton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klvKrypton.HideSelection = false;
            this.klvKrypton.Location = new System.Drawing.Point(541, 165);
            this.klvKrypton.Name = "klvKrypton";
            this.klvKrypton.Size = new System.Drawing.Size(532, 450);
            this.klvKrypton.TabIndex = 5;
            this.klvKrypton.View = System.Windows.Forms.View.Details;
            //
            // klblStatus
            //
            this.klblStatus.AutoSize = false;
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 621);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(1070, 32);
            this.klblStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.DimGray;
            this.klblStatus.TabIndex = 6;
            this.klblStatus.Values.Text = "Status";
            //
            // Feature3847ListViewVirtualModeDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.kryptonPanelMain);
            this.MinimumSize = new System.Drawing.Size(900, 520);
            this.Name = "Feature3847ListViewVirtualModeDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Issue #3847 — KryptonListView virtual mode";
            this.Load += new System.EventHandler(this.Feature3847ListViewVirtualModeDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowToolbar.ResumeLayout(false);
            this.flowToolbar.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInstructions;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
        private Krypton.Toolkit.KryptonLabel klblFilter;
        private Krypton.Toolkit.KryptonTextBox ktxtFilter;
        private Krypton.Toolkit.KryptonButton kbtnFilter;
        private Krypton.Toolkit.KryptonLabel klblFind;
        private Krypton.Toolkit.KryptonTextBox ktxtFind;
        private Krypton.Toolkit.KryptonButton kbtnFind;
        private Krypton.Toolkit.KryptonButton kbtnGrow;
        private Krypton.Toolkit.KryptonButton kbtnShrink;
        private Krypton.Toolkit.KryptonButton kbtnReset;
        private Krypton.Toolkit.KryptonLabel klblNativeCaption;
        private Krypton.Toolkit.KryptonLabel klblKryptonCaption;
        private System.Windows.Forms.ListView lvNative;
        private Krypton.Toolkit.KryptonListView klvKrypton;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}
