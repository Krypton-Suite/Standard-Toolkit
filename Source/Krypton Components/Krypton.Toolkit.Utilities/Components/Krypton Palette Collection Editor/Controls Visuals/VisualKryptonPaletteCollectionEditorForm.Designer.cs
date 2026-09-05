#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities
{
    partial class VisualKryptonPaletteCollectionEditorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualKryptonPaletteCollectionEditorForm));
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.tlpPath = new System.Windows.Forms.TableLayoutPanel();
            this.klblCollectionPath = new Krypton.Toolkit.KryptonLabel();
            this.ktxtCollectionPath = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnBrowse = new Krypton.Toolkit.KryptonButton();
            this.tlpName = new System.Windows.Forms.TableLayoutPanel();
            this.klblCollectionName = new Krypton.Toolkit.KryptonLabel();
            this.ktxtCollectionName = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnSaveName = new Krypton.Toolkit.KryptonButton();
            this.klblThemes = new Krypton.Toolkit.KryptonLabel();
            this.kbtnAdd = new Krypton.Toolkit.KryptonButton();
            this.kbtnRemove = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.klblViewBy = new Krypton.Toolkit.KryptonLabel();
            this.kcmbViewBy = new Krypton.Toolkit.KryptonComboBox();
            this.klvThemes = new Krypton.Toolkit.KryptonListView();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpPath.SuspendLayout();
            this.tlpName.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbViewBy)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.tlpMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(8);
            this.kpnlMain.Size = new System.Drawing.Size(704, 448);
            this.kpnlMain.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.kwlblInfo, 0, 0);
            this.tlpMain.Controls.Add(this.tlpPath, 0, 1);
            this.tlpMain.Controls.Add(this.tlpName, 0, 2);
            this.tlpMain.Controls.Add(this.klblThemes, 0, 3);
            this.tlpMain.Controls.Add(this.klblStatus, 0, 6);
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 5);
            this.tlpMain.Controls.Add(this.klvThemes, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(8, 8);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 7;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(688, 432);
            this.tlpMain.TabIndex = 0;
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInfo.Location = new System.Drawing.Point(3, 0);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(682, 72);
            this.kwlblInfo.Text = resources.GetString("kwlblInfo.Text");
            // 
            // tlpPath
            // 
            this.tlpPath.ColumnCount = 3;
            this.tlpPath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tlpPath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tlpPath.Controls.Add(this.klblCollectionPath, 0, 0);
            this.tlpPath.Controls.Add(this.ktxtCollectionPath, 1, 0);
            this.tlpPath.Controls.Add(this.kbtnBrowse, 2, 0);
            this.tlpPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPath.Location = new System.Drawing.Point(3, 75);
            this.tlpPath.Name = "tlpPath";
            this.tlpPath.RowCount = 1;
            this.tlpPath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPath.Size = new System.Drawing.Size(682, 30);
            this.tlpPath.TabIndex = 1;
            // 
            // klblCollectionPath
            // 
            this.klblCollectionPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblCollectionPath.Location = new System.Drawing.Point(3, 3);
            this.klblCollectionPath.Name = "klblCollectionPath";
            this.klblCollectionPath.Size = new System.Drawing.Size(82, 24);
            this.klblCollectionPath.TabIndex = 0;
            this.klblCollectionPath.Values.Text = "Collection file";
            // 
            // ktxtCollectionPath
            // 
            this.ktxtCollectionPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtCollectionPath.Location = new System.Drawing.Point(91, 3);
            this.ktxtCollectionPath.Name = "ktxtCollectionPath";
            this.ktxtCollectionPath.Size = new System.Drawing.Size(500, 23);
            this.ktxtCollectionPath.TabIndex = 0;
            // 
            // kbtnBrowse
            // 
            this.kbtnBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnBrowse.Location = new System.Drawing.Point(597, 3);
            this.kbtnBrowse.Name = "kbtnBrowse";
            this.kbtnBrowse.Size = new System.Drawing.Size(82, 24);
            this.kbtnBrowse.TabIndex = 1;
            this.kbtnBrowse.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnBrowse.Values.Text = "Browse...";
            // 
            // tlpName
            // 
            this.tlpName.ColumnCount = 3;
            this.tlpName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tlpName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tlpName.Controls.Add(this.klblCollectionName, 0, 0);
            this.tlpName.Controls.Add(this.ktxtCollectionName, 1, 0);
            this.tlpName.Controls.Add(this.kbtnSaveName, 2, 0);
            this.tlpName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpName.Location = new System.Drawing.Point(3, 111);
            this.tlpName.Name = "tlpName";
            this.tlpName.RowCount = 1;
            this.tlpName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpName.Size = new System.Drawing.Size(682, 30);
            this.tlpName.TabIndex = 2;
            // 
            // klblCollectionName
            // 
            this.klblCollectionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblCollectionName.Location = new System.Drawing.Point(3, 3);
            this.klblCollectionName.Name = "klblCollectionName";
            this.klblCollectionName.Size = new System.Drawing.Size(82, 24);
            this.klblCollectionName.TabIndex = 0;
            this.klblCollectionName.Values.Text = "Collection name";
            // 
            // ktxtCollectionName
            // 
            this.ktxtCollectionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtCollectionName.Location = new System.Drawing.Point(91, 3);
            this.ktxtCollectionName.Name = "ktxtCollectionName";
            this.ktxtCollectionName.Size = new System.Drawing.Size(500, 23);
            this.ktxtCollectionName.TabIndex = 2;
            // 
            // kbtnSaveName
            // 
            this.kbtnSaveName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnSaveName.Enabled = false;
            this.kbtnSaveName.Location = new System.Drawing.Point(597, 3);
            this.kbtnSaveName.Name = "kbtnSaveName";
            this.kbtnSaveName.Size = new System.Drawing.Size(82, 24);
            this.kbtnSaveName.TabIndex = 3;
            this.kbtnSaveName.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnSaveName.Values.Text = "Save name";
            // 
            // klblThemes
            // 
            this.klblThemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblThemes.Location = new System.Drawing.Point(3, 147);
            this.klblThemes.Name = "klblThemes";
            this.klblThemes.Size = new System.Drawing.Size(682, 18);
            this.klblThemes.TabIndex = 3;
            this.klblThemes.Values.Text = "Themes in collection";
            // 
            // kbtnAdd
            // 
            this.kbtnAdd.Location = new System.Drawing.Point(427, 3);
            this.kbtnAdd.Name = "kbtnAdd";
            this.kbtnAdd.Size = new System.Drawing.Size(80, 28);
            this.kbtnAdd.TabIndex = 5;
            this.kbtnAdd.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAdd.Values.Text = "Add...";
            // 
            // kbtnRemove
            // 
            this.kbtnRemove.Enabled = false;
            this.kbtnRemove.Location = new System.Drawing.Point(513, 3);
            this.kbtnRemove.Name = "kbtnRemove";
            this.kbtnRemove.Size = new System.Drawing.Size(80, 28);
            this.kbtnRemove.TabIndex = 6;
            this.kbtnRemove.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRemove.Values.Text = "Remove";
            // 
            // kbtnClose
            // 
            this.kbtnClose.Location = new System.Drawing.Point(599, 3);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(80, 28);
            this.kbtnClose.TabIndex = 7;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            // 
            // klblStatus
            // 
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 407);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(682, 22);
            this.klblStatus.TabIndex = 6;
            this.klblStatus.Values.Text = "Choose a .ktheme collection, then add .kthemex files.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.kbtnAdd, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnClose, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnRemove, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.klblViewBy, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kcmbViewBy, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 367);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(682, 34);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // klblViewBy
            // 
            this.klblViewBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblViewBy.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblViewBy.Location = new System.Drawing.Point(5, 5);
            this.klblViewBy.Margin = new System.Windows.Forms.Padding(5);
            this.klblViewBy.Name = "klblViewBy";
            this.klblViewBy.Size = new System.Drawing.Size(94, 24);
            this.klblViewBy.TabIndex = 8;
            this.klblViewBy.Values.Text = "View By:";
            // 
            // kcmbViewBy
            // 
            this.kcmbViewBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbViewBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbViewBy.Location = new System.Drawing.Point(109, 5);
            this.kcmbViewBy.Margin = new System.Windows.Forms.Padding(5);
            this.kcmbViewBy.Name = "kcmbViewBy";
            this.kcmbViewBy.Size = new System.Drawing.Size(310, 24);
            this.kcmbViewBy.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbViewBy.TabIndex = 9;
            this.kcmbViewBy.SelectedIndexChanged += new System.EventHandler(this.kcmbViewBy_SelectedIndexChanged);
            // 
            // klvThemes
            // 
            this.klvThemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klvThemes.HideSelection = false;
            this.klvThemes.Location = new System.Drawing.Point(3, 171);
            this.klvThemes.Name = "klvThemes";
            this.klvThemes.Size = new System.Drawing.Size(682, 190);
            this.klvThemes.TabIndex = 9;
            // 
            // VisualKryptonPaletteCollectionEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 448);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(560, 360);
            this.Name = "VisualKryptonPaletteCollectionEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Krypton Palette Collection Editor";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpPath.ResumeLayout(false);
            this.tlpPath.PerformLayout();
            this.tlpName.ResumeLayout(false);
            this.tlpName.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbViewBy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private System.Windows.Forms.TableLayoutPanel tlpPath;
        private Krypton.Toolkit.KryptonLabel klblCollectionPath;
        private Krypton.Toolkit.KryptonTextBox ktxtCollectionPath;
        private Krypton.Toolkit.KryptonButton kbtnBrowse;
        private System.Windows.Forms.TableLayoutPanel tlpName;
        private Krypton.Toolkit.KryptonLabel klblCollectionName;
        private Krypton.Toolkit.KryptonTextBox ktxtCollectionName;
        private Krypton.Toolkit.KryptonButton kbtnSaveName;
        private Krypton.Toolkit.KryptonLabel klblThemes;
        private Krypton.Toolkit.KryptonButton kbtnAdd;
        private Krypton.Toolkit.KryptonButton kbtnRemove;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonLabel klblViewBy;
        private KryptonComboBox kcmbViewBy;
        private KryptonListView klvThemes;
    }
}
