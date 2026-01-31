namespace Krypton.Toolkit
{
    partial class InternalSearchableExceptionTreeView
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ktxtSearchBox = new Krypton.Toolkit.KryptonTextBox();
            this.bsaClearSearch = new Krypton.Toolkit.ButtonSpecAny();
            this.kietvException = new Krypton.Toolkit.InternalExceptionTreeView();
            this.kwlblSearchResults = new Krypton.Toolkit.KryptonWrapLabel();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.ktxtSearchBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.kietvException, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.kwlblSearchResults, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(299, 485);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // ktxtSearchBox
            // 
            this.ktxtSearchBox.ButtonSpecs.Add(this.bsaClearSearch);
            this.ktxtSearchBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ktxtSearchBox.Location = new System.Drawing.Point(5, 5);
            this.ktxtSearchBox.Margin = new System.Windows.Forms.Padding(5);
            this.ktxtSearchBox.Name = "ktxtSearchBox";
            this.ktxtSearchBox.Size = new System.Drawing.Size(289, 23);
            this.ktxtSearchBox.TabIndex = 7;
            this.ktxtSearchBox.TextChanged += new System.EventHandler(this.ktxtSearchBox_TextChanged);
            // 
            // bsaClearSearch
            // 
            this.bsaClearSearch.Style = Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.bsaClearSearch.Type = Krypton.Toolkit.PaletteButtonSpecStyle.PendantClose;
            this.bsaClearSearch.UniqueName = "7bfa2bfe1deb43dd97ae731f010e3baf";
            this.bsaClearSearch.Visible = false;
            this.bsaClearSearch.Click += new System.EventHandler(this.bsaClearSearch_Click);
            // 
            // kietvException
            // 
            this.kietvException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kietvException.Location = new System.Drawing.Point(5, 38);
            this.kietvException.Margin = new System.Windows.Forms.Padding(5);
            this.kietvException.Name = "kietvException";
            this.kietvException.Size = new System.Drawing.Size(289, 397);
            this.kietvException.TabIndex = 1;
            // 
            // kwlblSearchResults
            // 
            this.kwlblSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblSearchResults.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblSearchResults.Location = new System.Drawing.Point(5, 445);
            this.kwlblSearchResults.Margin = new System.Windows.Forms.Padding(5);
            this.kwlblSearchResults.Name = "kwlblSearchResults";
            this.kwlblSearchResults.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblSearchResults.Size = new System.Drawing.Size(289, 35);
            this.kwlblSearchResults.Text = "kryptonWrapLabel1";
            this.kwlblSearchResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InternalSearchableExceptionTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "InternalSearchableExceptionTreeView";
            this.Size = new System.Drawing.Size(299, 485);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel2;
        private InternalExceptionTreeView kietvException;
        private KryptonTextBox ktxtSearchBox;
        private ButtonSpecAny bsaClearSearch;
        private KryptonWrapLabel kwlblSearchResults;
    }
}
