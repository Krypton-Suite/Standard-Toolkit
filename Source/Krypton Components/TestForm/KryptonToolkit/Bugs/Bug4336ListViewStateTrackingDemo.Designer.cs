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
    partial class Bug4336ListViewStateTrackingDemo
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
            this.klblView = new Krypton.Toolkit.KryptonLabel();
            this.kcmbView = new Krypton.Toolkit.KryptonComboBox();
            this.kchkHotTracking = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkCheckBoxes = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkItemToolTips = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkContrastTracking = new Krypton.Toolkit.KryptonCheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.kcmbView)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(1100, 640);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1076, 616);
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
            this.kwlblInstructions.Text = "Issue #4336 — KryptonListView hover tracking.\r\n\r\nHover rows on both lists. Native ListView uses Win32 hot-track (underline / Explorer highlight). KryptonListView must use StateTracking (and StateCheckedTracking on a selected row), matching KryptonListBox. Item tooltips: native uses Win32 infotips; Krypton uses KryptonToolTip from ToolTipText. Tick \"Orange StateTracking\" to force an obvious hover colour.";
            //
            // flowToolbar
            //
            this.flowToolbar.AutoSize = true;
            this.flowToolbar.Controls.Add(this.klblTheme);
            this.flowToolbar.Controls.Add(this.kcmbTheme);
            this.flowToolbar.Controls.Add(this.klblView);
            this.flowToolbar.Controls.Add(this.kcmbView);
            this.flowToolbar.Controls.Add(this.kchkHotTracking);
            this.flowToolbar.Controls.Add(this.kchkCheckBoxes);
            this.flowToolbar.Controls.Add(this.kchkItemToolTips);
            this.flowToolbar.Controls.Add(this.kchkContrastTracking);
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
            // klblView
            //
            this.klblView.Location = new System.Drawing.Point(283, 10);
            this.klblView.Name = "klblView";
            this.klblView.Size = new System.Drawing.Size(36, 20);
            this.klblView.TabIndex = 2;
            this.klblView.Values.Text = "View";
            //
            // kcmbView
            //
            this.kcmbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbView.DropDownWidth = 120;
            this.kcmbView.IntegralHeight = false;
            this.kcmbView.Location = new System.Drawing.Point(325, 7);
            this.kcmbView.Name = "kcmbView";
            this.kcmbView.Size = new System.Drawing.Size(120, 22);
            this.kcmbView.TabIndex = 3;
            this.kcmbView.SelectedIndexChanged += new System.EventHandler(this.kcmbView_SelectedIndexChanged);
            //
            // kchkHotTracking
            //
            this.kchkHotTracking.Checked = true;
            this.kchkHotTracking.Location = new System.Drawing.Point(451, 8);
            this.kchkHotTracking.Name = "kchkHotTracking";
            this.kchkHotTracking.Size = new System.Drawing.Size(110, 20);
            this.kchkHotTracking.TabIndex = 4;
            this.kchkHotTracking.Values.Text = "HotTracking";
            this.kchkHotTracking.CheckedChanged += new System.EventHandler(this.kchkHotTracking_CheckedChanged);
            //
            // kchkCheckBoxes
            //
            this.kchkCheckBoxes.Location = new System.Drawing.Point(567, 8);
            this.kchkCheckBoxes.Name = "kchkCheckBoxes";
            this.kchkCheckBoxes.Size = new System.Drawing.Size(100, 20);
            this.kchkCheckBoxes.TabIndex = 5;
            this.kchkCheckBoxes.Values.Text = "CheckBoxes";
            this.kchkCheckBoxes.CheckedChanged += new System.EventHandler(this.kchkCheckBoxes_CheckedChanged);
            //
            // kchkItemToolTips
            //
            this.kchkItemToolTips.Checked = true;
            this.kchkItemToolTips.Location = new System.Drawing.Point(673, 8);
            this.kchkItemToolTips.Name = "kchkItemToolTips";
            this.kchkItemToolTips.Size = new System.Drawing.Size(130, 20);
            this.kchkItemToolTips.TabIndex = 6;
            this.kchkItemToolTips.Values.Text = "Item tooltips";
            this.kchkItemToolTips.CheckedChanged += new System.EventHandler(this.kchkItemToolTips_CheckedChanged);
            //
            // kchkContrastTracking
            //
            this.kchkContrastTracking.Location = new System.Drawing.Point(809, 8);
            this.kchkContrastTracking.Name = "kchkContrastTracking";
            this.kchkContrastTracking.Size = new System.Drawing.Size(170, 20);
            this.kchkContrastTracking.TabIndex = 7;
            this.kchkContrastTracking.Values.Text = "Orange StateTracking";
            this.kchkContrastTracking.CheckedChanged += new System.EventHandler(this.kchkContrastTracking_CheckedChanged);
            //
            // klblNativeCaption
            //
            this.klblNativeCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblNativeCaption.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblNativeCaption.Location = new System.Drawing.Point(3, 137);
            this.klblNativeCaption.Name = "klblNativeCaption";
            this.klblNativeCaption.Size = new System.Drawing.Size(532, 18);
            this.klblNativeCaption.TabIndex = 2;
            this.klblNativeCaption.Values.Text = "Native ListView (Win32 hot-track)";
            //
            // klblKryptonCaption
            //
            this.klblKryptonCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblKryptonCaption.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblKryptonCaption.Location = new System.Drawing.Point(541, 137);
            this.klblKryptonCaption.Name = "klblKryptonCaption";
            this.klblKryptonCaption.Size = new System.Drawing.Size(532, 18);
            this.klblKryptonCaption.TabIndex = 3;
            this.klblKryptonCaption.Values.Text = "KryptonListView (StateTracking)";
            //
            // lvNative
            //
            this.lvNative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNative.FullRowSelect = true;
            this.lvNative.GridLines = true;
            this.lvNative.HideSelection = false;
            this.lvNative.HotTracking = true;
            this.lvNative.Location = new System.Drawing.Point(3, 165);
            this.lvNative.Name = "lvNative";
            this.lvNative.Size = new System.Drawing.Size(532, 416);
            this.lvNative.TabIndex = 4;
            this.lvNative.UseCompatibleStateImageBehavior = false;
            this.lvNative.View = System.Windows.Forms.View.Details;
            //
            // klvKrypton
            //
            this.klvKrypton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klvKrypton.FullRowSelect = true;
            this.klvKrypton.GridLines = true;
            this.klvKrypton.HideSelection = false;
            this.klvKrypton.Location = new System.Drawing.Point(541, 165);
            this.klvKrypton.Name = "klvKrypton";
            this.klvKrypton.Size = new System.Drawing.Size(532, 416);
            this.klvKrypton.TabIndex = 5;
            this.klvKrypton.View = System.Windows.Forms.View.Details;
            this.klvKrypton.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.klvKrypton_ItemSelectionChanged);
            //
            // klblStatus
            //
            this.klblStatus.AutoSize = false;
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 587);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(1070, 26);
            this.klblStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.DimGray;
            this.klblStatus.TabIndex = 6;
            this.klblStatus.Values.Text = "Status";
            //
            // Bug4336ListViewStateTrackingDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 640);
            this.Controls.Add(this.kryptonPanelMain);
            this.MinimumSize = new System.Drawing.Size(900, 480);
            this.Name = "Bug4336ListViewStateTrackingDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Issue #4336 — KryptonListView StateTracking";
            this.Load += new System.EventHandler(this.Bug4336ListViewStateTrackingDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kcmbView)).EndInit();
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
        private Krypton.Toolkit.KryptonLabel klblView;
        private Krypton.Toolkit.KryptonComboBox kcmbView;
        private Krypton.Toolkit.KryptonCheckBox kchkHotTracking;
        private Krypton.Toolkit.KryptonCheckBox kchkCheckBoxes;
        private Krypton.Toolkit.KryptonCheckBox kchkItemToolTips;
        private Krypton.Toolkit.KryptonCheckBox kchkContrastTracking;
        private Krypton.Toolkit.KryptonLabel klblNativeCaption;
        private Krypton.Toolkit.KryptonLabel klblKryptonCaption;
        private System.Windows.Forms.ListView lvNative;
        private Krypton.Toolkit.KryptonListView klvKrypton;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}
