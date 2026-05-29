namespace Krypton.Toolkit
{
    partial class InternalAssemblyDetails
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.kcbAssembly = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kdgvAssemblyDetails = new Krypton.Toolkit.KryptonDataGridView();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcbAssembly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvAssemblyDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.kcbAssembly, 0, 0);
            this.tlpMain.Controls.Add(this.kryptonBorderEdge1, 0, 1);
            this.tlpMain.Controls.Add(this.kdgvAssemblyDetails, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(635, 488);
            this.tlpMain.TabIndex = 0;
            // 
            // kcbAssembly
            // 
            this.kcbAssembly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcbAssembly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcbAssembly.DropDownWidth = 625;
            this.kcbAssembly.IntegralHeight = false;
            this.kcbAssembly.Location = new System.Drawing.Point(5, 5);
            this.kcbAssembly.Margin = new System.Windows.Forms.Padding(5);
            this.kcbAssembly.Name = "kcbAssembly";
            this.kcbAssembly.Size = new System.Drawing.Size(625, 21);
            this.kcbAssembly.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcbAssembly.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderSecondary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(5, 36);
            this.kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(625, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kdgvAssemblyDetails
            // 
            this.kdgvAssemblyDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kdgvAssemblyDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdgvAssemblyDetails.Location = new System.Drawing.Point(5, 47);
            this.kdgvAssemblyDetails.Margin = new System.Windows.Forms.Padding(5);
            this.kdgvAssemblyDetails.Name = "kdgvAssemblyDetails";
            this.kdgvAssemblyDetails.Size = new System.Drawing.Size(625, 436);
            this.kdgvAssemblyDetails.TabIndex = 2;
            // 
            // InternalAssemblyDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tlpMain);
            this.Name = "InternalAssemblyDetails";
            this.Size = new System.Drawing.Size(635, 488);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcbAssembly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvAssemblyDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tlpMain;
        private KryptonComboBox kcbAssembly;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonDataGridView kdgvAssemblyDetails;
    }
}
