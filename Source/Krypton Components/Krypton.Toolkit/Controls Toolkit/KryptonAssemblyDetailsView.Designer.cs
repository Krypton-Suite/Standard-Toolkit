namespace Krypton.Toolkit
{
    partial class KryptonAssemblyDetailsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kcmbAssemblyName = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kdgvAssemblyDetails = new Krypton.Toolkit.KryptonDataGridView();
            this.clmnAssemblyKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAssemblyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvAssemblyDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kcmbAssemblyName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonBorderEdge1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kdgvAssemblyDetails, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 408);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kcmbAssemblyName
            // 
            this.kcmbAssemblyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbAssemblyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbAssemblyName.DropDownWidth = 524;
            this.kcmbAssemblyName.IntegralHeight = false;
            this.kcmbAssemblyName.Location = new System.Drawing.Point(5, 5);
            this.kcmbAssemblyName.Margin = new System.Windows.Forms.Padding(5);
            this.kcmbAssemblyName.Name = "kcmbAssemblyName";
            this.kcmbAssemblyName.Size = new System.Drawing.Size(524, 21);
            this.kcmbAssemblyName.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbAssemblyName.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderSecondary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(5, 36);
            this.kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(524, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kdgvAssemblyDetails
            // 
            this.kdgvAssemblyDetails.AllowUserToAddRows = false;
            this.kdgvAssemblyDetails.AllowUserToDeleteRows = false;
            this.kdgvAssemblyDetails.AllowUserToResizeColumns = false;
            this.kdgvAssemblyDetails.AllowUserToResizeRows = false;
            this.kdgvAssemblyDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.kdgvAssemblyDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.kdgvAssemblyDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kdgvAssemblyDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnAssemblyKey,
            this.clmnValue});
            this.kdgvAssemblyDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdgvAssemblyDetails.Location = new System.Drawing.Point(5, 47);
            this.kdgvAssemblyDetails.Margin = new System.Windows.Forms.Padding(5);
            this.kdgvAssemblyDetails.Name = "kdgvAssemblyDetails";
            this.kdgvAssemblyDetails.Size = new System.Drawing.Size(524, 356);
            this.kdgvAssemblyDetails.TabIndex = 2;
            // 
            // clmnAssemblyKey
            // 
            this.clmnAssemblyKey.HeaderText = "Assembly Key";
            this.clmnAssemblyKey.Name = "clmnAssemblyKey";
            this.clmnAssemblyKey.Width = 109;
            // 
            // clmnValue
            // 
            this.clmnValue.HeaderText = "Value";
            this.clmnValue.Name = "clmnValue";
            this.clmnValue.Width = 64;
            // 
            // KryptonAssemblyDetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "KryptonAssemblyDetailsView";
            this.Size = new System.Drawing.Size(534, 408);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAssemblyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvAssemblyDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private KryptonComboBox kcmbAssemblyName;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonDataGridView kdgvAssemblyDetails;
        private DataGridViewTextBoxColumn clmnAssemblyKey;
        private DataGridViewTextBoxColumn clmnValue;
    }
}
