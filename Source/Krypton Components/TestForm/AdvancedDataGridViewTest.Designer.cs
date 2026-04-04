namespace TestForm
{
    partial class AdvancedDataGridViewTest
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
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnApplySavedFilters = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearFilters = new Krypton.Toolkit.KryptonButton();
            this.kbtnSaveFilters = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.kcmbSortSaved = new Krypton.Toolkit.KryptonComboBox();
            this.kcmbSavedFilters = new Krypton.Toolkit.KryptonComboBox();
            this.ktxtStringFilter = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtSortString = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtFilterString = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kcmbMemoryTest = new Krypton.Toolkit.KryptonComboBox();
            this.kbtnMemoryTest = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadRandomData = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslMemoryUsage = new System.Windows.Forms.ToolStripStatusLabel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.ktxtTotalRows = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel4 = new Krypton.Toolkit.KryptonPanel();
            this.kadgvMain = new Krypton.Utilities.KryptonAdvancedDataGridView();
            this.kryptonAdvancedDataGridViewSearchToolBar1 = new Krypton.Utilities.KryptonAdvancedDataGridViewSearchToolBar();
            this.bsData = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSortSaved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSavedFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbMemoryTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).BeginInit();
            this.kryptonPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kadgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsData)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnApplySavedFilters);
            this.kryptonPanel1.Controls.Add(this.kbtnClearFilters);
            this.kryptonPanel1.Controls.Add(this.kbtnSaveFilters);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kcmbSortSaved);
            this.kryptonPanel1.Controls.Add(this.kcmbSavedFilters);
            this.kryptonPanel1.Controls.Add(this.ktxtStringFilter);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.ktxtSortString);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.ktxtFilterString);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kcmbMemoryTest);
            this.kryptonPanel1.Controls.Add(this.kbtnMemoryTest);
            this.kryptonPanel1.Controls.Add(this.kbtnLoadRandomData);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1108, 189);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnApplySavedFilters
            // 
            this.kbtnApplySavedFilters.Location = new System.Drawing.Point(1005, 72);
            this.kbtnApplySavedFilters.Name = "kbtnApplySavedFilters";
            this.kbtnApplySavedFilters.Size = new System.Drawing.Size(90, 25);
            this.kbtnApplySavedFilters.TabIndex = 15;
            this.kbtnApplySavedFilters.Values.Text = "Apply";
            this.kbtnApplySavedFilters.Click += new System.EventHandler(this.kbtnApplySavedFilters_Click);
            // 
            // kbtnClearFilters
            // 
            this.kbtnClearFilters.Location = new System.Drawing.Point(669, 45);
            this.kbtnClearFilters.Name = "kbtnClearFilters";
            this.kbtnClearFilters.Size = new System.Drawing.Size(179, 25);
            this.kbtnClearFilters.TabIndex = 14;
            this.kbtnClearFilters.Values.Text = "Clean Filter And Sort";
            this.kbtnClearFilters.Click += new System.EventHandler(this.kbtnClearFilters_Click);
            // 
            // kbtnSaveFilters
            // 
            this.kbtnSaveFilters.Location = new System.Drawing.Point(669, 13);
            this.kbtnSaveFilters.Name = "kbtnSaveFilters";
            this.kbtnSaveFilters.Size = new System.Drawing.Size(180, 25);
            this.kbtnSaveFilters.TabIndex = 13;
            this.kbtnSaveFilters.Values.Text = "Save Current Filter And Sort";
            this.kbtnSaveFilters.Click += new System.EventHandler(this.kbtnSaveFilters_Click);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(860, 40);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel6.TabIndex = 12;
            this.kryptonLabel6.Values.Text = "Sort Saved:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(855, 12);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(76, 20);
            this.kryptonLabel5.TabIndex = 11;
            this.kryptonLabel5.Values.Text = "Filter Saved:";
            // 
            // kcmbSortSaved
            // 
            this.kcmbSortSaved.DropDownWidth = 159;
            this.kcmbSortSaved.IntegralHeight = false;
            this.kcmbSortSaved.Location = new System.Drawing.Point(937, 39);
            this.kcmbSortSaved.Name = "kcmbSortSaved";
            this.kcmbSortSaved.Size = new System.Drawing.Size(159, 21);
            this.kcmbSortSaved.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbSortSaved.TabIndex = 10;
            // 
            // kcmbSavedFilters
            // 
            this.kcmbSavedFilters.DropDownWidth = 159;
            this.kcmbSavedFilters.IntegralHeight = false;
            this.kcmbSavedFilters.Location = new System.Drawing.Point(937, 12);
            this.kcmbSavedFilters.Name = "kcmbSavedFilters";
            this.kcmbSavedFilters.Size = new System.Drawing.Size(159, 21);
            this.kcmbSavedFilters.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbSavedFilters.TabIndex = 9;
            // 
            // ktxtStringFilter
            // 
            this.ktxtStringFilter.Location = new System.Drawing.Point(147, 159);
            this.ktxtStringFilter.Name = "ktxtStringFilter";
            this.ktxtStringFilter.Size = new System.Drawing.Size(100, 23);
            this.ktxtStringFilter.TabIndex = 8;
            this.ktxtStringFilter.TextChanged += new System.EventHandler(this.ktxtStringFilter_TextChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(13, 159);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(128, 20);
            this.kryptonLabel4.TabIndex = 7;
            this.kryptonLabel4.Values.Text = "Filter column \"string\":";
            // 
            // ktxtSortString
            // 
            this.ktxtSortString.Location = new System.Drawing.Point(207, 72);
            this.ktxtSortString.Multiline = true;
            this.ktxtSortString.Name = "ktxtSortString";
            this.ktxtSortString.ReadOnly = true;
            this.ktxtSortString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtSortString.Size = new System.Drawing.Size(180, 80);
            this.ktxtSortString.TabIndex = 6;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(207, 45);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "Sort String:";
            // 
            // ktxtFilterString
            // 
            this.ktxtFilterString.Location = new System.Drawing.Point(13, 72);
            this.ktxtFilterString.Multiline = true;
            this.ktxtFilterString.Name = "ktxtFilterString";
            this.ktxtFilterString.ReadOnly = true;
            this.ktxtFilterString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtFilterString.Size = new System.Drawing.Size(180, 80);
            this.ktxtFilterString.TabIndex = 4;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 45);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(76, 20);
            this.kryptonLabel1.TabIndex = 3;
            this.kryptonLabel1.Values.Text = "Filter String:";
            // 
            // kcmbMemoryTest
            // 
            this.kcmbMemoryTest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbMemoryTest.DropDownWidth = 121;
            this.kcmbMemoryTest.IntegralHeight = false;
            this.kcmbMemoryTest.Location = new System.Drawing.Point(393, 13);
            this.kcmbMemoryTest.Name = "kcmbMemoryTest";
            this.kcmbMemoryTest.Size = new System.Drawing.Size(121, 21);
            this.kcmbMemoryTest.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbMemoryTest.TabIndex = 2;
            // 
            // kbtnMemoryTest
            // 
            this.kbtnMemoryTest.Location = new System.Drawing.Point(207, 12);
            this.kbtnMemoryTest.Name = "kbtnMemoryTest";
            this.kbtnMemoryTest.Size = new System.Drawing.Size(180, 25);
            this.kbtnMemoryTest.TabIndex = 1;
            this.kbtnMemoryTest.Values.Text = "Memory Test";
            this.kbtnMemoryTest.Click += new System.EventHandler(this.kbtnMemoryTest_Click);
            // 
            // kbtnLoadRandomData
            // 
            this.kbtnLoadRandomData.Location = new System.Drawing.Point(13, 13);
            this.kbtnLoadRandomData.Name = "kbtnLoadRandomData";
            this.kbtnLoadRandomData.Size = new System.Drawing.Size(167, 25);
            this.kbtnLoadRandomData.TabIndex = 0;
            this.kbtnLoadRandomData.Values.Text = "Load &Random Data";
            this.kbtnLoadRandomData.Click += new System.EventHandler(this.kbtnLoadRandomData_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.statusStrip1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 622);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1108, 22);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMemoryUsage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(1108, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslMemoryUsage
            // 
            this.tsslMemoryUsage.Name = "tsslMemoryUsage";
            this.tsslMemoryUsage.Size = new System.Drawing.Size(116, 17);
            this.tsslMemoryUsage.Text = "Memory Usage: /Mb";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.ktxtTotalRows);
            this.kryptonPanel3.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 593);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(1108, 29);
            this.kryptonPanel3.TabIndex = 2;
            // 
            // ktxtTotalRows
            // 
            this.ktxtTotalRows.Location = new System.Drawing.Point(92, 3);
            this.ktxtTotalRows.Name = "ktxtTotalRows";
            this.ktxtTotalRows.Size = new System.Drawing.Size(100, 23);
            this.ktxtTotalRows.TabIndex = 1;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 6);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel3.TabIndex = 0;
            this.kryptonLabel3.Values.Text = "Total Rows:";
            // 
            // kryptonPanel4
            // 
            this.kryptonPanel4.Controls.Add(this.kadgvMain);
            this.kryptonPanel4.Controls.Add(this.kryptonAdvancedDataGridViewSearchToolBar1);
            this.kryptonPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel4.Location = new System.Drawing.Point(0, 189);
            this.kryptonPanel4.Name = "kryptonPanel4";
            this.kryptonPanel4.Size = new System.Drawing.Size(1108, 404);
            this.kryptonPanel4.TabIndex = 3;
            // 
            // kadgvMain
            // 
            this.kadgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kadgvMain.FilterAndSortEnabled = true;
            this.kadgvMain.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.kadgvMain.Location = new System.Drawing.Point(0, 27);
            this.kadgvMain.Name = "kadgvMain";
            this.kadgvMain.Size = new System.Drawing.Size(1108, 377);
            this.kadgvMain.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.kadgvMain.TabIndex = 1;
            this.kadgvMain.SortStringChanged += new System.EventHandler<Krypton.Utilities.KryptonAdvancedDataGridView.SortEventArgs>(this.kadgvMain_SortStringChanged);
            this.kadgvMain.FilterStringChanged += new System.EventHandler<Krypton.Utilities.KryptonAdvancedDataGridView.FilterEventArgs>(this.kadgvMain_FilterStringChanged);
            // 
            // kryptonAdvancedDataGridViewSearchToolBar1
            // 
            this.kryptonAdvancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.kryptonAdvancedDataGridViewSearchToolBar1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonAdvancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.kryptonAdvancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 0);
            this.kryptonAdvancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.kryptonAdvancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.kryptonAdvancedDataGridViewSearchToolBar1.Name = "kryptonAdvancedDataGridViewSearchToolBar1";
            this.kryptonAdvancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(1108, 27);
            this.kryptonAdvancedDataGridViewSearchToolBar1.TabIndex = 0;
            this.kryptonAdvancedDataGridViewSearchToolBar1.Text = "kryptonAdvancedDataGridViewSearchToolBar1";
            this.kryptonAdvancedDataGridViewSearchToolBar1.Search += new Krypton.Utilities.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.kryptonAdvancedDataGridViewSearchToolBar1_Search);
            // 
            // bsData
            // 
            this.bsData.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bsData_ListChanged);
            // 
            // AdvancedDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 644);
            this.Controls.Add(this.kryptonPanel4);
            this.Controls.Add(this.kryptonPanel3);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "AdvancedDataGridView";
            this.Text = "AdvancedDataGridView";
            this.Load += new System.EventHandler(this.AdvancedDataGridView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSortSaved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSavedFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbMemoryTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).EndInit();
            this.kryptonPanel4.ResumeLayout(false);
            this.kryptonPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kadgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private KryptonPanel kryptonPanel3;
        private KryptonPanel kryptonPanel4;
        private KryptonButton kbtnLoadRandomData;
        private KryptonButton kbtnMemoryTest;
        private KryptonComboBox kcmbMemoryTest;
        private KryptonLabel kryptonLabel1;
        private KryptonTextBox ktxtFilterString;
        private KryptonTextBox ktxtSortString;
        private KryptonLabel kryptonLabel2;
        private Krypton.Utilities.KryptonAdvancedDataGridViewSearchToolBar kryptonAdvancedDataGridViewSearchToolBar1;
        private Krypton.Utilities.KryptonAdvancedDataGridView kadgvMain;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslMemoryUsage;
        private KryptonLabel kryptonLabel3;
        private KryptonTextBox ktxtTotalRows;
        private KryptonLabel kryptonLabel4;
        private KryptonTextBox ktxtStringFilter;
        private KryptonLabel kryptonLabel5;
        private KryptonComboBox kcmbSortSaved;
        private KryptonComboBox kcmbSavedFilters;
        private KryptonLabel kryptonLabel6;
        private KryptonButton kbtnSaveFilters;
        private KryptonButton kbtnClearFilters;
        private KryptonButton kbtnApplySavedFilters;
        private BindingSource bsData;
    }
}