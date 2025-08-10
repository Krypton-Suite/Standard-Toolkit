namespace Krypton.Toolkit
{
    partial class KryptonOutlookGridFilterValues
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
            this.PnlMain = new Krypton.Toolkit.KryptonPanel();
            this.TpMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).BeginInit();
            this.PnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.AutoSize = true;
            this.PnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PnlMain.Controls.Add(this.TpMain);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(383, 23);
            this.PnlMain.TabIndex = 0;
            // 
            // TpMain
            // 
            this.TpMain.AutoSize = true;
            this.TpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TpMain.ColumnCount = 3;
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TpMain.Location = new System.Drawing.Point(0, 0);
            this.TpMain.Margin = new System.Windows.Forms.Padding(0);
            this.TpMain.Name = "TpMain";
            this.TpMain.RowCount = 1;
            this.TpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TpMain.Size = new System.Drawing.Size(0, 0);
            this.TpMain.TabIndex = 0;
            // 
            // FilterValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PnlMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FilterValues";
            this.Size = new System.Drawing.Size(383, 23);
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).EndInit();
            this.PnlMain.ResumeLayout(false);
            this.PnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonPanel PnlMain;
        private TableLayoutPanel TpMain;
    }
}
