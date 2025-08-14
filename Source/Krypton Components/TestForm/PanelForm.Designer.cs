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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.Column12 = new Krypton.Toolkit.KryptonDataGridViewRatingColumn();
            this.kryptonPropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            this.kryptonOutlookGridAio1 = new Krypton.Toolkit.KryptonOutlookGridAio();
            this.kryptonOutlookGrid1 = new Krypton.Toolkit.KryptonOutlookGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGridAio1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGridAio1.OutlookGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGridAio1.Panel)).BeginInit();
            this.kryptonOutlookGridAio1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).BeginInit();
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
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kryptonDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column12});
            this.kryptonDataGridView1.Location = new System.Drawing.Point(263, 12);
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.Size = new System.Drawing.Size(240, 150);
            this.kryptonDataGridView1.TabIndex = 4;
            // 
            // Column12
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column12.HeaderText = "Column12";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.RatingMaximum = ((byte)(10));
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(535, 34);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.kryptonPropertyGrid1.SelectedObject = this.kryptonDataGridView1;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(392, 432);
            this.kryptonPropertyGrid1.TabIndex = 6;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.Location = new System.Drawing.Point(933, 34);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.SelectedObject = this.Column12;
            this.propertyGrid1.Size = new System.Drawing.Size(392, 432);
            this.propertyGrid1.TabIndex = 8;
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(118, 177);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton2.TabIndex = 9;
            this.kryptonButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton2.Values.Text = "kryptonButton2";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // kryptonOutlookGridAio1
            // 
            this.kryptonOutlookGridAio1.HeaderVisibleSecondary = false;
            this.kryptonOutlookGridAio1.Location = new System.Drawing.Point(41, 266);
            // 
            // 
            // 
            this.kryptonOutlookGridAio1.OutlookGrid.AllowDrop = true;
            this.kryptonOutlookGridAio1.OutlookGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kryptonOutlookGridAio1.OutlookGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonOutlookGridAio1.OutlookGrid.EnableSearchOnKeyPress = true;
            this.kryptonOutlookGridAio1.OutlookGrid.Location = new System.Drawing.Point(0, 0);
            this.kryptonOutlookGridAio1.OutlookGrid.Name = "OutlookGrid";
            this.kryptonOutlookGridAio1.OutlookGrid.ShowColumnFilter = true;
            this.kryptonOutlookGridAio1.OutlookGrid.Size = new System.Drawing.Size(148, 118);
            this.kryptonOutlookGridAio1.OutlookGrid.TabIndex = 0;
            this.kryptonOutlookGridAio1.ShowGroupBox = false;
            this.kryptonOutlookGridAio1.ShowSearchToolBar = false;
            this.kryptonOutlookGridAio1.Size = new System.Drawing.Size(150, 150);
            this.kryptonOutlookGridAio1.TabIndex = 11;
            this.kryptonOutlookGridAio1.ValuesPrimary.Image = null;
            this.kryptonOutlookGridAio1.ValuesSecondary.Heading = "";
            // 
            // kryptonOutlookGrid1
            // 
            this.kryptonOutlookGrid1.Border.Color1 = System.Drawing.Color.Red;
            this.kryptonOutlookGrid1.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonOutlookGrid1.Border.Width = 10;
            this.kryptonOutlookGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kryptonOutlookGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonOutlookGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.kryptonOutlookGrid1.Location = new System.Drawing.Point(246, 266);
            this.kryptonOutlookGrid1.Name = "kryptonOutlookGrid1";
            this.kryptonOutlookGrid1.Size = new System.Drawing.Size(240, 150);
            this.kryptonOutlookGrid1.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // PanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 450);
            this.Controls.Add(this.kryptonOutlookGrid1);
            this.Controls.Add(this.kryptonOutlookGridAio1);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.kryptonPropertyGrid1);
            this.Controls.Add(this.kryptonDataGridView1);
            this.Controls.Add(this.kryptonButton1);
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.ImageStyle = Krypton.Toolkit.PaletteImageStyle.TopLeft;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PanelForm";
            this.Text = "PanelForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGridAio1.OutlookGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGridAio1.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGridAio1)).EndInit();
            this.kryptonOutlookGridAio1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonButton kryptonButton1;
        private KryptonDataGridView kryptonDataGridView1;
        private System.Windows.Forms.PropertyGrid kryptonPropertyGrid1;
        private PropertyGrid propertyGrid1;
        private KryptonDataGridViewRatingColumn Column12;
        private KryptonButton kryptonButton2;
        private KryptonOutlookGridAio kryptonOutlookGridAio1;
        private KryptonOutlookGrid kryptonOutlookGrid1;
        private DataGridViewTextBoxColumn Column1;
    }
}