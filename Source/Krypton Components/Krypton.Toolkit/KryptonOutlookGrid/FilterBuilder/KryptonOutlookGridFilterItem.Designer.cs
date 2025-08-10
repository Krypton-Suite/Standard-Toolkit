namespace Krypton.Toolkit
{
    partial class KryptonOutlookGridFilterItem
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
            this.components = new System.ComponentModel.Container();
            this.PnlMain = new Krypton.Toolkit.KryptonPanel();
            this.TpMain = new System.Windows.Forms.TableLayoutPanel();
            this.ColumnsList = new Krypton.Toolkit.KryptonComboBox();
            this.OperatorsList = new Krypton.Toolkit.KryptonComboBox();
            this.FilterMenu = new Krypton.Toolkit.KryptonOutlookGridFilterItemMenuButton();
            this.filterValues1 = new Krypton.Toolkit.KryptonOutlookGridFilterValues();
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).BeginInit();
            this.PnlMain.SuspendLayout();
            this.TpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperatorsList)).BeginInit();
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
            this.PnlMain.Size = new System.Drawing.Size(791, 25);
            this.PnlMain.TabIndex = 0;
            // 
            // TpMain
            // 
            this.TpMain.AutoSize = true;
            this.TpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TpMain.BackColor = System.Drawing.Color.Transparent;
            this.TpMain.ColumnCount = 4;
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TpMain.Controls.Add(this.ColumnsList, 0, 0);
            this.TpMain.Controls.Add(this.OperatorsList, 1, 0);
            this.TpMain.Controls.Add(this.FilterMenu, 3, 0);
            this.TpMain.Controls.Add(this.filterValues1, 2, 0);
            this.TpMain.Location = new System.Drawing.Point(0, 0);
            this.TpMain.Margin = new System.Windows.Forms.Padding(0);
            this.TpMain.Name = "TpMain";
            this.TpMain.RowCount = 1;
            this.TpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TpMain.Size = new System.Drawing.Size(791, 25);
            this.TpMain.TabIndex = 0;
            // 
            // ColumnsList
            // 
            this.ColumnsList.DropDownWidth = 10;
            this.ColumnsList.IntegralHeight = false;
            this.ColumnsList.Location = new System.Drawing.Point(1, 1);
            this.ColumnsList.Margin = new System.Windows.Forms.Padding(1);
            this.ColumnsList.Name = "ColumnsList";
            this.ColumnsList.Size = new System.Drawing.Size(150, 22);
            this.ColumnsList.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ColumnsList.TabIndex = 0;
            this.ColumnsList.SelectedValueChanged += new System.EventHandler(this.ColumnsList_SelectedValueChanged);
            // 
            // OperatorsList
            // 
            this.OperatorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperatorsList.DropDownWidth = 10;
            this.OperatorsList.IntegralHeight = false;
            this.OperatorsList.Location = new System.Drawing.Point(153, 1);
            this.OperatorsList.Margin = new System.Windows.Forms.Padding(1);
            this.OperatorsList.Name = "OperatorsList";
            this.OperatorsList.Size = new System.Drawing.Size(200, 22);
            this.OperatorsList.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.OperatorsList.TabIndex = 0;
            this.OperatorsList.SelectedIndexChanged += new System.EventHandler(this.OperatorsList_SelectedIndexChanged);
            // 
            // FilterMenu
            // 
            this.FilterMenu.Location = new System.Drawing.Point(740, 1);
            this.FilterMenu.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.FilterMenu.Name = "FilterMenu";
            this.FilterMenu.Size = new System.Drawing.Size(50, 23);
            this.FilterMenu.Splitter = false;
            this.FilterMenu.TabIndex = 2;
            this.FilterMenu.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.FilterMenu.Values.Text = "End";
            // 
            // filterValues1
            // 
            this.filterValues1.Location = new System.Drawing.Point(354, 0);
            this.filterValues1.Margin = new System.Windows.Forms.Padding(0);
            this.filterValues1.Name = "filterValues1";
            this.filterValues1.Size = new System.Drawing.Size(383, 23);
            this.filterValues1.TabIndex = 3;
            // 
            // FilterItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.PnlMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FilterItem";
            this.Size = new System.Drawing.Size(791, 25);
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).EndInit();
            this.PnlMain.ResumeLayout(false);
            this.PnlMain.PerformLayout();
            this.TpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColumnsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperatorsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonPanel PnlMain;
        private TableLayoutPanel TpMain;
        private KryptonComboBox ColumnsList;
        private KryptonComboBox OperatorsList;
        private KryptonOutlookGridFilterItemMenuButton FilterMenu;
        private KryptonOutlookGridFilterValues filterValues1;
    }
}
