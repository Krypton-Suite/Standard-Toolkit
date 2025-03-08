namespace TestForm
{
    partial class PanelForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new Krypton.Toolkit.KryptonDataGridViewButtonColumn();
            this.Column2 = new Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.Column3 = new Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.Column4 = new Krypton.Toolkit.KryptonDataGridViewDateTimePickerColumn();
            this.Column5 = new Krypton.Toolkit.KryptonDataGridViewDomainUpDownColumn();
            this.Column6 = new Krypton.Toolkit.KryptonDataGridViewImageColumn();
            this.Column7 = new Krypton.Toolkit.KryptonDataGridViewLinkColumn();
            this.Column8 = new Krypton.Toolkit.KryptonDataGridViewMaskedTextBoxColumn();
            this.Column9 = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.Column10 = new Krypton.Toolkit.KryptonDataGridViewProgressColumn();
            this.Column11 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(106, 119);
            this.kryptonButton1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(68, 20);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "kryptonButton1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            this.dataGridView1.Location = new System.Drawing.Point(512, 150);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.FalseValue = null;
            this.Column2.HeaderText = "Column2";
            this.Column2.IndeterminateValue = null;
            this.Column2.Name = "Column2";
            this.Column2.TrueValue = null;
            // 
            // Column3
            // 
            this.Column3.DropDownWidth = 121;
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.Checked = false;
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.AllowDecimals = false;
            this.Column9.HeaderText = "Column9";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Column11";
            this.Column11.Name = "Column11";
            // 
            // PanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 636);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.kryptonButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.ImageStyle = Krypton.Toolkit.PaletteImageStyle.TopLeft;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "PanelForm";
            this.Text = "PanelForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonButton kryptonButton1;
        private DataGridView dataGridView1;
        private KryptonDataGridViewButtonColumn Column1;
        private KryptonDataGridViewCheckBoxColumn Column2;
        private KryptonDataGridViewComboBoxColumn Column3;
        private KryptonDataGridViewDateTimePickerColumn Column4;
        private KryptonDataGridViewDomainUpDownColumn Column5;
        private KryptonDataGridViewImageColumn Column6;
        private KryptonDataGridViewLinkColumn Column7;
        private KryptonDataGridViewMaskedTextBoxColumn Column8;
        private KryptonDataGridViewNumericUpDownColumn Column9;
        private KryptonDataGridViewProgressColumn Column10;
        private KryptonDataGridViewTextBoxColumn Column11;
    }
}